using AdventureWorksAPI.Data;
using Microsoft.Identity.Client.Extensibility;

namespace AdventureWorksAPI.Models
{
    public class SalesOrderHeaderMethods
    {
        public static IResult RemoveSalesOrderHeader(AdventureWorksLt2019Context context, int Id)
        {
            SalesOrderHeader salesOrderHeader = context.SalesOrderHeaders.Where(s => s.SalesOrderId == Id).FirstOrDefault();

            if (salesOrderHeader == null)
            {
                return Results.NotFound();
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
            try
            {
                salesOrderHeader.Rowguid = Guid.NewGuid();
                salesOrderHeader.ModifiedDate = DateTime.Now;
                context.Add(salesOrderHeader);
                context.SaveChanges();

                return Results.Created($"/salesOrderHeader?id={salesOrderHeader.SalesOrderId}", salesOrderHeader);
            } catch(Exception ex)
            {
                return Results.BadRequest();
            }
           
        }

        public static IResult UpdateSalesOrderHeader(AdventureWorksLt2019Context context, int Id, SalesOrderHeader? salesOrderHeader)
        {
            SalesOrderHeader CurrentSale = context.SalesOrderHeaders.Where(s => s.SalesOrderId == Id).FirstOrDefault();

            try
            {
                if (CurrentSale == null &&  salesOrderHeader != null)
                {
                    CreateSalesOrderHeader(context, salesOrderHeader);

                    return Results.Created($"/salesOrderHeader?id={salesOrderHeader.SalesOrderId}", salesOrderHeader);
                }
                else if (CurrentSale != null)
                {
                    CurrentSale.RevisionNumber = salesOrderHeader.RevisionNumber;
                    CurrentSale.DueDate = salesOrderHeader.DueDate;
                    CurrentSale.ShipDate = salesOrderHeader.ShipDate;
                    CurrentSale.Status = salesOrderHeader.Status;
                    CurrentSale.AccountNumber = salesOrderHeader.AccountNumber;
                    CurrentSale.ShipMethod = salesOrderHeader.ShipMethod;
                    CurrentSale.SubTotal = salesOrderHeader.SubTotal;
                    CurrentSale.TaxAmt = salesOrderHeader.TaxAmt;
                    CurrentSale.Freight = salesOrderHeader.Freight;
                    CurrentSale.TotalDue = salesOrderHeader.TotalDue;
                    CurrentSale.Comment = salesOrderHeader.Comment;
                    CurrentSale.Rowguid = Guid.NewGuid();
                    CurrentSale.ModifiedDate = DateTime.Now;


                    context.SalesOrderHeaders.Update(CurrentSale);
                    context.SaveChanges();

                    Read(context, CurrentSale.SalesOrderId);
                    return Results.Ok(CurrentSale);
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
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
