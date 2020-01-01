namespace Net.FreeORM.Framework.DBConnection
{
    using System.Data;

    internal interface IConnectionOperations
    {
        DataSet GetResultSetOfQuery(string query);

        DataSet GetResultSetOfQuery(string query, Property parameters);

        DataSet GetResultSetOfProcedure(string procedure);

        DataSet GetResultSetOfProcedure(string procedure, Property parameters);

        /* -------------------------------------------------------------- */

        int ExecuteQuery(string query);

        int ExecuteQuery(string query, Property parameters);

        int ExecuteProcedure(string procedure);

        int ExecuteProcedure(string procedure, Property parameters);

        /* -------------------------------------------------------------- */

        object ExecuteScalarAsQuery(string query);

        object ExecuteScalarAsQuery(string query, Property parameters);

        object ExecuteScalarAsProcedure(string procedure);

        object ExecuteScalarAsProcedure(string procedure, Property parameters);
    }
}