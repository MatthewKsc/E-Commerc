using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams {

        private const int MaxPageSize = 50;
        private int defaultPageSize = 6;
        private string searchFraze;

        public string Sort { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize {
            get => defaultPageSize;
            set => defaultPageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string Search {
            get => searchFraze;
            set => searchFraze = value.ToLower();
        }
    }
}
