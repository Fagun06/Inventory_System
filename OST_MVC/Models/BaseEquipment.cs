using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace OST_MVC.Models
{

    [Serializable]
    public class BaseEquipment
    {

        [DataMember]
        public int EquipmentID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EcCount { get; set; }

        [DataMember]
        public int Stock { get; set; }
        [DataMember]
        public DateTime EntryDate { get; set;}

        public List<BaseEquipment> ListEquipment { get; set; }

        public BaseEquipment() { 
            ListEquipment = new List<BaseEquipment>();
        }

        public static List<BaseEquipment> ListEquipmentDate()
        {
            List<BaseEquipment> plsData = new List<BaseEquipment>();
          
                //DataTable dataTable = new DataTable();
                string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
                SqlConnection sqlConnection = new SqlConnection(ConnString);
                sqlConnection.Open();
                SqlCommand cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "dbo.spOST_LstEquipment";
                cmd.Parameters.Clear();
                //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
                //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                SqlDataReader mrd = cmd.ExecuteReader();

                if(mrd.HasRows)
                {
                    while(mrd.Read())
                    {
                        BaseEquipment obj = new BaseEquipment(); 
                        obj.EquipmentID = Convert.ToInt32(mrd["EquipmentID"].ToString());
                        obj.Name = mrd["EquipmentName"].ToString();
                        obj.EcCount = Convert.ToInt16(mrd["Quantity"].ToString());
                        obj.Stock = Convert.ToInt16(mrd["Stock"].ToString());
                        obj.EntryDate = Convert.ToDateTime(mrd["EntryDate"].ToString());
                        plsData.Add(obj);
                    }
                }
                cmd.Dispose();
                sqlConnection.Close();
               
            
            //for(int i=0; i<50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop" + i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;

            //    plsData.Add(baseEquipment);
            //}




            return plsData;

        }

        public int SaveEquipment()
        {
            
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "dbo.spOST_insEquipment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@Name", this.Name));
            cmd.Parameters.Add(new SqlParameter("@EcCount", this.EcCount));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", this.EntryDate));
            
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;

            int returnResult = cmd.ExecuteNonQuery();

            
            cmd.Dispose();
            sqlConnection.Close();


            //for(int i=0; i<50; i++)
            //{
            //    BaseEquipment baseEquipment = new BaseEquipment();
            //    baseEquipment.Name = "Laptop" + i.ToString();
            //    baseEquipment.EcCount = 5+i;
            //    baseEquipment.EntryDate = DateTime.Now.Date;

            //    plsData.Add(baseEquipment);
            //}




            return returnResult;

        }
    }
}