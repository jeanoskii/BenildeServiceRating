<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BenildeServiceRating.login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Benilde Service Rating</title>
		<link rel="stylesheet" href="css/bootstrap.min.css"/>
        <link href="css/signin.css" rel="stylesheet"/>
        <link href="css/login.css" rel="stylesheet" /> 
        <link rel="Shortcut Icon" type="image/x-icon" href="css/images/favicon.ico" />
    </head>
    <body>
        <center>
            <img style="border:none;width:13em;height:13em; z-index:1; margin-top:5%;" src="css/images/bsr.png" alt="Benilde Service Rating"/>
            <div id="loginDiv">
                <form class="form-signin" id="formLog" method="post" role="form" runat="server">
                    <asp:ToolkitScriptManager runat="server" id="ajaxScriptManagerLogin"></asp:ToolkitScriptManager>
                    <asp:UpdatePanel runat="server" ID="UpdatePanelLogin">
                        <ContentTemplate>
                            <h3 class="form-signin-heading">Please login using your Infonet account</h3>
                            <asp:Label ForeColor="Red" Font-Bold ID="lblLoginError" runat="server" Text=""></asp:Label>
                            <asp:TextBox ID="txtUsername" required pattern="^[a-zA-Z0-9]+$" title="No special characters" name="username" class="form-control" placeholder="Username" runat="server" MaxLength="50"></asp:TextBox>
                            <asp:TextBox ID="txtPassword" required name="password" class="form-control" TextMode="Password" placeholder="Password" runat="server" MaxLength="50"></asp:TextBox>
                            <br />
                            <asp:Button ID="btnLogin" class="btn btn-lg btn-block btn-success" name="btnLogin" runat="server" Text="LOGIN" OnClick="btnLogin_Click"/>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </form>
            </div>
        </center>
    </body>
</html>