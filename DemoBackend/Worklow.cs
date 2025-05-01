using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using DemoBackend.Helpers.Output;
using DemoBackend.Parsing;

namespace DemoBackend;

[JsonSerializable(typeof(IList<string>))]

public static class WorkflowExecutor
{
    [UnmanagedCallersOnly(EntryPoint = "execute_code")]
    public static IntPtr ExecuteWorkflowCode(IntPtr codePtr)
    {
        // Convert C types to pure C# types
        string? code = Marshal.PtrToStringUTF8(codePtr);
        if ( code == null)
        {
            return IntPtr.Zero;
        }
        // Run pure C# method
        string result = ExecuteWorkflow(code);
            
        //Convert pure C# types to C types
        return Marshal.StringToHGlobalAnsi(result);
    }
    [UnmanagedCallersOnly(EntryPoint = "free_string")]
    public static void FreeString(IntPtr ptr)
    {
        //Can be used to clean up your memory
        if (ptr != IntPtr.Zero)
        {
            Marshal.FreeHGlobal(ptr);
        }
    }

    private static string ExecuteWorkflow(string code)
    {
        byte[] byteArray = Encoding.UTF8.GetBytes(code);
        MemoryStream memoryStream = new(byteArray);
        StreamReader stream = new (memoryStream);
        Parser parser = new(stream);
        ProgramNode program = parser.Parse();
        Output output = new();
        program.Execute(output);
        IList<string> result =  output.CollectOutput();
        return "[" + string.Join(",", result.Select(s => $"\"{s}\"")) + "]";
    }
    
}