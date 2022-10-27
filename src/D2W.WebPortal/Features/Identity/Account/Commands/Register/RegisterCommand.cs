namespace D2W.WebPortal.Features.Identity.Account.Commands.Register;

public class RegisterCommand
{
    #region Public Properties

    public string? FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string? HeardAboutUsFrom { get; set; }
    public bool IsBetaTester { get; set; }

    #endregion Public Properties
}