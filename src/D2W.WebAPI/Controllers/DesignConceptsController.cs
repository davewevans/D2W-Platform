using D2W.Application.Features.DesignConcepts.Commands.CreateDesignConcept;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConceptForEdit;
using D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;
[Route("api/DesignConcepts")]
[ApiController]
public class DesignConceptsController : ApiController
{
    #region Public Methods

    [HttpPost("GetDesignConcept")]
    public async Task<IActionResult> GetDesignConcept(GetDesignConceptForEditQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("GetDesignConcepts")]
    public async Task<IActionResult> GetDesignConcepts(GetDesignConceptsQuery request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    [HttpPost("CreateDesignConcept")]
    public async Task<IActionResult> CreateDesignConcept(CreateDesignConceptCommand request)
    {
        var response = await Mediator.Send(request);
        return TryGetResult(response);
    }

    //[HttpPut("UpdateDesignConcept")]
    //public async Task<IActionResult> UpdateDesignConcept(UpdateDesignConceptCommand request)
    //{
    //    var response = await Mediator.Send(request);
    //    return TryGetResult(response);
    //}

    //[HttpDelete("DeleteDesignConcept")]
    //public async Task<IActionResult> DeleteDesignConcept(string id)
    //{
    //    var response = await Mediator.Send(new DeleteDesignConceptCommand { Id = id });
    //    return TryGetResult(response);
    //}

    #endregion Public Methods
}
