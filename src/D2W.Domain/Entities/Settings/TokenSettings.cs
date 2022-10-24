namespace D2W.Domain.Entities.Settings;

public class TokenSettings : ISettingsSchema, IMayHaveTenant
{
    #region Public Properties

    public Guid Id { get; set; }
    public int AccessTokenUoT { get; set; }
    public double? AccessTokenTimeSpan { get; set; }
    public int RefreshTokenUoT { get; set; }
    public double? RefreshTokenTimeSpan { get; set; }
    public Guid? TenantId { get; set; }
    public bool IgnoreTenantId { get; set; }

    #endregion Public Properties
}