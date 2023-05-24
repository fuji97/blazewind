using System;
using System.Threading.Tasks;
using Blazewind.Components;
using FluentAssertions;
using Microsoft.AspNetCore.Components;
namespace Blazewind.Tests.Components;

public class AwaitTests : TestContext
{
    [Fact]
    public async Task CorrectRenderFragmentIsShown_Completed()
    {
        // Arrange
        RenderFragment<int> completedRf = (value) => builder => builder.AddContent(0, value);
        RenderFragment<Exception> errorRf = (value) => builder => builder.AddContent(0, ((AggregateException)value).InnerException!.Message);
        RenderFragment waitingRf = builder => builder.AddContent(0, "WAITING");
        var tcs = new TaskCompletionSource<int>();
        var cmp = RenderComponent<Await<int>>(p => p
            .Add(x => x.Task, tcs.Task)
            .Add(x => x.Completed, completedRf)
            .Add(x => x.Waiting, waitingRf)
            .Add(x => x.Error, errorRf));

        // Assert
        cmp.Markup.Should().Be("WAITING");

        // Act
        tcs.SetResult(42);
        await tcs.Task;
        await Task.Delay(1);    // Required to wait for the state to update

        // Assert
        cmp.Markup.Should().Be("42");
    }

    [Fact]
    public async Task CorrectRenderFragmentIsShown_Cancelled()
    {
        // Arrange
        RenderFragment<int> completedRf = (value) => builder => builder.AddContent(0, value);
        RenderFragment<Exception> errorRf = (value) => builder => builder.AddContent(0, value.Message);
        RenderFragment waitingRf = builder => builder.AddContent(0, "WAITING");
        var tcs = new TaskCompletionSource<int>();
        var cmp = RenderComponent<Await<int>>(p => p
            .Add(x => x.Task, tcs.Task)
            .Add(x => x.Completed, completedRf)
            .Add(x => x.Waiting, waitingRf)
            .Add(x => x.Error, errorRf));

        // Assert
        cmp.Markup.Should().Be("WAITING");

        // Act
        tcs.SetCanceled();
        await Task.Delay(1);    // Required to wait for the state to update

        // Assert
        cmp.Markup.Should().Be("Task failed");  // Not a good assert
    }

    [Fact]
    public async Task CorrectRenderFragmentIsShown_Exception()
    {
        // Arrange
        RenderFragment<int> completedRf = (value) => builder => builder.AddContent(0, value);
        RenderFragment<Exception> errorRf = (value) => builder => builder.AddContent(0, ((AggregateException)value).InnerException!.Message);
        RenderFragment waitingRf = builder => builder.AddContent(0, "WAITING");
        var tcs = new TaskCompletionSource<int>();
        var cmp = RenderComponent<Await<int>>(p => p
            .Add(x => x.Task, tcs.Task)
            .Add(x => x.Completed, completedRf)
            .Add(x => x.Waiting, waitingRf)
            .Add(x => x.Error, errorRf));

        // Assert
        cmp.Markup.Should().Be("WAITING");
        var e = new Exception("Exception");

        // Act
        tcs.SetException(e);
        await Task.Delay(1);    // Required to wait for the state to update

        // Assert
        cmp.Markup.Should().Be(e.Message);
    }
}