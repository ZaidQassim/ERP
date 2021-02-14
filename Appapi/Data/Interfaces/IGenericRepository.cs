using System.Collections.Generic;
using System.Threading.Tasks;
using Appapi.Models;

namespace Appapi.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(int? skip, int? take);
        Task<T> GetById(object id);
        Task<int> Count();
        Task Insert(T entity);
        void Update(T entity);
        void Delete(object id);

    }
}