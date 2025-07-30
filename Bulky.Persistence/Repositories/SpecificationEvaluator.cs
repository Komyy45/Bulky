using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Domain.Entities.common;
using Bulky.Core.Domain.Specification;
using Microsoft.EntityFrameworkCore;

namespace Bulky.Persistence.Repositories
{
	public static class SpecificationEvaluator
	{
		public static IQueryable<TEntity> Evaluate<TEntity, TKey>(this IQueryable<TEntity> query, ISpecification<TEntity,TKey> specification)
			where TEntity : BaseEntity<TKey>
			where TKey : IEquatable<TKey>
		{
			if(specification.Criteria is not null)
				query = query.Where(specification.Criteria);

			if(specification.IsPaginationEnabled)
				query = query.Skip(specification.Skip).Take(specification.Take);

			if (specification.Includes.Count > 0)
				query = specification.Includes.Aggregate(query, (prev, cur) => prev.Include(cur));

			return query;
		}
	}
}
