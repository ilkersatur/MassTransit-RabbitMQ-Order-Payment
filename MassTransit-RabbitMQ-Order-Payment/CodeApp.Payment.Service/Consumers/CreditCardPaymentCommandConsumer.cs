using CodeApp.Masstransit.Shared.Models.Payment.Commands;
using CodeApp.Masstransit.Shared.Models.Payment.Events;
using MassTransit;

namespace CodeApp.Payment.Service.Consumers;

public class CreditCardPaymentCommandConsumer : IConsumer<CreditCardPaymentCommand>
{
    private readonly IBus _bus;
    private readonly ILogger<CreditCardPaymentCommandConsumer> _logger;

    public CreditCardPaymentCommandConsumer(IBus bus, ILogger<CreditCardPaymentCommandConsumer> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<CreditCardPaymentCommand> context)
    {
        //Business rules
        //Find the customer stored credit card
        //Get the payment

        var customerId = context.Message.CustomerId;
        var orderId = context.Message.OrderId;
        var amount = context.Message.Amount;

        var cardNumber = "**** 1362";
        var paymentId = Guid.NewGuid();

        await _bus.Publish<OrderPaymentReceivedEvent>(new OrderPaymentReceivedEvent(CustomerId: customerId, Amount: amount, cardNumber, orderId, paymentId));
        
        _logger.LogInformation("Payment received, OrderId: {orderId}, PaymentId: {paymentId}", orderId, paymentId);
    }
}