using System.Linq.Expressions;
namespace Blazewind.Core.Helpers; 

public static class ExpressionUtils {
    public static MemberExpression GetMemberExpression<T>(Expression<Func<T,object>> expr) 
    {
        MemberExpression? me;
        switch (expr.Body.NodeType)
        {
            case ExpressionType.Convert:
            case ExpressionType.ConvertChecked:
                me = ((expr.Body is UnaryExpression ue) ? ue.Operand : null) as MemberExpression;
                break;
            default:
                me = expr.Body as MemberExpression;
                break;
        }
        
        if (me is null) {
            throw new ArgumentException($"Expression '{expr}' must be a member expression");
        }

        return me;
    }
}
