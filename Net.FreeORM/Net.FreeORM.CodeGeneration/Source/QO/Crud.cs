using Net.FreeORM.CodeGeneration.Source.Enumeration;

namespace Net.FreeORM.CodeGeneration.Source.QO
{
    internal class Crud
    {

        #region [ GetTablesAndColumns method ]

        public static string GetTablesAndColumns(ConnectionTypes connType)
        {
            switch (connType)
            {
                case ConnectionTypes.Sybase:
                    return @"select o.id [tableid], o.name [tableName], c.name [columnName], c.status [columnStatus], t.name [columnType] from sysobjects o
                            inner join syscolumns c on c.id = o.id
                            inner join systypes t on t.usertype = c.usertype
                            where o.type = 'U'";
                /*
                return @"SELECT sc.* 
                        FROM syscolumns sc
                        INNER JOIN sysobjects so ON sc.id = so.id
                        WHERE so.type='U'";
                */


                case ConnectionTypes.DB2:
                    return @"Select distinct(name) as name, ColType, Length, tbname from Sysibm.syscolumns";
                //-- NOTE: the where clause is case sensitive and needs to be uppercase
                // http://stackoverflow.com/questions/580735/description-of-columns-in-a-db2-table
                /*
                return @"select
                        t.table_schema as Library
                        ,t.table_name
                        ,t.table_type
                        ,c.column_name
                        ,c.ordinal_position
                        ,c.data_type
                        ,c.character_maximum_length as Length
                        ,c.numeric_precision as Precision
                        ,c.numeric_scale as Scale
                        ,c.column_default
                        ,t.is_insertable_into
                        from sysibm.tables t
                        join sysibm.columns c
                        on t.table_schema = c.table_schema
                        and t.table_name = c.table_name
                        order by t.table_name, c.ordinal_position";
                 */
                //break;

                case ConnectionTypes.PostgreSQL:
                    return "select column_name, udt_name, table_name, column_default from information_schema.columns where table_schema='public';";

                case ConnectionTypes.FireBird:
                    break;

                case ConnectionTypes.MySQL:
                case ConnectionTypes.MariaDB:
                    return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA  FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}' Order By TABLE_NAME;";

                case ConnectionTypes.OracleManaged:
                case ConnectionTypes.OracleNet:
                    return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE FROM COLS ORDER BY TABLE_NAME, COLUMN_ID;";

                case ConnectionTypes.SQLite:
                    break;

                case ConnectionTypes.OleDb:
                case ConnectionTypes.Odbc:
                    break;

                case ConnectionTypes.VistaDB:
                    return "Select table_catalog, table_schema, table_name, column_name, data_type, character_octet_length, is_identity from VistaDBColumnSchema()";

                case ConnectionTypes.SqlServerCe:
                case ConnectionTypes.SqlExpress:
                case ConnectionTypes.SqlServer:
                    return @"SELECT COLUMN_NAME, TABLE_NAME, DATA_TYPE, 
                            COLUMNPROPERTY(OBJECT_ID(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IdentityState
                            FROM INFORMATION_SCHEMA.COLUMNS";

                case ConnectionTypes.Informix:
                    return "select tabname, colname, coltype, collength from systables as a join syscolumns as b on a.tabid=b.tabid order by tabname";

                default:
                    break;
            }
            return string.Empty;
        }

        #endregion


        #region [ GetTablesQuery method ]

        public static string GetTablesQuery(ConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case ConnectionTypes.DB2:
                    break;

                case ConnectionTypes.PostgreSQL:
                    return "select table_name from information_schema.tables where table_schema='public'";

                case ConnectionTypes.FireBird:
                    return @"select f.rdb$relation_name as Table_Name, f.rdb$field_name as Column_Name, f.rdb$field_type as Data_Type
                                from rdb$relation_fields f
                                join rdb$relations r on f.rdb$relation_name = r.rdb$relation_name
                                and r.rdb$view_blr is null
                                and (r.rdb$system_flag is null or r.rdb$system_flag = 0)
                                order by 1, f.rdb$field_position;";
                // break;

                case ConnectionTypes.MySQL:
                case ConnectionTypes.MariaDB:
                    return "SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_KEY, EXTRA FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='{0}'";

                case ConnectionTypes.OracleManaged:
                case ConnectionTypes.OracleNet:
                    break;

                case ConnectionTypes.SQLite:
                    return "SELECT tbl_name FROM sqlite_master WHERE type='table' ORDER BY name;";

                case ConnectionTypes.OleDb:
                case ConnectionTypes.SqlExpress:
                case ConnectionTypes.SqlServer:
                    return "select name from sys.objects where type='u' order by name;";
                default:
                    break;
            }
            return string.Empty;
        }

        #endregion


        #region [ GetColumnsOfTablesQuery method ]

        public static string GetColumnsOfTablesQuery(ConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case ConnectionTypes.DB2:
                    break;

                case ConnectionTypes.PostgreSQL:
                    return "select column_name,udt_name,table_name from information_schema.columns where table_schema='public' and table_name='{0}';";

                case ConnectionTypes.FireBird:
                    break;

                case ConnectionTypes.MySQL:
                    break;

                case ConnectionTypes.OracleNet:
                    break;

                case ConnectionTypes.SQLite:
                    return "PRAGMA table_info('{0}');";

                case ConnectionTypes.OleDb:
                case ConnectionTypes.SqlExpress:
                case ConnectionTypes.SqlServer:
                    return @"SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}'";
                default:
                    break;
            }
            return string.Empty;
        }

        #endregion


        #region [ GetIdColumnOfTable method ]

        public static string GetIdColumnOfTable(ConnectionTypes ConnType)
        {
            switch (ConnType)
            {
                case ConnectionTypes.DB2:
                    break;

                case ConnectionTypes.PostgreSQL:
                    break;

                case ConnectionTypes.FireBird:
                    break;

                case ConnectionTypes.MySQL:
                    break;

                case ConnectionTypes.OracleNet:
                    break;

                case ConnectionTypes.SQLite:
                    break;

                case ConnectionTypes.OleDb:
                case ConnectionTypes.SqlExpress:
                case ConnectionTypes.SqlServer:
                    return @"select COLUMN_NAME
from INFORMATION_SCHEMA.COLUMNS
where COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') = 1 And
TABLE_NAME='{0}'";

                default:
                    break;
            }
            return string.Empty;
        }

        #endregion

    }
}
