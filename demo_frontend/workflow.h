#ifndef WORKFLOW_H
#define WORKFLOW_H

#ifdef __cplusplus
extern "C" {
#endif

// Declaration for the function to execute workflow code.
// It takes a const char* (input string) and returns a char* (output JSON string)
char* execute_code(const char* code);

// Declaration for the function to free the allocated memory of a string.
// It takes a pointer to memory that needs to be freed.
void free_string(char* ptr);

#ifdef __cplusplus
}
#endif

#endif // WORKFLOW_H
