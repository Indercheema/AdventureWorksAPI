using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

var app = builder.Build();

// ============== Customer Endpoints

app.MapGet("/customer", CustomerMethods.Read);

app.MapPost("/customer/create", CustomerMethods.CreateCustomer);

app.MapGet("/customer/delete", CustomerMethods.RemoveCustomer);

app.MapPut("/customer/update", CustomerMethods.UpdateCustomer);

app.MapGet("/customer/details", CustomerMethods.GetCustomerDetails);

app.MapPost("/customer/addtoaddress", CustomerMethods.AddCustomerToAddress);


//===================SalesOrderHeader EndPoints

app.MapGet("/salesOrderHeader", SalesOrderHeaderMethods.Read);

app.MapPost("/salesOrderHeader/Create", SalesOrderHeaderMethods.CreateSalesOrderHeader);

app.MapGet("/salesOrderHeader/Delete", SalesOrderHeaderMethods.RemoveSalesOrderHeader);

app.MapPut("/salesOrderHeader/update", SalesOrderHeaderMethods.UpdateSalesOrderHeader);


// =================== Address EndPoints

app.MapGet("/address", AddressMethods.Read);

app.MapPost("/address/create", AddressMethods.CreateAddress);

app.MapGet("/address/Delete", AddressMethods.RemoveAddress);

app.MapPut("/address/update", AddressMethods.UpdateAddress);

app.MapGet("/address/details", AddressMethods.GetAddressDetail);


//================ Product EndPoints

app.MapGet("/product", ProductMethods.Read);

app.MapPost("/product/create", ProductMethods.CreateProduct);

app.MapGet("/product/delete", ProductMethods.RemoveProduct);

app.MapPut("/product/update", ProductMethods.UpdateProduct);

app.MapGet("/product/details", ProductMethods.GetProductDetails);


app.Run();

