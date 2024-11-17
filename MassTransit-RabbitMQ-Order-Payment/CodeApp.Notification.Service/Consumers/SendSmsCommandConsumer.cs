using CodeApp.Masstransit.Shared.Models.Notification.Commands;
using MassTransit;

namespace CodeApp.Notification.Service.Consumers;

public class SendSmsCommandConsumer : IConsumer<SendSmsCommand>
{
    private readonly ILogger<SendSmsCommandConsumer> _logger;

    public SendSmsCommandConsumer(ILogger<SendSmsCommandConsumer> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<SendSmsCommand> context)
    {
        //Find the correct template etc. Or provider business flow
        
        _logger.LogInformation("Sent the sms");

        return Task.CompletedTask;
    }
}