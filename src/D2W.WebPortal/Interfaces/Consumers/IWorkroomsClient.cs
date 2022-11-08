using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.WebPortal.Features.Workrooms.Queries.GetWorkroomForEdit;
using D2W.WebPortal.Features.Workrooms.Queries.GetWorkrooms;

namespace D2W.WebPortal.Interfaces.Consumers
{
    public interface IWorkroomsClient
    {
        #region Public Methods

        Task<HttpResponseWrapper<object>> GetWorkroom(GetWorkroomForEditQuery request);

        Task<HttpResponseWrapper<object>> GetWorkrooms(GetWorkroomsQuery request);

        Task<HttpResponseWrapper<object>> CreateWorkroom(RegisterWorkroomCommand request);

        Task<HttpResponseWrapper<object>> UpdateWorkroom(UpdateWorkroomCommand request);

        Task<HttpResponseWrapper<object>> DeleteWorkroom(string id);

        #endregion Public Methods
    }
}