namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(p => p.Name).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Name is required");
            RuleFor(p => p.Description).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Description is required");
            RuleFor(p => p.ImageFile).Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Image File is required");
            RuleFor(p => p.Price).Cascade(CascadeMode.Stop).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    // here IDocumentSession is coming from marten library that contacts with Postgre SQL.
    // instead of creating primary constructor sending the params inside class declaration that will work as same since there is no dependency related to passing arguments
    //internal class CreateProductCommandHandler(IDocumentSession documentSession, IValidator<CreateProductCommand> validator) : ICommandHandler<CreateProductCommand, CreateProductResult>
    internal class CreateProductCommandHandler(IDocumentSession documentSession) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            #region manual validation using fluentvalidation

            //var validationResult = await validator.ValidateAsync(command, cancellationToken);
            //var error = validationResult.Errors.Select(e => e.ErrorMessage).FirstOrDefault();
            //if (error!.Any()) throw new ValidationException(error);

            #endregion

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