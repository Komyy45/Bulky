namespace Bulky.Core.Models.Product;

public record ProductDto(
    int Id, 
    string Title,
    string Author,
    string ISBN,
    decimal Price,
    string Category);