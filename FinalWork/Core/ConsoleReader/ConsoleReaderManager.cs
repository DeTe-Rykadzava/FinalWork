namespace FinalWork.Core.ConsoleReader;

public static class ConsoleReaderManager
{
    private static readonly ConsoleReader Reader = new();

    private static object? _userInput;

    public static object? GetUserInput(string message, UserInputType? requiredType = null)
    {
        Reader.SuccessReadEvent += (object? res) => { _userInput = res; };
        Reader.FailReadEvent += async () =>
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Error:");
            Console.ResetColor();
            Console.WriteLine(" The input string has the wrong type or is empty. Try again!");
            Reader.GetUserInput(requiredType);
        };
        Console.Write($"{message} ");
        Reader.GetUserInput(requiredType);
        return _userInput;
    }

    public static object? GetUserInput(UserInputType? requiredType = null) // without message for user
    {
        Reader.SuccessReadEvent += (object? res) => { _userInput = res; };
        Reader.FailReadEvent += async () =>
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.Write("Error:");
            Console.ResetColor();
            Console.WriteLine(" The input string has the wrong type or is empty. Try again!");
            Reader.GetUserInput(requiredType);
        };
        Reader.GetUserInput(requiredType);
        return _userInput;
    }

    private class ConsoleReader
    {
        public event Action<object?>? SuccessReadEvent;

        public event Action? FailReadEvent;

        public void GetUserInput(UserInputType? type = null)
        {
            var userInput = Console.ReadLine();

            if (type == null || string.IsNullOrWhiteSpace(userInput))
            {
                SuccessReadEvent?.Invoke(null);
                return;
            }

            switch (type)
            {
                case UserInputType.Int:
                    if (int.TryParse(userInput, out var @int))
                        SuccessReadEvent?.Invoke(@int);
                    else
                        FailReadEvent?.Invoke();
                    break;
                case UserInputType.Double:
                    if (double.TryParse(userInput, out var @double))
                        SuccessReadEvent?.Invoke(@double);
                    else
                        FailReadEvent?.Invoke();
                    break;
                case UserInputType.String:
                    SuccessReadEvent?.Invoke(userInput);
                    break;
                default:
                    break;
            }
        }
    }
}