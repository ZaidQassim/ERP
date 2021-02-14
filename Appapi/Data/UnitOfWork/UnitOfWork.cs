using System.Threading.Tasks;
using Appapi.Data.Interfaces;
using Appapi.Data.Repository;

namespace Appapi.Data.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _context;
        private IGenericRepository<T> _entity;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }
        public IGenericRepository<T> Entity
        {
            get
            {
                return _entity ?? (_entity = new GenericRepository<T>(_context));
            }
        }
        
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}