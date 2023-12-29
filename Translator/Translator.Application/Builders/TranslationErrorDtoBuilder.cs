using Translator.Application.DTO;

namespace Translator.Application.Builders
{
    public class TranslationErrorDtoBuilder
    {
        private TranslationErrorDto _error = new()
        {
            ErrorMessage = string.Empty,
            OriginalText = string.Empty,
            Result = string.Empty
        };

        public TranslationErrorDtoBuilder WithErrorMessage(string error)
        {
            _error.ErrorMessage = error;
            return this;
        }

        public TranslationErrorDtoBuilder WithOriginalText(string text)
        {
            _error.OriginalText = text;
            return this;
        }

        public TranslationErrorDtoBuilder WithResult(string result)
        {
            _error.Result = result;
            return this;
        }

        public TranslationErrorDto Build()
        {
            return _error;
        }
    }
}
