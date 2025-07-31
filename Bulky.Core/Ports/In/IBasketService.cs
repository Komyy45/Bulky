using Bulky.Core.Application.DTOs.Basket;
using Bulky.Core.Application.Models.Common;

namespace Bulky.Core.Ports.In
{
	public interface IBasketService
	{
		public Task<Result<BasketDto>> Get(string id);
		public Task<Result<BasketDto>> Add(int itemId,string id);
		public Task<Result<BasketDto>> Remove(int itemId, string id);
		public Task<Result> Delete(string id);
	}
}
