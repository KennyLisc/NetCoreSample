using NetCoreSample.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//IMPORTANT - Modifications to this file may be overwritten:
//If you need to implement your own logic/code do it in a partial class/interface.    

namespace NetCoreSample.Data
{
    /// <summary>
    /// The generic base interface for all repositories...
    /// Purpose:
    /// - Implement this on the repository... Regardless of datasource... Xml, MSSQL, MYSQL etc..
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T: BaseSimpleEntity
    {
        /// <summary>
        /// Retrieves an entity (T) from the repository by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Guid id);

        Task<T> GetByIdAsync(Guid id);

        T GetByIdReadOnly(Guid id);

        Task<T> GetByIdReadOnlyAsync(Guid id);

        /// <summary>
        /// Returns all persistent entities of type T.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Return all persistent entities of type T without tracking.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllReadOnly();

        /// <summary>
        /// Adds a new entity (T) and returns it´s id.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity);

        void Insert(T entity);

        /// <summary>
        /// Remove an entity (T) and persist changes into repository.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// Gives the possibility to search entities with strongly typed expressions.
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="maxHits"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        IQueryable<T> Search(Expression<Func<T, bool>> expression);

        DbSet<T> Entities { get; }

        //delete other entity
        void DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseSimpleEntity;

        void InsertEntity<TEntity>(TEntity entity) where TEntity : BaseSimpleEntity;
    }
}