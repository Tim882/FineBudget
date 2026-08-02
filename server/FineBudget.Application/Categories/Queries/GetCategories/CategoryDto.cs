using System;
namespace FineBudget.Application.Categories.Queries.GetCategories
{
    public record CategoryDto(Guid Id, string Name, string Icon, string DefaultType);
}

