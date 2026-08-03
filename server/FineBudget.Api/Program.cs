using System.Text;
using FineBudget.Api.Services;
using FineBudget.Application;
using FineBudget.Application.Auth;
using FineBudget.Application.Auth.Commands.Login;
using FineBudget.Application.Auth.Commands.Logout;
using FineBudget.Application.Auth.Commands.RefreshToken;
using FineBudget.Application.Auth.Commands.Register;
using FineBudget.Application.Categories.Commands.CreateCategory;
using FineBudget.Application.Categories.Commands.DeleteCategory;
using FineBudget.Application.Categories.Commands.UpdateCategory;
using FineBudget.Application.Categories.Queries.GetCategories;
using FineBudget.Application.Categories.Queries.GetCategoryById;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Application.Statistics.Queries.GetByCategory;
using FineBudget.Application.Transactions.Commands.CreateTransaction;
using FineBudget.Application.Transactions.Commands.DeleteTransaction;
using FineBudget.Application.Transactions.Commands.UpdateTransaction;
using FineBudget.Application.Transactions.Queries.GetTransactionById;
using FineBudget.Application.Transactions.Queries.GetTransactionsByMonth;
using FineBudget.Infrastructure;
using FineBudget.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// ================================================================
// Serilog
// ================================================================
builder.Host.UseSerilog((context, config) =>
{
    config
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(context.Configuration["Seq:ServerUrl"]!);
});

// ================================================================
// JWT Authentication
// ================================================================
var jwtSecret = builder.Configuration["Jwt:Secret"]!;
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = key
    };
});

builder.Services.AddAuthorization();

// ================================================================
// Services
// ================================================================
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FineBudget API", Version = "v1" });

    // Добавляем возможность вводить JWT в Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Health Checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>(tags: new[] { "db" });

// OpenTelemetry
builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("FineBudget.Api"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddEntityFrameworkCoreInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
            options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
        }))
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter(options =>
        {
            options.Endpoint = new Uri("http://localhost:4317");
            options.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.Grpc;
        }));

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

app.UseSerilogRequestLogging();
app.UseCors("AllowClient");

app.UseAuthentication();
app.UseAuthorization();

// Health Check endpoint
app.MapHealthChecks("/health");

// ================================================================
// Global exception handling
// ================================================================
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (FluentValidation.ValidationException ex)
    {
        context.Response.StatusCode = 400;
        context.Response.ContentType = "application/json";
        var errors = ex.Errors.Select(e => new { field = e.PropertyName, message = e.ErrorMessage });
        await context.Response.WriteAsJsonAsync(new { errors });
    }
    catch (KeyNotFoundException ex)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (InvalidOperationException ex)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (UnauthorizedAccessException ex)
    {
        context.Response.StatusCode = 401;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
});

// ================================================================
// AUTH ENDPOINTS (без авторизации)
// ================================================================
var auth = app.MapGroup("/api/auth")
    .WithTags("Authentication");

auth.MapPost("/register", async (RegisterCommand command, ISender sender) =>
{
    var result = await sender.Send(command);
    return Results.Ok(result);
});

auth.MapPost("/login", async (LoginCommand command, ISender sender) =>
{
    var result = await sender.Send(command);
    return Results.Ok(result);
});

auth.MapPost("/refresh", async (RefreshTokenCommand command, ISender sender) =>
{
    var result = await sender.Send(command);
    return Results.Ok(result);
});

auth.MapPost("/logout", async (LogoutCommand command, ISender sender) =>
{
    await sender.Send(command);
    return Results.NoContent();
}).RequireAuthorization();

// ================================================================
// CATEGORIES (требуют авторизации)
// ================================================================
var categories = app.MapGroup("/api/categories")
    .WithTags("Categories")
    .RequireAuthorization();

categories.MapGet("/", async (ISender sender) =>
    await sender.Send(new GetCategoriesQuery()));

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
// TRANSACTIONS (требуют авторизации)
// ================================================================
var transactions = app.MapGroup("/api/transactions")
    .WithTags("Transactions")
    .RequireAuthorization();

transactions.MapGet("/", async (int year, int month, ISender sender) =>
    await sender.Send(new GetTransactionsByMonthQuery(year, month)));

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
// STATISTICS (требуют авторизации)
// ================================================================
var statistics = app.MapGroup("/api/statistics")
    .WithTags("Statistics")
    .RequireAuthorization();

statistics.MapGet("/by-category", async (int year, int month, ISender sender) =>
    await sender.Send(new GetByCategoryQuery(year, month)));

app.Run();