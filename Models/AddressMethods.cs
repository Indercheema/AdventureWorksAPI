using Microsoft.EntityFrameworkCore;

namespace AdventureWorksAPI.Models
{
    public static class AddressMethods
    {
        public static IResult RemoveAddress(AdventureWorksLt2019Context context, int Id)
        {
            Address address = context.Addresses.Where(a => a.AddressId == Id).FirstOrDefault();

            if (address == null)
            {
                return Results.BadRequest();

            } 
            else if (address != null) 
            {
                context.Remove(address);
                context.SaveChanges();
            }

            return Results.Ok($" Address with Id {address.AddressId} is removed successfully.");  
        }
        public static IResult UpdateAddress(AdventureWorksLt2019Context context, int Id, Address? address)
        {
            Address selectedAddress = context.Addresses.Where(a => a.AddressId == Id).FirstOrDefault();

            try
            {
                if (selectedAddress == null && address != null)
                {
                    CreateAddress(context, address);

                    return Results.Created($"/address?id={address.AddressId}", address);
                }
                else if (selectedAddress != null)
                {
                    selectedAddress.AddressLine1 = address.AddressLine1;
                    selectedAddress.AddressLine2 = address.AddressLine2;
                    selectedAddress.City = address.City;
                    selectedAddress.StateProvince = address.StateProvince;
                    selectedAddress.CountryRegion = address.CountryRegion;
                    selectedAddress.PostalCode = address.PostalCode;
                    selectedAddress.ModifiedDate = DateTime.Now;

                    context.Addresses.Update(selectedAddress);
                    context.SaveChanges();

                    Read(context, selectedAddress.AddressId);
                    return Results.Ok(selectedAddress);
                }
                return Results.Ok();

            }
            catch (Exception ex) 
            {
                return Results.BadRequest();
            }
        }

        public static IResult CreateAddress(AdventureWorksLt2019Context context, Address address)
        {
            address.Rowguid = Guid.NewGuid();
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
