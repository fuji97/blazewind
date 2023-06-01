using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Moq;
namespace Blazewind.Tests;

public class Utils
{
    public static (Mock<IHandleEvent> mock, EventCallback<T> eventCallback) CreateEventCallbackMock<T>(Task task)
    {
        var mock = new Mock<IHandleEvent>();
        mock.Setup(x => x.HandleEventAsync(It.IsAny<EventCallbackWorkItem>(), It.IsAny<object?>()))
            .Returns<EventCallbackWorkItem, object?>((_, _) => task);
        MulticastDelegate @delegate = mock.Object.HandleEventAsync;
        var eventCallback = new EventCallback<T>(mock.Object, @delegate);
        return (mock, eventCallback);
    }
}
