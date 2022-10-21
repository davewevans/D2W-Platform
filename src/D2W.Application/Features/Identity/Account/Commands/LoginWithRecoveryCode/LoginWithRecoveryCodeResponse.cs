namespace D2W.Application.Features.Identity.Account.Commands.LoginWithRecoveryCode;

public class LoginWithRecoveryCodeResponse
{
    #region Public Properties

    public AuthResponse AuthResponse { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static LoginWithRecoveryCodeResponse MapFrom(AuthResponse authResponse)
    {
        return new()
        {
            AuthResponse = authResponse
        };
    }

    #endregion Public Methods
}