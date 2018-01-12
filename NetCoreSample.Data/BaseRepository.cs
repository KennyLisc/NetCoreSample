using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCoreSample.Core.Domain;

namespace NetCoreSample.Data
{
    /// <summary>
    /// An abstract baseclass handling basic CRUD operations against the context.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IDisposable, IRepository<T> where T : BaseSimpleEntity
    {
        private DbSet<T> _entities;
        private readonly ApplicationDbContext _applicationDbContext;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// The name of the Generic entity using the repository.
        /// Used for smoother queries.
        /// </summary>
        protected string EntitySetName { get; set; }

        #region IRepository<T> Members

        /// <summary>
        /// Get one entity of T
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(Guid id)
        {
            return Entities.Find(id);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Entities.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual T FindById(Guid id)
        {
            return Entities.Find(id);
        }

        public virtual T GetByIdReadOnly(Guid id)
        {
            return Entities.AsNoTracking().FirstOrDefault(x => x.Id.Equals(id));
        }

        public Task<T> GetByIdReadOnlyAsync(Guid id)
        {
            return Entities.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        /// <summary>
        /// Get all entities of T
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return Entities;
        }

        /// <summary>
        /// Get all entities of T without tracking
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAllReadOnly()
        {
            return Entities.AsNoTracking();
        }


        /// <summary>
        /// Insert a new entity of T.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Insert(T entity)
        {
            Entities.Add(entity);
            /*            if (UnitOfWork.IsPersistent(entity))
                        {
                            DataContext.Entry(entity).State = EntityState.Modified;
                        }
                        else
                            Entities.Add(entity);*/
        }

        /// <summary>
        /// updates an in the context existing entity (if it´s changed).
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Update(T entity)
        {
            _applicationDbContext.Entry(entity).State = EntityState.Modified;

        }

        /// <summary>
        /// Removes an entity T from the context and persist the change.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        /// <summary>
        /// The LinqExpression will give us the opportunity to write strongly typed object queries to this methodsignature.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IQueryable<T> Search(Expression<Func<T, bool>> expression)
        {
            return Entities.Where(expression);
        }

        /// <summary>
        /// The LinqExpression will give us the opportunity to write strongly typed object queries to this methodsignature.
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return Entities.Where(expression);
        }

        public void Dispose()
        {

        }


        #endregion

        public DbSet<T> Entities => _entities ?? (_entities = _applicationDbContext.Set<T>());

        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseSimpleEntity
        {
            _applicationDbContext.Set<TEntity>().Remove(entity);
        }

        public void InsertEntity<TEntity>(TEntity entity) where TEntity : BaseSimpleEntity
        {
            _applicationDbContext.Set<TEntity>().Add(entity);
        }
    }
}