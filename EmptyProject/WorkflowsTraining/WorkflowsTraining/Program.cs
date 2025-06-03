Console.WriteLine("My first workflow");

IEnumerable<string> commands = File.ReadLines("myworkflow.wfl");

foreach (string command in commands) {
    Console.WriteLine($"Command: {command}");

    // TODO make a command that can write a text to the console
    // maybe something like this:
    // print This text


    // TODO make a command that can loop to a number
    // maybe something like this: 
    // loop 2
    //      print("wow")
    // endloop


}