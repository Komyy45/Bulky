using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Domain.Entities.common;

namespace Bulky.Core.Domain.Entities
{
	public class Category : BaseEntity<int>
	{
		public required string Name { get; set; }

		public ICollection<Product> Products { get; set; } = new HashSet<Product>();
	}
}
