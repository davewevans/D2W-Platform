namespace D2W.WebPortal.Models.Settings.IdentitySettings;

public class PasswordSettings
{
    #region Public Properties

    public Guid Id { get; set; }
    public int RequiredLength { get; set; }
    public int RequiredUniqueChars { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireDigit { get; set; }
    public Guid? TenantId { get; set; }

    #endregion Public Properties
}