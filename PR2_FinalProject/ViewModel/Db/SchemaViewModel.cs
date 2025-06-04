using System.Collections.ObjectModel;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel.Db;

public class SchemaViewModel : ReactiveObject
{
    private bool _isExpanded;

    public bool IsExpanded
    {
        get => _isExpanded;
        set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
    }
    
    public string Name { get; set; }
    public string Owner { get; set; }
    public ObservableCollection<TableViewModel> Tables { get; set; } = [];

    public string DisplayName => $"s: {Name}";
    
}