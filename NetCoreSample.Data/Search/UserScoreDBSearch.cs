using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NetCoreSample.Core.Core.Interface;
using NetCoreSample.Core.Domain;

namespace NetCoreSample.Data.Search
{
    public class UserScoreDBSearch: DapperDbSearchBase, IUserScoreDBSearch
    {
        public UserScoreDBSearch(IDbConfig dbConfig) : base(dbConfig)
        {
        }


        public async Task<IList<UserScore>> SearchList(string keyword)
        {
            using (var con = GetOpenConnection())
            {
                return (await con.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE @keyword IS NULL OR UserName like '%'+@keyword + '%'",
                    new { keyword })).ToList();
            }
        }
    }
}
