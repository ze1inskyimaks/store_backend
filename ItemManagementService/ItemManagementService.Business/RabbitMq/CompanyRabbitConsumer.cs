using System.Text;
using System.Text.Json;
using ItemManagementService.Business.ModelDto.Company;
using ItemManagementService.Data;
using ItemManagementService.Data.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ItemManagementService.Business.RabbitMq;

public class CompanyRabbitConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IChannel _channel;

    public CompanyRabbitConsumer(IServiceScopeFactory scopeFactory) 
    {
        _scopeFactory = scopeFactory;
        
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
        _channel = connection.CreateChannelAsync().GetAwaiter().GetResult();

        _channel.QueueDeclareAsync(queue: "company_queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var data = JsonSerializer.Deserialize<CompanyDto>(message);

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                var company = new Company { Id = data!.Id }; 
                dbContext.Companies.Add(company);
                await dbContext.SaveChangesAsync();
                
            }

            await _channel.BasicAckAsync(ea.DeliveryTag, false);
        };

        await _channel.BasicConsumeAsync(queue: "company_queue", autoAck: false, consumer: consumer, cancellationToken: stoppingToken);
    }
}