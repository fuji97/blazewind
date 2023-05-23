using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;
namespace Blazewind.Core.Helpers; 

public static class ParameterViewExtensions {
    public static bool ParameterIsChanged<T>(this ParameterView parameters, string parameterName, [NotNullWhen(true)] T? value)
    {
        if (!parameters.TryGetValue(parameterName, out T? newValue)) 
            return false;
        
        return !EqualityComparer<T>.Default.Equals(value, newValue);
    }
}
