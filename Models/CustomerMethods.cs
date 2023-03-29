using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace AdventureWorksAPI.Models
{
    public static class CustomerMethods
    {
        public static IResult CreateCustomer(AdventureWorksLt2019Context context, Customer customer)
        {
            context.Add(customer);
            context.SaveChanges();

            return Results.Created($"/customer?id={customer.CustomerId}", customer);
        }

        public static IResult Read(AdventureWorksLt2019Context context, int? id)
        {
            HashSet<Customer> customers = new HashSet<Customer>();

            if(id == null)
            {
                customers = context.Customers.ToHashSet();
            }
            else
            {
                Customer customer = context.Customers.FirstOrDefault(c => c.CustomerId == id);

                if (customer == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(customer);
                }
            }

            return Results.Ok(customers);
        }

        public static IResult RemoveCustomer(AdventureWorksLt2019Context context, int id)
        {
            Customer customer = context.Customers.Find(id);

            if (customer == null)
            {
                return Results.BadRequest();
            }
            else if (customer != null)
            {
                context.Customers.Remove(customer);
                context.SaveChanges();
            }
            return Results.Ok($" Customer with Id {customer.CustomerId} is removed successfully.");
        }


        public static IResult UpdateCustomer(AdventureWorksLt2019Context context, int Id, Customer? customer)
        {
            Customer? selectedCustomer = context.Customers.Find(Id);

            try
            {
                if (selectedCustomer == null && customer != null)
                {
                    
                    CreateCustomer(context, customer);
                    return Results.Ok();
                }
                else if (selectedCustomer != null)
                {
                    selectedCustomer.NameStyle = customer.NameStyle;
                    selectedCustomer.Title = customer.Title;
                    selectedCustomer.FirstName= customer.FirstName;
                    selectedCustomer.MiddleName = customer.MiddleName;
                    selectedCustomer.LastName= customer.LastName;
                    selectedCustomer.CompanyName= customer.CompanyName;
                    selectedCustomer.Phone= customer.Phone;
                    selectedCustomer.EmailAddress= customer.EmailAddress;
                    selectedCustomer.Rowguid= Guid.NewGuid();
                    selectedCustomer.ModifiedDate= DateTime.Now;

                    context.Customers.Update(selectedCustomer);
                    context.SaveChanges();
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    

        public static IResult GetCustomerDetails(AdventureWorksLt2019Context context, int id)
        {

            Customer? selectedCustomer = context.Customers.Find(id);

            if(selectedCustomer == null)
            {
                return Results.NotFound();
            }

            var result = context.Customers.Where(c => c.CustomerId == id)
            .Select(x => new
            {
                Customer = x.CustomerAddresses.Select(ca => ca.Customer),
                Addresses = x.CustomerAddresses.Select(x => new
                {
                    Address = x.Address

                })
            });

            return Results.Ok(result);
        }

    }
}
