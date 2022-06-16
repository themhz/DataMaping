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

        public DataTable convertListToDataTable<T>(List<T> list)
        {
           if (list is null)
           {
               throw new ArgumentNullException(nameof(list));
           }

           DataTable dt = new DataTable();
           
            //foreach(var title in list.)
            //dt.Columns.Add(list.)

           foreach(var item in list)
           {
                
           }

           return dt;
        }
    }
}
