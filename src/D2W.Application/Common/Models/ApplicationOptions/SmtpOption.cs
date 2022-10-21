namespace D2W.Application.Common.Models.ApplicationOptions;

public class SmtpOption
{
    #region Public Fields

    public const string Section = "SmtpOption";

    #endregion Public Fields

    #region Public Properties

    public string From { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string AppUrl { get; set; }
    public string AppUrlAdmin { get; set; }
    public string ExceptionErrorEmail { get; set; }
    public bool EnableSsl { get; set; }
    public bool UseDefaultCredentials { get; set; }

    #endregion Public Properties
}