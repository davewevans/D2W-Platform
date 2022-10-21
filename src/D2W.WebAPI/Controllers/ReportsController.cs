namespace D2W.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[BpAuthorize]
public class ReportsController : ApiController
{
    #region Public Methods

    [HttpPost("GetReport")]
    public async Task<IActionResult> GetReport(GetReportForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetReports")]
    public async Task<IActionResult> GetReports(GetReportsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    #endregion Public Methods
}