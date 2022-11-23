using D2W.Application.Features.Fabrics.Commands.CreateFabric;
using D2W.Application.Features.Fabrics.Commands.DeleteFabric;
using D2W.Application.Features.Fabrics.Commands.UpdateFabric;
using D2W.Application.Features.Fabrics.Queries.GetFabricForEdit;
using D2W.Application.Features.Fabrics.Queries.GetFabrics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;

[Route("api/Fabrics")]
[ApiController]
public class FabricsController : ApiController
{
    #region Public Methods

    [HttpPost("GetFabric")]
    public async Task<IActionResult> GetFabric(GetFabricForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetFabrics")]
    public async Task<IActionResult> GetFabrics(GetFabricsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("CreateFabric")]
    public async Task<IActionResult> CreateFabric(CreateFabricCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPut("UpdateFabric")]
    public async Task<IActionResult> UpdateFabric(UpdateFabricCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpDelete("DeleteFabric")]
    public async Task<IActionResult> DeleteFabric(Guid id)
    {
        var response = await Mediator.Send(new DeleteFabricCommand { Id = id });
        return TryGetResult(response);
    }

    #endregion Public Methods

}
