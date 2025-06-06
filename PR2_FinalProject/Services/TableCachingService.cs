using System;
using System.Collections.Generic;
using PR2_FinalProject.ViewModel;

namespace PR2_FinalProject.Services;

public readonly struct TableCacheEntry
{
    private static readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(5);
    
    public string TableName { get; }
    public DataTableViewModel Table { get; }
    public DateTime Created { get; }
    
    public bool IsExpired => Created.Add(_defaultExpiration) > DateTime.Now;

    public TableCacheEntry(string tableName, DataTableViewModel table)
    {
        TableName = tableName;
        Table = table;
        Created = DateTime.Now;
    }
}

public class TableCachingService
{
    private readonly Dictionary<string, TableCacheEntry> _tableCache = new();

    public DataTableViewModel? GetTable(string tableName)
    {
        if (!_tableCache.TryGetValue(tableName, out var entry))
        {
            return null;
        }

        if (entry.IsExpired)
        {
            _tableCache.Remove(tableName);
            return null;
        }
        
        return entry.Table;
    }

    public void AddTable(string tableName, DataTableViewModel table)
    {
        _tableCache[tableName] = new TableCacheEntry(tableName, table);
    }
}