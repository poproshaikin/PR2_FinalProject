using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using PR2_FinalProject.View.Windows;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class MainViewModel
{
    private readonly Window _owner;
    public DbStructureTreeViewModel DbStructureTreeVm { get; set; }
    
    public MainViewModel(Window owner)
    {
        _owner = owner;
        DbStructureTreeVm = new DbStructureTreeViewModel();
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
}