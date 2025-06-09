using System.Collections.ObjectModel;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class TableViewModel : ReactiveObject
{
    public ObservableCollection<dynamic> Table { get; }
    
    public TableViewModel(ObservableCollection<dynamic> rows)
    {
        Table = rows;
    }
}