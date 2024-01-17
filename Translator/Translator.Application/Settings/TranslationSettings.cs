namespace Translator.Application.Settings
{
    public sealed class TranslationSettings
    {
        public required string TranslationCredential { get; set; }
        public required string LanguageCredential { get; set; }
        public required string LanguageEndpoint { get; set; }
        public required string Region { get; set; }
        public required string AzureServiceBusConnectionString { get; set; }
        public required string QueueName { get; set; }
    }
}
