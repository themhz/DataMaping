using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic;
using System.Reflection;

namespace WindowsFormsApp1 {
    class Queries
    {
        public delegate int delAdd(int Num1, int Num2);

        public DataTable Start(Xml xml, string jsonQuery)
        {
            //return Test(xml);
            DynamicQuery dq = new DynamicQuery();

            //return dq.innerJoinTwoTables(xml);
            //dq.getJson();
            return dq.innerJoinTwoTables(xml, jsonQuery);
        }


       
       
       
        
    }
}
