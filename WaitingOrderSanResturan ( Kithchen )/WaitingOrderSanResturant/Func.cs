using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;


    class Func
    {


        public static DataTable GetCard(string ConstrReader)
        {
            SqlConnection con = new SqlConnection(ConstrReader);
            DataTable dt = new DataTable();
            //string SelectBroker = "IF ((SELECT is_broker_enabled FROM sys.databases WHERE name = 'SanResturant') = 1) BEGIN ALTER DATABASE SanResturant SET NEW_BROKER WITH ROLLBACK IMMEDIATE  END  ALTER DATABASE SanResturant SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE";
            //string SelectBroker = "Select * From Vw_WatingKitchen";
            string SelectBroker = "Select [ForooshKalaParent_ID],ForooshKalaParent_TypeFact,[ForooshKalaParent_ShomareMiz],[ForooshKalaParent_ModateEntezar],[ForooshKalaParent_ShomareFish],[ForooshKalaParent_Tahvilgirande],[ForooshKalaParent_Delivery],ForooshKalaParent_Ready,ForooshKalaParent_Time  from TblParent_FrooshKala  Where [ForooshKalaParent_Date]=format(getdate(),'yyyy-MM-dd') and [ForooshKalaParent_Delivery]=0 ";

            SqlDataAdapter da = new SqlDataAdapter(SelectBroker, con);
            da.Fill(dt);
            int count = dt.Rows.Count;
            return dt;

        }

        public static DataTable GetFood(string ConstrReader, string IdFactor)
        {
            SqlConnection con = new SqlConnection(ConstrReader);
            DataTable dt = new DataTable();
            string SelectBroker = "Select [Name_Kala] as 'Food',[ChildForooshKala_TedadAsli] as 'Count',[ChildForooshKala_SharhKala] as 'Description' From Vw_WatingKitchen Where [ForooshKalaParent_ID]=" + IdFactor;
      
            SqlDataAdapter da = new SqlDataAdapter(SelectBroker, con);
            da.Fill(dt);
            int count = dt.Rows.Count;
            return dt;

        }


    }

