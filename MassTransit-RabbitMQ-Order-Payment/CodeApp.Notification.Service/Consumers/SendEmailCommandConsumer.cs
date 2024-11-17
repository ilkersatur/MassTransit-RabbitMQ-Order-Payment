using CodeApp.Masstransit.Shared.Models.Notification.Commands;
using MassTransit;

namespace CodeApp.Notification.Service.Consumers;

public class SendEmailCommandConsumer : IConsumer<SendEmailCommand>
{
    private readonly ILogger<SendSmsCommandConsumer> _logger;

    public SendEmailCommandConsumer(ILogger<SendSmsCommandConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<SendEmailCommand> context)
    {
        //Find the correct template etc. Or provider business flow

        //throw new Exception("Error");
        
        _logger.LogInformation("Sent the email");

        return Task.CompletedTask;
    }
}