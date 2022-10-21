namespace D2W.Application.Features.AppSettings.Queries.GetSettings.GetIdentitySettings;

public class GetIdentitySettingsQuery : IRequest<Envelope<IdentitySettingsForEdit>>
{
    #region Public Classes

    public class GetIdentitySettingsQueryHandler : IRequestHandler<GetIdentitySettingsQuery, Envelope<IdentitySettingsForEdit>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetIdentitySettingsQueryHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<IdentitySettingsForEdit>> Handle(GetIdentitySettingsQuery request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.GetIdentitySettings();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}