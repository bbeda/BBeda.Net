using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBeda.Net.Data.EntityFramework.Sql
{
    public class DataContext : DbContext
    {
        protected DataContext()
        {
        }

        public DataContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DataContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
        }

        public DataContext(ObjectContext objectContext, bool dbContextOwnsObjectContext) : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        public DataContext(string nameOrConnectionString, DbCompiledModel model) : base(nameOrConnectionString, model)
        {
        }

        public DataContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection) : base(existingConnection, model, contextOwnsConnection)
        {
        }

        protected DataContext(DbCompiledModel model) : base(model)
        {
        }

        protected async Task<TElement> ExecuteSingleElementStoredProcedureAsync<TElement>(string storeProcName, object parameters = null)
        {
            return (await this.ExecuteIEnumerableStoredProcedureAsync<TElement>(storeProcName, parameters)).FirstOrDefault();
        }

        protected async Task<IEnumerable<TElement>> ExecuteIEnumerableStoredProcedureAsync<TElement>(string storeProcName, object parameters = null)
        {
            return await DataContextHelper.ExecuteStoredProcedure<TElement>(this, storeProcName, parameters).ToListAsync();
        }

        protected async Task ExecuteStoredProcedureAsync(string storeProcName, object parameters = null)
        {
            (await this.ExecuteIEnumerableStoredProcedureAsync<object>(storeProcName, parameters)).FirstOrDefault();
        }
    }
}
