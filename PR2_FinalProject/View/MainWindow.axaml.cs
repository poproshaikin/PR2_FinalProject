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
}