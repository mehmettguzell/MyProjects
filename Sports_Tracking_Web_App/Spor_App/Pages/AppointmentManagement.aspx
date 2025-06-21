<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AppointmentManagement.aspx.cs" Inherits="Pages_Appointment_Management" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Appointment Management</title>
    <link rel="stylesheet" href="../css/AppointmentManagement.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <div class="logo">
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


<div class="SelectBranchList">
    <asp:DropDownList ID="BranchDropDownList" runat="server" CssClass="BranchDropDownList" AutoPostBack="true" OnSelectedIndexChanged="BranchDropDownList_SelectedIndexChanged">
    </asp:DropDownList>

</div>
        <div class="mainContainer">
            <div class="mainContent">
                <div>
                    <asp:DropDownList ID="StudentDropDownList" runat="server" CssClass="StudentDropDownList">
                        </asp:DropDownList>
                </div>
            </div>
                <div class="calendar">
                    <h3>Available Time Slots</h3>
                    <div class="time-slots">
                        <div class="time-slot">
                            <asp:RadioButton ID="RadioButton1" runat="server" Text="Monday-Wednesday 8:00-10:00" GroupName="TimeSlotGroup" />
                        </div>
                        <div class="time-slot">
                            <asp:RadioButton ID="RadioButton2" runat="server" Text="Tuesday-Thursday 8:00-10:00" GroupName="TimeSlotGroup" />
                        </div>
                        <div class="time-slot">
                            <asp:RadioButton ID="RadioButton3" runat="server" Text="Monday-Wednesday 11:00-13:00" GroupName="TimeSlotGroup" />
                        </div>
                        <div class="time-slot">
                            <asp:RadioButton ID="RadioButton4" runat="server" Text="Tuesday-Thursday 11:00-13:00" GroupName="TimeSlotGroup" />
                        </div>
                    </div>
                </div>
            <div class="calendar">
                <p><strong>Set Branch Status: </strong></p>
                <asp:RadioButton ID="RadioButton5" runat="server" Text="Available" />
                <asp:RadioButton ID="RadioButton6" runat="server" Text="Busy"/>
                <asp:RadioButton ID="RadioButton7" runat="server" Text="Closed"/>
            </div>
            <asp:label ID="uykusuz" runat="server" ForeColor="Green"></asp:label>
            <asp:label ID="Teacher" runat="server" ForeColor="#6666ff"></asp:label>
            <asp:label ID="Branch" runat="server" ForeColor="#ff9900"></asp:label>
        

            <asp:Button ID="UpdateButton" runat="server" CssClass="UpdateButton" Text="Update" OnClick="UpdateButton_Click" />
        </div>
    </form>
</body>
</html>