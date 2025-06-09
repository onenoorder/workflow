#include <iostream>
#include <fstream>
#include <string>

int main() {
    std::cout << "My first workflow" << std::endl;

    std::ifstream inputFile("myworkflow.wfl");

    if (inputFile.is_open()) {
        std::string command;
        while (std::getline(inputFile, command)) {
            std::cout << "Command: " << command << std::endl;

            // TODO make a command that can write a text to the console
            // maybe something like this:
            // print This text


            // TODO make a command that can loop to a number
            // maybe something like this:
            // loop 2
            //     print("wow")
            // endloop


        }
        inputFile.close();
    }
    else {
        std::cerr << "Error: Unable to open myworkflow.wfl" << std::endl;
        return 1;
    }

    return 0;
}