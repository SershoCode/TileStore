namespace TileStore;

public class UserAuthentificator
{
    private Dictionary<string, string> _users = new Dictionary<string, string> 
    {
        { "Админ",     "000" } ,
        { "Иванова",   "111" } ,
        { "Петрова",   "222" } ,
        { "Сергеева",  "333" } ,
        { "Васильева", "444" } ,
    };

    public bool Authorize(string username, string userPassword)
    {
        _users.TryGetValue(username, out var passwordFromDictionary );

        if( passwordFromDictionary == null) 
        {
            Console.WriteLine($"Не нашли в словаре пользователя: {username}");
        }

        return passwordFromDictionary == userPassword; 
    }
}
