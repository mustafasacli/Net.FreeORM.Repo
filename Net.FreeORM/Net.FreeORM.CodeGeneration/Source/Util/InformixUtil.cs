using System;

namespace Net.FreeORM.CodeGeneration.Source.Util
{
    internal class InformixUtil
    {
        public static string GetIntFrom(int dataTypeId)
        {
            string strDataType = "";
            try
            {
                switch (dataTypeId)
                {
                    case 0:
                        strDataType = "char";
                        break;

                    case 1:
                        strDataType = "smallint";
                        break;

                    case 2:
                        strDataType = "int";
                        break;

                    case 3:
                    case 4:
                        strDataType = "Float";
                        break;

                    case 5:
                        strDataType = "decimal";
                        break;

                    case 6:
                        strDataType = "int";
                        break;

                    case 7:
                        strDataType = "datetime";
                        break;

                    case 8:
                        strDataType = "Money";
                        break;

                    case 9:
                        strDataType = "varchar";
                        break;

                    case 10:
                        strDataType = "datetime";
                        break;

                    case 11:
                        strDataType = "binary";
                        break;

                    case 12:
                    case 13:
                        strDataType = "varchar";
                        break;

                    case 14:
                        strDataType = "int";
                        break;

                    case 15:
                        strDataType = "nchar";
                        break;

                    case 16:
                        strDataType = "nvarchar";
                        break;

                    case 17:
                    case 18:
                        strDataType = "bigint";
                        break;

                    case 19:
                    case 20:
                    case 21:
                    case 22:
                    case 23:
                        strDataType = "varchar";
                        break;

                    case 40:
                    case 41:
                        strDataType = "int";
                        break;

                    case 43:
                        strDataType = "varchar";
                        break;

                    case 45:
                        strDataType = "bit";
                        break;

                    case 52:
                    case 53:
                        strDataType = "bigint";
                        break;

                    case 2061:
                    case 4118:
                    default:
                        strDataType = "varchar";
                        break;
                }
            }
            catch (Exception)
            {
                strDataType = "varchar";
            }
            return strDataType;
        }
    }
}