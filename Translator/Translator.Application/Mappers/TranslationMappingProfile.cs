using AutoMapper;
using Translator.Application.Commands;
using Translator.Application.Responses;
using Translator.Domain.Entities;

namespace Translator.Application.Mappers
{
    public class TranslationMappingProfile : Profile
    {
        public TranslationMappingProfile()
        {
            CreateMap<Translation, TranslationResponse>().ReverseMap();
            CreateMap<Translation, CreateTranslationCommand>().ReverseMap();
        }
    }
}
