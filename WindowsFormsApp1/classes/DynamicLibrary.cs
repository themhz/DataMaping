//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Linq.Dynamic.Core;
using System.Data;

namespace System.Linq.Dynamic
{
    public static class DynamicQueryableExtender
    {

        
        public static IQueryable<DataRow> WhereByMapping2(this IQueryable<DataRow> source, DataRow parentSource, Dictionary<string, string> dictcolumnMapping)
        {
            foreach (var map in dictcolumnMapping)
            {
                source = source.Where(r => parentSource[map.Key] == r[map.Value]);
            }

            return source;
        }

    }


}
