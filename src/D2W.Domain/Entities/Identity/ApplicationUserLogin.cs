namespace D2W.Domain.Entities.Identity;

public class ApplicationUserLogin : IdentityUserLogin<string>
{
    #region Public Properties

    public ApplicationUser User { get; set; }

    #endregion Public Properties
}