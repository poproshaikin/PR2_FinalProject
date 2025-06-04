using Avalonia.Controls;
using PR2_FinalProject.View.Components;

namespace PR2_FinalProject.View.Windows;

public partial class ConnectionSettingsWindow : Window, IDialogCloser
{
    public ConnectionSettingsWindow()
    {
        InitializeComponent();
    }
    
    public void CloseDialog(bool result)
    {
        Close(result);
    }
}