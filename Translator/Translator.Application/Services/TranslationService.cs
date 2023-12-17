using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Translation.Text;
using Translator.Application.Services.Interfaces;

namespace Translator.Application.Services
{
    public class TranslationService : ITranslationService
    {
        // TODO: Mover a AppSettings
        private static readonly AzureKeyCredential translationCredential = new AzureKeyCredential("f054402f70a647c6ad577357693bdb6f");
        private static readonly AzureKeyCredential languageCredential = new AzureKeyCredential("28b443ff23434d16a883cbc3d3f0ddbf");
        private static readonly Uri languageEndpoint = new Uri("https://langanalyticsai.cognitiveservices.azure.com/");

        public async Task<string> TranslateText(string text, string language = "es")
        {
            TextTranslationClient client = new(translationCredential, "westeurope");

            try
            {
                Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(language, text).ConfigureAwait(false);
                IReadOnlyList<TranslatedTextItem> translations = response.Value;
                TranslatedTextItem translation = translations.FirstOrDefault();
                              
                return translation?.Translations?.FirstOrDefault().Text;
            }
            catch 
            {              
                throw;
            }
        }

        public async Task<string> DetectLanguage(string text)
        {
            var client = new TextAnalyticsClient(languageEndpoint, languageCredential);
            var result = await client.DetectLanguageAsync(text);

            return result.Value.Iso6391Name;
        }
    }
}
