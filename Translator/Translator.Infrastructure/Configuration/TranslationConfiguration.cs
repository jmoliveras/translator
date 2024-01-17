using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Translator.Domain.Entities;

namespace Translator.Infrastructure.Data.Configuration
{
    internal class TranslationConfiguration : IEntityTypeConfiguration<Translation>
    {
        public void Configure(EntityTypeBuilder<Translation> builder)
        {
            builder.ToTable("translations");
            builder.HasKey(c => c.Id);     
            builder.Property(c => c.Status).IsRequired();
            builder.Property(c => c.OriginalText).IsRequired();            
        }
    }
}