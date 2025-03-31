using WorkflowZero.AbstractSyntaxTree.Nodes;
using WorkflowZero.Interpreting;
using WorkflowZero.Parsing;


/*
 * Example One
 */

// StreamReader stream =  new("TestCode/exampeOnePartOne.txt");
// Parser parser = new(stream);
// ProgramNode program = parser.Parse();
//
// StreamReader stream2 =  new("TestCode/exampleOnePartTwo.txt");
// Parser parser2 = new(stream2);
// ProgramNode program2 = parser2.Parse();
//
// Interpreter interpreter = new();
//
// Console.WriteLine("Program 1:");
// interpreter.ExecuteProgram(program);
//
// Console.WriteLine("");
// Console.WriteLine("Program 2:");
// interpreter.ExecuteProgram(program2);

/*
 * Example two
 */

StreamReader stream =  new("TestCode/exampleTwo.txt");
Parser parser = new(stream);
ProgramNode program = parser.Parse();
Interpreter interpreter = new();
interpreter.ExecuteProgram(program);