namespace CafeSanchez.DataAccess.Entities;

public class Product
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
}
