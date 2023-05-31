using Blazewind.Core.Helpers;
using Microsoft.AspNetCore.Components;
namespace Blazewind.Components;

public abstract class BlazewindDomComponent : BlazewindComponent {
    protected virtual string BaseClass => String.Empty;

    protected ClassBuilder ClassBuilder { get; }
    
    public BlazewindDomComponent() {
        // ReSharper disable once VirtualMemberCallInConstructor
        ClassBuilder = new ClassBuilder();
    }

    /// <summary>
    /// Specifies one or more class names for an DOM element.
    /// </summary>
    [Parameter]
    public string Class { get; set; } = string.Empty;

    /// <summary>
    /// Specifies an inline style for a DOM element.
    /// </summary>
    [Parameter]
    public string Style { get; set; } = string.Empty;
    
    
    protected string FullClass => ClassBuilder.ToString();

    private void SetClass(string @class) {
        Class = @class;
    }

    private void SetStyle(string style) {
        Style = style;
        if (!string.IsNullOrWhiteSpace(Style) && !Style.EndsWith(";"))
        {
            Style += ";";
        }
    }
    
    public override async Task SetParametersAsync(ParameterView parameters) {
        await base.SetParametersAsync(parameters);

        if (parameters.ParameterIsChanged(nameof(Class), Class, out var newValue)) {
            SetClass(newValue);
        }
        
        if (parameters.ParameterIsChanged(nameof(Style), Style, out newValue)) {
            SetStyle(newValue);
        }
    }
}
