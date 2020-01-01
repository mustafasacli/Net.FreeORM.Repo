using Net.FreeORM.CodeGeneration.Source.Connect;
using Net.FreeORM.CodeGeneration.Source.Enumeration;
using Net.FreeORM.CodeGeneration.Source.QO;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;

namespace Net.FreeORM.CodeGeneration.Source.DL
{
    internal class GeneratorDL
    {

        #region [ GetSqlServerTables method ]

        public DataTable GetSqlServerTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.SqlServer, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.SqlServer);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetMySQLAndMariaDbTables method ]

        public DataTable GetMySQLAndMariaDbTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.MySQL, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = string.Format(Crud.GetTablesAndColumns(ConnectionTypes.MySQL), freeConn.Database);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetPostgreSQL method ]

        public DataTable GetPostgreSQL(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.PostgreSQL, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.PostgreSQL);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetOracleTablesM method ]

        public DataTable GetOracleTablesM(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.OracleManaged, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.OracleManaged);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetOracleTablesN method ]

        public DataTable GetOracleTablesN(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.OracleNet, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.OracleNet);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetVistaDBTables method ]

        public DataTable GetVistaDBTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.VistaDB, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.VistaDB);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetOledbSet method ]

        public DataSet GetOledbSet(string connectionString)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OleDbConnection oledbConn = new OleDbConnection(connectionString))
                {
                    oledbConn.Open();

                    DataTable dt = oledbConn.GetSchema("TABLES");
                    DataView dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc";
                    dt = dV.ToTable();

                    ds.Tables.Add(dt);

                    dt = new DataTable();
                    dt = oledbConn.GetSchema("COLUMNS");
                    dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc, ORDINAL_POSITION Asc";
                    dt = dV.ToTable();
                    ds.Tables.Add(dt);

                    oledbConn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        #endregion


        #region [ GetOdbcTable method ]

        public DataSet GetOdbcTable(string connectionString)
        {
            DataSet ds = new DataSet();
            try
            {
                using (OdbcConnection odbcConn = new OdbcConnection(connectionString))
                {
                    odbcConn.Open();
                    DataTable dt = odbcConn.GetSchema("TABLES");
                    DataView dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc";
                    dt = dV.ToTable();

                    ds.Tables.Add(dt);

                    dt = new DataTable();
                    dt = odbcConn.GetSchema("COLUMNS");
                    dV = dt.DefaultView;
                    dV.Sort = "TABLE_NAME Asc, ORDINAL_POSITION Asc";
                    dt = dV.ToTable();
                    ds.Tables.Add(dt);

                    odbcConn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return ds;
        }

        #endregion


        #region [ SQLite Methods ]

        #region [ GetSqliteTables method ]

        public DataTable GetSqliteTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.SQLite, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesQuery(ConnectionTypes.SQLite);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion

        #region [ GetSqliteColumns method ]

        public DataTable GetSqliteColumns(string connectionString, string tableName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.SQLite, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = string.Format(Crud.GetColumnsOfTablesQuery(ConnectionTypes.SQLite), tableName);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion

        #endregion


        #region [ GetSqlBaseTableSet method ]

        public DataSet GetSqlBaseTableSet(string connectionString)
        {
            DataSet _ds = new DataSet();
            try
            {

            }
            catch (Exception)
            {
                throw;
            }
            return _ds;
        }

        #endregion


        #region [ GetInformixTable method ]

        public DataTable GetInformixTable(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {

                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.Informix, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.Informix);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetDB2Tables method ]

        public DataTable GetDB2Tables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {

                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.DB2, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.DB2);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetFireBirdTables method ]

        public DataTable GetFireBirdTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {

                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.FireBird, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.FireBird);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


        #region [ GetSybaseTables method ]

        public DataTable GetSybaseTables(string connectionString)
        {
            DataTable dt = new DataTable();
            try
            {
                using (FreeConnection freeConn = new FreeConnection(ConnectionTypes.Sybase, connectionString))
                {
                    using (DbCommand dbCmd = freeConn.CreateCommand())
                    {
                        dbCmd.CommandText = Crud.GetTablesAndColumns(ConnectionTypes.Sybase);
                        dbCmd.CommandType = CommandType.Text;
                        using (DbDataAdapter adapter = freeConn.GetDataAdapter())
                        {
                            freeConn.Open();
                            adapter.SelectCommand = dbCmd;
                            adapter.Fill(dt);
                            freeConn.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }

        #endregion


    }
}