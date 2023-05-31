using System.Text;
namespace Blazewind.Core.Helpers; 

public class ClassBuilder {
    private readonly StringBuilder _builder = new();
    
    public void Clear() {
        _builder.Clear();
    }
    
    public ClassBuilder Add(string @class) {
        if (_builder.Length > 0) {
            _builder.Append(' ');
        }
        _builder.Append(@class.Trim());
        
        return this;
    }
    
    public ClassBuilder AddIf(bool condition, string @class) {
        if (condition) {
            Add(@class);
        }
        
        return this;
    }
    
    public ClassBuilder AddIfElse(bool condition, string @class, string elseClass) {
        if (condition) {
            Add(@class);
        } else {
            Add(elseClass);
        }
        
        return this;
    }

    public override string ToString() {
        return _builder.ToString();
    }
}
