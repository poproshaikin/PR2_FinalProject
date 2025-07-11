using System.Collections.ObjectModel;
using Avalonia;
using PR2_FinalProject.Services;
using PR2_FinalProject.Services.Messages;
using PR2_FinalProject.View;
using PR2_FinalProject.ViewModel.Db;
using ReactiveUI;
using PR2_FinalProject.Misc;

namespace PR2_FinalProject.ViewModel;

public class DbStructureTreeViewModel : ReactiveObject
{
    public ObservableCollection<SchemaViewModel> Schemas { get; set; }
    public bool HasSchemas => Schemas.Any();
    public bool NoSchemas => !HasSchemas;
    public event Action<object>? OnItemSelected;
    
    public DbStructureTreeViewModel()
    {
        Schemas = [];
        MessageBus.Current.Listen<ConnectionEstablishedMessage>().Subscribe(_ =>
        {
            Logger.LogAsync("ConnectionEstablished");
            LoadSchemas();
        });
    }

    public void SelectItem(object item)
    {
        OnItemSelected?.Invoke(item);
    }
    
    public void LoadSchemas()
    {
        var app = (App)Application.Current!;
        var session = app.Session;
        Logger.LogAsync("Loading schemas");
        SchemaViewModel[] schemas = session.ConnectionService.LoadSchemas();
        Schemas.Clear();
        Schemas.AddRange(schemas);
        this.RaisePropertyChanged(nameof(HasSchemas));
        this.RaisePropertyChanged(nameof(NoSchemas));
        Logger.LogAsync($"Loaded information about {schemas.Length} schemas.");
        Logger.LogAsync($"Current schemas list length is {schemas.Length}.");
    }
}