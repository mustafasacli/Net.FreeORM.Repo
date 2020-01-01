using System;
using System.Data.Common;

namespace Net.FreeORM.Framework.DBConnection
{
    internal class ExternalConnection : IConnection
    {
        private ExternalDbConnection dbConn = null;
        private DbTransaction dbTrans;

        private string _ConnString = string.Empty;

        public virtual void OpenConnection()
        {
            throw new NotImplementedException();
        }

        public void CloseConnection()
        {
            throw new NotImplementedException();
        }

        public System.Data.ConnectionState GetConnectionState()
        {
            throw new NotImplementedException();
        }

        public void CommitTransaction()
        {
            throw new NotImplementedException();
        }

        public void RollbackTransaction()
        {
            throw new NotImplementedException();
        }

        public void CreateTransaction()
        {
            throw new NotImplementedException();
        }

        public ConnectionTypes ConnectionType
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ConnectionString
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetResultSetOfQuery(string query)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetResultSetOfQuery(string query, Property parameters)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetResultSetOfProcedure(string procedure)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetResultSetOfProcedure(string procedure, Property parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteQuery(string query)
        {
            throw new NotImplementedException();
        }

        public int ExecuteQuery(string query, Property parameters)
        {
            throw new NotImplementedException();
        }

        public int ExecuteProcedure(string procedure)
        {
            throw new NotImplementedException();
        }

        public int ExecuteProcedure(string procedure, Property parameters)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarAsQuery(string query)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarAsQuery(string query, Property parameters)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarAsProcedure(string procedure)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalarAsProcedure(string procedure, Property parameters)
        {
            throw new NotImplementedException();
        }
    }
}