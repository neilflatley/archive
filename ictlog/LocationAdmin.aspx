<%@ Page Title="Location Administration" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="LocationAdmin.aspx.vb" Inherits="LocationAdmin" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style1
    {
        width: 100%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C" Runat="Server">
    <table class="style1">
    <tr>
        <td width="130px">
            Choose area office:</td>
        <td>
            <asp:DropDownList ID="OfficeDDL" runat="server" CssClass="text" 
                DataSourceID="OfficeSDS" DataTextField="OfficeName" 
                DataValueField="OfficeID" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="OfficeSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT [OfficeID], [OfficeName] FROM [tblOffice] ORDER BY [OfficeName]">
            </asp:SqlDataSource>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="LocationSDS" ShowFooter="True" DataKeyNames="LocationID" 
                CellPadding="5">
                <Columns>
                    <asp:TemplateField HeaderText="Location" SortExpression="LocationName">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("LocationName") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NameTB" runat="server" Text='<%# Bind("LocationName") %>'></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="NameTB_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="NameTB" 
                                WatermarkText="Enter new location">
                            </cc1:TextBoxWatermarkExtender>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Current?" SortExpression="LocationIsCurrent">
                        <EditItemTemplate>
                            <asp:DropDownList ID="IsCurrentDDL" runat="server" 
                                SelectedValue='<%# Bind("LocationIsCurrent") %>'>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="CurrentLabel" runat="server" 
                                Text='<%# Bind("LocationIsCurrent") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" onclick="InsertButton_Click" Text="Insert"></asp:LinkButton>
                            &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="LocationSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                DeleteCommand="UPDATE tblLocation SET LocationIsCurrent = 0 WHERE (LocationID = @LocationID)" 
                InsertCommand="INSERT INTO tblLocation(LocationName, OfficeID, LocationIsCurrent) VALUES (@LocationName, @OfficeID, 1)" 
                SelectCommand="SELECT LocationID, LocationName, LocationIsCurrent FROM tblLocation WHERE (OfficeID = @OfficeID) ORDER BY LocationName" 
                UpdateCommand="UPDATE tblLocation SET LocationName = @LocationName, LocationIsCurrent = @LocationIsCurrent WHERE (LocationID = @LocationID)">
                <SelectParameters>
                    <asp:ControlParameter ControlID="OfficeDDL" Name="OfficeID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
                <DeleteParameters>
                    <asp:Parameter Name="LocationID" />
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="LocationName" />
                    <asp:Parameter Name="LocationIsCurrent" />
                    <asp:Parameter Name="LocationID" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="LocationName" />
                    <asp:Parameter Name="OfficeID" />
                </InsertParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>

