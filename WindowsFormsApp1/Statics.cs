using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public static class Statics
    {
        public static IQueryable<DataRow> WhereByMapping(this IQueryable<DataRow> source, DataRow parentSource, Dictionary<string, string> dictcolumnMapping)
        {
            foreach (var map in dictcolumnMapping)
            {
                source = source.Where(r => parentSource[map.Key] == r[map.Value]);
            }

            return source;
        }
    }
}
