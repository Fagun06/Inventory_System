using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OST_MVC.Models
{
    public class BaseCustomer
    {

        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; } 
        public static List<BaseCustomer> ListCustomer()
        {
            List<BaseCustomer> plsCust = new List<BaseCustomer>();



            //DataTable dataTable = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "dbo.spOST_LstCustomer";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            SqlDataReader mrd = cmd.ExecuteReader();

            if (mrd.HasRows)
            {
                while (mrd.Read())
                {
                    BaseCustomer obj = new BaseCustomer();
                    obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
                    obj.CustomerName = mrd["CustomerName"].ToString();
                    obj.CustomerMobile = mrd["CustomerMobile"].ToString();

                    plsCust.Add(obj);
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




            return plsCust;

        }

        public static DataTable ListCustormerEquipment()
        {
            // List<BaseCustomer> plsCust = new List<BaseCustomer>();



            DataTable dataTable = new DataTable();
            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "dbo.spOst_LstCustomerEquipmentAssignment";
            cmd.Parameters.Clear();
            //cmd.Parameters.Add(new SqlParameter("@UserName", this.UserName));
            //cmd.Parameters.Add(new SqlParameter("@Password", this.Password));
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            /////SqlDataReader mrd = cmd.ExecuteReader();

            //if (mrd.HasRows)
            //{
            //    while (mrd.Read())
            //    {
            //        BaseCustomer obj = new BaseCustomer();
            //        obj.CustomerID = Convert.ToInt32(mrd["CustomerID"].ToString());
            //        obj.CustomerName = mrd["CustomerName"].ToString();
            //        obj.CustomerMobile = mrd["CustomerMobile"].ToString();

            //        plsCust.Add(obj);
            //    }
            //}

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dataTable);

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




            return dataTable;

        }


        public static int SaveAssignment(int CustomerID, int EquipmentID, int EquipmentQuantity)
        {

            string ConnString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(ConnString);
            sqlConnection.Open();
            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = "spOST_InsEquiAssignment";
            cmd.Parameters.Clear();
            cmd.Parameters.Add(new SqlParameter("@CustomerID",CustomerID));
            cmd.Parameters.Add(new SqlParameter("@EquipmentID",EquipmentID));
            cmd.Parameters.Add(new SqlParameter("@EquiCount",EquipmentQuantity));

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