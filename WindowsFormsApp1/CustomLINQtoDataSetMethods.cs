using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1 {
    public static class CustomLINQtoDataSetMethods {
        public static DataTable CopyToDataTable2<T>(this IEnumerable<T> source) {
            return new ObjectShredder<T>().Shred(source, null, null);
        }

        public static DataTable CopyToDataTable2<T>(this IEnumerable<T> source,
                                                    DataTable table, LoadOption? options) {
            return new ObjectShredder<T>().Shred(source, table, options);
        }
    }
}
