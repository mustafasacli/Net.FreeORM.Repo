using System;

namespace Net.FreeORM.ResourceBuilding
{
    public class ResourceBuilder
    {
        private System.Resources.ResourceManager resMan;

        private ResourceBuilder(string baseName, Type t)
        {
            resMan = new System.Resources.ResourceManager(baseName, t.Assembly);
        }

        public string this[string key]
        {
            get
            {
                try
                {
                    return resMan.GetString(key);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static ResourceBuilder CreateInstance(string baseName, Type t)
        {
            try
            {
                return new ResourceBuilder(baseName, t);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Get(string key)
        {
            try
            {
                return resMan.GetString(key);
            }
            catch
            {
                throw;
            }
        }
    }
}