namespace Net.FreeORM.Framework.DBConnection
{
    using System;
    using System.Data;

    internal interface IConnection : IDisposable, IConnectionOperations
    {
        void OpenConnection();

        void CloseConnection();

        ConnectionState GetConnectionState();

        void CommitTransaction();

        void RollbackTransaction();

        void CreateTransaction();

        ConnectionTypes ConnectionType { get; set; }

        string ConnectionString { get; set; }

    }
}