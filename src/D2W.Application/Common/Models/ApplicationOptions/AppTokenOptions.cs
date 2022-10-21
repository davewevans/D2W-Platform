namespace D2W.Application.Common.Models.ApplicationOptions;

public class AppTokenOptions
{
    #region Public Fields

    public const string Section = "TokenOptions";

    #endregion Public Fields

    #region Public Properties

    public int AccessTokenUoT { get; set; }
    public double AccessTokenTimeSpan { get; set; }
    public int RefreshTokenUoT { get; set; }
    public double RefreshTokenTimeSpan { get; set; }

    #endregion Public Properties

    #region Public Methods

    public TokenSettings MapToEntity()
    {
        return new()
        {
            AccessTokenUoT = AccessTokenUoT,
            AccessTokenTimeSpan = AccessTokenTimeSpan,
            RefreshTokenTimeSpan = RefreshTokenTimeSpan,
            RefreshTokenUoT = RefreshTokenUoT,
        };
    }

    #endregion Public Methods
}