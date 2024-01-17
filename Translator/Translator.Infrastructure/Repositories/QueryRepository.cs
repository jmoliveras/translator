using Microsoft.Extensions.Configuration;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces.Base;

namespace Translator.Infrastructure.Data.Repositories
{
    public class QueryRepository<T>(IConfiguration configuration) : DbConnector(configuration), IQueryRepository<T> where T : BaseEntity
    {
    }
}
