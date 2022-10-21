namespace D2W.Application.Features.Identity.Manage.Queries.DownloadPersonalData;

public class DownloadPersonalDataQuery : IRequest<Envelope<DownloadPersonalDataResponse>>
{
    #region Public Classes

    public class DownloadPersonalDataQueryHandler : IRequestHandler<DownloadPersonalDataQuery, Envelope<DownloadPersonalDataResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public DownloadPersonalDataQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<DownloadPersonalDataResponse>> Handle(DownloadPersonalDataQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.DownloadPersonalData();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}