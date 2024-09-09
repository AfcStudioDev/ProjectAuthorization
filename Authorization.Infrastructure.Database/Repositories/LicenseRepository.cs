using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Authorization.Infrastructure.Database.Repositories
{
    public class LicenseRepository : IRepository<License>
    {
        private readonly AuthorizationServiceContext _context;
        private readonly DbSet<License> _dbSet;

        public LicenseRepository(AuthorizationServiceContext context)
        {
            _context = context;
            _dbSet = _context.Set<License>();
        }

        public int Create(License item)
        {
            _dbSet.Add(item);

            return _context.SaveChanges();
        }

        public async Task<int> CreateAsync(License item)
        {
            _dbSet.Add(item);

            return await _context.SaveChangesAsync();
        }

        public License FindById(Guid id)
        {
            return _dbSet.Find(id)!;
        }

        public async Task<License> FindByIdAsync(Guid id)
        {
            return (await _dbSet.FindAsync(id))!;
        }

        public IEnumerable<License> Get()
        {
            return _dbSet;
        }

        public IEnumerable<License> Get(Func<License, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<License> GetWithInclude(Func<License, bool> predicate, params Expression<Func<License, object>>[] includeProperties)
        {
            var query = Include(includeProperties);

            return query.Where(predicate);
        }

        public IEnumerable<License> GetWithInclude(object includeProperty, params Expression<Func<License, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public int Remove(License item)
        {
            _dbSet.Remove(item);

            return _context.SaveChanges();
        }

        public async Task<int> RemoveAsync(License item)
        {
            _dbSet.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public int Update(License item)
        {
            _dbSet.Update(item);

            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(License item)
        {
            _dbSet.Update(item);

            return await _context.SaveChangesAsync();
        }

        private IQueryable<License> Include(params Expression<Func<License, object>>[] includeProperties)
        {
            IQueryable<License> query = _dbSet.AsNoTracking();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
