using System.Reactive;
using PR2_FinalProject.Services;
using ReactiveUI;

namespace PR2_FinalProject.ViewModel;

public class QueryConsoleViewModel : ReactiveObject
{
    public ReactiveCommand<Unit, Unit> RunQueryCommand { get; set; }
    public ReactiveCommand<Unit, string> ClearQueryCommand { get; set; }

    private string _query;
    public string Query
    {
        get => _query;
        set
        {
            if (_query != value)
            {
                _query = value;
                this.RaisePropertyChanged();
            }
        }
    }

    private string _output;
    public string Output
    {
        get => _output;
        set
        {
            if (_output != value)
            {
                _output = value;
                this.RaisePropertyChanged();
            }
        }
    }

    public QueryConsoleViewModel()
    {
        RunQueryCommand = ReactiveCommand.Create(RunQuery);
        ClearQueryCommand = ReactiveCommand.Create(() => Query = string.Empty);    
    }

    private void RunQuery()
    {
        Logger.Log($"Starting executing query: {Query}");
    }
}