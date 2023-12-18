using MediatR;
using Translator.Application.Responses;

namespace Translator.Application.Commands
{
    public class CreateTranslationCommand : IRequest<TranslationResponse>
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }        
    }
}
