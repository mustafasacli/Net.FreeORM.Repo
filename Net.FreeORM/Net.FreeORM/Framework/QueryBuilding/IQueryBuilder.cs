namespace Net.FreeORM.Framework.QueryBuilding
{
    using Net.FreeORM.Framework.DBConnection;
    using System;

    internal interface IQueryBuilder
    {
        /// <summary>
        /// Gets Property contains parameter(s).
        /// </summary>
        Property Properties { get; }

        /// <summary>
        /// Gets Sql Query.
        /// </summary>
        string QueryString { get; }

    }
}