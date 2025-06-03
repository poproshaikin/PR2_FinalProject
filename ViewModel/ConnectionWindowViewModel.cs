using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Model;
using ReactiveUI;

namespace ViewModel;

public class ConnectionWindowViewModel : ReactiveObject
{
    public ObservableCollection<string> AvailableDbs { get; set; } = new (Enum.GetNames<SupportedDb>());
    
    private SupportedDb? _selectedDb;

    public SupportedDb? SelectedDb 
    {
        get => _selectedDb;
        set => this.RaiseAndSetIfChanged(ref _selectedDb, value);
    }
}