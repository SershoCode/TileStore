// Пример замены вхождений в строке:

//var simpleString = "Привет";

//var replacedString = simpleString.Replace("вет", "страстие");

using TileStore;

// Константа для ограничения максимального количество попыток авторизации.
const int MaxAuthorizeTries = 3;

// Счетчик попыток, которые сделал пользователь.
var authorizeTryCount = 0;

// Заполняем базу данных стран.
var countryList = new List<Country>()
{
    new Country("Россия", "001", 1.0f),
    new Country("Беларусь", "002", 1.1f),
    new Country("Казахстан", "003K", 1.2f),
    new Country("Армения", "004A", 1.3f)
};

// Заполняем словарь кодов стран.
var countryCodes = new Dictionary<string, string>
    {
        { "К",     "K" } ,
        { "А",     "A" } ,
    };

Console.WriteLine("Кассовый аппарат 1.0");

var userAuthentificator = new UserAuthentificator();

# region Проходим авторизацию

while (true)
{
    authorizeTryCount++;

    Console.WriteLine("Введите имя пользователя:");
    var userName = Console.ReadLine();

    Console.WriteLine("Введите пароль пользователя:");
    var userPassword = Console.ReadLine();

    var userIsAuthorized = userAuthentificator.Authorize(userName, userPassword);

    if (userIsAuthorized)
    {
        Console.WriteLine($"Успешная авторизация! Добро пожаловать: {userName}");

        break;
    }
    else
    {
        Console.WriteLine("Некорректные логин или пароль, попробуйте снова.");

        if (authorizeTryCount >= MaxAuthorizeTries)
        {
            throw new Exception("Максимальное количество попыток исчерпано");
        }
    }
}

#endregion

Console.WriteLine("Далее мы авторизованный пользователь");

Console.WriteLine("Введите код страны, из списка ниже:");

Console.WriteLine("=================================\n" +
                  "Код для страны - покупателя:\n" +
                  "Россия:    001  || Беларусь:  002\n" +
                  "Казахстан: 003K || Армения:  004A\n" +
                  "================================= ");

while (true)
{
    var userCountryCode = Console.ReadLine();

    #region Обрабатываем некорретные коды стран

    if (userCountryCode.Length == 4)
    {
        Console.WriteLine("Обнаружен код страны с буквенным окончанием.");

        var suffix = userCountryCode.Last().ToString();

        countryCodes.TryGetValue(suffix, out var correctSuffix);

        // Если в коде страны
        if (correctSuffix != null)
        {
            Console.WriteLine($"Мы нашли русский символ в коде страны: {suffix}, подменяем на корректный на английском: {correctSuffix}");

            userCountryCode = userCountryCode.Replace(suffix, correctSuffix);
        }
    }

    // Пытаемся найти страну с таким кодом в нашей базе данных.
    var country = countryList.SingleOrDefault(cnt => cnt.Code == userCountryCode);

    // Если такая страна не найдена, тогда просим ввести заново.
    if (country == null)
    {
        Console.WriteLine("Мы не нашли такой код страны, введите заново!");

        continue;
    }
    else
    {
        Console.WriteLine("Код страны успешно найден, продолжаем");
        break;
    }
}

#endregion

Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
Console.ReadKey();