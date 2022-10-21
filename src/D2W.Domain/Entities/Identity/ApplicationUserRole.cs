namespace D2W.Domain.Entities.Identity;

public class ApplicationUserRole : IdentityUserRole<string>
{
    #region Public Properties

    public ApplicationUser User { get; set; }
    public ApplicationRole Role { get; set; }

    #endregion Public Properties
}