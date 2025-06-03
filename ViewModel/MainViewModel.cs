using System.Collections.ObjectModel;

namespace ViewModel;

public class MainViewModel
{
    public ObservableCollection<SchemaViewModel> Schemas { get; set; }

    public bool ConnectionStringInitialized { get; set; }
    
    public bool Connected { get; set; }
    
    public MainViewModel()
    {
        Schemas = [];
    }

    public void OpenConnectionSettingsWindow()
    {
        
    }
}