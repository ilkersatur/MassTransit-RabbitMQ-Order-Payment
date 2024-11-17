using MassTransit;

namespace CodeApp.Masstransit.Shared.Models.Payment.Commands;

//Exchanges isminin alındığı yer
[EntityName("payment.creditcardpaymentcommand")]
public record CreditCardPaymentCommand
{
    public Guid CustomerId { get; private set; }
    public double Amount { get; private set; }
    public Guid OrderId { get; private set; }

    public CreditCardPaymentCommand(Guid customerId, double amount, Guid orderId)
    {
        CustomerId = customerId;
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Amount = amount;
        OrderId = orderId;
    }
}