namespace Catalog.API.Products.GetProductByCategory;

public record GetProductsByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/Category/{category}", async (ISender sender, string category) =>
        {
            var result = await sender.Send(new GetProductsByCategoryQuery(category));

            var products = result.Adapt<GetProductsByCategoryResponse>();

            return Results.Ok(products);

        })
        .WithName("GetProductsByCategory")
        .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get products by category")
        .WithDescription("Get products with the category");
    }
}