<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="BenildeServiceRating.user" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--
	Big Picture by HTML5 UP
	html5up.net | @n33co
	Free for personal and commercial use under the CCA 3.0 license (html5up.net/license)
-->

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Benilde Service Rating</title>
        <link rel="Shortcut Icon" type="image/x-icon" href="css/images/favicon.ico" />
    

    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
		<meta name="description" content="" />
		<meta name="keywords" content="" />
		<!--[if lte IE 8]><script src="css/ie/html5shiv.js"></script><![endif]-->
		<script src="js/jquery.min.js"></script>
		<script src="js/jquery.poptrox.min.js"></script>
		<script src="js/jquery.scrolly.min.js"></script>
		<script src="js/jquery.scrollgress.min.js"></script>
		<script src="js/skel.min.js"></script>
		<script src="js/init.js"></script>
        <script src="js/highcharts.js"></script>
        <script src="js/user.js"></script>
		<noscript>
			<link rel="stylesheet" href="css/skel.css" />
			<link rel="stylesheet" href="css/style.css" />
			<link rel="stylesheet" href="css/style-wide.css" />
			<link rel="stylesheet" href="css/style-normal.css" />
		</noscript>
		<!--[if lte IE 8]><link rel="stylesheet" href="css/ie/v8.css" /><![endif]-->
		<link rel="stylesheet" href="css/bootstrap.min.css"/>
		<link rel="stylesheet" href="css/bootstrap-theme.min.css"/>
        <link rel="stylesheet" href="css/user.css" />
		<link rel="stylesheet" href="js/jquery.min.css" />
        


        <script>

            function InIEvent() {

                var MaxLength = 300;

                $('#commentsSuggestions').keypress(function (e) {
                    if ($(this).val().length >= MaxLength) {
                        e.preventDefault();
                    }

                });

                $('#commentsSuggestions').keyup(function () {
                    2
                    var total = parseInt($(this).val().length);
                    3
                    $("#lblCount").html('Characters entered <b>' + total + '</b> out of 300.');
                    4
                });

            }

            $(document).ready(InIEvent)



        </script>
        </head>

    <body>
        <form id="form1" runat="server">
        <script type="text/javascript">
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(InIEvent);
        </script>
        <!-- Navigation Bar -->
            <div class="container">
                <nav class="navbar navbar-default navbar-fixed-top">
                    <div class="container">
                        <div class="navbar-header">
                          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                            <span class="sr-only">Toggle navigation</span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                            <span class="icon-bar"></span>
                          </button>
                          <a class="navbar-brand" href="#">BENILDE SERVICE RATING</a>
                        </div>
                        <div id="navbar" class="navbar-collapse collapse" aria-expanded="false" style="height: 1px;">
                          <ul class="nav navbar-nav navbar-right">
                            <li><a href="#Home">Home</a></li>
                            <li><a href="#Rate">Rate Service Provider</a></li>
                            <li><a href="#commentsUser">Comments</a></li>
                            <li><a href="#ratingsSummary">Summary of Ratings</a></li>         
                            <li>
                            <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Logout</asp:LinkButton>
                            </li>
                          </ul>
                        </div><!--/.nav-collapse -->
                    </div>
                </nav>
            </div>



            <!-- Intro -->
			<section id="Home" class="main style1 dark fullscreen">
				<div class="content container 75%">				
					<img class="style2" style="border:none;width:15em;height:15em;"  src="css/images/bsr.png">
					<br>
					<header>
						    <h1>Welcome, <asp:Label ID="lbluName" runat="server" Text=""></asp:Label><br /></h1>    
					        <h3>Let us know how you feel about our services.</h3>
					</header>		
					<footer>
						<a href="#Rate" class="button style2 down">More</a>
					</footer>
				</div>
			</section>
		
		<!-- One -->
			<section id="Rate" class="main style3 primary dark fullscreen">
				<div class="content container 75%">
                        <h1 style="color:#555">RATE SERVICE PROVIDER</h1>
                        <asp:ToolkitScriptManager ID="toolkitID" runat="server"></asp:ToolkitScriptManager>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRateError" class="lblClassDesign" runat="server" Text="">
                                </asp:Label>
                                <br />
                                <center>
                                    <text style="color:#555">Service Provider </text><asp:LinkButton ID="btnSuggestSPLink" style="color:#555" onClick="btnSuggestSPLink_Click"  runat="server" Text="(suggest a service provider?)"></asp:LinkButton>
                                    <br />
				                    <asp:DropDownList ID="ddlServiceProvider" class="form-control"  runat="server" 
                                            onselectedindexchanged="ddlServiceProviderInRatingTab_SelectedIndexChanged" Width="50%" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />

                                    <text style="color:#555">Location</text>
                                    <br />      
                                    <asp:DropDownList ID="ddlLocation" class="form-control" runat="server" 
                                        Width="50%" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />
                                    <h2><asp:Label ID="lblQuestion" style="color:#555" runat="server" Text=""></asp:Label></h2>
                                <asp:Label ID="lblDP1" runat="server" Text="" style="display:none"></asp:Label>
                                <asp:TextBox ID="txtDP2" runat="server" style="display:none"></asp:TextBox>
                                 <asp:TextBox ID="Comment" runat="server" style="display:none"></asp:TextBox>
                                </center>
				            <br/>
                <asp:Button ID="btnTesting" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingSuggest" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingSuggestOk" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingThankyou" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingConfirm" runat="server" Text="" style="display:none"></asp:Button>
                



                
                <asp:ModalPopupExtender ID="mpeRatingModal" runat="server" PopupControlID="modalShow" TargetControlID="btnTesting">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="mpeSuggestModal" runat="server" PopupControlID="suggestModalShow" TargetControlID="btnTestingSuggest">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="mpeSuggestSuccess" runat="server" PopupControlID="thankyouModalShow" TargetControlID="btnTestingSuggestOk" CancelControlID="btnSuggestOk">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="mpeRatingNavigate" runat="server" PopupControlID="thankyouModal2" TargetControlID="btnTestingThankyou" CancelControlID="btnRateAgain">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="mpeRatingSubmit" runat="server" PopupControlID="confirmationRatingModal" TargetControlID="btnTestingConfirm">        			
                </asp:ModalPopupExtender>


                
         <asp:Panel ID="suggestModalShow" runat="server" CssClass="modaPopup" align="center">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title" style="color:#555">Suggest a Service Provider</h4>
                <br />
                <asp:Label ID="lblSuggestError" class="lblClassDesign" runat="server" Text=""></asp:Label>
              </div>
              <div class="modal-body" id="Div2">
                  <asp:TextBox ID="txtSuggestServiceProvider" pattern="^[a-zA-Z0-9,.!? ]*$" MaxLength="50" style="color:#555" class="form-control" placeholder="Please type in the name of the service provider" runat="server"></asp:TextBox>
                  <br>
                  </br>
                  <asp:DropDownList ID="ddlSuggestLocation" runat="server" class="form-control">
                    </asp:DropDownList>               
              </div>
              <div class="modal-footer">
                  <asp:Button ID="btnSuggest" onClick="btnSuggest_Click" runat="server" class="btn btn-success" Text="SUGGEST" />         
                  <asp:Button ID="btnSuggestCancel" onClick="btnSuggestCancel_Click" class="btn btn-default" runat="server" Text="CANCEL" /> 
              </div>
            </div>
          </div>
          </asp:Panel>

          

           <asp:Panel ID="confirmationRatingModal" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
              </div>
              <div class="modal-body" id="Div4">
                  <h3 style="color:#555">You are <asp:Label ID="lblConfirmRating" runat="server" Font-Bold></asp:Label> with <asp:Label ID="lblConfirmServiceProvider" Font-Bold runat="server"></asp:Label>
                  <br />
                  <asp:Label ID="lblConfirmComment" runat="server" Text=""></asp:Label></h3>
                  <br />
                  <asp:PlaceHolder ID="phComment" runat="server"></asp:PlaceHolder>
              </div>
              <div class="modal-footer">
                  <asp:Button ID="btnRatingYes" onClientClick="resetImage()" onClick="btnRatingYes_Click" class="btn btn-success" runat="server" Text="DONE"/>
                  <asp:Button ID="btnRatingNo" onClick="btnRatingNo_Click" class="btn btn-default" runat="server" Text="CANCEL"/>      
              </div>
            </div>
          </div>
        </asp:Panel>
          

         <asp:Panel ID="modalShow" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title" style="color:#555">Feel free to give comments and suggestions</h4>
              </div>
              <div class="modal-body" id="modalBody">
              <asp:Label ID="lblCommentError" class="lblClassDesign" runat="server" Text=""></asp:Label>
                  <asp:TextBox ID="commentsSuggestions" runat="server" onpaste="return false" TextMode="MultiLine" placeholder="Optional" style="resize:none; color:#555" MaxLength="300"></asp:TextBox>
              </div>
              <div class="modal-footer">
			  <asp:Label ID="lblCount" runat="server" style="float:left; font-weight:bold; color:#555" Text="Only 300 characters allowed"></asp:Label>
                  <asp:Button ID="btnRatingSubmit" class="btn btn-success" runat="server" Text="SUBMIT" OnClick="btnRatingSubmit_Click" />
                  <asp:Button ID="btnRatingNoThanks" OnClick="btnRatingNoThanks_Click" class="btn btn-default" runat="server" Text="NO THANKS" />
                
              </div>
            </div>
          </div>
          </asp:Panel>

         

                <div>
				 <asp:TextBox ID="HappyOrSad" runat="server" style="display:none"></asp:TextBox>
                 </div>

                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnRatingSubmit" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRatingModal" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRatingNoThanks" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSuggest" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSuggestCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRateAgain" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRatingYes" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnRatingNo" EventName="Click" />

                </Triggers>
                </asp:UpdatePanel>

                
        

             <div class="container 75%">
						    <div class="row 0% images">
								<div class="6u">
                                    <center>
                                        <img id="happy" class="image fit from-left" src="css/images/smileys/hnc.png" onclick="changeImage(this);" title="HAPPY" alt="" />
                                    </center>
                                </div>
                             
								<div class="6u">
                                    <center>        
                                        <img id="sad" class="image fit from-right" src="css/images/smileys/snc.png" onclick="changeImage2(this);" title="SAD" alt="" />
                                    </center>
                                </div>
                            </div>

            </div>
                               
                            
                                <div>
                                <asp:Button ID="btnRatingModal" runat="server"
                                    class="btn btn-success" Text="RATE" style="color:white;" onClick="btnRatingModal_Click">                                         
                                </asp:Button>
                                <%--<asp:Button ID="btnRatingModal" runat="server" class="btn btn-success" Text="SUBMIT" style="color:white" onClick="btnRatingModal_Click" />--%>
                                </div>

                  </div>  

					<a href="#commentsUser" class="button style1 down anchored">Next</a>
			</section>


		<!-- Two -->
		
			<section id="commentsUser" class="main style3 right dark fullscreen">
			<div class="content container 75%">
            <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
            <h1 style="color:white;">COMMENTS</h1>
            
            <h2 style="color:white">Reviews and Recommendations</h2>
                    
                    <div id="dropDowns">
                                <asp:DropDownList ID="ddlSearchServiceProviders" runat="server" class="form-control" AppendDataBoundItems="true" style="display:inline; width:30%;">
                                    <asp:ListItem Text="All Service Providers" Selected="true" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSearchLocations" runat="server" class="form-control" 
                                    AppendDataBoundItems="true" style="display:inline; width:30%;">
                                    <asp:ListItem Text="All Locations" Selected="true" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSearchRespondents" runat="server" class="form-control" AppendDataBoundItems="true" style="display:inline; width:30%;">
                                    <asp:ListItem Text="All Respondent Types" Selected="true" Value="All"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:LinkButton ID="searchButton" runat="server" onclick="filterCommentsInUser_Click">
                                    <img id="searchicon" src="css/images/searchwht.png" style="width:35px; height:35px">
                                </asp:LinkButton>
                     </div>
                     <br />


						<div class="bubble">
                            <asp:Label ID="lblRecommendationIdNum1" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblRecommendationDate1" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblRecommendationSPL1" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtRecommendation1" class="commentBoxes" runat="server" TextMode="MultiLine" 
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>
						<div class="bubble4">
                            <asp:Label ID="lblReviewIdNum1" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblReviewDate1" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblReviewSPL1" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtReview1" class="commentBoxes" runat="server" TextMode="MultiLine"
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>
						<div class="bubble5">
                            <asp:Label ID="lblRecommendationIdNum2" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblRecommendationDate2" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblRecommendationSPL2" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtRecommendation2" class="commentBoxes" runat="server" TextMode="MultiLine"
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>
						<div class="bubble2">
                            <asp:Label ID="lblReviewIdNum2" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblReviewDate2" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblReviewSPL2" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtReview2" class="commentBoxes" runat="server" TextMode="MultiLine"
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>
						<div class="bubble6">
                            <asp:Label ID="lblRecommendationIdNum3" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblRecommendationDate3" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblRecommendationSPL3" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtRecommendation3" class="commentBoxes" runat="server" TextMode="MultiLine"
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>
						<div class="bubble3">
                            <asp:Label ID="lblReviewIdNum3" class="commentBoxes" runat="server" Text=""></asp:Label><br />
                            <asp:Label class="commentBoxes" ID="lblReviewDate3" runat="server" Text=""></asp:Label>
                            <asp:Label ID="lblReviewSPL3" class="commentBoxes" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtReview3" class="commentBoxes" runat="server" TextMode="MultiLine" 
                                ReadOnly Width="407px" disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox>
                        </div>

            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="searchButton" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>
			
            </div>
			<a href="#ratingsSummary" class="button style2 down anchored">Next</a>
			</section>
            


            <section id="ratingsSummary" class="main style3 dark fullscreen">
			<div class="content container 75%">
            <h1 style="color:#555">SUMMARY OF RATINGS</h1>
            <div>
                    <center>
                        <asp:Label ID="lblChartsError" class="lblClassDesign" runat="server" Text=""></asp:Label>
                        <asp:UpdatePanel ID="upnlCharts" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlChartServiceProvider" style="width:30%; display:inline" 
                                        class="form-control" runat="server" AutoPostBack="True" 
                                        onselectedindexchanged="ddlChartServiceProvider_SelectedIndexChanged">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlChartLocation" style="width:30%; display:inline" 
                                        class="form-control" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <asp:Button ID="btnFilterChart" runat="server" style="display:inline" 
                        class="btn btn-success" Text="FILTER RATINGS" onclick="btnFilterChart_Click" PostBackUrl="user.aspx#ratingsSummary"/>
                        
                    </center>
                </div>
                <br />
                <div style="border:1px solid gray">
                    <asp:Literal ID="ltrPieChart" runat="server"></asp:Literal>
                </div>
            </div>

            <div id="footerDiv">
            <text id="textFooter">For other comments, email us at <strong><i><asp:Label runat="server" ID="lblEmailFooter" Text=""></asp:Label></i></strong> &copy; TEAM JARR</text>
            </div>

			</section>
        


        <!-- Modals -->

         <asp:Panel ID="thankyouModalShow" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
              </div>
              <div class="modal-body" id="Div1">
                  <h3>Suggestion successful! Thank you for suggesting a service provider. For other comments please
                  email at <i><u><asp:Label runat="server" Text="" ID="lblSuggestionAdminEmail"></asp:Label></u></i></h3>
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnSuggestOk" class="btn btn-success" runat="server" Text="OK"/>       
              </div>
            </div>
          </div>
        </asp:Panel>
        <asp:Panel ID="thankyouModal2" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
              </div>
              <div class="modal-body" id="Div3">
                  <h3>Thank you for giving us your insights on our service provider.</h3>
                  
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnRateAgain" style="display:inline" class="btn btn-success" runat="server" Text="RATE AGAIN"/>
                  <asp:Button ID="btnViewYourComment" PostBackUrl="user.aspx#commentsUser" OnClick="btnViewYourComment_Click" style="display:inline" class="btn btn-success" runat="server" Text="VIEW COMMENTS"/>
                  <asp:Button ID="btnViewYourRating" PostBackUrl="user.aspx#ratingsSummary" OnClick="btnViewYourRating_Click" style="display:inline" class="btn btn-success" runat="server" Text="VIEW RATINGS"/> 
              </div>
            </div>
          </div>
        </asp:Panel>
		
			<script type="text/javascript" src="js/bootstrap.js"></script>
        </form>
    </body>
</html>
