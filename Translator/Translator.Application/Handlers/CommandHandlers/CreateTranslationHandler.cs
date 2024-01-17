using Azure.Core;
using MediatR;
using Translator.Application.Commands;
using Translator.Application.Constants;
using Translator.Application.Mappers;
using Translator.Application.Services.Interfaces;
using Translator.Domain.Entities;
using Translator.Domain.Enums;
using Translator.Domain.Interfaces;

namespace Translator.Application.Handlers.CommandHandlers
{
    public class CreateTranslationHandler(IServiceBusService serviceBusService,
        ITranslationCommandRepository repository) : IRequestHandler<CreateTranslationCommand, Guid>
    {
        private readonly IServiceBusService _serviceBusService = serviceBusService;
        private readonly ITranslationCommandRepository _repository = repository;

        /// <summary>
        /// Generates the translation request identifier and triggers the translation async process.
        /// </summary>
        /// <param name="request">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The translate request identifier.</returns>
        /// <exception cref="ApplicationException">AutoMapper error.</exception>
        public async Task<Guid> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            var entity = TranslationMapper.Mapper.Map<Translation>(request) ?? throw new ApplicationException(ErrorMessages.AutoMapper);
            entity.Status = Status.Pending;

            entity = await _repository.AddAsync(entity);
            await _serviceBusService.SendMessageAsync(entity.Id);

            return entity.Id;
        }
    }
}