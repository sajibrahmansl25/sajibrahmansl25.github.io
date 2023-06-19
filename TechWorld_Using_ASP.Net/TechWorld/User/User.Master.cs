using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechWorld.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Request.Url.AbsoluteUri.ToString().Contains("Default.aspx"))
            {
                form1.Attributes.Add("class","sub_page");
            }
            else
            {
                form1.Attributes.Remove("class");
                Control sliderUserControl = (Control)Page.LoadControl("sliderusercontrol.ascx");

                pnlSliderUC.Controls.Add(sliderUserControl);
            }
            if (Session["userID"] != null)
            {
                lblLoginOrLogout.Text = "Logout";
            }

            else
            {
                lblLoginOrLogout.Text = "Login";
            }
        }

        protected void lblLoginOrLogout_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                Response.Redirect("login.aspx");
            }

            else
            {
                Session.Abandon();
                Response.Redirect("login.aspx");
            }

        }

    }
}