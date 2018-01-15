using Dapper;
using NetCoreSample.Core.Core.Interface;
using NetCoreSample.Core.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreSample.Data.Search
{
    public class RaceDBSearch: DapperDbSearchBase, IRaceDBSearch
    {
        public RaceDBSearch(IDbConfig dbConfig) : base(dbConfig)
        {
        }

        public async Task<IList<Race>> SearchRaceList(string keyword)
        {
            using (var con = GetOpenConnection())
            {
                return (await con.QueryAsync<Race>("SELECT * FROM Races WHERE @keyword IS NULL OR Name like '%'+@keyword + '%'",
                    new {keyword})).ToList();
            }
        }
    }
}
