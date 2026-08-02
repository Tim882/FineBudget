using FineBudget.Application;
using FineBudget.Application.Categories.Commands.CreateCategory;
using FineBudget.Application.Categories.Commands.DeleteCategory;
using FineBudget.Application.Categories.Commands.UpdateCategory;
using FineBudget.Application.Categories.Queries.GetCategories;
using FineBudget.Application.Categories.Queries.GetCategoryById;
using FineBudget.Application.Statistics.Queries.GetByCategory;
using FineBudget.Application.Transactions.Commands.CreateTransaction;
using FineBudget.Application.Transactions.Commands.DeleteTransaction;
using FineBudget.Application.Transactions.Commands.UpdateTransaction;
using FineBudget.Application.Transactions.Queries.GetTransactionById;
using FineBudget.Application.Transactions.Queries.GetTransactionsByMonth;
using FineBudget.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FineBudget API", Version = "v1" }));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowClient");

// ================================================================
// CATEGORIES
// ================================================================
var categories = app.MapGroup("/api/categories")
.WithTags("Categories");

categories.MapGet("/", async (ISender sender) =>
{
    var result = await sender.Send(new GetCategoriesQuery());
    return Results.Ok(result);
});

categories.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
{
    var result = await sender.Send(new GetCategoryByIdQuery(id));
    return result is not null ? Results.Ok(result) : Results.NotFound();
});

categories.MapPost("/", async (CreateCategoryCommand command, ISender sender) =>
{
    var id = await sender.Send(command);
    return Results.Created($"/api/categories/{id}", new { id });
});

categories.MapPut("/{id:guid}", async (Guid id, UpdateCategoryCommand command, ISender sender) =>
{
    if (id != command.Id)
        return Results.BadRequest("ID in URL and body must match");

    await sender.Send(command);
    return Results.NoContent();
});

categories.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
{
    await sender.Send(new DeleteCategoryCommand(id));
    return Results.NoContent();
});

// ================================================================
// TRANSACTIONS
// ================================================================
var transactions = app.MapGroup("/api/transactions")
.WithTags("Transactions");

transactions.MapGet("/", async (int year, int month, ISender sender) =>
{
    var result = await sender.Send(new GetTransactionsByMonthQuery(year, month));
    return Results.Ok(result);
});

transactions.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
{
    var result = await sender.Send(new GetTransactionByIdQuery(id));
    return result is not null ? Results.Ok(result) : Results.NotFound();
});

transactions.MapPost("/", async (CreateTransactionCommand command, ISender sender) =>
{
    var id = await sender.Send(command);
    return Results.Created($"/api/transactions/{id}", new { id });
});

transactions.MapPut("/{id:guid}", async (Guid id, UpdateTransactionCommand command, ISender sender) =>
{
    if (id != command.Id)
        return Results.BadRequest("ID in URL and body must match");

    await sender.Send(command);
    return Results.NoContent();
});

transactions.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
{
    await sender.Send(new DeleteTransactionCommand(id));
    return Results.NoContent();
});

// ================================================================
// STATISTICS
// ================================================================
var statistics = app.MapGroup("/api/statistics")
.WithTags("Statistics");

statistics.MapGet("/by-category", async (int year, int month, ISender sender) =>
{
    var result = await sender.Send(new GetByCategoryQuery(year, month));
    return Results.Ok(result);
});

app.Run();