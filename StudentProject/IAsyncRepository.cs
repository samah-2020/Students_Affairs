using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject
{
   public interface IAsyncRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAllAsQueryable();
        Task<T> AddAsync(T entity, bool saveChanges = false);
        Task DeleteAsync(T entity, bool saveChanges = false);
        Task<T> GetByIdAsync(int  id);
        
        Task<int> SaveChangesAsync();
        Task<int> GeCountOfList();
    }
}
