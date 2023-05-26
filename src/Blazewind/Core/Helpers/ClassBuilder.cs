namespace Blazewind.Core.Helpers;

/// <summary>
/// Based on <see cref="https://github.com/ant-design-blazor/ant-design-blazor/blob/master/components/button/DownloadButton.razor.cs"/>
/// </summary>
public class ClassBuilder
{
    private readonly List<(Func<bool> Condition, Func<string[]> Classes)> _elements = new();

    public ClassBuilder() : this(string.Empty, string.Empty) { }
    public ClassBuilder(string baseClass) : this(baseClass, string.Empty) { }

    public ClassBuilder(string baseClass, string userClass)
    {
        BaseClass = baseClass;
        UserClass = userClass;
    }

    public string Class => ToString();

    public string BaseClass { get; internal set; }
    public string UserClass { get; internal set; }

    public override string ToString() => string.Join(' ', 
            string.Concat(
                    BaseClass,
                    _elements
                        .Where(i => i.Condition())
                        .SelectMany(i => i.Classes()),
                    UserClass)
        .Reverse()
        .Distinct() // Apply distinct to reversed string to keep the last class added
        .Reverse());

    public ClassBuilder Add(params string[] classes) => Get(() => classes);

    public ClassBuilder Get(Func<string[]> getter)
    {
        _elements.Add((() => getter().Any(), getter));
        return this;
    }

    public ClassBuilder GetIf(Func<bool> condition, Func<string[]> getter)
    {
        _elements.Add((condition, getter));
        return this;
    }

    public ClassBuilder If(Func<bool> func, params string[] classes) => GetIf(func, () => classes);

    public ClassBuilder Clear()
    {
        _elements.Clear();

        return this;
    }

    public static implicit operator string(ClassBuilder classMap) => classMap.ToString();
}