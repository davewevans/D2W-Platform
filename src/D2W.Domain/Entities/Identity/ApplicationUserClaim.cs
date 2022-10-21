namespace D2W.Domain.Entities.Identity;

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    #region Public Properties

    public ApplicationUser User { get; set; }
    public bool IsExcluded { get; set; }

    #endregion Public Properties
}