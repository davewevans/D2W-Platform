namespace D2W.Application.Features.POC.Applicants.Queries.GetApplicantForEdit;

public class GetApplicantForEditQuery : IRequest<Envelope<ApplicantForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetApplicantForEditQueryHandler : IRequestHandler<GetApplicantForEditQuery, Envelope<ApplicantForEdit>>
    {
        #region Private Fields

        private readonly IApplicantUseCase _applicantUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetApplicantForEditQueryHandler(IApplicantUseCase applicantUseCase)
        {
            _applicantUseCase = applicantUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ApplicantForEdit>> Handle(GetApplicantForEditQuery request, CancellationToken cancellationToken)
        {
            return await _applicantUseCase.GetApplicant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}