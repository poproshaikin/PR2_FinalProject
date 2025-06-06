using PR2_FinalProject.ViewModel.Db;

namespace PR2_FinalProject.ViewModel;

public class TableDescriptionViewModel
{
    public string TableName { get; }
    public string Description { get; }

    public TableDescriptionViewModel(DbTreeTableViewModel dbTreeTable)
    {
        TableName = dbTreeTable.DisplayName;
        Description = $"Table {TableName} description here...";
    }
}