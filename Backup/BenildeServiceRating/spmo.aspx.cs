using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ThreeTier.BusinessLogic;
using System.Data;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System.Text.RegularExpressions;

namespace BenildeServiceRating
{
    public partial class spmo : System.Web.UI.Page
    {
        BLL bll = new BLL();
        string uid;
        int january, february, march, april, may, june, july, august, september, october, november, december;
        protected void Page_Load(object sender, EventArgs e)
        {
            bll.IsDeleted = "n";
            if (!Page.IsPostBack)
            {
                //bll.Username = Session["uid"].ToString();
                //uid = bll.Username;
                //lbluName.Text = bll.GetNameUsingSession().Rows[0][0].ToString() + " " + bll.GetNameUsingSession().Rows[0][1].ToString();
                BindDDLServiceProviderInCommentTab();
                BindDDLLocationInCommentTab();
                BindDLLRespondentsInCommentTab();
                BindCommentsGV();
                BindServiceProvidersGV();
                BindLocationsGV();
                BindDDLServiceProviderInServiceProviderLocation();
                BindDDLLocationInServiceProviderLocation();
                ddlServiceProvidersInServiceProviderLocationTab.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
                ddlLocationsInServiceProviderLocationTab.Items.Insert(0, new ListItem("Please select a location", "0", true));
                BindServiceProvidersLocationsGV();
                BindSuggestionsGV();
                BindRateLimitDDL();
                BindAdminTXT();
                BindQuestionDDL();
                DrawBarChart();
                DrawLoginChart();
            }
        }

        #region Button Clicks

        #region Manage - Comment Tab (4 Button Clicks - 100% Working)
        protected void btnSearchComment_Click(object sender, EventArgs e)
        {
            FilterComments();
        } //filter comments
        protected void btnPopUpModalComment_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow2 in gvComments.Rows)
            {
                CheckBox delete = (CheckBox)grow2.FindControl("cbSelectComment");
                if (delete.Checked == true)
                {
                    mpeCommentModal.Show(); lblCommentMessage.Text = "";
                }
                else
                {
                    lblCommentMessage.Text = "Please select comment(s) to delete";
                }
            }
        } //show confirmation 
        protected void btnDeleteComment_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow2 in gvComments.Rows)
            {
                CheckBox delete = (CheckBox)grow2.FindControl("cbSelectComment");
                if (delete.Checked == true)
                {
                    bll.CommentDelete("y", grow2.Cells[1].Text);
                    Audit("admin", "DELETE", "Comments", bll.CommentID);
                    mpeCommentModal.Hide();
                    mpeSuccessfulDeleteComments.Show();
                }
            }
            FilterComments(); 
        } //delete comment with audit 
        protected void btnCancelDeleteComment_Click(object sender, EventArgs e) 
        {
            foreach (GridViewRow grow2 in gvComments.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectComment");
                if (eachCheckBox.Checked == true)
                {
                    eachCheckBox.Checked = false;
                }
            }
            FilterComments(); 
        }  // uncheck all and cancel eachCheckBox
        #endregion

        #region Manage - Service Provider Tab (4 Button Clicks - 100% Working)
        protected void btnAddServiceProvider_Click(object sender, EventArgs e)
        {
            string serviceProviderName = txtServiceProvider.Text.Trim();
            if (txtServiceProvider.Text != "" && txtServiceProvider.Text != null)
            {
                string value = "\""; value = Regex.Escape(value);
                string trimmedServiceProvider = Regex.Replace(serviceProviderName, value, "");
                bll.Name = Regex.Replace(trimmedServiceProvider, "'", "");
                if (bll.ServiceProviderCheckExisting().Rows.Count == 0)
                {
                    bll.ServiceProviderID = bll.ServiceProviderAdd().Rows[0][0].ToString();
                    Audit("admin", "INSERT", "Service_Providers", bll.ServiceProviderID);
                    BindServiceProvidersGV();
                    txtServiceProvider.Text = ""; lblServiceProviderMessage.Text = "";
                    mpeServiceProviderSuccessAdd.Show();
                }
                else
                {
                    if (bll.ServiceProviderCheckIfDeleted().Rows.Count == 1)
                    {
                        bll.ServiceProviderID = bll.ServiceProviderRestore().Rows[0][0].ToString();
                        Audit("admin", "UPDATE", "Service_Providers", bll.ServiceProviderID);
                        if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                        {
                            bll.ServiceProviderLocationID = bll.ServiceProviderLocationRestore1().Rows[0][0].ToString();
                            Audit("admin", "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                        }
                        txtServiceProvider.Text = ""; lblServiceProviderMessage.Text = "";
                        mpeServiceProviderSuccessAdd.Show();
                        BindServiceProvidersGV();
                    }
                    else
                    {
                        lblServiceProviderMessage.Text = "Service provider already exist";
                    }
                }
            }
            else
            {
                lblServiceProviderMessage.Text = "Please input a service provider.";
            }
        } // add service provider with audit 
        protected void btnPopUpModalServiceProvider_Click(object sender, EventArgs e)
        {
            txtServiceProvider.Text = "";
            bool check = false;
            foreach (GridViewRow grow2 in gvServiceProviders.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectServiceProviders");
                if (eachCheckBox.Checked == true)
                {
                    lblServiceProviderMessage.Text = ""; mpeServiceProviderModal.Show();
                    check = true;
                }
            }
            if (check == false)
            {
                lblServiceProviderMessage.Text = "Please select service provider(s) to delete";
            }
        } // show confirmation 
        protected void btnDeleteServiceProvider_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in gvServiceProviders.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow.FindControl("cbSelectServiceProviders");
                if (eachCheckBox.Checked == true)
                {
                    bll.Name = grow.Cells[1].Text;
                    bll.ServiceProviderID = bll.ServiceProviderDelete().Rows[0][0].ToString();
                    Audit("admin", "DELETE", "Service_Providers", bll.ServiceProviderID);
                    if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                    {
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationDelete1().Rows[0][0].ToString();
                        Audit("admin", "DELETE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    }
                }
            }
            BindServiceProvidersGV();
            mpeServiceProviderModal.Hide();
            mpeServiceProviderSuccessDelete.Show();
        } // delete service provider with audit 
        protected void btnCancelDeleteServiceProvider_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow2 in gvServiceProviders.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectServiceProviders");
                if (eachCheckBox.Checked == true)
                {
                    eachCheckBox.Checked = false;
                }
            }
            BindServiceProvidersGV();
        } // uncheck all and cancel eachCheckBox
        #endregion

        #region Manage - Location Tab (4 button Clicks - 100% Working)
        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            string locationName = txtLocation.Text.Trim();
            if (txtLocation.Text != "" && txtLocation.Text != null)
            {
                string value = "\""; value = Regex.Escape(value);
                string trimmedLocation = Regex.Replace(locationName, value, "");
                bll.Name = Regex.Replace(trimmedLocation, "'", "");
                if (bll.LocationCheckExisting().Rows.Count == 0)
                {
                    bll.LocationID = bll.LocationAdd().Rows[0][0].ToString();
                    Audit("admin", "INSERT", "Locations", bll.LocationID);
                    lblLocationMessage.Text = ""; txtLocation.Text = "";
                    BindLocationsGV();
                    mpeLocationSuccessAdd.Show(); 
                }
                else
                {
                    if (bll.LocationCheckIfDeleted().Rows.Count == 1)
                    {
                        bll.LocationID = bll.LocationRestore().Rows[0][0].ToString();
                        Audit("admin", "UPDATE", "Locations", bll.LocationID);
                        if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                        {
                            bll.ServiceProviderLocationID = bll.ServiceProviderLocationRestore2().Rows[0][0].ToString();
                            Audit("admin", "UPDATE", "Locations", bll.ServiceProviderLocationID);
                            lblLocationMessage.Text = ""; txtLocation.Text = "";
                            BindLocationsGV();
                            mpeLocationSuccessAdd.Show();
                        }
                    }
                    else
                    {
                        lblLocationMessage.Text = "Location already exist";
                    }
                }
            }
            else
            {
                lblLocationMessage.Text = "Please input a location";
            }
        } // add location with audit
        protected void btnPopUpModalLocation_Click(object sender, EventArgs e)
        {
            txtLocation.Text = "";
            bool check = false;
            foreach (GridViewRow grow2 in gvLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectLocations");
                if (eachCheckBox.Checked == true)
                {
                    mpeLocationModal.Show(); lblLocationMessage.Text = ""; check = true;
                }
            }
            if (check == false)
            {
                    lblLocationMessage.Text = "Please select location(s) to delete";
            }
        } // show confirmation 
        protected void btnDeleteLocation_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in gvLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow.FindControl("cbSelectLocations");
                if (eachCheckBox.Checked == true)
                {
                    bll.Name = grow.Cells[1].Text;
                    bll.LocationID = bll.LocationDelete().Rows[0][0].ToString();
                    Audit("admin", "DELETE", "Locations", bll.LocationID);
                    if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                    {
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationDelete2().Rows[0][0].ToString();
                        Audit("admin", "DELETE", "Locations", bll.ServiceProviderLocationID);
                    }
                }
            }
            BindLocationsGV();
            mpeLocationModal.Hide();
            mpeLocationSuccessDelete.Show();
        } // delete service provider with audit 
        protected void btnCancelDeleteLocation_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow2 in gvLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectLocations");
                if (eachCheckBox.Checked == true)
                {
                    eachCheckBox.Checked = false;
                }
            }
            BindLocationsGV();
        } // uncheck all and cancel delete
        #endregion

        #region Manage - Service Provider Location Tab (4 button Clicks - 100% Working)
        protected void btnServiceProviderLocationAdd_Click(object sender, EventArgs e)
        {
            bll.ServiceProviderID = ddlServiceProvidersInServiceProviderLocationTab.SelectedValue;
            bll.LocationID = ddlLocationsInServiceProviderLocationTab.SelectedValue;
            if (bll.ServiceProviderID != "0" && bll.LocationID != "0")
            {
                if (bll.ServiceProviderLocationCheckIfServiceProviderExist2().Rows.Count == 0)
                {
                    bll.ServiceProviderLocationID = bll.ServiceProviderLocationAdd().Rows[0][0].ToString();
                    Audit("admin", "INSERT", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    ddlLocationsInServiceProviderLocationTab.SelectedValue = "0"; ddlServiceProvidersInServiceProviderLocationTab.SelectedValue = "0";
                    BindServiceProvidersLocationsGV();
                    mpeServiceProviderLocationSuccessAdd.Show();
                }
                else
                {
                    if (bll.ServiceProviderLocationCheckIfServiceProviderExist3().Rows.Count == 1)
                    {
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationRestore().Rows[0][0].ToString();
                        Audit("admin", "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                        ddlLocationsInServiceProviderLocationTab.SelectedValue = "0"; ddlServiceProvidersInServiceProviderLocationTab.SelectedValue = "0";
                        BindServiceProvidersLocationsGV();
                        mpeServiceProviderLocationSuccessAdd.Show();
                    }
                    else
                    {
                        lblServiceProviderLocationMessage.Text = "Service Provider Location already exist";
                    }
                }
            }
            else
            {
                lblServiceProviderLocationMessage.Text = "Please input a service provider and location";
            }

        } // add service provider Location with audit
        protected void btnPopUpModalServiceProviderLocation_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grow2 in gvServiceProvidersLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectServiceProvidersLocations");
                if (eachCheckBox.Checked == true)
                {
                    lblServiceProviderLocationMessage.Text = "";
                    mpeServiceProviderLocation.Show();
                    check = true;
                }
            }
            if (check == false)
            {
                lblServiceProviderLocationMessage.Text = "Please select service provider(s) and location(s) to delete";
            }
        } // show confirmation 
        protected void btnDeleteServiceProviderLocation_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow in gvServiceProvidersLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow.FindControl("cbSelectServiceProvidersLocations");
                if (eachCheckBox.Checked == true)
                {
                    bll.ServiceProviderName = grow.Cells[1].Text; bll.LocationName = grow.Cells[2].Text;
                    bll.ServiceProviderLocationID = bll.ServiceProviderLocationDelete().Rows[0][0].ToString();
                    Audit("admin", "DELETE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                }
            }
            BindServiceProvidersLocationsGV();
            mpeServiceProviderLocation.Hide();
            mpeServiceProviderLocationDeleteSuccess.Show();
        } // delete service provider Location with audit 
        protected void btnCancelDeleteServiceProviderLocation_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grow2 in gvServiceProvidersLocations.Rows)
            {
                CheckBox eachCheckBox = (CheckBox)grow2.FindControl("cbSelectServiceProvidersLocations");
                if (eachCheckBox.Checked == true)
                {
                    eachCheckBox.Checked = false;
                }
            }
            BindServiceProvidersLocationsGV();
        } // uncheck all and cancel deletes

        #endregion

        #region Manage - Pending Service Provider Tab (2 button Clicks - 100% Working)
        protected void btnApprovedSelected_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grow in gvSuggestions.Rows)
            {
                CheckBox checkbox = (CheckBox)grow.FindControl("cbSelectSuggestions");
                if (checkbox.Checked == true)
                {
                    bll.ServiceProviderLocationID = grow.Cells[1].Text;
                    bll.IsApproved = "y";
                    bll.ApproveDisapproveServiceProvider();
                    BindSuggestionsGV();
                    mpeApproveSP.Show();
                    BindServiceProvidersLocationsGV();
                    lblPending.Text = "";
                }
            }
            if (check == false)
            {
                lblPending.Text = "Please select service provider(s) to approve";
            }
        }
        protected void btnDisapprovedSelected_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grow in gvSuggestions.Rows)
            {
                CheckBox checkbox = (CheckBox)grow.FindControl("cbSelectSuggestions");
                if (checkbox.Checked == true)
                {
                    bll.ServiceProviderLocationID = grow.Cells[1].Text;
                    bll.IsApproved = "n";
                    bll.ApproveDisapproveServiceProvider();
                    BindSuggestionsGV();
                    mpeDisapproveSP.Show();
                    check = true;
                    lblPending.Text = "";
                }
            }
            if (check == false)
            {
                lblPending.Text = "Please select service provider(s) to disapprove";
            }
            
        }
        #endregion

        #region Change - Rate Limit Tab (3 button Click - 100% Working)
        protected void btnAddRateLimit_Click(object sender, EventArgs e)
        {
            bll.Limit = txtAddRateLimit.Text;
            if (bll.LimitSelectUsingLimit().Rows.Count == 0)
            {
                bll.IsActive = "n";
                bll.IsDeleted = "n";
                bll.LimitAdd();
                BindRateLimitDDL();
                mpeAddRLModal.Show();
            }
            else
            {
                lblRateLimitError.Text = "Hour limit already exists! Please select it in the dropdown below.";
            }
        }
        protected void btnSelectRateLimit_Click(object sender, EventArgs e)
        {
            mpeSetRLModal.Show();
        }
        protected void btnSetRateLimitModal_Click(object sender, EventArgs e)
        {
            bll.LimitID = ddlSelectRateLimit.SelectedValue.ToString();
            bll.IsActive = "y";
            bll.LimitSetActive();
            bll.IsActive = "n";
            bll.LimitSetInactive();
            mpeSetRLModal.Hide();
        }

        #endregion

        #region Change - Question Tab (3 button Clicks - 100% Working)
        protected void btnQuestionAdd_Click(object sender, EventArgs e)
        {
            string question = txtQuestionAdd.Text.Trim();
            string value = "\""; value = Regex.Escape(value);
            string trimmedQuestion = Regex.Replace(question, value, "");
            bll.Question = Regex.Replace(trimmedQuestion, "'", "");
            if (bll.QuestionSelectUsingQuestion().Rows.Count == 0)
            {
                bll.IsActive = "n";
                bll.IsDeleted = "n";
                bll.QuestionAdd();
                BindQuestionDDL();
                mpeAddQuestion.Show();
            }
            else
            {
                lblQuestionMessage.Text = "Existing question. Please select it in the dropdown below";
            }
        }
        protected void btnQuestionSet_Click(object sender, EventArgs e)
        {
            mpeSetQuestion.Show();
        }
        protected void btnSetQuestionModal_Click(object sender, EventArgs e)
        {
            //validate question textbox
            bll.IsActive = "y";
            bll.QuestionID = ddlQuestion.SelectedValue;
            bll.QuestionSetActive();
            bll.IsActive = "n";
            bll.QuestionSetInactive();
            mpeSetQuestion.Hide();
        }
        #endregion

        #region Change - Admin Tab (3 button Clicks - 100% Working)
        protected void btnSaveAdmin_Click(object sender, EventArgs e)
        {
            mpeAdminModal.Show();
        }
        protected void btnYesAdmin_Click(object sender, EventArgs e)
        {
            mpeAdminModal.Hide();
            mpeAdminLogoutModal.Show();
        }
        protected void btnAdminModalLogout_Click(object sender, EventArgs e)
        {
            bll.Username = txtNewAdmin.Text;
            if (bll.AdminCheckUsername().Rows.Count == 0)
            {
                bll.IsActive = "y";
                bll.AdminID = bll.AdminAdd().Rows[0][0].ToString();
                bll.IsActive = "n";
                bll.AdminSetInactive();
                btnLogout_Click(sender, e);
            }
            else
            {
                bll.IsActive = "y";
                bll.AdminID = bll.AdminSetActive().Rows[0][0].ToString();
                bll.IsActive = "n";
                bll.AdminSetInactive();
                btnLogout_Click(sender, e);
            }
        }
        #endregion

        #region logout
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
        #endregion

        #endregion  
        
        #region functions

        #region Audit Trails (1 Command - finished)
        private void Audit(string idNum, string action, string tableAffected, string idAffected)
        {
            bll.Username = idNum; //temporary
            bll.Action = action;
            bll.TableAffected = tableAffected;
            bll.IdAffected = idAffected;
            bll.Date = DateTime.Now.ToString();
            bll.AuditTrailInsert();
        }
        #endregion

        #region Manage - Comment Tab (5 commands - Finished)
        private void FilterComments()
        {
            bll.ServiceProviderID = ddlServiceProvidersInCommentTab.SelectedValue; bll.LocationID = ddlLocationsInCommentTab.SelectedValue; bll.Type = ddlRespondentsInCommentTab.SelectedValue;
            if (ddlServiceProvidersInCommentTab.SelectedValue != "All" && ddlLocationsInCommentTab.SelectedValue == "All" && ddlRespondentsInCommentTab.SelectedValue == "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter1();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue == "All" && ddlLocationsInCommentTab.SelectedValue != "All" && ddlRespondentsInCommentTab.SelectedValue == "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter2();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue == "All" && ddlLocationsInCommentTab.SelectedValue == "All" && ddlRespondentsInCommentTab.SelectedValue != "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter3();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue != "All" && ddlLocationsInCommentTab.SelectedValue != "All" && ddlRespondentsInCommentTab.SelectedValue == "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter4();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue == "All" && ddlLocationsInCommentTab.SelectedValue != "All" && ddlRespondentsInCommentTab.SelectedValue != "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter5();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue != "All" && ddlLocationsInCommentTab.SelectedValue == "All" && ddlRespondentsInCommentTab.SelectedValue != "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter6();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue != "All" && ddlLocationsInCommentTab.SelectedValue != "All" && ddlRespondentsInCommentTab.SelectedValue != "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter7();
            }
            if (ddlServiceProvidersInCommentTab.SelectedValue == "All" && ddlLocationsInCommentTab.SelectedValue == "All" && ddlRespondentsInCommentTab.SelectedValue == "All")
            {
                gvComments.DataSource = bll.SPMOCommentFilter8();
            }
            gvComments.DataBind();
        }
        private void BindDDLServiceProviderInCommentTab()
        {
            ddlServiceProvidersInCommentTab.DataSource = bll.ServiceProviderView();
            ddlServiceProvidersInCommentTab.DataTextField = "name";
            ddlServiceProvidersInCommentTab.DataValueField = "service_provider_id";
            ddlServiceProvidersInCommentTab.DataBind();
        }
        private void BindDDLLocationInCommentTab()
        {
            ddlLocationsInCommentTab.DataSource = bll.LocationView();
            ddlLocationsInCommentTab.DataTextField = "name";
            ddlLocationsInCommentTab.DataValueField = "location_id";
            ddlLocationsInCommentTab.DataBind();
        }
        private void BindDLLRespondentsInCommentTab()
        {
            ddlRespondentsInCommentTab.DataSource = bll.TypeView();
            ddlRespondentsInCommentTab.DataTextField = "type";
            ddlRespondentsInCommentTab.DataValueField = "type";
            ddlRespondentsInCommentTab.DataBind();
        }
        private void BindCommentsGV()
        {
            bll.IsDeleted = "n";
            gvComments.DataSource = bll.SPMOCommentFilter8();
            gvComments.DataBind();
        }
        #endregion

        #region Manage - Service Provider Tab (1 command - Finished)
        private void BindServiceProvidersGV()
        {
            gvServiceProviders.DataSource = bll.ServiceProviderViewWithoutID();
            gvServiceProviders.DataBind();
        }
        #endregion 

        #region Manage - Location Tab (1 command - Finished)
        private void BindLocationsGV()
        {
            gvLocations.DataSource = bll.LocationViewWithoutID();
            gvLocations.DataBind();
        }
        #endregion

        #region Manage - Service Provider Location Tab (3 commands - Finished)
        private void BindDDLServiceProviderInServiceProviderLocation()
        {
            ddlServiceProvidersInServiceProviderLocationTab.DataSource = bll.ServiceProviderViewAll();
            ddlServiceProvidersInServiceProviderLocationTab.DataValueField = "service_provider_id";
            ddlServiceProvidersInServiceProviderLocationTab.DataTextField = "name";
            ddlServiceProvidersInServiceProviderLocationTab.DataBind();
        }
        private void BindDDLLocationInServiceProviderLocation()
        {
            ddlLocationsInServiceProviderLocationTab.DataSource = bll.LocationViewAll();
            ddlLocationsInServiceProviderLocationTab.DataValueField = "location_id";
            ddlLocationsInServiceProviderLocationTab.DataTextField = "name";
            ddlLocationsInServiceProviderLocationTab.DataBind();
        }
        private void BindServiceProvidersLocationsGV()
        {
            gvServiceProvidersLocations.DataSource = bll.ServiceProviderLocationViewWithoutID();
            gvServiceProvidersLocations.DataBind();
        }
        #endregion

        #region Manage - Pending Service Provider Tab (1 command - Finished)
        private void BindSuggestionsGV()
        {
            bll.IsApproved = "p";
            gvSuggestions.DataSource = bll.ViewPending();
            gvSuggestions.DataBind();
            if (gvSuggestions.Rows.Count == 0)
            {
                lblPending.Text = "Yay! No pending suggestions!";
            }
        }
        #endregion

        #region Change - Rate Limit Tab (1 command - Finished)
        private void BindRateLimitDDL()
        {
            ddlSelectRateLimit.DataSource = bll.LimitView();
            ddlSelectRateLimit.DataValueField = "limit_id";
            ddlSelectRateLimit.DataTextField = "limit";
            ddlSelectRateLimit.DataBind();
            bll.IsActive = "y";
            ddlSelectRateLimit.SelectedValue = bll.LimitSelectActive().Rows[0][1].ToString();
        }
        #endregion

        #region Change - Question Tab (1 command - Finished)
        private void BindQuestionDDL()
        {
            bll.IsDeleted = "n";
            ddlQuestion.DataSource = bll.QuestionView();
            ddlQuestion.DataTextField = "question";
            ddlQuestion.DataValueField = "question_id";
            ddlQuestion.DataBind();
            bll.IsActive = "y";
            ddlQuestion.SelectedValue = bll.QuestionSelectActive().Rows[0][0].ToString();
        }
        #endregion 

        #region Change - Admin Tab (1 command - Finished)
        private void BindAdminTXT()
        {
            bll.IsActive = "y";
            lblCurrentAdmin.Text = bll.AdminSelectCurrent().Rows[0][1].ToString();
        }
        #endregion

        #region Charts - Summary of Ratings Tab (1 command - Finished)
        private void DrawBarChart()
        {
            DataTable dt = bll.ServiceProviderNumberOfRatings();
            List<string> serviceProviderList = new List<string>();
            List<object> happyList = new List<object>();
            List<object> sadList = new List<object>();
            List<object> totalList = new List<object>();
            for (int counter = 0; counter < dt.Rows.Count; counter++)
            {
                string rating = dt.Rows[counter][1].ToString();
                string serviceProvider = dt.Rows[counter][2].ToString();
                if (counter != 0)
                {
                    string previousServiceProvider = dt.Rows[counter - 1][2].ToString();
                    if (counter != (dt.Rows.Count - 1))
                    {
                        string nextServiceProvider = dt.Rows[counter + 1][2].ToString();
                        if (serviceProvider != previousServiceProvider && serviceProvider != nextServiceProvider)
                        {
                            serviceProviderList.Add(dt.Rows[counter][2].ToString());
                            if (rating.Equals("happy"))
                            {
                                happyList.Add(dt.Rows[counter][0].ToString());
                                sadList.Add("0");
                            }
                            else
                            {
                                sadList.Add(dt.Rows[counter][0].ToString());
                                happyList.Add("0");
                            }
                        }
                        else if (serviceProvider == nextServiceProvider)
                        {
                            serviceProviderList.Add(dt.Rows[counter][2].ToString());
                            if (rating.Equals("happy"))
                            {
                                happyList.Add(dt.Rows[counter][0].ToString());
                                sadList.Add(dt.Rows[counter + 1][0].ToString());
                            }
                            else
                            {
                                sadList.Add(dt.Rows[counter][0].ToString());
                                happyList.Add(dt.Rows[counter + 1][0].ToString());
                            }
                        }
                    }
                }
                else if (counter == 0)
                {
                    string nextServiceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter + 1][2].ToString();
                    if (serviceProvider != nextServiceProvider)
                    {
                        serviceProviderList.Add(dt.Rows[counter][2].ToString());
                        if (rating.Equals("happy"))
                        {
                            happyList.Add(dt.Rows[counter][0].ToString());
                            sadList.Add("0");
                        }
                        else
                        {
                            sadList.Add(dt.Rows[counter][0].ToString());
                            happyList.Add("0");
                        }
                    }
                }
            }
            for (int a = 0; a < happyList.Count; a++)
            {
                totalList.Add(int.Parse(happyList[a].ToString()) + int.Parse(sadList[a].ToString()));
            }
            DotNet.Highcharts.Highcharts summaryRatings = new DotNet.Highcharts.Highcharts("summaryRatings")
               .InitChart(new Chart
               {
                   DefaultSeriesType = ChartTypes.Bar
               })
               .SetXAxis(new XAxis
               {
                   Categories = serviceProviderList.ToArray()

               })
               .SetYAxis(new YAxis
               {
                   StackLabels = new YAxisStackLabels
                   {
                       Enabled = true
                   }
               })
               .SetPlotOptions(new PlotOptions
               {
                   Bar = new PlotOptionsBar
                   {
                       Stacking = Stackings.Normal,
                       DataLabels = new PlotOptionsBarDataLabels
                       {
                           Enabled = true
                       }
                   }
               })
               .SetTooltip(new Tooltip
               {
                   Formatter = "function () { return '<b>' + this.x + '</b><br/>' + this.series.name + ': ' + this.y + '<br/>' + 'Total: ' + this.point.stackTotal; }"
               })
               .SetSeries(new[]
               {
                   new Series { Name = "Sad", Data = new Data(sadList.ToArray()), Color = System.Drawing.Color.FromArgb(255,217,30,24) },
                   new Series { Name = "Happy", Data = new Data(happyList.ToArray()), Color = System.Drawing.Color.FromArgb(255,244,208,63) }
               })
               .SetExporting(new Exporting
               {
                   Enabled = true,
                   Filename = "Summary of Ratings (" + DateTime.Now + ")"
               })
               .SetTitle(new Title
               {
                   Text = "Summary of Ratings (" + DateTime.Now + ")"
               });
            ltrBarChart.Text = summaryRatings.ToHtmlString();
        }
        #endregion

        #region Charts - Login Trails Tab (1 command - Finished)
        private void DrawLoginChart()
        {
            bll.Action = "LOGIN";
            DataTable dt = bll.LoginTrailsView();
            for (int x = 0; x < dt.Rows.Count; x++)
            {
                DateTime date = DateTime.Parse(dt.Rows[x][0].ToString());
                if (date.Year == DateTime.Now.Year)
                {
                    if (date.Month == 01)
                    { january++; }
                    if (date.Month == 02)
                    { february++; }
                    if (date.Month == 03)
                    { march++; }
                    if (date.Month == 04)
                    { april++; }
                    if (date.Month == 05)
                    { may++; }
                    if (date.Month == 06)
                    { june++; }
                    if (date.Month == 07)
                    { july++; }
                    if (date.Month == 08)
                    { august++; }
                    if (date.Month == 09)
                    { september++; }
                    if (date.Month == 10)
                    { october++; }
                    if (date.Month == 11)
                    { november++; }
                    if (date.Month == 12)
                    { december++; }
                }
            }

            DotNet.Highcharts.Highcharts login = new DotNet.Highcharts.Highcharts("login")

                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new Series
                {
                    Name = "Number of Users for " + DateTime.Now.Year,
                    Data = new Data(new object[] { january, february, march, april, may, june, july, august, september, october, november, december })
                })
                .SetExporting(new Exporting
                {
                    Enabled = true,
                    Filename = "Number of Users for " + DateTime.Now.Year
                })
                .SetTitle(new Title
                {
                    Text = "Number of Users for " + DateTime.Now.Year
                });
            ltrLineChart.Text = login.ToHtmlString();
        }
        #endregion 

        #endregion



        
        
   
   
        
        
        
    }
}