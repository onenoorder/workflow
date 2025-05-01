using WorkflowZero.Helpers.Clients;

namespace WorkflowZero.Helpers.Users;

public record User(string Name, string Surname, int Age, string? Role = null, Client? Client = null);