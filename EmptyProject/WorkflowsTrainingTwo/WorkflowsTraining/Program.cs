using WorkflowsTraining.Parsing;

StreamReader stream = new("myworkflow.wfl");
Parser parser = new(stream);
ProgramNode program = parser.Parse();
program.Execute();