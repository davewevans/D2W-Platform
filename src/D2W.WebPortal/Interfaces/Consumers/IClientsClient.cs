using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using D2W.WebPortal.Features.Clients.Commands.UpdateClient;
using D2W.WebPortal.Features.Clients.Queries.GetClientForEdit;
using D2W.WebPortal.Features.Clients.Queries.GetClients;

namespace D2W.WebPortal.Interfaces.Consumers
{
    public interface IClientsClient
    {
        #region Public Methods

        Task<HttpResponseWrapper<object>> GetClient(GetClientForEditQuery request);

        Task<HttpResponseWrapper<object>> GetClients(GetClientsQuery request);

        Task<HttpResponseWrapper<object>> CreateClient(RegisterClientCommand request);

        Task<HttpResponseWrapper<object>> UpdateClient(UpdateClientCommand request);

        Task<HttpResponseWrapper<object>> DeleteClient(string id);

        #endregion Public Methods
    }
}