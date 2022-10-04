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

namespace WindowsFormsApp1.forms.Childs
{
    public class QueryEngine
    {
        public DataTable Select(DataTable table, string filter)
        {
            var Query1 = from table1 in table.AsEnumerable()                         
                         select new { table1 };
            
            DataTable dt = table.Select(filter).CopyToDataTable();
            dt.TableName = dt.TableName;
            return dt;
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
