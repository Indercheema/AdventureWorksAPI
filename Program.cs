using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

var app = builder.Build();


app.MapGet("/salesOrderHeader", SalesOrderHeaderMethods.Read);

app.MapPost("/salesOrderHeader/Create", SalesOrderHeaderMethods.CreateSalesOrderHeader);

app.MapGet("/salesOrderHeader/Delete", SalesOrderHeaderMethods.RemoveSalesOrderHeader);

app.MapPut("/salesOrderHeader/update", SalesOrderHeaderMethods.UpdateSalesOrderHeader);



app.Run();

