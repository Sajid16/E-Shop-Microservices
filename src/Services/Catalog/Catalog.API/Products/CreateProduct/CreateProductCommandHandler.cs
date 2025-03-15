namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    // here IDocumentSession is coming from marten library that contacts with Postgre SQL.
    // instead of creating primary constructor sending the params inside class declaration that will work as same since there is no dependency related to passing arguments
    internal class CreateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create product model from the command
            // save into database
            // return the saved product GUID id

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
                Name = command.Name
            };

            // save into database
            documentSession.Store(product);
            await documentSession.SaveChangesAsync(cancellationToken);

            // return the result
            return new CreateProductResult(product.Id);
        }
    }
}
