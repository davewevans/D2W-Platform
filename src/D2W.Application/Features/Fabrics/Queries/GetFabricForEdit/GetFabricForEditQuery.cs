using D2W.Application.Common.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Queries.GetFabricForEdit;

public class GetFabricForEditQuery : IRequest<Envelope<FabricForEdit>>
{
    #region Public Properties

    public Guid Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetFabricForEditQueryHandler : IRequestHandler<GetFabricForEditQuery, Envelope<FabricForEdit>>
    {
        #region Private Fields

        private readonly IFabricUseCase _fabricUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetFabricForEditQueryHandler(IFabricUseCase fabricUseCase)
        {
            _fabricUseCase = fabricUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<FabricForEdit>> Handle(GetFabricForEditQuery request, CancellationToken cancellationToken)
        {
            return await _fabricUseCase.GetFabric(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
