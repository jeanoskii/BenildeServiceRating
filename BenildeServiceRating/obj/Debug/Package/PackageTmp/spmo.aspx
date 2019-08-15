    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="spmo.aspx.cs" Inherits="BenildeServiceRating.spmo" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Benilde Service Rating</title>
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
        <script src="js/spmo.js"></script>
        <script src="js/highcharts.js"></script>  
        <script src="js/exporting.js"></script>
        <script src="js/ddScript.js"></script>
        <noscript>
	        <link rel="stylesheet" href="css/skel.css" />
	        <link rel="stylesheet" href="css/style.css" />
	        <link rel="stylesheet" href="css/style-wide.css" />
	        <link rel="stylesheet" href="css/style-normal.css" />
	        <link rel="stylesheet" href="css/bootstrap.min.css"/>
        </noscript>
        <!--[if lte IE 8]><link rel="stylesheet" href="css/ie/v8.css" /><![endif]-->
        <link rel="stylesheet" href="css/bootstrap.min.css"/>
        <link rel="stylesheet" href="css/bootstrap-theme.min.css" />
        <link rel="stylesheet" href="css/spmo.css" />
        <link rel="stylesheet" href="css/ddStyles.css" />
        <link rel="Shortcut Icon" type="image/x-icon" href="css/images/favicon.ico" />

    </head>
    <body>
    <form id="SPMOForm" runat="server" >
        <!-- Header -->
    
                    <asp:ToolkitScriptManager ID="toolkitID" runat="server"></asp:ToolkitScriptManager>
			    <header id="header">

				    <!-- Logo -->
					    <h1 id="logo"></h1>

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
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Manage<span class="caret"></span></a>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#commentsSPMO">Comments</a></li>
                    <li><a href="#manageServiceProvider">Service Provider</a></li>
                    <li><a href="#manageLocation">Location</a></li>
                    <li><a href="#manageServiceProviderLocation">Service Provider - Location</a></li>

                    <li><a href="#pendingServiceProvider">Pending Service Provider</a></li>
                
                  </ul>
                </li>
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Change<span class="caret"></span></a>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#changeRateLimit">Rate Limit</a></li>
                    <li><a href="#changeQuestion">Question</a></li>
                    <li><a href="#changeAdmin">Admin</a></li>
                  </ul>
                </li>
                <li class="dropdown">
                  <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-expanded="false">Charts<span class="caret"></span></a>
                  <ul class="dropdown-menu" role="menu">
                    <li><a href="#ratingsSummary">Summary of Ratings</a></li>
                    <li><a href="#loginTrails">Login Trails</a></li>
                  </ul>
                </li>
                <li>
                <asp:LinkButton ID="btnLogout" runat="server" OnClick="btnLogout_Click">Logout</asp:LinkButton>
                </li>
            
              </ul>
            </div><!--/.nav-collapse -->
          </div>
        </nav>

      
                        </div>


			    </header>
			
		    <!-- Intro -->
			    <section id="Home" class="main style1 dark fullscreen">
				    <div class="content container 75%">
                    <img class="style2" style="border:none;width:15em;height:15em;"  src="css/images/bsr.png">
                    <br />
					    <header>
						        <h1>Welcome, <asp:Label ID="lbluName" runat="server" Text=""></asp:Label><br /></h1>
                                <h3>It's nice to have you back.</h3>
					    </header>
					    <footer>
						    <a href="#commentsSPMO" class="button style2 down">                    
                            </a>
					    </footer>
				    </div>





                
			    </section>
		
		    <!-- One -->
			    <section id="commentsSPMO" class="main style3 primary dark fullscreen">
				    <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlSearch" runat="server">
                            <ContentTemplate>
                                <h1 style="color:Black;">MANAGE COMMENTS</h1>
                                <asp:Label class="lblClassDesign" ID="lblCommentMessage" runat="server" Text=""></asp:Label>
                                <br />
                                <div id="dropDowns" style="margin-top:2%;">
                                    <asp:DropDownList ID="ddlServiceProvidersInCommentTab" style="width:30%; display:inline" runat="server" class="form-control">
                                        <%--<asp:ListItem Text="All Service Providers" Selected="true" Value="All"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlLocationsInCommentTab" style="width:30%; display:inline" runat="server" class="form-control">
                                        <%--<asp:ListItem Text="All Locations" Selected="true" Value="All"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlRespondentsInCommentTab" style="width:30%; display:inline" runat="server" class="form-control">
                                        <%--<asp:ListItem Text="All Respondent Types" Selected="true" Value="All"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="searchButton" runat="server" onclick="btnSearchComment_Click">
                                        <img id="searchicon" src="css/images/search.png">
                                    </asp:LinkButton>
                                 </div>   
                                    <div id="gridview2" style="margin-top:5%; overflow-y:auto; height:300px;">
                                        <asp:GridView ID="gvComments" class="table table-responsive table-hover table-bordered" style="color:Black; background-color:White; border:1px solid black;" runat="server">
                                            <Columns>
                                                <asp:templatefield HeaderText="Select">
                                                    <itemtemplate>
                                                        <asp:CheckBox ID="cbSelectComment" runat="server" />
                                                    </itemtemplate>
                                                </asp:templatefield>                                    
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                     <asp:Button ID="btnPopUpModalComment" runat="server" class="btn btn-success" style="width:30%"
                                                 Text="DELETE SELECTED" onClick="btnPopUpModalComment_Click" />

                                
                            
                        
                    <asp:Button ID="btnTestingCModal" runat="server" Text="" style="display:none"></asp:Button>
                    <asp:Button ID="btnTestingSuccessDelete" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeCommentModal" runat="server" PopupControlID="modalCommentShow" TargetControlID="btnTestingCModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeSuccessfulDeleteComments" runat="server" PopupControlID="successfulDeleteCommentModalShow" TargetControlID="btnTestingSuccessDelete" CancelControlID="btnOkSuccessfulCommentDeleteModal">        			
                    </asp:ModalPopupExtender>



				            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteComment" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelDeleteComment" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkSuccessfulCommentDeleteModal" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>	
                    <a href="#manageServiceProvider" class="button style1 down anchored">Next</a>
			    </section>
		
		        <section id="manageServiceProvider" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
			                    <center>
				                    <h1 style="color:white; font-family:sans-serif; margin-bottom:2%;">MANAGE SERVICE PROVIDERS</h1>
                                    <asp:Label ID="lblServiceProviderMessage" class="lblClassDesign" runat="server" Text=""></asp:Label>
				                    <div id="div3" style="margin-top:2%">
                                
                                    <asp:TextBox ID="txtServiceProvider" class="form-control" placeholder="Service Provider" runat="server" style="width:50%; display:inline; background-color:White; color:Black;"></asp:TextBox>

                                     <asp:Button ID="btnAddServiceProvider" OnClick="btnAddServiceProvider_Click" style="width:30%" class="btn btn-success" runat="server" Text="ADD SERVICE PROVIDER"></asp:Button>

                                        <%--<asp:Button style="width:30%;" ID="btnAddServiceP" OnClick="btnAddServiceP_Click" class="btn btn-success" 
                                            runat="server" Text="ADD SERVICE PROVIDER"></asp:Button>--%>
				                    </div>
                                    <br />
                                    <div id="div4" style="overflow-y:auto; height:300px">
                                        <asp:GridView ID="gvServiceProviders" class="table table-bordered table-hover table-responsive" runat="server" style="color:Black; text-align:center; background-color:White; border:1px solid black;">
                                            <Columns>
                                                <asp:templatefield HeaderText="Select">
                                                    <itemtemplate>
                                                       <asp:CheckBox ID="cbSelectServiceProviders" runat="server"/>
                                                    </itemtemplate>
                                                </asp:templatefield>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
				                    <div id="div5" style="margin-top:3%;">

                                        <asp:Button ID="btnPopUpModalServiceProvider" OnClick="btnPopUpModalServiceProvider_Click" style="width:30%" class="btn btn-success" runat="server" Text="DELETE SELECTED"></asp:Button>


				                    </div>
			                    </center>

                     <asp:Button ID="btnTestingAddSPOnly" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingDeleteSPOnly" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingDeleteOkSPOnly" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeServiceProviderSuccessAdd" runat="server" PopupControlID="addServiceProviderlOnlyModalShow" TargetControlID="btnTestingAddSPOnly" CancelControlID="btnOkAddSPOnlyModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeServiceProviderModal" runat="server" PopupControlID="deleteSPOnlyModalShow" TargetControlID="btnTestingDeleteSPOnly">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeServiceProviderSuccessDelete" runat="server" PopupControlID="successfulDeleteSPOnlyModalShow" TargetControlID="btnTestingDeleteOkSPOnly" CancelControlID="btnDeleteOkSPOnlyModal">        			
                    </asp:ModalPopupExtender>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnOkAddSPOnlyModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelDeleteServiceProvider" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteServiceProvider" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteOkSPOnlyModal" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
			        </div>  
                    <a href="#manageLocation" class="button style1 down anchored">Next</a>
			    </section>

                <section id="manageLocation" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlLocation" runat="server">
                            <ContentTemplate>
			                    <center>
				                    <h1 style="color:black; font-family:sans-serif; margin-bottom:2%;">MANAGE LOCATIONS</h1>
				                    <div id="div1Location">
                                        <b><asp:Label ID="lblLocationMessage" class="lblClassDesign" runat="server" Text=""></asp:Label></b><br />
                                        <asp:TextBox style="display:inline; width:50%; background-color:White; color:Black;" class="form-control" ID="txtLocation" placeholder="Location Name" runat="server"></asp:TextBox>
                                        <asp:Button style="width:30%;" ID="btnAddLocation" class="btn btn-success" 
                                            runat="server" Text="ADD LOCATION" onclick="btnAddLocation_Click"></asp:Button>
					                    <br/>
                                        <br />
				                    </div>
                                    <div id="div2Location" style="overflow-y:auto; height:300px">
                                        <asp:GridView ID="gvLocations" class="table table-bordered table-hover table-responsive" runat="server" style="color:Black; text-align:center; background-color:White; border:1px solid black;">
                                            <Columns>
                                                <asp:templatefield HeaderText="Select">
                                                    <itemtemplate>
                                                       <asp:CheckBox ID="cbSelectLocations" runat="server"/>
                                                    </itemtemplate>
                                                </asp:templatefield>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
				                    <div id="div3Location" style="margin-top:3%;">

                                        <asp:Button ID="btnPopUpModalLocation" style="width:30%" class="btn btn-success" onClick="btnPopUpModalLocation_Click" runat="server" Text="DELETE SELECTED"></asp:Button>


				                    </div>
			                    </center>

                     <asp:Button ID="btnTestingManageLocation" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingAddLocation" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingSuccessfulDeleteLocation" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeLocationModal" runat="server" PopupControlID="deleteLocationModalShow" TargetControlID="btnTestingManageLocation" CancelControlID="btnCancelDeleteLocation">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeLocationSuccessAdd" runat="server" PopupControlID="addLocationModalShow" TargetControlID="btnTestingAddLocation" CancelControlID="btnAddLocationModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeLocationSuccessDelete" runat="server" PopupControlID="successfulDeleteLocationModalShow" TargetControlID="btnTestingSuccessfulDeleteLocation" CancelControlID="btnSuccessfulDeleteLocationModal">        			
                    </asp:ModalPopupExtender>
                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteLocation" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelDeleteLocation" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddLocation" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnSuccessfulDeleteLocationModal" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
			        </div>  
                    <a href="#manageServiceProviderLocation" class="button style1 down anchored">Next</a>
			    </section>


		    
			    <section id="manageServiceProviderLocation" class="main style3 right dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlServiceProviders" runat="server">
                            <ContentTemplate>   
                                <h1 style="color:white">MANAGE SERVICE PROVIDERS - LOCATIONS</h1>
                                <asp:Label ID="lblServiceProviderLocationMessage" class="lblClassDesign" runat="server" Text=""></asp:Label>
                                <br />
				                <div id="topForm">
                                    <center>
                                    <asp:DropDownList ID="ddlServiceProvidersInServiceProviderLocationTab" runat="server" class="form-control" style="width:30%; display:inline"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlLocationsInServiceProviderLocationTab" runat="server" class="form-control" style="width:30%; display:inline"></asp:DropDownList>
                                
                                    <asp:Button ID="btnServiceProviderLocationAdd" OnClick="btnServiceProviderLocationAdd_Click" runat="server" class="btn btn-success" 
                                             style="width:30%; display:inline" Text="ADD SERVICE PROVIDER" />
                                             </center>
				                </div>
				                <br>
                                <br>
                                <div id="gridView" style="overflow-y:auto; height:300px;">
                                    <asp:GridView ID="gvServiceProvidersLocations" class="table table-bordered table-hover" runat="server" style="color:Black; background-color:White; border:1px solid black;">
                                        <Columns>
                                            <asp:templatefield HeaderText="Select">
                                                <itemtemplate>
                                                   <asp:CheckBox ID="cbSelectServiceProvidersLocations" runat="server"/>
                                                </itemtemplate>
                                            </asp:templatefield>
                                        </Columns>
                                    </asp:GridView>
                                
                                </div>
                                <br />
                                <asp:Button ID="btnPopUpModalServiceProviderLocation" style="width:30%" runat="server" 
                                            class="btn btn-success"  
                                            Text="Delete Selected" OnClick="btnPopUpModalServiceProviderLocation_Click" />

                                

                     <asp:Button ID="btnTestingDeleteSPModal" runat="server" Text="" style="display:none"></asp:Button>

                     <asp:Button ID="btnTestingAddSPModal" runat="server" Text="" style="display:none"></asp:Button>

                     <asp:Button ID="btnTestingSuccessfulDeleteSP" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeServiceProviderLocation" runat="server" PopupControlID="deleteServiceProviderLocationModal" TargetControlID="btnTestingDeleteSPModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeServiceProviderLocationSuccessAdd" runat="server" PopupControlID="addSPandLModalShow" TargetControlID="btnTestingAddSPModal" CancelControlID="btnOkAddSP">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeServiceProviderLocationDeleteSuccess" runat="server" PopupControlID="deleteSPSuccessfulModalShow" TargetControlID="btnTestingSuccessfulDeleteSP" CancelControlID="btnDeleteSPSuccessfulModal">        			
                    </asp:ModalPopupExtender>
            


                            </ContentTemplate>
				            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteServiceProviderLocation" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelDeleteServiceProviderLocation" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkAddSP" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnDeleteSPSuccessfulModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnPopUpModalServiceProviderLocation" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                     </div> 
                     <a href="#pendingServiceProvider" class="button style1 down anchored">Next</a> 
                </section>


                <section id="pendingServiceProvider" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
					                <h1 style="color:black">PENDING SERVICE PROVIDER</h1>
					                <p>
                                        <asp:Label ID="lblPending" class="lblClassDesign" runat="server"></asp:Label>
                                    </p>
					                <div id="Div7" style="overflow-y:auto; height:300px">
						                <asp:GridView ID="gvSuggestions" class="table table-bordered table-hover" runat="server" style="color:black; background-color:White">
                                            <Columns>
                                                <asp:templatefield HeaderText="Select">
                                                    <itemtemplate>
                                                       <asp:CheckBox ID="cbSelectSuggestions" runat="server"/>
                                                    </itemtemplate>
                                                </asp:templatefield>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <asp:Button ID="btnApprovedSelected" OnClick="btnApprovedSelected_Click" style="width:160px" runat="server" class="btn btn-success"  
                                            Text="Approve"></asp:Button>
					
                                    <asp:Button ID="btnDisapprovedSelected" OnClick="btnDisapprovedSelected_Click" style="width:160px" runat="server" class="btn btn-success"  
                                            Text="Disapprove"></asp:Button>

                     <asp:Button ID="btnTestingApproveSP" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingDisapproveSP" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeApproveSP" runat="server" PopupControlID="approveSPModalShow" TargetControlID="btnTestingApproveSP" CancelControlID="btnOkApproveSPModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeDisapproveSP" runat="server" PopupControlID="disapproveSPModalShow" TargetControlID="btnTestingDisapproveSP" CancelControlID="btnOkDisapproveSPModal">        			
                    </asp:ModalPopupExtender>
        


                                </ContentTemplate>
                                <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnOkApproveSPModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnOkDisapproveSPModal" EventName="Click" />
                            </Triggers>

                    </asp:UpdatePanel>
			        </div>  
                    <a href="#changeRateLimit" class="button style1 down anchored">Next</a>
			    </section>





                <section id="changeRateLimit" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlRateLimit" runat="server">
                            <ContentTemplate>
                            <center> 
				                        <h1 style="color:white; font-family:sans-serif; margin-bottom:2%;">MANAGE RATE LIMIT</h1> 
                                        <b><asp:Label ID="lblRateLimitError" class="lblClassDesign" runat="server" Text=""></asp:Label></b>
                                        <br />
			                    
                                <div style="width:100%;">
                                    <div style="width:60%">
                                        <asp:TextBox style="background-color:White; color:Black;" class="form-control" 
                                            placeholder="Hour" ID="txtAddRateLimit" runat="server" 
                                            type="number" min="0" onpaste="return false" Width="100%" Font-Size="42pt" Height="50%"></asp:TextBox>
                                        <br />
                                        <asp:Button ID="btnAddRateLimit" class="btn btn-success" 
                                            runat="server" Text="ADD RATE LIMIT" style="width:160px" onclick="btnAddRateLimit_Click"></asp:Button>
				                    </div>
                                    <br />
				                    <div id="div2RateLimit" style="width:60%">
                                        <asp:DropDownList ID="ddlSelectRateLimit" 
                                            runat="server" class="form-control" Width="100%" Font-Size="42pt" Height="50%"></asp:DropDownList>
                                         <br />
                                        <asp:Button class="btn btn-success" ID="btnSelectRateLimit" 
                                            runat="server" Text="SELECT RATE LIMIT" style="width:160px" onclick="btnSelectRateLimit_Click"></asp:Button>
				                    </div>
                                </div>
			                 </center>

                    <asp:Button ID="btnTestingSetRL" runat="server" Text="" style="display:none"></asp:Button>
                    <asp:Button ID="btnTestingAddRL" runat="server" Text="" style="display:none"></asp:Button>


                
                    <asp:ModalPopupExtender ID="mpeSetRLModal" runat="server" PopupControlID="manageRLModalShow" TargetControlID="btnTestingSetRL" CancelControlID="btnCancelRateLimitModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeAddRLModal" runat="server" PopupControlID="addRLModalShow" TargetControlID="btnTestingAddRL" CancelControlID="btnAddRLModal">        			
                    </asp:ModalPopupExtender>


                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSetRateLimitModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelRateLimitModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddRLModal" EventName="Click" />
                            </Triggers>

                        </asp:UpdatePanel>
			        </div>
			        <a href="#changeQuestion" class="button style2 down anchored">Next</a>
			    </section>

                <section id="changeQuestion" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlQuestion" runat="server">
                            <ContentTemplate>
                                <h1 style="color:black;">CHANGE QUESTION</h1>
                                <asp:Label ID="lblQuestionMessage" class="lblClassDesign" runat="server" Text=""></asp:Label>
                                <br />
                                <asp:TextBox ID="txtQuestionAdd" runat="server" placeholder="Add question" style="background-color:white; color:Black;" class="form-control"></asp:TextBox>
                                <asp:Button ID="btnQuestionAdd" runat="server" Text="Add Question" 
                                    onclick="btnQuestionAdd_Click" class="btn btn-success"></asp:Button>
                                <br/>
                                <br/>
                                
                                <asp:DropDownList ID="ddlQuestion" runat="server" class="form-control" ></asp:DropDownList>
                                <asp:Button ID="btnQuestionSet" runat="server" class="btn btn-success" 
                                    Text="Set Question" onclick="btnQuestionSet_Click"></asp:Button>
                                    <br />
                                    <br />
			                	<%--<asp:Button ID="btnQuestionDelete" runat="server" Text="Delete Question" class="btn btn-success"
                                    onClick="btnQuestionDelete_Click"></asp:Button>
                                --%>

                     <asp:Button ID="btnTestingSQ" runat="server" Text="" style="display:none"></asp:Button>
                     <asp:Button ID="btnTestingAddQuestion" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeSetQuestion" runat="server" PopupControlID="setQuestionModalShow" TargetControlID="btnTestingSQ" CancelControlID="btnCancelQuestionModal">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeAddQuestion" runat="server" PopupControlID="addQuestionModalShow" TargetControlID="btnTestingAddQuestion" CancelControlID="btnAddQuestionModal">        			
                    </asp:ModalPopupExtender>

                            </ContentTemplate>

                             <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSetQuestionModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnCancelQuestionModal" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddQuestionModal" EventName="Click" />
                            </Triggers>

                         </asp:UpdatePanel>
			        </div>
                    <a href="#changeAdmin" class="button style1 down anchored">Next</a>
			    </section>



                <section id="changeAdmin" class="main style3 dark fullscreen">
			        <div class="content container 75%">
                        <asp:UpdatePanel ID="upnlAdmin" runat="server">
                            <ContentTemplate>
			                    <center>
				                    <h1 style="color:white; font-family:sans-serif; margin-bottom:5%;">CHANGE ADMIN</h1>
				                    <div id="div1">
                                         <asp:Label ID="lblAdminMessage" class="lblClassDesign" runat="server" Text=""></asp:Label>
                                        <br />
                                        <p style="display:inline;">Current Admin:</p>
                                        <br />
                                        <h2><asp:Label style="color:white; font-weight:bold; width:50%;" ID="lblCurrentAdmin" runat="server" Text=""></asp:Label></h2>
					                    <br/>
                                        <br />
				                    </div>
				                    <div id="div2" style="margin-top:3%;">
                                        <p>New Admin: </p>
                                        <asp:TextBox style="display:inline; width:50%; background-color:white; color:Black;" class="form-control" placeholder="New Admin" ID="txtNewAdmin" runat="server"></asp:TextBox>
                                    
                                    </div>
                                    <br />
                                    <div id="div3CA">

                                        <asp:Button ID="btnSaveAdmin" class="btn btn-success" OnClick="btnSaveAdmin_Click" runat="server" Text="SAVE"></asp:Button>

                                    </div>
			                    </center>

                    <asp:Button ID="btnTestingSaveAdmin" runat="server" Text="" style="display:none"></asp:Button>

                    <asp:Button ID="btnTestingLogoutAdmin" runat="server" Text="" style="display:none"></asp:Button>

                
                    <asp:ModalPopupExtender ID="mpeAdminModal" runat="server" PopupControlID="saveAdminModalShow" TargetControlID="btnTestingSaveAdmin">        			
                    </asp:ModalPopupExtender>

                    <asp:ModalPopupExtender ID="mpeAdminLogoutModal" runat="server" PopupControlID="changeAdminModalShow" TargetControlID="btnTestingLogoutAdmin">        			
                    </asp:ModalPopupExtender>


                            </ContentTemplate>
				            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnYesAdmin" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnNoAdmin" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdminModalLogout" EventName="Click" />

                            </Triggers>
                        </asp:UpdatePanel>
			        </div>
			        <a href="#ratingsSummary" class="button style1 down anchored">Next</a>
			    </section>

      

                <section id="ratingsSummary" class="main style3 dark fullscreen">
			    <div class="content container 75%">
                <h1 style="color:black;">SUMMARY OF RATINGS</h1>
                </br>
				
				    <asp:Literal ID="ltrBarChart" runat="server"></asp:Literal>
				    <br />
				
			    <a href="#loginTrails" class="button style1 down anchored">Next</a>	
			    </div>
			    </section>
			
			    <section id="loginTrails" class="main style3 dark fullscreen">
			    <div class="content container 75%">
                <h1 style="color:white">LOGIN TRAILS</h1>
			    <br />
                    <asp:Literal ID="ltrLineChart" runat="server"></asp:Literal>
                <br />   
                 </div>

                 <div id="footerDiv">
                    <text id="textFooter"><strong>&copy; TEAM JARR</strong></text>
                 </div>

                 </section>
			
				    <asp:Panel ID="deleteServiceProviderLocationModal" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to remove this/these service provider(s) and location(s)?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnDeleteServiceProviderLocation" onclick="btnDeleteServiceProviderLocation_Click" class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelDeleteServiceProviderLocation" onclick="btnCancelDeleteServiceProviderLocation_Click" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>
                

                    <asp:Panel ID="disapproveSPModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully disapproved a service provider.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnOkDisapproveSPModal" class="btn btn-success" runat="server" Text="Ok" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="deleteSPOnlyModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to remove this/these service provider(s)?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnDeleteServiceProvider" onclick="btnDeleteServiceProvider_Click" class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelDeleteServiceProvider" onclick="btnCancelDeleteServiceProvider_Click" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>


                    <asp:Panel ID="saveAdminModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to transfer your rights to  this user?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnYesAdmin" class="btn btn-success" runat="server" Text="Yes" 
                                   onclick="btnYesAdmin_Click"/>
                              <asp:Button ID="btnNoAdmin" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="addSPandLModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully added a service provider and location.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnOkAddSP" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                      </asp:Panel>

                      <asp:Panel ID="successfulDeleteCommentModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully deleted a comment.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnOkSuccessfulCommentDeleteModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>

                    </asp:Panel>

                    <asp:Panel ID="successfulDeleteSPOnlyModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully deleted a service provider.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnDeleteOkSPOnlyModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>

                    </asp:Panel>


                    <asp:Panel ID="changeAdminModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully changed the admin. You will be automatically logged out on the system.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnAdminModalLogout" OnClick="btnAdminModalLogout_Click" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>

                    </asp:Panel>


                    <asp:Panel ID="successfulDeleteLocationModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully deleted a location.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnSuccessfulDeleteLocationModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>

                    </asp:Panel>

                    <asp:Panel ID="approveSPModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully approved a service provider.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnOkApproveSPModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="addServiceProviderlOnlyModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully added a service provider.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnOkAddSPOnlyModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="deleteSPSuccessfulModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully deleted a service provider-location</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnDeleteSPSuccessfulModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="addLocationModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully added a location.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnAddLocationModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="addRLModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully added a rate limit.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnAddRLModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="addQuestionModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">You have successfully added a question.</h4>
                          </div>
                          <div class="modal-footer">
			  
                              <asp:Button ID="btnAddQuestionModal" class="btn btn-success" runat="server" Text="OK" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>


                    <asp:Panel ID="modalCommentShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to delete this/these comment(s)?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnDeleteComment" onClick="btnDeleteComment_Click" class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelDeleteComment" onClick="btnCancelDeleteComment_Click" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>




                    <asp:Panel ID="manageRLModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to set this rate limit?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnSetRateLimitModal" onclick="btnSetRateLimitModal_Click" class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelRateLimitModal" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>


                    <asp:Panel ID="setQuestionModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to set this question?</h4>
                          </div>
                          <div class="modal-footer">
			  
                               <asp:Button ID="btnSetQuestionModal" onclick="btnSetQuestionModal_Click" class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelQuestionModal" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>

                    <asp:Panel ID="deleteLocationModalShow" runat="server" CssClass="modalPopup" align="center" style="display:none">
                      <div class="modal-dialog">
                        <div class="modal-content">
                          <div class="modal-header">
                            <h4 class="modal-title" style="color:black">Are you sure you want to remove this/these location(s)?</h4>
                          </div>
                          <div class="modal-footer">
                              <asp:Button ID="btnDeleteLocation" onclick="btnDeleteLocation_Click" 
                                  class="btn btn-success" runat="server" Text="Yes"/>
                              <asp:Button ID="btnCancelDeleteLocation" OnClick="btnCancelDeleteLocation_Click" class="btn btn-success" runat="server" Text="No" />
                 
                          </div>
                        </div>
                      </div>
                    </asp:Panel>
			
			    <script type="text/javascript" src="js/bootstrap.js"></script>
    </form>
	    </body>
    </html>

