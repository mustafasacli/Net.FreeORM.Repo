
using System;
using System.Collections;
using System.Collections.Generic;
namespace Net.FreeORM.ConfigurationManagement
{
    public class AppSettings
    {
        private static readonly AppSettings settings = new AppSettings();
        private static Dictionary<string, string> _hash = new Dictionary<string, string>();

        public static AppSettings Instance
        {
            get { return settings; }
        }

        public string this[string key]
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(key))
                        throw new Exception("key can not be null or just include space.");

                    if (_hash.ContainsKey(key) == false)
                        throw new Exception("Dictionary does not contain key.");

                    return _hash[key];
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public string Get(string key)
        {
            try
            {
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private AppSettings()
        {
            _hash = GetHash();
        }

        private static Dictionary<string, string> GetHash()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            return dict;
        }
    }
}