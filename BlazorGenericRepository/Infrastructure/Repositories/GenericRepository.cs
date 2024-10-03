using BlazorGenericRepository.Data;
using BlazorGenericRepository.Infrastructure.Contracts;
using BlazorGenericRepository.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlazorGenericRepository.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>, IDisposable where TEntity : class, IEntity
    {
        internal EmployeeDBContext _context;
        internal DbSet<TEntity> _dbSet;
        private bool _tracking = false;

        public GenericRepository(IDbContextFactory<EmployeeDBContext> DbFactory)
        {
            _context = DbFactory.CreateDbContext();
            _dbSet = _context.Set<TEntity>();
        }

        public GenericRepository()
        {
            _context = new EmployeeDBContext();
            _dbSet = _context.Set<TEntity>();
        }

        public GenericRepository<TEntity> WithTracking()
        {
            _tracking = true;
            return this;
        }

        public GenericRepository<TEntity> WithoutTracking()
        {
            _tracking = false;
            return this;
        }

        public IQueryable<TEntity> All()
        {
            if (_tracking)
                return _dbSet;
            else
                return _dbSet.AsNoTracking();
        }

        public void Delete(TEntity e)
        {
            if (_tracking)
                _dbSet.Remove(e);
            else
                _context.Entry(e).State = EntityState.Deleted;
        }

        public void Delete(int Id)
        {
            Delete(Find(Id));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public bool Exists(TEntity entity)
        {
            return _dbSet.ToList().Any(e => e.Id == entity.Id);
        }

        public TEntity Find(int Id)
        {
            if (_tracking)
                return _dbSet.Find(Id);
            else
            {
                _context.ChangeTracker.Clear();
                return _dbSet.AsNoTracking().ToList().FirstOrDefault(x => x.Id == Id);
            }                
        }

        public IEnumerable<TEntity> FindBy(Func<TEntity, bool> predicate)
        {
            if (_tracking)
                return _dbSet.Where(predicate);
            else
                return _dbSet.AsNoTracking().ToList().Where(predicate);
        }

        public IEnumerable<TEntity> FindByExpr(Expression<Func<TEntity, bool>> predicate)
        {
            if (_tracking)
                return _dbSet.Where(predicate);
            else
                return _dbSet.AsNoTracking().Where(predicate);
        }

        public void Insert(TEntity e)
        {
            _dbSet.Add(e);
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void Update(TEntity e)
        {
            if (!_tracking)
            {
                _context.Entry(e).State = EntityState.Modified;
                _context.Set<TEntity>().Update(e);
            }
        }
    }
}
