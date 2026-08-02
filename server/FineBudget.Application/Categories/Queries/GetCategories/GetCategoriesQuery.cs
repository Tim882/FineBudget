using System;
using MediatR;

namespace FineBudget.Application.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<List<CategoryDto>>;
}

