namespace D2W.Application.Common.Models.ApplicationOptions;

public class SmtpOptions
{
    #region Public Fields

    public const string Section = "SmtpOptions";

    #endregion Public Fields

    #region Public Properties

    public string ConnectionString { get; set; }
    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AppUrl { get; set; }
    public string AppUrlAdmin { get; set; }
    public string ExceptionErrorEmail { get; set; }
    public string DisplayName { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }
    public bool IsForTesting { get; set; }

    #endregion Public Properties
}