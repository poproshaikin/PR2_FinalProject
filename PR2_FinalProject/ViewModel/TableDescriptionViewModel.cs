using PR2_FinalProject.ViewModel.Db;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class TableDescriptionViewModel : ReactiveObject
{
    public string TableName { get; }
    public string Description { get; }
    private TableViewModel _bindTable = null!;

    public TableViewModel BindTable
    {
        get => _bindTable;    
        set => this.RaiseAndSetIfChanged(ref _bindTable, value);
    }
    
    public TableDescriptionViewModel(DbTreeTableViewModel dbTreeTable, TableViewModel bindTable)
    {
        TableName = dbTreeTable.DisplayName;
        Description = $"Table {TableName} description here...";
        BindTable = bindTable;
    }
}