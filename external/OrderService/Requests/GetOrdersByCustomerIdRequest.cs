namespace OrderService.Requests;

public class GetOrdersByCustomerIdRequest
{
    public long CustomerId { get; set; }
    public int Limit { get; set; }
    public int Offset { get; set; }
}