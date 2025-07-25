namespace Bulky.Web.Areas.Admin.Models.Product
{
	public class ProductDetailsViewModel
	{
		public int Id { get; set; }
		public string? PictureUrl { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ISBN { get; set; }
		public string Author { get; set; }
		public decimal Price { get; set; }
		public int CategoryId { get; set; }
	}
}
