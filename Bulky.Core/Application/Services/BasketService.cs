using Bulky.Core.Application.DTOs.Basket;
using Bulky.Core.Application.Models.Common;
using Bulky.Core.Domain.Entities;
using Bulky.Core.Domain.Specification.Products;
using Bulky.Core.Ports.In;
using Bulky.Core.Ports.Out;

namespace Bulky.Core.Application.Services
{
	internal class BasketService(IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IBasketService
	{
		private readonly IGenericRepository<Product, int> _productRepository =  unitOfWork.GetRepository<Product, int>();

		public async Task<Result<BasketDto>> Add(int itemId, string id)
		{
			var basket = await basketRepository.Get(id);

			var item = basket.Items.SingleOrDefault(x => x.Id == itemId);
			
			if (item is not null)
			{
				var basketItems = basket!.Items.Where(item => item.Id == itemId).Select(item => item with { Quantity = item.Quantity + 1 });

				basket = basket with { TotalPrice = basket.TotalPrice + item.Price, Items = basketItems };
			}
			else
			{
				var spec = new GetProductWithCategorySpecification();
				var product = await _productRepository.Get(itemId, spec, CancellationToken.None);;

				var basketItemDto = new BasketItemDto(itemId, product.Picture, product.Title, product.Category.Name, 1, product.Price);


				basket = basket with
				{
					TotalPrice = basket.TotalPrice + product.Price,
					Items = new List<BasketItemDto>(basket.Items.Concat(new[] { basketItemDto }))
				};
			}

			await basketRepository.Update(basket, TimeSpan.FromDays(10));

			return Result<BasketDto>.Success(basket);
		}

		public async Task<Result> Delete(string id)
		{
			var isDeleted = await basketRepository.Delete(id);

			return isDeleted ? Result.Success() : Result.Failure();
		}

		public async Task<Result<BasketDto>> Get(string id)
		{
			var basket = await basketRepository.Get(id);

			if (basket is null) 
				basket = await basketRepository.Update(new BasketDto(id, default, new List<BasketItemDto>()), TimeSpan.FromDays(10));
			

			return Result<BasketDto>.Success(basket!);
		}

		public async Task<Result<BasketDto>> Remove(int itemId, string id)
		{
			var basket = await basketRepository.Get(id);

			var basketItem = basket!.Items.SingleOrDefault(item => item.Id == itemId);

			if(basketItem is not null)
			{
				IEnumerable<BasketItemDto> items = null!;
				if (basketItem.Quantity == 1)
					items = basket.Items.Except([basketItem]);
				else
					items = basket!.Items.Where(item => item.Id == itemId && item.Quantity - 1 > 0).Select(item => item with { Quantity = item.Quantity - 1 });

				basket = basket with { TotalPrice = items.Sum(item => item.Price * item.Quantity), Items = items };
			}

			await basketRepository.Update(basket, TimeSpan.FromDays(10));

			return Result<BasketDto>.Success(basket);
		}

		public async Task<Result<BasketDto>> Update(BasketDto basket)
		{
			var updatedBasket = await basketRepository.Update(basket, TimeSpan.FromDays(10));

			return Result<BasketDto>.Success(updatedBasket!);
		}
	}
}
