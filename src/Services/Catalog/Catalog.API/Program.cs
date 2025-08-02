using BuildingBlocks.Behaviors;

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

var app = builder.Build();

// Configure the HTTP request pipeline

app.MapGet("/", () => "Hello Catalog API!");

app.MapCarter();

app.Run();
