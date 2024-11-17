using CodeApp.Masstransit.Shared.Extensions;
using CodeApp.Masstransit.Shared.Models.Payment.Commands;
using CodeApp.Masstransit.Shared.Settings;
using CodeApp.Payment.Service;
using CodeApp.Payment.Service.Consumers;
using MassTransit;
using MassTransit.Configuration;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker>();

builder.Services.Configure<MassTransitSettings>(builder.Configuration.GetSection("MassTransitSettings"));
builder.Services.AddMassTransit(x =>
{
    x.RegisterConsumer<CreditCardPaymentCommandConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        var massTransitSettings = context.GetRequiredService<IOptions<MassTransitSettings>>().Value;
        cfg.Host(massTransitSettings.Host, massTransitSettings.VirtualHost, host =>
        {
            host.Username(massTransitSettings.Username);
            host.Password(massTransitSettings.Password);
        });
        cfg.RegisterQueue<CreditCardPaymentCommandConsumer>(context, massTransitSettings.QueueName, typeof(CreditCardPaymentCommand));
    });
});

var host = builder.Build();
host.Run();