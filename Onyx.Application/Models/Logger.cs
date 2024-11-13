namespace Onyx.Application.Models;

public class Logger
{
    private readonly StreamWriter _writer;

    public Logger(string path)
    {
        _writer = new StreamWriter(File.Open(path, FileMode.Append))
        {
            AutoFlush = true
        };

        Log("Logger initialized");
    }

    public void Log(string str)
    {
        _writer.WriteLine(string.Format("[{0:dd.MM.yy HH:mm:ss}] {1}", DateTime.Now, str));
    }
}

public static class LogsMesaagesPatterns
{
    public const string DefaultMessage = "[{0:dd.MM.yy HH:mm:ss}] {1}";
}


public class LoggerWritter
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IWriteProvider _writeProvider;

    public LoggerWritter(IWriteProvider loggerProvider, IDateTimeProvider dateTimeProvider)
    {
        _writeProvider = loggerProvider;
        _dateTimeProvider = dateTimeProvider;

        Log("Logger initialized");
    }

    public void Log(string str)
    {
        _writeProvider.Write(string.Format(LogsMesaagesPatterns.DefaultMessage, _dateTimeProvider.GetTime(), str));
    }
}

public interface IDateTimeProvider
{
    DateTime GetTime();
}

public interface IWriteProvider
{
    void Write(string info);
}