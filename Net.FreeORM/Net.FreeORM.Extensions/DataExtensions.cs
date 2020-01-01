﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;

namespace Net.FreeORM.Extensions
{
    /// <summary>
    /// Description of DataExtensions.
    /// </summary>
    public static class DataExtensions
    {

        #region [ DataTable To Generic List ]

        /// <summary>
        /// This method returns A List of T object.
        /// </summary>
        /// <typeparam name="T">T object type</typeparam>
        /// <param name="datatable">Datatble object</param>
        /// <param name="accordingToColumn">if it is true, returns a List with DataTable Columns else returns a List with PropertyInfo of Object.</param>
        /// <returns>Returns A List of T object.</returns>
        public static List<T> ToList<T>(this DataTable datatable, bool accordingToColumn) where T : new()
        {
            try
            {
                List<T> liste = new List<T>();
                object obj;
                T item = new T();
                if (accordingToColumn == true)
                {
                    PropertyInfo propInfo = null;
                    foreach (DataRow row in datatable.Rows)
                    {
                        item = new T();
                        foreach (DataColumn col in datatable.Columns)
                        {
                            obj = row[col.ColumnName];
                            if (null != obj && obj != DBNull.Value)
                            {
                                propInfo = typeof(T).GetProperty(col.ColumnName);
                                propInfo.SetValue(item, obj);
                            }

                        }
                        liste.Add(item);
                    }
                }
                else
                {
                    PropertyInfo[] pInfos = typeof(T).GetProperties();
                    foreach (DataRow row in datatable.Rows)
                    {
                        item = new T();
                        for (int proCounter = 0; proCounter < pInfos.Length; proCounter++)
                        {
                            obj = row[pInfos[proCounter].Name];
                            if (obj.IsNullOrDbNull() == false)
                                pInfos[proCounter].SetValue(item, obj);
                        }
                        liste.Add(item);
                    }
                }
                return liste;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region [ GetPageOfDataTable method ]

        public static DataTable GetPageOfDataTable(this DataTable dt, int pageNumber, int rowCount)
        {
            try
            {
                if (dt == null)
                    throw new System.NullReferenceException("DataTable object can not be null.");

                if (pageNumber < 0)
                    throw new Exception("Page Number cannot be less than 0.");
                if (rowCount < 1)
                    throw new Exception("Row Count of Page cannot be less than 1.");

                DataTable retDt = new DataTable();
                foreach (DataColumn col in dt.Columns)
                {
                    retDt.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
                }

                int totalRow = dt.Rows.Count;
                int rowNo = pageNumber * rowCount;
                if (totalRow > rowNo)
                {
                    int rowForCounter = totalRow > (rowNo + rowCount) ? rowCount : (totalRow - rowNo);

                    for (int i = 0; i < rowForCounter; i++)
                    {
                        DataRow row = dt.Rows[rowNo + i];
                        retDt.Rows.Add(row.ItemArray);
                    }
                }

                return retDt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Get Column Numbers Of DataTable ]

        /// <summary>
        /// Returns a DataTable with Selected column names.
        /// </summary>
        /// <param name="dt">DataTable object</param>
        /// <param name="columnList"> column names array </param>
        /// <returns>Returns a DataTable with Selected column names.</returns>
        public static DataTable GetColumnsOfDataTable(this DataTable dt, params string[] columnList)
        {
            try
            {
                DataTable ndt = new DataTable();
                DataColumn dtCol = null;
                foreach (string colName in columnList)
                {
                    dtCol = new DataColumn(dt.Columns[colName].ColumnName, dt.Columns[colName].DataType);
                    ndt.Columns.Add(dtCol);
                }
                List<object> rowItems = null;
                foreach (DataRow row in dt.Rows)
                {
                    rowItems = new List<object>();
                    foreach (string colName in columnList)
                    {
                        rowItems.Add(row[colName]);
                    }
                    ndt.Rows.Add(rowItems.ToArray());
                }
                return ndt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Get Columns Of DataTable ColumnNumbers ]

        /// <summary>
        /// Returns a DataTable with Selected column numbers.
        /// </summary>
        /// <param name="dt">DataTable object</param>
        /// <param name="columnList">column numbers array</param>
        /// <returns>Returns a DataTable with Selected column numbers.</returns>
        public static DataTable GetColumnsOfDataTable(this DataTable dt, params int[] columnList)
        {
            try
            {
                DataTable ndt = new DataTable();
                DataColumn dtCol = null;
                foreach (int colNo in columnList)
                {
                    dtCol = new DataColumn(dt.Columns[colNo].ColumnName, dt.Columns[colNo].DataType);
                    ndt.Columns.Add(dtCol);
                }
                List<object> rowItems = null;
                foreach (DataRow row in dt.Rows)
                {
                    rowItems = new List<object>();
                    foreach (int colNo in columnList)
                    {
                        rowItems.Add(row[colNo]);
                    }
                    ndt.Rows.Add(rowItems.ToArray());
                }
                return ndt;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Get Object With Selected Column ]
        /// <summary>
        /// Returns a object with given parameters.
        /// </summary>
        /// <param name="dt">DataTable object</param>
        /// <param name="refColumn">Name of Reference Column</param>
        /// <param name="refValue">Value of Reference Column</param>
        /// <param name="destinationColumn">Name of Destination Column</param>
        /// <returns>Returns a object at destination column which contains reference value at reference column. Otherwise return null.</returns>
        public static object GetObjectWithSelectedColumn(this DataTable dt, string refColumn, object refValue, string destinationColumn)
        {
            try
            {
                object retObj = null;
                foreach (DataRow row in dt.Rows)
                {
                    if (row[refColumn].ToString().Equals(refValue.ToString()))
                    {
                        retObj = row[destinationColumn];
                        break;
                    }
                }
                return retObj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Export As Excel With Include Columns ]

        public static void ExportAsExcelWithIncludeColumns(this DataTable dt, string fileName, params object[] includeColumns)
        {
            try
            {
                if (includeColumns.IsNull())
                {
                    return;
                }
                else
                {
                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (string col in includeColumns)
                        {
                            sWriter.Write("{0}\t", col);
                        }
                        sWriter.Write("\n");

                        foreach (DataRow rw in dt.Rows)
                        {
                            foreach (string col in includeColumns)
                            {
                                sWriter.Write("{0}\t", rw[col].ToStr().Replace("\n", " "));
                            }
                            sWriter.Write("\n");
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Export As Excel With Exclude Columns ]

        public static void ExportAsExcelWithExcludeColumns(this DataTable dt, string fileName, params object[] excludeColumns)
        {
            try
            {
                if (excludeColumns.IsNull())
                {
                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (DataColumn col in dt.Columns)
                        {
                            sWriter.Write("{0}\t", col.ColumnName);
                        }
                        sWriter.Write("\n");

                        foreach (DataRow rw in dt.Rows)
                        {
                            foreach (DataColumn col in dt.Columns)
                            {
                                sWriter.Write("{0}\t", rw[col].ToStr().Replace("\n", " "));
                            }
                            sWriter.Write("\n");
                        }
                    }
                }
                else
                {

                    System.Collections.Generic.List<string> colList = new System.Collections.Generic.List<string>();

                    foreach (DataColumn col in dt.Columns)
                    {
                        colList.Add(col.ColumnName);
                    }

                    foreach (object obj in excludeColumns)
                    {
                        if (colList.Contains(obj.ToStr()) == true)
                        {
                            colList.Remove(obj.ToStr());
                        }
                    }


                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (string col in colList)
                        {
                            sWriter.Write(string.Format("{0}\t", col));
                        }
                        sWriter.Write("\n");

                        foreach (DataRow rw in dt.Rows)
                        {
                            foreach (string col in colList)
                            {
                                sWriter.Write("{0}\t", rw[col].ToStr().Replace("\n", " "));
                            }
                            sWriter.Write("\n");
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Copy method ]

        public static DataTable Copy(this DataTable dt)
        {
            try
            {
                DataTable _dataT = new DataTable();
                foreach (DataColumn col in dt.Columns)
                {
                    _dataT.Columns.Add(col.ColumnName, col.DataType);
                }

                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    dr = null;
                    dr = _dataT.NewRow();
                    foreach (DataColumn col in dt.Columns)
                    {
                        dr[col.ColumnName] = row[col.ColumnName];
                    }
                    _dataT.Rows.Add(dr);
                }

                return _dataT;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ GetSomeColumnsAsTable method ]

        public static DataTable GetSomeColumnsAsTable(this DataTable dt, string[] columnList)
        {
            try
            {
                DataTable dtNew = new DataTable();
                if (dt == null)
                    return dtNew;

                if (columnList == null)
                    return dtNew;

                if (columnList.Length == 0)
                    return dtNew;

                DataColumn _col;
                foreach (string col in columnList)
                {
                    _col = dt.Columns[col];
                    dtNew.Columns.Add(_col.ColumnName, _col.DataType);
                }

                DataRow dr = null;
                foreach (DataRow row in dt.Rows)
                {
                    dr = dtNew.NewRow();
                    foreach (string col in columnList)
                    {
                        dr[col] = row[col];
                    }
                    dtNew.Rows.Add(dr);
                }
                return dtNew;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
