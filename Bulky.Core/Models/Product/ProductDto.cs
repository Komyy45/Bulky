namespace Bulky.Core.Models.Product;

public record ProductDto(
    int Id, 
    string Title,
    string Description,
    string Author,
    string ISBN,
    decimal Price,
    string Category);