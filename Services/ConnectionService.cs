using System.Data;
using System.Data.Common;
using Model;
using Npgsql;

namespace Services;

public class ConnectionService
{
    public string? ConnectionString { get; set; }
    
    public SupportedDb? ConnectedDb { get; set; }

    public DbConnection? CurrentConnection { get; set; }
    
    public bool IsConnected => !string.IsNullOrEmpty(ConnectionString) && CurrentConnection is { State: ConnectionState.Open };


    public bool TryCreateConnection(string connectionString, SupportedDb db, out DbConnection connection)
    {
        if (IsConnected)
            CurrentConnection!.Close();

        DbConnection cnn;
        
        try
        {
            cnn = GetConnection(connectionString, db);
            cnn.Open();
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
}