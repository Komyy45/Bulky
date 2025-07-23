using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Models.Product;

public class ProductCreateEditViewModel
{
    public int Id { get; set; }
    [Required]
    [Length(5, 100)]
    public string Title { get; set; }
    [Required]
    [Length(5, 300)]
    public string Description { get; set; }
    [Required]
    public string ISBN { get; set; }
    [Required]
    [Length(3, 100)]
    public string Author { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int CategoryId { get; set; }
}