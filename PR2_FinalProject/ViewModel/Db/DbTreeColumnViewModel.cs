namespace PR2_FinalProject.ViewModel.Db;

public class DbTreeColumnViewModel
{
    public string Name { get; set; }
    public string DataType { get; set; }
    public bool IsNullable { get; set; }
    public bool IsPrimaryKey { get; set; }
    public bool IsForeignKey { get; set; }
    public string? DefaultValue { get; set; }

    public string DisplayName => $"c: {Name}";
}