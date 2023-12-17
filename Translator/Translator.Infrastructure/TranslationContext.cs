using Microsoft.EntityFrameworkCore;
using Translator.Domain.Entities;
using Translator.Infrastructure.Data.Configuration;

namespace Translator.Infrastructure.Data
{
    public class TranslationContext : DbContext
    {

        public TranslationContext(DbContextOptions<TranslationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.ApplyConfiguration(new TranslationConfiguration());
        }

        public DbSet<Translation> Translations { get; set; }
    }
}
