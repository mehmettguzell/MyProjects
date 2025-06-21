<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <link rel="stylesheet" href="../css/Login.css"/>
</head>

<body>
        <div id="LoginForm">
            <h1>Login!</h1>
            <p class="form-description">Please Login to the system</p>
            <form id="form3" runat="server">
                <div class="form-group">
                    <label for="IDLabel"><strong>Personel ID : </strong></label>
                    <asp:TextBox ID="IDBox" runat="server" placeholder="Enter your ID:" class="form-input"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="PassLabel"><strong>Password : </strong></label>
                    <asp:TextBox ID="PassBox" runat="server" TextMode="Password" placeholder="Enter your password" class="form-input"></asp:TextBox>
                </div>

                <div class="form-group">
                    <label for="RemindCheckBox"><strong>Remember Me</strong></label>
                    <asp:CheckBox ID="RemindCheckBox" runat="server" />
                </div>

                <div class="login-button">
                    <asp:Button ID="SubmitButton" runat="server" Text="Login" CssClass="button-style" OnClick="SubmitClick"/>
                </div>

                <div class="helper-links">
                    <asp:LinkButton ID="ForgetButton" runat="server">Forgot Password?</asp:LinkButton>
                </div>

                <div class="message">
                    <asp:Label ID="LabelMessage" runat="server" Text=""></asp:Label>
                </div>
                <div>
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="lblTrueMessage" runat="server" ForeColor="Green" Text=""></asp:Label>
                    <asp:Label ID="denemeLabel" runat="server" ForeColor="Green" Text=""></asp:Label>


                </div>
                
            </form>
        </div>
</body>
</html>
