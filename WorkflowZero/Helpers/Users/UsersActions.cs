namespace WorkflowZero.Helpers.Users;

public static class UsersActions
{
    private static IList<User> _usersList =
    [
        new("Gerard" , "Kroes", 31),
        new("Martijn","Lankhof", 25),
        new("Alice","Johnson", 28),
        new("Bob","Smith", 35),
        new("Charlie","Davis", 22),
        new("Diana","Moore", 30),
        new("Eve","Thompson", 27),
        new("Frank","Williams", 41),
        new("Grace","Miller", 33),
        new("Hank","Wilson", 45),
        new("Ivy","Taylor", 26),
        new("Jack","Anderson", 31),
        new("Karen","Martinez", 38),
        new("Leo","Brown", 29),
        new("Mona","Clark", 24),
        new("Nate","Lewis", 36),
        new("Olivia","Walker", 32),
        new("Paul","Hall", 40),
        new("Quinn","Young", 23),
        new("Rose", "King", 34),
        new("Steve","Wright", 37),
        new("Tina","Scott", 21)
    ];

    public static User Find(string name)
    {
        User? user = _usersList.FirstOrDefault(user => user != null && (user.Name + user.Surname).Equals(name.Replace(" ", "")), null);

        if (user == null)
        {
            throw new Exception($"Expected to find user {name} but did not find it");
        }

        return user;
    }

    public static IList<User> All()
    {
        return _usersList;
    }

    public static User Add(string name, string surname, int age)
    {
        User newUser = new (name, surname, age);
        _usersList.Add(newUser);
        return newUser;
    }

    public static string GetInitial(User user)
    {
        return user.Name[0].ToString();
    }
}