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
    public class ProductRepository : IProductRepository {
        
        private readonly StoreContext context;

        public ProductRepository(StoreContext context) {
            this.context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id) {

            return await context.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync() {

            return await context.Products.ToListAsync();
        }
    }
}
