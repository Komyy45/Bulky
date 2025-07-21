using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Entities.common;

namespace Bulky.Core.Entities
{
	public class Category : BaseEntity<int>
	{
		public required string Name { get; set; }
	}
}
