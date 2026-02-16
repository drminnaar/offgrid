using System.Linq.Expressions;
using Offgrid.Framework.System.Linq.Expressions;
using Offgrid.Portal.Customers.Domain.Entities;

namespace Offgrid.Portal.Customers.Application.Queries.GetAllCustomers.Filters;

public static partial class FilterExpressionExtensions
{
    /// <summary>
    /// Converts a <see cref="GetAllCustomersQuery"/> into a filter expression for querying customers.
    /// </summary>
    /// <remarks>
    /// This method builds an expression tree based on the filter criteria specified in the query.
    /// It currently supports filtering by customer status, but can be extended to include additional
    /// criteria as needed.
    /// </remarks>
    /// <param name="query">The query containing filter criteria.</param>
    /// <returns>An expression representing the filter criteria.</returns>
    public static Expression<Func<Customer, bool>> ToFilterExpression(this GetAllCustomersQuery query)
    {
        var filter = ExpressionBuilder.For<Customer>();

        if (query.ParsedStatus.HasValue)
        {
            filter = filter.And(c => c.Status == query.ParsedStatus.Value);
        }

        return filter;
    }
}
