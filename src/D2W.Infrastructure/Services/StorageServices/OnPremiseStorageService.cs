namespace D2W.Infrastructure.Services.StorageServices;

public class OnPremiseStorageService : IFileStorageService
{
    #region Private Fields

    private readonly IWebHostEnvironment _env;
    private readonly IHttpContextAccessor _httpContextAccessor;

    #endregion Private Fields

    #region Public Constructors

    public OnPremiseStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    #endregion Public Constructors

    #region Public Methods

    public async Task<string> UploadFile(IFormFile formFile, string containerName, string fileNamePrefix)
    {
        if (formFile is { Length: > 0 })
        {
            try
            {
                var directory = Path.Combine(_env.WebRootPath, containerName);

                Directory.CreateDirectory(directory).CreateSubdirectory(fileNamePrefix);

                var fileName = $"{fileNamePrefix}-{formFile.FileName.ReplaceSpaceAndSpecialCharsWithDashes()}";

                var physicalPath = Path.Combine(Path.Combine(directory, fileNamePrefix), fileName);

                await using var stream = File.Create(physicalPath);

                await formFile.CopyToAsync(stream);

                var host = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

                var url = $"{host}/{containerName}/{fileNamePrefix}/{fileName}";

                return url;
            }
            catch (Exception)
            {
                throw new Exception(Resource.File_has_not_been_uploaded);
            }
        }
        else
        {
            return null;
        }
    }

    public async Task<List<FileMetaData>> UploadMultipleFiles(IList<IFormFile> formFiles, string containerName, string fileNamePrefix, int defaultFileIndex = 0, string subContainerName = "attachments")
    {
        containerName = containerName.ReplaceSpaceAndSpecialCharsWithDashes();
        fileNamePrefix = fileNamePrefix.ReplaceSpaceAndSpecialCharsWithDashes();
        subContainerName = subContainerName.ReplaceSpaceAndSpecialCharsWithDashes();

        var filePaths = new List<FileMetaData>();

        if (formFiles == null || formFiles.Count == 0)
            return new List<FileMetaData>();

        var directory = Path.Combine(_env.WebRootPath, containerName);

        Directory.CreateDirectory(directory).CreateSubdirectory(fileNamePrefix).CreateSubdirectory(subContainerName);

        foreach (var formFile in formFiles.Select((value, index) => new { Index = index, Value = value }))
        {
            if (formFile.Value.Length > 0)
            {
                try
                {
                    var fileName = $"{fileNamePrefix}-{formFile.Value.FileName.ReplaceSpaceAndSpecialCharsWithDashes()}";

                    var physicalPath = Path.Combine(Path.Combine(directory, Path.Combine(fileNamePrefix, subContainerName), fileName));

                    await using (var stream = File.Create(physicalPath))
                    {
                        await formFile.Value.CopyToAsync(stream);
                    }

                    var host = $"{_httpContextAccessor.HttpContext?.Request.Scheme}://{_httpContextAccessor.HttpContext?.Request.Host}";

                    var url = $"{host}/{containerName}/{fileNamePrefix}/{subContainerName}/{fileName}";

                    filePaths.Add(new FileMetaData { FileName = fileName, FileUri = url, IsDefault = defaultFileIndex == formFile.Index });
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

    public Task DeleteFileIfExists(string fileUri)
    {
        if (!string.IsNullOrEmpty(fileUri))
        {
            var uri = new Uri(fileUri);
            var localPath = Path.Combine(_env.WebRootPath, uri.AbsolutePath.TrimStart('/'));//TrimStart('/') in order to keep WebRootPath

            if (File.Exists(localPath))
                File.Delete(localPath);
        }
        return Task.FromResult(0);
    }

    public Task DeleteContainer(string containerName, string subContainerName)
    {
        var pathToDelete = Path.Combine(_env.WebRootPath, Path.Combine(containerName, subContainerName));

        if (Directory.Exists(pathToDelete))
            Directory.Delete(pathToDelete, true);

        return Task.FromResult(0);
    }

    public FileStatus GetFileState(IFormFile formFile, string oldUrl)
    {
        if (formFile is not null or { Length: > 0 })
            return FileStatus.Modified;

        return !string.IsNullOrWhiteSpace(oldUrl) ? FileStatus.Unchanged : FileStatus.Deleted;
    }

    #endregion Public Methods
}