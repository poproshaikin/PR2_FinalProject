using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using PR2_FinalProject.View.Components;
using PR2_FinalProject.View.Windows;
using PR2_FinalProject.ViewModel.Db;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class MainViewModel : ReactiveObject
{
    private readonly Window _owner;
    public DbStructureTreeViewModel DbStructureTreeVm { get; set; }
    public UserControl CurrentControl { get; set; }
    
    public MainViewModel(Window owner)
    {
        _owner = owner;
        DbStructureTreeVm = new DbStructureTreeViewModel();
        DbStructureTreeVm.OnItemSelected += HandleItemSelected;
        CurrentControl = new UserControl();
    }

    public async Task OpenConnectionDialogAsync()
    {
        var vm = new ConnectionSettingsWindowViewModel();
        vm.OnConnectionEstablished += (_, _) =>
        {
            DbStructureTreeVm.LoadSchemas();
        };
        
        var window = new ConnectionSettingsWindow()
        {
            DataContext = vm
        };
        
        vm.DialogCloser = window;

        await window.ShowDialog(_owner);
    }
        
    private void HandleItemSelected(object selectedItem)
    {
        if (selectedItem is DbTreeTableViewModel tableVm)
        {
            var tableDescriptionVm = new TableDescriptionViewModel(tableVm);
            CurrentControl = new TableDescription
            {
                DataContext = tableDescriptionVm
            };
        }
        else if (selectedItem is DbTreeColumnViewModel columnVm)
        {
            var columnDescriptionVm = new ColumnDescriptionViewModel(columnVm);
            CurrentControl = new ColumnDescription
            {
                DataContext = columnDescriptionVm
            };
        }
        else
        {
            CurrentControl = null; // или какой-то дефолт
        }
        
        this.RaisePropertyChanged(nameof(CurrentControl));
    }
}