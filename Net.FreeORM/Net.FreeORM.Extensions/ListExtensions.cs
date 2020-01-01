using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;

namespace Net.FreeORM.Extensions
{
    /// <summary>
    /// Description of ListExtensions.
    /// </summary>
    public static class ListExtensions
    {

        #region [ List To DataTable ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> data)
        {
            try
            {
                PropertyDescriptorCollection properties =
                    TypeDescriptor.GetProperties(typeof(T));
                DataTable table = new DataTable();
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
                return table;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion


        #region [ Create DataSet From Generic List ]
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataSet CreateDataSet<T>(this List<T> list)
        {
            try
            {
                var properties = list[0].GetType().GetProperties();
                var dataSet = new DataSet();
                var dataTable = new DataTable();

                DataColumn[] columns = new DataColumn[properties.Length];

                for (int i = 0; i < properties.Length; i++)
                {
                    columns[i] = new DataColumn(properties[i].Name, properties[i].PropertyType);
                }

                dataTable.Columns.AddRange(columns);

                foreach (var item in list)
                {
                    var dataRow = dataTable.NewRow();

                    var itemProperties = item.GetType().GetProperties();

                    for (int i = 0; i < itemProperties.Length; i++)
                    {
                        dataRow[i] = itemProperties[i].GetValue(item, null) ?? DBNull.Value;
                    }

                    dataTable.Rows.Add(dataRow);
                }

                dataSet.Tables.Add(dataTable);

                return dataSet;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region [ Export List As Excel ]

        public static void ExportListAsExcel<T>(this IList<T> list, string fileName)
        {
            try
            {
                using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                {
                    PropertyInfo[] propInfos = typeof(T).GetProperties();

                    sWriter.AutoFlush = true;

                    foreach (PropertyInfo inf in propInfos)
                    {
                        sWriter.Write("{0}\t", inf.Name);
                    }
                    sWriter.Write("\n");

                    foreach (T _t in list)
                    {
                        foreach (PropertyInfo inf in propInfos)
                        {
                            sWriter.Write("{0}\t", inf.GetValue(_t, null).ToStr().Replace("\n", " "));
                        }
                        sWriter.Write("\n");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region [ Export List As Excel ]

        public static void ExportListAsExcelWithIncludeCols<T>(this IList<T> list, string fileName, params object[] includedProperties)
        {
            try
            {
                if (includedProperties.IsNull())
                {
                    return;
                }
                else
                {
                    List<PropertyInfo> propInfos = new List<PropertyInfo>();
                    PropertyInfo prpInf;
                    foreach (object obj in includedProperties)
                    {
                        prpInf = typeof(T).GetProperty(obj.ToStr());
                        propInfos.Add(prpInf);
                    }

                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (PropertyInfo prp in propInfos)
                        {
                            sWriter.Write("{0}\t", prp);
                        }
                        sWriter.Write("\n");

                        foreach (T _t in list)
                        {
                            foreach (PropertyInfo prp in propInfos)
                            {
                                sWriter.Write("{0}\t", prp.GetValue(_t, null).ToStr().Replace("\n", " "));
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

        public static void ExportAsExcelWithExcludeColumns<T>(this IList<T> list, string fileName, params object[] excludeColumns)
        {
            try
            {
                PropertyInfo[] propInfos = typeof(T).GetProperties();
                if (excludeColumns.IsNull())
                {
                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (PropertyInfo inf in propInfos)
                        {
                            sWriter.Write("{0}\t", inf.Name);
                        }
                        sWriter.Write("\n");

                        foreach (T _t in list)
                        {
                            foreach (PropertyInfo inf in propInfos)
                            {
                                sWriter.Write("{0}\t", inf.GetValue(_t, null).ToStr().Replace("\n", " "));
                            }
                            sWriter.Write("\n");
                        }
                    }
                }
                else
                {

                    System.Collections.Generic.List<PropertyInfo> propList = new System.Collections.Generic.List<PropertyInfo>();
                    bool contain = false;
                    foreach (PropertyInfo inf in propInfos)
                    {
                        contain = false;
                        foreach (object obj in excludeColumns)
                        {
                            contain = inf.Name.Equals(obj.ToStr());
                            if (contain == true)
                                break;
                        }

                        if (contain == false)
                            propList.Add(inf);
                    }

                    using (StreamWriter sWriter = new StreamWriter(new FileStream(fileName, FileMode.OpenOrCreate)))
                    {
                        sWriter.AutoFlush = true;

                        foreach (PropertyInfo inf in propList)
                        {
                            sWriter.Write("{0}\t", inf.Name);
                        }
                        sWriter.Write("\n");

                        foreach (T _t in list)
                        {
                            foreach (PropertyInfo prpInf in propList)
                            {
                                sWriter.Write("{0}\t", prpInf.GetValue(_t, null).ToStr().Replace("\n", " "));
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

    }
}
