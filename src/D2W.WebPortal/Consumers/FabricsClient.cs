using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Fabrics.Commands.CreateFabric;
using D2W.WebPortal.Features.Fabrics.Commands.UpdateFabric;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabricForEdit;
using D2W.WebPortal.Features.Fabrics.Queries.GetFabrics;

namespace D2W.WebPortal.Consumers
{
    public class FabricsClient : IFabricsClient
    {
        #region Private Fields

        private readonly IHttpService _httpService;

        #endregion Private Fields

        #region Public Constructors

        public FabricsClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion Public Constructors

        public async Task<HttpResponseWrapper<object>> GetFabric(GetFabricForEditQuery request)
        {
            return await _httpService.Post<GetFabricForEditQuery, FabricForEdit>("fabrics/GetFabric", request);
        }

        public async Task<HttpResponseWrapper<object>> GetFabrics(GetFabricsQuery request)
        {
            return await _httpService.Post<GetFabricsQuery, FabricsResponse>("fabrics/GetFabrics", request);
        }

        public async Task<HttpResponseWrapper<object>> CreateFabric(CreateFabricCommand request)
        {
            return await _httpService.Post<CreateFabricCommand, CreateFabricResponse>("fabrics/CreateFabric", request);
        }

        public async Task<HttpResponseWrapper<object>> UpdateFabric(UpdateFabricCommand request)
        {
            return await _httpService.Put<UpdateFabricCommand, string>("fabrics/UpdateFabric", request);
        }

        public async Task<HttpResponseWrapper<object>> DeleteFabric(Guid id)
        {
            return await _httpService.Delete<string>($"fabrics/DeleteFabric?id={id}");
        }
    }
}