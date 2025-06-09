using PR2_FinalProject.ViewModel;
using PR2_FinalProject.ViewModel.Db;

namespace PR2_FinalProject.Services;

public class DbCache
{
    private readonly struct TableCacheEntry
    {
        private static readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(5);
    
        public string SchemaName { get; }
        public string TableName { get; }
        public TableViewModel Table { get; }
        public DateTime Created { get; }
    
        public bool IsExpired => Created.Add(_defaultExpiration) > DateTime.Now;

        public TableCacheEntry(string schemaName,string tableName, TableViewModel table)
        {
            SchemaName = schemaName;
            TableName = tableName;
            Table = table;
            Created = DateTime.Now;
        }
    }

    private readonly struct SchemasListCacheEntry(SchemaViewModel[] schemas)
    {
        private static readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(5);
        
        public SchemaViewModel[] Schemas { get; } = schemas;
        public DateTime Created { get; } = DateTime.Now;
        public bool IsExpired => Created.Add(_defaultExpiration) > DateTime.Now;
    }
    
    private SchemasListCacheEntry? _schemasListCache;
    
    private readonly Dictionary<(string SchemaName, string TableName), TableCacheEntry> _tableCache = new();

    public void CacheSchemas(SchemaViewModel[] schemas)
    {
        _schemasListCache = new SchemasListCacheEntry(schemas);
    }

    public SchemaViewModel[]? GetSchemas()
    {
        if (_schemasListCache is null)
            return null;

        if (_schemasListCache.Value.IsExpired)
        {
            _schemasListCache = null;
            return null;
        }
        
        return _schemasListCache.Value.Schemas;
    }
    
    public TableViewModel? GetTable(string schemaName, string tableName)
    {
        if (!_tableCache.TryGetValue((schemaName, tableName), out var entry))
        {
            return null;
        }

        if (entry.IsExpired)
        {
            _tableCache.Remove((schemaName, tableName));
            return null;
        }
        
        return entry.Table;
    }

    public void AddTable(string schemaName,string tableName, TableViewModel table)
    {
        _tableCache[(schemaName, tableName)] = new TableCacheEntry(schemaName, tableName, table);
    }

    public bool TryGetTable(string schemaName, string tableName, out TableViewModel table)
    {
        table = GetTable(schemaName, tableName)!;
        return table != null!;
    }

    public void InvalidateTable(string schemaName, string tableName)
    {
        _tableCache.Remove((schemaName, tableName));
    }
}