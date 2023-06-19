using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechWorld.User
{
    public partial class Login : System.Web.UI.Page
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "Admin" && txtPassword.Text.Trim() == "123")
            {
                Session["admin"] = txtUsername.Text.Trim();
                Response.Redirect("../admin/dashboard.aspx");
            }
            else
            {
                con = new SqlConnection(Connection.GetConnectionString());
                cmd = new SqlCommand("User_Crud", con);
                cmd.Parameters.AddWithValue("@Action", "SELECT4LOGIN");
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
                cmd.CommandType = CommandType.StoredProcedure;
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count == 1)
                {
                    Session["username"] = txtUsername.Text.Trim();
                    Session["userID"] = dt.Rows[0]["UserID"];
                    Response.Redirect("default.aspx");
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Invalid Credentials.!";
                    lblMsg.CssClass = "alert alert-danger";
                }


            }

        }
    }
}