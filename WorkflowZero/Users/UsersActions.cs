namespace WorkflowZero.Users;

public static class UsersActions
{
    private static readonly IList<User> UsersList =
    [
        new User("Gerard", 31),
        new User("Martijn", 25),
        new User("Tester", 315)
    ];

    public static User Find(string name)
    {
        User? user = UsersList.FirstOrDefault(user => user != null && user.Name.Equals(name), null);

        if (user == null)
        {
            throw new Exception($"Expected to find user {name} but did not find it");
        }

        return user;
    }

    public static IList<User> All()
    {
        return UsersList;
    }
}