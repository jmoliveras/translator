using MediatR;
using Translator.Application.Responses;

namespace Translator.Application.Commands
{
    public class CreateTranslationCommand : IRequest<TranslationResponse>
    {
        public string Text { get; set; }
    }
}
