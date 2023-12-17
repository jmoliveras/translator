using Translator.Domain.Entities;

namespace Translator.Domain.Interfaces.Base
{
    public interface ICommandRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
    }
}
