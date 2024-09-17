﻿using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Authorization.Infrastructure.Database.Repositories
{
    public class DeviceRepository : IRepository<Device>
    {
        private readonly AuthorizationServiceContext _context;
        private readonly DbSet<Device> _dbSet;

        public DeviceRepository(AuthorizationServiceContext context)
        {
            _context = context;
            _dbSet = _context.Set<Device>();
        }

        public int Create(Device item)
        {
            _dbSet.Add(item);

            return _context.SaveChanges();
        }

        public async Task<int> CreateAsync(Device item)
        {
            _dbSet.Add(item);

            return await _context.SaveChangesAsync();
        }

        public Device FindById(Guid id)
        {
            return _dbSet.Find(id)!;
        }

        public async Task<Device> FindByIdAsync(Guid id)
        {
            return (await _dbSet.FindAsync(id))!;
        }

        public IEnumerable<Device> Get()
        {
            return _dbSet;
        }

        public IEnumerable<Device> Get(Func<Device, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<Device> GetWithInclude(Func<Device, bool> predicate, params Expression<Func<Device, object>>[] includeProperties)
        {
            var query = Include(includeProperties);

            return query.Where(predicate);
        }

        public IEnumerable<Device> GetWithInclude(object includeProperty, params Expression<Func<Device, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public int Remove(Device item)
        {
            _dbSet.Remove(item);

            return _context.SaveChanges();
        }

        public async Task<int> RemoveAsync(Device item)
        {
            _dbSet.Remove(item);

            return await _context.SaveChangesAsync();
        }

        public int Update(Device item)
        {
            _dbSet.Update(item);

            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(Device item)
        {
            _dbSet.Update(item);

            return await _context.SaveChangesAsync();
        }

        private IQueryable<Device> Include(params Expression<Func<Device, object>>[] includeProperties)
        {
            IQueryable<Device> query = _dbSet.AsNoTracking();

            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}