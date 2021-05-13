using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class PaginationResult<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItemsCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public PaginationResult(int pageIndex, int pageSize, int totalItems, IReadOnlyList<T> result) {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalItemsCount = totalItems;
            Items = result;
        }
    }
}
