namespace D2W.Application.Common.Interfaces.Services;

public interface INotificationService
{
    #region Public Methods

    Task SendSmsAsync(Message message);

    Task SendEmailAsync(string email, string subject, string htmlMessage);

    #endregion Public Methods
}