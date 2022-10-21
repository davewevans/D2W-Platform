namespace D2W.HostApp.Pages;

public partial class Index
{
    #region Private Properties

    [Inject] private ITenantUrlProvider TenantApiUrlProvider { get; set; }
    [Inject] private ITenantsClient TenantsClient { get; set; }

    private bool Success { get; set; }
    private bool SubmitButtonDisabled { get; set; } = true;
    private bool ShowProgress { get; set; }
    private MudForm Form { get; set; }

    private string TenantUrl { get; set; }
    private string InitialTenantName { get; set; }
    private string FinalTenantName { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }
    private CreateTenantCommand CreateTenantCommand { get; set; } = new();

    #endregion Private Properties

    #region Private Methods

    private void BuildTenantUrl(string tenantName)
    {
        tenantName = tenantName.ReplaceSpaceAndSpecialCharsWithDashes().ToLower();
        InitialTenantName = tenantName;
        var postfix = $"{DateTime.Now.Year}{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}";
        FinalTenantName = string.IsNullOrWhiteSpace(tenantName) ? postfix : $"{tenantName}-{postfix}";
        TenantUrl = string.Format(TenantApiUrlProvider.TenantUrl, FinalTenantName);
    }

    private string TenantCharLengthValidator(string tenantName)
    {
        if (!string.IsNullOrEmpty(InitialTenantName) && InitialTenantName.ToCharArray().Length >= 20)
        {
            return "Tenant name is too long!";
        }
        SubmitButtonDisabled = false;
        return null;
    }

    private async Task SubmitForm()
    {
        SubmitButtonDisabled = true;
        ShowProgress = true;
        CreateTenantCommand.TenantName = FinalTenantName;
        SubmitButtonDisabled = true;

        var httpResponseWrapper = await TenantsClient.CreateTenant(CreateTenantCommand);

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<CreateTenantResponse>;
            NavigationManager.NavigateTo($"{TenantUrl}/account/login");
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
            SubmitButtonDisabled = false;
            ShowProgress = false;
        }
    }

    #endregion Private Methods
}