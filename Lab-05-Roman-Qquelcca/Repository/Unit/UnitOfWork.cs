// using System.Collections;
// using Lab_05_Roman_Qquelcca.Models;
//
// namespace Lab_05_Roman_Qquelcca.Repository.Unit;
//
// public class UnitOfWork : IUnitOfWork
// {
//     private readonly TecsupDB _context;
//     private Hashtable _repositories;
//
//     public UnitOfWork(TecsupDB context)
//     {
//         _context = context;
//         _repositories = new Hashtable();
//     }
//
//     // Método para obtener repositorios genéricos
//     public IGenericRepository<T> Repository<T>() where T : class
//     {
//         var type = typeof(T).Name;
//
//         if (!_repositories.ContainsKey(type))
//         {
//             var repoType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
//             var repoInstance = Activator.CreateInstance(repoType, _context);
//             _repositories[type] = repoInstance!;
//         }
//
//         return (IGenericRepository<T>)_repositories[type]!;
//     }
//
//     // Método para guardar cambios asíncronos
//     public async Task<int> Complete() => await _context.SaveChangesAsync();
//     public TecsupDB GetContext() => _context;
// }


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab_05_Roman_Qquelcca.Models;

namespace Lab_05_Roman_Qquelcca.Repository.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TecsupDB _context;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(TecsupDB context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }

        // Método para obtener repositorios genéricos
        public IGenericRepository<T> Repository<T>() where T : class
        {
            var type = typeof(T).Name;

            // Verifica si el repositorio ya está en el diccionario
            if (!_repositories.ContainsKey(type))
            {
                // Si no está, crea una nueva instancia del repositorio genérico para la entidad
                var repoType = typeof(GenericRepository<>).MakeGenericType(typeof(T));
                var repoInstance = Activator.CreateInstance(repoType, _context);
                _repositories[type] = repoInstance!;
            }

            return (IGenericRepository<T>)_repositories[type]!;
        }

        // Método para guardar cambios asíncronos
        public async Task<int> Complete() => await _context.SaveChangesAsync();

        // Método para obtener el contexto (DbContext)
        public TecsupDB GetContext() => _context;
    }
}
