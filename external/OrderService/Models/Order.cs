using OrderService.Models.Enums;

namespace OrderService.Models;

public record Order
{
    public required long Id { get; init; }

    public required long CustomerId { get; init; }

    public required Region Region { get; set; }

    public required OrderStatusEnum OrderStatus { get; init; }

    public required string Comment { get; init; }

    public DateTimeOffset CreatedAt { get; set; }

    public required long TotalCount { get; init; }
}