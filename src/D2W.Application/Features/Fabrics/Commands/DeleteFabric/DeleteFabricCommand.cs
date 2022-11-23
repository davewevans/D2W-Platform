using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.Fabrics.Commands.DeleteFabric;

public class DeleteFabricCommand : IRequest<Envelope<string>>
{
    #region Public Properties

    public Guid Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class DeleteFabricCommandHandler : IRequestHandler<DeleteFabricCommand, Envelope<string>>
    {
        #region Private Fields

        private readonly IFabricUseCase _fabricUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DeleteFabricCommandHandler(IFabricUseCase fabricUseCase)
        {
            _fabricUseCase = fabricUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<string>> Handle(DeleteFabricCommand request, CancellationToken cancellationToken)
        {
            return await _fabricUseCase.DeleteFabric(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
