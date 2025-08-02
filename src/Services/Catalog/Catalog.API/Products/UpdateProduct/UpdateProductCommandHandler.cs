namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool IsUpdated);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description is required");
            RuleFor(p => p.ImageFile).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Image File is required");
            RuleFor(p => p.Price).Cascade(CascadeMode.Stop).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null) throw new ProductNotFoundException();

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
