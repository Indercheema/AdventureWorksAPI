using AdventureWorksAPI.Data;

namespace AdventureWorksAPI.Models
{
    public class ProductMethods
    {

        public static IResult CreateProduct(AdventureWorksLt2019Context context, Product product)
        {
            try
            {
                product.Rowguid = Guid.NewGuid();
                product.ModifiedDate = DateTime.Now;
                context.Add(product);
                context.SaveChanges();

                return Results.Created($"/product?id={product.ProductId}", product);
            }
            catch (Exception ex)
            {
                return Results.BadRequest();
            }
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
                return Results.NotFound();
            }
            else if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
            return Results.Ok($" Product with Id {product.ProductId} is removed successfully.");
        }

        public static IResult UpdateProduct(AdventureWorksLt2019Context context, int Id, Product? product)
        {
            Product? selectedProduct = context.Products.Find(Id);

            try
            {
                if (selectedProduct == null && product != null)
                {

                    CreateProduct(context, product);
                    return Results.Created($"/product?id={product.ProductId}", product);
                }
                else if (selectedProduct != null)
                {
                    selectedProduct.Name = product.Name;
                    selectedProduct.ProductNumber = product.ProductNumber;
                    selectedProduct.Color = product.Color;
                    selectedProduct.StandardCost= product.StandardCost;
                    selectedProduct.ListPrice= product.ListPrice;
                    selectedProduct.SellStartDate= product.SellStartDate;
                    selectedProduct.Rowguid = Guid.NewGuid();
                    selectedProduct.ModifiedDate = DateTime.Now;

                    context.Products.Update(selectedProduct);
                    context.SaveChanges();

                    Read(context, selectedProduct.ProductId);
                    return Results.Ok(selectedProduct);
                }
                return Results.Ok();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        public static IResult GetProductDetails(AdventureWorksLt2019Context context, int id)
        {
            Product? product = context.Products.Find(id);

            if (product == null)
            {
                return Results.NotFound();
            }

            var result = context.Products.Where(p => p.ProductId == id)
            .Select(p => new
            {
                product = p.ProductModel.Products.FirstOrDefault(),
                productModel = p.ProductModel.Name,
                productCategory = p.ProductCategory.Name,
                productDescription = p.ProductModel.ProductModelProductDescriptions.Select(p => p.ProductDescription.Description)
            });

            return Results.Ok(result);
        }

    }
}
