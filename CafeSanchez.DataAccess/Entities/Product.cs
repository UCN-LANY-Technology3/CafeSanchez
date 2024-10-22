namespace CafeSanchez.DataAccess.Entities;

public class Product
{
    public int Id { get; set; }
    public Guid WebId { get; set; } = Guid.Empty;
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
}
