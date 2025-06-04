using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using Npgsql;
using Dapper;
using PR2_FinalProject.Model;
using PR2_FinalProject.Services;
using PR2_FinalProject.ViewModel.Db;

namespace Services;

public class ConnectionService
{
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
            Logger.Log($"Opened connection: {cnn.DataSource}");
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message);
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
        if (
            !IsConnected || 
            CurrentConnection is null || 
            CurrentConnection.State != ConnectionState.Open
        ) {
            throw new InvalidOperationException("Connection was not initialized");
        }
        
        var query = """
                    SELECT 
                        table_schema AS SchemaName,
                        table_name AS TableName,
                        column_name AS ColumnName,
                        data_type AS DataType,
                    FROM information_schema.columns
                    WHERE table_schema NOT IN ('pg_catalog', 'information_schema')
                    ORDER BY table_schema, table_name, ordinal_position;
                    """;

        var result =
            CurrentConnection.Query<(string SchemaName, string TableName, string ColumnName, string DataType)>(query);
        
        var schemaDict = new Dictionary<string, SchemaViewModel>();

        foreach (var row in result)
        {
            Logger.Log($"Schema loading row -> {row.SchemaName} -> {row.TableName} -> {row.ColumnName} -> {row.DataType}");
            
            if (!schemaDict.TryGetValue(row.SchemaName, out var schemaVm))
            {
                schemaDict[row.SchemaName] = schemaVm = new SchemaViewModel { Name = row.SchemaName, };
            }

            var tableVm = schemaVm.Tables.FirstOrDefault(t => t.Name == row.TableName);
            if (tableVm == null)
            {
                tableVm = new TableViewModel
                {
                    Name = row.TableName,
                    RowCount = CurrentConnection.ExecuteScalar<int>("SELECT COUNT(*) FROM @TableName", new { row.TableName })
                };
            }
        
            tableVm.Columns.Add(new ColumnViewModel
            {
                Name = row.ColumnName,
                DataType = row.DataType,
            });
        }
        
        return schemaDict.Values.ToArray();
    }
}