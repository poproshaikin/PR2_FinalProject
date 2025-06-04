using Avalonia;
using Avalonia.Controls;

namespace PR2_FinalProject.View.Components;

public partial class SchemaCard : UserControl
{  
    public static readonly StyledProperty<string> SchemaCardProperty =
        AvaloniaProperty.Register<SchemaCard, string>(nameof(SchemaCard)); 
}