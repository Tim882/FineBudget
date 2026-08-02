using System;
using MediatR;

namespace FineBudget.Application.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : IRequest;
}

