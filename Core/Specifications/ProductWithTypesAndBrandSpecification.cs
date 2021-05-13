using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductWithTypesAndBrandSpecification : BaseSpecification<Product>
    {
        
        public ProductWithTypesAndBrandSpecification(string sort, int? brandId, int? typeId) 
            : base(x => (!brandId.HasValue || x.ProductBrandId == brandId) && (!typeId.HasValue || x.ProductTypeId == typeId)){

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            if (!string.IsNullOrEmpty(sort)) {
                switch(sort) {
                    case "priceAsc": AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc": AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithTypesAndBrandSpecification(int id) : base(p => p.Id == id) {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}
