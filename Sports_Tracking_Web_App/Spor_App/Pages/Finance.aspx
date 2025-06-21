<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Finance.aspx.cs" Inherits="Pages_Finance" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Finance</title>
<link rel="stylesheet" href="../css/Finance.css"/>
</head>
<body>
        <form id="form1" runat="server">
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
        <section class="main-content">
                <div class="dates">
                    <asp:Label ID="StartDateLabel" CssClass="date-label" runat="server" Text="Start Date:"></asp:Label>
                    <asp:TextBox ID="StartDateText" CssClass="date-input" runat="server" TextMode="Date"></asp:TextBox>

                    <asp:Label ID="EndDateLabel" CssClass="date-label" runat="server" Text="End Date:"></asp:Label>
                    <asp:TextBox ID="EndDateText" CssClass="date-input" runat="server" TextMode="Date"></asp:TextBox>

                    <asp:Button ID="FilterButton" CssClass="date-button" runat="server" Text="Filter Data" OnClick="FilterButton_Click" />
                 </div>

                <div class="graph">
                     <h2>Chart</h2>
                    <div class="income-expense-chart">
                        <div class="income" style="height: 70%;"></div>
                        <div class="expense" style="height: 50%;"></div>
                    </div>
                    <div class="labels">
                        <span class="label income-label">Revenue</span>
                        <span class="label expense-label">Expenses</span>
                    </div>
                </div>

                <div class="statistics">
                    <ul>
                        <li><strong>Total Revenue:</strong> <asp:Label ID="TotalRevenueLabel" runat="server" Text=""></asp:Label></li>
                        <li><strong>Total Expenses:</strong> <asp:Label ID="TotalExpensesLabel" runat="server" Text=""></asp:Label></li>
                        <li><strong>Total:</strong> <asp:Label ID="MoneyTotal" runat="server" Text=""></asp:Label></li>
                        <li><strong>Total Dropouts:</strong> <asp:Label ID="TotalDropoutsLabel" runat="server" Text=""></asp:Label></li>
                        <li><strong>Total Registrations:</strong> <asp:Label ID="TotalRegistrationsLabel" runat="server" Text=""></asp:Label></li>
                        <li><strong>New Registrations:</strong> <asp:Label ID="NewRegistrationsLabel" runat="server" Text=""></asp:Label></li>
                    </ul>
                </div>
                <asp:Label ID="deneme" runat="server" Text=""></asp:Label>

          </section>
        </form>
            <script type="text/javascript">
                function updateCharts(kar, zarar) {
                    var total = revenue + expense;
                    var incomePercentage = (kar / total) * 100;
                    var expensePercentage = (zarar / total) * 100;

                    document.querySelector('.income').style.height = incomePercentage + '%';
                    document.querySelector('.expense').style.height = expensePercentage + '%';
                }
            </script>


    </body>
</html>
