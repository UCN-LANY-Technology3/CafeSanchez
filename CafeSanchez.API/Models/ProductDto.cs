namespace CafeSanchez.API.Models;

public class ProductDto
{
    public int Id { get; set; }
    public Guid WebId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
