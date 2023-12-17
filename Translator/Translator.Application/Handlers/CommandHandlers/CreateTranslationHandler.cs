using MediatR;
using Translator.Application.Commands;
using Translator.Application.Mappers;
using Translator.Application.Responses;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers.CommandHandlers
{
    public class CreateTranslationHandler : IRequestHandler<CreateTranslationCommand, TranslationResponse>
    {
        private readonly ITranslationCommandRepository _translationCommandRepository;
        public CreateTranslationHandler(ITranslationCommandRepository repository)
        {
            _translationCommandRepository = repository;
        }
        public async Task<TranslationResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {            
            var entity = TranslationMapper.Mapper.Map<Translation>(request);

            if (entity is null)
            {
                throw new ApplicationException("AutoMapper error.");
            }

            var id = Guid.NewGuid();

            entity.Id = id;

            // TRADUCIR CON IA, Devolver id

            var newTranslation = await _translationCommandRepository.AddAsync(entity);
            var response = TranslationMapper.Mapper.Map<TranslationResponse>(newTranslation);

            return response;
        }
    }
}
