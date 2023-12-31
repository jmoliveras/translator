﻿using AutoMapper;
using Translator.Application.Commands;
using Translator.Domain.Entities;

namespace Translator.Application.Mappers
{
    public class TranslationMappingProfile : Profile
    {
        public TranslationMappingProfile()
        {
            CreateMap<Translation, CreateTranslationCommand>().ReverseMap();
        }
    }
}
