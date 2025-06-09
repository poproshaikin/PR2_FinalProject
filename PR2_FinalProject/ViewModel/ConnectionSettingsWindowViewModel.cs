using Avalonia;
using PR2_FinalProject.Model;
using PR2_FinalProject.Services;
using PR2_FinalProject.View;
using PR2_FinalProject.View.Components;

namespace PR2_FinalProject.ViewModel;

public class ConnectionSettingsWindowViewModel
{
    public string? ConnectionString { get; set; }
    public IDialogCloser? DialogCloser { get; set; }
    public event EventHandler? OnConnectionEstablished;

    public async Task ConnectAsync()
    {
        if (string.IsNullOrWhiteSpace(ConnectionString))
            return;

        try
        {
            var app = (App)Application.Current!;
            var session = app.Session;

            bool success = session.Connect(ConnectionString, SupportedDb.PostgreSQL);
            if (!success)
                return;
            
            DialogCloser?.CloseDialog(true);
            OnConnectionEstablished?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception ex)
        {
            Logger.LogAsync(ex.Message);
        }
    }
}