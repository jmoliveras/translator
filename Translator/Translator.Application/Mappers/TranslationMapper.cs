﻿using AutoMapper;

namespace Translator.Application.Mappers
{
    public class TranslationMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => (p.GetMethod?.IsPublic ?? false) || (p.GetMethod?.IsAssembly ?? false);
                cfg.AddProfile<TranslationMappingProfile>();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
