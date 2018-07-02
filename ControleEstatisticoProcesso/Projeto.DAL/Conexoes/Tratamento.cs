using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.DAL.Repositorio
{
    public static class Tratamento
    {
        public static SqlParameter AddWithNullValue(this SqlParameterCollection collection, string parameterName, object value)
        {
            return value == null ? collection.AddWithValue(parameterName, DBNull.Value) : collection.AddWithValue(parameterName, value);
        }
    }
}
