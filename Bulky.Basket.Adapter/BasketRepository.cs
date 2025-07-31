using System.Text.Json;
using Bulky.Core.Application.DTOs.Basket;
using Bulky.Core.Ports.Out;
using StackExchange.Redis;

namespace Bulky.Basket.Adapter
{
	public class BasketRepository(IConnectionMultiplexer muxer) : IBasketRepository
	{
		private readonly IDatabase database = muxer.GetDatabase();


		public async Task<BasketDto?> Get(string id)
		{
			var basket = await database.StringGetAsync(id);

			return basket.HasValue ? JsonSerializer.Deserialize<BasketDto>(basket!) : null; 
		}

		public async Task<BasketDto?> Update(BasketDto basket, TimeSpan timeToLive)
		{
			var isUpdated = await database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), timeToLive);

			return isUpdated ? basket : null;
		}

		public async Task<bool> Delete(string id) => await database.KeyDeleteAsync(id);
	}
}
