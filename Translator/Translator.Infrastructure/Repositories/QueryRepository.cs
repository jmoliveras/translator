using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;
using Translator.Infrastructure.Data.SqlQueries;

namespace Translator.Infrastructure.Data.Repositories
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : BaseEntity
    {
        protected readonly TranslationContext _context;

        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {
            
        }        
    }
}
