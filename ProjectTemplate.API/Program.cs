using ProjectTemplate.Infrastructure;
using ProjectTemplate.Applications;
using ProjectTemplate.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using ProjectTemplate.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("Postgres")!;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add services (automated)
builder.Services.AddRepositories();
builder.Services.AddServices();

// Map controllers endpoints
builder.Services.AddControllers();

// Configure postgres database context
builder.Services.AddDbContext<CoreDbContext>((sp, options) =>
{
	options.UseNpgsql(connectionString)
		.EnableDetailedErrors();
});

var app = builder.Build();

// Use middleware for custom exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();

}
app.MapControllers();
app.UseHttpsRedirection();

app.Run();