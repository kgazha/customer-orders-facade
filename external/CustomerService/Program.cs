using AutoFixture;
using CustomerService.Data;
using CustomerService.Models;

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

var customerId = 1;
var customers = fixture.Build<Customer>()
    .With(i => i.Id, () => customerId++)
    .With(i => i.CreatedAt, DateTime.Now)
    .With(i => i.FullName, GenerateNewCustomerName)
    .With(i => i.Region, () => regions[Random.Shared.Next(0, regions.Count)])
    .CreateMany(10)
    .ToList();

app.MapGet("/customers", () => customers)
    .WithName("GetAllCustomers");
app.MapGet("/customers/{id:long}", (long id) => customers.FirstOrDefault(c => c.Id == id))
    .WithName("GetCustomerById");
app.MapGet("/customersByRegionId/{id:long}", (long id) => customers.Where(x => x.Region.Id == id))
    .WithName("GetAllCustomersByRegionId");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
return;


string GenerateNewCustomerName()
{
    var gender = Random.Shared.Next(0, 2) != 0;

    var name = gender
        ? CustomerNames.MaleNames[Random.Shared.Next(0, CustomerNames.MaleNames.Length)]
        : CustomerNames.FemaleNames[Random.Shared.Next(0, CustomerNames.FemaleNames.Length)];
    
    var surname = CustomerNames.Surnames[Random.Shared.Next(0, CustomerNames.Surnames.Length)];

    return $"{name} {surname}";
}