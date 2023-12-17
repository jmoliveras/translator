using MediatR;
using Translator.Application.Queries;
using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers.QueryHandlers
{
    public class GetTranslationByIdHandler : IRequestHandler<GetTranslationByIdQuery, string>
    {
        private readonly ITranslationQueryRepository _translationQueryRepository;

        public GetTranslationByIdHandler(ITranslationQueryRepository repository)
        {
            _translationQueryRepository = repository;
        }

        public async Task<string> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            return await _translationQueryRepository.GetTranslationByIdAsync(request.Id);
        }
    }
}
