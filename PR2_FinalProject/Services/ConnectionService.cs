using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using Dapper;
using Npgsql;
using PR2_FinalProject.Model;
using PR2_FinalProject.ViewModel;
using PR2_FinalProject.ViewModel.Db;

namespace PR2_FinalProject.Services;

public class ConnectionService
{
    private readonly DbCache _cache = new();
    
    public string? ConnectionString { get; set; }

    public SupportedDb? ConnectedDb { get; set; }

    public DbConnection? CurrentConnection { get; set; }
    
    public bool IsConnected =>
        !string.IsNullOrEmpty(ConnectionString) && CurrentConnection is { State: ConnectionState.Open };


    public bool TryCreateConnection(string connectionString, SupportedDb db, out DbConnection connection)
    {
        if (IsConnected)
            CurrentConnection!.Close();

        DbConnection cnn;

        try
        {
            cnn = GetConnection(connectionString, db);
            cnn.Open();
            Logger.LogAsync($"Opened connection: {cnn.DataSource}");
        }
        catch (Exception ex)
        {
            Logger.LogAsync(ex.Message);
            connection = null!;
            return false;
        }

        connection = cnn;
        return true;
    }

    private DbConnection GetConnection(string connectionString, SupportedDb db)
    {
        return db switch
        {
            SupportedDb.PostgreSQL => new NpgsqlConnection(connectionString),
            _ => throw new NotSupportedException("This database is not supported yet.")
        };
    }

    public SchemaViewModel[] LoadSchemas()
    {
        if (!IsConnected) 
        {
            throw new InvalidOperationException("Connection was not initialized");
        }

        var cached = _cache.GetSchemas();

        if (cached is not null)
            return cached;

        var query = """
                    SELECT 
                        table_schema AS SchemaName,
                        table_name AS TableName,
                        column_name AS ColumnName,
                        data_type AS DataType,
                        is_nullable AS IsNullable,
                        column_default AS DefaultValue
                    FROM information_schema.columns
                    WHERE table_schema NOT IN ('pg_catalog', 'information_schema')
                    ORDER BY table_schema, table_name, ordinal_position;
                    """;

        var result =
            CurrentConnection.Query<(
                string SchemaName, 
                string TableName, 
                string ColumnName, 
                string DataType,
                string IsNullable,
                string DefaultValue
                )>(query);
        
        var schemaDict = new Dictionary<string, SchemaViewModel>();
        
        Logger.LogAsync($"Rows count: {result.Count()}");

        foreach (var row in result)
        {
            Logger.LogAsync($"Schema loading row -> {row.SchemaName} -> {row.TableName} -> {row.ColumnName} -> {row.DataType}");
            
            if (!schemaDict.TryGetValue(row.SchemaName, out var schemaVm))
            {
                schemaDict[row.SchemaName] = schemaVm = new SchemaViewModel { Name = row.SchemaName, };
            }

            var tableVm = schemaVm.Tables.FirstOrDefault(t => t.Name == row.TableName) ??
                          new DbTreeTableViewModel
                          {
                              Name = row.TableName,
                              Schema = row.SchemaName,
                              // TODO: avoid inserting a table and schema name directly to avoid SQL injection
                              RowCount = CurrentConnection.ExecuteScalar<int>($"SELECT COUNT(*) FROM \"{row.SchemaName}\".\"{row.TableName}\"")
                          };

            tableVm.Columns.Add(new DbTreeColumnViewModel
            {
                Name = row.ColumnName,
                DataType = row.DataType,
                IsNullable = row.IsNullable == "YES",
                DefaultValue = row.DefaultValue
            });

            if (schemaVm.Tables.All(t => t.Name != row.TableName))
            {
                schemaVm.Tables.Add(tableVm);
            }
        }
        
        return schemaDict.Values.ToArray();
    }

    public TableViewModel LoadTable(string schemaName, string tableName, bool withPlaceholder = false)
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("Connection was not initialized");
        }

        var table = _cache.GetTable(schemaName, tableName);
        if (table is not null)
            return table;

        var query = $"SELECT * FROM \"{schemaName}\".\"{tableName}\"";
        var data = CurrentConnection!.Query(query).ToList();

        var result = new TableViewModel(new ObservableCollection<dynamic>(data));
            
        return result;
    }
}