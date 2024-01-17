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

        public async Task<T> GetAsync(Guid id)
        {
            return await _context.FindAsync<T>(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
