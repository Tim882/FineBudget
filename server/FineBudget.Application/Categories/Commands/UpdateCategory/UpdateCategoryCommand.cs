using System;
using MediatR;

namespace FineBudget.Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name, string Icon, int DefaultType) : IRequest;
}

