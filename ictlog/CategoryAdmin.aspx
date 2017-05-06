<%@ Page Title="Category Administration" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="CategoryAdmin.aspx.vb" Inherits="CategoryAdmin" %>

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
            </td>
        <td>
            Choose parent category:
            <asp:DropDownList ID="ParentDDL" runat="server" CssClass="medium" 
                DataSourceID="ParentSDS" DataTextField="CategoryTitle" 
                DataValueField="CategoryID" AutoPostBack="True">
            </asp:DropDownList>
            <asp:SqlDataSource ID="ParentSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                
                SelectCommand="SELECT CategoryID, CategoryTitle FROM tblCategory WHERE (CategoryParentID IS NULL) ORDER BY CategoryTitle">
            </asp:SqlDataSource>
            <br /><br />&nbsp;
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" 
                DataSourceID="CategorySDS" ShowFooter="True" DataKeyNames="CategoryID" 
                CellPadding="5" Width="800px">
                <Columns>
                    <asp:TemplateField HeaderText="Category" SortExpression="CategoryTitle">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("CategoryTitle") %>' Width="98%"></asp:TextBox>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="NameTB" runat="server" Text='<%# Bind("CategoryTitle") %>' Width="98%"></asp:TextBox>
                            <cc1:TextBoxWatermarkExtender ID="NameTB_TextBoxWatermarkExtender" 
                                runat="server" Enabled="True" TargetControlID="NameTB" 
                                WatermarkText="Enter new category">
                            </cc1:TextBoxWatermarkExtender>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("CategoryTitle") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Detail">
                        <EditItemTemplate>
                            <asp:Label ID="UpdateHeader" runat="server" Text="Last update<br />" />
                            <asp:Label ID="LastUpdateLabel" runat="server" Text='<%# Bind("CategoryLastUpdate") %>' />
                            <asp:Label ID="LastUpdateByLabel" runat="server" Text='<%# Bind("CategoryLastUpdateBy") %>' CssClass="smaller" />
                            <asp:Label ID="RemovedHeader" runat="server" Text="Deleted<br />" />
                            <asp:Label ID="RemovedLabel" runat="server" Text='<%# Bind("CategoryDeleted") %>' />
                            <asp:Label ID="RemovedByLabel" runat="server" Text='<%# Bind("CategoryDeletedBy") %>' CssClass="smaller" />
                        </EditItemTemplate>
                        <FooterTemplate>
                            
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:Label ID="UpdateHeader" runat="server" Text="Last update<br />" />
                            <asp:Label ID="LastUpdateLabel" runat="server" Text='<%# Bind("CategoryLastUpdate") %>' />
                            <asp:Label ID="LastUpdateByLabel" runat="server" Text='<%# Bind("CategoryLastUpdateBy") %>' CssClass="smaller" />
                            <asp:Label ID="RemovedHeader" runat="server" Text="Deleted<br />" />
                            <asp:Label ID="RemovedLabel" runat="server" Text='<%# Bind("CategoryDeleted") %>' />
                            <asp:Label ID="RemovedByLabel" runat="server" Text='<%# Bind("CategoryDeletedBy") %>' CssClass="smaller" />
                        </ItemTemplate>
                        <ItemStyle CssClass="small darkGrey" Width="120px" />
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update"></asp:LinkButton> |
                            <asp:LinkButton ID="RemoveButton" runat="server" CausesValidation="True" 
                                CommandName="Remove" Text="Remove"></asp:LinkButton> |
                            <asp:LinkButton ID="CancelButton" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Insert"></asp:LinkButton> |
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" 
                                CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </FooterTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" 
                                CommandName="Edit" Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle CssClass="" Width="170px" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:SqlDataSource ID="CategorySDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                InsertCommand="INSERT INTO tblCategory(CategoryTitle, CategoryParentID, CategoryLastUpdate, CategoryLastUpdateBy) VALUES (@CategoryTitle, @CategoryParentID, @CategoryLastUpdate, @CategoryLastUpdateBy)" 
                SelectCommand="SELECT     - 1 AS CategoryID, '' AS CategoryTitle, '' AS CategoryLastUpdate, '' AS CategoryLastUpdateBy, NULL AS CategoryDeleted, '' AS CategoryDeletedBy   UNION
SELECT CategoryID, CategoryTitle, CategoryLastUpdate, CategoryLastUpdateBy, CategoryDeleted, CategoryDeletedBy FROM tblCategory WHERE (CategoryParentID = @ParentID) ORDER BY CategoryDeleted, CategoryTitle" 
                
                
                UpdateCommand="UPDATE tblCategory SET CategoryTitle = @CategoryTitle, CategoryLastUpdate = @CategoryLastUpdate, CategoryLastUpdateBy = @CategoryLastUpdateBy WHERE (CategoryID = @CategoryID)" CancelSelectOnNullParameter="False">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ParentDDL" Name="ParentID" 
                        PropertyName="SelectedValue" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="CategoryTitle" />
                    <asp:Parameter Name="CategoryLastUpdate" />
                    <asp:Parameter Name="CategoryLastUpdateBy" />
                    <asp:Parameter Name="CategoryID" />
                </UpdateParameters>
                <InsertParameters>
                    <asp:Parameter Name="CategoryTitle" />
                    <asp:Parameter Name="CategoryParentID" />
                    <asp:Parameter Name="CategoryLastUpdate" />
                    <asp:Parameter Name="CategoryLastUpdateBy" />
                </InsertParameters>
            </asp:SqlDataSource>
        </td>
    </tr>
</table>
</asp:Content>

