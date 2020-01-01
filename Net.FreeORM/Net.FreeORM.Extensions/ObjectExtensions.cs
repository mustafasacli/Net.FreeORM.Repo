﻿using System;

namespace Net.FreeORM.Extensions
{
    /// <summary>
    /// Description of ObjectExtensions.
    /// </summary>
    internal static class ObjectExtensions
    {

        public static bool IsNull(this object o)
        {
            return o == null;
        }

        public static bool IsNullOrDbNull(this object obj)
        {
            return (null == obj | obj == DBNull.Value);
        }

        public static string ToStr(this object obj)
        {
            return obj.IsNullOrDbNull() == true ? string.Empty : obj.ToString();
        }

        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }

    }
}
