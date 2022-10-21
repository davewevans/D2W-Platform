namespace D2W.Application.Features.AppSettings.Commands.UpdateSettings;

public class UpdateTokenSettingsCommand : IRequest<Envelope<TokenSettingsResponse>>
{
    #region Public Properties

    public TokenSettings TokenSettings { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void MapToEntity(TokenSettings tokenSettings)
    {
        tokenSettings.Id = TokenSettings.Id;
        tokenSettings.AccessTokenUoT = TokenSettings.AccessTokenUoT;
        tokenSettings.AccessTokenTimeSpan = TokenSettings.AccessTokenTimeSpan;
        tokenSettings.RefreshTokenUoT = TokenSettings.RefreshTokenUoT;
        tokenSettings.RefreshTokenTimeSpan = TokenSettings.RefreshTokenTimeSpan;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateTokenSettingsCommandHandler : IRequestHandler<UpdateTokenSettingsCommand, Envelope<TokenSettingsResponse>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateTokenSettingsCommandHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<TokenSettingsResponse>> Handle(UpdateTokenSettingsCommand request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.EditTokenSettings(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}