namespace D2W.Application.Features.Identity.Manage.Queries.LoadSharedKeyAndQrCodeUri;

public class LoadSharedKeyAndQrCodeUriQuery : IRequest<Envelope<LoadSharedKeyAndQrCodeUriResponse>>
{
    #region Public Classes

    public class LoadSharedKeyAndQrCodeUriQueryHandler : IRequestHandler<LoadSharedKeyAndQrCodeUriQuery, Envelope<LoadSharedKeyAndQrCodeUriResponse>>
    {
        #region Private Fields

        private readonly IManageUseCase _manageUseCase;

        #endregion Private Fields

        #region Public Constructors

        public LoadSharedKeyAndQrCodeUriQueryHandler(IManageUseCase manageUseCase)
        {
            _manageUseCase = manageUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<LoadSharedKeyAndQrCodeUriResponse>> Handle(LoadSharedKeyAndQrCodeUriQuery request, CancellationToken cancellationToken)
        {
            return await _manageUseCase.LoadSharedKeyAndQrCodeUri();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}