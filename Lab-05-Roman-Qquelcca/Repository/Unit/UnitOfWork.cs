namespace Lab_05_Roman_Qquelcca.Repository.Unit;
using System.Collections;
using Lab_05_Roman_Qquelcca.Models;

public class UnitOfWork : IUnitOfWork
{
    private readonly Hashtable _repositories = new();
    private readonly TecsupDB _context;

    public UnitOfWork(TecsupDB context)
    {
        _context = context;
    }

    public Task<int> Complete()
    {
        return _context.SaveChangesAsync();
    }

    public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;

        if (_repositories.ContainsKey(type))
        {
            return (IGenericRepository<TEntity>)_repositories[type]!;
        }

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance is IGenericRepository<TEntity> repo)
        {
            _repositories.Add(type, repo);
            return repo;
        }

        throw new Exception($"No se pudo crear la instancia del repositorio para el tipo {type}");
    }
}
