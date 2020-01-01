namespace Net.FreeORM.Framework.BaseDal
{
    using Net.FreeORM.Framework.Base;
    using Net.FreeORM.Framework.DBConnection;
    using Net.FreeORM.Framework.Extensions;
    using Net.FreeORM.Framework.QueryBuilding;
    using Net.FreeORM.Framework.Util;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Reflection;
    using System.Linq;
    using Net.FreeORM.Framework.Core;

    public abstract class BaseDL : IBaseDL
    {
        #region [ private fields ]

        private IConnection Conn = null;
        private string _ConnStr;
        private PropertyUtil propUtil;
        private ConnectionTypes _driverType;
        private string _connName = "main";
        protected QueryBuilder queryBuilder = null;

        #endregion


        #region [ BaseDL ctors ]

        /// <summary>
        /// protected ctor of BaseDL.
        /// </summary>
        protected BaseDL()
            : this("main")
        {
        }

        /// <summary>
        /// protected ctor of BaseDL.
        /// </summary>
        /// <param name="connectionName">connection property name</param>
        protected BaseDL(string connectionName)
        {
            _connName = connectionName;
            propUtil = ConfUtil.BuildProperty(ConnectionName);
            _driverType = propUtil.DriverType;
            _ConnStr = propUtil.PropertyString;
            Conn = new Connection(_driverType, _ConnStr);
        }

        /// <summary>
        /// protected ctor of BaseDL.
        /// </summary>
        /// <param name="connectionType">Connection Type.</param>
        /// <param name="connectionString">Connection string.</param>
        protected BaseDL(ConnectionTypes connectionType, string connectionString)
        {
            _driverType = connectionType;
            _ConnStr = connectionString;
            Conn = new Connection(_driverType, _ConnStr);
        }

        #endregion


        #region [ ConnectionName Property ]

        /// <summary>
        /// Gets ConnectionName.
        /// </summary>
        public virtual string ConnectionName
        {
            get { return _connName; }
        }

        #endregion


        #region [ Insert method ]

        /// <summary>
        /// Gets exceution result of Insert Operation.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>returns int</returns>
        public int Insert(BaseBO baseBO)
        {
            try
            {
                queryBuilder =
                     CreateQueryBuilder(QueryTypes.Insert, baseBO);

                return ExecuteQuery(queryBuilder.QueryString, queryBuilder.Properties);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ Insert And Get Id method ]

        /// <summary>
        /// Inserts and Gets Id of BaseBO object.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>returns int</returns>
        public int InsertAndGetId(BaseBO baseBO)
        {
            try
            {
                queryBuilder =
                     CreateQueryBuilder(QueryTypes.InsertAndGetId, baseBO);

                object obj = ExecuteScalarAsQuery(queryBuilder.QueryString, queryBuilder.Properties);

                return obj.ToInt();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ Update method ]

        /// <summary>
        /// Gets exceution result of Update Operation.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>returns int</returns>
        public int Update(BaseBO baseBO)
        {
            try
            {
                queryBuilder =
                    CreateQueryBuilder(QueryTypes.Update, baseBO);

                return ExecuteQuery(queryBuilder.QueryString, queryBuilder.Properties);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ Delete method ]

        /// <summary>
        /// Gets exceution result of Delete Operation.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>returns int</returns>
        public int Delete(BaseBO baseBO)
        {
            try
            {
                queryBuilder = null;
                queryBuilder =
                    CreateQueryBuilder(QueryTypes.Delete, baseBO);

                return ExecuteQuery(queryBuilder.QueryString, queryBuilder.Properties);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* ------------------------------------------- */

        #region [ Insert method ]

        /// <summary>
        /// Insert the record to table with table_name with given fields.
        /// </summary>
        /// <param name="table_name">table name</param>
        /// <param name="fields">column and values</param>
        /// <returns>Returns exec result of Insert.</returns>
        public int Insert(string table_name, Hashmap fields)
        {
            int result = -1;
            try
            {
                if (string.IsNullOrWhiteSpace(table_name))
                {
                    throw new Exception("Table Name can not be null or empty.");
                }

                if (table_name.Contains("drop") || table_name.Contains("--"))
                {
                    throw new Exception(
                            "Table Name can not be contain restricted characters and text.");
                }

                if (fields == null)
                {
                    throw new Exception(
                            "Column list can not be null.");
                }

                if (fields.IsEmpty())
                {
                    throw new Exception(
                            "Column list can not be empty.");
                }

                QueryFormat qf = new QueryFormat(QueryTypes.Insert);
                QueryObject qo = new QueryObject(this._driverType);

                string query = "", cols = "", vals = "";
                //query = qf.Format;
                Property p = Property.Instance;
                FreeParameter fp;
                foreach (var field in fields.Keys())
                {
                    cols = string.Format("{0}, {1}{2}{3}", cols, qo.Prefix, field, qo.Suffix);
                    vals = string.Format("{0}, {1}{2}", cols, qo.ParameterPrefix, field);
                    fp = new FreeParameter
                    {
                        Name = string.Format("{0}{1}", qo.ParameterPrefix, field),
                        Value = fields.Get(field)
                    };
                    p.Put(fp);
                }

                cols = cols.TrimStart(',').TrimStart();
                vals = vals.TrimStart(',').TrimStart();
                query = string.Format(qf.Format, table_name, cols, vals);
                result = this.ExecuteQuery(query, p);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion


        #region [ Update method ]

        /// <summary>
        /// Update records with given parameters.
        /// </summary>
        /// <param name="table_name">table name</param>
        /// <param name="where_column">id column name, if null or empty value will be "id"</param>
        /// <param name="where_value">id column value</param>
        /// <param name="fields">column and values</param>
        /// <returns>Returns exec result of Update.</returns>
        int Update(string table_name, string where_column, object where_value, Hashmap fields)
        {
            int result = -1;

            try
            {
                if (string.IsNullOrWhiteSpace(table_name))
                {
                    throw new Exception("Table Name can not be null or empty.");
                }

                if (table_name.Contains("drop") || table_name.Contains("--"))
                {
                    throw new Exception(
                            "Table Name can not be contain restricted characters and text.");
                }

                if (fields == null)
                {
                    throw new Exception(
                            "Column list can not be null.");
                }

                if (fields.IsEmpty())
                {
                    throw new Exception(
                            "Column list can not be empty.");
                }


                if (string.IsNullOrWhiteSpace(where_column))
                {
                    throw new Exception("Table Name can not be null or empty.");
                }

                if (where_column.Contains("drop") || where_column.Contains("--"))
                {
                    throw new Exception(
                            "Table Name can not be contain restricted characters and text.");
                }


                QueryFormat qf = new QueryFormat(QueryTypes.Update);
                QueryObject qo = new QueryObject(this._driverType);

                string query = "", cols = "", vals = "";
                //query = qf.Format;
                Property p = Property.Instance;
                FreeParameter fp;
                foreach (var field in fields.Keys())
                {
                    cols = string.Format("{0}, {1}{2}{3}={4}{2}", cols, qo.Prefix, field, qo.Suffix, qo.ParameterPrefix);
                    fp = new FreeParameter
                    {
                        Name = string.Format("{0}{1}", qo.ParameterPrefix, field),
                        Value = fields.Get(field)
                    };
                    p.Put(fp);
                }
                fp = new FreeParameter
                {
                    Name = string.Format("{0}{1}", qo.ParameterPrefix, where_column),
                    Value = where_value
                };
                p.Put(fp);

                cols = cols.TrimStart(',').TrimStart();
                vals = string.Format("{0}{1}{2}={3}{1}", qo.Prefix, where_column, qo.Suffix, qo.ParameterPrefix);
                query = string.Format(qf.Format, table_name, cols, vals);
                result = this.ExecuteQuery(query, p);

            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion


        #region [ Delete method ]

        int Delete(string table_name, string where_column, object where_value)
        {
            int result = -1;

            try
            {
                if (string.IsNullOrWhiteSpace(table_name))
                {
                    throw new Exception("Table Name can not be null or empty.");
                }

                if (table_name.Contains("drop") || table_name.Contains("--"))
                {
                    throw new Exception(
                            "Table Name can not be contain restricted characters and text.");
                }


                if (string.IsNullOrWhiteSpace(where_column))
                {
                    throw new Exception("Table Name can not be null or empty.");
                }

                if (where_column.Contains("drop") || where_column.Contains("--"))
                {
                    throw new Exception(
                            "Table Name can not be contain restricted characters and text.");
                }

                QueryFormat qf = new QueryFormat(QueryTypes.Delete);
                QueryObject qo = new QueryObject(this._driverType);

                string query = "", vals = "";
                vals = string.Format("{0}{1}{2}={3}{1}", qo.Prefix, where_column, qo.Suffix, qo.ParameterPrefix);
                query = string.Format(qf.Format, table_name, vals);

                Property p = Property.Instance;
                FreeParameter fp;
                fp = new FreeParameter
                {
                    Name = string.Format("{0}{1}", qo.ParameterPrefix, where_column),
                    Value = where_value
                };
                p.Put(fp);
                result = this.ExecuteQuery(query, p);
            }
            catch (Exception)
            {
                throw;
            }

            return result;
        }

        #endregion

        /* ------------------------------------------- */

        #region [ Get Table method ]

        /// <summary>
        /// Gets All Table Records of BaseBO object.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>Returns a System.Data.DataTable</returns>
        public DataTable GetTable(BaseBO baseBO)
        {
            try
            {
                queryBuilder = CreateQueryBuilder(QueryTypes.Select, baseBO);

                return GetResultSetOfQuery(queryBuilder.QueryString).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ GetById method ]

        /// <summary>
        /// Gets One Record of BaseBO object if any object has BaseBO Id.
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>Returns a System.Data.DataTable</returns>
        public DataTable GetById(BaseBO baseBO)
        {
            try
            {
                queryBuilder =
                    CreateQueryBuilder(QueryTypes.SelectWhereId, baseBO);

                DataTable dT = GetResultSetOfQuery(
                    queryBuilder.QueryString,
                    queryBuilder.Properties).Tables[0];

                return dT;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ GetObjectListByIds method ]

        public List<T> GetObjectListByIds<T>(List<int> idList) where T : new()
        {
            try
            {
                List<T> t_List = null;

                if (idList == null)
                    return t_List;

                t_List = new List<T>();
                if (idList.Count == 0)
                    return t_List;

                T t = (T)Activator.CreateInstance(typeof(T));

                string IdCol = string.Format("{0}", t.GetType().GetMethod("GetIdColumn").Invoke(t, null));
                if (IdCol.Trim().Length == 0)
                    throw new Exception("Id Column can not be empty.");

                string tableName = string.Format("{0}", t.GetType().GetMethod("GetTableName").Invoke(t, null));

                if (tableName.Replace(" ", "").Length == 0)
                    throw new Exception("TableName can not be empty.");

                string strIn = string.Empty;

                foreach (var item in idList)
                {
                    strIn = string.Format("{0}, {1}", strIn, item);
                }
                strIn = strIn.TrimStart(',').TrimStart();

                DataTable dtBO = GetResultSetOfQuery(string.Format("Select * From {0} Where {1} IN ({2})", tableName, IdCol, strIn)).Tables[0];

                t_List = dtBO.ToList<T>(true);

                return t_List;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetObjectById method ]

        public T GetObjectById<T>(int Id) where T : new()
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T));

                string IdCol = string.Format("{0}", t.GetType().GetMethod("GetIdColumn").Invoke(t, null));
                if (IdCol.Trim().Length == 0)
                    throw new Exception("Id Column can not be empty.");

                string tableName = string.Format("{0}", t.GetType().GetMethod("GetTableName").Invoke(t, null));
                if (tableName.Trim().Length == 0)
                    throw new Exception("TableName can not be empty.");

                DataTable dtBO = GetResultSetOfQuery(string.Format("Select * From {0} Where {1}={2}", tableName, IdCol, Id)).Tables[0];

                List<T> listT = dtBO.ToList<T>(true);

                return listT[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetSpecialColumnsWithBO method ]

        public DataTable GetSpecialColumnsWithBO(string[] columnList, BaseBO baseBO)
        {
            try
            {
                if (columnList.IsNull())
                    throw new Exception("Column List can not be null.");

                if (columnList.Length == 0)
                    throw new Exception("Column List Length can not be Zero.");

                queryBuilder = CreateQueryBuilder(QueryTypes.SelectWhereChangeColumns, baseBO);
                string tmpQ = queryBuilder.QueryString;
                int _index = tmpQ.FirstIndexOf('*');
                string cols = string.Empty;
                foreach (var column in columnList)
                {
                    cols = string.Format("{0}, {1}", cols, column);
                }
                cols = cols.TrimStart(',').TrimStart();
                string query = string.Format("{0} {1} {2}", tmpQ.Substring(0, _index), cols, tmpQ.Substring(_index + 1, tmpQ.Length - _index - 1));
                return GetResultSetOfQuery(query, queryBuilder.Properties).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ GetChangeColumnList method ]

        /// <summary>
        /// Gets Columns which baseBo object changelist contains.
        /// Example: Column Change List Of BaseBO : Col1, Col2, Col3.
        /// Query = Select Col1, Col2, Col3 From TableOfBaseBO;
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>Returns a System.Data.DataTable</returns>
        public DataTable GetChangeColumnList(BaseBO baseBO)
        {
            try
            {
                queryBuilder = CreateQueryBuilder(QueryTypes.SelectWhereChangeColumns, baseBO);
                return GetResultSetOfQuery(queryBuilder.QueryString, queryBuilder.Properties).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion


        #region [ Get Where Change Column List nethod ]

        /// <summary>
        /// Gets Columns which baseBo object changelist contains.
        /// Example: Column Change List Of BaseBO : Col1, Col2, Col3.
        /// Query = Select *  From TableOfBaseBO Where Col1=Col1Value And Col2=Col2Value And Col3=Col3Value;
        /// </summary>
        /// <param name="baseBO">BaseBO object.</param>
        /// <returns>Returns a System.Data.DataTable</returns>
        public DataTable GetWhereChangeColumnList(BaseBO baseBO)
        {
            try
            {
                queryBuilder = CreateQueryBuilder(QueryTypes.SelectWhereChangeColumns, baseBO);
                return GetResultSetOfQuery(queryBuilder.QueryString, queryBuilder.Properties).Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ CreateQueryBuilder method. ]

        /// <summary>
        /// Create A QueryBuilder object with ConnectionType Of Connection and given querytype and AbstractBaseBo object.
        /// </summary>
        /// <param name="queryType">QueryType Of QueryBuilder</param>
        /// <param name="tableObject">Table class object.</param>
        /// <returns>Returns A QueryBuilder object with ConnectionType Of Connection and given querytype and AbstractBaseBo object.</returns>
        public QueryBuilder CreateQueryBuilder(QueryTypes queryType, BaseBO tableObject)
        {
            return new QueryBuilder(_driverType, queryType, tableObject);
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Get ResultSet of Query ]

        /// <summary>
        /// Returns A DataSet with given Query without any parameter
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <returns>Returns A DataSet with given Query without any parameter</returns>
        public DataSet GetResultSetOfQuery(string query)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                DataSet ds = Conn.GetResultSetOfQuery(query);
                Conn.CommitTransaction();
                return ds;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion


        #region [ Get ResultSet Of Procedure ]

        /// <summary>
        /// Gets ResultSet with given Procedure without any parameter.
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <returns>Returns A DataSet with given Procedure without any parameter</returns>
        public DataSet GetResultSetOfProcedure(string procedure)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                DataSet ds = Conn.GetResultSetOfProcedure(procedure);
                Conn.CommitTransaction();
                return ds;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Get ResultSet of Query with Parameters ]

        /// <summary>
        /// Returns A DataSet with given Query with parameter(s).
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">Property which contains parameters</param>
        /// <returns>Returns A DataSet with given Query without any parameter</returns>
        public DataSet GetResultSetOfQuery(string query, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                DataSet ds = Conn.GetResultSetOfQuery(query, parameters);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        #region [ Get ResultSet Of Procedure ]

        /// <summary>
        /// Returns A DataSet with given Procedure without any parameter
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <param name="parameters">Property parameters</param>
        /// <returns>Returns A DataSet with given Procedure without any parameter</returns>
        public DataSet GetResultSetOfProcedure(string procedure, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                DataSet ds = Conn.GetResultSetOfProcedure(procedure, parameters);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Execute Query ]

        /// <summary>
        /// Returns execution value of query.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <returns>Returns execution value of query.</returns>
        public int ExecuteQuery(string query)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                int retInt = Conn.ExecuteQuery(query);
                Conn.CommitTransaction();
                return retInt;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        #region [ Execute Procedure ]

        /// <summary>
        /// Returns execution value of Procedure.
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <returns>Returns execution value of Procedure.</returns>
        public int ExecuteProcedure(string procedure)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                int retInt = Conn.ExecuteProcedure(procedure);
                Conn.CommitTransaction();
                return retInt;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Execute Query with Parameters ]

        /// <summary>
        /// Returns execution value of query.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">Property contains parameters.</param>
        /// <returns>Returns execution value of query with parameters.</returns>
        public int ExecuteQuery(string query, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                int retInt = Conn.ExecuteQuery(query, parameters);
                Conn.CommitTransaction();
                return retInt;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        #region [ Execute Procedure ]

        /// <summary>
        /// Returns execution value of Procedure.
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <param name="parameters">Property contains parameters.</param>
        /// <returns>Returns execution value of Procedure with parameters.</returns>
        public int ExecuteProcedure(string procedure, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                int retInt = Conn.ExecuteProcedure(procedure, parameters);
                Conn.CommitTransaction();
                return retInt;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Execute Scalar Query ]

        /// <summary>
        /// Returns scalar execution value of query.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <returns>Returns scalar execution value of query.</returns>
        public object ExecuteScalarAsQuery(string query)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                object retObj = Conn.ExecuteScalarAsQuery(query);
                Conn.CommitTransaction();
                return retObj;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        #region [ Execute Scalar As Procedure ]

        /// <summary>
        /// Returns scalar execution value of Procedure.
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <returns>Returns scalar execution value of Procedure.</returns>
        public object ExecuteScalarAsProcedure(string procedure)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                object retObj = Conn.ExecuteScalarAsProcedure(procedure);
                Conn.CommitTransaction();
                return retObj;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ Execute Scalar Query with Parameters ]

        /// <summary>
        /// Returns scalar execution value of query with parameters.
        /// </summary>
        /// <param name="query">Sql Query</param>
        /// <param name="parameters">Prperty contains parameters.</param>
        /// <returns>Returns scalar execution value of query with parameters.</returns>
        public object ExecuteScalarAsQuery(string query, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                object retObj = Conn.ExecuteScalarAsQuery(query, parameters);
                Conn.CommitTransaction();
                return retObj;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        #region [ Execute Scalar Query with Parameters ]

        /// <summary>
        /// Returns scalar execution value of Procedure with parameters.
        /// </summary>
        /// <param name="query">Sql Procedure</param>
        /// <param name="parameters">Property contains parameters.</param>
        /// <returns>Returns scalar execution value of Procedure with parameters.</returns>
        public object ExecuteScalarAsProcedure(string procedure, Property parameters)
        {
            try
            {
                Conn.OpenConnection();
                Conn.CreateTransaction();
                object retObj = Conn.ExecuteScalarAsProcedure(procedure, parameters);
                Conn.CommitTransaction();
                return retObj;
            }
            catch (Exception)
            {
                Conn.RollbackTransaction();
                throw;
            }
            finally
            {
                Conn.CloseConnection();
            }
        }

        #endregion

        /* ------------------------------------------------- */

        #region [ GetTableAsList method ]

        public List<T> GetTableAsList<T>() where T : new()
        {
            try
            {
                T t = (T)Activator.CreateInstance(typeof(T), null);
                DataTable dt = new DataTable();
                if (t is IBaseBO)
                {
                    queryBuilder = CreateQueryBuilder(QueryTypes.Select, t as BaseBO);
                    dt = GetResultSetOfQuery(queryBuilder.QueryString, queryBuilder.Properties).Tables[0];
                }
                else
                {
                    MethodInfo mthdInf = t.GetType().GetMethod("GetTableName");

                    if (mthdInf == null)
                        throw new NotImplementedException("You should implement GetTableName method.");

                    object objStr = mthdInf.Invoke(t, null);

                    objStr = objStr == null ? string.Empty : objStr.ToString();
                    dt = GetResultSetOfQuery(string.Format("Select * From {0};", objStr)).Tables[0];
                }

                return dt.ToList<T>(true);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                queryBuilder = null;
            }
        }


        #endregion

        #region [ Tables method ]

        public virtual List<T> TableRecords<T>() where T : new()
        {
            try
            {
                return GetTableAsList<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region [ Dispose method ]

        /// <summary>
        /// Disposes DbManager object with sub objects.
        /// </summary>
        public void Dispose()
        {
            if (Conn != null)
            {
                Conn.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        #endregion

        #region [ BaseDL Destructor ]

        ~BaseDL()
        {
            Dispose();
        }

        #endregion

        #region [ DriverType Property ]

        /// <summary>
        /// Gets DriverType of Base DataLayer.
        /// </summary>
        public ConnectionTypes DriverType
        {
            get { return _driverType; }
        }

        #endregion

    }
}