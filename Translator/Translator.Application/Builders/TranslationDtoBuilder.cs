using Translator.Application.DTO;

namespace Translator.Application.Builders
{
    public class TranslationDtoBuilder
    {
        private TranslationDto _translation = new()
        {
            DetectedLanguage = string.Empty,
            OriginalText = string.Empty,
            Result = string.Empty
        };

        public TranslationDtoBuilder WithDetectedLanguage(string lang)
        {
            _translation.DetectedLanguage = lang;
            return this;
        }

        public TranslationDtoBuilder WithOriginalText(string text)
        {
            _translation.OriginalText = text;
            return this;
        }

        public TranslationDtoBuilder WithTranslation(string translation)
        {
            _translation.Result = translation;
            return this;
        }

        public TranslationDto Build()
        {
            return _translation;
        }
    }
}
