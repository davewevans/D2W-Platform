using D2W.Application.Common.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2W.Application.Features.Fabrics.Queries.GetFabrics;

public class GetFabricsQuery : FilterableQuery, IRequest<Envelope<FabricsResponse>>
{
    #region Public Classes

    public class GetFabricsQueryHandler : IRequestHandler<GetFabricsQuery, Envelope<FabricsResponse>>
    {
        #region Private Fields

        private readonly IFabricUseCase _fabricUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetFabricsQueryHandler(IFabricUseCase fabricUseCase)
        {
            _fabricUseCase = fabricUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<FabricsResponse>> Handle(GetFabricsQuery request, CancellationToken cancellationToken)
        {
            return await _fabricUseCase.GetFabrics(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
