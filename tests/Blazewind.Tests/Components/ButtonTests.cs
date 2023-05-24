using System;
using System.Threading.Tasks;
using Blazewind.Components;
using FluentAssertions;
using Microsoft.AspNetCore.Components.Web;
using Moq;
namespace Blazewind.Tests.Components;

public class ButtonTests : TestContext
{
    [Fact]
    public void ClickingButtonTriggersEvent()
    {
        // Arrange
        var clickMock = new Mock<Action<MouseEventArgs>>();
        var button = RenderComponent<Button>(p => p.Add(x => x.OnClick, clickMock.Object));

        // Act
        button.Find("button").Click();

        // Assert
        clickMock.Verify();
    }

    [Fact]
    public async Task ClickingButtonTriggersEventAsync()
    {
        // Arrange
        var tcs = new TaskCompletionSource();
        var (mock, eventCallback) = Utils.CreateEventCallbackMock<MouseEventArgs>(tcs.Task);
        var cmp = RenderComponent<Button>(p => p.Add(x => x.OnClickAsync, eventCallback));
        var button = cmp.Find("button");

        // Act
        button.Click();

        // Assert
        button.HasAttribute("disabled").Should().BeTrue();

        // Act
        tcs.SetResult();
        await tcs.Task;
        await Task.Delay(1);    // Required to wait for the state to update

        // Assert
        mock.Verify();
        button.HasAttribute("disabled").Should().BeFalse();
    }

    [Fact]
    public async Task ButtonDisabledUntilEndOfTask()
    {
        // Arrange
        var tcs = new TaskCompletionSource();
        var cmp = RenderComponent<Button>(p => p.Add(x => x.Task, tcs.Task));
        var button = cmp.Find("button");

        // Assert
        button.HasAttribute("disabled").Should().BeTrue();


        // Act
        tcs.SetResult();
        await tcs.Task;
        await Task.Delay(1);    // Required to wait for the state to update

        // Assert
        button.HasAttribute("disabled").Should().BeFalse();
    }
}
