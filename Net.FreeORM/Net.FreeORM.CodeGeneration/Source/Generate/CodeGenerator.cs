﻿using Net.FreeORM.CodeGeneration.Source.BLH;
using Net.FreeORM.CodeGeneration.Source.BO;
using Net.FreeORM.CodeGeneration.Source.Connect;
using Net.FreeORM.CodeGeneration.Source.Enumeration;
using Net.FreeORM.CodeGeneration.Source.QO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Net.FreeORM.CodeGeneration.Source.Generate
{
    internal class CodeGenerator
    {
        public static string ConnStr = string.Empty;
        public static int Index = -1;

        private string _ConnectionString = string.Empty;
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        private ConnectionTypes _ConnType = ConnectionTypes.SqlServer;

        internal ConnectionTypes ConnType
        {
            get { return _ConnType; }
            set { _ConnType = value; }
        }


        public List<Table> GetTablesAndColumns(ConnectionTypes connectionType, string connStr)
        {
            try
            {
                GeneratorLH genLH = new GeneratorLH();
                return genLH.GetTablesAndColumns(connectionType, connStr);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Table> GetTableList()
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string sqlQuery = Crud.GetTablesQuery(frCon.ConnectionType);

                DataTable dT = frCon.GetResultSet(sqlQuery).Tables[0];
                List<Table> classtable = new List<Table>();

                foreach (DataRow row in dT.Rows)
                {
                    classtable.Add(new Table()
                    {
                        TableName = row[0].ToString()
                    });
                }

                return classtable;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public List<Column> GetColumnsOfTable(string tableName)
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string sqlQuery = Crud.GetColumnsOfTablesQuery(frCon.ConnectionType);

                DataTable dT = frCon.GetResultSet(string.Format(sqlQuery, tableName)).Tables[0];
                List<Column> columns = new List<Column>();

                foreach (DataRow row in dT.Rows)
                {
                    columns.Add(
                        new Column(
                            row[0].ToString(),  // for Column Name
                            row[1].ToString()   // for Column Datatype
                    ));
                }

                return columns;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetIdColumnOfTable(string tableName)
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string query = string.Format(Crud.GetIdColumnOfTable(frCon.ConnectionType), tableName);
                if (string.IsNullOrWhiteSpace(query))
                    return string.Empty;
                DataTable dtIdCols = frCon.GetResultSet(query).Tables[0];
                string IdColumn = "";
                foreach (DataRow row in dtIdCols.Rows)
                {
                    IdColumn = row[0].ToString(); // IdColumn
                    break;
                }
                return IdColumn;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
