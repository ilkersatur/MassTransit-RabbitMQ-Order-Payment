using CodeApp.Masstransit.Shared.Models.Product.Models;

namespace CodeApp.Order.Api.Models;

public record CreateOrder(Guid CustomerId, List<Product> Products);