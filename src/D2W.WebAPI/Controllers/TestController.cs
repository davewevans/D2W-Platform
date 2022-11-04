using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;
[Route("api/test")]
[ApiController]
public class TestController : ApiController
{
    private readonly INotificationService _notificationService;
    private readonly ILogger<TestController> _logger;

    public TestController(INotificationService notificationService, ILogger<TestController> logger)
    {
        _notificationService = notificationService;
        _logger = logger;
    }

    [HttpGet("TestEmail")]
    public async Task<IActionResult> TestSendEmail()
    {
        var email = new { Email = "davewevans72@gmail.com", Subject = "Hello", HtmlMessage = "Hey" };

        await _notificationService.SendEmailAsync(email.Email, email.Subject, email.HtmlMessage);
        
        return Ok();
    }

    [HttpGet("TestSms")]
    public async Task<IActionResult> TestSendSms()
    {
        var message = new Message
        {
            From = "",
            To = "",
            Subject = "",
            Body = ""
        };

        await _notificationService.SendSmsAsync(message);

        return Ok();
    }

    [HttpGet("TestSerilog")]
    public IActionResult TestSerilog()
    {
        _logger.LogInformation("This is a test of Serilog logger. If you can see this, it's working!");
        _logger.LogInformation("This is a log message. This is an object: {User}", new { name = "John Doe" });
        _logger.LogCritical("Critical error!");
        _logger.LogError("An error has occured!");
        _logger.LogWarning("Warning!");


        return Ok();
    }
}
