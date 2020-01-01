using Net.FreeORM.Framework.BaseDal;
using System;

namespace Net.FreeORM.Log.Transaction
{
    internal class TransactionLogDL : BaseDL
    {
        public TransactionLogDL()
            : base()
        { }

        public TransactionLogDL(String name)
            : base(name)
        { }
    }
}