namespace CustomerService.Models;

public class Customer
{
    public long Id { get; set; }
    public required string FullName { get; set; }
    public required Region Region { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}