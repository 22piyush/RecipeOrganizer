using System.Data;

namespace RecipeOrganizer.Infrastructure.Query
{
    internal class SQLHelper
    {
        //private SqlConnection connection;

        ///// <summary>
        ///// Function use for initialised SqlConnection and open sql connetion.
        ///// </summary>
        //private SqlConnection OpenSqlConnection(string connectionString)
        //{
        //    if (connection != null && connection.ConnectionString.Equals(connectionString))
        //    {
        //        return connection;
        //    }
        //    else
        //    {
        //        CloseSqlConnection();
        //    }

        //    connection = new SqlConnection(connectionString);
        //    connection.Open();
        //    return connection;
        //}

        ///// <summary>
        /////  Function used for close sql connetion.
        ///// </summary>
        //public void CloseSqlConnection()
        //{
        //    if (connection != null)
        //        connection.Close();
        //}

        ///// <summary>
        ///// Executes the query.
        ///// </summary>
        ///// <returns>The query.</returns>
        ///// <param name="query">Query.</param>
        //public SqlDataReader ExecuteQuery(string query, string connectionString)
        //{

        //    connection = OpenSqlConnection(connectionString);
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Connection = connection; //Pass the connection object to Command   


        //    return command.ExecuteReader();
        //}

        //public async Task<SqlDataReader> ExecuteQueryAsync(string query, string connectionString)
        //{
        //    var connection = new SqlConnection(connectionString);
        //    await connection.OpenAsync();

        //    var command = new SqlCommand(query, connection);

        //    // Important: CommandBehavior.CloseConnection
        //    // ensures connection closes when reader is disposed
        //    return await command.ExecuteReaderAsync(
        //        CommandBehavior.CloseConnection);
        //}

        //public object ExecuteScalar(string query, string connectionString)
        //{
        //    connection = OpenSqlConnection(connectionString);
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Connection = connection; //Pass the connection object to Command   
        //    var retVal = command.ExecuteScalar();
        //    return retVal;
        //}

        //public int ExecuteNonQuery(string query, string connectionString)
        //{

        //    connection = OpenSqlConnection(connectionString);
        //    SqlCommand command = new SqlCommand(query, connection);
        //    command.Connection = connection; //Pass the connection object to Command   
        //    return command.ExecuteNonQuery();
        //}

        //public async Task<int> ExecuteNonQueryAsync(string query, string connectionString)
        //{
        //    connection = OpenSqlConnection(connectionString);
        //    using (SqlCommand command = new SqlCommand(query, connection))
        //    {
        //        return await command.ExecuteNonQueryAsync();
        //    }

        //}

        //public static string GetStringValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        if (reader[columnName] != DBNull.Value)
        //            return Convert.ToString(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return "";
        //}
        //public static long GetLongValue(SqlDataReader reader, string columnName)
        //{ try { return (long)reader[columnName]; } catch (Exception ex) { Console.WriteLine(ex); } return 0; }
        //public static Guid GetGuidValue(SqlDataReader reader, string columnName)
        //{ try { return (Guid)reader[columnName]; } catch (Exception ex) { Console.WriteLine(ex); } return Guid.Empty; }

        //public static bool GetBitValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        if (reader[columnName] != DBNull.Value)
        //            return Convert.ToBoolean(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return false;
        //}

        //public static string GetNumberStringValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        return Convert.ToString(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Console.WriteLine(ex);
        //    }
        //    return "";
        //}
        //public static int GetIntValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        if (reader[columnName] != DBNull.Value)
        //            return Convert.ToInt32(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return 0;
        //}


        //public static object GetDateValue(SqlDataReader reader, string column)
        //{

        //    try
        //    {
        //        if (reader[column] != DBNull.Value && (DateTime)reader[column] != DateTime.MinValue)
        //            return (DateTime)reader[column];
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //    return null;
        //}

        //public static double GetDoubleValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        if (reader[columnName] != DBNull.Value)
        //            return Convert.ToDouble(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return 0;
        //}

        //public static decimal GetDecimalValue(SqlDataReader reader, string columnName)
        //{
        //    try
        //    {
        //        if (reader[columnName] != DBNull.Value)
        //            return Convert.ToDecimal(reader[columnName]);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return 0;
        //}
    }
}

