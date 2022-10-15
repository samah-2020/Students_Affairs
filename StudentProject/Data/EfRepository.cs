using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly StudentsAffairsContext _dbContext;
      
        public EfRepository(StudentsAffairsContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        public async Task<T> AddAsync(T entity, bool saveChanges = false)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            if (saveChanges)
                await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task DeleteAsync(T entity, bool saveChanges = false)
        {
            _dbContext.Remove(entity);
        
            if (saveChanges)
                await _dbContext.SaveChangesAsync();
        }

        public async Task<int> GeCountOfList()
        {
            IQueryable<T> query = _dbContext.Set<T>();

            int count = query.Count();

            return count;
        }

        public  IQueryable<T> GetAllAsQueryable()
        {
            IQueryable<T> query = _dbContext.Set<T>();
           query = query.OrderByDescending(a => a.Id).AsQueryable<T>();
            return query;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbContext.Set<T>().FindAsync(id);
            if (item != null)
                return item;
            return null;
        }
 

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
