namespace AdventureWorksAPI.Models
{
    public class SalesOrderHeaderMethods
    {
        public static IResult RemoveSalesOrderHeader(AdventureWorksLt2019Context context, int Id)
        {
            SalesOrderHeader salesOrderHeader = context.SalesOrderHeaders.Where(s => s.SalesOrderId == Id).FirstOrDefault();
            if (salesOrderHeader == null)
            {
                return Results.BadRequest();
            }
            else if (salesOrderHeader != null)
            {
                context.Remove(salesOrderHeader);
                context.SaveChanges();
            }

            return Results.Ok($" Sales Order Header with Id {salesOrderHeader.SalesOrderId} is removed successfully.");
        }

        public static IResult CreateSalesOrderHeader(AdventureWorksLt2019Context context, SalesOrderHeader salesOrderHeader)
        {
            context.Add(salesOrderHeader);
            context.SaveChanges();

            return Results.Created($"/salesOrderHeader?id={salesOrderHeader.SalesOrderId}", salesOrderHeader);
        }

        public static IResult Read(AdventureWorksLt2019Context context, int? id)
        {
            HashSet<SalesOrderHeader> salesOrderHeaders = new HashSet<SalesOrderHeader>();

            if (id == null)
            {
                salesOrderHeaders = context.SalesOrderHeaders.ToHashSet();
            }
            else
            {
                SalesOrderHeader salesOrderHeader = context.SalesOrderHeaders.FirstOrDefault(s => s.SalesOrderId == id);



                if (salesOrderHeader == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(salesOrderHeader);
                }
            }



            return Results.Ok(salesOrderHeaders);
        }

    }
}
