<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DetailedSearch.aspx.cs" Inherits="Pages_DetailedSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="../css/DetailedSearch.css" />
    <title>Detailed Search</title>
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
                <h2>Detailed Search</h2>
                <p>Search for theses using specific criteria below.</p>

                <section class="search-form">
                    <div class="left-column">
                        <label for="university">University:</label>
                        <asp:DropDownList ID="DropDownList1" runat="server" placeholder="Select university">
                        </asp:DropDownList>

                        <label for="institute">Institute:</label>
                        <asp:DropDownList ID="DropDownList2" runat="server" placeholder="Select institute">
                        </asp:DropDownList>

                        <label for="thesisType">Thesis Type:</label>
                        <asp:DropDownList ID="DropDownList3" runat="server">
                        </asp:DropDownList>

                        <label for="language">Language:</label>
                        <asp:DropDownList ID="DropDownList4" runat="server" placeholder="Select language">
                        </asp:DropDownList>

                        <label for="Topic">Topic:</label>
                        <asp:DropDownList ID="DropDownList6" runat="server" placeholder="Select Topic">
                        </asp:DropDownList>

                        <label for="abstract">Abstract:</label>
                        <asp:TextBox ID="TextBox1" runat="server" placeholder="Enter abstract" ></asp:TextBox>
                    </div>

                    <div class="right-column">

                        <div class="dates">
                            <asp:Label ID="StartDateLabel" CssClass="date-label" runat="server" Text="Start Date:"></asp:Label>
                            <asp:TextBox ID="StartDateText" CssClass="date-input" runat="server" TextMode="Date"></asp:TextBox>

                            <asp:Label ID="EndDateLabel" CssClass="date-label" runat="server" Text="End Date:"></asp:Label>
                            <asp:TextBox ID="EndDateText" CssClass="date-input" runat="server" TextMode="Date"></asp:TextBox>
                        </div>

                        <label for="thesisNo">Thesis Number: (ex:T1) </label>
                        <asp:TextBox ID="TextBox2" runat="server" placeholder="Enter thesis number" ></asp:TextBox>

                        <label for="thesisTitle">Thesis Title:</label>
                        <asp:DropDownList ID="LAN" runat="server" placeholder="Enter thesis title">
                        </asp:DropDownList>

                        <label for="thesisKey">Thesis Keys:</label>
                        <asp:TextBox ID="TextBox3" runat="server" placeholder="Enter Keys" ></asp:TextBox>

                        <label for="author">Author:</label>
                        <asp:DropDownList ID="DropDownList7" runat="server" placeholder="Select Author">
                        </asp:DropDownList>

                        <label for="advisor">Advisor:</label>
                        <asp:DropDownList ID="DropDownList5" runat="server" placeholder="Select Advisor">
                        </asp:DropDownList>
                    </div>

                    <div class="buttons">
                         <asp:Button ID="SubmitButton" CssClass="date-button" runat="server" Text="Filter Data"  OnClick="SubmitButton_Click"/>
                         <asp:Button ID="ClearButton" CssClass="clear-button" runat="server" Text="Clear"  OnClick="ClearButton_Click"/>

                    </div>
                </section>

                <asp:GridView ID="ThesisGrid" runat="server" CssClass="thesis-grid" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Title" HeaderText="Thesis Title" SortExpression="ThesisTitle" />
                        <asp:BoundField DataField="Author" HeaderText="Author" SortExpression="Author" />
                        <asp:BoundField DataField="LANGUAGE" HeaderText="Language" SortExpression="LANGUAGE" />
                        <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:dd.MM.yyyy}" SortExpression="DateAdded" />
                        <asp:BoundField DataField="TYPE" HeaderText="Type" SortExpression="TYPE" />
                        <asp:BoundField DataField="PAGE" HeaderText="Page" SortExpression="PAGE" />
                        <asp:BoundField DataField="UNIVERSITY" HeaderText="University" SortExpression="UNIVERSITY" />
                        <asp:BoundField DataField="INSTITUE" HeaderText="Institute" SortExpression="INSTITUE" />
                        <asp:BoundField DataField="TOPIC" HeaderText="Topic" SortExpression="TOPIC" />
                        <asp:BoundField DataField="SUPERVISOR" HeaderText="Supervisor" SortExpression="SUPERVISOR" />
                        <asp:BoundField DataField="CO_SUPERVISOR" HeaderText="CO_SUPERVISOR" SortExpression="CO_SUPERVISOR" />

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