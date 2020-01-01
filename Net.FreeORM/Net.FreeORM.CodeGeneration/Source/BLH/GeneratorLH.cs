using Net.FreeORM.CodeGeneration.Source.BO;
using Net.FreeORM.CodeGeneration.Source.DL;
using Net.FreeORM.CodeGeneration.Source.Enumeration;
using Net.FreeORM.CodeGeneration.Source.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Net.FreeORM.CodeGeneration.Source.BLH
{
    internal class GeneratorLH
    {
        private GeneratorDL generatorDL;

        #region [ GeneratorLH Ctor ]

        public GeneratorLH()
        {
            generatorDL = new GeneratorDL();
        }

        #endregion

        #region [ GetTablesAndColumns method ]

        public List<Table> GetTablesAndColumns(ConnectionTypes connectionType, string connectionString)
        {
            List<Table> tables = new List<Table>();

            try
            {
                switch (connectionType)
                {
                    default:
                        break;

                    case ConnectionTypes.SqlExpress:
                    case ConnectionTypes.SqlServer:
                    case ConnectionTypes.SqlServerCe:
                        tables = GetSqlServerTables(connectionString);
                        break;

                    case ConnectionTypes.PostgreSQL:
                        tables = GetPostgreSQLTables(connectionString);
                        break;

                    case ConnectionTypes.DB2:
                        GetDB2Tables(connectionString);
                        break;

                    case ConnectionTypes.OracleNet:
                        tables = GetOracleTablesN(connectionString);
                        break;

                    case ConnectionTypes.OracleManaged:
                        tables = GetOracleTablesM(connectionString);
                        break;

                    case ConnectionTypes.MySQL:
                    case ConnectionTypes.MariaDB:
                        tables = GetMySQLAndMariaDbTables(connectionString);
                        break;

                    case ConnectionTypes.VistaDB:
                        tables = GetVistaDBTables(connectionString);
                        break;

                    case ConnectionTypes.OleDb:
                        tables = GetOledbTable(connectionString);
                        break;

                    case ConnectionTypes.SQLite:
                        tables = GetSqliteTables(connectionString);
                        break;

                    case ConnectionTypes.FireBird:
                        tables = GetFireBirdTables(connectionString);
                        break;

                    case ConnectionTypes.Sybase:
                        tables = GetSybaseTables(connectionString);
                        break;

                    case ConnectionTypes.Informix:
                        tables = GetInformixTables(connectionString);
                        break;

                    case ConnectionTypes.U2:
                        break;

                    case ConnectionTypes.Synergy:
                        break;

                    case ConnectionTypes.Ingres:
                        break;

                    case ConnectionTypes.SqlBase:
                        tables = GetSqlBaseTables(connectionString);
                        break;

                    case ConnectionTypes.Odbc:
                        tables = GetOdbcTable(connectionString);
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return tables;
        }

        #endregion

        #region [ GetSqlServerTables method ]

        private List<Table> GetSqlServerTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetSqlServerTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["COLUMN_NAME"].ToString(), rw["DATA_TYPE"].ToString()));
                    }

                    tbl.TableColumns = columns;

                    rows = null;
                    rows = dt.Select(string.Format("TABLE_NAME='{0}' AND IdentityState=1", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                        break;
                    }

                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetPostgreSQLTables method ]

        private List<Table> GetPostgreSQLTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetPostgreSQL(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "table_name");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("table_name='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["column_name"].ToString(), rw["udt_name"].ToString()));
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND udt_name LIKE '%int%' AND column_default LIKE '%nextval%'", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["column_name"].ToString();
                        break;
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetMySQLAndMariaDbTables method ]

        private List<Table> GetMySQLAndMariaDbTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetMySQLAndMariaDbTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["COLUMN_NAME"].ToString(), rw["DATA_TYPE"].ToString()));
                    }

                    tbl.TableColumns = columns;

                    rows = null;
                    rows = dt.Select(string.Format("TABLE_NAME='{0}' AND EXTRA='auto_increment'", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                        break;
                    }

                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetOracleTablesM method ]

        private List<Table> GetOracleTablesM(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetOracleTablesM(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["COLUMN_NAME"]), string.Format("{0}", row["DATA_TYPE"])));
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                     */
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        #region [ GetOracleTablesN method ]

        private List<Table> GetOracleTablesN(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetOracleTablesN(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["COLUMN_NAME"]), string.Format("{0}", row["DATA_TYPE"])));
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                     */
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        #region [ GetVistaDBTables method ]

        private List<Table> GetVistaDBTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetVistaDBTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "table_name");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("table_name='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["column_name"].ToString(), rw["data_type"].ToString()));
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND is_identity=true", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["column_name"].ToString();
                        break;
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetOledbTable method ]

        private List<Table> GetOledbTable(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataSet ds = new DataSet();
                DataRow[] rows;
                List<string> tables = new List<string>();
                ds = generatorDL.GetOledbSet(connectionString);
                rows = ds.Tables[0].Select("TABLE_TYPE='TABLE'");
                string str_tbl;
                foreach (DataRow rw in rows)
                {
                    str_tbl = string.Format("{0}", rw["TABLE_NAME"]);

                    if (tables.Contains(str_tbl) == false)
                        tables.Add(str_tbl);
                }

                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = ds.Tables[1].Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["COLUMN_NAME"].ToString(), OleDbUtil.GetIntFrom(ConvertUtil.ToInt(rw["DATA_TYPE"].ToString()))));
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = ds.Tables[1].Select(string.Format("TABLE_NAME='{0}' AND DATA_TYPE=3", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                        break;
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetOdbcTable method ]

        private List<Table> GetOdbcTable(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = generatorDL.GetOdbcTable(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(ds.Tables[0], "TABLE_NAME");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                dt = ds.Tables[1];
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column(rw["COLUMN_NAME"].ToString(), rw["TYPE_NAME"].ToString()));
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = dt.Select(string.Format("TABLE_NAME='{0}' AND TYPE_NAME='int identity'", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetSqliteTables method ]

        private List<Table> GetSqliteTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetSqliteTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "tbl_name");
                List<Column> columns;
                Table tbl;
                DataTable dtColumns;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    dtColumns = generatorDL.GetSqliteColumns(connectionString, strTable);

                    foreach (DataRow row in dtColumns.Rows)
                    {
                        columns.Add(new Column(row["name"].ToString(), row["type"].ToString()));
                        if (ConvertUtil.ToInt(string.Format("{0}", row["pk"])) == 1)
                        {
                            tbl.IdColumn = row["name"].ToString();
                        }
                    }
                    tbl.TableColumns = columns;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return tableList;
        }

        #endregion

        #region [ GetInformixTables method ]

        private List<Table> GetInformixTables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetInformixTable(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "tabname");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("tabname='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["colname"]),
                            InformixUtil.GetIntFrom(ConvertUtil.ToInt(string.Format("{0}", row["coltype"])))));
                    }
                    _table.TableColumns = cols;

                    rows = null;
                    rows = dt.Select(string.Format("tabname='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        #region [ GetDB2Tables method ]

        private List<Table> GetDB2Tables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetDB2Tables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "tbname");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("tbname='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["name"]), string.Format("{0}", row["coltype"])));
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                     */
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        #region [ GetFireBirdTables method ]

        private List<Table> GetFireBirdTables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetFireBirdTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "Table_Name");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("Table_Name='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["Column_Name"]),
                            FireBirdUtil.GetIntFrom(ConvertUtil.ToInt(string.Format("{0}", row["coltype"])))));
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                     */
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        #region [ GetSybaseTables method ]

        private List<Table> GetSybaseTables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetSybaseTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "tableName");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("tableName='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column(string.Format("{0}", row["columnName"]), string.Format("{0}", row["columnType"])));
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                    _tables.Add(_table);
                    rows = null;
                     */
                }
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion

        // Not Done

        #region [ GetSqlBaseTables method ]

        private List<Table> GetSqlBaseTables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
            }
            catch (Exception)
            {
                throw;
            }
            return _tables;
        }

        #endregion
    }
}