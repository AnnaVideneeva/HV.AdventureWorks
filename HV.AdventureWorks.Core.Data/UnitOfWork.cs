using Microsoft.EntityFrameworkCore;
using HV.AdventureWorks.Core.Data.Interfaces;

namespace HV.AdventureWorks.Core.Data
{
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : DbContext
    {
        private readonly DbContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>()
            where TEntity : class
        {
            return new Repository<TEntity>(_context);
        }

        public void SaveChanges()
        {
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }
    }
}
