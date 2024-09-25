using System.Linq.Expressions;

using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Infrastructure.Database.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Database.Repositories
{
    internal class UserRepository : IRepository<User>
    {
        private readonly AuthorizationServiceContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository( AuthorizationServiceContext context )
        {
            _context = context;
            _dbSet = _context.Set<User>();
        }

        public int Create( User item )
        {
            _ = _dbSet.Add( item );

            return _context.SaveChanges();
        }

        public async Task<int> CreateAsync( User item )
        {
            _ = _dbSet.Add( item );

            return await _context.SaveChangesAsync();
        }

        public User FindById( Guid id )
        {
            return _dbSet.Find( id )!;
        }

        public async Task<User> FindByIdAsync( Guid id )
        {
            return (await _dbSet.FindAsync( id ))!;
        }

        public IEnumerable<User> Get()
        {
            return _dbSet;
        }

        public IEnumerable<User> Get( Func<User, bool> predicate )
        {
            return _dbSet.Where( predicate );
        }

        public IEnumerable<User> GetWithInclude( Func<User, bool> predicate, params Expression<Func<User, object>>[] includeProperties )
        {
            IQueryable<User> query = Include( includeProperties );

            return query.Where( predicate );
        }

        public IEnumerable<User> GetWithInclude( object includeProperty, params Expression<Func<User, object>>[] includeProperties )
        {
            return Include( includeProperties );
        }

        public int Remove( User item )
        {
            _ = _dbSet.Remove( item );

            return _context.SaveChanges();
        }

        public async Task<int> RemoveAsync( User item )
        {
            _ = _dbSet.Remove( item );

            return await _context.SaveChangesAsync();
        }

        public int Update( User item )
        {
            _ = _dbSet.Update( item );

            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync( User item )
        {
            _ = _dbSet.Update( item );

            return await _context.SaveChangesAsync();
        }

        private IQueryable<User> Include( params Expression<Func<User, object>>[] includeProperties )
        {
            IQueryable<User> query = _dbSet.AsNoTracking();

            return includeProperties
                .Aggregate( query, ( current, includeProperty ) => current.Include( includeProperty ) );
        }
    }
}
