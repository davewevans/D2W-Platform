namespace D2W.Application.Features.POC.Applicants.Queries.GetApplicantsReferences;

public class GetApplicantReferencesQuery : FilterableQuery, IRequest<Envelope<ApplicantReferencesResponse>>
{
    #region Public Properties

    public Guid ApplicantId { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetApplicantReferencesQueryHandler : IRequestHandler<GetApplicantReferencesQuery, Envelope<ApplicantReferencesResponse>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _ApplicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetApplicantReferencesQueryHandler(IApplicantUseCase ApplicantUseCase)
        {
            _ApplicantUseCase = ApplicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ApplicantReferencesResponse>> Handle(GetApplicantReferencesQuery request, CancellationToken cancellationToken)
        {
            return await _ApplicantUseCase.GetApplicantReferences(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}