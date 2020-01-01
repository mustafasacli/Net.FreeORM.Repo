
namespace Net.FreeORM.Framework.QueryBuilding
{
    internal class QueryFormat
    {
        private string _format = "";

        /// <summary>
        /// Gets Format Of Query Type.
        /// </summary>
        public string Format
        {
            get { return _format; }
        }

        /// <summary>
        /// Creates Format Of Given Query Type.
        /// </summary>
        /// <param name="query_type"></param>
        public QueryFormat(QueryTypes query_type)
        {
            _format = get_query_format(query_type);
        }

        #region [ Query Format of QueryType ]

        /// <summary>
        /// Returns Format of Query according to QueryType.
        /// </summary>
        /// <param name="queryType"></param>
        /// <returns></returns>
        private string get_query_format(QueryTypes queryType)
        {
            string f = "";

            switch (queryType)
            {
                case QueryTypes.Select:
                    f = "SELECT * FROM {0};";
                    break;

                case QueryTypes.Insert:
                    f = "INSERT INTO {0}({1}) VALUES({2});";
                    break;

                case QueryTypes.InsertAndGetId:
                    f = "INSERT INTO {0}({1}) VALUES({2})";
                    break;

                case QueryTypes.Update:
                    f = "UPDATE {0} SET {1} WHERE {2};";
                    break;

                case QueryTypes.Delete:
                    f = "DELETE FROM {0} WHERE {1};";
                    break;

                case QueryTypes.SelectWhereChangeColumns:
                case QueryTypes.SelectWhereId:
                    f = "SELECT * FROM {0} WHERE {1};";
                    break;

                case QueryTypes.SelectChangeColumns:
                    f = "SELECT {0} FROM {0};";
                    break;

                default:
                    break;
            }

            return f;
        }

        #endregion [ Query Format of QueryType ]
    }
}