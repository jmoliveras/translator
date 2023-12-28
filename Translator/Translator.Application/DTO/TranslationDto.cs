namespace Translator.Application.DTO
{
    public record TranslationDto : TranslationBaseDto
    {
        public required string DetectedLanguage { get; set; }
    }
}
