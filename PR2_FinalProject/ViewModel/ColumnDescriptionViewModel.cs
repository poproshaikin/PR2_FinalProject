using PR2_FinalProject.ViewModel.Db;

namespace PR2_FinalProject.ViewModel;

public class ColumnDescriptionViewModel
{
    public string ColumnName { get; }
    public string DataType { get; }

    public ColumnDescriptionViewModel(DbTreeColumnViewModel dbTreeColumn)
    {
        ColumnName = dbTreeColumn.DisplayName;
        DataType = dbTreeColumn.DataType;
    }
    
}