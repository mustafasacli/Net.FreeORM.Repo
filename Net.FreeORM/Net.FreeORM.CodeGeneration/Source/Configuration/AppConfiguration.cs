using Net.FreeORM.CodeGeneration.Source.Settings;

namespace Net.FreeORM.CodeGeneration.Source.Configuration
{
    internal static class AppConfiguration
    {
        private static Setting _setting = new Setting();

        public static Setting Settings
        {
            get
            {
                return _setting;
            }
        }
    }
}