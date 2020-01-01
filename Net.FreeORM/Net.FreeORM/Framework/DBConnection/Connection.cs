namespace Net.FreeORM.Framework.DBConnection
{
    using System;
    using System.Data;
    using System.Data.Common;

    internal class Connection : IConnection
    {
        #region [ Private Fields ]

        private NsConnection dbConn;
        private DbTransaction dbTrans;

        private string _ConnString = String.Empty;
        private ConnectionTypes _ConnType = ConnectionTypes.SqlServer;

        #endregion


        #region [ Connection Constructor with Connection Type And Connection String ]

        /// <summary>
        /// Creates Connection Object with given ConnectionType And ConnectionString.
        /// </summary>
        /// <param name="ConnectionType">Connection Type</param>
        /// <param name="connectionString">Connection String</param>
        public Connection(ConnectionTypes ConnectionType, String connectionString)
        {
            _ConnString = connectionString;
            _ConnType = ConnectionType;
            dbConn = new NsConnection(_ConnType, _ConnString);
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ ConnectionType Property Of Connection ]

        /// <summary>
        ///  ConnectionType Of Connection.
        /// </summary>
        public ConnectionTypes ConnectionType
        {
            get
            {
                return _ConnType;
            }
            set
            {
                DisposeConnection();
                _ConnType = value;
                dbConn = new NsConnection(_ConnType, _ConnString);
            }
        }

        #endregion


        #region [ ConnectionString Property Of Connection ]

        /// <summary>
        /// Gets, Sets Connection String Of Connection.
        /// </summary>
        public String ConnectionString
        {
            get
            {
                return _ConnString;
            }
            set
            {
                _ConnString = value;
                dbConn.ConnectionString = _ConnString;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Get Connection State ]

        /// <summary>
        /// Gets Connection State of database Connection.
        /// </summary>
        /// <returns>Returns System.Data.ConnectionState object.</returns>
        public ConnectionState GetConnectionState()
        {
            return dbConn.State;
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Dispose Method ]

        /// <summary>
        /// Disposes Connection object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                DisposeConnection();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                GC.SuppressFinalize(this);
            }
        }

        #endregion


        #region [ Open Connection ]

        /// <summary>
        /// Opens Database connection.
        /// </summary>
        public void OpenConnection()
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


        #region [ Close Connection ]

        /// <summary>
        /// Closes Database connection.
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (null != dbConn)
                {
                    if (GetConnectionState() == ConnectionState.Open)
                        dbConn.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Dispose Connection Method ]

        /// <summary>
        /// Closes and Disposes Connection Object.
        /// </summary>
        protected void DisposeConnection()
        {
            try
            {
                CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                dbConn.Dispose();
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Create Transaction for DbConnection ]

        /// <summary>
        /// Creates Transaction Object.
        /// </summary>
        public void CreateTransaction()
        {
            try
            {
                if (dbConn.State != ConnectionState.Open)
                {
                    dbConn.Open();
                }

                dbTrans = dbConn.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Commit And Dispose Transaction ]

        /// <summary>
        /// Commits And Disposes Transaction.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (dbTrans != null)
                {
                    dbTrans.Commit();
                    dbTrans.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Rollback And Dispose Transaction ]

        /// <summary>
        /// Rollbacks And Disposes Transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (dbTrans != null)
                {
                    dbTrans.Rollback();
                    dbTrans.Dispose();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Get Result Set ]

        protected DataSet GetResultSet(String queryOrProc, CommandType cmdType)
        {
            try
            {
                DataSet ds = new DataSet();

                using (DbCommand dbCmd = dbConn.CreateCommand())
                {
                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    using (DbDataAdapter dbAdapter = dbConn.GetDataAdapter())
                    {
                        dbAdapter.SelectCommand = dbCmd;
                        dbAdapter.Fill(ds);
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


        #region [ Get Result Set Of Query ]

        /// <summary>
        /// Gets Resultset of query.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <returns>A System.Data.DataSet object.</returns>
        public DataSet GetResultSetOfQuery(String query)
        {
            try
            {
                return GetResultSet(query, CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Get Result Set Of Procedure ]

        /// <summary>
        /// Gets ResultSet of procedure.
        /// </summary>
        /// <param name="procedure">sql procedure string.</param>
        /// <returns>A System.Data.DataSet object.</returns>
        public DataSet GetResultSetOfProcedure(String procedure)
        {
            try
            {
                return GetResultSet(procedure, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Get Result Set ]

        protected DataSet GetResultSet(String queryOrProc, CommandType cmdType, Property property)
        {
            try
            {
                DataSet ds = new DataSet();
                using (DbCommand dbCmd = dbConn.CreateCommand())
                {

                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    if (null != property)
                    {
                        DbParameter dbParam;
                        foreach (FreeParameter prm in property.GetParameters())
                        {
                            dbParam = null;
                            dbParam = dbCmd.CreateParameter();

                            dbParam.ParameterName = prm.Name;
                            dbParam.Value = prm.Value;
                            dbCmd.Parameters.Add(dbParam);
                        }
                    }

                    using (DbDataAdapter dbAdapter = dbConn.GetDataAdapter())
                    {
                        dbAdapter.SelectCommand = dbCmd;
                        dbAdapter.Fill(ds);
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

        /* --- Düzenlendi. --- */
        #region [ Get Result Set With Parameters ]

        /// <summary>
        /// Gets ResultSet of Query with Given Parameters.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <param name="parameters">Hashtable object contains parameter names and values.</param>
        /// <returns>A System.Data.DataSet object.</returns>
        public DataSet GetResultSetOfQuery(String query, Property parameters)
        {
            try
            {
                return GetResultSet(query, CommandType.Text, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --- Düzenlendi. --- */
        #region [ Get Result Set Of Procedure ]

        /// <summary>
        /// Gets ResultSet of Procedure with Given Parameters.
        /// </summary>
        /// <param name="procedure">sql procedure string.</param>
        /// <param name="parameters">Hashtable object contains parameter names and values.</param>
        /// <returns>A System.Data.DataSet object.</returns>
        public DataSet GetResultSetOfProcedure(String procedure, Property parameters)
        {
            try
            {
                return GetResultSet(procedure, CommandType.StoredProcedure, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Execute ]

        protected Int32 Execute(String queryOrProc, CommandType cmdType)
        {
            try
            {
                Int32 retInt;
                using (DbCommand dbCmd = dbConn.CreateCommand())
                {

                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    retInt = dbCmd.ExecuteNonQuery();
                }
                return retInt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Execute Query ]

        /// <summary>
        /// Gets Execution Result of query as int.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <returns>Returns execution result as int.</returns>
        public Int32 ExecuteQuery(String query)
        {
            try
            {
                return Execute(query, CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Execute Procedure ]

        /// <summary>
        /// Gets Execution Result of procedure as int.
        /// </summary>
        /// <param name="procedure">sql procedure string.</param>
        /// <returns>Returns execution result as int.</returns>
        public Int32 ExecuteProcedure(String procedure)
        {
            try
            {
                return Execute(procedure, CommandType.StoredProcedure);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        /* --------------------------------------------------- */

        #region [ Execute ]

        protected Int32 Execute(String queryOrProc, CommandType cmdType, Property property)
        {
            try
            {
                Int32 retInt;
                using (DbCommand dbCmd = dbConn.CreateCommand())
                {

                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    if (null != property)
                    {
                        DbParameter dbParam;
                        foreach (FreeParameter prmtr in property.GetParameters())
                        {
                            dbParam = dbCmd.CreateParameter();

                            dbParam.ParameterName = prmtr.Name;
                            dbParam.Value = prmtr.Value;
                            dbCmd.Parameters.Add(dbParam);
                        }
                    }
                    retInt = dbCmd.ExecuteNonQuery();
                    dbCmd.Parameters.Clear();
                }
                return retInt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --- Düzenlendi. --- */
        #region [ Execute Query With Parameters ]

        /// <summary>
        /// Gets Exceution Result of query with given parameters.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <param name="parameters">Hashtable object contains parameter names and values.</param>
        /// <returns>Returns execution result as int.</returns>
        public Int32 ExecuteQuery(String query, Property parameters)
        {
            try
            {
                return Execute(query, CommandType.Text, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --- Düzenlendi. --- */
        #region [ Execute Procedure ]

        /// <summary>
        /// Gets Exceution Result of procedure with given parameters.
        /// </summary>
        /// <param name="query">sql procedure string.</param>
        /// <param name="parameters">Hashtable object contains parameter names and values.</param>
        /// <returns>Returns execution result as int.</returns>
        public Int32 ExecuteProcedure(String procedure, Property parameters)
        {
            try
            {
                return Execute(procedure, CommandType.StoredProcedure, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Execute Scalar ]

        protected Object ExecuteScalar(String queryOrProc, CommandType cmdType)
        {
            try
            {
                Object retObj;
                using (DbCommand dbCmd = dbConn.CreateCommand())
                {

                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    retObj = dbCmd.ExecuteScalar();
                }
                return retObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Execute Scalar As Query ]
        /// <summary>
        /// Gets Execution Result of query as object.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <returns>Returns execution result as object.</returns>
        public Object ExecuteScalarAsQuery(String query)
        {
            try
            {
                return ExecuteScalar(query, CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Execute Scalar As Procedure ]

        /// <summary>
        /// Gets Execution Result of procedure as object.
        /// </summary>
        /// <param name="query">sql procedure string.</param>
        /// <returns>Returns execution result as object.</returns>
        public Object ExecuteScalarAsProcedure(String procedure)
        {
            try
            {
                return ExecuteScalar(procedure, CommandType.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */

        #region [ Execute Scalar ]

        protected Object ExecuteScalar(String queryOrProc, CommandType cmdType, Property property)
        {
            try
            {
                Object retObj;
                using (DbCommand dbCmd = dbConn.CreateCommand())
                {

                    if (dbTrans != null)
                        dbCmd.Transaction = dbTrans;

                    dbCmd.CommandText = queryOrProc;
                    dbCmd.CommandType = cmdType;

                    if (null != property)
                    {
                        DbParameter dbParam;
                        foreach (FreeParameter prmtr in property.GetParameters())
                        {
                            dbParam = dbCmd.CreateParameter();

                            dbParam.ParameterName = prmtr.Name;
                            dbParam.Value = prmtr.Value;
                            dbCmd.Parameters.Add(dbParam);
                        }
                    }
                    retObj = dbCmd.ExecuteScalar();
                    dbCmd.Parameters.Clear();
                }
                return retObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --- Düzenlendi. --- */
        #region [ Execute Scalar With Parameters ]
        /// <summary>
        /// Gets Execution Result of query as object.
        /// </summary>
        /// <param name="query">sql query string.</param>
        /// <param name="parameters">Hashtable object contains parameter names and values.</param>
        /// <returns>Returns execution result as object.</returns>
        public Object ExecuteScalarAsQuery(String query, Property parameters)
        {
            try
            {
                return ExecuteScalar(query, CommandType.Text, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --- Düzenlendi. --- */
        #region [ Execute Scalar As Procedure ]

        /// <summary>
        /// Gets Execution Result of procedure as object.
        /// </summary>
        /// <param name="query">sql procedure string.</param>
        /// <param name="parameters">Property object contains parameter names and values.</param>
        /// <returns>Returns execution result as object.</returns>
        public Object ExecuteScalarAsProcedure(String procedure, Property parameters)
        {
            try
            {
                return ExecuteScalar(procedure, CommandType.StoredProcedure, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        /* --------------------------------------------------- */
    }
}
