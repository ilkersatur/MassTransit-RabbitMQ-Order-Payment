using MassTransit;

namespace CodeApp.Masstransit.Shared.Models.Notification.Commands;

[EntityName("notification.sendsmscommand")]
public record SendSmsCommand(string phoneNumber, string content);