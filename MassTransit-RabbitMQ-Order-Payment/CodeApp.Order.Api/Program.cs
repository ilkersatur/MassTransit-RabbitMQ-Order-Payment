using CodeApp.Masstransit.Shared.Extensions;
using CodeApp.Masstransit.Shared.Models.Payment.Events;
using CodeApp.Masstransit.Shared.Settings;
using CodeApp.Order.Api.Consumers;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.Configure<MassTransitSettings>(builder.Configuration.GetSection("MassTransitSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<OrderPaymentReceivedEventConsumers>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<MassTransitSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });
       cfg.RegisterQueue<OrderPaymentReceivedEventConsumers>(context, massTransitSettings.QueueName, typeof(OrderPaymentReceivedEvent));
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseRouting();
app.UseHttpsRedirection();
app.Run();