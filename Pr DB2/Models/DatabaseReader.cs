using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Reflection;

namespace Cursova.Models
{
    public static class DatabaseReader
    {
        static public void ExecuteNonQuery(string query)
        {
            using (OdbcConnection connection = new OdbcConnection(Configuration.connectionString))
            {
                OdbcCommand command = new OdbcCommand
                {
                    Connection = connection,
                    CommandText = query
                };
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        static public DataTable ExecuteQuery(string query)
        {
            using (OdbcConnection connection = new OdbcConnection(Configuration.connectionString))
            {
                OdbcCommand command = new OdbcCommand();
                command.Connection = connection;
                command.CommandText = query;
                connection.Open();

                DataTable dt = new DataTable();

                try
                {
                    using (OdbcDataReader reader = command.ExecuteReader())
                    {
                        dt.Load(reader);

                        reader.Close();
                        return dt;
                    }
                }
                catch (Exception)
                {
                    return dt;
                }
            }
        }

        static public int ExecuteScalar(string query)
        {
            using (OdbcConnection connection = new OdbcConnection(Configuration.connectionString))
            {
                OdbcCommand command = new OdbcCommand
                {
                    Connection = connection,
                    CommandText = query
                };
                connection.Open();
                var result = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return result;
            }
        }

        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (DataRow row in table.Rows)
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}
