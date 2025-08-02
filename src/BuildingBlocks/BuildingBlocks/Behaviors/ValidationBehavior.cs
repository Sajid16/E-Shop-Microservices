using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // fetching the incoming request
            var context = new ValidationContext<TRequest>(request);

            // running all the validations related to request model for the related handle methods.
            // such as for "CreateProductCommand" it will run "CreateProductCommandValidator"
            var validationResults = await Task.WhenAll(validators
                                    .Select(v => v.ValidateAsync(context, cancellationToken)));

            // making list of failures if there is any
            var failures = validationResults
                           .Where(f => f.Errors.Any())
                           .SelectMany(f => f.Errors)
                           .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();

            // since it is acting as middleware so we need to register this into mediatr pipeline in program.cs
        }
    }
}
