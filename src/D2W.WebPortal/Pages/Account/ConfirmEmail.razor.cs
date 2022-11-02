namespace D2W.WebPortal.Pages.Account;

public partial class ConfirmEmail
{
    #region Private Fields

    private string _userId;
    private string _code;

    private string _tenantName;
    private string _returnUrl;

    #endregion Private Fields

    #region Private Properties

    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private IAccountsClient AccountsClient { get; set; }

    [Inject ] private IAuthenticationService AuthService {get; set; }
    
    private ServerSideValidator ServerSideValidator { get; set; }
    private ConfirmEmailCommand ConfirmEmailCommand { get; set; } = new();

    #endregion Private Properties

    #region Protected Methods

    protected override async Task OnInitializedAsync()
    {
        NavigationManager.TryGetQueryString("userId", out _userId);

        NavigationManager.TryGetQueryString("code", out _code);

        NavigationManager.TryGetQueryString("tenantName", out _tenantName);

        NavigationManager.TryGetQueryString("returnUrl", out _returnUrl);

        System.Console.WriteLine("userId: " + _userId);
        System.Console.WriteLine("code: " + _code);
        System.Console.WriteLine("tenantName: " + _tenantName);

        if (!string.IsNullOrWhiteSpace(_tenantName)) {
            await AuthService.StoreTenantName(_tenantName);
        }

        ConfirmEmailCommand = new ConfirmEmailCommand
        {
            Code = _code,
            UserId = _userId
        };

        var httpResponseWrapper = await AccountsClient.ConfirmEmail(ConfirmEmailCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<string>;
            Snackbar.Add(successResult.Result, Severity.Success);
            NavigationManager.NavigateTo("account/emailConfirmed");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    #endregion Protected Methods
}