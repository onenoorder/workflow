namespace DemoBackend.Helpers.Output;

public class Output
{
    private readonly IList<string> _outputStrings = [];


    public void WriteLine(string text)
    {
        _outputStrings.Add(text);
    }

    public IList<string> CollectOutput()
    {
        return _outputStrings;
    }
}