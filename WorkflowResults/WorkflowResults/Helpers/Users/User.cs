using WorkflowResults.Helpers.Clients;

namespace WorkflowResults.Helpers.Users;

public record User(string Name, string Surname, int Age, string? Role = null, Client? Client = null);