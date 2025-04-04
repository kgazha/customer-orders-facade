using AutoFixture;
using OrderService.Models;
using OrderService.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var fixture = new Fixture();
var regions = new List<Region>
{
    new(1, "Chelyabinsk"),
    new(2, "Moscow"),
    new(3, "Saint-Petersburg")
};

var idx = 1;

var orders = Enumerable.Range(1, 10).SelectMany(x =>
{
    var ordersPerCustomer = Random.Shared.Next(50, 110);
    return fixture.Build<Order>()
        .With(i => i.Region, () => regions[Random.Shared.Next(0, regions.Count)])
        .With(i => i.CreatedAt, DateTime.Now)
        .With(i => i.CustomerId, x)
        .With(i => i.Id, () => idx++)
        .With(i => i.TotalCount, ordersPerCustomer)
        .CreateMany(ordersPerCustomer)
        .ToList();
}).ToList();

app.MapPost("/ordersByCustomerId/", (GetOrdersByCustomerIdRequest request) =>
    {
        return orders.Where(i => i.CustomerId == request.CustomerId)
            .OrderBy(i => i.Id)
            .Skip(request.Offset)
            .Take(request.Limit);
    })
    .WithName("GetOrdersByCustomerId");
app.MapPost("/ordersByRegionId/",
        (GetOrdersByRegionIdRequest request) =>
        {
            return orders.Where(i => i.Region.Id == request.RegionId)
                .OrderBy(i => i.Id)
                .Skip(request.Offset)
                .Take(request.Limit);
        })
    .WithName("GetOrdersByRegionId");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();