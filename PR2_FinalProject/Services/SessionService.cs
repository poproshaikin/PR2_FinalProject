using System.Collections.Generic;
using System.Data.Common;
using PR2_FinalProject.Model;
using Services;

namespace PR2_FinalProject.Services;

public class SessionService
{
    private ConnectionService _connectionService { get; }
    
    public bool IsConnected => _connectionService.IsConnected;
    public DbConnection? Connection => _connectionService.CurrentConnection;
    public ConnectionService ConnectionService => _connectionService;
    public TableCachingService Cache { get; set; } = new();

    public SessionService()
    {
        _connectionService = new ConnectionService();
    }

    public bool Connect(string connectionString, SupportedDb db)
    {
        bool success = _connectionService.TryCreateConnection(connectionString, db, out DbConnection connection);

        if (!success) return false;

        _connectionService.CurrentConnection = connection;
        _connectionService.ConnectionString = connectionString;
        return true;
    }
}