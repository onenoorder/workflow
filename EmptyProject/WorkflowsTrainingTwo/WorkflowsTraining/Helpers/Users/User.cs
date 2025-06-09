using WorkflowsTraining.Helpers.Clients;

namespace WorkflowsTraining.Helpers.Users;

public record User(string Name, string Surname, int Age, string? Role = null, Client? Client = null);