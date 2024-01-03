using Microsoft.Extensions.Configuration;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Infrastructure.Data.Repositories
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : BaseEntity
    {
        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
