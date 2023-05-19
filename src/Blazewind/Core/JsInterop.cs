using Microsoft.JSInterop;
namespace Blazewind.Core;

public class JsInterop : IAsyncDisposable {
    private const string ModulePath = "./_content/Blazewind/js/blazewind.js";

    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public JsInterop(IJSRuntime jsRuntime) {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
           "import", ModulePath).AsTask());
    }

    public async ValueTask<string> Prompt(string message) {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("showPrompt", message);
    }

    public async ValueTask DisposeAsync() {
        if (_moduleTask.IsValueCreated) {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
