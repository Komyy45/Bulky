using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Bulky.Core.Application.Models.Product
{
	public record ProductCreateEditDto(
		int Id,
		string Title,
		string? ExistingPictureUrl,
		IFormFile? Picture,
		string Description,
		string Author,
		string ISBN,
		decimal Price,
		int CategoryId
	);
}
