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
            lblRateError.Text = "";
            //bll.Username = Session["uid"].ToString();
            //uid = bll.Username;
            //lbluName.Text = bll.GetNameUsingSession().Rows[0][0].ToString() + " " + bll.GetNameUsingSession().Rows[0][1].ToString();
            bll.IsActive = "y";
            bll.IsDeleted = "n";
            lblQuestion.Text = bll.QuestionSelectActive().Rows[0][1].ToString();
            if (!Page.IsPostBack)
            {
                BindSearchServiceProvidersDDL(); BindSearchLocationsDDL(); BindSearchBenildeanTypeDDL(); BindChartServiceProviderDDL();
                ddlServiceProvider.DataSource = bll.ServiceProviderView();
                ddlServiceProvider.DataTextField = "name";
                ddlServiceProvider.DataValueField = "service_provider_id";
                ddlServiceProvider.DataBind();
                ddlServiceProvider.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
                ddlSuggestLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
                ddlLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
                ddlChartLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("login.aspx");
        }
        protected void ddlServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRateError.Text = ""; bll.IsDeleted = "n"; bll.IsApproved = "y";
            bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
            ddlLocation.DataSource = bll.LocationView();
            ddlLocation.DataTextField = "name";
            ddlLocation.DataValueField = "location_id";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
        }
        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            bll.LocationID = ddlLocation.SelectedValue;
        }
        protected void btnSuggest_Click(object sender, EventArgs e)
        {
            txtSuggestServiceProvider.Text.Trim();
            if (txtSuggestServiceProvider.Text != "" && txtSuggestServiceProvider.Text != null && ddlSuggestLocation.SelectedValue != "0" && ddlSuggestLocation.SelectedValue != null)
            {
                string value = "\""; value = Regex.Escape(value);
                string serviceProvider = Regex.Replace(txtSuggestServiceProvider.Text, value, "");
                bll.Name = Regex.Replace(serviceProvider, "'", ""); bll.LocationID = ddlSuggestLocation.SelectedValue;
                if (bll.CheckExistingServiceProviderAndLocation().Rows.Count == 0)
                {
                    bll.ServiceProviderID = bll.InsertServiceProviderInSuggestion().Rows[0][0].ToString();
                    Audit(uid, "INSERT", "Service_Providers", bll.ServiceProviderID);
                    bll.ServiceProviderLocationID = bll.InsertServiceProviderAndLocationInSuggestion().Rows[0][0].ToString();
                    Audit(uid, "INSERT", "Service_Providers_Locations", bll.ServiceProviderLocationID);
                    bll.Username = uid; string SuggestionID = bll.InsertSuggestion().Rows[0][0].ToString();
                    Audit(uid, "INSERT", "Suggestions", SuggestionID);
                    lblSuggestError.Text = ""; txtSuggestServiceProvider.Text = ""; ddlSuggestLocation.SelectedValue = "0";
                    ModalPopupExtender2.Hide(); ModalPopupExtender3.Show();
                }
                else
                {
                    ModalPopupExtender2.Show();
                    lblSuggestError.Text = "The Service Provider is already existing.";
                }
            }
            else
            {
                ModalPopupExtender2.Show();
                lblSuggestError.Text = "Please input the desired service provider and location.";
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblSuggestError.Text = ""; txtSuggestServiceProvider.Text = ""; ddlSuggestLocation.SelectedValue = "0";
        }
        protected void btnSuggestSPLink_Click(object sender, EventArgs e)
        {
            ModalPopupExtender2.Show();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ddlLocation.SelectedValue!="0" && ddlLocation.SelectedValue != null && ddlLocation.SelectedValue != "" 
                && ddlServiceProvider.SelectedValue!="0" && ddlServiceProvider.SelectedValue !=null && ddlServiceProvider.SelectedValue != "")
            {
                if (HappyOrSad.Text!=null && HappyOrSad.Text != "")
                {
                    lblRateError.Text = ""; bll.Username = uid;
                    bll.LocationID = ddlLocation.SelectedValue;
                    bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
                    bll.ServiceProviderLocationID = bll.GetServiceProviderLocationID().Rows[0][0].ToString();
                    if (bll.GetRatingUsingUsername().Rows.Count == 0)
                    {
                        ModalPopupExtender1.Show(); 
                    }
                    else
                    {
                        DateTime userRatingDate = (DateTime)bll.GetRatingUsingUsername().Rows[0][4];
                        bll.IsActive = "y";
                        int RateLimit = int.Parse(bll.LimitSelectActive().Rows[0][1].ToString());
                        if ((DateTime.Now - userRatingDate).TotalHours >= RateLimit)
                        {
                            ModalPopupExtender1.Show(); 
                        }
                        else
                        {
                           lblRateError.Text = "You can only rate this service provider once every " + RateLimit + " hour(s).";
                        }
                    }
                }
                else
                {
                    lblRateError.Text = "Please rate the service provider"; HappyOrSad.Text = null;
                }
            }
            else
            {
                lblRateError.Text = "Please select a service provider and location"; 
            }
        }
        protected void btnSubmitRatingAndComment_Click(object sender, EventArgs e)
        {
            lblRateError.Text = ""; bll.Username = uid;
            bll.LocationID = ddlLocation.SelectedValue;
            bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
            bll.ServiceProviderLocationID = bll.GetServiceProviderLocationID().Rows[0][0].ToString();
            bll.Date = DateTime.Now.ToString(); bll.Username = uid; bll.Rating = HappyOrSad.Text;
            bll.RatingID = bll.RatingAdd().Rows[0][0].ToString();
            Audit(uid, "INSERT", "Ratings", bll.RatingID);
            sendNotification();
            string comment = commentsSuggestions.Text.Trim();
            if (comment != "" && comment != null)
            {
                string value = "\""; value = Regex.Escape(value);
                string trimmedComments = Regex.Replace(comment, value, "");
                bll.Comment = Regex.Replace(trimmedComments, "'", "");
                if (HappyOrSad.Text.Equals("happy"))
                {
                    bll.IsReview = "y";
                }
                else
                {
                    bll.IsReview = "n";
                }
                bll.IsDeleted = "n"; bll.CommentID = bll.CommentAdd().Rows[0][0].ToString();
                Audit(uid, "INSERT", "Comments", bll.CommentID);
            }
            ModalPopupExtender1.Hide(); ModalPopupExtender4.Show();
            ddlServiceProvider.SelectedValue = "0"; ddlLocation.SelectedValue = "0"; commentsSuggestions.Text = ""; HappyOrSad.Text = "";
        }
        protected void btnSubmitRating_Click(object sender, EventArgs e)
        {
            lblRateError.Text = ""; bll.Username = uid;
            bll.LocationID = ddlLocation.SelectedValue;
            bll.ServiceProviderID = ddlServiceProvider.SelectedValue;
            bll.ServiceProviderLocationID = bll.GetServiceProviderLocationID().Rows[0][0].ToString();
            bll.Date = DateTime.Now.ToString(); bll.Username = uid; bll.Rating = HappyOrSad.Text;
            bll.RatingID = bll.RatingAdd().Rows[0][0].ToString();
            ddlServiceProvider.SelectedValue = "0"; ddlLocation.SelectedValue = "0"; commentsSuggestions.Text = ""; HappyOrSad.Text = "";
            Audit(uid, "INSERT", "Ratings", bll.RatingID);
            sendNotification();
            ModalPopupExtender1.Hide();
            ModalPopupExtender4.Show();
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
            ddlChartLocation.DataSource = bll.LocationSelectWithRatingsUsingServiceProviderID();
            ddlChartLocation.DataTextField = "name";
            ddlChartLocation.DataValueField = "location_id";
            ddlChartLocation.DataBind();
            ddlChartLocation.Items.Insert(0, new ListItem("Please select a location", "0", true));
        }

        protected void btnFilterChart_Click(object sender, EventArgs e)
        {
            if (ddlChartServiceProvider.SelectedValue == "0" || ddlChartLocation.SelectedValue == "0")
            {
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

            bll.NumberOfRatingsPerServiceProviderAndLocation();

            for (int counter = 0; bll.NumberOfRatingsPerServiceProviderAndLocation().Rows.Count > counter; counter++)

            {

                int numberOfRating = int.Parse(bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][0].ToString());

                string rating = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][1].ToString();

                string serviceProvider = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][2].ToString();

                string location = bll.NumberOfRatingsPerServiceProviderAndLocation().Rows[counter][3].ToString();

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

                DateTime emailAddressDate = (DateTime)bll.getEmailAddressDate().Rows[0][2];

                if ((DateTime.Now - emailAddressDate).TotalHours >= 24)
                {

                    MailMessage mail = new MailMessage();

                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                    mail.From = new MailAddress("teamjarr2014@gmail.com", "Benilde Service Rating");

                    mail.To.Add(bll.EmailAddress);

                    mail.Subject = "Performance Notification";

                    mail.Body = "This email notification is to inform you that the Service Provider(s): " + message + "\n\nhas a negative rating that is greater than or equal to 50%";

                    SmtpServer.Port = 587;

                    SmtpServer.Credentials = new System.Net.NetworkCredential("teamjarr2014@gmail.com", "ermitanogarcialeycanoyu");

                    SmtpServer.EnableSsl = true;

                    SmtpServer.Send(mail);

                    bll.EmailTrails();

                }

                else
                {

                    lblRateError.Text = "Email limit is 1 day";

                }

            }

            catch (Exception ex)
            {
            }
        }
        private void filterFunction(DataTable dtReview, DataTable dtRecommendation)
        {
            if (dtReview.Rows.Count == 3)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString(); txtReview2.Text = dtReview.Rows[1][0].ToString(); txtReview3.Text = dtReview.Rows[2][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString(); lblReviewDate2.Text = DateTime.Parse(dtReview.Rows[1][1].ToString()).ToShortDateString(); lblReviewDate3.Text = DateTime.Parse(dtReview.Rows[2][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString(); lblReviewIdNum2.Text = dtReview.Rows[1][2].ToString() + " " + dtReview.Rows[1][3].ToString(); lblReviewIdNum3.Text = dtReview.Rows[2][2].ToString() + " " + dtReview.Rows[2][3].ToString();
            }
            else if (dtReview.Rows.Count == 2)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString(); txtReview2.Text = dtReview.Rows[1][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString(); lblReviewDate2.Text = DateTime.Parse(dtReview.Rows[1][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString(); lblReviewIdNum2.Text = dtReview.Rows[1][2].ToString() + " " + dtReview.Rows[1][3].ToString();
                txtReview3.Text = ""; lblReviewDate3.Text = ""; lblReviewIdNum3.Text = "";
            }
            else if (dtReview.Rows.Count == 1)
            {
                txtReview1.Text = dtReview.Rows[0][0].ToString();
                lblReviewDate1.Text = DateTime.Parse(dtReview.Rows[0][1].ToString()).ToShortDateString();
                lblReviewIdNum1.Text = dtReview.Rows[0][2].ToString() + " " + dtReview.Rows[0][3].ToString();
                txtReview2.Text = ""; lblReviewDate2.Text = ""; lblReviewIdNum2.Text = "";
                txtReview3.Text = ""; lblReviewDate3.Text = ""; lblReviewIdNum3.Text = "";
            }
            else if (dtReview.Rows.Count == 0)
            {
                txtReview1.Text = ""; lblReviewDate1.Text = ""; lblReviewIdNum1.Text = "";
                txtReview2.Text = ""; lblReviewDate2.Text = ""; lblReviewIdNum2.Text = "";
                txtReview3.Text = ""; lblReviewDate3.Text = ""; lblReviewIdNum3.Text = "";
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
            }
            else if (dtRecommendation.Rows.Count == 2)
            {
                txtRecommendation1.Text = dtRecommendation.Rows[0][0].ToString();
                txtRecommendation2.Text = dtRecommendation.Rows[1][0].ToString();
                lblRecommendationDate1.Text = DateTime.Parse(dtRecommendation.Rows[0][1].ToString()).ToShortDateString();
                lblRecommendationDate2.Text = DateTime.Parse(dtRecommendation.Rows[1][1].ToString()).ToShortDateString();
                lblRecommendationIdNum1.Text = dtRecommendation.Rows[0][2].ToString() + " " + dtRecommendation.Rows[0][3].ToString();
                lblRecommendationIdNum2.Text = dtRecommendation.Rows[1][2].ToString() + " " + dtRecommendation.Rows[1][3].ToString();
                txtRecommendation3.Text = ""; lblRecommendationDate3.Text = ""; lblRecommendationIdNum3.Text = "";
            }
            else if (dtRecommendation.Rows.Count == 1)
            {
                txtRecommendation1.Text = dtRecommendation.Rows[0][0].ToString();
                lblRecommendationDate1.Text = DateTime.Parse(dtRecommendation.Rows[0][1].ToString()).ToShortDateString();
                lblRecommendationIdNum1.Text = dtRecommendation.Rows[0][2].ToString() + " " + dtRecommendation.Rows[0][3].ToString();
                txtRecommendation2.Text = ""; lblRecommendationDate2.Text = ""; lblRecommendationIdNum2.Text = "";
                txtRecommendation3.Text = ""; lblRecommendationDate3.Text = ""; lblRecommendationIdNum3.Text = "";
            }
            else if (dtRecommendation.Rows.Count == 0)
            {
                txtRecommendation1.Text = ""; lblRecommendationDate1.Text = ""; lblRecommendationIdNum1.Text = "";
                txtRecommendation2.Text = ""; lblRecommendationDate2.Text = ""; lblRecommendationIdNum2.Text = "";
                txtRecommendation3.Text = ""; lblRecommendationDate3.Text = ""; lblRecommendationIdNum3.Text = "";
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
                               Sliced = true,
                               Selected = true
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
            ddlSearchRespondents.DataSource = bll.TypeView();
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
            ddlChartServiceProvider.DataSource = bll.ServiceProviderSelectWithRatings();
            ddlChartServiceProvider.DataTextField = "name";
            ddlChartServiceProvider.DataValueField = "service_provider_id";
            ddlChartServiceProvider.DataBind();
            ddlChartServiceProvider.Items.Insert(0, new ListItem("Please select a service provider", "0", true));
        }
        #endregion
    }
}