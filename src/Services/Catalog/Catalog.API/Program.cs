using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Databse")!);
}).UseLightweightSessions();

builder.Services.AddCarter();

#region for custom exception handler
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

var app = builder.Build();

#region for custom exception handler
app.UseExceptionHandler(options => { });
#endregion

// Configure the HTTP request pipeline

app.MapGet("/", () => "Hello Catalog API!");

app.MapCarter();

app.Run();
