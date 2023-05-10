using AdventureWorksAPI.Data;
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
                return Results.NotFound();

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
            try
            {
                address.Rowguid = Guid.NewGuid();
                address.ModifiedDate = DateTime.Now;
                context.Add(address);
                context.SaveChanges();

                return Results.Created($"/address?id={address.AddressId}", address);
            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
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


        public static IResult GetAddressDetails(AdventureWorksLt2019Context context, int addressId)
        {
            Address? address = context.Addresses.Find(addressId);

            if (address == null)
            {
                return Results.NotFound();
            }

            var result = context.Addresses.Where(a => a.AddressId == addressId)
                .Select(a => new
                {
                    AddressId = a.AddressId,
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    City = a.City,
                    StateProvince = a.StateProvince,
                    CountryRegion = a.CountryRegion,
                    PostalCode = a.PostalCode,
                    customers = a.CustomerAddresses.Select(ca => new
                    {
                        customer = ca.Customer,
                    })
                }) ;

            return Results.Ok(result);
        }
    }
}
