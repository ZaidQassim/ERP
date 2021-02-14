using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Appapi.Data.Interfaces;
using Appapi.Dtos;
using Appapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appapi.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;

        public GenericRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll(int? skip, int? take)
        {
            return await table.AsNoTracking().Skip(take.Value * skip.Value).Take(take.Value).ToListAsync();
        }


        public async Task<T> GetById(object id)
        {
            return await table.FindAsync(id);
        }

        public async void Delete(object id)
        {
            T existing = await GetById(id);
            table.Remove(existing);
        }

        public async Task Insert(T entity) => await table.AddAsync(entity);

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> Count()
        {
            return await table.AsNoTracking().CountAsync();
        }


        // public async Task<IEnumerable<SubDepartment>> GetSubDepartment(int id)
        // {
        //     return await _context.SubDepartments.Where(u => u.DepartmentId == id).ToListAsync();
        // }

    }
}