using CarRentalApp.Data.Entity;
using CarRentalApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRentalApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CarRentalDbContext _context;
        private Dictionary<Type, object> _repositories;
        private IDbContextTransaction _transaction;

        public UnitOfWork(CarRentalDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> GetRepository<T>() where T : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(T)))
            {
                return (IRepository<T>)_repositories[typeof(T)];
            }

            var repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
