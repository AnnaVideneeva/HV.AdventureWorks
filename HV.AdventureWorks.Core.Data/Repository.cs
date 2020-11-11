using System.Linq;
using Microsoft.EntityFrameworkCore;
using HV.AdventureWorks.Core.Data.Interfaces;
using System.Collections.Generic;

namespace HV.AdventureWorks.Core.Data
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbset;
        
        public Repository(DbContext context)
        {
            _context = context;
            _dbset = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get()
        {
            return _dbset;
        }

        public IQueryable<TEntity> GetAsNoTracking()
        {
            return _dbset.AsNoTracking();
        }

        public void Add(TEntity entity)
        {
            _dbset.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void DeleteRange(IList<TEntity> entities)
        {
            var detached = entities.Where(entity => _context.Entry(entity).State == EntityState.Detached);
            foreach (var item in detached)
            {
                _dbset.Attach(item);
            }

            _dbset.RemoveRange(entities);
        }

        public void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbset.Attach(entity);
            }

            _dbset.Remove(entity);
        } 
    }
}
