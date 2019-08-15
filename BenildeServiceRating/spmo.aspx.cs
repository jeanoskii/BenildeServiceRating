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
            if (Session["uid"] != null)
            {
                bll.Username = Session["uid"].ToString();
                uid = bll.Username;
                if (bll.SessionSPMOCheck().Rows.Count != 0)
                {
                    if (!Page.IsPostBack)
                    {
                        lbluName.Text = bll.GetNameUsingSession().Rows[0][0].ToString() + " " + bll.GetNameUsingSession().Rows[0][1].ToString();
                        BindDDLServiceProviderInCommentTab();
                        BindDDLLocationInCommentTab();
                        BindDLLRespondentsInCommentTab();
                        BindCommentsGV();
                        BindServiceProvidersGV();
                        BindLocationsGV();
                        BindDDLServiceProviderInServiceProviderLocation();
                        BindDDLLocationInServiceProviderLocation();
                        BindServiceProvidersLocationsGV();
                        BindSuggestionsGV();
                        BindDDLInRateLimitTab();
                        BindTXTInAdminTab();
                        BindDDLInQuestionTab();
                        BindDDLAnswersInQuestionTab();
                        DrawBarChart();
                        DrawLoginChart();
                    }
                }
                else
                {
                    Response.Redirect("user.aspx");
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        #region Manage - Comment Tab (4 Button Clicks - 1 Function)
        protected void btnSearchComment_Click(object sender, EventArgs e)
        {
            FilterComments();
        } //filter comments
        protected void btnPopUpModalComment_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grow2 in gvComments.Rows)
            {
                CheckBox delete = (CheckBox)grow2.FindControl("cbSelectComment");
                if (delete.Checked == true)
                {
                    mpeCommentModal.Show(); lblCommentMessage.Text = "";
                    check = true;
                }
            }
            if (check == false)
            {
                lblCommentMessage.Text = "Please select comment(s) to delete";
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
                    Audit(uid, "DELETE", "Comments", bll.CommentID);
                    Clear();
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
            Clear();
            FilterComments();
        }  // uncheck all and cancel eachCheckBox
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
        } //filter function
        #endregion

        #region Manage - Service Provider Tab (4 Button Clicks)
        protected void btnAddServiceProvider_Click(object sender, EventArgs e)
        {
            bll.Name = txtServiceProvider.Text.Trim();
            if (bll.Name != "" && bll.Name != null)
            {
                if (Regex.IsMatch(bll.Name, @"^[a-zA-Z- ]+$"))
                {
                    if (bll.ServiceProviderCheckIfPending().Rows.Count == 1)
                    {
                        lblServiceProviderMessage.Text = "Service Provider is in the Pending list.";
                    }
                    else
                    {
                        if (bll.ServiceProviderCheckExisting().Rows.Count == 0)
                        {
                            bll.ServiceProviderID = bll.ServiceProviderAdd().Rows[0][0].ToString();
                            Audit(uid, "INSERT", "Service_Providers", bll.ServiceProviderID);
                            BindServiceProvidersGV(); BindDDLServiceProviderInServiceProviderLocation();
                            Clear(); mpeServiceProviderSuccessAdd.Show();
                        }
                        else
                        {
                            if (bll.ServiceProviderCheckIfDeleted().Rows.Count == 1)
                            {
                                bll.ServiceProviderID = bll.ServiceProviderRestore().Rows[0][0].ToString();
                                Audit(uid, "UPDATE", "Service_Providers", bll.ServiceProviderID);
                                if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                                {
                                    bll.ServiceProviderLocationRestore1();
                                    bll.ServiceProviderLocationID = bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows[0][0].ToString();
                                    Audit(uid, "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                                    BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                                }
                                txtServiceProvider.Text = ""; lblServiceProviderMessage.Text = "";
                                mpeServiceProviderSuccessAdd.Show();
                                BindServiceProvidersGV(); BindDDLServiceProviderInServiceProviderLocation();
                            }
                            else
                            {
                                lblServiceProviderMessage.Text = "Service provider already exist";
                            }
                        }
                    }
                }
                else
                {
                    lblServiceProviderMessage.Text = "Numbers and special characters are not allowed.";
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
                    Audit(uid, "DELETE", "Service_Providers", bll.ServiceProviderID);
                    if (bll.ServiceProviderLocationCheckIfServiceProviderExist().Rows.Count != 0)
                    {
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationDelete1().Rows[0][0].ToString();
                        Audit(uid, "DELETE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                        BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                    }
                }
            }
            BindServiceProvidersGV(); BindDDLServiceProviderInServiceProviderLocation();
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

        #region Manage - Location Tab (4 Button Clicks)
        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            bll.Name = txtLocation.Text.Trim();
            if (bll.Name != "" && bll.Name != null)
            {
                if (Regex.IsMatch(bll.Name, @"^[a-zA-Z- ]+$"))
                {
                    if (bll.LocationCheckExisting().Rows.Count == 0)
                    {
                        bll.LocationID = bll.LocationAdd().Rows[0][0].ToString();
                        Audit(uid, "INSERT", "Locations", bll.LocationID);
                        Clear(); BindLocationsGV(); BindDDLLocationInServiceProviderLocation();
                        mpeLocationSuccessAdd.Show();
                    }
                    else
                    {
                        if (bll.LocationCheckIfDeleted().Rows.Count == 1)
                        {
                            bll.LocationID = bll.LocationRestore().Rows[0][0].ToString();
                            Audit(uid, "UPDATE", "Locations", bll.LocationID);
                            if (bll.ServiceProviderLocationCheckIfLocationExist().Rows.Count != 0)
                            {
                                bll.ServiceProviderLocationRestore2();
                                bll.ServiceProviderLocationID = bll.ServiceProviderLocationCheckIfLocationExist().Rows[0][0].ToString();
                                Audit(uid, "UPDATE", "Locations", bll.ServiceProviderLocationID);
                                BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                            }
                            lblLocationMessage.Text = ""; txtLocation.Text = "";
                            BindLocationsGV(); BindDDLLocationInServiceProviderLocation();
                            mpeLocationSuccessAdd.Show();
                        }
                        else
                        {
                            lblLocationMessage.Text = "Location already exist";
                        }
                    }
                }
                else
                {
                    lblLocationMessage.Text = "Numbers and special characters are not allowed.";
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
                    Audit(uid, "DELETE", "Locations", bll.LocationID);
                    if (bll.ServiceProviderLocationCheckIfLocationExist().Rows.Count != 0)
                    {
                        bll.ServiceProviderLocationDelete2();
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationCheckIfLocationExist().Rows[0][0].ToString();
                        Audit(uid, "DELETE", "Locations", bll.ServiceProviderLocationID);
                        BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                    }
                }
            }
            BindLocationsGV(); BindDDLLocationInServiceProviderLocation();
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

        #region Manage - Service Provider Location Tab (4 Button Clicks)
        protected void btnServiceProviderLocationAdd_Click(object sender, EventArgs e)
        {
            bll.ServiceProviderID = ddlServiceProvidersInServiceProviderLocationTab.SelectedValue;
            bll.LocationID = ddlLocationsInServiceProviderLocationTab.SelectedValue;
            if (bll.ServiceProviderID != "0" && bll.LocationID != "0")
            {
                if (bll.ServiceProviderLocationCheckIfServiceProviderExist2().Rows.Count == 0)
                {
                    bll.ServiceProviderLocationAdd();
                    bll.ServiceProviderLocationID = bll.ServiceProviderLocationCheckIfServiceProviderExist2().Rows[0][0].ToString();
                    Audit(uid, "INSERT", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    ddlLocationsInServiceProviderLocationTab.SelectedValue = "0"; ddlServiceProvidersInServiceProviderLocationTab.SelectedValue = "0";
                    BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                    mpeServiceProviderLocationSuccessAdd.Show(); Clear();
                }
                else
                {
                    if (bll.ServiceProviderLocationCheckIfServiceProviderExist3().Rows.Count == 1)
                    {
                        bll.ServiceProviderLocationID = bll.ServiceProviderLocationRestore().Rows[0][0].ToString();
                        Audit(uid, "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                        ddlLocationsInServiceProviderLocationTab.SelectedValue = "0"; ddlServiceProvidersInServiceProviderLocationTab.SelectedValue = "0";
                        BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
                        mpeServiceProviderLocationSuccessAdd.Show(); Clear();
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
                    Clear();
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
                    Audit(uid, "DELETE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                }
            }
            BindServiceProvidersLocationsGV(); BindDDLLocationInCommentTab(); BindDDLServiceProviderInCommentTab();
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

        #region Manage - Pending Service Provider Tab (2 Button Clicks)
        protected void btnApprovedSelected_Click(object sender, EventArgs e)
        {
            bool check = false;
            foreach (GridViewRow grow in gvSuggestions.Rows)
            {
                CheckBox checkbox = (CheckBox)grow.FindControl("cbSelectSuggestions");
                if (checkbox.Checked == true)
                {
                    bll.IsApproved = "y";
                    bll.ServiceProviderLocationID = grow.Cells[1].Text;
                    bll.ApproveDisapproveServiceProviderLocation();
                    Audit(uid, "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    bll.ServiceProviderName = grow.Cells[3].Text;
                    bll.ServiceProviderID = bll.ApproveDisapproveServiceProvider().Rows[0][0].ToString();
                    Audit(uid, "UPDATE", "Service_Providers", bll.ServiceProviderID);
                    BindSuggestionsGV();
                    mpeApproveSP.Show();
                    BindServiceProvidersLocationsGV();
                    BindDDLServiceProviderInServiceProviderLocation();
                    BindDDLServiceProviderInCommentTab();
                    BindServiceProvidersGV();
                    Clear();
                    check = true;
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
                    bll.IsApproved = "n";
                    bll.ServiceProviderLocationID = grow.Cells[1].Text;
                    bll.ApproveDisapproveServiceProviderLocation();
                    Audit(uid, "UPDATE", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    bll.ServiceProviderName = grow.Cells[3].Text;
                    bll.ServiceProviderID = bll.ApproveDisapproveServiceProvider().Rows[0][0].ToString();
                    Audit(uid, "UPDATE", "Service_Providers", bll.ServiceProviderID);
                    BindSuggestionsGV();
                    mpeDisapproveSP.Show();
                    BindServiceProvidersLocationsGV();
                    check = true;
                    Clear();
                }
            }
            if (check == false)
            {
                lblPending.Text = "Please select service provider(s) to disapprove";
            }

        }
        #endregion

        #region Change - Rate Limit Tab (3 Button Clicks)
        protected void btnAddRateLimit_Click(object sender, EventArgs e)
        {
            bll.Limit = txtAddRateLimit.Text.Trim();
            if (bll.Limit != "" && bll.Limit != null)
            {
                if (bll.LimitSelectUsingLimit().Rows.Count == 0)
                {
                    bll.IsActive = "n";
                    bll.IsDeleted = "n";
                    bll.LimitAdd();
                    lblRateLimitError.Text = "";
                    mpeAddRLModal.Show();
                    BindDDLInRateLimitTab();
                }
                else
                {
                    lblRateLimitError.Text = "Hour limit already exists! Please select it in the dropdown below.";
                }
            }
            else
            {
                lblRateLimitError.Text = "Please input a number.";
            }
        }
        protected void btnSelectRateLimit_Click(object sender, EventArgs e)
        {
            if (bll.LimitSelectActive().Rows[0][0].ToString() == ddlSelectRateLimit.SelectedValue) 
            {
                lblRateLimitError.Text = "Rate Limit is already set";
            }
            else 
            {
            mpeSetRLModal.Show();
            }

        }
        protected void btnSetRateLimitModal_Click(object sender, EventArgs e)
        {
            lblRateLimitError.Text = "";
            bll.LimitID = ddlSelectRateLimit.SelectedValue.ToString();
            bll.IsActive = "y";
            bll.LimitSetActive();
            bll.IsActive = "n";
            bll.LimitSetInactive();
            mpeSetRLModal.Hide();
        }

        #endregion

        #region Change - Question Tab (3 Button Clicks)
        protected void btnQuestionAdd_Click(object sender, EventArgs e)
        {
            string newQuestion = "";
            bll.Question = txtQuestionAdd.Text.Trim();
            if (bll.Question != "" && bll.Question != null)
            {
                if (Regex.IsMatch(bll.Question, @"^[a-zA-Z? ]+$"))
                {
                    for (int counter = 0, counter2 = 1; counter < bll.Question.Length; counter++)
                    {
                        if ((bll.Question.Substring(counter, counter2)) != "?")
                        {
                            newQuestion = newQuestion + bll.Question.Substring(counter, counter2);
                        }
                    }
                    bll.Question = newQuestion + "?";
                    if (bll.Question != "?")
                    {
                        if (bll.QuestionSelectUsingQuestion().Rows.Count == 0)
                        {
                            string questionID = bll.QuestionAdd().Rows[0][0].ToString();
                            BindDDLInQuestionTab();
                            Clear();
                            mpeAddQuestion.Show();
                            Audit(uid, "INSERT", "Questions", questionID);
                        }
                        else
                        {
                            lblQuestionMessage.Text = "Existing question. Please select it in the dropdown below";
                        }
                    }
                    else
                    {
                        lblQuestionMessage.Text = "Please input a Question.";
                    }
                }
                else
                {
                    lblQuestionMessage.Text = "Numbers and special characters are not allowed.";
                }
            }
            else
            {
                lblQuestionMessage.Text = "Please input a Question.";
            }
        }
        protected void btnQuestionSet_Click(object sender, EventArgs e)
        {
            if(bll.QuestionSelectActive().Rows[0][0].ToString() == ddlQuestion.SelectedValue)
            {
                lblQuestionMessage.Text = "Question is already set as active";
            }
            else
            {
                    mpeSetQuestion.Show();
            }
        }
        protected void btnSetQuestionModal_Click(object sender, EventArgs e)
        {
            bll.QuestionID = ddlQuestion.SelectedValue;
            string questionID = bll.QuestionSetActive().Rows[0][0].ToString();
            bll.QuestionSetInactive();
            Audit(uid, "UPDATE", "Questions", questionID);
            mpeSetQuestion.Hide();
            Clear();
        }

        //protected void btnQuestionDelete_Click(object sender, EventArgs e)
        //{ 

        //}
        #endregion

        #region Change - Admin Tab (3 Button Clicks)
        protected void btnSaveAdmin_Click(object sender, EventArgs e)
        {
            bll.Username = txtNewAdmin.Text.Trim();
            if (bll.Username != "" && bll.Username != null)
            {
                if (Regex.IsMatch(bll.Username, @"^[a-zA-Z]+$"))
                {
                    if (bll.AdminCheckIfExisting().Rows.Count != 0)
                    {
                        if (uid == bll.Username)
                        {
                            lblAdminMessage.Text = "Input is already the current admin.";
                        }
                        else
                        {
                            mpeAdminModal.Show();
                        }
                    }
                    else
                    {
                        lblAdminMessage.Text = "Admin does not exist or is no longer active.";
                    }
                }
                else
                {
                    lblAdminMessage.Text = "Numbers and special characters are not allowed.";
                }
            }
            else
            {
                lblAdminMessage.Text = "Please input the new admin.";
            }

        }
        protected void btnYesAdmin_Click(object sender, EventArgs e)
        {
            mpeAdminModal.Hide();
            mpeAdminLogoutModal.Show();
        }
        protected void btnAdminModalLogout_Click(object sender, EventArgs e)
        {
            bll.Username = txtNewAdmin.Text.Trim();
            if (bll.AdminCheckUsername().Rows.Count == 0)
            {
                bll.AdminID = bll.AdminAdd().Rows[0][0].ToString();
                Audit(uid, "INSERT", "Admins", bll.AdminID);
                bll.Username = txtNewAdmin.Text.Trim();
                bll.AdminID = bll.AdminSetInactive().Rows[0][0].ToString();
                Audit(uid, "UPDATE", "Admins", bll.AdminID);
                btnLogout_Click(sender, e);
            }
            else
            {
                bll.AdminID = bll.AdminSetActive().Rows[0][0].ToString();
                Audit(uid, "UPDATE", "Admins", bll.AdminID);
                bll.Username = txtNewAdmin.Text.Trim();
                bll.AdminID = bll.AdminSetInactive().Rows[0][0].ToString();
                Audit(uid, "UPDATE", "Admins", bll.AdminID);
                btnLogout_Click(sender, e);
            }
        }
        #endregion

        #region Charts - Summary of Ratings Tab (1 Function)
        private void DrawBarChart()
        {
            DataTable dt = bll.NumberOfRatingsPerServiceProvider();
            List<string> serviceProviderList = new List<string>();
            List<object> happyList = new List<object>();
            List<object> sadList = new List<object>();
            List<object> totalList = new List<object>();
            for (int counter = 0; counter < dt.Rows.Count; counter++)
            {
                string rating = dt.Rows[counter][1].ToString();
                if (dt.Rows.Count == 1)
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
                else
                {
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
            }
            if (dt.Rows.Count != 0)
            {
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
                       Text = "Summary of Ratings as of " + DateTime.Now.ToShortDateString()
                   });
                ltrBarChart.Text = summaryRatings.ToHtmlString();
            }
        }
        #endregion

        #region Charts - Login Trails Tab (1 Function)
        private void DrawLoginChart()
        {
            bll.Action = "LOGIN";
            DataTable dt = bll.LoginTrailsView();
            int countWeekly = int.Parse(bll.LoginTrailsCountWeekly().Rows[0][0].ToString());
            int countDaily = int.Parse(bll.LoginTrailsCountDaily().Rows[0][0].ToString());
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

                .InitChart(new Chart
                {
                    DefaultSeriesType = ChartTypes.Line,
                    Events = new ChartEvents
                    {
                        Load = "function () {" +
                        "var ren = this.renderer; " +
                        "ren.label('Weekly Logins: <b>" + countWeekly + "</b>' , 20, 5) " +
                        ".add(); " +
                        "ren.label('Daily Logins: <b>" + countDaily + "</b>' , 20, 20) " +
                        ".add(); } "
                    }
                })
                .SetXAxis(new XAxis
                {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                .SetSeries(new Series
                {
                    Name = "Logins",
                    Data = new Data(new object[] { january, february, march, april, may, june, july, august, september, october, november, december }),
                    Color = System.Drawing.Color.Green
                })
                .SetPlotOptions(new PlotOptions
                {
                    Line = new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Enabled = true,
                        }
                    }
                })
                .SetExporting(new Exporting
                {
                    Enabled = true,
                    Filename = "Number of Logins (" + DateTime.Now + ")"
                })
                .SetTitle(new Title
                {
                    Text = "Number of Logins for " + DateTime.Now.Year
                });
            ltrLineChart.Text = login.ToHtmlString();
        }

        #endregion

        #region Logout (1 Button Click)
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
        #endregion

        #region Audit Trails (1 Function)
        private void Audit(string idNum, string action, string tableAffected, string idAffected)
        {
            bll.Username = idNum;
            bll.Action = action;
            bll.TableAffected = tableAffected;
            bll.IdAffected = idAffected;
            bll.Date = DateTime.Now.ToString();
            bll.AuditTrailInsert();
        }
        #endregion

        #region Bind Drop Down List, Grid View and Text (13 Functions)

        private void BindDDLServiceProviderInCommentTab()
        {
            ddlServiceProvidersInCommentTab.DataSource = bll.ServiceProviderView();
            ddlServiceProvidersInCommentTab.DataTextField = "name";
            ddlServiceProvidersInCommentTab.DataValueField = "service_provider_id";
            ddlServiceProvidersInCommentTab.DataBind();
            ddlServiceProvidersInCommentTab.Items.Insert(0, new ListItem("All Service Providers", "All", true));
        }
        private void BindDDLLocationInCommentTab()
        {
            ddlLocationsInCommentTab.DataSource = bll.LocationView();
            ddlLocationsInCommentTab.DataTextField = "name";
            ddlLocationsInCommentTab.DataValueField = "location_id";
            ddlLocationsInCommentTab.DataBind();
            ddlLocationsInCommentTab.Items.Insert(0, new ListItem("All Locations", "All", true));
        }
        private void BindDLLRespondentsInCommentTab()
        {
            ddlRespondentsInCommentTab.DataSource = bll.TypeView();
            ddlRespondentsInCommentTab.DataTextField = "type";
            ddlRespondentsInCommentTab.DataValueField = "type";
            ddlRespondentsInCommentTab.DataBind();
            ddlRespondentsInCommentTab.Items.Insert(0, new ListItem("All Types of Respondents", "All", true));
        }
        private void BindDDLServiceProviderInServiceProviderLocation()
        {
            ddlServiceProvidersInServiceProviderLocationTab.DataSource = bll.ServiceProviderViewAll();
            ddlServiceProvidersInServiceProviderLocationTab.DataValueField = "service_provider_id";
            ddlServiceProvidersInServiceProviderLocationTab.DataTextField = "name";
            ddlServiceProvidersInServiceProviderLocationTab.DataBind();
            ddlServiceProvidersInServiceProviderLocationTab.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
        }
        private void BindDDLLocationInServiceProviderLocation()
        {
            ddlLocationsInServiceProviderLocationTab.DataSource = bll.LocationViewAll();
            ddlLocationsInServiceProviderLocationTab.DataValueField = "location_id";
            ddlLocationsInServiceProviderLocationTab.DataTextField = "name";
            ddlLocationsInServiceProviderLocationTab.DataBind();
            ddlLocationsInServiceProviderLocationTab.Items.Insert(0, new ListItem("Please select a location", "0", true));
        }
        private void BindDDLInRateLimitTab()
        {
            ddlSelectRateLimit.DataSource = bll.LimitView();
            ddlSelectRateLimit.DataValueField = "limit_id";
            ddlSelectRateLimit.DataTextField = "limit";
            ddlSelectRateLimit.DataBind();
            bll.IsActive = "y";
            ddlSelectRateLimit.SelectedValue = bll.LimitSelectActive().Rows[0][0].ToString();
        }
        private void BindDDLInQuestionTab()
        {
            ddlQuestion.DataSource = bll.QuestionView();
            ddlQuestion.DataTextField = "question";
            ddlQuestion.DataValueField = "question_id";
            ddlQuestion.DataBind();
            bll.IsActive = "y";
            ddlQuestion.SelectedValue = bll.QuestionSelectActive().Rows[0][0].ToString();
        }

        private void BindDDLAnswersInQuestionTab()
        {
            ddlAnswer.DataSource = bll.ViewAnswers();
            ddlAnswer.DataTextField = "bothAnswers";
            ddlAnswer.DataValueField = "answer_id";
            ddlAnswer.DataBind();
            ddlAnswer.SelectedValue = bll.SelectActiveAnswers().Rows[0][0].ToString();
        }
        private void BindCommentsGV()
        {
            bll.IsDeleted = "n";
            gvComments.DataSource = bll.SPMOCommentFilter8();
            gvComments.DataBind();
        }
        private void BindServiceProvidersGV()
        {
            gvServiceProviders.DataSource = bll.ServiceProviderViewWithoutID();
            gvServiceProviders.DataBind();
        }
        private void BindLocationsGV()
        {
            gvLocations.DataSource = bll.LocationViewWithoutID();
            gvLocations.DataBind();
        }
        private void BindServiceProvidersLocationsGV()
        {
            gvServiceProvidersLocations.DataSource = bll.ServiceProviderLocationViewWithoutID();
            gvServiceProvidersLocations.DataBind();
        }
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

        private void BindTXTInAdminTab()
        {
            bll.IsActive = "y";
            lblCurrentAdmin.Text = bll.AdminSelectCurrent().Rows[0][1].ToString();
        }

        #endregion

        private void Clear()
        {
            txtServiceProvider.Text = ""; txtLocation.Text = ""; txtAddRateLimit.Text = ""; txtQuestionAdd.Text = ""; txtNewAdmin.Text = "";
            lblCommentMessage.Text = ""; lblServiceProviderMessage.Text = ""; lblLocationMessage.Text = ""; lblServiceProviderLocationMessage.Text = ""; lblPending.Text = ""; lblRateLimitError.Text = ""; lblQuestionMessage.Text = ""; lblAdminMessage.Text = "";
        }

        protected void btnSetAnswer_Click(object sender, EventArgs e)
        {
                if (bll.SelectActiveAnswers().Rows[0][0].ToString() == ddlAnswer.SelectedValue)
                {
                    lblQuestionMessage.Text = "Answer is already set as active";
                }
                else
                {
                    mpeSetAnswer.Show();
                }
        }
        protected void btnSetAnswerModal_Click(object sender, EventArgs e)
        {
            bll.AnswerID = ddlAnswer.SelectedValue;
            string answerID = bll.AnswerSetActive().Rows[0][0].ToString();
            bll.AnswerSetInactive();
            Audit(uid, "UPDATE", "Answers", answerID);
            mpeSetAnswer.Hide();
            Clear();
        }

        protected void btnAddAnswers_Click(object sender, EventArgs e)
        {
            bll.Positive = txtAnswerPositive.Text.Trim();
            bll.Negative = txtAnswerNegative.Text.Trim();
            if (bll.Negative != "" && bll.Positive != "")
            {
                if (Regex.IsMatch(bll.Negative, @"^[a-zA-Z ]+$") && Regex.IsMatch(bll.Positive, @"^[a-zA-Z ]+$"))
                {
                    if (bll.AnswersCheckIfExisting().Rows.Count != 0)
                    {
                        lblQuestionMessage.Text = "Answers already exist.";
                    }
                    else
                    {
                        bll.AnswerID = bll.AddAnswers().Rows[0][0].ToString();
                        Audit(uid, "INSERT", "Answers", bll.AnswerID);
                        BindDDLAnswersInQuestionTab();
                        mpeAddAnswer.Show();
                        lblQuestionMessage.Text = "";
                        txtAnswerPositive.Text = "";
                        txtAnswerNegative.Text = "";
                    }
                }
                else
                {
                    lblQuestionMessage.Text = "Special Characters are not allowed.";
                }
            }
            else
            {
                lblQuestionMessage.Text = "Please input both positive and negative answers.";
            }
        }

    }
}