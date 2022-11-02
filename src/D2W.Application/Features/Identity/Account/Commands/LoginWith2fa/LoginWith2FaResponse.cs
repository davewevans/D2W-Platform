namespace D2W.Application.Features.Identity.Account.Commands.LoginWith2fa;

public class LoginWith2FaResponse
{
    #region Public Properties
    public string ReturnUrl { get; set; }

    public AuthResponse AuthResponse { get; set; }

    #endregion Public Properties
}