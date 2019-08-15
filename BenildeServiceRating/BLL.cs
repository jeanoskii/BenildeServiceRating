using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using ThreeTier.DataAccess;
using System.Data.SqlClient;

namespace ThreeTier
{
    namespace BusinessLogic
    {
        class BLL
        {
            #region get/set
            #region shared
            private string username;
            public string Username
            {
                get { return username; }
                set { username = value; }
            }
            private string idNumber;
            public string IDNumber
            {
                get { return idNumber; }
                set { idNumber = value; }
            }
            private string ratingID;
            public string RatingID
            {
                get { return ratingID; }
                set { ratingID = value; }
            }
            private string date;
            public string Date
            {
                get { return date; }
                set { date = value; }
            }
            private string serviceProviderID;
            public string ServiceProviderID
            {
                get { return serviceProviderID; }
                set { serviceProviderID = value; }
            }
            private string serviceProviderName;
            public string ServiceProviderName
            {
                get { return serviceProviderName; }
                set { serviceProviderName = value; }
            }
            private string locationName;
            public string LocationName
            {
                get { return locationName; }
                set { locationName = value; }
            }
            private string serviceProviderLocationID;
            public string ServiceProviderLocationID
            {
                get { return serviceProviderLocationID; }
                set { serviceProviderLocationID = value; }
            }
            private string locationID;
            public string LocationID
            {
                get { return locationID; }
                set { locationID = value; }
            }
            private string answerID;
            public string AnswerID
            {
                get { return answerID; }
                set { answerID = value; }
            }
            private string name;
            public string Name
            {
                get { return name; }
                set { name = value; }
            }
            private string isDeleted;
            public string IsDeleted
            {
                get { return isDeleted; }
                set { isDeleted = value; }
            }
            private string isActive;
            public string IsActive
            {
                get { return isActive; }
                set { isActive = value; }
            }
            #endregion

            #region Admins
            private string adminID;
            public string AdminID
            {
                get { return adminID; }
                set { adminID = value; }
            }
            #endregion

            #region Answer
            private string positive;
            public string Positive
            {
                get { return positive; }
                set { positive = value; }
            }
            private string negative;
            public string Negative
            {
                get { return negative; }
                set { negative = value; }
            }
            #endregion

            #region Benildean
            private string password;
            public string Password
            {
                get { return password; }
                set { password = value; }
            }
            private string isValid;
            public string IsValid
            {
                get { return isValid; }
                set { isValid = value; }
            }
            private string type;
            public string Type
            {
                get { return type; }
                set { type = value; }
            }
            #endregion

            #region Ratings
            private string rating;
            public string Rating
            {
                get { return rating; }
                set { rating = value; }
            }
            #endregion

            #region Comments
            private string commentID;
            public string CommentID
            {
                get { return commentID; }
                set { commentID = value; }
            }
            private string comment;
            public string Comment
            {
                get { return comment; }
                set { comment = value; }
            }
            private string isReview;
            public string IsReview
            {
                get { return isReview; }
                set { isReview = value; }
            }
            #endregion

            #region ServiceProvidersLocations
            private string isApproved;
            public string IsApproved
            {
                get { return isApproved; }
                set { isApproved = value; }
            }
            #endregion

            #region Questions
            private string questionID;
            public string QuestionID
            {
                get { return questionID; }
                set { questionID = value; }
            }
            private string question;
            public string Question
            {
                get { return question; }
                set { question= value; }
            }
            #endregion

            #region AuditTrails
            private string action;
            public string Action
            {
                get { return action; }
                set { action = value; }
            }
            private string tableAffected;
            public string TableAffected
            {
                get { return tableAffected; }
                set { tableAffected = value; }
            }
            private string idAffected;
            public string IdAffected
            {
                get { return idAffected; }
                set { idAffected = value; }
            }
            #endregion

            #region RateLimit
            private string limitID;
            public string LimitID
            {
                get { return limitID; }
                set { limitID = value;}
            }
            private string limit;
            public string Limit
            {
                get { return limit; }
                set { limit = value; }
            }
            #endregion

            #region EmailTrails
            private string emailAddress;
            public string EmailAddress
            {
                get { return emailAddress; }
                set { emailAddress = value; }
            }
            #endregion
            #endregion

            #region Commands

            #region AuditTrails (1 command) (Finished)
            public void AuditTrailInsert()
            {
                DAL.Execute("INSERT INTO Audit_Trails VALUES ('" + username + "','" + action + "','" + tableAffected + "','" + idAffected + "','" + date + "')");
            } //Used in All Pages
            #endregion

            #region LoginTrails
            public DataTable LoginTrailsView()
            {
                return DAL.GetData("SELECT date FROM Login_Trails where action = '" + action + "'");
            }
            public void LoginTrailInsert()
            {
                DAL.Execute("INSERT INTO Login_Trails VALUES ('" + username + "','" + action + "','" + date + "')");
            }
            #endregion

            #region Admins
            public DataTable AdminSelectCurrent()
            {
                return DAL.GetData("SELECT * FROM Admins WHERE is_active = '" + isActive + "' AND username = '" + username + "'");
            }
            public DataTable AdminCheckUsername()
            {
                return DAL.GetData("SELECT * FROM Admins WHERE username = '" + username + "'");
            }
            public DataTable AdminAdd()
            {
                return DAL.GetData("INSERT INTO Admins OUTPUT inserted.admin_id VALUES('" + username + "','y')");
            }
            public DataTable AdminSetActive()
            {
                return DAL.GetData("UPDATE Admins SET is_active = 'y' OUTPUT deleted.admin_id, inserted.admin_id WHERE username = '" + username + "'");
            }
            public DataTable AdminSetInactive()
            {
                return DAL.GetData("UPDATE Admins SET is_active = 'n' OUTPUT deleted.admin_id, inserted.admin_id WHERE username NOT IN ('" + username + "')");            
            }
            public DataTable AdminCheckIfExisting()
            {
                return DAL.GetData("Select * from Benildeans where (type='faculty' or type='staff') and username = '" + username + "' and is_valid = 'y'");
            }

            public DataTable GetAdminEmail()
            {
                return DAL.GetData("Select email from Benildeans, Admins where Benildeans.username = Admins.username and Admins.is_active = 'y'");
            }

            #endregion

            #region Login
            public DataTable LoginUser()
            {
                return DAL.GetData("SELECT * FROM Benildeans WHERE username = '" + username + "' AND is_valid = '" + isValid + "'");
            }
            public DataTable LoginAdmin()
            {
                return DAL.GetData("SELECT * FROM Admins WHERE username = '" + username + "' AND is_active = '" + isActive + "'");
            }
            
            public DataTable GetNameUsingSession()
            {
                return DAL.GetData("SELECT first_name, last_name from Benildeans where username = '" + username + "'");
            }
            public DataTable SessionSPMOCheck()
            {
                return DAL.GetData("Select * from Benildeans, Admins where Benildeans.username = Admins.username " +
                "and Admins.is_active = 'y' and Benildeans.is_valid = 'y' and Benildeans.username = '" + username + "'");
            }
            #endregion

            #region Rating
            public DataTable GetRatingUsingUsername()
            {
                return DAL.GetData("Select Top 1 * from Ratings where username='" + username + "' AND service_provider_location_id = '" + serviceProviderLocationID + "' ORDER BY date desc");
            }
            public DataTable RatingAdd()
            {
                return DAL.GetData("INSERT INTO Ratings OUTPUT inserted.rating_id VALUES ('" + username + "','" + serviceProviderLocationID + "','" + rating + "','" + date + "')");
            }
            public DataTable RatingView()
            {
                return DAL.GetData("SELECT * FROM Ratings");
            }
            #endregion

            #region Comments
            public void CommentDelete(string isDeleted, string commentID)
            {
                DAL.Execute("EXEC CommentDelete '" + isDeleted + "','" + commentID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to deleted the selected comment(s) in the grid view
            public DataTable SPMOCommentFilter1()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Service_Providers.service_provider_id = '" + serviceProviderID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter2()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Locations.location_id = '" + locationID +"'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter3()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Benildeans.type = '" + type + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter4()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter5()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter6()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Benildeans.type = '" + type + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter7()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view
            public DataTable SPMOCommentFilter8()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'ID Number', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +
                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +
                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Comments.is_deleted = 'n'");
            } //Used in SPMO Page : Manage - Comment Tab to display filtered comments in the grid view

            public DataTable CommentReviewFilter()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter1()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter2()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Locations.location_id = '" + locationID + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter3()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Benildeans.type = '" + type + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter4()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter5()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilter6()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' " +
                "and Benildeans.type = '" + type + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter1()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter2()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Locations.location_id = '" + locationID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter3()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Benildeans.type = '" + type + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter4()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Locations.location_id = '" + locationID + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter5()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Benildeans.type = '" + type + "' and Locations.location_id = '" + locationID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilter6()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' " +
                "and Benildeans.type = '" + type + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' ORDER BY NEWID()");
            }
            public DataTable CommentRecommendationFilterAll()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'n' ORDER BY NEWID()");
            }
            public DataTable CommentReviewFilterAll()
            {
                return DAL.GetData("Select top 3 comment, Ratings.date, Benildeans.first_name, Benildeans.last_name, Service_Providers.name, Locations.name " +
                "from Comments inner join Ratings on Comments.rating_id = Ratings.rating_id " +
                "inner join Service_Providers_Locations on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                "inner join Service_Providers on Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "inner join Locations on Service_Providers_Locations.location_id = Locations.location_id " +
                "inner join Benildeans on Ratings.username = Benildeans.username " +
                "where Comments.is_deleted = 'n' and Comments.is_review = 'y' ORDER BY NEWID()");
            }
            public DataTable CommentSearch()
            {
                return DAL.GetData("Select Comments.comment_id as 'Comment ID', " +
                "Ratings.username as 'Username', Benildeans.type as 'User Type', Comments.comment as 'Comment', " +

                "Service_Providers.name as 'Service Provider', Locations.name as 'Location', Ratings.date as 'Date' " +
                "from Comments, Ratings, Service_Providers_Locations, Service_Providers, Locations, Benildeans " +

                "where Comments.rating_id = Ratings.rating_id and Ratings.username = Benildeans.username " +
                "and Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +

                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Locations.name = '" + name +"'");
            }
            public DataTable CommentViewOld()
            {
                return DAL.GetData("SELECT * FROM Comments WHERE is_deleted = 'n'");
            }
            public DataTable CommentAdd()
            {
                return DAL.GetData("INSERT INTO Comments OUTPUT inserted.comment_id VALUES ('" + ratingID + "','" + comment + "','" + isReview + "','n')");
            }

            #endregion

            #region ServiceProvider
            public DataTable ServiceProviderSelectWithLocations()
            {
                return DAL.GetData("select distinct(Service_Providers.service_provider_id), Service_Providers.name from Locations, Service_Providers_Locations, Service_Providers " +
                    "where Locations.location_id = Service_Providers_Locations.location_id " +
                    "AND Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                    "AND Service_Providers_Locations.is_deleted = 'n' " +
                    "AND Service_Providers.is_deleted = 'n' " +
                    "AND Locations.is_deleted = 'n' " +
                    "AND Service_Providers_Locations.is_approved = 'y' " +
                    "AND Service_Providers.is_approved = 'y'");
            }
            public DataTable ServiceProviderView()
            {
                return DAL.GetData("Select DISTINCT(Service_Providers.service_provider_id), Service_Providers.name from Service_Providers, Service_Providers_Locations " +
                "where Service_Providers.is_deleted = 'n' and Service_Providers_Locations.is_deleted = 'n' and Service_Providers_Locations.is_approved = 'y' " +
                "and Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id");
            } //used in the drop down lists to display Service Providers that is not deleted and approved by the admin 
            public DataTable ServiceProviderViewAll()
            {
                return DAL.GetData("Select DISTINCT(Service_Providers.service_provider_id), Service_Providers.name from Service_Providers Where is_deleted = 'n' and is_approved = 'y'");
            } //used in the drop down lists to display all Service Providers that is not deleted
            public DataTable ServiceProviderViewWithoutID()
            {
                return DAL.GetData("Select DISTINCT(Service_Providers.name) as 'Service Provider' from Service_Providers Where is_deleted = 'n' and is_approved = 'y'");
            } //used in SPMO Manage - Service Provder Tab to display Service Provider Names in the gridview
            public DataTable ServiceProviderAdd()
            {
                return DAL.GetData("INSERT INTO Service_Providers OUTPUT inserted.service_provider_id VALUES ('" + name + "','n','y')");
            } //used in SPMO Manage - Service Provider Tab to insert a new Service Provider and return the ID value
            public DataTable ServiceProviderCheckExisting()
            {
                return DAL.GetData("SELECT * FROM Service_Providers WHERE name = '" + name + "' and is_approved = 'y'");
            } //used in SPMO Manage - Service Provider Tab to check if already existing
            public DataTable ServiceProviderCheckIfPending()
            {
                return DAL.GetData("SELECT * FROM Service_Providers WHERE name = '" + name + "' and is_approved = 'p'");
            }
            public DataTable ServiceProviderCheckIfDeleted()
            {
                return DAL.GetData("SELECT * FROM Service_Providers WHERE name = '" + name + "' and is_deleted = 'y' and is_approved = 'y'");
            }  //used in SPMO Manage - Service Provider Tab to check if the service provider is deleted
            public DataTable ServiceProviderDelete()
            {
                return DAL.GetData("UPDATE Service_Providers SET is_deleted = 'y' OUTPUT deleted.service_provider_id, inserted.service_provider_id WHERE name = '" + name + "'");
            } //used in SPMO Manage - Service Provider Tab to delete each service provider that is selected and return the ID value
            public DataTable ServiceProviderRestore()
            {
                return DAL.GetData("UPDATE Service_Providers SET is_deleted = 'n' OUTPUT deleted.service_provider_id, inserted.service_provider_id WHERE name = '" + name + "'");
            } //used in SPMO Manage - Service Provider Tab to restore deleted service provider and return the ID value
            public DataTable ServiceProviderSelectWithRatings()
            {
                return DAL.GetData("Select Distinct(Service_Providers.service_provider_id), Service_Providers.name from Service_Providers inner join Service_Providers_Locations " +
                    "on Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                    "inner join Locations on Locations.location_id = Service_Providers_Locations.location_id " +
                    "inner join Ratings on Ratings.service_provider_location_id = Service_Providers_Locations.service_provider_location_id " +
                    "where Service_Providers.is_deleted = '" + isDeleted + "' " +
                    "and Ratings.rating = 'happy' or Ratings.rating = 'sad' " +
                    "and Service_Providers_Locations.is_deleted = '" + isDeleted + "' " +
                    "and Locations.is_deleted = '" + isDeleted + "' "+
                    "and Service_Providers_Locations.is_approved = '" + isApproved + "' " +
                    "group by Service_Providers.service_provider_id, Service_Providers.name");
            } //used in USER - Chart Tab (not yet checked)
            public DataTable ServiceProviderNumberOfRatings()
            {
                return DAL.GetData("Select count(rating) as count, rating, Service_Providers.name from Ratings, Service_Providers_Locations, Service_Providers, Locations where Service_Providers_Locations.service_provider_location_id = Ratings.service_provider_location_id and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id and Service_Providers_Locations.location_id = Locations.location_id group by Service_Providers.name, rating order by Service_Providers.name");
            } // used in USER (not yet checked)
            public void ApproveDisapproveServiceProviderLocation()
            {
                DAL.Execute("Update Service_Providers_Locations SET is_approved = '" + isApproved + "' WHERE service_provider_location_id = '" + serviceProviderLocationID + "'");
            } //used in SPMO
            public DataTable ApproveDisapproveServiceProvider()
            {
                return DAL.GetData("Update Service_Providers SET is_approved = '"+ isApproved + "' OUTPUT deleted.service_provider_id, inserted.service_provider_id WHERE name = '" + serviceProviderName + "'");
            } //used in SPMO
            public DataTable NumberOfRatingsPerServiceProvider() 
            {
                return DAL.GetData("Select count(rating) as count, rating, Service_Providers.name from Ratings, Service_Providers_Locations, Service_Providers, Locations where Service_Providers_Locations.service_provider_location_id = Ratings.service_provider_location_id and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id and Service_Providers_Locations.location_id = Locations.location_id and Service_Providers_Locations.is_approved = 'y' and Service_Providers_Locations.is_deleted = 'n' group by Service_Providers.name, rating order by Service_Providers.name");
            } //used in USER Page
   
            #endregion

            #region ServiceProviderLocation
            public DataTable ServiceProviderLocationViewWithoutID()
            {
                return DAL.GetData("Select Distinct(Service_Providers.name) as 'Service Provider', Locations.name as 'Location' from Service_Providers_Locations, Service_Providers, Locations " +
                "where Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers_Locations.is_approved = 'y' and Service_Providers_Locations.is_deleted = 'n' order by Locations.name asc");
            } // used in SPMO Manage - Service Provider Location to display all service provider with location that is not delete and approved by the admin
            public DataTable ServiceProviderLocationCheckIfServiceProviderExist()
            {
                return DAL.GetData("Select * from Service_Providers_Locations where service_provider_id = '" + serviceProviderID + "'");
            } // used in SPMO Manage - Service Provider Tab to check if service provider exist in Service Provider Location TabLe
            public DataTable ServiceProviderLocationCheckIfLocationExist()
            {
                return DAL.GetData("Select * from Service_Providers_Locations where location_id = '" + locationID + "'");
            } // used in SPMO Manage - Service Provider Tab to check if service provider exist in Service Provider Location TabLe
            public DataTable ServiceProviderLocationRestore1()
            {
                return DAL.GetData("UPDATE Service_Providers_Locations SET is_deleted = 'n' OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id From Locations WHERE Service_Providers_Locations.location_id =  Locations.location_id and Locations.is_deleted = 'n' and service_provider_id = '" + serviceProviderID + "'");
            } //used in SPMO Manage - Service Provider Tab to restore deleted service provider location and return the ID value
            public DataTable ServiceProviderLocationDelete1()
            {
                return DAL.GetData("UPDATE Service_Providers_Locations SET is_deleted = 'y' OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id WHERE service_provider_id = '" + serviceProviderID + "'");
            } //used in SPMO Manage - Service Provider Tab to eachCheckBox service provider location and return the ID value
            public DataTable ServiceProviderLocationRestore2()
            {
                return DAL.GetData("UPDATE Service_Providers_Locations SET is_deleted = 'n' OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id From Service_Providers WHERE Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id and Service_Providers.is_deleted = 'n' and location_id = '" + locationID + "'");
            } //used in SPMO Manage - location Tab to restore deleted service provider location and return the ID value
            public DataTable ServiceProviderLocationDelete2()
            {
                return DAL.GetData("UPDATE Service_Providers_Locations SET is_deleted = 'y' OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id WHERE location_id = '" + locationID + "'");
            } //used in SPMO Manage - Location Tab to eachCheckBox service provider location and return the ID value
            public DataTable ServiceProviderLocationCheckIfServiceProviderExist2() 
            {
                return DAL.GetData("Select * from Service_Providers_Locations, Service_Providers, Locations " +
                "where Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers_Locations.service_provider_id = '" + serviceProviderID + "' and Service_Providers_Locations.location_id = '" + locationID + "' and Service_Providers_Locations.is_approved = 'y' and Service_Providers_Locations.is_deleted = 'n'");
            } // used in SPMO Manage - Service Provider Location
            public DataTable ServiceProviderLocationCheckIfServiceProviderExist3() //used in SPMO Manage - Service Provider Location Tab to check if the service provider and Location exist in the tabLe
            {
                return DAL.GetData("Select * from Service_Providers_Locations, Service_Providers, Locations " +
                "where Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers_Locations.service_provider_id = '" + serviceProviderID + "' and Service_Providers_Locations.location_id = '" + locationID + "' and Service_Providers_Locations.is_approved = 'y' and Service_Providers_Locations.is_deleted = 'y'");
            } // used in SPMO Manage - Service Provider Location
            public DataTable ServiceProviderLocationAdd()
            {
                return DAL.GetData("Insert into Service_Providers_Locations OUTPUT inserted.service_provider_location_id values ('" + locationID + "','"+ serviceProviderID +"','y','n')");
            }   // used in SPMO Manage - Service Provider Location
            public DataTable ServiceProviderLocationRestore()
            {
                return DAL.GetData("Update Service_Providers_Locations Set is_deleted ='n' OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id where service_provider_id ='" + serviceProviderID + "' and location_id = '" + locationID + "' and is_approved = 'y' and is_deleted = 'y'");
            } // used in SPMO Manage - Service Provider Location
            public DataTable ServiceProviderLocationDelete()
            {
                return DAL.GetData("Update Service_Providers_Locations Set is_deleted ='y' " +
                "OUTPUT deleted.service_provider_location_id, inserted.service_provider_location_id from Service_Providers, Locations " +
                "where Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers.name = '" + serviceProviderName + "' and Locations.name = '" + locationName + "'");
            } // used in SPMO Manage - Service Provider Location
            public DataTable GetServiceProviderLocationID()
            {
                return DAL.GetData("SELECT service_provider_location_id from Service_Providers_Locations WHERE location_id = '" + locationID + "' AND service_provider_id = '" + serviceProviderID + "'");
            }
            public DataTable ServiceProviderLocationView()
            {
                return DAL.GetData("SELECT * FROM Service_Providers_Locations WHERE is_deleted = '" + isDeleted + "'");
            } // used in SPMO
            public void ServiceProviderLocationApprove()
            {
                DAL.Execute("UPDATE Service_Providers SET is_approved = '" + isApproved + "' WHERE service_provider_location_id = '" + serviceProviderLocationID + "'");
            }
            public DataTable GetServiceProviderLocationUsingServiceProviderID()
            {
                return DAL.GetData("SELECT * FROM Service_Providers_Locations WHERE service_provider_id = '" + serviceProviderID + "'");
            }
            #endregion

            #region Locations
            public DataTable LocationsSelectUsingServiceProviderID()
            {
                return DAL.GetData("SELECT Locations.location_id, Locations.name FROM Locations, Service_Providers_Locations, Service_Providers " +
                    "WHERE Locations.location_id = Service_Providers_Locations.location_id " +
                    "AND Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                    "AND Service_Providers_Locations.is_deleted = 'n' " +
                    "AND Service_Providers.is_deleted = 'n' " +
                    "AND Locations.is_deleted = 'n' " +
                    "AND Service_Providers_Locations.is_approved = 'y' " +
                    "AND Service_Providers.is_approved = 'y' " +
                    "AND Service_Providers.service_provider_id = '" + serviceProviderID + "'");
            }
            public DataTable LocationView()
            {
                return DAL.GetData("Select DISTINCT(Locations.location_id), Locations.name from Locations, Service_Providers_Locations " +
                "where Locations.is_deleted = 'n' and Service_Providers_Locations.is_deleted = 'n' and Service_Providers_Locations.is_approved = 'y' " +
                "and Locations.location_id = Service_Providers_Locations.location_id");
            } //used in drop down lists to display Locations of the service providers that is not deleted
            public DataTable LocationViewAll()
            {
                return DAL.GetData("Select DISTINCT(location_id), Locations.name from Locations where is_deleted = 'n'");
            } //used in drop down to display aLL Locations of the service providers that is not deleted
            public DataTable LocationViewWithoutID()
            {
                return DAL.GetData("Select DISTINCT(Locations.name) as 'Locations' from Locations where is_deleted = 'n'");
            } //used in grid view to display Locations of the service providers that is not deleted
            public DataTable LocationAdd()
            {
                return DAL.GetData("INSERT INTO Locations OUTPUT inserted.location_id VALUES ('" + name + "','n')");
            } //used in the SPMO - Location Tab to add the desired Location and return the ID value
            public DataTable LocationCheckExisting()
            {
                return DAL.GetData("SELECT * FROM Locations WHERE name = '" + name + "'");
            } //used in SPMO Manage - Location Tab to check if already existing
            public DataTable LocationCheckIfDeleted()
            {
                return DAL.GetData("SELECT * FROM Locations WHERE name = '" + name + "' and is_deleted = 'y'");
            }  //used in SPMO Manage - Location Tab to check if the Location is deleted
            public DataTable LocationDelete()
            {
                return DAL.GetData("UPDATE Locations SET is_deleted = 'y' OUTPUT deleted.location_id, inserted.location_id WHERE name = '" + name + "'");
            } //used in SPMO Manage - Location Tab to delete each location that is selected and return the ID value
            public DataTable LocationRestore()
            {
                return DAL.GetData("UPDATE Locations SET is_deleted = 'n' OUTPUT deleted.location_id, inserted.location_id WHERE name = '" + name + "'");
            } //used in SPMO Manage - Location Tab to restore deleted location and return the ID value
            public DataTable LocationSelectWithRatingsUsingServiceProviderID()
            {
                return DAL.GetData("Select Distinct(Locations.location_id), Locations.name from Service_Providers " +
                    "inner join Service_Providers_Locations on Service_Providers.service_provider_id = Service_Providers_Locations.service_provider_id " +
                    "inner join Locations on Locations.location_id = Service_Providers_Locations.location_id " +
                    "where Service_Providers.is_deleted = '" + isDeleted + "' and Service_Providers_Locations.is_deleted = '" + isDeleted + "' " +
                    "and Locations.is_deleted = '" + isDeleted + "' and Service_Providers.service_provider_id = '" + serviceProviderID + "' " +
                    "group by Locations.location_id, Locations.name");
            } // used in USER - Chart Tab
            #endregion

            #region Questions
            public DataTable QuestionSelectUsingQuestion()
            {
                return DAL.GetData("SELECT * FROM Questions WHERE question = '" + question + "'");
            }
            public DataTable QuestionSelectActive()
            {
                return DAL.GetData("SELECT * FROM Questions WHERE is_active = 'y'");
            }
            public DataTable QuestionView()
            {
                return DAL.GetData("SELECT * FROM Questions");
            }
            public DataTable QuestionAdd()
            {
                return DAL.GetData("INSERT INTO Questions OUTPUT inserted.question_id VALUES ('" + question + "','n')"); 
            } //used in SPMO Manage - Question Tab to add new question(s) and return the ID Value.
            public void QuestionDelete()
            {
                DAL.Execute("UPDATE Questions SET is_deleted = '" + isDeleted + "' WHERE question_id = '" + questionID + "'");
            }
            public DataTable QuestionSetActive()
            {
                return DAL.GetData("UPDATE Questions SET is_active = 'y' OUTPUT deleted.question_id, inserted.question_id WHERE question_id = '" + questionID + "'");
            } //used in SPMO Manage - Question Tab to set the desired question to active and return the ID value
            public DataTable QuestionSetInactive()
            {
                return DAL.GetData("UPDATE Questions SET is_active = 'n' WHERE question_id NOT IN ('" + questionID + "')");
            } //used in SPMO Manage - Question Tab to set the desired question to inactive and return the ID value
            public DataTable QuestionCheckIfSetActive()
            {
                return DAL.GetData("Select * from Questions where is_active = 'y' and question_id = '" + questionID + "'");
            } //used in SPMO Manage - Question Tab to check if the question is already set as active.
            #endregion

            #region RateLimit
            public DataTable LimitSelectActive()
            {
                return DAL.GetData("SELECT * FROM Rate_Limit WHERE is_active = 'y'");
            }
            public DataTable LimitSelectUsingLimit()
            {
                return DAL.GetData("SELECT * FROM Rate_Limit WHERE limit = '" + limit + "'");
            }
            public DataTable LimitView()
            {
                return DAL.GetData("SELECT * FROM Rate_Limit");
            }
            public void LimitAdd()
            {
                DAL.Execute("INSERT INTO Rate_Limit VALUES ('" + limit + "','" + isActive + "')");
            }
            public void LimitSetActive()
            {
                DAL.Execute("UPDATE Rate_Limit SET is_active = '" + isActive + "' WHERE limit_id = '" + limitID + "'");
            }
            public void LimitSetInactive()
            {
                DAL.Execute("UPDATE Rate_Limit SET is_active = '" + isActive + "' WHERE limit_id NOT IN ('" + limitID + "')");
            }
            #endregion

            #region Benildeans
            public DataTable SortBenildeanType()
            {
                return DAL.GetData("Select Distinct(type) from Benildeans");
            } //used in USER Page
            public DataTable TypeView() 
            {
                return DAL.GetData("Select DISTINCT(type) from Benildeans where Benildeans.type != 'admin'");
            } //used in the drop down lists top get each type of user that is not an admin
            #endregion

            #region Charts
            public DataTable FilterPieChart()
            {
                return DAL.GetData("Select count(rating) as count, rating, Service_Providers.name, Locations.name " +
                "from Ratings, Service_Providers_Locations, Service_Providers, Locations " +
                "where Service_Providers_Locations.service_provider_location_id = Ratings.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers.service_provider_id = '" + serviceProviderID + "' " +
                "and Locations.location_id = '" + locationID + "' " +
                "group by Service_Providers.name, Locations.name, rating order by Service_Providers.name");
            }
            public DataTable LoginTrailsCountWeekly()
            {
                return DAL.GetData("SELECT COUNT(date) AS count FROM Login_Trails WHERE " +
                    "date >= DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()) / 7 * 7, 0) AND " +
                    "date < DATEADD(DAY, DATEDIFF(DAY, -1, GETDATE()), 0) AND action='" + action + "'");
            }
            public DataTable LoginTrailsCountDaily()
            {
                return DAL.GetData("SELECT COUNT(date) as count FROM Login_Trails WHERE " +
                    "date >= Dateadd(DAY, Datediff(DAY, 0, Getdate()), 0) AND " +
                    "date < Dateadd(DAY, Datediff(DAY, 0, Getdate()) + 1, 0) AND action = '" + action + "'");
            }
 
            #endregion

            #region Suggestions
            public DataTable InsertServiceProviderInSuggestion()
            {
                return DAL.GetData  ("INSERT INTO Service_Providers OUTPUT inserted.service_provider_id VALUES ('" + name + "','n','p')");
            }
            public DataTable InsertServiceProviderAndLocationInSuggestion()
            {
                return DAL.GetData("INSERT INTO Service_Providers_Locations OUTPUT inserted.service_provider_location_id VALUES ('" + locationID + "','" + serviceProviderID +"','p','n')");
            }
            public DataTable InsertSuggestion()
            {
                return DAL.GetData("INSERT INTO Suggestions OUTPUT inserted.suggestion_id VALUES ('" + username + "','" + serviceProviderLocationID + "')");
            }
            public DataTable CheckExistingServiceProviderAndLocation()
            {
                return DAL.GetData("SeLect * from Service_Providers, Service_Providers_Locations, Locations " +
                "where Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id " +
                "and Service_Providers.name = '" + name + "' and Locations.location_id = '" + locationID + "'");
            }
            public DataTable ViewPending()
            {
                return DAL.GetData("Select Service_Providers_Locations.service_provider_location_id as 'Number', Suggestions.username as 'ID Number', Service_Providers.name as 'Service Provider', Locations.name as 'Location' from Suggestions, Service_Providers, Service_Providers_Locations, Locations, Benildeans where Service_Providers_Locations.service_provider_location_id = Suggestions.service_provider_location_id and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id and Locations.location_id = Service_Providers_Locations.location_id and Benildeans.username = Suggestions.username and Service_Providers_Locations.is_approved = '" + isApproved + "'");
            }
            #endregion

            #region Answers
            public DataTable SelectActiveAnswers()
            {
                return DAL.GetData("SELECT * FROM Answers WHERE is_active='y'");
            }

            public DataTable AddAnswers()
            {
                return DAL.GetData("Insert into Answers OUTPUT inserted.answer_id values ('" + positive + "','" + negative + "','n')");
            }
            public DataTable AnswersCheckIfExisting()
            {
                return DAL.GetData("Select * from Answers where positive = '" + positive + "' and negative = '" + negative +"'");
            }
            public DataTable ViewAnswers()
            {
                return DAL.GetData("Select answer_id, (positive + ' - ' + negative) as bothAnswers from Answers");
            }
            public DataTable AnswerSetActive()
            {
                return DAL.GetData("UPDATE Answers SET is_active = 'y' OUTPUT deleted.answer_id, inserted.answer_id WHERE answer_id = '" + answerID + "'");
            } 
            public DataTable AnswerSetInactive()
            {
                return DAL.GetData("UPDATE Answers SET is_active = 'n' WHERE answer_id NOT IN ('" + answerID + "')");
            }
            #endregion

            #region Mail
            public DataTable NumberOfRatingsPerServiceProviderAndLocation()
            {
                return DAL.GetData("Select count(rating), rating, Service_Providers.name, Locations.name " +
                "from Ratings, Service_Providers_Locations, Service_Providers, Locations " +
                "where Service_Providers_Locations.service_provider_location_id = Ratings.service_provider_location_id " +
                "and Service_Providers_Locations.service_provider_id = Service_Providers.service_provider_id " +
                "and Service_Providers_Locations.location_id = Locations.location_id and Service_Providers_Locations.is_approved = 'y' and Service_Providers_Locations.is_deleted='n' " +
                "group by Service_Providers.name, Locations.name, rating");
            }
            public DataTable getEmailAddress()
            {
                return DAL.GetData("Select email from Benildeans, Admins where Benildeans.username = Admins.username and is_active = 'y'");
            }

            public void EmailTrails()
            {
                DAL.Execute("Insert into Email_Trails values ('" + emailAddress + "','" + date + "')");
            }

            public DataTable getEmailAddressDate()
            {
                return DAL.GetData("Select Top 1 * from Email_Trails where email = '" + emailAddress + "' ORDER BY date desc");
            }

            #endregion
            #endregion
        }
    }
}