namespace D2W.Application.Features.POC.Applicants.Queries.ExportApplicants
{
    public class ExportApplicantsQuery : IRequest<Envelope<ExportApplicantsResponse>>
    {
        #region Public Classes

        public string SearchText { get; set; }
        public string SortBy { get; set; }

        public class CreateApplicantQueryHandler : IRequestHandler<ExportApplicantsQuery, Envelope<ExportApplicantsResponse>>
        {
            #region Private Fields

            private readonly IApplicantUseCase _applicantUseCase;

            #endregion Private Fields

            #region Public Constructors

            public CreateApplicantQueryHandler(IApplicantUseCase applicantUseCase)
            {
                _applicantUseCase = applicantUseCase;
            }

            #endregion Public Constructors

            #region Public Methods

            public async Task<Envelope<ExportApplicantsResponse>> Handle(ExportApplicantsQuery request, CancellationToken cancellationToken)
            {
                return await _applicantUseCase.ExportAsPdf(request);
            }

            #endregion Public Methods
        }

        #endregion Public Classes
    }
}