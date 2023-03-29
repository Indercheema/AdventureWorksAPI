using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

var app = builder.Build();


app.MapGet("/address/Delete", AddressMethods.RemoveAddress);

app.MapPut("/address/update", AddressMethods.UpdateAddress);

app.MapGet("/address", AddressMethods.Read);

app.MapPost("/address/create", AddressMethods.CreateAddress);

app.MapGet("/product/details", AddressMethods.GetProduct);

app.MapGet("/address/details", AddressMethods.GetAddressDetail);





app.MapGet("/Check", (int Id, AdventureWorksLt2019Context context) =>
{
    return context.Addresses.Where(a => a.AddressId == Id).FirstOrDefault();
});


app.Run();

