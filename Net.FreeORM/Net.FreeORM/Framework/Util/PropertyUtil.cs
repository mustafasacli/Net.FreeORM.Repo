namespace Net.FreeORM.Framework.Util
{
    using System;
    using System.Collections;
    using System.Text;
    using Net.FreeORM.Framework.Extensions;
    using DBConnection;

    internal class PropertyUtil
    {
        private Hashtable _hash;
        private string _connStr = string.Empty;

        public PropertyUtil()
        {
            _hash = new Hashtable();
        }

        public void Add(object key, object value)
        {
            try
            {
                if (key.ToStr().ToLower().Equals("conn-str") == false)
                    _hash.Add(key, value);
                else
                    _connStr = value.ToStr();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConnectionTypes DriverType
        {
            get
            {
                try
                {
                    return DBConnection.ConnectionTypeBuilder.GetConnectionType(GetValue("driver-type").ToString());
                }
                catch (Exception)
                {
                    return DBConnection.ConnectionTypes.SqlServer;
                }
            }
        }


        public void Remove(object key)
        {
            try
            {
                _hash.Remove(key);
                if (key.ToStr().ToLower().Equals("conn-str"))
                    _connStr = string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public object GetValue(object key)
        {
            try
            {
                if (_hash.Contains(key) == true)
                {
                    return _hash[key];
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string PropertyString
        {
            get
            {
                try
                {
                    if (_connStr.IsNullOrSpace() == true)
                    {
                        IDictionaryEnumerator dictEnumerator = _hash.GetEnumerator();
                        StringBuilder strBuilder = new StringBuilder();
                        while (dictEnumerator.MoveNext())
                        {
                            if (dictEnumerator.Key.ToString().Equals("driver-type") == false)
                                strBuilder.AppendFormat("{0}={1}; ", dictEnumerator.Key, dictEnumerator.Value);
                        }
                        string retstr = strBuilder.ToString();
                        retstr = retstr.Length > 0 ? retstr.Substring(0, retstr.Length - 1) : string.Empty;
                        return retstr;
                    }
                    else
                        return _connStr;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public override string ToString()
        {
            return PropertyString;
        }
    }
}