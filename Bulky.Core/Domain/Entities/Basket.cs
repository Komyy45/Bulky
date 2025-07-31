using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bulky.Core.Domain.Entities.common;

namespace Bulky.Core.Domain.Entities
{
	public class Basket
	{
		public string Id { get; set; }
		public IEnumerable<BasketItem> Items { get; set; }
	}
}
