using Translator.Domain.Entities;

namespace Translator.Domain.Interfaces.Base
{
    public interface IQueryRepository<T> where T : BaseEntity
    {
        //Task<T> GetByIdAsync(Guid id);
    }
}
