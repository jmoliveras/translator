using MediatR;

namespace Translator.Application.Commands
{
    public class CreateTranslationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string OriginalText { get; set; }
    }
}
