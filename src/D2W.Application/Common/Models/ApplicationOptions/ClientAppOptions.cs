namespace D2W.Application.Common.Models.ApplicationOptions;

public class ClientAppOptions
{
    #region Public Fields

    public const string Section = "ClientApp";

    #endregion Public Fields

    #region Public Properties

    public string SingleTenantHostName { get; set; }
    public string MultiTenantHostName { get; set; }
    public string ConfirmEmailChangeUrl { get; set; }
    public string ConfirmEmailUrl { get; set; }
    public string ConfirmEmailUrlWithReturnUrl { get; set; }
    public string ResetPasswordUrl { get; set; }

    #endregion Public Properties
}