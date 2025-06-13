namespace WorkflowResults.Helpers.Clients;

public static class ClientActions
{
    private static IList<Client> _clientList =
    [
        new("Idle")
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

    public static Client Add(string name)
    {
        Client newClient = new (name);

        _clientList.Add(newClient);

        return newClient;
    }
}