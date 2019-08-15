using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data;
using ThreeTier.BusinessLogic;

namespace BenildeServiceRating
{
    public partial class login : System.Web.UI.Page
    {
        BLL bll = new BLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            bll.Username = txtUsername.Text.Trim();
            bll.Password = txtPassword.Text.Trim();
            bll.IsValid = "y";
            DataTable dt = bll.LoginUser();
            if (Regex.IsMatch(bll.Username, @"^\d+$"))
            {
                if (dt.Rows.Count != 0)
                {
                    if (bll.Password == dt.Rows[0][3].ToString())
                    {
                        bll.Action = "LOGIN";
                        bll.Date = DateTime.Now.ToString();
                        bll.LoginTrailInsert();
                        Session.Add("uid", bll.Username);
                        Response.Redirect("user.aspx");

                    }
                    else
                    {
                        lblLoginError.Text = "Wrong Username and/or password. Please try again.";
                    }
                }
                else
                {
                    lblLoginError.Text = "Wrong Username and/or password. Please try again.";
                }
            }
            else if (!Regex.IsMatch(bll.Username, @"^\d+$"))
            {
                if (dt.Rows.Count != 0)
                {
                    if (bll.Password == dt.Rows[0][3].ToString())
                    {
                        bll.IsActive = "y";
                        DataTable dt1 = bll.LoginAdmin();
                        if (dt1.Rows.Count != 0)
                        {
                            bll.Action = "LOGIN";
                            bll.Date = DateTime.Now.ToString();
                            Session.Add("uid", bll.Username);
                            Response.Redirect("spmo.aspx");
                        }
                        else
                        {
                            bll.Action = "LOGIN";
                            bll.Date = DateTime.Now.ToString();
                            bll.LoginTrailInsert();
                            Session.Add("uid", bll.Username);
                            Response.Redirect("user.aspx"); 
                        }
                    }
                    else
                    {
                        lblLoginError.Text = "Wrong Username and/or password. Please try again.";
                    }
                }
                else
                {
                    lblLoginError.Text = "Wrong Username and/or password. Please try again.";
                }
            }
            else
            {
                lblLoginError.Text = "Wrong Username and/or password. Please try again.";
            }
        }
    }
}