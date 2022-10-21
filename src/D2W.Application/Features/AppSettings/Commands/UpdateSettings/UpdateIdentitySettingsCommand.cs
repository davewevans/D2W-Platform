namespace D2W.Application.Features.AppSettings.Commands.UpdateSettings;

public class UpdateIdentitySettingsCommand : IRequest<Envelope<IdentitySettingsResponse>>
{
    #region Public Properties

    public UserSettings UserSettings { get; set; }
    public PasswordSettings PasswordSettings { get; set; }
    public LockoutSettings LockoutSettings { get; set; }
    public SignInSettings SignInSettings { get; set; }

    #endregion Public Properties

    #region Public Methods

    public void MapToEntity(UserSettings userSettings, PasswordSettings passwordSettings, LockoutSettings lockoutSettings, SignInSettings signInSettings)
    {
        userSettings.Id = UserSettings.Id;
        userSettings.AllowedUserNameCharacters = UserSettings.AllowedUserNameCharacters;
        userSettings.NewUsersActiveByDefault = UserSettings.NewUsersActiveByDefault;

        passwordSettings.Id = PasswordSettings.Id;
        passwordSettings.RequiredLength = PasswordSettings.RequiredLength;
        passwordSettings.RequiredUniqueChars = PasswordSettings.RequiredUniqueChars;
        passwordSettings.RequireNonAlphanumeric = PasswordSettings.RequireNonAlphanumeric;
        passwordSettings.RequireLowercase = PasswordSettings.RequireLowercase;
        passwordSettings.RequireUppercase = PasswordSettings.RequireUppercase;
        passwordSettings.RequireDigit = PasswordSettings.RequireDigit;

        lockoutSettings.Id = LockoutSettings.Id;
        lockoutSettings.AllowedForNewUsers = LockoutSettings.AllowedForNewUsers;
        lockoutSettings.MaxFailedAccessAttempts = LockoutSettings.MaxFailedAccessAttempts;
        lockoutSettings.DefaultLockoutTimeSpan = LockoutSettings.DefaultLockoutTimeSpan;

        signInSettings.Id = SignInSettings.Id;
        //signInSettings.RequireConfirmedEmail =SignInSettings.RequireConfirmedEmail;
        //signInSettings.RequireConfirmedPhoneNumber =SignInSettings.RequireConfirmedPhoneNumber;
        signInSettings.RequireConfirmedAccount = SignInSettings.RequireConfirmedAccount;
    }

    #endregion Public Methods

    #region Public Classes

    public class UpdateIdentitySettingsCommandHandler : IRequestHandler<UpdateIdentitySettingsCommand, Envelope<IdentitySettingsResponse>>
    {
        #region Private Fields

        private readonly IAppSettingsUseCase _appSettingsUseCase;

        #endregion Private Fields

        #region Public Constructors

        public UpdateIdentitySettingsCommandHandler(IAppSettingsUseCase appSettingsUseCase)
        {
            _appSettingsUseCase = appSettingsUseCase;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<IdentitySettingsResponse>> Handle(UpdateIdentitySettingsCommand request, CancellationToken cancellationToken)
        {
            return await _appSettingsUseCase.EditIdentitySettings(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}