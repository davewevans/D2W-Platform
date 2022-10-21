namespace D2W.Application.Features.Reports.GetReportForEdit;

public class GetReportForEditQuery : IRequest<Envelope<ReportForEdit>>
{
    #region Public Properties

    public string Id { get; set; }

    #endregion Public Properties

    #region Public Classes

    public class GetReportForEditQueryHandler : IRequestHandler<GetReportForEditQuery, Envelope<ReportForEdit>>
    {
        #region Private Fields

        private readonly IReportUseCase _reportUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetReportForEditQueryHandler(IReportUseCase reportUseCase)
        {
            _reportUseCase = reportUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<ReportForEdit>> Handle(GetReportForEditQuery request, CancellationToken cancellationToken)
        {
            return await _reportUseCase.GetReport(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}