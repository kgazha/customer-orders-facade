namespace OrderService.Requests;

public class GetOrdersByRegionIdRequest
{
    public long RegionId { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}