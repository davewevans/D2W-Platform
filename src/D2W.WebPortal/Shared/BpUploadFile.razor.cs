namespace D2W.WebPortal.Shared;

public partial class BpUploadFile
{
    #region Public Properties

    [Parameter] public bool AllowRename { get; set; }
    [Parameter] public bool AllowRemove { get; set; }
    [Parameter] public string AllowedExtensions { get; set; }
    [Parameter] public string ButtonId { get; set; }
    [Parameter] public string ButtonName { get; set; }
    [Parameter] public string ButtonIcon { get; set; }
    [Parameter] public long MaxFileSize { get; set; } = 1073741824; // 1073741824 Bytes = 1 Gigabyte
    [Parameter] public EventCallback<string> OnImageUpload { get; set; }
    [Parameter] public EventCallback<StreamContent> OnFileSelected { get; set; }
    [Parameter] public EventCallback<StreamContent> OnFileUnSelected { get; set; }

    #endregion Public Properties

    #region Private Properties

    private bool IsHidden { get; set; }
    private bool IsFileUploadCancelled { get; set; }
    private bool AllowCancel { get; set; }
    private bool AllowClear { get; set; }
    private bool IsFileInfoBoxHidden { get; set; } = true;
    private string ImageSrc { get; set; }
    private string NewFileName { get; set; }
    private string FileExtension { get; set; }
    private long MinProgressValue { get; set; }
    private long ProgressValue { get; set; }
    private FileUploadMetaData FileUploadMetaData { get; set; }
    private StreamContent StreamContent { get; set; }
    private CancellationTokenSource CancellationTokenSource { get; set; }
    private IList<IBrowserFile> BrowserFiles { get; set; } = new List<IBrowserFile>();
    [Inject] private ISnackbar Snackbar { get; set; }

    [Inject] private IDialogService DialogService { get; set; }

    #endregion Private Properties

    #region Public Methods

    public async Task ClearFile()
    {
        await Reinitialize();

        if (StreamContent != null)
        {
            await OnFileUnSelected.InvokeAsync(StreamContent);
        }

        AllowClear = false;
    }

    public async Task CancelFileUpload()
    {
        CancellationTokenSource.Cancel();
        await Reinitialize();
    }

    public async Task HideFileUploadComponent()
    {
        IsHidden = !IsHidden;

        await Reinitialize();

        if (StreamContent != null)
        {
            await OnFileUnSelected.InvokeAsync(StreamContent);
        }
    }

    public async Task Reinitialize()
    {
        BrowserFiles.Clear();
        await ResetValues();
        await InvokeAsync(StateHasChanged);
    }

    public async Task ResetValues()
    {
        FileUploadMetaData = new FileUploadMetaData();
        MinProgressValue = 0;
        ProgressValue = 0;
        IsFileInfoBoxHidden = true;
        IsFileUploadCancelled = false;
        AllowCancel = false;
        AllowClear = false;
        ImageSrc = null;
        await OnImageUpload.InvokeAsync(ImageSrc);
        await InvokeAsync(StateHasChanged);
    }

    public async Task ConvertToStream()
    {
        if (!IsFileUploadCancelled)
        {
            var fileExtension = $".{BrowserFiles[0].Name.Split(".").Last()}";
            if (!IsAllowFileExtension(fileExtension))
            {
                Snackbar.Add(Resource.The_selected_file_extension_is_not_allowed, Severity.Error);
                await Reinitialize();
                return;
            }

            if (BrowserFiles[0].Size == 0)
            {
                Snackbar.Add(Resource.Unable_to_upload_an_empty_file, Severity.Error);
                await Reinitialize();
                return;
            }

            MinProgressValue = BrowserFiles[0].Size;

            AllowCancel = true;

            await InvokeAsync(StateHasChanged);

            FileUploadMetaData.Name = NewFileName;

            FileUploadMetaData.Type = BrowserFiles[0].ContentType;

            FileUploadMetaData.Size = $"{BrowserFiles[0].Size} bytes";

            StreamContent = new StreamContent(BrowserFiles[0].OpenReadStream(MaxFileSize));

            StreamContent.Headers.Add("FileName", NewFileName);

            await OnFileSelected.InvokeAsync(StreamContent);

            ProgressValue = MinProgressValue;

            IsFileInfoBoxHidden = false;

            AllowClear = true;

            AllowCancel = false;

            await InvokeAsync(StateHasChanged);
        }
        else
        {
            await Reinitialize();
        }
    }

    public async Task<bool> RenameFile(string safeFileName)
    {
        var parameters = new DialogParameters { ["FileName"] = safeFileName };

        var dialog = DialogService.Show<FileUploadDialog>(Resource.Please_provide_new_name_for_the_file_or_leave_it_as_is, parameters);

        var result = await dialog.Result;

        if (result.Cancelled)
            return false;

        if (string.IsNullOrWhiteSpace(result.Data.ToString()))
        {
            Snackbar.Add(Resource.Invalid_file_name, Severity.Error);
            IsFileUploadCancelled = true;
            return false;
        }

        safeFileName = Path.GetFileNameWithoutExtension(result.Data.ToString());

        if (safeFileName != null)
        {
            safeFileName = safeFileName.Replace(".", "_");

            NewFileName = safeFileName;
        }

        NewFileName += FileExtension;

        return true;
    }

    #endregion Public Methods

    #region Protected Methods

    protected override void OnInitialized()
    {
        var random = new Random();
        ButtonId += random.Next(1111111, 9999999).ToString();
    }

    #endregion Protected Methods

    #region Private Methods

    private async Task UploadFiles(InputFileChangeEventArgs e)
    {
        BrowserFiles.Clear();

        foreach (var file in e.GetMultipleFiles())
        {
            BrowserFiles.Add(file);
        }
        if (MaxFileSize != 0 && BrowserFiles[0].Size > MaxFileSize)
        {
            Snackbar.Add(Resource.The_selected_file_exceeds_the_allowed_maximum_size_limit, Severity.Error);
            await Reinitialize();
            return;
        }
        await HandleFileSelection();

        await GetBase64StringImageUrl(e);
    }

    private async Task<string> GetBase64StringImageUrl(InputFileChangeEventArgs eventArgs)
    {
        var imageFile = eventArgs.GetMultipleFiles(999999999).FirstOrDefault();

        if (imageFile != null && !imageFile.ContentType.Contains("image"))
            return string.Empty;

        var format = imageFile.ContentType;

        var resizedImageFile = await imageFile.RequestImageFileAsync(format, 300, 300);

        var buffer = new byte[resizedImageFile.Size];

        await resizedImageFile.OpenReadStream().ReadAsync(buffer);

        ImageSrc = $"data:{format};base64,{Convert.ToBase64String(buffer)}";

        await OnImageUpload.InvokeAsync(ImageSrc);

        return ImageSrc;
    }

    private async Task HandleFileSelection()
    {
        await ResetValues();

        CancellationTokenSource?.Dispose();

        CancellationTokenSource = new CancellationTokenSource();

        IsFileInfoBoxHidden = true;

        try
        {
            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(BrowserFiles[0].Name);

            FileExtension = Path.GetExtension(BrowserFiles[0].Name);

            var safeFileName = fileNameWithoutExtension.Replace(".", "_");

            NewFileName = safeFileName; //Initialize File Name.

            if (AllowRename)
            {
                if (await RenameFile(safeFileName))
                {
                    await ConvertToStream();
                }
                else
                {
                    await Reinitialize();
                }
            }
            else
            {
                NewFileName += FileExtension;

                await ConvertToStream();
            }
        }
        catch (OperationCanceledException)
        {
            await InvokeAsync(StateHasChanged);

            await Reinitialize();
        }
    }

    private bool IsAllowFileExtension(string fileExtension)
    {
        if (!string.IsNullOrWhiteSpace(AllowedExtensions))
        {
            return !string.IsNullOrWhiteSpace(fileExtension) && AllowedExtensions.ToUpper().Split(",").ToArray().Contains(fileExtension.ToUpper());
        }

        return true; //Which means allowed for all extensions.
    }

    #endregion Private Methods
}