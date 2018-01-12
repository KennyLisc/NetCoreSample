using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using FXTech.PDCA.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using NetCoreSample.Core.Domain;
using NetCoreSample.Data;

namespace ConsoleApp.Services
{

    public class UserScoreService : IUserScoreService
    {
        #region Fields

        private readonly IRepository<UserScore> _userScoreRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public UserScoreService(IRepository<UserScore> userScoreRepository, IUnitOfWork unitOfWork)
        {
            _userScoreRepository = userScoreRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IUserScoreService Members

        public async Task<UserScore> FindById(Guid id)
        {
            return await _userScoreRepository.GetByIdAsync(id);
        }

        public async Task<UserScore> GetById(Guid id)
        {
            return await _userScoreRepository.GetByIdReadOnlyAsync(id);
        }

        public async Task Insert(UserScore item)
        {
            _userScoreRepository.Insert(item);
            
            await Task.CompletedTask;
        }

        public async Task Update(UserScore item)
        {
            _userScoreRepository.Update(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(UserScore item)
        {
            _userScoreRepository.Delete(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task SaveChanges()
        {
            await _unitOfWork.CommitAsync();
        }

        #endregion

        #region Search

        public async Task<IList<UserScore>> Search(string keyword)
        {
            Expression<Func<UserScore, bool>> func = x => keyword == null || x.UserName.Contains(keyword);

            var data = _userScoreRepository.GetAllReadOnly().Where(func).OrderBy(x => x.UserName);

            return await data.ToListAsync();
        }

        #endregion
    }
}
