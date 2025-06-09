using Avalonia;
using Avalonia.Controls;

namespace PR2_FinalProject.Misc;

public static class GridHelper
{
    public static readonly AttachedProperty<int> ColumnCountProperty =
        AvaloniaProperty.RegisterAttached<Grid, int>("ColumnCount", typeof(GridHelper));

    static GridHelper()
    {
        ColumnCountProperty.Changed.Subscribe(OnColumnCountChanged);
    }

    public static int GetColumnCount(AvaloniaObject obj) =>
        obj.GetValue(ColumnCountProperty);

    public static void SetColumnCount(AvaloniaObject obj, int value) =>
        obj.SetValue(ColumnCountProperty, value);

    private static void OnColumnCountChanged(AvaloniaPropertyChangedEventArgs<int> e)
    {
        if (e.Sender is Grid grid)
        {
            var count = e.NewValue.Value;

            grid.ColumnDefinitions.Clear();

            for (int i = 0; i < count; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
            }
        }
    }
}
