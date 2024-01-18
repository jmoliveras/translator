using MediatR;
using Translator.Application.DTO;
using Translator.Application.Queries;
using Translator.Domain.Interfaces;
using Translator.Domain.Enums;
using Translator.Application.Constants;
using Translator.Application.Builders;

namespace Translator.Application.Handlers.QueryHandlers
{
    public class GetTranslationByIdHandler(ITranslationQueryRepository repository) : IRequestHandler<GetTranslationByIdQuery, BaseDto>
    {
        private readonly ITranslationQueryRepository _repository = repository;

        public async Task<BaseDto> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetTranslationByIdAsync(request.Id);

                if (result == null)
                {
                    return new BaseDto
                    {
                        Result = ErrorMessages.NoTranslation
                    };
                }

                if (result.Status == Status.Pending)
                {
                    return new TranslationDtoBuilder()
                       .WithTranslation(Messages.TranslationInProgress)
                       .WithOriginalText(result.OriginalText)
                       .Build();
                }
                else if (result.Status == Status.Error)
                {
                    return new TranslationErrorDtoBuilder()
                        .WithErrorMessage(result.Result ?? string.Empty)
                        .WithResult(ErrorMessages.ErrorOccurred)
                        .WithOriginalText(result.OriginalText)
                        .Build();
                }
                else
                {
                    return new TranslationDtoBuilder()
                       .WithTranslation(result.Result ?? string.Empty)
                       .WithDetectedLanguage(result.DetectedLanguage ?? string.Empty)
                       .WithOriginalText(result.OriginalText)
                       .Build();
                }
            }
            catch (Exception ex)
            {
                return new BaseDto
                {
                    Result = ex.Message
                };
            }
        }
    }
}
