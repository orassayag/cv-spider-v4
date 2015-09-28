using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CVSpider.Code
{
    public class DbUtilsDal
    {
        public const string MainDB = "MailDB";

        /*Description:
        */
        private DbUtilsDal() { }

        /*Description:
        */
        public static SqlConnection SetConnection(string connectionName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
            SqlConnection connection = null;
            if (!string.IsNullOrEmpty(connectionString))
            {
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }

        /*Description:
        */
        public static SqlConnection OpenConnection(string connectionName)
        {
            SqlConnection connection = SetConnection(connectionName);
            if (connection != null && connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        /*Description:
        */
        public static SqlConnection SetConnectionWithConnectionString(string connectionString)
        {
            SqlConnection connection = null;
            if (!string.IsNullOrEmpty(connectionString))
            {
                connection = new SqlConnection(connectionString);
            }
            return connection;
        }

        /*Description:
        */
        public static SqlConnection OpenConnectionWithConnectionString(string connectionString)
        {
            SqlConnection connection = SetConnectionWithConnectionString(connectionString);
            if (connection != null && connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        /*Description:
        */
        public static SqlCommand CreateCommand(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandType = commandType;
            cmd.CommandText = commandText;
            for (int index = 0; index < parametersNames.Length; index++)
            {
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = parametersNames[index],
                    SqlDbType = parametersTypes[index],
                    SqlValue = parametersValues[index]
                });
            }
            #if DEBUG
                cmd.CommandTimeout = 0;
            #endif
            return cmd;
        }

        #region Execute Non Query

        /// <summary>
        /// Sends a stored procedure command to SqlCommand.ExecuteNonQuery method
        /// </summary>
        /// <param name="connection">SqlConnection to connect to</param>
        /// <param name="procedureName">Sql stored procedure name</param>
        /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
        /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
        /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            return ExecuteNonQuery(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
        }

        /*Description:
        */
        public static int ExecuteNonQuery(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
            #if DEBUG
                cmd.CommandTimeout = 0;
            #endif
            return cmd.ExecuteNonQuery();
        }

        #endregion

        #region Execute Data Table

        /// <summary>
        /// Sends a stored procedure command and returns a DataTable object
        /// </summary>
        /// <param name="connection">SqlConnection to connect to</param>
        /// <param name="procedureName">Sql stored procedure name</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(SqlConnection connection, string procedureName, bool noTimeOut = false)
        {
            return ExecuteDataTable(connection, CommandType.StoredProcedure, procedureName, new string[] { }, new SqlDbType[] { }, new object[] { }, noTimeOut);
        }

        /// <summary>
        /// Sends a stored procedure command and returns a DataTable object
        /// </summary>
        /// <param name="connection">SqlConnection to connect to</param>
        /// <param name="procedureName">Sql stored procedure name</param>
        /// <param name="parametersNames">An array of paraneter names to pass to the SqlCommand object</param>
        /// <param name="parametersTypes">An array of paraneter types to pass to the SqlCommand object</param>
        /// <param name="parametersValues">An array of paraneter values to pass to the SqlCommand object</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            return ExecuteDataTable(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
        }

        /*Description:
        */
        public static DataTable ExecuteDataTable(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues, bool noTimeOut = false)
        {
            SqlCommand cmd = CreateCommand(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
            if (noTimeOut)
            {
                cmd.CommandTimeout = 0;
            }

            #if DEBUG
                cmd.CommandTimeout = 0;
            #endif

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable returnedTable = new DataTable();
            adapter.Fill(returnedTable);
            return returnedTable;
        }

        #endregion

        #region Execute Data Row

        /*Description:
        */
        public static DataRow ExecuteDataRow(SqlConnection connection, string procedureName, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            return ExecuteDataRow(connection, CommandType.StoredProcedure, procedureName, parametersNames, parametersTypes, parametersValues);
        }

        /*Description:
        */
        public static DataRow ExecuteDataRow(SqlConnection connection, CommandType commandType, string commandText, string[] parametersNames, SqlDbType[] parametersTypes, object[] parametersValues)
        {
            DataTable table = ExecuteDataTable(connection, commandType, commandText, parametersNames, parametersTypes, parametersValues);
            if (table.Rows.Count > 0)
            {
                return table.Rows[0];
            }
            return null;
        }

        #endregion
    }
}