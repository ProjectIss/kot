using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestarentService.Models
{
    public class Param
    {

        public string[] value { get; set; }
        //public string KOTNo { get; set; }
        //public string Datewithouttime { get; set; }
        //public string WaiterName { get; set; }
        //public string TableNo { get; set; }
        //public string ItemName { get; set; }
        //public string Qty { get; set; }
        
    }
    public class LoginParam
    {
        public string username { get; set; }
        public string password { get; set; }
    }
    public class NewKOTParam
    {
        public string ENo { get; set; }
        public string Date { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public int Rate { get; set; }
        public int Amount { get; set; }
        public int Tax { get; set; }
        public string ItemNo { get; set; }
        public string SupName { get; set; }
        public string TableNo { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string KotType { get; set; }
        public string FoodType { get; set; }
        public string UsrName { get; set; }
        public string FileName { get; set; }
        public string FYear { get; set; }
       
    }
}