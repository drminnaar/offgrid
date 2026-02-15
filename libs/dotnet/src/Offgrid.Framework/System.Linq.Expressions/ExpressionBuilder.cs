using System.Linq.Expressions;

namespace Offgrid.Framework.System.Linq.Expressions;

public static class ExpressionBuilder
{
    public static Expression<Func<T, bool>> For<T>() => True<T>();
    public static Expression<Func<T, bool>> True<T>() => f => true;
    public static Expression<Func<T, bool>> False<T>() => f => false;

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        ArgumentNullException.ThrowIfNull(expr1);
        ArgumentNullException.ThrowIfNull(expr2);

        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());

        return Expression.Lambda<Func<T, bool>>(
            Expression.OrElse(expr1.Body, invokedExpr),
            expr1.Parameters);
    }

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
    {
        ArgumentNullException.ThrowIfNull(expr1);
        ArgumentNullException.ThrowIfNull(expr2);

        var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());

        return Expression.Lambda<Func<T, bool>>(
            Expression.AndAlso(expr1.Body, invokedExpr),
            expr1.Parameters);
    }
}
