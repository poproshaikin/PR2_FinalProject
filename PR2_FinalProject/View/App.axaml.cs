using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using PR2_FinalProject.Services;

namespace PR2_FinalProject.View;

public partial class App : Application
{
    public SessionService Session { get; private set; } = new();
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        Dispatcher.UIThread.UnhandledException += (s, e) =>
        {
            Logger.LogAsync($"Unhandled UI exception: {e.Exception}");
        };

        TaskScheduler.UnobservedTaskException += (s, e) =>
        {
            Logger.LogAsync($"Unhandled task exception: {e.Exception}");
        };
        
        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            if (e.ExceptionObject is Exception ex)
            {
                Logger.LogAsync($"Unhandled domain exception: {ex.Message}");
            }
            else
            {
                Logger.LogAsync("Unhandled domain exception: unknown object");
            }
        };        
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}