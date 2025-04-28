using WorkflowZero.Parsing;


/*
 * Hello world
 */
{

    StreamReader stream = new("TestCode/helloworld.txt");
    Parser parser = new(stream);
    ProgramNode program = parser.Parse();
    program.Execute();
}

// /*
//  * Example one
//  */
// {
//     StreamReader stream = new("TestCode/exampeOnePartOne.txt");
//     Parser parser = new(stream);
//     ProgramNode program = parser.Parse();
//
//     StreamReader stream2 = new("TestCode/exampleOnePartTwo.txt");
//     Parser parser2 = new(stream2);
//     ProgramNode program2 = parser2.Parse();
//
//     Console.WriteLine();
//     Console.WriteLine("Example1:");
//     Console.WriteLine("Part 1:");
//     program.Execute();
//
//     Console.WriteLine();
//     Console.WriteLine("Part 2:");
//     program2.Execute();
// }
//
// /*
//  * Example two
//  */
// {
//     Console.WriteLine();
//     Console.WriteLine("Example 2:");
//
//     StreamReader stream = new("TestCode/exampleTwo.txt");
//     Parser parser = new(stream);
//     ProgramNode program = parser.Parse();
//     program.Execute();
// }
//
// /*
//  * Example three
//  */
// {
//     Console.WriteLine();
//     Console.WriteLine("Example 3:");
//
//     StreamReader stream = new("TestCode/printNames.txt");
//     Parser parser = new(stream);
//     ProgramNode program = parser.Parse();
//     program.Execute();
// }


/*
 * Solutions exercises
 */
{
    Console.WriteLine();
    Console.WriteLine("Solutions:");

    StreamReader stream = new("TestCode/solutions.txt");
    Parser parser = new(stream);
    ProgramNode program = parser.Parse();
    program.Execute();
}