using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WindowsFormsApp1.classes
{
    public static class XsdHandler
    {

        public static List<TableInfo> GetTablesFromXSD(string xsdFilePath, string schemaName)
        {
            var tables = new List<TableInfo>();

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xsdFilePath);

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");

                XmlNodeList tableNodes = xmlDoc.SelectNodes("//xs:element[@name='"+ schemaName + "']/xs:complexType/xs:choice/xs:element", nsmgr);

                foreach (XmlNode tableNode in tableNodes)
                {
                    var tableInfo = new TableInfo
                    {
                        TableName = tableNode.Attributes["name"].Value,
                        FieldNames = new List<string>()
                    };

                    XmlNodeList fieldNodes = tableNode.SelectNodes("xs:complexType/xs:sequence/xs:element", nsmgr);

                    foreach (XmlNode fieldNode in fieldNodes)
                    {
                        string fieldName = fieldNode.Attributes["name"].Value;
                        tableInfo.FieldNames.Add(fieldName);
                    }

                    tables.Add(tableInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return tables;
        }


        public static List<string> ListXSDFilesInDirectory(string directoryPath)
        {
            List<string> xsdFiles = new List<string>();

            try
            {
                string[] files = Directory.GetFiles(directoryPath, "*.xsd");
                xsdFiles.AddRange(files);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return xsdFiles;
        }
    }
}
