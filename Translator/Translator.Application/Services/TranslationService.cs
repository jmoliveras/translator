using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Translation.Text;
using Translator.Application.Constants;
using Translator.Application.Services.Interfaces;

namespace Translator.Application.Services
{
    public class TranslationService : ITranslationService
    {
        // TODO: Mover a AppSettings
        private static readonly AzureKeyCredential translationCredential = new("f054402f70a647c6ad577357693bdb6f");
        private static readonly AzureKeyCredential languageCredential = new("28b443ff23434d16a883cbc3d3f0ddbf");
        private static readonly Uri languageEndpoint = new("https://langanalyticsai.cognitiveservices.azure.com/");
        private static readonly string region = "westeurope";
        
        /// <summary>
        /// Translates given text into given language.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <param name="language">The language code, "es" by default.</param>
        /// <returns>The translated text.</returns>
        public async Task<string> TranslateText(string text, string language = Languages.Spanish)
        {
            TextTranslationClient client = new(translationCredential, region);

            try
            {
                Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(language, text).ConfigureAwait(false);
                IReadOnlyList<TranslatedTextItem> translations = response.Value;

                return translations.FirstOrDefault()?.Translations?.FirstOrDefault()?.Text ?? throw new Exception(ErrorMessages.NoTranslation);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Detects the language of the given text.
        /// </summary>
        /// <param name="text">The text whose language will be detected.</param>
        /// <returns>The ISO 6391 language name.</returns>
        public async Task<string> DetectLanguage(string text)
        {
            var client = new TextAnalyticsClient(languageEndpoint, languageCredential);
            var result = await client.DetectLanguageAsync(text);

            return result.Value.Iso6391Name;
        }
    }
}
