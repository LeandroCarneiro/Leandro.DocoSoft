using Leandro.DocoSoft.Common.Exceptions;
using Leandro.DocoSoft.DI;
using Leandro.DocoSoft.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Leandro.DocoSoft.Data
{
    public class BaseContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly ILogger _log;

        public BaseContext() : base()
        {
            _loggerFactory = AppContainer.Resolve<ILoggerFactory>();
            _configuration = AppContainer.Resolve<IConfiguration>();

            _log = _loggerFactory.CreateLogger<BaseContext>();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return Database.BeginTransaction();
        }

        public void Rollback()
        {
            Dispose();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
            }
            catch (Exception ex)
            {
                var msg = "Error trying to commit";
                _log.LogError(msg, ex);

                Rollback();
                throw new AppPersistenceException(msg);
            }
        }

        public DbSet<TEntity> GetEntity<TEntity>() where TEntity : EntityBase<long>
        {
            return base.Set<TEntity>();
        }

        public EntityEntry EntryEntity<TEntity>(TEntity entity) where TEntity : EntityBase<long>
        {
            return base.Entry(entity);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}