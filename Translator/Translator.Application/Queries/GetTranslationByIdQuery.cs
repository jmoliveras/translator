using MediatR;

namespace Translator.Application.Queries
{
    public class GetTranslationByIdQuery : IRequest<string>
    {
        public Guid Id { get; private set; }

        public GetTranslationByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
