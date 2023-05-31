using Microsoft.AspNetCore.Components;
namespace Blazewind.Core.Helpers; 

public class HeadBlazewindComponent : IComponent {
    private RenderHandle _renderHandle;
    
    public void Attach(RenderHandle renderHandle)
        => _renderHandle = renderHandle;

    public Task SetParametersAsync(ParameterView parameters)
    {
        _renderHandle.Render((builder) =>
            {
                builder.AddMarkupContent(0, "<link rel=\"stylesheet\" href=\"_content/Blazewind/css/blazewind.css\" />");
            }
            );

        return Task.CompletedTask;
    }
}
