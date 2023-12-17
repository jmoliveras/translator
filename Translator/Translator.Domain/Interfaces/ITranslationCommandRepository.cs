using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Domain.Interfaces
{
    public interface ITranslationCommandRepository : ICommandRepository<Translation>
    {
    }
}
