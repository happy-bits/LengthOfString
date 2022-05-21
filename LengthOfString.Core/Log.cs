namespace LengthOfString.Core;

public class Log
{
    private readonly bool _active;

    public Log(bool active)
    {
        _active = active;
    }

    public void WriteLine(object o)
    {
        if (_active)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(o);
        }
    }
}
