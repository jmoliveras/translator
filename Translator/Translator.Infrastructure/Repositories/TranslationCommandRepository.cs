using Translator.Domain.Entities;
using Translator.Domain.Interfaces;

namespace Translator.Infrastructure.Data.Repositories
{
    public class TranslationCommandRepository : CommandRepository<Translation>, ITranslationCommandRepository
    {
        public TranslationCommandRepository(TranslationContext context) : base(context)
        {

        }
    }
}
