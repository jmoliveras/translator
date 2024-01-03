using Translator.Application.Constants;

namespace Translator.Application.Services.Interfaces
{
    public interface ITranslationService
    {
        Task<string> TranslateText(string text, string language = Languages.Spanish);
        Task<string> DetectLanguage(string text);
    }
}
