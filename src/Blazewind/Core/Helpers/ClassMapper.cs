namespace Blazewind.Core.Helpers;

public interface IClassEntry {
    public ICollection<string> Classes { get; }
}

/// <summary>
/// Based on <see cref="https://github.com/ant-design-blazor/ant-design-blazor/blob/master/components/button/DownloadButton.razor.cs"/>
/// </summary>
public class ClassMap
{
    private readonly Dictionary<Func<string>, Func<bool>> _map = new();

    public ClassMap() : this(string.Empty)
    {
    }

    public ClassMap(string originalClass)
    {
        OriginalClass = originalClass;

        _map.Add(() => OriginalClass, () => !string.IsNullOrEmpty(OriginalClass));
    }

    public string Class => ToString();

    public string OriginalClass { get; internal set; }

    public override string ToString() => string.Join(' ', _map.Where(i => i.Value())
        .Select(i => i.Key()));

    public ClassMap Add(string name) => Get(() => name);

    public ClassMap Get(Func<string> funcName)
    {
        _map.Add(funcName, () => !string.IsNullOrEmpty(funcName()));
        return this;
    }

    public ClassMap GetIf(Func<string> funcName, Func<bool> func)
    {
        _map.Add(funcName, func);
        return this;
    }

    public ClassMap If(string name, Func<bool> func) => GetIf(() => name, func);

    public ClassMap Clear()
    {
        _map.Clear();

        _map.Add(() => OriginalClass, () => !string.IsNullOrEmpty(OriginalClass));

        return this;
    }
    
    public class ConditionalClassMap
    {
        private readonly ClassMap _classMap;
        private readonly Func<bool> _condition;

        public ConditionalClassMap(ClassMap classMap, Func<bool> condition)
        {
            _classMap = classMap;
            _condition = condition;
        }

        public ClassMap Get(Func<string> funcName)
        {
            _classMap.GetIf(funcName, _condition);
            return _classMap;
        }

        public ClassMap If(string name, Func<bool> func) => Get(() => name);
    }
}