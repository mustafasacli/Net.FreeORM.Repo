using Net.FreeORM.CodeGeneration.Source.Enumeration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Net.FreeORM.CodeGeneration.Source.Connect
{
    internal class FreeConnection : DbConnection
    {
        private ConnectionTypes _ConnectionType;
        private DbProviderFactory _fact = null;
        private DbConnection dbConn = null;

        public FreeConnection()
            : this(ConnectionTypes.SqlServer, string.Empty)
        { }

        public FreeConnection(string connectionString)
            : this(ConnectionTypes.SqlServer, connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public FreeConnection(ConnectionTypes ConnType)
            : this(ConnType, string.Empty)
        {
            _ConnectionType = ConnType;
        }

        public FreeConnection(ConnectionTypes ConnType, string connectionString)
            : base()
        {
            _ConnectionType = ConnType;
            _fact = GetFactory(ConnType);

            dbConn = GetConn();

            if (string.Empty.Equals(connectionString) == false)
                this.ConnectionString = connectionString;
        }

        ~FreeConnection()
        {
            Dispose();
        }

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

        public ConnectionTypes ConnectionType
        {
            get { return _ConnectionType; }
        }

        #region [ GetFactory method ]

        private DbProviderFactory GetFactory(ConnectionTypes connType)
        {
            try
            {
                DbProviderFactory fact = DbProviderFactories.GetFactory(GetProviderName(connType));
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

        public override string Database
        {
            get { return dbConn.Database; }
        }

        public override string DataSource
        {
            get { return dbConn.DataSource; }
        }

        public override string ServerVersion
        {
            get { return dbConn.ServerVersion; }
        }

        public override ConnectionState State
        {
            get { return dbConn.State; }
        }

        public static FreeConnection GetConnection(int index)
        {
            switch (index)
            {
                default:
                case 0:
                case 1:
                    return new FreeConnection(ConnectionTypes.SqlServer);

                case 2:
                    return new FreeConnection(ConnectionTypes.PostgreSQL);
                //return new EnterpriseDBManager();

                case 3:
                    return new FreeConnection(ConnectionTypes.DB2);
                //return new DB2Manager();

                case 4:
                    return new FreeConnection(ConnectionTypes.OracleNet);
                //return new OracleManager();

                case 5:
                    return new FreeConnection(ConnectionTypes.MySQL);
                //return new MySQLManager();

                case 6:
                    return new FreeConnection(ConnectionTypes.OleDb);
                //return new OleDbManager();

                case 7:
                    return new FreeConnection(ConnectionTypes.SQLite);
                //return new SQLiteManager();

                case 8:
                    return new FreeConnection(ConnectionTypes.FireBird);
                //return new FireBirdManager();
            }
        }

        #region [ Create Connection with Connection Type ]

        /// <summary>
        /// Returns a IDbConnection object instance.
        /// </summary>
        /// <param name="conType">Connection Type</param>
        /// <returns>Returns a IDbConnection object instance.</returns>
        protected DbConnection CreateConnection()
        {
            try
            {
                switch (_ConnectionType)
                {
                    case ConnectionTypes.SqlExpress:
                    case ConnectionTypes.SqlServer:
                        return new SqlConnection();

                    case ConnectionTypes.PostgreSQL:
                        return DbProviderFactories.GetFactory("Npgsql").CreateConnection();// throw new NotSupportedException("PostgreSQL Driver is not supported.");

                    case ConnectionTypes.DB2:
                        throw new NotSupportedException("DB2 Driver is not supported.");

                    case ConnectionTypes.OleDb:
                        return new OleDbConnection();

                    case ConnectionTypes.FireBird:
                        throw new NotSupportedException("FireBirdSQL Driver is not supported.");

                    case ConnectionTypes.OracleNet:
                        //   return new OracleConnection();
                        throw new NotSupportedException("Oracle Driver is not supported.");

                    case ConnectionTypes.SQLite:
                        throw new NotSupportedException("SQLite Driver is not supported.");

                    case ConnectionTypes.MariaDB:
                    case ConnectionTypes.MySQL:
                        throw new NotSupportedException("MySQL Driver is not supported.");

                    case ConnectionTypes.VistaDB:
                        throw new NotSupportedException("VistaDB Driver is not supported.");

                    case ConnectionTypes.SqlServerCe:
                        throw new NotSupportedException("SqlServerCe Driver is not supported.");

                    default:
                        throw new NotSupportedException("UnSupported Driver Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [ Create Data Adapter with Connection Type ]

        /// <summary>
        /// Returns a IDataAdapter object instance.
        /// </summary>
        /// <param name="conType">Connection Type</param>
        /// <returns>Returns a IDataAdapter object instance.</returns>
        protected DbDataAdapter CreateAdapter(DbCommand dbCmd)
        {
            try
            {
                switch (_ConnectionType)
                {
                    case ConnectionTypes.SqlExpress:
                    case ConnectionTypes.SqlServer:
                        return new SqlDataAdapter((SqlCommand)dbCmd);

                    case ConnectionTypes.PostgreSQL:
                        return DbProviderFactories.GetFactory("Npgsql").CreateDataAdapter();// throw new NotSupportedException("Npgsql Driver is not supported.");

                    case ConnectionTypes.DB2:
                        throw new NotSupportedException("DB2 Driver is not supported.");

                    case ConnectionTypes.OracleNet:
                        //return new OracleDataAdapter((OracleCommand)dbCmd);
                        throw new NotSupportedException("Oracle Driver is not supported.");

                    case ConnectionTypes.MariaDB:
                    case ConnectionTypes.MySQL:
                        throw new NotSupportedException("MySQL Driver is not supported.");

                    case ConnectionTypes.OleDb:
                        return new OleDbDataAdapter((OleDbCommand)dbCmd);

                    case ConnectionTypes.SQLite:
                        throw new NotSupportedException("SQLite Driver is not supported.");

                    case ConnectionTypes.FireBird:
                        throw new NotSupportedException("FireBirdSQL Driver is not supported.");

                    case ConnectionTypes.VistaDB:
                        throw new NotSupportedException("VistaDB Driver is not supported.");

                    case ConnectionTypes.SqlServerCe:
                        throw new NotSupportedException("SqlServerCe Driver is not supported.");

                    default:
                        throw new NotSupportedException("UnSupported Driver Type");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [ Get Result Set ]

        public DataSet GetResultSet(string query)
        {
            try
            {
                DataSet ds = new DataSet();
                using (DbConnection _dbconn = CreateConnection())
                {
                    _dbconn.ConnectionString = this.ConnectionString;
                    using (DbCommand dbCmd = _dbconn.CreateCommand())
                    {
                        dbCmd.CommandText = query;
                        dbCmd.CommandType = CommandType.Text;
                        _dbconn.Open();
                        using (DbDataAdapter dbAdapter = CreateAdapter(dbCmd))
                        {
                            dbAdapter.SelectCommand = dbCmd;
                            dbAdapter.Fill(ds);
                        }
                        _dbconn.Close();
                    }
                }

                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            System.GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

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
    }
}