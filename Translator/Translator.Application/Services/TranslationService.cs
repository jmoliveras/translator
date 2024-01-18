using Azure;
using Azure.AI.TextAnalytics;
using Azure.AI.Translation.Text;
using Microsoft.Extensions.Logging;
using Translator.Application.Constants;
using Translator.Application.Services.Interfaces;
using Translator.Application.Settings;
using Translator.Domain.Enums;
using Translator.Domain.Interfaces;

namespace Translator.Application.Services
{
    public class TranslationService(TranslationSettings settings,
        ILogger<TranslationService> logger,
        ITranslationQueryRepository queryRepository,
        ITranslationCommandRepository commandRepository) : ITranslationService
    {
        private readonly TranslationSettings _settings = settings;
        private readonly ILogger _logger = logger;
        private readonly ITranslationQueryRepository _queryRepository = queryRepository;
        private readonly ITranslationCommandRepository _commandRepository = commandRepository;

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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
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
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Retrieves pending translation with given identifier, performs translation and updates the entity with the translation result.
        /// </summary>
        /// <param name="id">The translation identifier.</param>
        /// <exception cref="NullReferenceException">Translation with given identifier not found.</exception>
        public async Task ProcessTranslation(Guid id)
        {
            var entity = await _queryRepository.GetTranslationByIdAsync(id) ?? throw new NullReferenceException(ErrorMessages.NoTranslation);

            try
            {
                var detectedLang = await DetectLanguage(entity.OriginalText);

                entity.Result = detectedLang != Languages.Spanish ? await TranslateText(entity.OriginalText) : null;
                entity.DetectedLanguage = detectedLang;
                entity.Status = Status.Success;
            }
            catch (Exception ex)
            {
                entity.Result = ex.Message;
                entity.Status = Status.Error;
            }
            finally
            {
                await _commandRepository.UpdateAsync(entity);
            }
        }
    }
}
