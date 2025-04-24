using WorkflowZero.Parsing;


/*
 * Example One
 */
{
    Console.WriteLine();
    Console.WriteLine("Example 1:");

    StreamReader stream = new("TestCode/helloworld.txt");
    Parser parser = new(stream);
    ProgramNode program = parser.Parse();
    program.Execute();
}

/*
 * Example two
 */
{
    StreamReader stream = new("TestCode/exampeOnePartOne.txt");
    Parser parser = new(stream);
    ProgramNode program = parser.Parse();

    StreamReader stream2 = new("TestCode/exampleOnePartTwo.txt");
    Parser parser2 = new(stream2);
    ProgramNode program2 = parser2.Parse();

    Console.WriteLine();
    Console.WriteLine("Example2:");
    Console.WriteLine("Part 1:");
    program.Execute();

    Console.WriteLine();
    Console.WriteLine("Part 2:");
    program2.Execute();
}

/*
 * Example three
 */
{
    Console.WriteLine();
    Console.WriteLine("Example 3:");

    StreamReader stream = new("TestCode/exampleTwo.txt");
    Parser parser = new(stream);
    ProgramNode program = parser.Parse();
    program.Execute();
}