namespace Translator.Application.DTO
{
    public abstract record TranslationBaseDto : BaseDto
    {
        public required string OriginalText { get; set; }
    }
}
