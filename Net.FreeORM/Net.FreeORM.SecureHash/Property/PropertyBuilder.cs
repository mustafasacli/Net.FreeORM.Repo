using Net.FreeORM.SecureHash.Encryption;
using Net.FreeORM.SecureHash.Test;
using System;
using System.Xml;

namespace Net.FreeORM.SecureHash.Property
{
    internal class PropertyBuilder
    {

        public static string Build(ConnectionTypes connType, string propertyName, string strProperty)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(strProperty) == true)
                    throw new Exception("StrProperty is null or empty.");

                string strConnType = GetConnectionType(connType);

                string[] crypted = strProperty.Split(';', '=');

                for (int i = 0; i < crypted.Length; i++)
                {
                    crypted[i] = Encryptor.EncodeString(crypted[i]);
                }
                XmlDocument xmlDoc = new XmlDocument();

                XmlNode mainNode = CreateNode(xmlDoc, "properties", "name", "driver-type", "conn-str");
                mainNode.Attributes["name"].InnerText = propertyName;
                mainNode.Attributes["driver-type"].InnerText = strConnType;
                mainNode.Attributes["conn-str"].InnerText = strProperty;

                XmlNode childNode = null;
                for (int cryptedCounter = 0; cryptedCounter < crypted.Length - 1; cryptedCounter += 2)
                {
                    childNode = CreateNode(xmlDoc, "property", "key", "value");
                    childNode.Attributes["key"].InnerText = crypted[cryptedCounter];
                    childNode.Attributes["value"].InnerText = crypted[cryptedCounter + 1];
                    mainNode.AppendChild(childNode);
                }
                childNode = null;
                xmlDoc.AppendChild(mainNode);
                return xmlDoc.OuterXml;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static XmlNode CreateNode(XmlDocument xml_doc, string nodeName, params object[] attrs)
        {
            try
            {
                XmlNode node = xml_doc.CreateElement(nodeName);

                XmlAttribute attr;
                foreach (object obj in attrs)
                {
                    if (String.Format("{0}", obj).Length > 0)
                    {
                        attr = xml_doc.CreateAttribute(obj.ToString());
                        node.Attributes.Append(attr);
                    }
                }

                return node;
            }
            catch (Exception)
            {
                throw;
            }
        }

        internal static string GetConnectionType(ConnectionTypes connType)
        {
            switch (connType)
            {
                default:
                    return string.Empty;

                case ConnectionTypes.SqlExpress:
                    return "sqlexpress";

                case ConnectionTypes.SqlServer:
                    return "sqlserver";

                case ConnectionTypes.PostgreSQL:
                    return "postgresql";

                case ConnectionTypes.DB2:
                    return "db2";

                case ConnectionTypes.OracleNet:
                    return "oracle";

                case ConnectionTypes.OracleManaged:
                    return "oracle-managed";

                case ConnectionTypes.MySQL:
                    return "mysql";

                case ConnectionTypes.MariaDB:
                    return "mariadb";

                case ConnectionTypes.VistaDB:
                    return "vistadb";

                case ConnectionTypes.OleDb:
                    return "oledb";

                case ConnectionTypes.SQLite:
                    return "sqlite";

                case ConnectionTypes.FireBird:
                    return "firebird";

                case ConnectionTypes.SqlServerCe:
                    return "sqlserverce";

                case ConnectionTypes.Sybase:
                    return "sybase";

                case ConnectionTypes.Informix:
                    return "informix";

                case ConnectionTypes.U2:
                    return "u2";

                case ConnectionTypes.Synergy:
                    return "synergy";

                case ConnectionTypes.Ingres:
                    return "ingres";

                case ConnectionTypes.SqlBase:
                    return "sqlbase";

                case ConnectionTypes.Odbc:
                    return "odbc";
            }
        }
    }
}
