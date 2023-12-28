namespace Translator.Application.DTO
{
    public record TranslationErrorDto : TranslationBaseDto
    {
        public required string ErrorMessage { get; set; }
    }
}
