using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ThreeTier.BusinessLogic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using DotNet.Highcharts.Attributes;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace BenildeServiceRating
{
    public partial class user : System.Web.UI.Page
    {
        BLL bll = new BLL(); string uid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["uid"] != null)
            {
                lblRateError.Text = "";
                bll.Username = Session["uid"].ToString();
                uid = bll.Username;
                lbluName.Text = bll.GetNameUsingSession().Rows[0][0].ToString() + " " + bll.GetNameUsingSession().Rows[0][1].ToString();
                bll.IsActive = "y";
                bll.IsDeleted = "n";
                lblSuggestionAdminEmail.Text = bll.GetAdminEmail().Rows[0][0].ToString();
                lblEmailFooter.Text = bll.GetAdminEmail().Rows[0][0].ToString();
                lblQuestion.Text = bll.QuestionSelectActive().Rows[0][1].ToString();
                bll.QuestionID = bll.QuestionSelectActive().Rows[0][0].ToString();
                lblPositiveAnswer.Text = bll.SelectActiveAnswers().Rows[0][1].ToString();
                lblNegativeAnswer.Text = bll.SelectActiveAnswers().Rows[0][2].ToString();
                if (!Page.IsPostBack)
                {
                    BindSearchServiceProvidersDDL();
                    BindSearchLocationsDDL();
                    BindSearchBenildeanTypeDDL(); BindChartServiceProviderDDL();
                    ddlServiceProvider.DataSource = bll.ServiceProviderSelectWithLocations();
                    ddlServiceProvider.DataTextField = "name";
                    ddlServiceProvider.DataValueField = "service_provider_id";
                    ddlServiceProvider.DataBind();
                    ddlServiceProvider.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
                    ddlSuggestLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
                    ddlLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
                    ddlChartLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
                    filterCommentsInUser_Click(sender, e);
                    DrawBarChart();
                }
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

        #region Logout Tab (1 button click)
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
        #endregion

        #region Rating Tab
        protected void btnRatingModal_Click(object sender, EventArgs e)
        {
            lblConfirmRating.Text = "";
            lblConfirmServiceProvider.Text = "";
            if (ddlLocation.SelectedValue != "0" && ddlLocation.SelectedValue != null && ddlLocation.SelectedValue != ""
            && ddlServiceProvider.SelectedValue != "0" && ddlServiceProvider.SelectedValue != null && ddlServiceProvider.SelectedValue != "")
            {
                if (HappyOrSad.Text.Equals("happy") || HappyOrSad.Text.Equals("sad"))
                {
                    lblRateError.Text = ""; bll.Username = uid;
                    bll.LocationID = ddlLocation.SelectedValue;
                    bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
                    bll.ServiceProviderLocationID = bll.GetServiceProviderLocationID().Rows[0][0].ToString();
                    if (bll.GetRatingUsingUsername().Rows.Count == 0)
                    {
                        mpeRatingModal.Show();
                    }
                    else
                    {
                        DateTime userRatingDate = (DateTime)bll.GetRatingUsingUsername().Rows[0][4];
                        int RateLimit = int.Parse(bll.LimitSelectActive().Rows[0][1].ToString());
                        if ((DateTime.Now - userRatingDate).TotalHours >= RateLimit)
                        {
                            mpeRatingModal.Show();
                        }
                        else
                        {
                            lblRateError.Text = "You can only rate this service provider once every " + RateLimit + " hour(s).";
                        }
                    }
                }
                else
                {
                    lblRateError.Text = "Please rate the service provider"; HappyOrSad.Text = ""; Answer.Text = "";
                }
            }
            else
            {
                lblRateError.Text = "Please select a service provider and location";
            }
        }
        protected void btnRatingSubmit_Click(object sender, EventArgs e)
        {
            if (commentsSuggestions.Text.Trim() == "" || commentsSuggestions.Text == null)
            {
                lblConfirmRating.Text = Answer.Text;
                lblConfirmServiceProvider.Text = ddlServiceProvider.SelectedItem.Text + " - " + ddlLocation.SelectedItem.Text;
                lblConfirmComment.Text = "";
                lblCommentError.Text = "";
                mpeRatingModal.Hide();
                mpeRatingSubmit.Show();
            }
            else
            {
                if (!Regex.IsMatch(commentsSuggestions.Text, @"^[a-zA-Z0-9,.!? ]*$"))
                {
                    lblCommentError.Text = "Special characters are not allowed.";
                    mpeRatingModal.Show();
                }
                else
                {

                    TextBox txtComment = new TextBox();
                    txtComment.Text = commentsSuggestions.Text.Trim();
                    txtComment.TextMode = TextBoxMode.MultiLine;
                    txtComment.CssClass = "txtConfirmCommentBox";
                    txtComment.ForeColor = System.Drawing.ColorTranslator.FromHtml("#555");
                    txtComment.ReadOnly = true;
                    phComment.Controls.Add(txtComment);
                    lblConfirmRating.Text = Answer.Text;
                    lblConfirmServiceProvider.Text = ddlServiceProvider.SelectedItem.Text + " - " + ddlLocation.SelectedItem.Text;
                    lblConfirmComment.Text = "With the comment: ";
                    lblCommentError.Text = "";
                    mpeRatingModal.Hide();
                    mpeRatingSubmit.Show();
                }
            }
            
        }

        protected void btnRatingYes_Click(object sender, EventArgs e)
        {
            lblRateError.Text = ""; bll.Username = uid;
            bll.LocationID = ddlLocation.SelectedValue;
            bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
            bll.ServiceProviderLocationID = bll.GetServiceProviderLocationID().Rows[0][0].ToString();
            bll.Date = DateTime.Now.ToString();
            bll.Rating = HappyOrSad.Text;
            bll.RatingID = bll.RatingAdd().Rows[0][0].ToString();
            Audit(uid, "INSERT", "Ratings", bll.RatingID);
            sendNotification();
            if (commentsSuggestions.Text.Trim().Length != 0)
            {
                bll.Comment = commentsSuggestions.Text.Trim();
                if (bll.Rating == "happy")
                {
                    bll.IsReview = "y";
                }
                else
                {
                    bll.IsReview = "n";
                }
                bll.CommentID = bll.CommentAdd().Rows[0][0].ToString();
                Audit(uid, "INSERT", "Comments", bll.CommentID);
            }
            ddlSearchLocations.SelectedValue = ddlLocation.SelectedValue;
            ddlSearchServiceProviders.SelectedValue = ddlServiceProvider.SelectedValue;
            filterCommentsInUser_Click(sender, e);
            ddlChartServiceProvider.SelectedValue = ddlServiceProvider.SelectedValue;
            ddlChartServiceProvider_SelectedIndexChanged(sender, e);
            ddlChartLocation.SelectedValue = ddlLocation.SelectedValue;
            ddlServiceProvider.SelectedValue = "0"; ddlLocation.SelectedValue = "0"; HappyOrSad.Text = "";
            Answer.Text = ""; commentsSuggestions.Text = "";
            mpeRatingSubmit.Hide(); mpeRatingNavigate.Show();
        }
        protected void btnRatingNoThanks_Click(object sender, EventArgs e)
        {
            mpeRatingModal.Hide();
            mpeRatingSubmit.Show();
            lblConfirmRating.Text = Answer.Text;
            lblCommentError.Text = "";
            commentsSuggestions.Text = "";
            lblConfirmComment.Text = "";
            lblConfirmServiceProvider.Text = ddlServiceProvider.SelectedItem.Text + " - " + ddlLocation.SelectedItem.Text;
        }
        
        #endregion

        #region Suggestion Tab (3 button clicks)
        protected void btnSuggestSPLink_Click(object sender, EventArgs e)
        {
            mpeSuggestModal.Show();
        }
        protected void btnSuggest_Click(object sender, EventArgs e)
        {
            txtSuggestServiceProvider.Text.Trim();
            if (txtSuggestServiceProvider.Text != "" && txtSuggestServiceProvider.Text != null && ddlSuggestLocation.SelectedValue != "0" && ddlSuggestLocation.SelectedValue != null)
            {
                bll.Name = txtSuggestServiceProvider.Text.Trim();
                bll.LocationID = ddlSuggestLocation.SelectedValue;
                if (Regex.IsMatch(bll.Name, @"^[a-zA-Z0-9- ]+$"))
                {
                    if (bll.CheckExistingServiceProviderAndLocation().Rows.Count == 0)
                    {
                        bll.ServiceProviderID = bll.InsertServiceProviderInSuggestion().Rows[0][0].ToString();
                        Audit(uid, "INSERT", "Service_Providers", bll.ServiceProviderID);
                        bll.ServiceProviderLocationID = bll.InsertServiceProviderAndLocationInSuggestion().Rows[0][0].ToString();
                        Audit(uid, "INSERT", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                        bll.Username = uid; string SuggestionID = bll.InsertSuggestion().Rows[0][0].ToString();
                        Audit(uid, "INSERT", "Suggestions", SuggestionID);
                        lblSuggestError.Text = ""; txtSuggestServiceProvider.Text = ""; ddlSuggestLocation.SelectedValue = "0";
                        mpeSuggestModal.Hide(); mpeSuggestSuccess.Show();
                    }
                    else
                    {
                        mpeSuggestModal.Show();
                        lblSuggestError.Text = "The service provider is already existing.";
                    }
                }
                else
                {
                    mpeSuggestModal.Show();
                    lblSuggestError.Text = "Special characters are not allowed.";
                }
            }
            else
            {
                mpeSuggestModal.Show();
                lblSuggestError.Text = "Please input a name and a location";
            }
        }
        protected void btnSuggestCancel_Click(object sender, EventArgs e)
        {
            lblSuggestError.Text = ""; txtSuggestServiceProvider.Text = ""; ddlSuggestLocation.SelectedValue = "0";
        }
        #endregion

        #region Comment Tab
        #endregion

        #region Chart Tab
        #endregion

        #region Bind Drop Down List and Grid View
        protected void ddlServiceProviderInRatingTab_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRateError.Text = "";
            bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
            ddlLocation.DataSource = bll.LocationsSelectUsingServiceProviderID();
            ddlLocation.DataTextField = "name";
            ddlLocation.DataValueField = "location_id";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
        }

        //Service Provider ddL for Comment
        //Location ddL for comment
        //type ddL for comment

        //Service Provider ddL for chart
        //Location ddL for chart
        #endregion

        protected void btnRatingNo_Click(object sender, EventArgs e)
        {
            commentsSuggestions.Text = "";
            mpeRatingSubmit.Hide();
        }

        protected void filterCommentsInUser_Click(object sender, EventArgs e)
        {
            bll.IsDeleted = "n";
            bll.ServiceProviderID = ddlSearchServiceProviders.SelectedValue;
            bll.LocationID = ddlSearchLocations.SelectedValue;
            bll.Type = ddlSearchRespondents.SelectedValue;
            if (ddlSearchServiceProviders.SelectedValue != "All" && ddlSearchLocations.SelectedValue == "All" && ddlSearchRespondents.SelectedValue == "All")
            {
                filterFunction(bll.CommentReviewFilter1(), bll.CommentRecommendationFilter1());
            }
            else if (ddlSearchServiceProviders.SelectedValue == "All" && ddlSearchLocations.SelectedValue != "All" && ddlSearchRespondents.SelectedValue == "All")
            {
                filterFunction(bll.CommentReviewFilter2(), bll.CommentRecommendationFilter2());
            }
            else if (ddlSearchServiceProviders.SelectedValue == "All" && ddlSearchLocations.SelectedValue == "All" && ddlSearchRespondents.SelectedValue != "All")
            {
                filterFunction(bll.CommentReviewFilter3(), bll.CommentRecommendationFilter3());
            }
            else if (ddlSearchServiceProviders.SelectedValue != "All" && ddlSearchLocations.SelectedValue != "All" && ddlSearchRespondents.SelectedValue == "All")
            {
                filterFunction(bll.CommentReviewFilter4(), bll.CommentRecommendationFilter4());
            }
            else if (ddlSearchServiceProviders.SelectedValue == "All" && ddlSearchLocations.SelectedValue != "All" && ddlSearchRespondents.SelectedValue != "All")
            {
                filterFunction(bll.CommentReviewFilter5(), bll.CommentRecommendationFilter5());
            }
            else if (ddlSearchServiceProviders.SelectedValue != "All" && ddlSearchLocations.SelectedValue == "All" && ddlSearchRespondents.SelectedValue != "All")
            {
                filterFunction(bll.CommentReviewFilter6(), bll.CommentRecommendationFilter6());
            }
            else if (ddlSearchServiceProviders.SelectedValue != "All" && ddlSearchLocations.SelectedValue != "All" && ddlSearchRespondents.SelectedValue != "All")
            {
                filterFunction(bll.CommentReviewFilter(), bll.CommentRecommendationFilter());
            }
            else if (ddlSearchServiceProviders.SelectedValue == "All" && ddlSearchLocations.SelectedValue == "All" && ddlSearchRespondents.SelectedValue == "All")
            {
                filterFunction(bll.CommentReviewFilterAll(), bll.CommentRecommendationFilterAll());
            }
        }

        protected void ddlChartServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            bll.IsDeleted = "n";
            bll.ServiceProviderID = ddlChartServiceProvider.SelectedValue;
            ddlChartLocation.DataSource = bll.LocationsSelectUsingServiceProviderID();
            ddlChartLocation.DataTextField = "name";
            ddlChartLocation.DataValueField = "location_id";
            ddlChartLocation.DataBind();
            ddlChartLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
        }

        protected void btnFilterChart_Click(object sender, EventArgs e)
        {
            if (ddlChartServiceProvider.SelectedValue == "0" || ddlChartLocation.SelectedValue == "0")
            {
                DrawBarChart();
                lblChartsError.Text = "Please select a service provider and location.";
            }
            else
            {
                lblChartsError.Text = "";
                DrawPieChart();
            }
        }

        #region functions

        private void sendNotification()
        {
            string emailMessage = "";
            bool isPerformanceLow = false;
            DataTable dt = bll.NumberOfRatingsPerServiceProviderAndLocation();
            for (int counter = 0; bll.NumberOfRatingsPerServiceProviderAndLocation().Rows.Count > counter; counter++)
            {
                int numberOfRating = int.Parse(bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][0].ToString());
                string rating = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][1].ToString();
                string serviceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][2].ToString();
                string location = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][3].ToString();
                if (dt.Rows.Count == 1)
                {
                    if (rating == "sad")
                    {
                        emailMessage = emailMessage + "\n" + serviceProvider + " in " + location + " campus";
                        isPerformanceLow = true;
                    }
                }
                else
                {
                    if (counter != 0)
                    {
                        string previousServiceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter - 1][2].ToString();
                        string previousLocation = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter - 1][3].ToString();
                        int previousNumberOfRating = int.Parse(bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter - 1][0].ToString());
                        if (serviceProvider == previousServiceProvider && location == previousLocation && numberOfRating >= previousNumberOfRating)
                        {
                            emailMessage = emailMessage + "\n" + serviceProvider + " in " + location + " campus";
                            isPerformanceLow = true;
                        }
                        else if (counter != (bll.NumberOfRatingsPerServiceProviderAndLocation().Rows.Count - 1))
                        {
                            string nextServiceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter + 1][2].ToString();
                            string nextLocation = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter + 1][3].ToString();
                            if ((serviceProvider != nextServiceProvider || location != nextLocation) && (serviceProvider != previousServiceProvider || location != previousLocation) && rating == "sad")
                            {
                                emailMessage = emailMessage + "\n" + serviceProvider + " in " + location + " campus";
                                isPerformanceLow = true;
                            }
                        }
                    }
                    else if (counter == 0)
                    {
                        string nextServiceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter + 1][2].ToString();
                        string nextLocation = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter + 1][3].ToString();
                        if (serviceProvider != nextServiceProvider || location != nextLocation && rating == "sad")
                        {
                            emailMessage = emailMessage + "\n" + serviceProvider + " in " + location + " campus";
                            isPerformanceLow = true;
                        }
                    }
                }
                
            }
            if (isPerformanceLow == true)
            {

                sendEmail(emailMessage);

            }
        }

        private void sendEmail(string message)
        {
            bll.Date = DateTime.Now.ToString();
            bll.EmailAddress = bll.getEmailAddress().Rows[0][0].ToString();
            try
            {
                DataTable recentEmailTrail = bll.getEmailAddressDate();
                if (recentEmailTrail.Rows.Count == 0)
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("teamjarr2014@gmail.com", "Benilde Service Rating");
                    mail.To.Add(bll.EmailAddress);
                    mail.Subject = "Performance Notification";
                    mail.Body = "This email notification is to inform you that the following service providers: " +
                        message + "\n\nHave a negative rating that is greater than or equal to 50%. Kindly take " +
                        "immediate action to rectify the subpar performance of the service providers listed above.";
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("teamjarr2014@gmail.com", "ermitanogarcialeycanoyu");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    bll.EmailTrails();
                }
                else
                {
                    DateTime emailAddressDate = DateTime.Parse(recentEmailTrail.Rows[0][2].ToString());
                    if ((DateTime.Now - emailAddressDate).TotalHours >= 0)
                    {
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                        mail.From = new MailAddress("teamjarr2014@gmail.com", "Benilde Service Rating");
                        mail.To.Add(bll.EmailAddress);
                        mail.Subject = "Performance Notification";
                        mail.Body = "This email notification is to inform you that the following service providers: " +
                            message + "\n\nHave a negative rating that is greater than or equal to 50%. Kindly take " +
                            "immediate action to rectify the subpar performance of the service providers listed above.";
                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("teamjarr2014@gmail.com", "ermitanogarcialeycanoyu");
                        SmtpServer.EnableSsl = true;
                        SmtpServer.Send(mail);
                        bll.EmailTrails();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
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
                                sadList.Add(dt.Rows[counter+1][0].ToString());
                            }
                            else
                            {
                                sadList.Add(dt.Rows[counter][0].ToString());
                                happyList.Add(dt.Rows[counter+1][0].ToString());
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
                ltrPieChart.Text = summaryRatings.ToHtmlString();
            }
        }
        private void filterFunction(DataTable dtReview, DataTable dtRecommendation)
        {
            if (dtReview.Rows.Count == 3)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString();
                txtReview2.Text = dtReview.Rows[1][0].ToString();
                txtReview3.Text = dtReview.Rows[2][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString();
                lblReviewDate2.Text = DateTime.Parse(dtReview.Rows[1][1].ToString()).ToShortDateString();
                lblReviewDate3.Text = DateTime.Parse(dtReview.Rows[2][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString();
                lblReviewIdNum2.Text = dtReview.Rows[1][2].ToString() + " " + dtReview.Rows[1][3].ToString();
                lblReviewIdNum3.Text = dtReview.Rows[2][2].ToString() + " " + dtReview.Rows[2][3].ToString();
                lblReviewSPL1.Text = ": " + dtReview.Rows[0][4].ToString() + " - " + dtReview.Rows[0][5].ToString();
                lblReviewSPL2.Text = ": " + dtReview.Rows[1][4].ToString() + " - " + dtReview.Rows[1][5].ToString();
                lblReviewSPL3.Text = ": " + dtReview.Rows[2][4].ToString() + " - " + dtReview.Rows[2][5].ToString();
            }
            else if (dtReview.Rows.Count == 2)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString();
                txtReview2.Text = dtReview.Rows[1][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString();
                lblReviewDate2.Text = DateTime.Parse(dtReview.Rows[1][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString();
                lblReviewIdNum2.Text = dtReview.Rows[1][2].ToString() + " " + dtReview.Rows[1][3].ToString();
                lblReviewSPL1.Text = ": " + dtReview.Rows[0][4].ToString() + " - " + dtReview.Rows[0][5].ToString();
                lblReviewSPL2.Text = ": " + dtReview.Rows[1][4].ToString() + " - " + dtReview.Rows[1][5].ToString();
                txtReview3.Text = "";
                lblReviewDate3.Text = "";
                lblReviewIdNum3.Text = "";
                lblReviewSPL3.Text = "";
            }
            else if (dtReview.Rows.Count == 1)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString();
                lblReviewSPL1.Text = ": " + dtReview.Rows[0][4].ToString() + " - " + dtReview.Rows[0][5].ToString();
                txtReview2.Text = "";
                lblReviewDate2.Text = "";
                lblReviewIdNum2.Text = "";
                txtReview3.Text = "";
                lblReviewDate3.Text = "";
                lblReviewIdNum3.Text = "";
                lblReviewSPL2.Text = "";
                lblReviewSPL3.Text = "";

            }
            else if (dtReview.Rows.Count == 0)
            {
                txtReview1.Text = ""; lblReviewDate1.Text = ""; lblReviewIdNum1.Text = ""; lblReviewSPL1.Text = "";
                txtReview2.Text = ""; lblReviewDate2.Text = ""; lblReviewIdNum2.Text = ""; lblReviewSPL2.Text = "";
                txtReview3.Text = ""; lblReviewDate3.Text = ""; lblReviewIdNum3.Text = ""; lblReviewSPL3.Text = "";
            }
            if (dtRecommendation.Rows.Count == 3)
            {
                txtRecommendation1.Text = dtRecommendation.Rows[0][0].ToString();
                txtRecommendation2.Text = dtRecommendation.Rows[1][0].ToString();
                txtRecommendation3.Text = dtRecommendation.Rows[2][0].ToString();
                lblRecommendationDate1.Text = DateTime.Parse(dtRecommendation.Rows[0][1].ToString()).ToShortDateString();
                lblRecommendationDate2.Text = DateTime.Parse(dtRecommendation.Rows[1][1].ToString()).ToShortDateString();
                lblRecommendationDate3.Text = DateTime.Parse(dtRecommendation.Rows[2][1].ToString()).ToShortDateString();
                lblRecommendationIdNum1.Text = dtRecommendation.Rows[0][2].ToString() + " " + dtRecommendation.Rows[0][3].ToString();
                lblRecommendationIdNum2.Text = dtRecommendation.Rows[1][2].ToString() + " " + dtRecommendation.Rows[1][3].ToString();
                lblRecommendationIdNum3.Text = dtRecommendation.Rows[2][2].ToString() + " " + dtRecommendation.Rows[2][3].ToString();
                lblRecommendationSPL1.Text = ": " + dtRecommendation.Rows[0][4].ToString() + " - " + dtRecommendation.Rows[0][5].ToString();
                lblRecommendationSPL2.Text = ": " + dtRecommendation.Rows[1][4].ToString() + " - " + dtRecommendation.Rows[1][5].ToString();
                lblRecommendationSPL3.Text = ": " + dtRecommendation.Rows[2][4].ToString() + " - " + dtRecommendation.Rows[2][5].ToString();
            }
            else if (dtRecommendation.Rows.Count == 2)
            {
                txtRecommendation1.Text = dtRecommendation.Rows[0][0].ToString();
                txtRecommendation2.Text = dtRecommendation.Rows[1][0].ToString();
                lblRecommendationDate1.Text = DateTime.Parse(dtRecommendation.Rows[0][1].ToString()).ToShortDateString();
                lblRecommendationDate2.Text = DateTime.Parse(dtRecommendation.Rows[1][1].ToString()).ToShortDateString();
                lblRecommendationIdNum1.Text = dtRecommendation.Rows[0][2].ToString() + " " + dtRecommendation.Rows[0][3].ToString();
                lblRecommendationIdNum2.Text = dtRecommendation.Rows[1][2].ToString() + " " + dtRecommendation.Rows[1][3].ToString();
                lblRecommendationSPL1.Text = ": " + dtRecommendation.Rows[0][4].ToString() + " - " + dtRecommendation.Rows[0][5].ToString();
                lblRecommendationSPL2.Text = ": " + dtRecommendation.Rows[1][4].ToString() + " - " + dtRecommendation.Rows[1][5].ToString();
                lblRecommendationSPL3.Text = "";
                txtRecommendation3.Text = "";
                lblRecommendationDate3.Text = "";
                lblRecommendationIdNum3.Text = "";
            }
            else if (dtRecommendation.Rows.Count == 1)
            {
                txtRecommendation1.Text = dtRecommendation.Rows[0][0].ToString();
                lblRecommendationDate1.Text = DateTime.Parse(dtRecommendation.Rows[0][1].ToString()).ToShortDateString();
                lblRecommendationIdNum1.Text = dtRecommendation.Rows[0][2].ToString() + " " + dtRecommendation.Rows[0][3].ToString();
                lblRecommendationSPL1.Text = ": " + dtRecommendation.Rows[0][4].ToString() + " - " + dtRecommendation.Rows[0][5].ToString();
                lblRecommendationSPL2.Text = "";
                lblRecommendationSPL3.Text = "";
                txtRecommendation2.Text = "";
                lblRecommendationDate2.Text = "";
                lblRecommendationIdNum2.Text = "";
                txtRecommendation3.Text = "";
                lblRecommendationDate3.Text = "";
                lblRecommendationIdNum3.Text = "";
            }
            else if (dtRecommendation.Rows.Count == 0)
            {
                txtRecommendation1.Text = ""; lblRecommendationDate1.Text = ""; lblRecommendationIdNum1.Text = ""; lblRecommendationSPL1.Text = "";
                txtRecommendation2.Text = ""; lblRecommendationDate2.Text = ""; lblRecommendationIdNum2.Text = ""; lblRecommendationSPL2.Text = "";
                txtRecommendation3.Text = ""; lblRecommendationDate3.Text = ""; lblRecommendationIdNum3.Text = ""; lblRecommendationSPL3.Text = "";
            }
        }
        private void DrawPieChart()
        {
            bll.ServiceProviderID = ddlChartServiceProvider.SelectedValue; bll.LocationID = ddlChartLocation.SelectedValue;
            DataTable dt = bll.FilterPieChart();
            List<string> serviceProviderList = new List<string>();
            List<object> happyList = new List<object>();
            List<object> sadList = new List<object>();
            if (dt.Rows.Count == 2)
            {
                string rating = dt.Rows[0][1].ToString();
                if (rating.Equals("happy"))
                {
                    happyList.Add(dt.Rows[0][0].ToString());
                    sadList.Add(dt.Rows[1][0].ToString());
                }
                else
                {
                    sadList.Add(dt.Rows[0][0].ToString());
                    happyList.Add(dt.Rows[1][0].ToString());
                }
            }
            else if (dt.Rows.Count == 1)
            {
                string rating = dt.Rows[0][1].ToString();
                if (rating.Equals("happy"))
                {
                    happyList.Add(dt.Rows[0][0].ToString());
                    sadList.Add("0");
                }
                else if (rating.Equals("sad"))
                {
                    sadList.Add(dt.Rows[0][0].ToString());
                    happyList.Add("0");
                }
            }
            else
            {
                happyList.Add("0");
                sadList.Add("0");
            }

            DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
               .InitChart(new Chart { DefaultSeriesType = ChartTypes.Pie })
               .SetPlotOptions(new PlotOptions
               {
                   Pie = new PlotOptionsPie
                   {
                       DataLabels = new PlotOptionsPieDataLabels
                       {
                           Enabled = true,
                           Format = "<b>{point.name}</b>: {point.percentage:.0f} %",
                       }
                   }
               })
               .SetYAxis(new YAxis
               {
                   Max = (int.Parse(happyList[0].ToString()) + int.Parse(sadList[0].ToString()))
               })
               .SetTitle(new Title
               {
                   Text = ddlChartServiceProvider.SelectedItem + ": " + ddlChartLocation.SelectedItem
               })
               .SetSeries(new[]
               {
                   new Series {
                       Name = "Ratings",
                       Data = new Data(new object[]
                       {
                           new DotNet.Highcharts.Options.Point 
                           { 
                               Name = "Sad", 
                               Y = int.Parse(sadList[0].ToString()),
                               Color = System.Drawing.Color.FromArgb(255, 217, 30, 24)
                           },
                           new DotNet.Highcharts.Options.Point 
                           { 
                               Name = "Happy",
                               Y = int.Parse(happyList[0].ToString()),
                               Color = System.Drawing.Color.FromArgb(255,244,208,63),
                               Sliced = true
                           }
                       })
                   }
               });
            ltrPieChart.Text = chart.ToHtmlString();
        }
        private void BindSearchServiceProvidersDDL()
        {
            bll.IsDeleted = "n";
            ddlSearchServiceProviders.DataSource = bll.ServiceProviderView();
            ddlSearchServiceProviders.DataTextField = "name";
            ddlSearchServiceProviders.DataValueField = "service_provider_id";
            ddlSearchServiceProviders.DataBind();
        }
        private void BindSearchLocationsDDL()
        {
            ddlSearchLocations.DataSource = bll.LocationView();
            ddlSearchLocations.DataTextField = "name";
            ddlSearchLocations.DataValueField = "location_id";
            ddlSearchLocations.DataBind();

            ddlSuggestLocation.DataSource = bll.LocationView();
            ddlSuggestLocation.DataTextField = "name";
            ddlSuggestLocation.DataValueField = "location_id";
            ddlSuggestLocation.DataBind();
        }
        private void BindSearchBenildeanTypeDDL()
        {
            ddlSearchRespondents.DataSource = bll.SortBenildeanType();
            ddlSearchRespondents.DataTextField = "type";
            ddlSearchRespondents.DataValueField = "type";
            ddlSearchRespondents.DataBind();
        }
        private void Audit(string userName, string action, string tableAffected, string idAffected)
        {
            bll.Username = userName;
            bll.Action = action;
            bll.TableAffected = tableAffected;
            bll.IdAffected = idAffected;
            bll.Date = DateTime.Now.ToString();
            bll.AuditTrailInsert();
        }
        private void BindChartServiceProviderDDL()
        {
            bll.IsDeleted = "n";
            bll.IsApproved = "y";
            ddlChartServiceProvider.DataSource = bll.ServiceProviderSelectWithLocations();
            ddlChartServiceProvider.DataTextField = "name";
            ddlChartServiceProvider.DataValueField = "service_provider_id";
            ddlChartServiceProvider.DataBind();
            ddlChartServiceProvider.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
        }
        #endregion

        protected void btnViewYourComment_Click(object sender, EventArgs e)
        {
            btnFilterChart_Click(sender, e);
        }

        protected void btnViewYourRating_Click(object sender, EventArgs e)
        {
            btnFilterChart_Click(sender, e);
        }
    }
}