namespace D2W.Infrastructure.Services.StorageServices;

public class AzureStorageService : IFileStorageService
{
    #region Private Fields

    private readonly string _connectionString;

    #endregion Private Fields

    #region Public Constructors

    public AzureStorageService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("AzureStorageConnection");
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<string> UploadFile(IFormFile formFile, string containerName, string fileNamePrefix)
    {
        if (formFile is not { Length: > 0 })
            return null;

        try
        {
            var fileName = $"{fileNamePrefix}-{formFile.FileName.ReplaceSpaceAndSpecialCharsWithDashes()}";
            var blobName = $"{fileNamePrefix}/{fileNamePrefix}-{fileName}";

            // Get a reference to a container named "users" and then create it
            var container = new BlobContainerClient(_connectionString, containerName);
            await container.CreateIfNotExistsAsync();
            await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            // Get a reference to a blob named "user-file" in a container named "users"
            var blob = container.GetBlobClient(blobName);

            // Upload local file
            await blob.UploadAsync(formFile.OpenReadStream(), new BlobHttpHeaders
            {
                ContentType = formFile.ContentType
            });
            return blob.Uri.ToString();
        }
        catch
        {
            throw new Exception(Resource.File_has_not_been_uploaded);
        }
    }

    public async Task<List<FileMetaData>> UploadMultipleFiles(IList<IFormFile> formFiles, string containerName, string fileNamePrefix, int defaultFileIndex = 0, string subContainerName = "attachments")
    {
        containerName = containerName.ReplaceSpaceAndSpecialCharsWithDashes().ToLower();
        fileNamePrefix = fileNamePrefix.ReplaceSpaceAndSpecialCharsWithDashes().ToLower();
        subContainerName = subContainerName.ReplaceSpaceAndSpecialCharsWithDashes().ToLower();

        var filePaths = new List<FileMetaData>();

        if (formFiles == null || formFiles.Count == 0)
            return new List<FileMetaData>();

        foreach (var formFile in formFiles.Select((value, index) => new { Index = index, Value = value }))
        {
            if (formFile.Value.Length > 0)
            {
                try
                {
                    fileNamePrefix = fileNamePrefix.Replace(".", "");

                    var fileName = $"{fileNamePrefix}-{formFile.Value.FileName}".ReplaceSpaceAndSpecialCharsWithDashes();
                    var blobName = $"{fileNamePrefix}/{fileNamePrefix}-{subContainerName}/{fileName}";

                    // Get a reference to a container named "user/attachments" and then create it
                    var container = new BlobContainerClient(_connectionString, $"{containerName}");
                    await container.CreateIfNotExistsAsync();
                    await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

                    // Get a reference to a blob named "user-file" in a container named "users"
                    var blob = container.GetBlobClient(blobName);

                    await blob.UploadAsync(formFile.Value.OpenReadStream(), new BlobHttpHeaders
                    {
                        ContentType = formFile.Value.ContentType
                    });

                    filePaths.Add(new FileMetaData
                    {
                        FileName = fileName,
                        FileUri = blob.Uri.ToString(),
                        IsDefault = defaultFileIndex == formFile.Index
                    });
                }
                catch
                {
                    throw new Exception(Resource.File_has_not_been_uploaded);
                }
            }
            else
            {
                throw new Exception(Resource.File_is_empty);
            }
        }
        return filePaths;
    }

    public async Task<string> EditFile(IFormFile formFile, string containerName, string fileNamePrefix, string oldFileUri)
    {
        if (formFile == null)
            return oldFileUri;

        if (!string.IsNullOrEmpty(oldFileUri))
            await DeleteFileIfExists(oldFileUri);

        return await UploadFile(formFile, containerName, fileNamePrefix);
    }

    public async Task DeleteFileIfExists(string fileUri)
    {
        if (!string.IsNullOrEmpty(fileUri))
        {
            var container = new BlobContainerClient(_connectionString, "users");

            if (await container.ExistsAsync())
            {
                await container.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
                var fileUriArray = fileUri.Split('/').ToArray();
                var fileName = fileUriArray.LastOrDefault();
                await container.DeleteBlobIfExistsAsync(fileName, DeleteSnapshotsOption.IncludeSnapshots);
            }
        }
    }

    public async Task DeleteContainer(string containerName, string subContainer)
    {
        var container = new BlobContainerClient(_connectionString, $"{containerName}");
        await ListBlobsForPrefixRecursive(container, subContainer, 0);
    }

    public FileStatus GetFileState(IFormFile formFile, string oldUrl)
    {
        if (formFile is not null or { Length: > 0 })
            return FileStatus.Modified;

        return !string.IsNullOrWhiteSpace(oldUrl) ? FileStatus.Unchanged : FileStatus.Deleted;
    }

    #endregion Public Methods

    #region Private Methods

    private static async Task ListBlobsForPrefixRecursive(BlobContainerClient container, string prefix, int level)
    {
        await foreach (var page in container.GetBlobsByHierarchyAsync(prefix: prefix, delimiter: "/").AsPages())
        {
            foreach (var blob in page.Values.Where(item => item.IsBlob).Select(item => item.Blob))
                await container.DeleteBlobIfExistsAsync(blob.Name);

            var prefixes = page.Values.Where(item => item.IsPrefix).Select(item => item.Prefix);

            foreach (var p in prefixes)
                await ListBlobsForPrefixRecursive(container, p, level + 1);
        }
    }

    #endregion Private Methods
}