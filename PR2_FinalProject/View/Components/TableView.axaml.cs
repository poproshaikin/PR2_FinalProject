using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Threading;
using PR2_FinalProject.Misc;
using PR2_FinalProject.ViewModel;

namespace PR2_FinalProject.View.Components;

public partial class TableView : UserControl
{
    public TableView()
    {
        InitializeComponent();
        InitializeColumns();
    }

    private void InitializeColumns()
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            if (DataContext is null)
            {
                return;
            }

            var table = ((TableViewModel)DataContext).Table;
            if (table.Count == 0)
            {
                return;
            }

            var asDict = (IDictionary<string, object>)table[0];
            foreach (var name in asDict.Keys)
            {
                MainGrid.Columns.Add(new DataGridTextColumn() { Header = name, Binding = new Binding(name) });
            }
        });

        /*
        var tableVm = (TableViewModel)DataContext;

        if (tableVm is null || tableVm.Table.Count == 0)
            return;

        var first = (IDictionary<string, object>)tableVm.Table.First();

        MainGrid.Columns.AddRange(first.Select(kvp => new DataGridTextColumn
        {
            Header = kvp.Key,
            Binding = new Binding($"[{kvp.Key}]")
        }));
        */
    }
}