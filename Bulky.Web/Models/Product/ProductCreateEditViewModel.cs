using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Models.Product;

public class ProductCreateEditViewModel
{
    public int Id { get; set; }
	public string? ExistingPicture { get; set; }
	public IFormFile? Picture { get; set; }

	[Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(5000)]
    public string Description { get; set; }

    [Required]
    public string ISBN { get; set; }
    [Required]
    [MaxLength(100)]
    public string Author { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}