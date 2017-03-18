<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Blog.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link rel="stylesheet" type="text/css" href="css/myStyle.css" />
    <title>Blog</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1><asp:Label ID="Label1" runat="server" Text="Hello, Konfa.ch!"></asp:Label>
            <asp:LinqDataSource ID="LinqDataSource1" runat="server" ContextTypeName="Blog.Models.BlogDBClassesDataContext" EnableInsert="True" EntityTypeName="" TableName="Users" EnableDelete="True" EnableUpdate="True">
  <InsertParameters>
    <asp:Parameter Name="Name" DefaultValue="Алисик" />
    <asp:Parameter Name="Password" DefaultValue="Люблю" />
  </InsertParameters>
            </asp:LinqDataSource>
        </h1>
    </div>
        <asp:TextBox ID="Name" runat="server"></asp:TextBox>
        <p>
            <asp:TextBox ID="Password" runat="server"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>