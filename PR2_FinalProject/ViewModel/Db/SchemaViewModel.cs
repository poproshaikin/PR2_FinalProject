using System.Collections.ObjectModel;

namespace PR2_FinalProject.ViewModel.Db;

public class SchemaViewModel
{
    public string Name { get; set; }
    public string Owner { get; set; }
    public ObservableCollection<TableViewModel> Tables { get; set; } = [];

    public string DisplayName => $"s: {Name}";
}