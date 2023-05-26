using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
namespace Blazewind.Core.Helpers; 

public static class ParameterViewExtensions {
    public static bool ParameterIsChanged<T>(this ParameterView parameters, string parameterName, T? value)
    {
        return ParameterIsChanged(parameters, parameterName, value, out _);
    }
    
    public static bool ParameterIsChanged<T>(this ParameterView parameters, string parameterName, T? value, [NotNullWhen(true)] out T? newValue)
    {
        if (!parameters.TryGetValue(parameterName, out newValue)) 
            return false;
        
        return !EqualityComparer<T>.Default.Equals(value, newValue);
    }
}
