using System.Net;

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
            Customer customer = context.Customers.Where(c => c.CustomerId == id).FirstOrDefault();

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

    }
}
