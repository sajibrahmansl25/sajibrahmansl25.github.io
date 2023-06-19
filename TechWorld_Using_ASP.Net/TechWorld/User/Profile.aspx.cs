//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Data;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace TechWorld.User
//{
//    public partial class profile : System.Web.UI.Page
//    {
//        SqlConnection con;
//        SqlCommand cmd;
//        SqlDataAdapter sda;
//        DataTable dt;
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                if (Session["userID"] == null)
//                {
//                    Response.Redirect("default.aspx");
//                }

//                else
//                {
//                    getUserDetails();
//                }

//            }
//        }



//        void getUserDetails()
//        {
//            con = new SqlConnection(Connection.GetConnectionString());
//            cmd = new SqlCommand("User_Crud", con);
//            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
//            cmd.Parameters.AddWithValue("UserID", Session["userId"]);
//            cmd.CommandType = CommandType.StoredProcedure;
//            sda = new SqlDataAdapter(cmd);
//            dt = new DataTable();
//            sda.Fill(dt);
//            rUserProfile.DataSource = dt;
//            rUserProfile.DataBind();
//            if (dt.Rows.Count == 1)
//            {
//                Session["name"] = dt.Rows[0]["Name"].ToString();
//                Session["email"] = dt.Rows[0]["Email"].ToString();
//                Session["imageUrl"] = dt.Rows[0]["ImageUrl"].ToString();
//                Session["createdDate"] = dt.Rows[0]["CreatedDate"].ToString();
//            }
//        }

//    }
//}