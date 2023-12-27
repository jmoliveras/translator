using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Translation.Text;
using Microsoft.Extensions.Logging;
using Translator.Application.Constants;
using Translator.Application.Services.Interfaces;
using Translator.Application.Settings;

namespace Translator.Application.Services
{
    public class TranslationService(TranslationSettings settings, ILogger<TranslationService> logger) : ITranslationService
    {
        private readonly TranslationSettings _settings = settings;
        private readonly ILogger _logger = logger;

        /// <summary>
        /// Translates given text into given language.
        /// </summary>
        /// <param name="text">The text to translate.</param>
        /// <param name="language">The language code, "es" by default.</param>
        /// <returns>The translated text.</returns>
        public async Task<string> TranslateText(string text, string language = Languages.Spanish)
        {
            AzureKeyCredential translationCredential = new(_settings.TranslationCredential);
            var region = _settings.Region;

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
            Uri languageEndpoint = new(_settings.LanguageEndpoint);
            AzureKeyCredential languageCredential = new(_settings.LanguageCredential);          
            var client = new TextAnalyticsClient(languageEndpoint, languageCredential);

            try 
            { 
                var result = await client.DetectLanguageAsync(text);
                return result.Value.Iso6391Name;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }

           
        }
    }
}
