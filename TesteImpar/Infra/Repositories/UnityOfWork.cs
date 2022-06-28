

using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using TesteImpar.Context.Infra;
using TesteImpar.Infra.Repositories;

namespace Infra.Repositories
{
    public interface IUnitOfWork
    {
        public TesteImparContext Context { get; }
        public Task Save();
        IRepository<TEntity> Repository<TEntity>();
        public Task CloseConnection();
        public void Commit();
        public void BeginTransaction();
        public void Rollback();
        public void Dispose();

    }
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposed;
        private readonly TesteImparContext _dbContext;
        public TesteImparContext Context => _dbContext;
        private Dictionary<string, dynamic> _repositories;
        private IDbContextTransaction _transaction;

        public UnitOfWork(TesteImparContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }



        public IRepository<TEntity> Repository<TEntity>()
        {
            if (_repositories == null)
                _repositories = new Dictionary<string, dynamic>(); var type = typeof(TEntity).Name; if (_repositories.ContainsKey(type))
                return (IRepository<TEntity>)_repositories[type]; var repositoryType = typeof(Repository<>);
            _repositories.Add(type, Activator.CreateInstance(
                repositoryType.MakeGenericType(typeof(TEntity)), this)
            );
            return _repositories[type];
        }

        public async void BeginTransaction()
        {
            this._transaction = await this.Context.Database.BeginTransactionAsync();
        }


        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }
            this._disposed = true;
        }
        public async Task CloseConnection()
        {
            await _dbContext.DisposeAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            this._transaction.Commit();
        }
        public void Rollback()
        {
            _transaction.Rollback();
        }



    }
}
