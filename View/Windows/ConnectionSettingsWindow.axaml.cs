using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Model;

namespace View.Windows;

public partial class ConnectionSettingsWindow : Window
{
    public ConnectionSettingsWindow()
    {
        InitializeComponent();
    }

    private void OnConfirmClick(object? sender, RoutedEventArgs e)
    {
        var app = (App)Application.Current!;
        var session = app.Session;
        if (string.IsNullOrEmpty(CnnStrInput.Text) || SelectDb.SelectedItem == null)
            return;

        bool success = session.Connect(CnnStrInput.Text, Enum.Parse<SupportedDb>(SelectDb.SelectedItem!.ToString()!));
        ErrorText.Text = success ? "Failed to connect to the database." : "";
       
        if (success)
            Close();
    }
}