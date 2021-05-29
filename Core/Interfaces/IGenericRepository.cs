using Core.Entities;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity{

        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetListAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetListWithSpec(ISpecification<T> specification);
        Task<int> CountAsync(ISpecification<T> specification);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
