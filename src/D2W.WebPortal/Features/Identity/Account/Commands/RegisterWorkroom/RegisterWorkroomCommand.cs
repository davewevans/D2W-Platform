namespace D2W.WebPortal.Features.Identity.Account.Commands.Register;

public class RegisterWorkroomCommand
{
    #region Public Properties

    public string CompanyName { get; set; }
    public string Email { get; set; }
    public string AltEmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public string AltPhone1 { get; set; }
    public string ContactName1 { get; set; }
    public string ContactName2 { get; set; }
    public string AvatarUri { get; set; }
    public ApplicationUserType AppUserType { get; set; }

    #endregion Public Properties
}