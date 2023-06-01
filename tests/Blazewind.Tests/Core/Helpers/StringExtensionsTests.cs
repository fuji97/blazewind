using Blazewind.Core.Helpers;
using FluentAssertions;
namespace Blazewind.Tests.Core.Helpers;

public class StringExtensionsTests : TestContext
{
    [Theory]
    [InlineData("class-1", new[] { "foo", "bar" }, "class-1 foo bar")]
    [InlineData("btn ", new[] { " disabled " }, "btn disabled")]
    public void JoinClasses(string baseClass, string[] classes, string expected)
    {
        // Arrange

        // Act
        var res = baseClass.JoinClasses(classes);

        // Assert
        res.Should().BeEquivalentTo(expected);
    }
}
