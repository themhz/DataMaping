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
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1
{
    class Tests
    {

        public DataTable Test2(Xml xml)
        {
            DataTable Table1 = xml.DataSet.Tables["PageA"];
            DataTable Table2 = xml.DataSet.Tables["PageADetails"];

            Dictionary<string, string> dictcolumnMapping = new Dictionary<string, string>();
            dictcolumnMapping.Add("ID", "PageADetailID");


            var joins = from filerow in Table1.AsEnumerable()
                        from dbrow in Table2.AsEnumerable().AsQueryable().WhereByMapping2(filerow, dictcolumnMapping)
                        select new { dbrow };


            //var result = joins.Select("new (dbrow.ID,dbrow.PageADetailsID)").AsEnumerable().CopyToDataTable();
            return new DataTable();
        }


        public DataTable Test(Xml xml)
        {
            DataTable Table1 = xml.DataSet.Tables["PageA"];
            DataTable Table2 = xml.DataSet.Tables["PageADetails"];

            var result = Table1.Select("Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)'");
            var Query1 = from table1 in result.AsEnumerable().AsQueryable()
                         join table2 in Table2.AsEnumerable() on table1.Field<Guid>("ID") equals table2.Field<Guid>("PageADetailID")
                         select new { table1, table2 };

            string selectStatement = "new (pageA.ID,pageA.Name, pageA.RecNumber, pageADetails.Density)";
            IQueryable iq = Query1.Select(selectStatement);


            return LINQToDataTable(iq.AsEnumerable());
        }

        public DataTable Annex1(Xml xml)
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            var result = from pageA in PageA.AsEnumerable()
                         join pageADetails in PageADetails.AsEnumerable() on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
                         select new
                         {
                             ID = pageA.Field<Guid>("ID"),
                             BuildingID = pageA.Field<Guid>("BuildingID"),
                             TypeID = pageA.Field<Guid>("TypeID"),
                             RecNumber = pageA.Field<String>("RecNumber"),
                             PageA_Name = pageA.Field<String>("Name"),
                             pageADetails_Name = pageADetails.Field<String>("Name")
                         };



            return result.CopyToDataTable();
        }


        public DataTable Annex2(Xml xml)
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];


            IQueryable result = (from pageA in PageA.AsEnumerable()
                                 select new { pageA }).AsQueryable();

            //string selectStatement = "new (pageA.ID, pageA.RecNumber)";
            //string selectStatement2 = "new (pageADetails.ID, pageADetails.Name)";
            string selectStatement3 = "new (pageA.ID, pageA.RecNumber, pageADetails.ID)";

            var Query1 = from pageA in (PageA.AsEnumerable().AsQueryable()
                        .Where(x => x.Field<Guid>("ID") == Guid.Parse("fc12703b-41b8-4c3c-94c1-d9a0c5e83f08")))
                         select new { pageA }
            ;
            var Query2 = from pageADetails in (PageADetails.AsEnumerable().AsQueryable()
                        .Where(x => x.Field<Guid>("PageADetailID") == Guid.Parse("fc12703b-41b8-4c3c-94c1-d9a0c5e83f08")))
                         select new { pageADetails }
            ;

            var Query3 = from pageA in PageA.AsEnumerable().AsQueryable()
                         join pageADetails in PageADetails.AsEnumerable().AsQueryable()
                         .Where(x => x.Field<Guid>("PageADetailID") == Guid.Parse("fc12703b-41b8-4c3c-94c1-d9a0c5e83f08"))
                         on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
                         select new { pageA, pageADetails };



            //return Query3.ToDynamicArray().CopyToDataTable();
            return Query3.Select(selectStatement3).ToDynamicArray().CopyToDataTable();


        }


        public DataTable Annex4(Xml xml)
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];


            Dictionary<string, string> dictcolumnMapping = new Dictionary<string, string>();
            dictcolumnMapping.Add("HeatInsulationRecNumber", "Index");


            var joins = from filerow in PageA.AsEnumerable().AsQueryable()
                        from dbrow in PageADetails.AsEnumerable().AsQueryable().WhereByMapping(filerow, dictcolumnMapping)
                        select new { filerecord = filerow, db = dbrow };


            return joins.CopyToDataTable();
        }

        public DataTable Annex3(Xml xml)
        {
            DataTable PageA = xml.DataSet.Tables["PageA"];
            DataTable PageADetails = xml.DataSet.Tables["PageADetails"];


            var result = (from pageA in PageA.AsEnumerable()
                          join pageADetails in PageADetails.AsEnumerable() on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
                          select new { pageA, pageADetails }).AsQueryable();


            //string selectStatement = " new ( ID = pageA.Field<Guid>(\"ID\"),  PageA_Name = pageA.Field<String>(\"Name\"))";
            string selectStatement = "new (pageA.ID, pageADetails.Name, pageA.RecNumber)";
            IQueryable iq = result.Select(selectStatement);



            return LINQToDataTable(iq.AsEnumerable());

        }

        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 

                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    if (pi.Name.ToString() != "Item")
                    {
                        dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                    }

                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
    }
}
