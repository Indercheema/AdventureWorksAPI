using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Models
{
    public static class AddressMethods
    {
        public static IResult RemoveAddress(AdventureWorksLt2019Context context, int Id)
        {
            Address address = context.Addresses.Where(a => a.AddressId == Id).FirstOrDefault();
            if (address == null )
            {
                return Results.BadRequest();
            } else if (address != null) 
            {
                context.Remove(address);
                context.SaveChanges();
            }

            return Results.Ok($" Address with Id {address.AddressId} is removed successfully.");  
        }
        public static IResult UpdateAddress(AdventureWorksLt2019Context context, int Id, Address? address)
        {
            Address checkAddress = context.Addresses.Where(a => a.AddressId == Id).FirstOrDefault();

            try
            {
                if (checkAddress == null)
                {
                    context.Addresses.Add(address);
                    context.SaveChanges();
                }
                else if (checkAddress != null)
                {
                    checkAddress.AddressLine1 = address.AddressLine1;
                    checkAddress.AddressLine2 = address.AddressLine2;
                    checkAddress.City = address.City;
                    checkAddress.StateProvince = address.StateProvince;
                    checkAddress.CountryRegion = address.CountryRegion;
                    checkAddress.PostalCode = address.PostalCode;
                    checkAddress.ModifiedDate = DateTime.Now;
                    context.Addresses.Update(checkAddress);
                    context.SaveChanges();
                }
                return Results.Created($"/address?id={address.AddressId}", address);

            }
            catch (Exception ex) 
            {
                return Results.BadRequest();
            }
        }

        public static IResult CreateAddress(AdventureWorksLt2019Context context, Address address)
        {
            context.Add(address);
            context.SaveChanges();

            return Results.Created($"/address?id={address.AddressId}", address);
        }



        public static IResult Read(AdventureWorksLt2019Context context, int? id)
        {
            HashSet<Address> addresses = new HashSet<Address>();

            if (id == null)
            {
                addresses = context.Addresses.ToHashSet();
            }
            else
            {
                Address address = context.Addresses.FirstOrDefault(a => a.AddressId == id);



                if (address == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(address);
                }
            }



            return Results.Ok(addresses);
        }

        public static IResult GetProduct(AdventureWorksLt2019Context context, int productId)
        {
            //var result = context.Products.Where(p => p.ProductId == productId).Select(p => new { p.ProductModel, p.ProductCategory});

            var result = context.Products.Where(p => p.ProductId == productId)
                .Select(p => new
                {
                product = p.ProductModel.Products.FirstOrDefault(),
                productModel = p.ProductModel.Name,
                productCategory= p.ProductCategory.Name,
                productDescription = p.ProductModel.ProductModelProductDescriptions.Select(p =>  p.ProductDescription.Description)
            });
         

            //var result2 = context.ProductModelProductDescriptions.Include(p => p.ProductModel).ThenInclude(p => p.Products).Select(p => new { p.ProductModel, p.ProductDescription }).Wher;
            return Results.Ok(result);
        }

        public static IResult GetAddressDetail(AdventureWorksLt2019Context context, int addressId)
        {
            var result = context.Addresses.Where(a => a.AddressId == addressId)
                .Select(a => new
                {
                    address = a.CustomerAddresses.Select(a => a.Address),
                    customers = a.CustomerAddresses.Select(ca => new
                    {
                        customer = ca.Customer,
                    })
                }) ;

            return Results.Ok(result);
        }
    }
}
