using View.Windows;
using ViewModel;

namespace View.Services;

public interface IWindowsService
{
    void ShowConnectionSettingsWindow();
}

public class WindowsService : IWindowsService
{
    public void ShowConnectionSettingsWindow()
    {
        var window = new ConnectionSettingsWindow()
        {
            DataContext = new ConnectionWindowViewModel()
        };
        window.Show();
    }
}