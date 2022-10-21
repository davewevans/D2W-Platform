namespace D2W.Application.Common.Models.ApplicationOptions.ApplicationIdentityOptions;

public class AppPasswordOptions
{
    #region Public Fields

    public const string Section = "AppPasswordOptions";

    #endregion Public Fields

    #region Public Properties

    public int RequiredLength { get; set; }
    public int RequiredUniqueChars { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireDigit { get; set; }

    #endregion Public Properties

    #region Public Methods

    public PasswordSettings MapToEntity()
    {
        return new()
        {
            RequiredLength = RequiredLength,
            RequiredUniqueChars = RequiredUniqueChars,
            RequireNonAlphanumeric = RequireNonAlphanumeric,
            RequireLowercase = RequireLowercase,
            RequireUppercase = RequireUppercase,
            RequireDigit = RequireDigit
        };
    }

    #endregion Public Methods
}