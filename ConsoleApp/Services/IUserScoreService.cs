using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreSample.Core.Domain;

namespace ConsoleApp.Services
{

    public interface IUserScoreService
    {
        #region UserScore

        Task<UserScore> FindById(Guid id);

        Task<UserScore> GetById(Guid id);

        Task Insert(UserScore item);

        Task Update(UserScore item);

        Task Delete(UserScore item);

        Task SaveChanges();

        #endregion

        #region Search

        Task<IList<UserScore>> Search(string keyword);

        #endregion

    }
}
