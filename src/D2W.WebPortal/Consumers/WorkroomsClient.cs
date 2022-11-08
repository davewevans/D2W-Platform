using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Workrooms.Commands.UpdateWorkroom;
using D2W.WebPortal.Features.Workrooms.Queries.GetWorkroomForEdit;
using D2W.WebPortal.Features.Workrooms.Queries.GetWorkrooms;

namespace D2W.WebPortal.Consumers
{
    public class WorkroomsClient : IWorkroomsClient
    {
        #region Private Fields

        private readonly IHttpService _httpService;

        #endregion Private Fields

        #region Public Constructors

        public WorkroomsClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<HttpResponseWrapper<object>> GetWorkroom(GetWorkroomForEditQuery request)
        {
            return await _httpService.Post<GetWorkroomForEditQuery, WorkroomForEdit>("workrooms/GetWorkroom", request);
        }

        public async Task<HttpResponseWrapper<object>> GetWorkrooms(GetWorkroomsQuery request)
        {
            return await _httpService.Post<GetWorkroomsQuery, WorkroomsResponse>("workrooms/GetWorkrooms", request);
        }

        public async Task<HttpResponseWrapper<object>> CreateWorkroom(RegisterWorkroomCommand request)
        {
            return await _httpService.Post<RegisterWorkroomCommand, RegisterWorkroomResponse>("account/WorkroomRegister", request);
        }

        public async Task<HttpResponseWrapper<object>> UpdateWorkroom(UpdateWorkroomCommand request)
        {
            return await _httpService.Put<UpdateWorkroomCommand, string>("workrooms/UpdateWorkroom", request);
        }

        public async Task<HttpResponseWrapper<object>> DeleteWorkroom(string id)
        {
            return await _httpService.Delete<string>($"workrooms/DeleteWorkroom?id={id}");
        }

        #endregion Public Constructors
    }
}