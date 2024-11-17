using CodeApp.Masstransit.Shared.Models.Payment.Commands;
using CodeApp.Order.Api.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CodeApp.Order.Api.Controllers;

[Route("api/v1/order")]
public class OrderController : ControllerBase
{
    private readonly IBus _bus;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IBus bus, ILogger<OrderController> logger)
    {
        _bus = bus;
        _logger = logger;
    }
    
    [HttpPost]
    public async Task<AcceptedResult> CreateOrder([FromBody] CreateOrder order)
    {
        //Business rules
        //Save the order into DB
        var orderId = Guid.NewGuid();
        var customerId = order.CustomerId;
        var amount = order.Products.Sum(x => x.Quantity * x.Price);

        await _bus.Publish<CreditCardPaymentCommand>(new CreditCardPaymentCommand(customerId, amount, orderId));

        _logger.LogInformation("Order accepted {orderId}", orderId);
        return Accepted();
    }
}