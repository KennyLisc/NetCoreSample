using FXTech.PDCA.Core.Interfaces.Data;
using Microsoft.EntityFrameworkCore;
using NetCoreSample.Core.Domain;
using NetCoreSample.Core.Domain.User;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreSample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UnitOfWork(ApplicationDbContext applicationContext)
        {
            _applicationDbContext = applicationContext;
            // _applicationDbContext.ObjectContext().SavingChanges += (sender, e) => BeforeSave(GetChangedOrNewEntities());
        }

        /*
         var entities = this.ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added &&
            x.Entity != null && typeof(BaseEntity<int>).IsAssignableFrom(x.Entity.GetType())))
            .Select(x => x.Entity)
            .ToList();

        // Set the create/modified date as appropriate
        foreach (var entity in entities)
        {
            var entityBase = entity as BaseEntity<int>;
            if (entry.State == EntityState.Added)
            {
                entityBase.CreateDate = currentTime;
                entityBase.CreateUserId = userId;
            }

            entityBase.ModifyDate = currentTime;
            entityBase.ModifyUserId = userId;
        }
         */
        private void BeforeSave()
        {
            var now = DateTime.Now;
            var transactionId = Guid.NewGuid();
            var entities = this._applicationDbContext.ChangeTracker
                .Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added &&
                            x.Entity != null && (x.Entity is BaseEntity || x.Entity is ApplicationUser))
                .Select(x => x)
                .ToList();
            foreach (var entity in entities)
            {
                if (entity.Entity is BaseEntity baseEntity)
                {
                    baseEntity.LastModificationDate = now;
                    // baseEntity.LastModifierUserId = _applicationContext.UserId;

                    if (entity.State == EntityState.Added)
                    {
                        baseEntity.CreateDate = now;
                    }
                    // baseEntity.CreateUserId = entity.IsNew && string.IsNullOrEmpty(entity.Entity.CreateUserId) ? _applicationContext.UserId : entity.Entity.CreateUserId;
                    baseEntity.VersionNo = baseEntity.VersionNo == null ? 1 : baseEntity.VersionNo + 1;
                    baseEntity.TransactionId = transactionId;
                }
                /*else
                {
                    var user = entity.Entity as ApplicationUser;
                    if (user != null)
                    {
                        user.LastModificationDate = now;
                        user.LastModifierUserId = _applicationContext.UserId;
                        user.CreateDate = entity.IsNew && entity.Entity.CreateDate == null ? now : entity.Entity.CreateDate;
                        user.CreateUserId = entity.IsNew && string.IsNullOrEmpty(entity.Entity.CreateUserId) ? _applicationContext.UserId : entity.Entity.CreateUserId;
                        user.VersionNo = user.VersionNo == null ? 1 : user.VersionNo + 1;
                        user.TransactionId = transactionId;
                    }

                }*/
            }
        }

        public static bool IsPersistent(BaseEntity entity)
        {
            return entity.Id != Guid.Empty;
        }

        public int Commit()
        {
            try
            {
                BeforeSave();
                return _applicationDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException sqlException)
                {
                    switch (sqlException.Number)
                    {
                        case 2627: // Primary key violation
                            throw new DataException("The primary key conflict.");
                        case 547: // Foreign Key violation
                            throw new ForeignKeyReferenceAlreadyHasValueException(sqlException.Message);
                        case 2601: // uxindex key violation
                            throw new DuplicateKeyException(sqlException.Message);
                        default:
                            throw new DataException("数据库错误!");   
                    }
                }
                throw new DataException("数据库错误!"); //操作发生不确定的错误,请联系管理员!
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                BeforeSave();
                return await _applicationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //_logger.Error(ex, ex.Message);
                if (ex.InnerException is SqlException sqlException)
                {
                    switch (sqlException.Number)
                    {
                        case 2627: // Primary key violation
                            throw new DataException("The primary key conflict.");
                        case 547: // Foreign Key violation
                            throw new ForeignKeyReferenceAlreadyHasValueException(sqlException.Message);
                        case 2601: // uxindex key violation
                            throw new DuplicateKeyException(sqlException.Message);
                        default:
                            throw new DataException("数据库错误!");
                    }
                }
                throw new DataException("数据库错误!"); //操作发生不确定的错误,请联系管理员!
            }
        }
    }

    public class ForeignKeyReferenceAlreadyHasValueException : Exception
    {
        public ForeignKeyReferenceAlreadyHasValueException(string sqlExceptionMessage)
        {
            throw new Exception(sqlExceptionMessage);
        }
    }

    public class DuplicateKeyException : Exception
    {
        public DuplicateKeyException(string sqlExceptionMessage)
        {
            throw new Exception(sqlExceptionMessage);
        }
    }
}