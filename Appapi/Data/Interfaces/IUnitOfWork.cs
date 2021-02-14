using System.Threading.Tasks;

namespace Appapi.Data.Interfaces
{
    public interface IUnitOfWork<T> where T : class
    {
        IGenericRepository<T> Entity { get; }
        Task<bool> Save();
    }
}