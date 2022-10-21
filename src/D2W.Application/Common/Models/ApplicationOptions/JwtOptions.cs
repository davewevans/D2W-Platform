namespace D2W.Application.Common.Models.ApplicationOptions;

public class JwtOptions
{
    #region Public Fields

    public const string Section = "jwt";

    #endregion Public Fields

    #region Public Properties

    public string Issuer { get; set; }

    public string SecurityKey { get; set; }

    public string Audience { get; set; }

    #endregion Public Properties
}