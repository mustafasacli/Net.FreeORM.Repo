using System;
namespace Net.FreeORM.Framework.DBConnection
{
    #region [ ConnectionTypes enum ]

    public enum ConnectionTypes : byte
    {
        SqlExpress = 0,
        SqlServer = 1,
        PostgreSQL = 2,
        DB2 = 3,
        OracleNet = 4,
        MySQL = 5,
        MariaDB = 6,
        VistaDB = 7,
        OleDb = 8,
        SQLite = 9,
        FireBird = 10,
        SqlServerCe = 11,
        Sybase = 12,
        Informix = 13,
        U2 = 14,
        Synergy = 15,
        Ingres = 16,

        /// <summary>
        /// Version must be upper than 11.7.
        /// </summary>
        SqlBase = 17,

        Odbc = 18,
        OracleManaged = 19,
        External = 20
    };

    #endregion


    #region [ ConnectionTypeBuilder class ]

    internal static class ConnectionTypeBuilder
    {
        public static ConnectionTypes GetConnectionType(String driverTypeName)
        {
            try
            {
                driverTypeName = driverTypeName.TrimEnd().TrimStart().ToLower();
                switch (driverTypeName)
                {
                    case "sqlexpress":
                        return ConnectionTypes.SqlExpress;

                    case "sqlserver":
                    default:
                        return ConnectionTypes.SqlServer;

                    case "postgresql":
                        return ConnectionTypes.PostgreSQL;

                    case "db2":
                        return ConnectionTypes.DB2;

                    case "oracle":
                        return ConnectionTypes.OracleNet;

                    case "oracle-managed":
                    case "oraclemanaged":
                    case "oracle-mngd":
                        return ConnectionTypes.OracleManaged;

                    case "mysql":
                        return ConnectionTypes.MySQL;

                    case "mariadb":
                    case "marıadb":
                        return ConnectionTypes.MariaDB;

                    case "vistadb":
                    case "vıstadb":
                        return ConnectionTypes.VistaDB;

                    case "oledb":
                        return ConnectionTypes.OleDb;

                    case "sqlite":
                    case "sqlıte":
                        return ConnectionTypes.SQLite;

                    case "firebird":
                    case "firebırd":
                    case "fırebird":
                    case "fırebırd":
                        return ConnectionTypes.FireBird;

                    case "sqlserverce":
                        return ConnectionTypes.SqlServerCe;

                    case "sybase":
                        return ConnectionTypes.Sybase;

                    case "informix":
                    case "ınformıx":
                    case "ınformix":
                    case "informıx":
                        return ConnectionTypes.Informix;

                    case "u2":
                        return ConnectionTypes.U2;

                    case "synergy":
                        return ConnectionTypes.Synergy;

                    case "ingres":
                    case "ıngres":
                        return ConnectionTypes.Ingres;

                    case "sqlbase":
                        return ConnectionTypes.SqlBase;

                    case "odbc":
                        return ConnectionTypes.Odbc;
                }
            }
            catch (System.Exception)
            {
                return ConnectionTypes.SqlServer;
            }
        }
    }

    #endregion

}