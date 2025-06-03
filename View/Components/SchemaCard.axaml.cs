using Avalonia;
using Avalonia.Controls;

namespace View.Components;

public partial class SchemaCard : UserControl
{  
    public static readonly StyledProperty<string> SchemaCardProperty =
        AvaloniaProperty.Register<SchemaCard, string>(nameof(SchemaCard)); 
}