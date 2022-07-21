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
            DynamicQuery dq = new DynamicQuery(xml, jsonQuery);
            var joins = dq.Join;
            if (joins != null)
            {
                switch (joins.Count())
                {
                    case 0:
                        return dq.SimpleSelect();
                    case 1:
                        return dq.InnerJoinTwoTables();
                    case 2:

                    case 3:

                    case 4:

                    case 5:

                    case 6:

                    case 7:

                    case 8:
                        break;
                }
            }
            else
            {
                return dq.SimpleSelect();
            }
            


            return new DataTable();
        }


        





    }
}
