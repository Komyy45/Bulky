using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Domain.Entities.common;

namespace Bulky.Core.Domain.Specification
{
	public abstract class BaseSpecification<TEntity, TKey> : ISpecification<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>> Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
		public int Skip { get; set; }
		public int Take { get; set; }
		public bool IsPaginationEnabled { get; set; }

		public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
		{
			Criteria = criteria;
		}

		public void AddIncludes(params Expression<Func<TEntity, object>>[] includes)
		{
			Includes.AddRange(includes);
		}
	}
}
