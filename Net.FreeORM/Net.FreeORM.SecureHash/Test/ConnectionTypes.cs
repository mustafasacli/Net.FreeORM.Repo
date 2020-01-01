namespace Net.FreeORM.SecureHash.Test
{
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
        OracleManaged = 19
    };
}