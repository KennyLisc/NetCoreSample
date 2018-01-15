using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreSample.Core.Domain;

namespace NetCoreSample.Core.Core.Interface
{
    public interface IUserScoreDBSearch
    {
        Task<IList<UserScore>> SearchList(string keyword);
    }
}
