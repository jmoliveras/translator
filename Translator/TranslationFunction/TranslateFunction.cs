using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Translator.Application.Services.Interfaces;

namespace TranslationFunction
{
    public class TranslateFunction(ILoggerFactory loggerFactory, ITranslationService translationService)
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<TranslateFunction>();
        private readonly ITranslationService _translationService = translationService;

        [Function("Translate")]
        public async Task Run([ServiceBusTrigger("translatorqueue", Connection = "AzureServiceBusConnectionString")] string myQueueItem)
        {
            await _translationService.ProcessTranslation(new Guid(myQueueItem));
        }
    }
}
