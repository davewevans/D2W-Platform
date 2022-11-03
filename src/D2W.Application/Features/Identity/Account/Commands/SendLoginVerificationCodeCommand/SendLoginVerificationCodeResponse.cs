namespace D2W.Application.Features.Identity.Account.Commands.SendLoginVerificationCodeCommand;

public class SendLoginVerificationCodeResponse
{
    #region Public Properties

    public string ReturnUrl { get; set; }

    public bool TwoFactorCodeSent { get; set; }

    #endregion Public Properties
}
