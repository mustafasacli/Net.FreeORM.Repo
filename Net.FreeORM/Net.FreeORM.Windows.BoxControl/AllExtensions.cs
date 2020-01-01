using System;

namespace Net.FreeORM.Windows.BoxControl
{
    #region [ Internal Extension Class ]

    /// <summary>
    /// All Extension methods contains.
    /// </summary>
    internal static class AllExtensions
    {

        public static bool IsNull(object o)
        {
            return o == null;
        }

        public static bool IsNullOrDbNull(object obj)
        {
            return (null == obj | obj == DBNull.Value);
        }

        public static string ToStr(object obj)
        {
            return IsNullOrDbNull(obj) == true ? string.Empty : obj.ToString();
        }


        public static bool IsNullOrEmpty(string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        public static bool IsNullOrSpace(string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Trim().Length == 0;
            }
        }

    }

    #endregion
}
