﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using Translator.Domain.Entities;
using Translator.Domain.Interfaces;
using Translator.Infrastructure.Data.SqlQueries;

namespace Translator.Infrastructure.Data.Repositories
{
    public class TranslationQueryRepository : QueryRepository<Translation>, ITranslationQueryRepository
    {
        public TranslationQueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }

        public async Task<string> GetTranslationByIdAsync(Guid id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("id", id, DbType.Guid);

                using var connection = CreateConnection();
                return await connection.QuerySingleOrDefaultAsync<string>(TranslationQueries.GetTranslationByIdQuery(), parameters) ?? throw new Exception("Databse connection error.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}