using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreSample.Core.Domain;

namespace ConsoleApp.Services
{
    public interface IRaceService
    {
        #region Race

        Task<Race> FindById(Guid id);

        Task<Race> GetById(Guid id);

        Task Insert(Race item);

        Task Update(Race item);

        Task Delete(Race item);

        Task SaveChanges();

        #endregion


        #region Search

        Task<IList<Race>> Search(string keyword);

        #endregion

    }
}
