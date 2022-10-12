using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Xml
    {
        public string XmlPath;
        public string XsdPath;
        public DataSet DataSet;

        public Xml(string xmlPath, string xsdPath)
        {
            XmlPath = xmlPath;
            XsdPath = xsdPath;
            ConvertXmlToDataset();
        }

        public Xml()
        {
            

            //XmlPath = @"C:\Users\themis\Desktop\test\dataHeatInsulation.xml";
            //XsdPath = @"C:\Users\themis\Desktop\test\dsBuildingHeatInsulation.xsd";

            XmlPath = ConfigurationManager.AppSettings["XmlPath"];
            XsdPath = ConfigurationManager.AppSettings["XsdPath"];

            ConvertXmlToDataset();
        }

        public void setXml(string xmlPath)
        {
            XmlPath = xmlPath;            
        }

        public void setXsd(string xsdPath)
        {
            XsdPath = xsdPath;            
        }

        public void reload()
        {
            ConvertXmlToDataset();
        }

        private void ConvertXmlToDataset()
        {
            DataSet = new DataSet();
            DataSet.ReadXmlSchema(XsdPath);
            DataSet.ReadXml(XmlPath);
            
        }
        public DataSet GetDataSet()
        {
            return DataSet;
        }

  
    }
}