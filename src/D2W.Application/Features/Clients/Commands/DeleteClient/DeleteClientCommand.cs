using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IClientUseCase _clientUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteClientCommandHandler(IClientUseCase clientUseCase)
        {
            _clientUseCase = clientUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            return await _clientUseCase.DeleteClient(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
