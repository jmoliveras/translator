using MediatR;
using Translator.Application.Commands;
using Translator.Application.Constants;
using Translator.Application.Handlers.Interfaces;
using Translator.Application.Mappers;
using Translator.Application.Responses;
using Translator.Application.Services.Interfaces;
using Translator.Domain.Entities;

namespace Translator.Application.Handlers.CommandHandlers
{
    public class CreateTranslationHandler(ITranslationService translationService,
        IFireForgetRepositoryHandler fireForgetRepositoryHandler) : IRequestHandler<CreateTranslationCommand, TranslationResponse>
    {
        private readonly ITranslationService _translationService = translationService;
        private readonly IFireForgetRepositoryHandler _fireForgetRepositoryHandler = fireForgetRepositoryHandler;

        /// <summary>
        /// Handles language detection and translation, then triggers an insert into the db using another context to avoid ObjectDisposedException triggered when using the main thread context.
        /// </summary>
        /// <param name="request">The command.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="ApplicationException">AutoMapper error.</exception>
        public async Task<TranslationResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            var entity = TranslationMapper.Mapper.Map<Translation>(request) ?? throw new ApplicationException(ErrorMessages.AutoMapper);
            var detectedLang = await _translationService.DetectLanguage(request.Text);

            entity.Text = detectedLang != Languages.Spanish ? await _translationService.TranslateText(request.Text) : request.Text;

            _fireForgetRepositoryHandler.Execute(async repository =>
            {
                await repository.AddAsync(entity);
            });

            return new TranslationResponse();
        }
    }
}
