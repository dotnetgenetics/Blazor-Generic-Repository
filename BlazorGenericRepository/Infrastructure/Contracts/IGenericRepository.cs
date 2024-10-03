using BlazorGenericRepository.Model;
using System.Linq.Expressions;

namespace BlazorGenericRepository.Infrastructure.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        public IQueryable<TEntity> All();

        public IEnumerable<TEntity> FindByExpr(Expression<Func<TEntity, bool>> predicate);

        public IEnumerable<TEntity> FindBy(Func<TEntity, bool> predicate);

        public TEntity Find(int Id);

        public void Insert(TEntity e);

        public void Update(TEntity e);

        public void Delete(TEntity e);

        public void Delete(int Id);

        public bool Exists(TEntity entity);

        public void SaveContext();
    }
}
