using Bulky.Core.Domain.Entities;

namespace Bulky.Core.Domain.Specification.Products
{
	internal class GetProductWithCategorySpecification : BaseSpecification<Product, int>
	{
		public GetProductWithCategorySpecification()
		{
			AddIncludes(item => item.Category);
		}
	}
}
