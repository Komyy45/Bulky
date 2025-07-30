using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Domain.Specification;

namespace Bulky.Core.Domain.Specification.Products
{
	public class GetAllProductsSpecification : BaseSpecification<Product, int>
	{
		public GetAllProductsSpecification(Expression<Func<Product, bool>> criteria, int skip, int take) : base(criteria)
		{
			Skip = skip;
			Take = take;
			IsPaginationEnabled = true;

			AddIncludes(p => p.Category);
		}
	}
}
