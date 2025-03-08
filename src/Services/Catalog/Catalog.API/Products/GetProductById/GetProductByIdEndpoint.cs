using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductById
{
    // if i want to make the endpoint post then i need the request model
    //public record GetProductByIdRequest(Guid ProductId);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //app.MapPost("/product",
            //async (GetProductByIdRequest request, ISender sender) =>
            //{
            //    var query = request.Adapt<GetProductByIdQuery>();

            //    var result = await sender.Send(query);

            //    var response = result.Adapt<GetProductByIdResponse>();

            //    return Results.Ok(response);

            //})
            //.WithName("GetProductById")
            //.Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            //.ProducesProblem(StatusCodes.Status400BadRequest)
            //.WithSummary("Get Product By Id")
            //.WithDescription("Get Product By Id");
            
            app.MapGet("/product/{productId}",
            async (Guid productId, ISender sender) =>
            {
                var query = new GetProductByIdQuery(productId);

                var result = await sender.Send(query);

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);

            })
            .WithName("GetProductById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product By Id")
            .WithDescription("Get Product By Id");
        }
    }
}
