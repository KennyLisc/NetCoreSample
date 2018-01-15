using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using NetCoreSample.Core.Core.Interface;

namespace NetCoreSample.Data.Search
{
    public abstract class DapperDbSearchBase
    {
        //DataContext
        //private readonly string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly IDbConfig _dbConfig;

        protected DapperDbSearchBase(IDbConfig dbConfig)
        {
            Console.WriteLine("DapperDbSearchBase cntrs");
            Console.WriteLine(dbConfig.ConnectionString);
            _dbConfig = dbConfig;
        }

        public IDbConnection GetOpenConnection()
        {
            // var connection = new SqlConnection(_dbConfig.GetConnectionString());
            var connection = new SqlConnection(_dbConfig.ConnectionString);
            connection.Open();
            return connection;
        }

        protected async Task<int> GetDataCount(string sql, dynamic param)
        {
            using (var connection = GetOpenConnection())
            {
                var item = (await connection.QueryAsync<dynamic>(sql, (object)param)).FirstOrDefault();
                return item == null ? 0 : item.Count;
            }
        }

        protected async Task<IEnumerable<T>> GetData<T>(string sql, dynamic param)
        {
            using (var connection = GetOpenConnection())
            {
                var items = (await connection.QueryAsync<T>(sql, (object)param)).ToList();
                return items;
            }
        }
    }
}
