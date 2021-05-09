using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntitiy> where TEntitiy : BaseEntity
    {
        
        public static IQueryable<TEntitiy> GetQuery(IQueryable<TEntitiy> inputQuery,
            ISpecification<TEntitiy> specification) {

            var query = inputQuery;

            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }

    }
}
