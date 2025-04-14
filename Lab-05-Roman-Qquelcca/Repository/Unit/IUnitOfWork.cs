namespace Lab_05_Roman_Qquelcca.Repository.Unit;
using Lab_05_Roman_Qquelcca.Repository;


public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : class;
    Task<int> Complete();
}
