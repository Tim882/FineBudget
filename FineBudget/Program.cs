﻿using FineBudget;
using Microsoft.EntityFrameworkCore;
using FineBudget.Healthcheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text;
using System.Text.Json;
using DTOs.Profiles;
using FineBudget.Services.Interfaces;
using FineBudget.Services.Implementations;
using Data.UnitOfWork;
using BudgetData;
using FluentValidation.AspNetCore;
using BudgetData.Validators;
using FluentValidation;
using Models.DbModels.MainModels;
using DTOs;
using DTOs.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder => builder
            .WithOrigins("http://localhost:5174") // Ваш фронтенд-адрес
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});
builder.Services.AddAutoMapper(typeof(BudgetProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<AccountRequestDto>, AccountRequestDtoValidator>();
builder.Services.AddScoped<IValidator<AssetRequestDto>, AssetRequestDtoValidator>();
builder.Services.AddScoped<IValidator<CostRequestDto>, CostRequestDtoValidator>();
builder.Services.AddScoped<IValidator<IncomeRequestDto>, IncomeRequestDtoValidator>();
builder.Services.AddScoped<IValidator<LiabilityRequestDto>, LiabilityRequestDtoValidator>();
builder.Services.AddControllers();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddDbContext<BudgetContext>(options => options.UseNpgsql(connection));
builder.Services.AddScoped<IUnitOfWork, BudgetUnitOfWork>();
builder.Services.AddScoped<IAssetDataService, AssetDataService>();
builder.Services.AddScoped<IAccountDataService, AccountDataService>();
builder.Services.AddScoped<ICostDataService, CostDataService>();
builder.Services.AddScoped<IIncomeDataService, IncomeDataService>();
builder.Services.AddScoped<ILiabilityDataService, LiabilityDataService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks().AddCheck<LivenessHealthCheck>("live", tags: new[] { "Liveness" });
builder.Services.AddHealthChecks().AddCheck<DatabaseHealthCheck>("database", tags: new[] { "Database", "Readiness" });
builder.Services.AddHealthChecks().AddCheck<ReadinessHealthCheck>("ready", tags: new[] { "Readiness" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowReactApp");
app.UseExceptionHandler(opt => { });
//app.UseExceptionHandler();
app.MapControllers();

app.MapHealthChecks("/health/live", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("Liveness")
});
app.MapHealthChecks("/health/database", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("Database")
});
app.MapHealthChecks("/health/ready", new HealthCheckOptions
{
    Predicate = healthCheck => healthCheck.Tags.Contains("Readiness"),
    ResponseWriter = WriteResponse
});

app.Run();

static Task WriteResponse(HttpContext context, HealthReport healthReport)
{
    context.Response.ContentType = "application/json; charset=utf-8";

    var options = new JsonWriterOptions { Indented = true };

    using var memoryStream = new MemoryStream();
    using (var jsonWriter = new Utf8JsonWriter(memoryStream, options))
    {
        jsonWriter.WriteStartObject();
        jsonWriter.WriteString("status", healthReport.Status.ToString());
        jsonWriter.WriteStartObject("results");

        foreach (var healthReportEntry in healthReport.Entries)
        {
            jsonWriter.WriteStartObject(healthReportEntry.Key);
            jsonWriter.WriteString("status",
                healthReportEntry.Value.Status.ToString());
            jsonWriter.WriteString("description",
                healthReportEntry.Value.Description);
            jsonWriter.WriteStartObject("data");

            foreach (var item in healthReportEntry.Value.Data)
            {
                jsonWriter.WritePropertyName(item.Key);

                JsonSerializer.Serialize(jsonWriter, item.Value,
                    item.Value?.GetType() ?? typeof(object));
            }

            jsonWriter.WriteEndObject();
            jsonWriter.WriteEndObject();
        }

        jsonWriter.WriteEndObject();
        jsonWriter.WriteEndObject();
    }

    return context.Response.WriteAsync(
        Encoding.UTF8.GetString(memoryStream.ToArray()));
}