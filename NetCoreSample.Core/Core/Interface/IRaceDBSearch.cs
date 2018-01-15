using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreSample.Core.Domain;

namespace NetCoreSample.Core.Core.Interface
{
    public interface IRaceDBSearch
    {
        Task<IList<Race>> SearchRaceList(string keyword);
    }
}
