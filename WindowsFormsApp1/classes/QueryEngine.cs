using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp1.forms.Childs
{
    public class QueryEngine
    {
        public DataTable ExecuteQuery(string strJson, Xml xml)
        {
            JObject json = JObject.Parse(strJson);
            string from = json["from"].ToString();
            DataTable selector = xml.DataSet.Tables[from];
            DataTable result = this.Select(selector);

            result = parseJoins(xml, json, from, result);

            string where = "";
            if (json["where"] != null)
                where = json["where"].ToString();
            DataTable dt = result;

            //Adding index to the table
            if (!dt.Columns.Contains("Rows"))
            {
                dt.Columns.Add("Rows", System.Type.GetType("System.Int32"));
                
            }

            dt.Columns["Rows"].SetOrdinal(0);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["Rows"] = i + 1;
            }

            dt = dt.Select(where).CopyToDataTable();

            dt = this.FilterColumns(dt, json["select"].ToString().Trim().Replace("[", "").Replace("]", "").Replace("\r\n", "").Replace("\"", "").Trim());
            if (json["sort"] != null)
            {
                string sort = json["sort"].ToString();
                dt.DefaultView.Sort = sort;
            }

            

            return dt;
        }

        private DataTable parseJoins(Xml xml, JObject json, string from, DataTable result)
        {
            JToken joins = json["join"];

            if (joins != null)
            {
                for (int i = 0; i < joins.Count(); i++)
                {
                    if (i == 0)
                    {
                        if (joins[i][0].ToString().Split('.')[0].ToString() == from)
                        {
                            DataTable Table1 = xml.DataSet.Tables[joins[i][0].ToString().Split('.')[0].ToString()];
                            DataTable Table2 = xml.DataSet.Tables[joins[i][1].ToString().Split('.')[0].ToString()];
                            result = this.Join(Table1, Table2, joins[i][0].ToString().Split('.')[1] + "=" + joins[i][1].ToString().Split('.')[1], "");
                        }
                        else if (joins[i][1].ToString().Split('.')[0].ToString() == from)
                        {
                            DataTable Table1 = xml.DataSet.Tables[joins[i][1].ToString().Split('.')[0].ToString()];
                            DataTable Table2 = xml.DataSet.Tables[joins[i][0].ToString().Split('.')[0].ToString()];
                            result = this.Join(Table1, Table2, joins[i][1].ToString().Split('.')[1] + "=" + joins[i][0].ToString().Split('.')[1], "");
                        }
                        else
                        {
                            //there was an error in your select
                        }

                    }
                    else
                    {
                        string table1 = joins[i][0].ToString().Split('.')[0].ToString();
                        string table1Key = joins[i][0].ToString().Split('.')[1].ToString();
                        string table2 = joins[i][1].ToString().Split('.')[0].ToString();
                        string table2Key = joins[i][1].ToString().Split('.')[1].ToString();


                        DataTable table = xml.DataSet.Tables[table1];

                        if (joins[i - 1][0].ToString().Split('.')[0].ToString() == table2)
                        {
                            result = this.Join(result, table, table2 + "_" + table2Key + "=" + table1Key, "");
                        }
                        else
                        {
                            table = xml.DataSet.Tables[table2];
                            result = this.Join(result, table, table1 + "_" + table1Key + "=" + table2Key, "");
                        }
                    }
                }
                foreach (DataColumn col in result.Columns)
                {
                    col.ColumnName = col.ColumnName.Replace("EYABYMSJMZUWPRZZVRSBZZZZ_", "");
                }
            }

            return result;
        }
        public DataTable Select(DataTable table)
        {

            return table;
        }
        public DataTable Join(DataTable Table1, DataTable Table2, string joinFields, string filter)
        {
            joinFields = joinFields.Replace(".", "_");
            string[] joins = joinFields.Split('=');

            var Query1 = from table1 in Table1.AsEnumerable()
                         join table2 in Table2.AsEnumerable() on table1.Field<Guid>(joins[0]) equals table2.Field<Guid>(joins[1])
                         select new { table1, table2 };

            string columns = SelectColumns(Table1, Table2);
            string selectStatement = "new (" + columns + ")";

            IQueryable iq = Query1.AsQueryable().Select(selectStatement);
            DataTable dt = LINQToDataTable(iq.AsEnumerable());
            dt.TableName = "EYABYMSJMZUWPRZZVRSBZZZZ";

            return dt;
        }
        private static string SelectColumns(DataTable Table1, DataTable Table2)
        {
            string columns = "";
            int i = 0;
            foreach (DataColumn colName in Table1.Columns)
            {
                if (colName.ColumnName.ToLower() != "item")
                {
                    if (i == 0)
                        columns = "table1." + colName.ToString() + " as " + Table1.TableName + "_" + colName.ColumnName;
                    else
                        columns += ",table1." + colName.ToString() + " as " + Table1.TableName + "_" + colName.ColumnName;
                }
                i++;
            }

            foreach (DataColumn colName in Table2.Columns)
            {
                columns += ",table2." + colName.ToString() + " as " + Table2.TableName + "_" + colName.ToString();
            }

            return columns;
        }
        public DataTable FilterColumns(DataTable Table, String select="")
        {
            var query = from table in Table.AsEnumerable().AsQueryable()
                        select new { table };

            if (select.Trim() != "*" && select.Trim() != "")
            {                
                string[] columns = select.Split(',');
                string selectStatement = "new (";
                for (int i = 0; i < columns.Length; i++)
                {
                    if (columns[i].ToString() != "Item")
                    {
                        if (i == 0)
                        {
                            selectStatement += "table." + columns[i].Replace("\r\n", "").Replace("\"", "").Trim();

                        }
                        else
                        {
                            selectStatement += "," + "table." + columns[i].Replace("\r\n", "").Replace("\"", "").Trim();
                        }
                    }

                }
                selectStatement += ")";
                return LINQToDataTable(query.Select(selectStatement).AsEnumerable());
            }
            


            return Table;
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
                        if (pi.Name.ToString() != "Item")
                        {
                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                        }
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
