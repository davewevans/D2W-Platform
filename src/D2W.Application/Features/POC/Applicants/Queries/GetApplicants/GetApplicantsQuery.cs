namespace D2W.Application.Features.POC.Applicants.Queries.GetApplicants;

public class GetApplicantsQuery : FilterableQuery, IRequest<Envelope<ApplicantsResponse>>
{
    #region Public Classes

    public class GetApplicantsQueryHandler : IRequestHandler<GetApplicantsQuery, Envelope<ApplicantsResponse>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _applicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetApplicantsQueryHandler(IApplicantUseCase applicantUseCase)
        {
            _applicantUseCase = applicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ApplicantsResponse>> Handle(GetApplicantsQuery request, CancellationToken cancellationToken)
        {
            return await _applicantUseCase.GetApplicants(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}