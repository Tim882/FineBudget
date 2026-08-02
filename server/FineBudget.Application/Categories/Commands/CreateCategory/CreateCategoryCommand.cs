using System;
using MediatR;

namespace FineBudget.Application.Categories.Commands.CreateCategory
{
    public record CreateCategoryCommand(
        string Name,
        string Icon,
        int DefaultType
    ) : IRequest<Guid>;
}

