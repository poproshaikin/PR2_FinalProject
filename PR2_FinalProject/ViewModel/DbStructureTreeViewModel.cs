using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using DynamicData;
using PR2_FinalProject.Services;
using PR2_FinalProject.Services.Messages;
using PR2_FinalProject.View;
using PR2_FinalProject.ViewModel.Db;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class DbStructureTreeViewModel
{
    public ObservableCollection<SchemaViewModel> Schemas { get; set; }
    public bool HasSchemas => Schemas.Any();
    public bool NoSchemas => !HasSchemas;

    public DbStructureTreeViewModel()
    {
        Schemas = [];
        MessageBus.Current.Listen<ConnectionEstablishedMessage>().Subscribe(_ =>
        {
            Logger.Log("ConnectionEstablished");
            LoadSchemas();
        });
    }
    
    private void LoadSchemas()
    {
        var app = (App)Application.Current!;
        var session = app.Session;
        Logger.Log("Loading Schemas");
        SchemaViewModel[] schemas = session.ConnectionService.LoadSchemas();
        Schemas.Clear();
        Schemas.AddRange(schemas);
    }
}