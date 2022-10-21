namespace D2W.WebPortal.Shared;

public partial class ServerSideValidator
{
    #region Public Properties

    [Inject] public ISnackbar Snackbar { get; set; }

    public ExceptionResult ExceptionResult { get; set; } = new ExceptionResult();

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