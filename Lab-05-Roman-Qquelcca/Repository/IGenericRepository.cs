using System.Linq.Expressions;

namespace Lab_05_Roman_Qquelcca.Repository;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(object id);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    Task InsertAndSaveAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
