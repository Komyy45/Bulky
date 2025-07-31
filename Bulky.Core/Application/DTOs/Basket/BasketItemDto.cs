using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Core.Application.DTOs.Basket
{
	public record BasketItemDto(int Id, string? PictureUrl, string Name, string Category,int Quantity, decimal Price);
}
