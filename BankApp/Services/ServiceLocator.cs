namespace BankApp.Services;

public static class ServiceLocator
{
    public static string UserKey { get; } = GenerateUserKey();

    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    private static string GenerateUserKey()
    {
        var random = new Random();
        return random.Next(10000000, 99999999).ToString();
    }
}
