using Avalonia.Controls;
using Avalonia.Interactivity;
using PR2_FinalProject.View.Windows;
using PR2_FinalProject.ViewModel;

namespace PR2_FinalProject.View;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        DataContext = new MainViewModel(this);
        InitializeComponent();
    }
    
    private void OpenConnectionDialog(object sender, RoutedEventArgs e)
    {
        var vm = new ConnectionSettingsWindowViewModel();
        var window = new ConnectionSettingsWindow()
        {
            DataContext = vm
        };
        vm.DialogCloser = window;

        window.ShowDialog(this);
    }
}