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
    
    public ReactiveCommand<Unit, Unit> OpenConnectionDialogCommand { get; }

    public MainViewModel(Window owner)
    {
        OpenConnectionDialogCommand = ReactiveCommand.CreateFromTask(OpenConnectionDialogAsync);
        _owner = owner;
    }

    public async Task OpenConnectionDialogAsync()
    {
        var vm = new ConnectionSettingsWindowViewModel();
        var window = new ConnectionSettingsWindow()
        {
            DataContext = vm
        };
        vm.DialogCloser = window;

        await Dispatcher.UIThread.InvokeAsync(() => window.ShowDialog(_owner));
    }
}