using System;

namespace Net.FreeORM.Framework.Base
{
    internal interface IBaseBO
    {
        /// <summary>
        /// Returns Table Name Of IBaseBO object.
        /// </summary>
        /// <returns>Returns Table Name Of IBaseBO object.</returns>
        string GetTableName();

        /// <summary>
        ///  Returns Identity Name Of IBaseBO object.
        /// </summary>
        /// <returns>Returns Identity Column Name Of IBaseBO object.</returns>
        string GetIdColumn();

        /// <summary>
        /// Clears all change columns list.
        /// </summary>
        void Clear();
    }
}
