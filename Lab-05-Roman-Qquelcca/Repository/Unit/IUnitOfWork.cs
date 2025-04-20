namespace Lab_05_Roman_Qquelcca.Repository.Unit;
using Lab_05_Roman_Qquelcca.Repository;


public interface IUnitOfWork
{
    IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task<int> Complete();
}
