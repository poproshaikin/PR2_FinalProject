using System.IO;

namespace PR2_FinalProject.Services;

public static class Logger
{
    private static readonly string logFilePath;
    
    static Logger()
    {
        if (!Directory.Exists("logs"))
            Directory.CreateDirectory("logs");
        
        logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs",
            DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
    }
    
    public static async void LogAsync(string message)
    {
        try
        {
            Console.WriteLine(message);
            await File.AppendAllTextAsync(logFilePath, message + Environment.NewLine);
        }
        catch
        {
            // ignore (wtf)
        }
    }
}