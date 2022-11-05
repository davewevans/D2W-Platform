using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Features.Clients.Commands.DeleteClient;
using D2W.Application.Features.Clients.Commands.UpdateClient;
using D2W.Application.Features.Clients.Queries.GetClientForEdit;
using D2W.Application.Features.Clients.Queries.GetClients;

namespace D2W.Application.Common.Interfaces.UseCases;

public interface IClientUseCase
{
    #region Public Methods

    Task<Envelope<ClientForEdit>> GetClient(GetClientForEditQuery request);
    Task<Envelope<ClientsResponse>> GetClients(GetClientsQuery request);

    // Use RegisterClient instead
    //Task<Envelope<CreateClientResponse>> AddClient(CreateClientCommand request);

    Task<Envelope<string>> EditClient(UpdateClientCommand request);
    Task<Envelope<string>> DeleteClient(DeleteClientCommand request);


    #endregion Public Methods
}
