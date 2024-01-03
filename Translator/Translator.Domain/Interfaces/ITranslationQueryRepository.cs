using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Domain.Interfaces
{
    public interface ITranslationQueryRepository : IQueryRepository<Translation>
    {
        Task<Translation?> GetTranslationByIdAsync(Guid id);
    }
}
