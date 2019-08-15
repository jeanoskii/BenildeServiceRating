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
    </head>

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

    <body>
        <form id="form1" runat="server">

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
                            <li><a href="#comments">Comments</a></li>
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
                        <h4 style="color:black">RATE SERVICE PROVIDER</h4>
                        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
                        <asp:ToolkitScriptManager ID="toolkitID" runat="server"></asp:ToolkitScriptManager>

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRateError" runat="server" Text="error">
                                </asp:Label>
                                <br />
                                <center>
                                    <text style="color:black">Service Provider</text>
                                    <br />
				                    <asp:DropDownList ID="ddlServiceProvider" class="form-control"  runat="server" 
                                            onselectedindexchanged="ddlServiceProvider_SelectedIndexChanged" Width="50%" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <br />

                                    <asp:LinkButton ID="btnSuggestSPLink" style="color:black" onClick="btnSuggestSPLink_Click"  runat="server" Text="Suggest Service Provider"></asp:LinkButton>

				                    <br />
                                    <br />
                                    <text style="color:black">Location</text>
                                    <br />      
                                    <asp:DropDownList ID="ddlLocation" class="form-control" runat="server" 
                                        Width="50%" AutoPostBack="true" 
                                        onselectedindexchanged="ddlLocation_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <h2><asp:Label ID="lblQuestion" style="color:black" runat="server" Text=""></asp:Label></h2>
                                <asp:Label ID="lblDP1" runat="server" Text="" style="display:none"></asp:Label>
                                <asp:TextBox ID="txtDP2" runat="server" style="display:none"></asp:TextBox>
                                </center>
				            <br/>
                <asp:Button ID="btnTesting" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingSuggest" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingSuggestOk" runat="server" Text="" style="display:none"></asp:Button>

                <asp:Button ID="btnTestingThankyou" runat="server" Text="" style="display:none"></asp:Button>

                
                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="modalShow" TargetControlID="btnTesting">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="suggestModalShow" TargetControlID="btnTestingSuggest">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="thankyouModalShow" TargetControlID="btnTestingSuggestOk" CancelControlID="btnSuggestOk">        			
                </asp:ModalPopupExtender>

                <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="thankyouModal2" TargetControlID="btnTestingThankyou">        			
                </asp:ModalPopupExtender>
                
                <asp:Panel ID="suggestModalShow" runat="server" CssClass="modaPopup" align="center">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title" style="color:black">Suggest a Service Provider</h4>
                <br />
                <asp:Label ID="lblSuggestError" runat="server" Text="" style="color:red"></asp:Label>
              </div>
              <div class="modal-body" id="Div2">
                  <asp:TextBox ID="txtSuggestServiceProvider" MaxLength="50" style="color:black" class="form-control" placeholder="Service Provider" runat="server"></asp:TextBox>
                  </br>
                    <asp:DropDownList ID="ddlSuggestLocation" runat="server" class="form-control">
                    </asp:DropDownList>
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnSuggest" onClick="btnSuggest_Click" runat="server" class="btn btn-success" Text="SUGGEST" 
                        ></asp:Button>
                  <asp:Button ID="btnCancel" onClick="btnCancel_Click" class="btn btn-success" runat="server" Text="Cancel" />
                
              </div>
            </div>
          </div>
          </asp:Panel>
          
          
          <asp:Panel ID="modalShow" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title" style="color:black">Feel free to give comments and suggestions (Optional)</h4>
              </div>
              <div class="modal-body" id="modalBody">
                  <asp:TextBox ID="commentsSuggestions" runat="server" TextMode="MultiLine" style="resize:none; color:black" MaxLength="300"></asp:TextBox>
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnSubmitComment" class="btn btn-success" runat="server" Text="Submit" 
                       onclick="btnSubmitRatingAndComment_Click" onclientclick="resetImage()" />
                  <asp:Button ID="btnNo" OnClick="btnSubmitRating_Click" onclientclick="resetImage()" class="btn btn-success" runat="server" Text="No, thanks" />
                
              </div>
            </div>
          </div>
        </asp:Panel>

                <div>
				 <asp:TextBox ID="HappyOrSad" runat="server" style = "display:none"></asp:TextBox>
                 </div>
                     
                                               

                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSubmitComment" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmitRating" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSuggest" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnThankyouOk" EventName="Click" />
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
                                <div>
                                <asp:Button ID="btnSubmitRating" runat="server"
                                    class="btn btn-success" Text="SUBMIT" style="color:white;"  onclick="btnSubmit_Click">                                         
                                </asp:Button>
                                </div>

                        

                  </div>  
                
                    
				
					<a href="#comments" class="button style1 down anchored">Next</a>
			</section>


		<!-- Two -->
		
			<section id="comments" class="main style3 right dark fullscreen">
			<div class="content container 75%">
            <asp:UpdatePanel ID="up2" runat="server">
            <ContentTemplate>
            <h3 style="color:white;">COMMENTS</h3>
            
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
                                    <img id="searchicon" src="css/images/search.png" style="width:35px; height:35px">
                                </asp:LinkButton>
                     </div>
                     <br />


						<div class="bubble"><asp:Label ID="lblRecommendationIdNum1" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblRecommendationDate1" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtRecommendation1" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>
						<div class="bubble4"><asp:Label ID="lblReviewIdNum1" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblReviewDate1" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtReview1" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>

						<div class="bubble5"><asp:Label ID="lblRecommendationIdNum2" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblRecommendationDate2" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtRecommendation2" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>
						<div class="bubble2"><asp:Label ID="lblReviewIdNum2" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblReviewDate2" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtReview2" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>
					
						<div class="bubble6"><asp:Label ID="lblRecommendationIdNum3" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblRecommendationDate3" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtRecommendation3" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>
						<div class="bubble3"><asp:Label ID="lblReviewIdNum3" class="commentBoxes" runat="server" Text=""></asp:Label><br /><asp:Label class="commentBoxes" ID="lblReviewDate3" runat="server" Text=""></asp:Label><asp:TextBox 
                                ID="txtReview3" class="commentBoxes" runat="server" TextMode="MultiLine" ReadOnly Width="407px" 
                                disabled BorderStyle="None" Font-Bold style="resize:none"></asp:TextBox></div>
			
            </div>
			<a href="#ratingsSummary" class="button style1 down anchored">Next</a>
			</section>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="searchButton" EventName="Click" />
                </Triggers>

            </asp:UpdatePanel>


            <section id="ratingsSummary" class="main style3 dark fullscreen">
			<div class="content container 75%">
            <h1 style="color:black">SUMMARY OF RATINGS</h1>
                <div>
                    <center>
                        <asp:Label ID="lblChartsError" style="color:red" runat="server" Text=""></asp:Label>
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
                        class="btn btn-success" Text="Filter Ratings" onclick="btnFilterChart_Click" PostBackUrl="user.aspx#ratingsSummary"/>
                        
                    </center>
                </div>
                <br />
                <asp:Literal ID="ltrPieChart" runat="server"></asp:Literal>
            </div>
                    
				    
                    
            
				<a href="#footer" class="button style1 down anchored">Next</a>
			</section>
        


        <!-- Modals -->

         <asp:Panel ID="thankyouModalShow" runat="server" CssClass="modalPopup" align="center" Style="display:none">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
              </div>
              <div class="modal-body" id="Div1">
                  <h3>Suggestion successful! Thank you for suggesting a service provider. For other comments please
                  email at <i><u>franz.mendoza@benilde.edu.ph</u></i></h3>
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnSuggestOk" class="btn btn-success" runat="server" Text="Ok"/>       
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
                  <h3>Thank you <asp:Label ID="Label1" runat="server" Text=""></asp:Label> for giving us your thoughts on our service provider(s).</h3>
                  
              </div>
              <div class="modal-footer">
			  
                  <asp:Button ID="btnThankyouOk" class="btn btn-success" runat="server" Text="Ok"/>       
              </div>
            </div>
          </div>
        </asp:Panel>

		<!-- Footer -->
			<footer id="footer">
			
				<!-- Menu -->
					<ul class="menu" style="color:white; margin-top:2%;">
						<li>For other comments, email us at <strong style="color:white"><i>franz.mendoza@benilde.edu.ph</i></strong></li><li>&copy; Team JARR</li><li>Design: <a href="http://html5up.net">HTML5 UP</a></li>
					</ul>
			
			</footer>
			
			<script type="text/javascript" src="js/bootstrap.js"></script>
        </form>
    </body>
</html>
