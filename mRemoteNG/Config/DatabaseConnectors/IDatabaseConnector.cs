using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace mRemoteNG.Config.DatabaseConnectors;

public interface IDatabaseConnector : IDisposable
{
    bool IsConnected { get; }
    DbConnection DbConnection();
    DbCommand DbCommand(string dbCommand);
    void Connect();
    Task ConnectAsync();
    void Disconnect();
    void AssociateItemToThisConnector(DbCommand dbCommand);
}