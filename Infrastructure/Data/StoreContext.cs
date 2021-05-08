using Core.Entities;
using Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) {

        }

        public DbSet<Product> Products {get; set;}
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.ApplyConfiguration(new ProductConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
