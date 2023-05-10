using AdventureWorksAPI.Data;
using AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksLT2019Context")));

builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();


var app = builder.Build();

// ============== Customer Endpoints

app.MapGet("/customer", CustomerMethods.Read);

app.MapPost("/customer/create", CustomerMethods.CreateCustomer);

app.MapDelete("/customer/delete", CustomerMethods.RemoveCustomer);

app.MapPut("/customer/update", CustomerMethods.UpdateCustomer);

app.MapGet("/customer/details", CustomerMethods.GetCustomerDetails);

app.MapPost("/customer/addtoaddress", CustomerMethods.AddCustomerToAddress);


//===================SalesOrderHeader EndPoints

app.MapGet("/salesOrderHeader", SalesOrderHeaderMethods.Read);

app.MapPost("/salesOrderHeader/create", SalesOrderHeaderMethods.CreateSalesOrderHeader);

app.MapDelete("/salesOrderHeader/delete", SalesOrderHeaderMethods.RemoveSalesOrderHeader);

app.MapPut("/salesOrderHeader/update", SalesOrderHeaderMethods.UpdateSalesOrderHeader);


// =================== Address EndPoints

app.MapGet("/address", AddressMethods.Read);

app.MapPost("/address/create", AddressMethods.CreateAddress);

app.MapDelete("/address/delete", AddressMethods.RemoveAddress);

app.MapPut("/address/update", AddressMethods.UpdateAddress);

app.MapGet("/address/details", AddressMethods.GetAddressDetails);


//================ Product EndPoints

app.MapGet("/product", ProductMethods.Read);

app.MapPost("/product/create", ProductMethods.CreateProduct);

app.MapDelete("/product/delete", ProductMethods.RemoveProduct);

app.MapPut("/product/update", ProductMethods.UpdateProduct);

app.MapGet("/product/details", ProductMethods.GetProductDetails);


app.Run();

