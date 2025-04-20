using Lab_05_Roman_Qquelcca.Models;

namespace Lab_05_Roman_Qquelcca.Repository;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;



public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly TecsupDB _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(TecsupDB context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(object id) => await _dbSet.FindAsync(id);

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _dbSet.Where(predicate).ToListAsync();


    public async Task InsertAsync(T entity) => await _dbSet.AddAsync(entity);

    public async Task InsertAndSaveAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);

    public async Task<int> CountAsync() => await _dbSet.CountAsync();

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}