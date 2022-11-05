using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Clients.Commands.CreateClient;
using D2W.WebPortal.Features.Clients.Commands.UpdateClient;
using D2W.WebPortal.Features.Clients.Queries.GetClientForEdit;
using D2W.WebPortal.Features.Clients.Queries.GetClients;

namespace D2W.WebPortal.Consumers
{
    public class ClientsClient : IClientsClient
    {
        #region Private Fields

        private readonly IHttpService _httpService;

        #endregion Private Fields

        #region Public Constructors

        public ClientsClient(IHttpService httpService)
        {
            _httpService = httpService;
        }

        #endregion Public Constructors

        public async Task<HttpResponseWrapper<object>> GetClient(GetClientForEditQuery request)
        {
            return await _httpService.Post<GetClientForEditQuery, ClientForEdit>("clients/GetClient", request);
        }

        public async Task<HttpResponseWrapper<object>> GetClients(GetClientsQuery request)
        {
            return await _httpService.Post<GetClientsQuery, ClientsResponse>("clients/GetClients", request);
        }

        public async Task<HttpResponseWrapper<object>> CreateClient(CreateClientCommand request)
        {
            return await _httpService.Post<CreateClientCommand, CreateClientResponse>("clients/CreateClient", request);
        }

        public async Task<HttpResponseWrapper<object>> UpdateClient(UpdateClientCommand request)
        {
            return await _httpService.Put<UpdateClientCommand, string>("clients/UpdateClient", request);
        }

        public async Task<HttpResponseWrapper<object>> DeleteClient(string id)
        {
            return await _httpService.Delete<string>($"clients/DeleteClient?id={id}");
        }

    }
}