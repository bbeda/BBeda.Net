using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BBeda.Net.Data.Sql
{
    public class SqlParameterMapper
    {
        public static IEnumerable<SqlParameter> ToSqlParameters(object parameter)
        {
            if (parameter == null)
            {
                yield break;
            }

            var properties = parameter.GetType().GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance).ToList();
            foreach (var prop in properties)
            {
                var name = prop.Name;
                name = "@" + string.Join(string.Empty, name.Take(1).Select(c => char.ToLower(c))) + string.Join(string.Empty, name.Skip(1));
                var value = prop.GetValue(parameter);
                yield return new SqlParameter(name, value ?? DBNull.Value);
            }
        }

        public static string GetParametersString(IEnumerable<SqlParameter> parameters)
        {
            return string.Join(",", parameters.Select(p => p.ParameterName));
        }
    }
}
