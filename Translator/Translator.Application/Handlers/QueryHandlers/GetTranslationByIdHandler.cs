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
        private readonly ITranslationQueryRepository _translationQueryRepository = repository;

        public async Task<BaseDto> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _translationQueryRepository.GetTranslationByIdAsync(request.Id);

                if (result == null)
                {
                    return new BaseDto
                    {
                        Result = ErrorMessages.NoTranslation
                    };
                }
                else if (result.Status == Status.Error)
                {
                    return new TranslationErrorDtoBuilder()
                        .WithErrorMessage(result.Result)
                        .WithResult(ErrorMessages.ErrorOccurred)
                        .WithOriginalText(result.OriginalText)
                        .Build();
                }
                else
                {
                    return new TranslationDtoBuilder()
                       .WithTranslation(result.Result)
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
