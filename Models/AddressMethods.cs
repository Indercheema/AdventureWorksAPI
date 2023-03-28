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

            if (checkAddress == null )
            {
                context.Addresses.Add(address);
                context.SaveChanges();
                return Results.Ok($"Address with Id {address.AddressId} is created");
            } else if(checkAddress != null)
            {
                context.Addresses.Update(checkAddress);
                context.SaveChanges();
            }
            return Results.Ok($"Address with Id{checkAddress.AddressLine1} is Updated");
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
    }
}
