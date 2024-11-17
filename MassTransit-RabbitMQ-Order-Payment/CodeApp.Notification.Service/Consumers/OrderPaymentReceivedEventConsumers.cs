using CodeApp.Masstransit.Shared.Models.Notification.Commands;
using CodeApp.Masstransit.Shared.Models.Payment.Events;
using MassTransit;

namespace CodeApp.Notification.Service.Consumers;

public class OrderPaymentReceivedEventConsumers : IConsumer<OrderPaymentReceivedEvent>
{
    private readonly IBus _bus;
    private readonly ILogger<OrderPaymentReceivedEventConsumers> _logger;

    public OrderPaymentReceivedEventConsumers(IBus bus, ILogger<OrderPaymentReceivedEventConsumers> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<OrderPaymentReceivedEvent> context)
    {
        //Business rules
        //Find the customer phone
        //Find the customer email address

        var orderId = context.Message.OrderId;
        var customerId = context.Message.CustomerId;
        var amount = context.Message.Amount;
        var phoneNumber = "+90 532 632 63 22";
        var emailAddreses = "ilkersatur@gmail.com";
        var cardNumber = context.Message.CardNumber;

        var content =
            $"Payment received for this order, OrderId: {orderId}, Payment Amount: {amount}, CardNumber:{cardNumber}";


        await _bus.Publish<SendSmsCommand>(new SendSmsCommand(phoneNumber, content));
        await _bus.Publish<SendEmailCommand>(
            new SendEmailCommand(new List<string>()
                    {
                        emailAddreses
                    }, "Payment received", content)
            );
        
        _logger.LogInformation("Order Payment Received, OrderId: {orderId}, CustomerId: {customerId}", orderId, customerId);
        
    }
}