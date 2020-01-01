using System;

namespace Net.FreeORM.CodeGeneration.Source.Util
{
    class FireBirdUtil
    {
        public static string GetIntFrom(int dataTypeId)
        {
            string strDataType = "";
            try
            {
                switch (dataTypeId)
                {
                    case 7:
                        strDataType = "smallint";
                        break;

                    case 8:
                    case 9:
                        strDataType = "int";
                        break;

                    case 10:
                    case 11:
                        strDataType = "real";//"Float";
                        break;

                    case 12:
                    case 13:
                        strDataType = "datetime";
                        break;

                    case 14:
                        strDataType = "char";
                        break;

                    case 16:
                        strDataType = "bigint";
                        break;

                    case 27:
                        strDataType = "real";
                        break;

                    case 35:
                        strDataType = "datetime";
                        break;

                    case 37:
                    case 40:
                        strDataType = "varchar";
                        break;

                    case 261:
                        strDataType = "blob";
                        break;

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