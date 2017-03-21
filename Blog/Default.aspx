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
        </h1>
    </div>
        <asp:TextBox ID="Name" runat="server" Width="216px"></asp:TextBox>
        <p>
            <asp:TextBox ID="Password" runat="server" Width="216px"></asp:TextBox>
        </p>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Добавить в базу" />
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:BlogDBConnectionString %>" DeleteCommand="DELETE FROM [Users] WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Password] = @original_Password" InsertCommand="INSERT INTO [Users] ([Name], [Password]) VALUES (@Name, @Password)" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT * FROM [Users]" UpdateCommand="UPDATE [Users] SET [Name] = @Name, [Password] = @Password WHERE [Id] = @original_Id AND [Name] = @original_Name AND [Password] = @original_Password">
            <DeleteParameters>
                <asp:Parameter Name="original_Id" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_Password" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:ControlParameter Name="Name" Type="String" ControlID="Name" />
                <asp:ControlParameter Name="Password" Type="String" ControlID="Password" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="Name" Type="String" />
                <asp:Parameter Name="Password" Type="String" />
                <asp:Parameter Name="original_Id" Type="Int32" />
                <asp:Parameter Name="original_Name" Type="String" />
                <asp:Parameter Name="original_Password" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>