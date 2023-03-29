using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

var app = builder.Build();

app.MapPost("/customer/addtoaddress", (int customerId, int addressId, AdventureWorksLt2019Context context) =>
{
    Address newAdress =  context.Addresses.Where(a => a.AddressId == addressId).FirstOrDefault();
    Customer newCustomer = context.Customers.Where(c => c.CustomerId == customerId).FirstOrDefault();

    CustomerAddress cs = new CustomerAddress();
    cs.CustomerId = newCustomer.CustomerId;
    cs.AddressId = newAdress.AddressId;
    cs.AddressType = "Main Office";
    cs.Rowguid = Guid.NewGuid();
    cs.ModifiedDate= DateTime.Now;
    if(!context.CustomerAddresses.Any())
    {
        context.CustomerAddresses.Add(cs);
    } else
    {
        return Results.BadRequest("Customer alread added to address");
    }
    context.SaveChanges();
    return Results.Ok($"{newCustomer.FirstName} is added to {newAdress.AddressLine1}");
});

app.Run();

