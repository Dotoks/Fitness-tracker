using System.Linq.Expressions;

namespace Fitness_Tracker.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        //T - Category
        IQueryable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
