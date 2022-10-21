namespace D2W.WebPortal.Shared;

public partial class MultipleFileUpload
{
    #region Public Properties

    [Parameter] public string AllowedExtensions { get; set; }

    [Parameter] public long MaxFileSize { get; set; }

    [Parameter] public bool AllowRename { get; set; }

    [Parameter] public bool AllowRemove { get; set; }

    [Parameter] public EventCallback<StreamContent> OnFileSelected { get; set; }

    [Parameter] public EventCallback<StreamContent> OnFileUnSelected { get; set; }

    [Parameter] public string ButtonName { get; set; }
    [Parameter] public string ButtonIcon { get; set; }

    #endregion Public Properties

    #region Private Properties

    private int CurrentCount { get; set; }
    private List<int> List { get; set; }

    #endregion Private Properties

    #region Protected Methods

    protected override void OnInitialized()
    {
        List = new List<int>();
    }

    protected void AddBpUploadFileComponent()
    {
        List.Add(CurrentCount++);
    }

    #endregion Protected Methods
}