using MediatR;

namespace Translator.Application.Commands
{
    public class CreateTranslationCommand : IRequest<Guid>
    {
        public required string OriginalText { get; set; }
    }
}
