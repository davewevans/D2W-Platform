using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Clients.Queries.GetClientForEdit;

public class GetClientForEditQuery : IRequest<Envelope<ClientForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetClientForEditQueryHandler : IRequestHandler<GetClientForEditQuery, Envelope<ClientForEdit>>
    {
        #region Private Fields

        private readonly IClientUseCase _clientUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetClientForEditQueryHandler(IClientUseCase clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ClientForEdit>> Handle(GetClientForEditQuery request, CancellationToken cancellationToken)
        {
            return await _clientUseCase.GetClient(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
