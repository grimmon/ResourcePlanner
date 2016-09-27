
using ResourcePlanner.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ResourcePlanner.Services.Utilities
{
    public class AdoUtility
    {

        public static SqlParameter CreateSqlParameter(string parameterName, SqlDbType sqlDbType, object value)
        {
            return new SqlParameter
            {
                ParameterName = parameterName,
                SqlDbType = sqlDbType,
                Value = value ?? DBNull.Value,
            };
        }

        public static T ExecuteQuery<T>(Func<SqlDataReader, T> resultAction, string connectionString, string sqlStatement, CommandType type, int timeout, params SqlParameter[] parameters)
        {
            T returnValue;
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandTimeout = timeout;
                cmd.CommandText = sqlStatement;
                cmd.CommandType = type;
                parameters.ForEach(i => cmd.Parameters.Add(i));

#if DEBUG
                try
                {
                    var queryText = SqlQueryToString(sqlStatement, parameters);
#endif
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        try
                        {
                            returnValue = resultAction(reader);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("callback error", ex);
                        }
                    }
#if DEBUG
                }
                catch (Exception ex)
                {
                    throw GenerateSqlError(sqlStatement, parameters, ex);
                }
                finally
                {
                    conn.Close();
                }
#endif
            }
            return returnValue;
        }
         public static string SqlQueryToString(string sqlStatement, SqlParameter[] parameters)
         {
                var result = sqlStatement + "; \nParameters:\n";

                foreach (var parameter in parameters)
                {
                    result += SqlParameterToString(parameter) + "\n";
                }

                return result;
         }
        public static string SqlParameterToString(SqlParameter parameter)
        {
            var result = parameter.ParameterName;

            var valueTypeFormat = "({0}): ";
            var valueFormat = "\t{0}";

            if (parameter.Value is DataTable)
            {
                result += string.Format(valueTypeFormat, parameter.TypeName);

                var data = (DataTable)parameter.Value;

                foreach (var datum in data.Rows)
                {
                    foreach (var value in ((DataRow)datum).ItemArray)
                    {
                        result += string.Format(valueFormat, value);
                    }
                }
            }
            else
            {
                result += string.Format(valueTypeFormat, parameter.SqlDbType);
                result += string.Format(valueFormat, parameter.Value);
            }

            return result;
        }

        private static Exception GenerateSqlError(string sqlStatement, SqlParameter[] parameters, Exception ex)
        {
            if (ex.Message == "callback error")
            {
                throw new Exception("Callback Error for " + sqlStatement + "; " + ex.InnerException.Message, ex.InnerException);
            }

            var errorMessage = ex.Message + ":\n" + SqlQueryToString(sqlStatement, parameters);

            return new Exception(errorMessage);
        }

    }



}