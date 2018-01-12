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
    public class RaceService : IRaceService
    {
        #region Fields

        private readonly IRepository<Race> _raceRepository;
        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public RaceService(IRepository<Race> raceRepository, IUnitOfWork unitOfWork)
        {
            _raceRepository = raceRepository;
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRaceService Members

        public async Task<Race> FindById(Guid id)
        {
            return await _raceRepository.GetByIdAsync(id);
        }

        public async Task<Race> GetById(Guid id)
        {
            return await _raceRepository.GetByIdReadOnlyAsync(id);
        }

        public async Task Insert(Race item)
        {
            _raceRepository.Insert(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task Update(Race item)
        {
            _raceRepository.Update(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(Race item)
        {
            _raceRepository.Delete(item);
            await _unitOfWork.CommitAsync();
        }

        public async Task SaveChanges()
        {
            await _unitOfWork.CommitAsync();
        }

        #endregion

        #region Search

        public async Task<IList<Race>> Search(string keyword)
        {
            Expression<Func<Race, bool>> func = x => keyword == null || x.Name.Contains(keyword);

            var data = _raceRepository.GetAllReadOnly().Where(func).OrderBy(x => x.Name);

            return await data.ToListAsync();
        }

        #endregion
    }
}
