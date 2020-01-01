using System;

namespace Net.FreeORM.CodeGeneration.Source.Util
{
    internal class ConvertUtil
    {
        public static int ToInt(string str)
        {
            int retInt = 0;
            try
            {
                int.TryParse(str, out retInt);
            }
            catch (Exception)
            {
            }
            return retInt;
        }
    }
}