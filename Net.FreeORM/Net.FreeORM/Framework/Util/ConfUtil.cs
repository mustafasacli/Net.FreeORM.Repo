namespace Net.FreeORM.Framework.Util
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    internal class ConfUtil
    {
        private static readonly string XML_FILE_PATH = @"C:\FreeORM\config\conf.xml";
        private static readonly string PROP_NODE = "appsettings/dblogin/db-properties/properties";
        private static readonly string SAVE_NODE = "appsettings/appconfig/log-setting";
        private static Hashtable _properties;
        private static string _logType;
        private static string _saveType;
        private static string _savePath;
        private static List<string> paramHashList = new List<string>()
        {
           "uid",
           "userid",
           "logonid",
           "user",
           "username",
           "pwd",
           "password"
        };

        static ConfUtil()
        {
            _properties = new Hashtable();
            PrepareConf();
        }

        public static string Get(string key)
        {
            if (_properties.ContainsKey(key) == true)
            {
                object obj = _properties[key];
                return obj != null ? obj.ToString() : string.Empty;
            }
            else
                return string.Empty;
        }

        public static PropertyUtil BuildProperty(string propName)
        {
            try
            {
                // Buraya ekleme kısımları gelecek.
                PropertyUtil propUtil = new PropertyUtil();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XML_FILE_PATH);
                XmlNodeList propNodeList = xmlDoc.SelectNodes(PROP_NODE);
                foreach (XmlNode node in propNodeList)
                {
                    if (node.Attributes.Count > 0)
                    {
                        if (node.Attributes["name"].InnerText.Equals(propName) == true)
                        {
                            foreach (XmlAttribute attr in node.Attributes)
                            {
                                if (attr.Name.Equals("name") == false)
                                    propUtil.Add(attr.Name, attr.Value);
                            }

                            XmlNodeList xnlNdLst = node.SelectNodes("property");
                            string strPrm;
                            foreach (XmlNode xml_node in xnlNdLst)
                            {
                                try
                                {
                                    strPrm = xml_node.Attributes["key"].InnerText;
                                    strPrm = strPrm.ToLower().Replace('ı', 'i');
                                    strPrm = strPrm.Replace(" ", "");
                                    if (paramHashList.Contains(strPrm))
                                    {
                                        propUtil.Add(
                                            xml_node.Attributes["key"].InnerText,
                                            SecurityUtil.DecodeString(xml_node.Attributes["value"].InnerText)
                                            );
                                    }
                                    else
                                    {
                                        propUtil.Add(
                                            xml_node.Attributes["key"].InnerText,
                                            xml_node.Attributes["value"].InnerText
                                            );

                                    }
                                }
                                catch
                                {
                                    throw;
                                }
                            }
                        }
                    }
                }

                return propUtil;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string SaveType
        {
            get { return _saveType; }
        }

        public static string LogType
        {
            get
            {
                return _logType;
            }
        }

        public static string SaveFilePath
        {
            get
            {
                return _savePath;
            }
        }

        private static void PrepareConf()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XML_FILE_PATH);
                XmlNodeList saveNodeList = xmlDoc.SelectNodes(SAVE_NODE);

                foreach (XmlNode node in saveNodeList)
                {
                    _logType = node.SelectSingleNode("log-type").InnerText;
                    _saveType = node.SelectSingleNode("save-type").InnerText;
                    _savePath = node.SelectSingleNode("log-path").InnerText;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}