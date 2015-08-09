<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobViewer.aspx.cs" Inherits="PSA_Prototype_1.JobViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:GridView ID="gvPerson" runat="server" AutoGenerateColumns="False"
                OnRowEditing="gvPerson_RowEditing" OnRowUpdating="gvPerson_RowUpdating">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />

                    <asp:BoundField DataField="SchoolCode" HeaderText="School Code" ReadOnly="True"
                        SortExpression="SchoolCode" />

                    <asp:TemplateField HeaderText="JobName" SortExpression="JobName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("JobName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("JobName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

            <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">Add</asp:LinkButton>

        </div>
    </form>
</body>
</html>
