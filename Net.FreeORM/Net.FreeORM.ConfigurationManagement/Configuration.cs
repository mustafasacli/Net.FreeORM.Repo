namespace Net.FreeORM.ConfigurationManagement
{
    public class Configuration
    {
        public static AppSettings Settings
        {
            get { return AppSettings.Instance; }
        }
    }
}