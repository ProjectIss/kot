using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Configuration;
using RestarentService.Models;

namespace RestarentService
{
    public class HandlerAccess
    {


        private static SqlConnection m_connection;

        public static void OpenConnection()
        {
            try
            {
                string sConnectionString = string.Empty;
                sConnectionString = ConfigurationManager.ConnectionStrings["issModel"].ToString();
                string sconnection = sConnectionString;
                m_connection = new SqlConnection(sConnectionString);
                m_connection.Open();
            }
            catch (Exception err)
            {
                throw new Exception("Can't open connection \r\n" + err.Message.ToString());
            }
        }
        public SqlConnection sqlConnection()

        {

            SqlConnection Connection = new SqlConnection();

            try
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings["issModel"].ToString();

                Connection.ConnectionString = ConnectionString;
                if (Connection.State == ConnectionState.Open)
                {

                    Connection.Close();

                    Connection.Dispose();

                }

                Connection.Open();

            }

            catch (Exception e)
            {

            }

            return Connection;

        }



        public static void CloseConnection()
        {
            try
            {
                m_connection.Close();
            }
            catch (Exception err)
            {
                throw new Exception("Can't close the connection \r\n" + err.Message.ToString());
            }
        }


        /// <summary>
        ///  To execute the Sql Statement
        ///  Executing Insert/Update/Delete statements
        /// <param name="sSql">SQL Statement</param>
        /// </summary>
        public static void ExecuteCommand(string sSql, string sConnectionString)
        {
            try
            {
                OpenConnection();
                SqlCommand command = getCommand(sSql);
                command.ExecuteNonQuery();
                command = null;
                CloseConnection();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   " + sSql);
            }
        }


        /// <summary>
        ///  To read single column value
        /// <param name="SQL">SQL Statement</param>
        /// </summary>
        public static object ExecuteScalar(string SQL, string sConnectionString)
        {

            try
            {
                OpenConnection();
                SqlCommand command = getCommand(SQL);
                object obj = command.ExecuteScalar();
                CloseConnection();
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   " + SQL);
            }
        }
        // Comman Comman = new Comman();
        public DataTable ExecuteTable(string sSQL, string TableName)

        {

            try
            {
                // sConnectionString = Comman.Connection.ConnectionString;


                DataTable dt = new DataTable(TableName);
                List<ItemSetUp> lst;
                SqlDataAdapter adapter = new SqlDataAdapter();
                OpenConnection();
                SqlCommand command = getCommand(sSQL);
                adapter.SelectCommand = command;
                //     adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                adapter.Fill(dt);
                command = null;

                adapter = null;
                CloseConnection();
                //reslt.ResultDataTable = dt;
                return dt;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   " + sSQL);
            }
        }
        public int ExecuteQuery(string sSQL, string TableName)

        {

            try
            {
                // SqlDataAdapter adapter = new SqlDataAdapter();
                OpenConnection();
                SqlCommand command = getCommand(sSQL);
                int obj = command.ExecuteNonQuery();
                //     adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                CloseConnection();
                //reslt.ResultDataTable = dt;
                return obj;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + "   " + sSQL);
            }
        }



        //public static String[] GetExcelSheetNames(string excelFile)
        //{
        //    string sConnectionString = string.Empty;

        //    sConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sConnectionString + ";";

        //    using (SqlConnection excelConn = new SqlConnection(sConnectionString))
        //    {

        //        DataTable dt = new DataTable();
        //        excelConn.Open();
        //        dt = excelConn.GetDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //        if (dt == null)
        //        {
        //            return null;
        //        }
        //        String[] excelSheets = new String[dt.Rows.Count];
        //        int i = 0;
        //        // Add the sheet name to the string array.
        //        foreach (DataRow row in dt.Rows)
        //        {
        //            excelSheets[i] = row["TABLE_NAME"].ToString();
        //            i++;
        //        }
        //        if (excelConn.State == ConnectionState.Open)
        //            excelConn.Close();
        //        return excelSheets;
        //    }
        //}

        public static SqlCommand getCommand(string SQL)
        {
            SqlCommand cmd = m_connection.CreateCommand();
            cmd.CommandText = SQL;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
        }
    }
}
