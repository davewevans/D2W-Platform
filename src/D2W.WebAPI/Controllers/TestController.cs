using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace D2W.WebAPI.Controllers;
[Route("api/test")]
[ApiController]
public class TestController : ApiController
{
    private readonly INotificationService _notificationService;

    public TestController(INotificationService notificationService)
    {
        _notificationService = notificationService;
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
}
