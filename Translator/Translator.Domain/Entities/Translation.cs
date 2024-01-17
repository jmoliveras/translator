using Translator.Domain.Enums;

namespace Translator.Domain.Entities
{
    public class Translation : BaseEntity
    {
        public string? Result { get; set; }
        public required string OriginalText { get; set; }
        public string? DetectedLanguage { get; set; }
        public required Status Status { get; set; }       
    }
}
