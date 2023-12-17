using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Domain.Interfaces
{
    public interface ITranslationQueryRepository : IQueryRepository<Translation>
    {
        Task<string> GetTranslationByIdAsync(Guid id);
    }
}
