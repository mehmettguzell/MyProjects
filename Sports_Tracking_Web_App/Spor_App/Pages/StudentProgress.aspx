<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentProgress.aspx.cs" Inherits="Pages_Student_development" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Student Progress</title>
    <link rel="stylesheet" href="../css/StudentProgress.css"/>
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

        <div class="SelectStudent">
            <label for="StudentDropDownList">Select Student:</label>
            <asp:DropDownList ID="SelectStudentDropDownList" runat="server" AutoPostBack="true" ></asp:DropDownList>
        </div>
        <div class="month-container">
            <asp:DropDownList ID="MonthDropDownList" runat="server" CssClass="month-dropdown">
                <asp:ListItem Text="January" Value="1"></asp:ListItem>
                <asp:ListItem Text="February" Value="2"></asp:ListItem>
                <asp:ListItem Text="March" Value="3"></asp:ListItem>
                <asp:ListItem Text="April" Value="4"></asp:ListItem>
                <asp:ListItem Text="May" Value="5"></asp:ListItem>
                <asp:ListItem Text="June" Value="6"></asp:ListItem>
                <asp:ListItem Text="July" Value="7"></asp:ListItem>
                <asp:ListItem Text="August" Value="8"></asp:ListItem>
                <asp:ListItem Text="September" Value="9"></asp:ListItem>
                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                <asp:ListItem Text="December" Value="12"></asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button1" runat="server" Text="Get Details" CssClass="month-button" OnClick="SubmitButton_Click" />
        </div>


        <div id="student-info" class="student-info">
            <h2>Student Monthly Progress</h2>
            <div class="student-physical-info">
                <div class="info-item">
                    <label for="height">Height (cm):</label>
                    <asp:label ID="HeightLiteral" runat="server"></asp:label>
                </div>
                <div class="info-item">
                    <label for="weight">Weight (kg):</label>
                    <asp:label ID="WeightLiteral" runat="server"></asp:label>
                </div>
                <div class="info-item">
                    <label for="muscle-mass">Muscle Mass (kg):</label>
                    <asp:label ID="MuscleMassLiteral" runat="server"></asp:label>
                </div>
                <div class="info-item">
                    <label for="jump">Vertical Jump (cm):</label>
                    <asp:label ID="JumpLiteral" runat="server"></asp:label>
                </div>
                    <asp:label ID="lblError" runat="server"></asp:label>

            </div>
        </div>

        <div id="teacher-comments" class="teacher-comments">
            <h2>Teacher Comments</h2>
            <ul>
                <li>
                    <div class="comment-text">
                        <asp:label ID="comnment" runat="server"></asp:label>
                        <div class="comment-details">
                            <asp:label ID="commentAuthor" runat="server" CssClass="comment-author"></asp:label>

                            <asp:PlaceHolder ID="StarPlaceholder" runat="server"></asp:PlaceHolder>
<%--                            <span class="comment-rating">
                                <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" width="16" height="16"><path d="M12 17.27L18.18 21l-1.64-7.03L22 9.24l-7.19-.61L12 2 9.19 8.63 2 9.24l5.46 4.73L5.82 21z"></path></svg>
                            </span>--%>
                        </div>
                    </div>
                </li>
            </ul>
        </div>

    </form>
</body>
</html>
