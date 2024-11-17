# Project Documentation: Order Creation, Payment, and Notification with MassTransit and RabbitMQ in .NET Core, Docker

## Overview

This project demonstrates the integration of MassTransit and RabbitMQ in a .NET Core application to manage order creation, payment processing, and sending SMS and email notifications. The system involves multiple services working together to simulate a real-world order processing and notification flow. RabbitMQ, running in a Docker container, is used for message queuing, ensuring decoupled communication between services. MassTransit is employed to simplify message-based communication and to manage the message-driven workflows.

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
- **Docker**: Application management in a container-based environment.

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

   Example of configuration in `Program.cs`:

   ```csharp
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

![Screenshot 1](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180438.png?raw=true)

![Screenshot 2](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180459.png?raw=true)

![Screenshot 3](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180505.png?raw=true)

![Screenshot 4](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180508.png?raw=true)

![Screenshot 5](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180511.png?raw=true)

![Screenshot 6](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180515.png?raw=true)

![Screenshot 7](https://github.com/ilkersatur/MassTransit-RabbitMQ-Order-Payment/blob/main/MassTransit-RabbitMQ-Order-Payment/Screenshot%202024-11-17%20180518.png?raw=true)


### RabbitMQ Installation with Docker

1. Download and install Docker on your computer from: [Docker Desktop](https://www.docker.com/products/docker-desktop)

2. To install RabbitMQ with Docker, use the following command:

```bash
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management

