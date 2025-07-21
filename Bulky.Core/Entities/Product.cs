using Bulky.Core.Entities.common;

namespace Bulky.Core.Entities;

public class Product : BaseEntity<int>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ISBN { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}