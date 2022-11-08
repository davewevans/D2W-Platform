using D2W.Application.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.Application.Features.Workrooms.Queries.GetWorkroomForEdit;
using D2W.Application.Features.Workrooms.Queries.GetWorkrooms;

namespace D2W.WebAPI.Controllers
{
    [Route("api/workrooms")]
    [ApiController]
    public class WorkroomsController : ApiController
    {
        #region Public Methods

        [Authorize("Workroom,Designer")]
        [HttpPost("GetWorkroom")]
        public async Task<IActionResult> GetWorkroom(GetWorkroomForEditQuery request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[Authorize("Designer")]
        [HttpPost("GetWorkrooms")]
        public async Task<IActionResult> GetWorkrooms(GetWorkroomsQuery request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[HttpPost("CreateWorkroom")]
        //public async Task<IActionResult> CreateWorkroom(CreateWorkroomCommand request)
        //{
        //    var response = await Mediator.Send(request);
        //    return TryGetResult(response);
        //}

        [Authorize("Workroom,Designer")]
        [HttpPut("UpdateWorkroom")]
        public async Task<IActionResult> UpdateWorkroom(UpdateWorkroomCommand request)
        {
            var response = await Mediator.Send(request);
            return TryGetResult(response);
        }

        //[HttpDelete("DeleteWorkroom")]
        //public async Task<IActionResult> DeleteWorkroom(string id)
        //{
        //    var response = await Mediator.Send(new DeleteWorkroomCommand { Id = id });
        //    return TryGetResult(response);
        //}


        //[AllowAnonymous]
        //[HttpPost("ExportAsPdf")]
        //public async Task<IActionResult> ExportAsPdf(ExportWorkroomsQuery request)
        //{
        //    var response = await Mediator.Send(request);
        //    return TryGetResult(response);
        //}

        #endregion Public Methods
    }
}
