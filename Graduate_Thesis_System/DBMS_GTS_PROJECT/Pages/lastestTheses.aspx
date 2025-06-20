<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lastestTheses.aspx.cs" Inherits="Pages_sonEklenen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
        <link rel="stylesheet" href="../css/lastestTheses.css" />
        <title>Latest Added Theses</title>
        <meta charset="UTF-8" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
                <h2>Latest Added Theses</h2>
                <p>How many days back would you like to view the theses added?</p>

                <div>
                    <asp:TextBox ID="gun" runat="server" type="number" Placeholder="Enter number of days"></asp:TextBox>

                    <asp:Button ID="Submit1" runat="server" Text="Submit" OnClick="SubmitButton_Click" />
                </div>

                <asp:GridView ID="ThesisGrid" runat="server" CssClass="thesis-grid" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="ThesisTitle" HeaderText="Thesis Title" />
                        <asp:BoundField DataField="Author" HeaderText="Author" />
                        <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:dd.MM.yyyy}" />
                    </Columns>
                </asp:GridView>

            </main>
            <footer>
                <p>&copy; 2025 GTS Thesis Management System</p>
            </footer>
        </div>
    </form>
</body>
</html>