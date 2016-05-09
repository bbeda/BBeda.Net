using BBeda.Net.Data.Sql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBeda.Net.Data.EntityFramework.Sql
{
    public static class DataContextHelper
    {
        public static DbRawSqlQuery<TElement> ExecuteStoredProcedure<TElement>(DbContext context, string storeProcName, object parameters = null)
        {
            if (parameters == null)
            {
                return context.Database.SqlQuery<TElement>(string.Format("EXEC {0}", storeProcName));
            }

            var sqlParams = SqlParameterMapper.ToSqlParameters(parameters).ToArray();
            var paramsString = SqlParameterMapper.GetParametersString(sqlParams);
            return context.Database.SqlQuery<TElement>(string.Format("EXEC {0} {1}", storeProcName, paramsString), sqlParams);
        }

        public static async Task<TElement> ExecuteSingleElementStoredProcedure<TElement>(DbContext context, string storeProcName, object parameters = null)
        {
            return (await ExecuteIEnumerableStoredProcedure<TElement>(context, storeProcName, parameters)).FirstOrDefault();
        }

        public static async Task<IEnumerable<TElement>> ExecuteIEnumerableStoredProcedure<TElement>(DbContext context, string storeProcName, object parameters = null)
        {
            return await ExecuteStoredProcedure<TElement>(context, storeProcName, parameters).ToListAsync();
        }

        public static async Task ExecuteStoredProcedure(DbContext context, string storeProcName, object parameters = null)
        {
            (await ExecuteIEnumerableStoredProcedure<object>(context, storeProcName, parameters)).FirstOrDefault();
        }
    }
}
