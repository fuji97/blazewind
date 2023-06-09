﻿using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
namespace Blazewind.Core.Helpers;

public static class ParameterViewExtensions
{
    public static bool IsChanged<T>(this ParameterView parameters, string parameterName, T? value)
    {
        return IsChanged(parameters, parameterName, value, out _);
    }

    public static bool AreChanged<T>(this ParameterView parameters, T context, params Expression<Func<T, object?>>[] parametersExpr)
    {
        foreach (var paramExpr in parametersExpr)
        {
            var memberExpr = ExpressionUtils.GetMemberExpression(paramExpr);

            var parameterName = memberExpr.Member.Name;
            var param = paramExpr.Compile()(context);
            if (parameters.IsChanged(parameterName, param, out _))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsChanged<T>(this ParameterView parameters, string parameterName, T? value, [NotNullWhen(true)] out T? newValue)
    {
        if (!parameters.TryGetValue(parameterName, out newValue))
            return false;

        return !Equals(value, newValue);
    }
}
