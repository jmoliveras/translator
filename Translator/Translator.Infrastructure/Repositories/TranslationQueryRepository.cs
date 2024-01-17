using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces;
using Translator.Infrastructure.Data.SqlQueries;

namespace Translator.Infrastructure.Data.Repositories
{
    public class TranslationQueryRepository(IConfiguration configuration) : QueryRepository<Translation>(configuration), ITranslationQueryRepository
    {
        public async Task<Translation> GetTranslationByIdAsync(Guid id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("id", id, DbType.Guid);

                using var connection = CreateConnection();
                return await connection.QuerySingleAsync<Translation>(TranslationQueries.GetTranslationByIdQuery(), parameters);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
