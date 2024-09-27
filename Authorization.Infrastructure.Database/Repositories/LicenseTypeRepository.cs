using System.Linq.Expressions;

using Authorization.Application.Abstractions;
using Authorization.Application.Domain.Entities;
using Authorization.Infrastructure.Database.Contexts;

using Microsoft.EntityFrameworkCore;

namespace Authorization.Infrastructure.Database.Repositories
{
    public class LicenseTypeRepository : IRepository<LicenseType>
    {
        private readonly AuthorizationServiceContext _context;
        private readonly DbSet<LicenseType> _dbSet;

        public LicenseTypeRepository( AuthorizationServiceContext context )
        {
            _context = context;
            _dbSet = _context.Set<LicenseType>();
        }

        public int Create( LicenseType item )
        {
            _ = _dbSet.Add( item );

            return _context.SaveChanges();
        }

        public async Task<int> CreateAsync( LicenseType item )
        {
            _ = _dbSet.Add( item );

            return await _context.SaveChangesAsync();
        }

        public LicenseType FindById( uint id )
        {
            return _dbSet.Find( id )!;
        }

        public async Task<LicenseType> FindByIdAsync( uint id )
        {
            return (await _dbSet.FindAsync( id ))!;
        }

        public IEnumerable<LicenseType> Get()
        {
            return _dbSet;
        }

        public IEnumerable<LicenseType> Get( Func<LicenseType, bool> predicate )
        {
            return _dbSet.Where( predicate );
        }

        public IEnumerable<LicenseType> GetWithInclude( Func<LicenseType, bool> predicate, params Expression<Func<LicenseType, object>>[] includeProperties )
        {
            IQueryable<LicenseType> query = Include( includeProperties );

            return query.Where( predicate );
        }

        public IEnumerable<LicenseType> GetWithInclude( object includeProperty, params Expression<Func<LicenseType, object>>[] includeProperties )
        {
            return Include( includeProperties );
        }

        public int Remove( LicenseType item )
        {
            _ = _dbSet.Remove( item );

            return _context.SaveChanges();
        }

        public async Task<int> RemoveAsync( LicenseType item )
        {
            _ = _dbSet.Remove( item );

            return await _context.SaveChangesAsync();
        }

        public int Update( LicenseType item )
        {
            _ = _dbSet.Update( item );

            return _context.SaveChanges();
        }

        public async Task<int> UpdateAsync( LicenseType item )
        {
            _ = _dbSet.Update( item );

            return await _context.SaveChangesAsync();
        }

        private IQueryable<LicenseType> Include( params Expression<Func<LicenseType, object>>[] includeProperties )
        {
            IQueryable<LicenseType> query = _dbSet.AsNoTracking();

            return includeProperties
                .Aggregate( query, ( current, includeProperty ) => current.Include( includeProperty ) );
        }
    }
}
