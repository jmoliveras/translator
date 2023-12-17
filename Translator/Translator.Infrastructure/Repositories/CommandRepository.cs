using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Infrastructure.Data.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : BaseEntity
    {
        protected readonly TranslationContext _context;

        public CommandRepository(TranslationContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
