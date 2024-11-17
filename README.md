# Project Documentation: Order Creation, Payment, and Notification System with MassTransit and RabbitMQ in .NET Core

## Overview

This project demonstrates the integration of MassTransit and RabbitMQ in a .NET Core application to manage order creation, payment processing, and sending SMS and email notifications. The system involves multiple services working together to simulate a real-world order processing and notification flow. RabbitMQ is used for message queuing, ensuring decoupled communication between services. MassTransit is employed to simplify message-based communication and to manage the message-driven workflows.

---

## Key Components
1. **Order Service** - Responsible for receiving and processing new orders.
2. **Payment Service** - Handles payment processing after an order is placed.
3. **Notification Service** - Sends email and SMS notifications once the payment is confirmed.

---

## Architecture

The architecture follows a microservices approach, where each service is responsible for a distinct part of the workflow:
- **Order Service**: Initiates the order and communicates with the Payment Service to process the payment.
- **Payment Service**: Receives payment details, processes the payment, and triggers notifications upon successful payment.
- **Notification Service**: Sends SMS and email notifications.

MassTransit is used for asynchronous communication between these services via RabbitMQ as the message broker.

---

## Technologies Used
- **MassTransit**: A .NET-based service bus for building message-based applications.
- **RabbitMQ**: A message broker that handles the transmission of messages between services.
- **.NET Core**: Framework used to build the application and the individual services.
- **SMTP**: For sending email notifications.
- **SMS Provider (e.g., Twilio)**: For sending SMS notifications.

---

## Workflow

1. **Order Creation**:
   - The user places an order, which is captured by the Order Service.
   - A message is sent to the Payment Service to process the payment. The message includes order details and payment information.

2. **Payment Processing**:
   - The Payment Service receives the order and payment details.
   - The payment is processed (e.g., payment gateway integration).
   - Upon successful payment, a message is sent to the Notification Service to trigger the SMS and email notifications.

3. **Notification Sending**:
   - The Notification Service receives the payment success message and sends SMS and email notifications to the customer, confirming the successful order and payment.

---

## MassTransit & RabbitMQ Integration

1. **Setting up RabbitMQ**:
   - RabbitMQ is configured to manage queues for each service communication. Separate queues are set for orders, payments, and notifications.
   - Services subscribe to their respective queues to listen for incoming messages.

2. **MassTransit Configuration**:
   - MassTransit is integrated with RabbitMQ to handle the asynchronous communication.
   - The `ISendEndpoint` and `IReceiveEndpoint` interfaces are used to send and receive messages between the services.

   Example of configuration in `Startup.cs`:

   ```csharp
   services.AddMassTransit(x =>
   {
       x.AddConsumer<PaymentConsumer>();
       x.AddConsumer<NotificationConsumer>();
       x.UsingRabbitMq((context, cfg) =>
       {
           cfg.Host("rabbitmq://localhost");
           cfg.ReceiveEndpoint("order_queue", e =>
           {
               e.ConfigureConsumer<OrderConsumer>(context);
           });
           cfg.ReceiveEndpoint("payment_queue", e =>
           {
               e.ConfigureConsumer<PaymentConsumer>(context);
           });
           cfg.ReceiveEndpoint("notification_queue", e =>
           {
               e.ConfigureConsumer<NotificationConsumer>(context);
           });
       });
   });
