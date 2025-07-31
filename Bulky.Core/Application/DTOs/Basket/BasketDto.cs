using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Application.DTOs.Basket
{
	public record BasketDto(string Id, decimal TotalPrice, IEnumerable<BasketItemDto> Items);
}
