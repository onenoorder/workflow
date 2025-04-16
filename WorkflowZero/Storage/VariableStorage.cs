namespace WorkflowZero.Storage;

public static class VariableStorage
{
    private static readonly IDictionary<string, object> Variables = new Dictionary<string, object>();

    public static object GetVariable(string varName)
    {
        if (Variables.TryGetValue(varName, out object? value))
        {
            return value ?? throw new Exception($"Variable {varName} was null");
        }

        throw new Exception($"Unknown variable {varName}");
    }
    
    public static void StoreVariable(string varName, object value)
    {
        Variables[varName] = value;
    }
}