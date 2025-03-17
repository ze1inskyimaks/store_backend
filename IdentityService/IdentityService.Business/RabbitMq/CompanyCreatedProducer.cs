using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace IdentityService.Business.RabbitMq;

public class CompanyCreatedProducer
{
    private readonly IChannel _channel;

    public CompanyCreatedProducer()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.ExchangeDeclareAsync(exchange: "company_exchange", type: ExchangeType.Direct);
        _channel.QueueDeclareAsync(queue: "company_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBindAsync(queue: "company_queue", exchange: "company_exchange", routingKey: "company.created");
    }

    public void SendCompanyCreatedMessage(string companyId)
    {
        var message = new { Id = companyId };
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        _channel.BasicPublishAsync(exchange: "company_exchange",
            routingKey: "company.created",
            body: body);
    }
}