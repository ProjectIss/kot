using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.OleDb;
using System.Data;
using System.IO;
using static RestarentService.Comman;
using Newtonsoft.Json;
using RestarentService.Models;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DatabaseConnections;
using RestarentService.Models;
using System.Data.SqlClient;

namespace RestarentService.Controllers
{

    [RoutePrefix("_Api/KOTController")]

    public class KODController : ApiController
    {
        //ExcelHandler objKODController = new ExcelHandler();
        Comman.Connection connection = new Comman.Connection();
        HandlerAccess obj = new HandlerAccess();
        private object dataTable;

        [Route("GetItemSetup")]
        [HttpGet]
        public IHttpActionResult GetItemSetup()
        {
            HandlerAccess obj = new HandlerAccess();
            DataTable dt = obj.ExecuteTable("SELECT * FROM [RMSData].[dbo].[ItemSetup] WHERE STATUS = 'Active'", "Itemsetup");
            string JSONString = DataTableToJSONWithStringBuilder(dt);
            return Ok(JSONString);
        }

        [Route("GetTblTable")]
        [HttpGet]
        public IHttpActionResult GetTblTable()
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection sqlConnection = obj.sqlConnection();
                SqlCommand sql_cmnd = new SqlCommand("[dbo].[GetTables]", sqlConnection);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sql_cmnd;
                da.Fill(ds);
                sqlConnection.Close();

                string JSONString = DataTableToJSONWithStringBuilder(ds.Tables[0]);
                return Ok(JSONString);
            }
            catch (Exception ex)
            {
                return Ok("");
            }




        }
        [Route("GetTblwaiter")]
        [HttpGet]
        public IHttpActionResult GetTblwaiter()
        {
            HandlerAccess obj = new HandlerAccess();
            DataTable dt = obj.ExecuteTable("SELECT * FROM [RMSData].[dbo].[TblWaiter]", "Watiers");
            string JSONString = DataTableToJSONWithStringBuilder(dt);
            return Ok(JSONString);
        }


        [Route("Login")]
        [HttpPost]
        public IHttpActionResult Login(Models.LoginParam login)
        {
            DataTable data = obj.ExecuteTable("SELECT Admin,Trans,Report,UsrName,Pwd,Usertype,TW.Name FROM [RMSData].[dbo].[TblUser]  TU " +
                                                $"LEFT JOIN[RMSData].[dbo].TblWaiter  TW ON TU.UsrName = TW.Name WHERE UsrName = '{login.username}' AND Pwd = '{login.password}'", "Login");
            string JSONString = DataTableToJSONWithStringBuilder(data);
            return Ok(JSONString);
        }
        [Route("NewKOT")]
        [HttpPost]
        public IHttpActionResult NewKOT(List<NewKOTParam> jsonKOT)
        {
            try
            {
                //if (!string.IsNullOrEmpty(jsonKOT))
                //{
                List<NewKOTParam> lstKot = jsonKOT;// JsonConvert.DeserializeObject<List<NewKOTParam>>(jsonKOT);
                    if (lstKot.Count > 0)
                    {
                        foreach (var item in lstKot)
                        {
                            int data = obj.ExecuteQuery("INSERT INTO KOT "
                             + "( Date, ItemName, Qty, Rate, Amount, Tax, ItemNo, SupName, TableNo, Type, Status, KotType, FoodType, UsrName, FileName, FYear)"
                             + $"VALUES(N'{item.Date}','{item.ItemName}','{item.Qty}','{item.Rate}','{item.Amount}','{item.Tax}','{item.ItemNo}','{item.SupName}','{item.TableNo}'," +
                             $"'{item.Type}','{item.Status}','{item.KotType}','{item.FoodType}','{item.UsrName}','{item.FileName}','{item.FYear}')", "KOT");
                        }
                    }
             //  }

            }
            catch (Exception ex)
            {
                return Ok("Error :-" + ex.Message);
            }

            return Ok("Success");
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new System.Text.StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append(table.Columns[j].ColumnName.ToString() + ":" + table.Rows[i][j].ToString() + ",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append(table.Columns[j].ColumnName.ToString() + ":" + table.Rows[i][j].ToString() + ",");

                            //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }
    }
}

