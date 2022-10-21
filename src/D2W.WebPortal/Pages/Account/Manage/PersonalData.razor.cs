namespace D2W.WebPortal.Pages.Account.Manage;

public partial class PersonalData
{
    #region Private Properties

    [Inject] private IDialogService DialogService { get; set; }
    [Inject] private IManageClient ManageClient { get; set; }
    [Inject] private IJSRuntime JsRuntime { get; set; }

    private ServerSideValidator ServerSideValidator { get; set; }

    #endregion Private Properties

    #region Private Methods

    private async Task DownloadPersonalData()
    {
        var httpResponseWrapper = await ManageClient.DownloadPersonalData();

        if (httpResponseWrapper.Success)
        {
            var successResult = httpResponseWrapper.Response as SuccessResult<DownloadPersonalDataResponse>;
            if (JsRuntime is Microsoft.JSInterop.WebAssembly.WebAssemblyJSRuntime webAssemblyJsRuntime)
            {
                webAssemblyJsRuntime.InvokeUnmarshalled<string, string, byte[], bool>("BlazorDownloadFileFast", successResult.Result.FileName, successResult.Result.ContentType, successResult.Result.PersonalData);
            }
            else
            {
                // Fall back to the slow method if not in WebAssembly
                await JsRuntime.InvokeVoidAsync("BlazorDownloadFile", successResult.Result.FileName, successResult.Result.ContentType, successResult.Result.PersonalData);
            }
        }
        else
        {
            var exceptionResult = httpResponseWrapper.Response as ExceptionResult;
            ServerSideValidator.Validate(exceptionResult);
        }
    }

    private void DeletePersonalData()
    {
        DialogService.Show<DeletePersonalData>();
    }

    #endregion Private Methods
}