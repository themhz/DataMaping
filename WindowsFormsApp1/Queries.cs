using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic;

namespace WindowsFormsApp1 {
    class Queries {
        public delegate int delAdd(int Num1, int Num2);

        public DataTable Anex1(Xml xml) {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            var result = from pageA in PageA.AsEnumerable()
                         join pageADetails in PageADetails.AsEnumerable()
                         on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
                         where pageA.Field<String>("Name") == "Διπλή δρομική-ορθοδρομική οπτοπλινθοδομή (6cm - Β ζώνη) (Νέο κτήριο)"
                         select new {
                             ID = pageA.Field<Guid>("ID"),
                             BuildingID = pageA.Field<Guid>("BuildingID"),
                             TypeID = pageA.Field<Guid>("TypeID"),
                             RecNumber = pageA.Field<String>("RecNumber"),
                             PageA_Name = pageA.Field<String>("Name"),
                             pageADetails_Name = pageADetails.Field<String>("Name")
                         };



            return result.CopyToDataTable();
        }

      



    }
}
