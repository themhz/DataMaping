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
    //Για τα query εδώ http://json.parser.online.fr/
    //Παράδειγμα 
    //{"query":{
    //"select":["PageA.ID", "PageA.Name", "PageA.RecNumber", "PageADetails.Density", "PageADetails.Index"],
    //"from":["PageA", "PageADetails"],
    //"join":["ID", "PageADetailID"],
    //"filter":["PageA.Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)' and PageA.RecNumber > '0004'"]
    //}}
    class DynamicQuery
    {
        public JToken Query { get; set; }
        public JToken Select { get; set; }
        public JToken From { get; set; }
        public JToken Filter { get; set; }
        public JToken Join { get; set; }
        public Xml Xml { get; set; }
        
        public DynamicQuery(Xml xml, string jsonQuery)
        {
            JObject JsonToken = getJson(jsonQuery);
            Query = (JToken)JsonToken["query"];
            Select = (JToken)Query["select"];
            From = (JToken)Query["from"];
            Filter = (JToken)Query["filter"];
            Join = (JToken)Query["join"];
            Xml = xml;
        }
        public DataTable innerJoinTwoTables(Xml xml, string jsonQuery)
        {
            JObject JsonToken = getJson(jsonQuery);

            JToken query = (JToken)JsonToken["query"];
            JToken select = (JToken)query["select"];
            JToken from = (JToken)query["from"];
            JToken filter = (JToken)query["filter"];
            JToken join = (JToken)query["join"];
            
            DataTable Table1 = xml.DataSet.Tables[from[0].ToString()];
            DataTable Table2 = xml.DataSet.Tables[from[1].ToString()];

            DataRow[] where1 = null;
            DataRow[] where2 = null;

            if(filter.Count()>0)
                where1 = Table1.Select(filter[0].ToString().Replace(from[0].ToString()+ ".", ""));
            else
                where1 = Table1.Select("");

            if (filter.Count() > 1)
                where2 = Table2.Select(filter[1].ToString().Replace(from[1].ToString() + ".", ""));
            else
                where2 = Table2.Select("");

            var Query1 = from table1 in where1.AsEnumerable().AsQueryable()
                         join table2 in where2.AsEnumerable() on table1.Field<Guid>("ID") equals table2.Field<Guid>("PageADetailID")
                         select new { table1, table2 };


           
            string selectStatement = "new ("+ getSelects(select, from) + ")";
            IQueryable iq = Query1.Select(selectStatement);


            return LINQToDataTable(iq.AsEnumerable());
        }

       
       
        public DataTable innerJoinThreeTables(Xml xml)
        {
            DataTable Table1 = xml.DataSet.Tables["PageBLevels"];
            DataTable Table2 = xml.DataSet.Tables["HorizontalLevels"];
            DataTable Table3 = xml.DataSet.Tables["HorizontalLevelElements"];

            var where1 = Table1.Select("RecNumber = '01'");
            var where2 = Table2.Select("");
            var where3 = Table3.Select("GroupIndex = '2'");

            var query =  from table1 in where1.AsEnumerable().AsQueryable()
                         join table2 in where2.AsEnumerable() on table1.Field<Guid>("ID") equals table2.Field<Guid>("PageBLevelID")
                         join table3 in where3.AsEnumerable() on table2.Field<Guid>("ID") equals table3.Field<Guid>("HorizontalLevelID")
                         select new { table1, table2, table3 };

            string selectStatement = "new (table1.Name, table1.RecNumber, table1.CategoryName, table2.Name as table2Name, table2.LevelName, table3.IsLeftSideElementEmpty, table3.GroupIndex)";
            IQueryable iq = query.Select(selectStatement);


            return LINQToDataTable(iq.AsEnumerable());
        }

        public DataTable innerJoinFourTables(Xml xml)
        {
            DataTable Table1 = xml.DataSet.Tables["PageBLevels"];
            DataTable Table2 = xml.DataSet.Tables["HorizontalLevels"];
            DataTable Table3 = xml.DataSet.Tables["HorizontalLevelElements"];
            DataTable Table4 = xml.DataSet.Tables["HorizontalLevelElements"];

            var where1 = Table1.Select("RecNumber = '01'");
            var where2 = Table2.Select("");
            var where3 = Table3.Select("GroupIndex = '2'");
            var where4 = Table4.Select("GroupIndex = '2'");

            var query = from table1 in where1.AsEnumerable().AsQueryable()
                        join table2 in where2.AsEnumerable() on table1.Field<Guid>("ID") equals table2.Field<Guid>("PageBLevelID")
                        join table3 in where3.AsEnumerable() on table2.Field<Guid>("ID") equals table3.Field<Guid>("HorizontalLevelID")
                        join table4 in where4.AsEnumerable() on table3.Field<Guid>("ID") equals table4.Field<Guid>("HorizontalLevelID")
                        select new { table1, table2, table3 };


            string selectStatement = "new (table1.Name, table1.RecNumber, table1.CategoryName, table2.Name as table2Name, table2.LevelName, table3.IsLeftSideElementEmpty, table3.GroupIndex)";

            
            return LINQToDataTable(query.Select(selectStatement).AsEnumerable());
        }

        public JObject getJson(string jsonQuery) {

            //Example ://"{\"query\":{\"select\":[\"PageA.ID\", \"PageA.Name\", \"PageA.RecNumber\", \"PageADetails.Density\"],\"from\":[\"PageA\", \"PageADetails\"],\"join\":[\"ID\", \"PageADetailID\"],\"filter\":[\"Name = 'Δοκός σε ενδιάμεσο όροφο  (6cm - Β ζώνη) (Νέο κτήριο)'\", \"Density = '1800'\"]}}";
            string json = jsonQuery;
            

            JObject JsonToken = JObject.Parse(json);
           
            return JsonToken;
        }

        public string getSelects(JToken select, JToken from)
        {
            StringBuilder selectString = new StringBuilder();
            for (int i = 0; i < select.Count(); i++)
            {
                string[] selected = select[i].ToString().Split('.');
                if (selected[0] == from[0].ToString())
                {
                    selectString.Append((i == 0 ? "" : ",") + "table1." + selected[1]);
                }
                else if (selected[0] == from[1].ToString())
                {
                    selectString.Append((i == 0 ? "" : ",") + "table2." + selected[1]);
                }
            }

            return selectString.ToString();
        }

        //https://www.c-sharpcorner.com/uploadfile/VIMAL.LAKHERA/convert-a-linq-query-resultset-to-a-datatable/
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

