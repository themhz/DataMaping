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
using System.Security;


namespace WindowsFormsApp1
{
    class Tests
    {
        public DataTable Select(DataTable Table1, DataTable Table2, string joinFields, string filter, int flag)
        {
            
            string[] joins = joinFields.Split('=');
            //var result = Table1.Select("Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)'");
            //var result = Table1;
            var Query1 = from table1 in Table1.AsEnumerable()
                         join table2 in Table2.AsEnumerable() on table1.Field<Guid>(joins[0]) equals table2.Field<Guid>(joins[1])
                         select new { table1, table2 };

            string columns = "";
            int i = 0;
            foreach (DataColumn colName in Table1.Columns)
            {
                if(colName.ColumnName.ToLower() != "item")
                {
                    if (i == 0)
                        columns = "table1." + colName.ToString() + " as " + Table1.TableName + "_" + colName.ColumnName;
                    else
                        columns += ",table1." + colName.ToString() + " as " + Table1.TableName + "_" + colName.ColumnName;

                    if (flag == 1 && i == 75)
                        break;
                }                
                i++;
            }
            
            foreach (DataColumn colName in Table2.Columns)
            {                
                    columns += ",table2." + colName.ToString() + " as " + Table2.TableName + "_" + colName.ToString();             
            }


            string selectStatement = "new ("+ columns + ")";
            //string selectStatement = "new (table1.ID, table1.BuildingID, table1.TypeID, table1.RecNumber, table2.ID as table2_ID )";

            IQueryable iq = Query1.AsQueryable().Select(selectStatement);

            DataTable dt = LINQToDataTable(iq.AsEnumerable());
            //DataTable dt = iq.AsEnumerable().CopyToDataTable2();
            dt.TableName = Table1.TableName + "_" + Table2.TableName;
           
            return dt;
        }

        public DataTable Test4(Xml xml)
        {
            DataTable Table1 = xml.DataSet.Tables["PageA"];
            DataTable Table2 = xml.DataSet.Tables["PageADetails"];

            var result = Table1.Select("Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)'");
            var Query1 = from table1 in result.AsEnumerable().AsQueryable()
                         join table2 in Table2.AsEnumerable() on table1.Field<Guid>("ID") equals table2.Field<Guid>("PageADetailID")
                         select new { table1, table2 };

            string selectStatement = "new (table1.ID,table1.Name, table1.RecNumber, table2.Density)";
            IQueryable iq = Query1.Select(selectStatement);


            return LINQToDataTable(iq.AsEnumerable());
        }


        public DataTable Test3(Xml xml)
        {
            //DataTable PageA = xml.DataSet.Tables["PageA"];
            //DataTable PageADetails = xml.DataSet.Tables["PageADetails"];

            DataTable PageBLevels = xml.DataSet.Tables["PageBLevels"];
            DataTable PageAOpeningsPerLevel = xml.DataSet.Tables["PageAOpeningsPerLevel"];
            DataTable PageAOpenings = xml.DataSet.Tables["PageAOpenings"];
            

            var result = JoinDataTable(PageBLevels, PageAOpeningsPerLevel, "ID=PageBLevelID");

            //var result = JoinDataTable(PageA, PageADetails, "ID=PageADetailID");
            return result;

            //var result = (from pageA in PageA.AsEnumerable()
            //              join pageADetails in PageADetails.AsEnumerable() on pageA.Field<Guid>("ID") equals pageADetails.Field<Guid>("PageADetailID")
            //              select new { pageA, pageADetails }).AsQueryable();


            ////string selectStatement = " new ( ID = pageA.Field<Guid>(\"ID\"),  PageA_Name = pageA.Field<String>(\"Name\"))";
            //string selectStatement = "new (pageA.ID, pageADetails.Name, pageA.RecNumber)";
            //IQueryable iq = result.Select(selectStatement);



            //return LINQToDataTable(iq.AsEnumerable());


            //DataTable Table1 = xml.DataSet.Tables["PageA"];
            //DataTable Table2 = xml.DataSet.Tables["PageADetails"];

            //var results = Table1.Select().Join(Table2.Select(), t1 => (Guid)t1["ID"], t2 => (Guid)t2["PageADetailID"], (t1, t2) => new
            //{
            //    DestinationRow = t1,
            //    SourceRow = t2
            //}).ToList().CopyToDataTable2();



            //Dictionary<string, string> dictcolumnMapping = new Dictionary<string, string>();
            //dictcolumnMapping.Add("ID", "PageADetailID");


            //var joins = from filerow in Table1.AsEnumerable()
            //            from dbrow in Table2.AsEnumerable().AsQueryable().WhereByMapping2(filerow, dictcolumnMapping)
            //            select new { dbrow };


            ////var result = Table1.Select("TypeID = '371b2510-ddf1-463f-8aba-74c184599195'");

            //var result = JoinDataTable(Table1, Table2, "ID=PageADetailID");


            //return results.CopyToDataTable3();
        }

        public DataTable JoinDataTable(DataTable dataTable1, DataTable dataTable2, string joinField)
        {
            string[] joins = joinField.Split('=');

            var dt = new DataTable();
            var joinTable = from t1 in dataTable1.AsEnumerable()
                            join t2 in dataTable2.AsEnumerable()
                                on t1[joins[0]] equals t2[joins[1]]
                            select new { t1, t2 };

            foreach (DataColumn col in dataTable1.Columns)
                dt.Columns.Add(dataTable1.TableName+"."+col.ColumnName, typeof(Object));
            

            foreach (DataColumn col in dataTable2.Columns)
                dt.Columns.Add(dataTable2.TableName + "." + col.ColumnName, typeof(Object));                                
                

            foreach (var row in joinTable)
            {
                var newRow = dt.NewRow();
                newRow.ItemArray = row.t1.ItemArray.Union(row.t2.ItemArray).ToArray();
                

                dt.Rows.Add(newRow);
            }
            return dt;
        }

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



            return result.CopyToDataTable2();
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
            return Query3.Select(selectStatement3).ToDynamicArray().CopyToDataTable2();


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


            return joins.CopyToDataTable2();
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
