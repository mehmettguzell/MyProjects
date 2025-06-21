<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Pages_Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Home</title>
<link rel="stylesheet" href="../css/Home.css"/>
</head>
<body>
    <header>
    <div>
        <img src="../img/logo.png" alt="Logo"/>
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
<main>
<div class="content">
    <div class="container-block">
        <h2>Upcoming Payments</h2>
        <ul>
            <li>
                <label for="payment1"><strong>Payment 1:</strong></label>
                <asp:Label ID="payment1" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label for="payment2"><strong>Payment 2:</strong></label>
                <asp:Label ID="payment2" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label for="payment3"><strong>Payment 3:</strong></label>
                <asp:Label ID="payment3" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label for="payment4"><strong>Payment 4:</strong></label>
                <asp:Label ID="payment4" runat="server" Text=""></asp:Label>
            </li>
            <li>
                <label for="payment5"><strong>Payment 5:</strong></label>
                <asp:Label ID="payment5" runat="server" Text=""></asp:Label>
            </li>
        </ul>
    </div>


<div class="container-block">
    <h2>Star Students</h2>
    <ul>
        <li>
            <label for="student1"><strong>Student 1:</strong></label>
            <asp:Label ID="student1" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="student2"><strong>Student 2:</strong></label>
            <asp:Label ID="student2" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="student3"><strong>Student 3:</strong></label>
            <asp:Label ID="student3" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="student4"><strong>Student 4:</strong></label>
            <asp:Label ID="student4" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="student5"><strong>Student 5:</strong></label>
            <asp:Label ID="student5" runat="server" Text=""></asp:Label>
        </li>
    </ul>
</div>

<div class="container-block">
    <h2>Branch Status</h2>
    <ul>
        <li>
            <label for="maltepe"><strong>Maltepe Branch:</strong></label>
            <asp:Label ID="maltepe" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="kartal"><strong>Kartal Branch:</strong></label>
            <asp:Label ID="kartal" runat="server" Text=""></asp:Label>
        </li>
        <li>
            <label for="besiktas"><strong>Beşiktaş Branch:</strong></label>
            <asp:Label ID="besiktas" runat="server" Text=""></asp:Label>
        </li>
    </ul>
</div>
</div>

</main>

</body>
</html>
