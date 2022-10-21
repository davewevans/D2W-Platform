namespace D2W.Application.Features.AppSettings.Queries.GetSettings.GetTokenSettings;

public class GetTokenSettingsQuery : IRequest<Envelope<TokenSettingsForEdit>>
{
    #region Public Classes

    public class GetTokenSettingsQueryHandler : IRequestHandler<GetTokenSettingsQuery, Envelope<TokenSettingsForEdit>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public GetTokenSettingsQueryHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<TokenSettingsForEdit>> Handle(GetTokenSettingsQuery request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.GetTokenSettings();
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}