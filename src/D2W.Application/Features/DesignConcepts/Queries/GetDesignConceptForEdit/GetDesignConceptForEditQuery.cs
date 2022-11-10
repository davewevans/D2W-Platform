using D2W.Application.Common.Interfaces.UseCases;

namespace D2W.Application.Features.DesignConcepts.Queries.GetDesignConceptForEdit;

public class GetDesignConceptForEditQuery : IRequest<Envelope<DesignConceptForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetDesignConceptForEditQueryHandler : IRequestHandler<GetDesignConceptForEditQuery, Envelope<DesignConceptForEdit>>
    {
        #region Private Fields

        private readonly IDesignConceptUseCase _designConceptUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetDesignConceptForEditQueryHandler(IDesignConceptUseCase designConceptUseCase)
        {
            _designConceptUseCase = designConceptUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<DesignConceptForEdit>> Handle(GetDesignConceptForEditQuery request, CancellationToken cancellationToken)
        {
            return await _designConceptUseCase.GetDesignConcept(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
