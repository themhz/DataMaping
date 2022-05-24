using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Xml
    {
        public string XmlPath;
        public string XsdPath;
        public DataSet DataSet;
        public Xml(string xmlPath, string xsdPath)
        {
            XmlPath = xmlPath;
            XsdPath = xsdPath;
            convertXmlToDataset();
        }

        private void convertXmlToDataset()
        {
            DataSet = new DataSet();
            DataSet.ReadXmlSchema(XsdPath);
            DataSet.ReadXml(XmlPath);
        }
        public DataSet getDataSet()
        {
            return DataSet;
        }
    }
}
