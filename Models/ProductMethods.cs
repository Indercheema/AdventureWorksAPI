namespace AdventureWorksAPI.Models
{
    public class ProductMethods
    {

        public static IResult CreateProduct(AdventureWorksLt2019Context context, Product product)
        {
            context.Add(product);
            context.SaveChanges();

            return Results.Created($"/product?id={product.ProductId}", product);
        }

        public static IResult Read(AdventureWorksLt2019Context context, int? id)
        {
            HashSet<Product> products = new HashSet<Product>();

            if (id == null)
            {
                products = context.Products.ToHashSet();
            }
            else
            {
                Product product = context.Products.FirstOrDefault(p => p.ProductId == id);

                if (product == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(product);
                }
            }

            return Results.Ok(products);
        }

        public static IResult RemoveProduct(AdventureWorksLt2019Context context, int id)
        {
            Product product = context.Products.Where(p => p.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return Results.BadRequest();
            }
            else if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return Results.Ok($" Product with Id {product.ProductId} is removed successfully.");
        }

    }
}
