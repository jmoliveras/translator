using Translator.Application.Services.Interfaces;
using Azure.Messaging.ServiceBus;
using Translator.Application.Settings;

namespace Translator.Application.Services
{
    public class ServiceBusService(TranslationSettings settings) : IServiceBusService
    {
        private readonly TranslationSettings _settings = settings;

        public async Task SendMessageAsync(Guid id)
        {
            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };

            var client = new ServiceBusClient(_settings.AzureServiceBusConnectionString, clientOptions);
            var sender = client.CreateSender(_settings.QueueName);

            // create a batch 
            using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();
            messageBatch.TryAddMessage(new ServiceBusMessage(id.ToString()));
            try
            {
                // Use the producer client to send the batch of messages to the Service Bus queue
                await sender.SendMessagesAsync(messageBatch);
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await sender.DisposeAsync();
                await client.DisposeAsync();
            }
        }
    }
}
