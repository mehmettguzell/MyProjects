<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddThesis.aspx.cs" Inherits="Pages_AddThesis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" href="../css/AddThesis.css"/>

</head>
<body>
    <form id="formAddThesis" runat="server">
        <header>
            <h1>GTS Thesis Management System</h1>
        </header>

        <nav>
            <ul>
                <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Home.aspx">Home</asp:HyperLink></li>
                <li><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="AddThesis.aspx">Add Thesis</asp:HyperLink></li>
                <li><asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="DetailedSearch.aspx">Detailed Search</asp:HyperLink></li>
                <li><asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="LastestTheses.aspx">Latest Added Theses</asp:HyperLink></li>
            </ul>
        </nav>

        <main>
            <h2>Add a New Thesis</h2>
            <div class="form-section">
             <div class="search-form">
                <h3>Thesis Information</h3>
                <asp:TextBox ID="ThesisTitle" runat="server" placeholder="Thesis Title" class="form-input"></asp:TextBox>
                <asp:TextBox ID="ThesisAbstract" runat="server" placeholder="Thesis Abstract" class="form-input" TextMode="MultiLine"></asp:TextBox>

                <h3>Author Information</h3>
                <asp:TextBox ID="AuthorName" runat="server" placeholder="Author Name" class="form-input"></asp:TextBox>
                <asp:TextBox ID="AuthorSurname" runat="server" placeholder="Author Surname" class="form-input"></asp:TextBox>
                <asp:TextBox ID="AuthorUni" runat="server" placeholder="University" class="form-input"></asp:TextBox>
                <asp:TextBox ID="AuthorId" runat="server" placeholder="Author Id" class="form-input"></asp:TextBox>
                <asp:DropDownList ID="AuthorIns" runat="server" CssClass="thesis-grid"></asp:DropDownList>


                <h3>Thesis Details</h3>
                <asp:TextBox ID="ThesisDate" runat="server" placeholder="Thesis Date" class="form-input" TextMode="Date" ReadOnly="true"></asp:TextBox>
                <asp:TextBox ID="ThesisYear" runat="server" placeholder="Year" class="form-input" TextMode="number"></asp:TextBox>
                <asp:TextBox ID="ThesisPage" runat="server" placeholder="Page" class="form-input" TextMode="number"></asp:TextBox>
                <asp:TextBox ID="ThesisSuperVisor" runat="server" placeholder="Supervisor Name" class="form-input"></asp:TextBox>
                <asp:DropDownList ID="ThesisType" runat="server" CssClass="thesis-grid"></asp:DropDownList>
                <asp:DropDownList ID="ThesisLanguage" runat="server" CssClass="thesis-grid"></asp:DropDownList>



            </div>

            <div class="form-submit">
                <asp:Button ID="SubmitButton" runat="server" Text="Submit Thesis" CssClass="button-style" OnClick="SubmitButton_Click"/>
            </div>
        </div>
        </main>

        <footer>
            <p>&copy; 2025 GTS Thesis Management System</p>
        </footer>
    </form>
</body>
</html>
