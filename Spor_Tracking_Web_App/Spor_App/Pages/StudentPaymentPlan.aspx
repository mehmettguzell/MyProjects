<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentPaymentPlan.aspx.cs" Inherits="Pages_StudentPaymentPlan" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Student Payment Plan</title>
    <link rel="stylesheet" href="../css/StudentPaymentPlan.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <header>
        <div>
            <img src="../img/logo.png" alt="Logo">
        </div>
            <asp:Label ID="userInfo" runat="server" ForeColor="Green" Text=""></asp:Label>

             <nav>
        <a href="#">Social Media</a>
        <a href="/Pages/Register.aspx">Student Registration</a>
    </nav>
    </header>
        <div class="pages">
            <a href="/Pages/Finance.aspx" class="page">Finance</a>
            <a href="/Pages/StudentProgress.aspx" class="page">Student Progress</a>
            <a href="/Pages/Home.aspx" class="page home">Home</a>
            <a href="/Pages/AppointmentManagement.aspx" class="page">Appointment Management</a>
            <a href="/Pages/StudentPaymentPlan.aspx" class="page">Student Payment Plan</a>
        </div>
    
    <section class="main-content">
    <div class="student-selection">
        <asp:DropDownList ID="studentSelect" runat="server" CssClass="student-select" AutoPostBack="True" OnSelectedIndexChanged="studentSelect_SelectedIndexChanged">
        </asp:DropDownList>
    </div>

<div class="student-info">
    <asp:Image ID="StudentPhoto" runat="server" CssClass="student-photo" ImageUrl="../img/messi.png" />
    <div class="info-container">
        <div class="info-row">
            <div class="info-item">
                <asp:Label ID="NameLabel" runat="server" Text="First Name:" CssClass="info-label"></asp:Label>
                <asp:Label ID="NameValue" runat="server" CssClass="info-value"></asp:Label>
            </div>
            <div class="info-item">
                <asp:Label ID="SurnameLabel" runat="server" Text="Last Name:" CssClass="info-label"></asp:Label>
                <asp:Label ID="SurnameValue" runat="server" CssClass="info-value"></asp:Label>
            </div>
        </div>
        <div class="info-row">
            <div class="info-item">
                <asp:Label ID="AthleteNumberLabel" runat="server" Text="Athlete Number:" CssClass="info-label"></asp:Label>
                <asp:Label ID="AthleteNumberValue" runat="server" CssClass="info-value"></asp:Label>
            </div>
            <div class="info-item">
                <asp:Label ID="AgeLabel" runat="server" Text="Age:" CssClass="info-label"></asp:Label>
                <asp:Label ID="AgeValue" runat="server" CssClass="info-value"></asp:Label>
            </div>
        </div>
    </div>
</div>
<%--        <div class="payment-plan">
            <asp:Image ID="PaymentPlanIcon" runat="server" CssClass="payment-photo" ImageUrl="../img/a.png" />
        </div>--%>

<div class="payment-info-container">
    <div class="payment-info-item">
        <span class="info-label">Payment Method:</span>
        <span class="info-value"><asp:Label ID="PaymentMethodValue" runat="server"></asp:Label></span>
    </div>
    <div class="payment-info-item">
        <span class="info-label">Payment Type:</span>
        <span class="info-value"><asp:Label ID="PaymentTypeValue" runat="server"></asp:Label></span>
    </div>
    <div class="payment-info-item">
        <span class="info-label">Payment Date:</span>
        <span class="info-value"><asp:Label ID="PaymentDateValue" runat="server"></asp:Label></span>
    </div>
    <div class="payment-info-item">
        <span class="info-label">Parent Info:</span>
        <span class="info-value"><asp:Label ID="ParentInfoValue" runat="server"></asp:Label></span>
    </div>
</div>

</section>

</form>
</body>
</html>
