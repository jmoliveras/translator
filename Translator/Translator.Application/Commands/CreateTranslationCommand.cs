using MediatR;

namespace Translator.Application.Commands
{
    public class CreateTranslationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public required string OriginalText { get; set; }        
    }
}
