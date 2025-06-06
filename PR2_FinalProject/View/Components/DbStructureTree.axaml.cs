using Avalonia.Controls;
using PR2_FinalProject.ViewModel;

namespace PR2_FinalProject.View.Components;

public partial class DbStructureTree : UserControl
{
    public DbStructureTree()
    {
        InitializeComponent();
    }
    private void OnTreeItemSelected(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is TreeView treeView)
        {
            var selectedItem = treeView.SelectedItem;
            if (selectedItem != null)
            {
                if (DataContext is DbStructureTreeViewModel vm)
                    vm.SelectItem(selectedItem);
            }
        }
    }
}