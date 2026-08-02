using System;
namespace FineBudget.Application.Statistics.Queries.GetByCategory
{
    public record CategoryStatDto(string CategoryName, string CategoryIcon, decimal Total);
}

