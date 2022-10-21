namespace D2W.HostApp.Shared;

public partial class ServerSideValidator
{
    #region Public Properties

    [Inject] public ISnackbar Snackbar { get; set; }

    public ExceptionResult ExceptionResult { get; set; } = new();

    #endregion Public Properties

    #region Private Properties

    private bool IsError { get; set; }

    #endregion Private Properties

    #region Public Methods

    public void Validate(ExceptionResult exceptionResult)
    {
        IsError = true;
        ExceptionResult = exceptionResult;
        StateHasChanged();
    }

    public void Invalidate()
    {
        IsError = false;
        ExceptionResult = new ExceptionResult();
        StateHasChanged();
    }

    #endregion Public Methods
}