using System.Data;
using System.Data.Common;
using Model;

namespace Services;

public class SessionService
{
    private ConnectionService _connectionService { get; }

    public SessionService()
    {
        _connectionService = new ConnectionService();
    }

    public bool Connect(string connectionString, SupportedDb db)
    {
        bool success = _connectionService.TryCreateConnection(connectionString, db, out DbConnection connection);

        if (!success) return false;

        _connectionService.CurrentConnection = connection;
        return true;
    }
}