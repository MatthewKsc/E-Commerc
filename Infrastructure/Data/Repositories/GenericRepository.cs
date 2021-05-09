using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity {
        
        private readonly StoreContext context;

        public GenericRepository(StoreContext context) {
            this.context = context;
        }

        public async Task<T> GetByIdAsync(int id) {
            return await context.Set<T>()
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync() {
            return await context.Set<T>()
                .ToListAsync();
        }
    }
}
