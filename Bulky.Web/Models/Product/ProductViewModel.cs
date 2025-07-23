using System.ComponentModel.DataAnnotations;

namespace Bulky.Web.Models.Product;

public class ProductViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
}