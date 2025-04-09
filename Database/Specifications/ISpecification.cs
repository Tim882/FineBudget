using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Database
{
    public interface ISpecification<T>
    {
        // Filtering
        Expression<Func<T, bool>> Criteria { get; }

        // Includes (eager loading)
        List<string> IncludeStrings { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        // Sorting
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        // Pagination
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
