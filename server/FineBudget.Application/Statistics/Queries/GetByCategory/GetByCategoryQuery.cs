using System;
using MediatR;

namespace FineBudget.Application.Statistics.Queries.GetByCategory
{
    public record GetByCategoryQuery(int Year, int Month) : IRequest<List<CategoryStatDto>>;
}

