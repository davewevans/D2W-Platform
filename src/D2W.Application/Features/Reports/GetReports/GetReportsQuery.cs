namespace D2W.Application.Features.Reports.GetReports;

public class GetReportsQuery : FilterableQuery, IRequest<Envelope<ReportsResponse>>
{
    #region Public Classes

    public ReportStatus? SelectedReportStatus { get; set; }

    public class GetReportsQueryHandler : IRequestHandler<GetReportsQuery, Envelope<ReportsResponse>>
    {
        #region Private Fields

        private readonly IReportUseCase _reportUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetReportsQueryHandler(IReportUseCase reportUseCase)
        {
            _reportUseCase = reportUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ReportsResponse>> Handle(GetReportsQuery request, CancellationToken cancellationToken)
        {
            return await _reportUseCase.GetReports(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}