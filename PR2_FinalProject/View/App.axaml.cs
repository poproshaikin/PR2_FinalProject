using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PR2_FinalProject.Services;

namespace PR2_FinalProject.View;

public partial class App : Application
{
    public SessionService Session { get; private set; } = new();
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new View.MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}