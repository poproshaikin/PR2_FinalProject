using Avalonia.Controls;
using Avalonia.Interactivity;
using View.Windows;
using ViewModel;

namespace View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }

    private void OpenConnectionWindow(object? sender, RoutedEventArgs e)
    {
        var window = new ConnectionSettingsWindow();
        window.ShowDialog(this);
    }
}