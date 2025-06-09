namespace WorkflowsTraining.Helpers.Clients;

public static class ClientActions
{
    private static IList<Client> _clientList =
    [
        new("Idle"),
        new("Thales"),
        new("ASML")
    ];

    public static Client Find(string name)
    {
        Client? client = _clientList.FirstOrDefault(client => client != null && client.Name.Equals(name), null);

        if (client == null)
        {
            throw new Exception($"Expected to find client {name} but did not find it");
        }

        return client;
    }

    public static IList<Client> All()
    {
        return _clientList;
    }
}