<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Pages_Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="UTF-8" />
    <title>Anasayfa</title>
    <link rel="stylesheet" href="../css/styles.css" />
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
                <h2>Hoş Geldiniz!</h2>
                <p>GTS Tez Yönetim Sistemi'ne hoş geldiniz. Bu platform, tezlerinizi yönetmek ve aramak için kolay bir çözüm sunar.</p>

                <div class="search-form">
                    <asp:Label ID="Label1" runat="server" Text="Tarama terimi giriniz:" AssociatedControlID="SearchTerm" />
                    <asp:TextBox ID="SearchTerm" runat="server" CssClass="search-input" Placeholder="Arama terimi..." />

                    <asp:Label ID="Label2" runat="server" Text="Aranacak Alan:"  />
                    <asp:DropDownList ID="SearchField" runat="server">
                        <asp:ListItem Text="Tez Adı" Value="Tez Adı" />
                        <asp:ListItem Text="Yazar" Value="Yazar" />
                        <asp:ListItem Text="Konu" Value="Konu" />
                        <asp:ListItem Text="SuperVisor" Value="SUPERVISOR" />
                        <asp:ListItem Text="Abstract" Value="ABSTRACT" />
                        <asp:ListItem Text="University" Value="UNIVERSITY" />
                        <asp:ListItem Text="Institute" Value="INSTITUTE" />
                    </asp:DropDownList>


                    <asp:Label ID="Label3" runat="server" Text="Language:" />
                    <asp:DropDownList ID="Language" runat="server">
                    </asp:DropDownList>

                    <asp:Label ID="Label4" runat="server" Text="Tez Türü:" />
                    <asp:DropDownList ID="ThesisType" runat="server">
                    </asp:DropDownList>

                    <div class="buttons">
                        <asp:Button type="reset" ID="Button2" runat="server" Text="sil" />
                        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                    </div>
                    <asp:GridView ID="ThesisGrid" runat="server" CssClass="thesis-grid" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="ThesisTitle" HeaderText="Thesis Title" />
                            <asp:BoundField DataField="Author" HeaderText="Author" />
                            <asp:BoundField DataField="DateAdded" HeaderText="Date Added" DataFormatString="{0:dd.MM.yyyy}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </main>
            <footer>
                <p>&copy; 2025 GTS Tez Yönetim Sistemi</p>
            </footer>
            <asp:Label ID="kontrol" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
