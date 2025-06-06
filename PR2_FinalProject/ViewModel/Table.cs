using System.Collections.ObjectModel;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class DataCellViewModel
{
    public string Value { get; set; }
}

public class DataRowViewModel
{
    public ObservableCollection<DataCellViewModel> Cells { get; set; } = [];
}

public class DataTableViewModel : ReactiveObject
{
    public ObservableCollection<string> Columns { get; set; } = [];
    public ObservableCollection<DataRowViewModel> Rows { get; set; } = [];
}