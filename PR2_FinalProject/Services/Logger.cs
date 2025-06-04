
using System;

namespace PR2_FinalProject.Services;

public static class Logger
{
    private static string _currentSessionLogFileName;
    
    static Logger()
    {
        // TODO create a session file 
    }
    
    public static void Log(string message)
    {
        Console.WriteLine(message);
        // TODO write message into session file 
    }
}