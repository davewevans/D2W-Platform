namespace D2W.WebPortal.Pages.Account.Manage;

public partial class ConfirmEmailChange
{
    #region Private Fields

    private string _userId;
    private string _email;
    private string _code;

    #endregion Private Fields

    #region Private Properties

    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private ConfirmEmailChangeCommand ConfirmEmailChangeCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        NavigationManager.TryGetQueryString("userId", out _userId);

        NavigationManager.TryGetQueryString("email", out _email);

        NavigationManager.TryGetQueryString("code", out _code);

        ConfirmEmailChangeCommand = new ConfirmEmailChangeCommand
        {
            Code = _code,
            UserId = _userId,
            Email = _email
        };

        var httpResponseWrapper = await ManageClient.ConfirmEmailChange(ConfirmEmailChangeCommand);

        if (httpResponseWrapper.Success)
        {
            NavigationManager.NavigateTo("account/manage/emailChangeConfirmed");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods
}