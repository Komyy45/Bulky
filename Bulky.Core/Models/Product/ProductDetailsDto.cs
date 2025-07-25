namespace Bulky.Core.Models.Product;

public record ProductDetailsDto(
    int Id, 
    string Title,
    string? Picture,
    string Description,
    string Author,
    string ISBN,
    decimal Price,
    int CategoryId);