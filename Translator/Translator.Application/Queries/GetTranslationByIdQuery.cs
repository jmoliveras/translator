using MediatR;
using Translator.Application.DTO;

namespace Translator.Application.Queries
{
    public class GetTranslationByIdQuery : IRequest<BaseDto>
    {
        public Guid Id { get; private set; }

        public GetTranslationByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
