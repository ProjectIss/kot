using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace RestarentService
{
    public class Comman
    {


       public static string filePath = System.Web.Hosting.HostingEnvironment.MapPath("~/DB/Hotelmaster.mdb");

        //public static object MessageBox { get; private set; }

        // public  static string  ConnectionString = ConfigurationManager.ConnectionStrings["RestaurentConnection"].ConnectionString;
        // <add name = "RestaurentConnection" connectionString="Data Source=D:\Service\restaurant\Hotelmaster.mdb" providerName="Microsoft.Jet.OLEDB.4.0"/>
        // public static   ConnectionStringSettings    = ConfigurationManager.ConnectionStrings["RestaurentConnection"];
        // public static string ConnectionString = ConfigurationManager.ConnectionStrings("RestaurentConnection");


        //  myDataTable.WriteToCsvFile("C:\\MyDataTable.csv");



        // public static object ConnectionString { get; internal set; }
        public class Connection
        {
            //public static string ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Service\restaurant\Hotelmaster.mdb;";

            public  string ToCsv( DataTable dataTable)
            {
                StringBuilder sbData = new StringBuilder();

                // Only return Null if there is no structure.
                if (dataTable.Columns.Count == 0)
                    return null;

                foreach (var col in dataTable.Columns)
                {
                    if (col == null)
                        sbData.Append(",");
                    else
                        sbData.Append("\"" + col.ToString().Replace("\"", "\"\"") + "\",");
                }

                sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);

                foreach (DataRow dr in dataTable.Rows)
                {
                    foreach (var column in dr.ItemArray)
                    {
                        if (column == null)
                            sbData.Append(",");
                        else
                            sbData.Append("\"" + column.ToString().Replace("\"", "\"\"") + "\",");
                    }
                    sbData.Replace(",", System.Environment.NewLine, sbData.Length - 1, 1);
                }

                return sbData.ToString();
            }
        }
}
         }
      


    

