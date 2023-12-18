using MediatR;
using Translator.Application.Queries;
using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers.QueryHandlers
{
    public class GetTranslationByIdHandler(ITranslationQueryRepository repository) : IRequestHandler<GetTranslationByIdQuery, string>
    {
        private readonly ITranslationQueryRepository _translationQueryRepository = repository;

        public async Task<string> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _translationQueryRepository.GetTranslationByIdAsync(request.Id);
        }
    }
}
