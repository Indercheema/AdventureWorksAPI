using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

var app = builder.Build();





app.MapGet("/product", ProductMethods.Read);

app.MapPost("/product/create", ProductMethods.CreateProduct);

app.MapGet("/product/delete", ProductMethods.RemoveProduct);

app.MapPut("/product/update", ProductMethods.UpdateProduct);

app.MapGet("/product/details", ProductMethods.GetProductDetails);

app.Run();

