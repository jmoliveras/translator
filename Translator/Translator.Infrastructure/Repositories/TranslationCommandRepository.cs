using Translator.Domain.Entities;
using Translator.Domain.Interfaces;

namespace Translator.Infrastructure.Data.Repositories
{
    public class TranslationCommandRepository(TranslationContext context) : CommandRepository<Translation>(context), ITranslationCommandRepository
    {
    }
}
