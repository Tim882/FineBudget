using System;
using FineBudget.Application.Categories.Queries.GetCategories;
using MediatR;

namespace FineBudget.Application.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto?>;
}

