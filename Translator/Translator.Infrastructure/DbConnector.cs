﻿using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Translator.Infrastructure.Data
{
    public class DbConnector
    {
        private readonly IConfiguration _configuration;

        protected DbConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            string _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing connection string");
            return new SqlConnection(_connectionString);
        }
    }
}
