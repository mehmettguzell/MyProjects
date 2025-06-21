<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Pages_Register" %>

<!DOCTYPE html>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Register</title>
    <link rel="stylesheet" href="../css/Register.css">
</head>
<body>
    <div class="container">
    <div id="RegisterForm">
        <h1>Register Form</h1>
    <asp:Label ID="userInfo" runat="server" ForeColor="Green" Text=""></asp:Label>
        <form id="formRegister" runat="server">
            <div class="top-section">
                <div class="form-group student-info">
                    <h2>Student Information</h2>
                    <asp:TextBox ID="StudentName" runat="server" placeholder="Name" class="form-input"></asp:TextBox>
                    <asp:TextBox ID="StudentSurname" runat="server" placeholder="Surname" class="form-input"></asp:TextBox>
                    <asp:TextBox ID="StudentAge" runat="server" placeholder="Age" class="form-input" TextMode="Number"></asp:TextBox>
                    <asp:TextBox ID="StudentHeight" runat="server" placeholder="Height" class="form-input" TextMode="Number"></asp:TextBox>
                    <asp:TextBox ID="StudentWeight" runat="server" placeholder="Weight" class="form-input" TextMode="Number"></asp:TextBox>
                    <asp:TextBox ID="StudentMuscleWeight" runat="server" placeholder="Muscle Weight" class="form-input" TextMode="Number"></asp:TextBox>
                    <asp:TextBox ID="VerticalJump" runat="server" placeholder="Vertical Jump" class="form-input" TextMode="Number"></asp:TextBox>
                    <asp:DropDownList ID="Location" runat="server">

                    </asp:DropDownList>
                </div>

                <div class="form-group parent-info">
                    <h2>Parent Information</h2>
                    <asp:TextBox ID="ParentName" runat="server" placeholder="Parent Name" class="form-input"></asp:TextBox>
                    <asp:TextBox ID="ParentSurname" runat="server" placeholder="Parent Surname" class="form-input"></asp:TextBox>
                    <asp:TextBox ID="ParentPhone" runat="server" placeholder="Parent Phone" class="form-input" TextMode="Phone"></asp:TextBox>
                    <asp:TextBox ID="ParentEmail" runat="server" placeholder="Parent Email" class="form-input" TextMode="Email"></asp:TextBox>
                </div>
            </div>

            <div class="bottom-section">
                <div class="form-group payment-info">
                    <h2>Membership and Payment Information</h2>
                    <asp:TextBox ID="Amount" runat="server" placeholder="Amount" Cssclass="form-input"></asp:TextBox>
                    <asp:TextBox ID="StartDate" runat="server" placeholder="Start Date" class="form-input" TextMode="Date"></asp:TextBox>
                    <asp:DropDownList ID="PaymentOptions" runat="server" class="form-input">
                        <asp:ListItem Text="Payment Options" Value=""></asp:ListItem>
                        <asp:ListItem Text="cash" Value="cash"></asp:ListItem>
                        <asp:ListItem Text="installment" Value="installment"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="PaymentMethod" runat="server" class="form-input">
                        <asp:ListItem Text="Payment Method" Value=""></asp:ListItem>
                        <asp:ListItem Text="card" Value=""></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="form-group card-details">
                    <h2>Card Details</h2>
                    <asp:TextBox ID="txtCardHolderName" runat="server" placeholder="Card Holder's Name" Cssclass="form-input"></asp:TextBox>
                    <asp:TextBox ID="txtCardNumber" runat="server" placeholder="Card Number" Cssclass="form-input" MaxLength="16"></asp:TextBox>
                    <asp:TextBox ID="txtExpirationDate" runat="server" placeholder="Expiration Date (MM/YY)" Cssclass="form-input" MaxLength="5"></asp:TextBox>
                    <asp:TextBox ID="txtCVVCode" runat="server" placeholder="CVV Code" Cssclass="form-input" MaxLength="3"></asp:TextBox>
                </div>
            </div>
            <div class="submit-container">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit" CssClass="button-style" onclick="SubmitClick"/>
            </div>

        </form>
    </div>
        </div>
</body>
</html>
