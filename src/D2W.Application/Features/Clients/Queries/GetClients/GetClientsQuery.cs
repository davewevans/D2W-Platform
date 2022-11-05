using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Clients.Queries.GetClients;

public class GetClientsQuery : FilterableQuery, IRequest<Envelope<ClientsResponse>>
{
    #region Public Classes

    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, Envelope<ClientsResponse>>
    {
        #region Private Fields

        private readonly IClientUseCase _clientUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetClientsQueryHandler(IClientUseCase clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ClientsResponse>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            return await _clientUseCase.GetClients(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}

