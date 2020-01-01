using System;

namespace Net.FreeORM.SecureHash.Test
{
    internal class ConnectionTester
    {
        public static bool TestConnection(ConnectionTypes connType, string connectionString)
        {
            bool retBool = false;
            try
            {
                using (NsConnection conn = new NsConnection(connType, connectionString))
                {
                    conn.Open();
                    retBool = true;
                    conn.Close();
                }
                FrmSecureHash.Error = retBool == true ? "Valid Connection" : "Unknown Error.";
            }
            catch (Exception exc)
            {
                FrmSecureHash.Error = exc.Message;
                retBool = false;
            }
            return retBool;
        }
    }
}