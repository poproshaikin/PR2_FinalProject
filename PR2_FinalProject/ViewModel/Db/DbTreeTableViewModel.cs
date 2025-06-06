using System.Collections.ObjectModel;

namespace PR2_FinalProject.ViewModel.Db;

public class DbTreeTableViewModel
{
    public string Name { get; set; }
    public int RowCount { get; set; }
    public ObservableCollection<DbTreeColumnViewModel> Columns { get; set; } = [];
    
    public string DisplayName => $"t: {Name}";
}