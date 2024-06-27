using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.classes
{
    public static class GlobalVariables
    {
        public static string XmlPath { get; set; }
        public static string XsdPath { get; set; }
        public static Xml Xml { get; set; }
        public static List<string> tables { get; set; }
    }

}
