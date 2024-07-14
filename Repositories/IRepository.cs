using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace socialMedia.Repositories
{
    public interface IRepository<T>
    {
        Task AddAsync(T entity);
        Task SaveAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate); 
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> FirstOrDefaultAsync(Func<T, bool> predicate);
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task Delete(int key1, int key2);
    }


    public interface ICompositeKeyRepository<T> : IRepository<T>
    {
        Task Delete(int key1, int key2);
    }
}
