namespace D2W.WebPortal.Models.Settings;

public class TokenSettings
{
    #region Public Properties

    public Guid Id { get; set; }
    public int AccessTokenUoT { get; set; }
    public double? AccessTokenTimeSpan { get; set; }
    public int RefreshTokenUoT { get; set; }
    public double? RefreshTokenTimeSpan { get; set; }
    public Guid? TenantId { get; set; }

    #endregion Public Properties
}