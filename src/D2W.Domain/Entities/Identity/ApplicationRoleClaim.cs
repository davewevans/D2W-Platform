namespace D2W.Domain.Entities.Identity;

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    #region Public Properties

    public ApplicationRole Role { get; set; }

    #endregion Public Properties
}