using Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {

        private readonly StoreContext context;

        public StoreContextSeed(StoreContext context) {
            this.context = context;
        }

        public void Seed() {
            try {
                
                if (!context.ProductBrands.Any()) {
                    var brandsData = 
                        File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    context.ProductBrands.AddRange(brands);
                    context.SaveChanges();
                }

                if (!context.ProductTypes.Any()) {
                    var typesData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    context.ProductTypes.AddRange(types);
                    context.SaveChanges();
                }

                if (!context.Products.Any()) {
                    var productsData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    context.Products.AddRange(products);
                    context.SaveChanges();
                }


            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}
