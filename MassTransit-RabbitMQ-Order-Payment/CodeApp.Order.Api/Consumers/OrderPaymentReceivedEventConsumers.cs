using CodeApp.Masstransit.Shared.Models.Payment.Events;
using MassTransit;

namespace CodeApp.Order.Api.Consumers;

public class OrderPaymentReceivedEventConsumers : IConsumer<OrderPaymentReceivedEvent>
{
    private readonly ILogger<OrderPaymentReceivedEventConsumers> _logger;

    public OrderPaymentReceivedEventConsumers(ILogger<OrderPaymentReceivedEventConsumers> logger)
    {
        _logger = logger;
    }
    
    public Task Consume(ConsumeContext<OrderPaymentReceivedEvent> context)
    {
        //Business rules
        //Change order status
        //Update the stocks quantity

        var orderId = context.Message.OrderId;
        var customerId = context.Message.CustomerId;
        
        _logger.LogInformation("Order Payment Received, OrderId: {orderId}, CustomerId: {customerId}", orderId, customerId);
        
        return Task.CompletedTask;
    }
}