﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TechWorld;

namespace TechWorld.User
{
    public partial class Registration : System.Web.UI.Page
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null) /* && Session["userID"] != null */
                {
                    getUserDetails();
                }
                else if (Session["userID"] != null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string actionName = string.Empty, imagePath = string.Empty, fileExtension = string.Empty;
            bool isValidToExecute = false;
            int userID = Convert.ToInt32(Request.QueryString["id"]);
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);

            cmd.Parameters.AddWithValue("@Action", userID == 0 ? "INSERT" : "UPDATE");
            cmd.Parameters.AddWithValue("@UserID", userID);
            cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
            cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());
            cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@PostCode", txtPostCode.Text.Trim());
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text.Trim());
            if (fuUserImage.HasFile)
            {
                if (Utils.IsValidExtension(fuUserImage.FileName))
                {
                    Guid obj = Guid.NewGuid();
                    fileExtension = Path.GetExtension(fuUserImage.FileName);
                    imagePath = "Images/User/" + obj.ToString() + fileExtension;
                    fuUserImage.PostedFile.SaveAs(Server.MapPath("~/Images/User/") + obj.ToString() + fileExtension);
                    cmd.Parameters.AddWithValue("@ImageUrl", imagePath);
                    isValidToExecute = true;
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Please select .jpg, .jpeg, or .png image";
                    lblMsg.CssClass = "alert alert-danger";
                    isValidToExecute = false;
                }
            }
            else
            {
                isValidToExecute = true;
            }

            if (isValidToExecute)
            {
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    actionName = userID == 0 ?
                        "registration is successful! <b><a href='Login.aspx'>Click here</a> to do login" :
                        "details updated successfully! <b><a href='Profile.aspx'>Can check here</a></b>";
                    lblMsg.Visible = true;
                    lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> " + actionName;
                    lblMsg.CssClass = "alert alert-success";
                    if (userID != 0)
                    {
                        Response.AddHeader("REFRESH", "1;URL=Profile.aspx");
                    }
                    clear();
                }
                catch (SqlException ex)
                {
                    if (ex.Message.Contains("Violation of UNIQUE KEY constraint"))
                    {
                        lblMsg.Visible = true;
                        lblMsg.Text = "<b>" + txtUsername.Text.Trim() + "</b> username already exists, try new one..!";
                        lblMsg.CssClass = "alert alert-danger";
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Error-" + ex.Message;
                    lblMsg.CssClass = "alert alert-danger";
                }
                finally
                {
                    con.Close();
                }
            }
        }

        void getUserDetails()
        {
            con = new SqlConnection(Connection.GetConnectionString());
            cmd = new SqlCommand("User_Crud", con);
            cmd.Parameters.AddWithValue("@Action", "SELECT4PROFILE");
            cmd.Parameters.AddWithValue("UserID", Request.QueryString["id"]);
            cmd.CommandType = CommandType.StoredProcedure;
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                txtName.Text = dt.Rows[0]["Name"].ToString();
                txtUsername.Text = dt.Rows[0]["Username"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                txtEmail.Text = dt.Rows[0]["Email"].ToString();
                txtAddress.Text = dt.Rows[0]["Address"].ToString();
                txtPostCode.Text = dt.Rows[0]["PostCode"].ToString();
                imgUser.ImageUrl = string.IsNullOrEmpty(dt.Rows[0]["ImageUrl"].ToString())
                    ? "../Images/No_image.png" : "../" + dt.Rows[0]["ImageUrl"].ToString();
                imgUser.Height = 200;
                imgUser.Width = 200;
                txtPassword.TextMode = TextBoxMode.SingleLine;
                txtPassword.ReadOnly = true;
                txtPassword.Text = dt.Rows[0]["Password"].ToString();
            }
            lblHeaderMsg.Text = "<h2>Edit Profile</h2>";
            btnRegister.Text = "Update";
            lblAlreadyUser.Text = "";
        }

        private void clear()
        {
            txtName.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtPostCode.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}