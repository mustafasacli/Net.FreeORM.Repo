using Net.FreeORM.Framework.BaseDal;
using System;

namespace Net.FreeORM.Log.Error
{
    internal class LogDL : BaseDL
    {
        public LogDL()
            : base()
        { }

        public LogDL(String logName)
            : base(logName)
        {
        }
    }
}