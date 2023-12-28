using MediatR;
using Translator.Application.DTO;
using Translator.Application.Queries;
using Translator.Domain.Interfaces;
using Translator.Domain.Enums;
using Translator.Application.Constants;

namespace Translator.Application.Handlers.QueryHandlers
{
    public class GetTranslationByIdHandler(ITranslationQueryRepository repository) : IRequestHandler<GetTranslationByIdQuery, BaseDto>
    {
        private readonly ITranslationQueryRepository _translationQueryRepository = repository;

        public async Task<BaseDto> Handle(GetTranslationByIdQuery request, CancellationToken cancellationToken)
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
                return new TranslationErrorDto
                {
                    Result = ErrorMessages.ErrorOccurred,
                    ErrorMessage = result.Result, 
                    OriginalText = result.OriginalText 
                };
            } 
            else
            {
                return new TranslationDto
                {
                    DetectedLanguage = result.DetectedLanguage ?? string.Empty,
                    OriginalText = result.OriginalText,
                    Result = result.Result
                };
            }
        }
    }
}
