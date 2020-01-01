using System;
using System.Data;
using System.Data.Common;

namespace Net.FreeORM.Framework.DBConnection
{
    internal sealed class NsConnection : DbConnection
    {
        #region [ Private Fields ]

        private DbConnection dbConn = null;
        private ConnectionTypes _connType = ConnectionTypes.SqlServer;
        private DbProviderFactory _fact = null;

        #endregion


        #region [ NsConnection Ctors ]

        public NsConnection()
            : this(ConnectionTypes.SqlServer, string.Empty)
        { }


        public NsConnection(string connectionString)
            : this(ConnectionTypes.SqlServer, connectionString)
        { }

        public NsConnection(ConnectionTypes connType)
            : this(connType, string.Empty)
        { }

        public NsConnection(ConnectionTypes connType, string connectionString)
        {
            try
            {
                _connType = connType;

                _fact = GetFactory(_connType);

                dbConn = GetConn();

                if (string.IsNullOrWhiteSpace(connectionString) == false)
                    dbConn.ConnectionString = connectionString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------- */

        #region [ Open method ]

        /// <summary>
        /// Opens Database Connection.
        /// </summary>
        public override void Open()
        {
            try
            {
                dbConn.Open();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Close method ]

        /// <summary>
        /// Closes Connection with database.
        /// </summary>
        public override void Close()
        {
            try
            {
                dbConn.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ BeginDbTransaction method ]

        /// <summary>
        /// Starts a database database transaction with specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">Specifies the isolation level for transaction.</param>
        /// <returns></returns>
        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                return dbConn.BeginTransaction(isolationLevel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ ChangeDatabase method ]

        /// <summary>
        /// Changes Database Name of NsConnection.
        /// </summary>
        /// <param name="databaseName">Database Name</param>
        public override void ChangeDatabase(string databaseName)
        {
            try
            {
                dbConn.ChangeDatabase(databaseName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ CreateDbCommand method ]

        /// <summary>
        /// Creates DbCommand object.
        /// </summary>
        /// <returns>Returns DbCommand object instance.</returns>
        protected override DbCommand CreateDbCommand()
        {
            try
            {
                return dbConn.CreateCommand();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetFactory method ]

        private DbProviderFactory GetFactory(ConnectionTypes connType)
        {
            try
            {
                string factory_invariant_name = GetProviderName(connType);
                DbProviderFactory fact = DbProviderFactories.GetFactory(factory_invariant_name);
                return fact;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetProviderName method ]

        private string GetProviderName(ConnectionTypes connType)
        {
            try
            {
                string providerName = string.Empty;
                switch (connType)
                {
                    case ConnectionTypes.SqlExpress:
                    case ConnectionTypes.SqlServer:
                        providerName = "System.Data.SqlClient";
                        break;

                    case ConnectionTypes.PostgreSQL:
                        providerName = "Npgsql";
                        break;

                    case ConnectionTypes.DB2:
                        providerName = "IBM.Data.DB2";
                        break;

                    case ConnectionTypes.OracleNet:
                        providerName = "System.Data.OracleClient";
                        break;

                    case ConnectionTypes.MySQL:
                    case ConnectionTypes.MariaDB:
                        providerName = "MySql.Data.MySqlClient";
                        break;

                    case ConnectionTypes.VistaDB:
                        providerName = "System.Data.VistaDB5";//providerName = "VistaDB.Provider";
                        break;

                    case ConnectionTypes.OleDb:
                        providerName = "System.Data.OleDb";
                        break;

                    case ConnectionTypes.SQLite:
                        providerName = "System.Data.SQLite";
                        break;

                    case ConnectionTypes.FireBird:
                        providerName = "FirebirdSql.Data.FirebirdClient";
                        break;

                    case ConnectionTypes.SqlServerCe:
                        providerName = "System.Data.SqlServerCe";
                        break;

                    case ConnectionTypes.Sybase:
                        providerName = "Sybase.Data.AseClient";
                        break;

                    case ConnectionTypes.Informix:
                        providerName = "IBM.Data.Informix";
                        break;

                    case ConnectionTypes.U2:
                        providerName = "U2.Data.Client";
                        break;

                    case ConnectionTypes.Synergy:
                        providerName = "Synergex.Data.SynergyDBMSClient";
                        break;

                    case ConnectionTypes.Ingres:
                        providerName = "Ingres.Client";
                        break;


                    case ConnectionTypes.SqlBase:
                        providerName = "Gupta.SQLBase.Data";//"Unify.SQLBase.Data";
                        break;

                    case ConnectionTypes.Odbc:
                        providerName = "System.Data.Odbc";
                        break;

                    case ConnectionTypes.OracleManaged:
                        providerName = "Oracle.ManagedDataAccess.Client";
                        break;

                    default:
                        break;
                }

                return providerName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetConn method ]

        private DbConnection GetConn()
        {
            try
            {
                return _fact.CreateConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetDataAdapter method ]

        /// <summary>
        /// Creates DbDataAdapter Object with specified NsConnectionType.
        /// </summary>
        /// <returns>DbDataAdapter object Instance.</returns>
        public DbDataAdapter GetDataAdapter()
        {
            try
            {
                return _fact.CreateDataAdapter();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------- */

        #region [ ConnectionString Prpoerty ]

        /// <summary>
        /// Gets, Sets Connection String Of NsConnection.
        /// </summary>
        public override string ConnectionString
        {
            get
            {
                return dbConn.ConnectionString;
            }
            set
            {
                dbConn.ConnectionString = value;
            }
        }

        #endregion


        #region [ DataSource Property ]

        /// <summary>
        /// Gets DataSource Name Of NsConnection.
        /// </summary>
        public override string DataSource
        {
            get
            {
                try
                {
                    return dbConn.DataSource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion


        #region [ Database Property ]
        /// <summary>
        /// Gets Database Name Of NsConnection.
        /// </summary>
        public override string Database
        {
            get
            {
                try
                {
                    return dbConn.Database;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion


        #region [ ServerVersion Property ]

        /// <summary>
        /// Gets Version Of Database Server.
        /// </summary>
        public override string ServerVersion
        {
            get
            {
                try
                {
                    return dbConn.ServerVersion;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion


        #region [ State Property ]

        /// <summary>
        /// Gets Connection State Of Database Connection.
        /// </summary>
        public override ConnectionState State
        {
            get
            {
                try
                {
                    return dbConn.State;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        #endregion


        #region [ ConnectionType Property ]

        /// <summary>
        /// Gets ConnectionType Of NsConnection.
        /// </summary>
        public ConnectionTypes ConnectionType
        {
            get
            { return _connType; }
        }

        #endregion

    }
}
