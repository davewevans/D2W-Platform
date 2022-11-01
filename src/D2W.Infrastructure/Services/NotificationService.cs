using Azure;
using Azure.Communication.Email;
using Azure.Communication.Email.Models;

namespace D2W.Infrastructure.Services;

public class NotificationService : INotificationService
{
    #region Private Fields

    private readonly IConfigReaderService _configReaderService;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<NotificationService> _logger;

    #endregion Private Fields

    #region Public Constructors

    public NotificationService(IConfigReaderService configReaderService, IApplicationDbContext dbContext, ILogger<NotificationService> logger)
    {
        _configReaderService = configReaderService;
        _dbContext = dbContext;
        _logger = logger;
    }

    #endregion Public Constructors

    #region Public Methods

    public Task SendSmsAsync(Message message)
    {
        return Task.CompletedTask;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var connectionString = _configReaderService.GetSmtpOptions().ConnectionString;
        var emailClient = new EmailClient(connectionString);
        string displayName = _configReaderService.GetSmtpOptions().DisplayName;
        string sender = _configReaderService.GetSmtpOptions().From;


        if (_configReaderService.GetSmtpOptions().IsForTesting)
        {
            email = "davewevans72@gmail.com";

            // Send to app user email

            // Get tenant id from resolver
            // Find app user by tenant id
            // Use that user's email for the To email

        }

        //Replace with your domain and modify the content, recipient details as required

        EmailContent emailContent = new EmailContent(subject);
        //emailContent.PlainText = "This email message is sent from Azure Communication Service Email using .NET SDK.";
        emailContent.Html = htmlMessage;
        List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(email) { DisplayName = displayName } };
        EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);
        EmailMessage emailMessage = new EmailMessage(sender, emailContent, emailRecipients);
        SendEmailResult emailResult = emailClient.Send(emailMessage, CancellationToken.None);


        // TODO use SingleR to update front-end

        // Get status of email
        Response<SendStatusResult> messageStatus = null;
        messageStatus = emailClient.GetSendStatus(emailResult.MessageId);

        _logger.LogInformation($"MessageStatus = {messageStatus.Value.Status}");
        Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");

        TimeSpan duration = TimeSpan.FromMinutes(3);
        long start = DateTime.Now.Ticks;
        do
        {
            messageStatus = emailClient.GetSendStatus(emailResult.MessageId);
            if (messageStatus.Value.Status != SendStatus.Queued)
            {
                _logger.LogInformation($"MessageStatus = {messageStatus.Value.Status}");
                Console.WriteLine($"MessageStatus = {messageStatus.Value.Status}");
                break;
            }
            Thread.Sleep(10000);

        } while (DateTime.Now.Ticks - start < duration.Ticks);



        //MailAddress to = new MailAddress(email);
        //MailAddress from = new MailAddress(_configReaderService.GetSmtpOption().From);

        //MailMessage message = new MailMessage(from, to);
        //message.Subject = subject;
        //message.Body = htmlMessage;
        //message.IsBodyHtml = true;
        //SmtpClient client = new SmtpClient
        //{
        //    Host = _configReaderService.GetSmtpOption().SmtpServer,//"smtp.gmail.com",
        //    Port = _configReaderService.GetSmtpOption().Port,
        //    EnableSsl = _configReaderService.GetSmtpOption().EnableSsl
        //};

        //client.UseDefaultCredentials = _configReaderService.GetSmtpOption().UseDefaultCredentials;
        //client.Credentials = new NetworkCredential(_configReaderService.GetSmtpOption().UserName, _configReaderService.GetSmtpOption().Password);
        //client.Send(message);

        return Task.CompletedTask;
    }

    #endregion Public Methods
}