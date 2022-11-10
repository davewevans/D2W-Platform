using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConcepts;

public class GetDesignConceptsQuery : FilterableQuery, IRequest<Envelope<DesignConceptsResponse>>
{
    #region Public Classes

    public class GetDesignConceptsQueryHandler : IRequestHandler<GetDesignConceptsQuery, Envelope<DesignConceptsResponse>>
    {
        #region Private Fields

        private readonly IDesignConceptUseCase _designConceptUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetDesignConceptsQueryHandler(IDesignConceptUseCase designConceptUseCase)
        {
            _designConceptUseCase = designConceptUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<DesignConceptsResponse>> Handle(GetDesignConceptsQuery request, CancellationToken cancellationToken)
        {
            return await _designConceptUseCase.GetDesignConcepts(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}

