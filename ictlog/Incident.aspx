
<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Incident.aspx.vb" Inherits="Incident" title="QuickTicket" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    .commentOuter { padding-top: 5px; padding-bottom: 5px; }
    .commentInner { max-height:200px; overflow-x:none; overflow-y:auto; }
    .style2 { width: 100%; }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C" Runat="Server">
    <div style="width:100%; margin-left:auto; margin-right:auto; text-align:left;">
        <asp:SqlDataSource ID="CopySqlDataSource" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
            SelectCommand="SELECT [IncidentTitle], [CategoryID], [IncidentDescription], [IncidentSolution], [PriorityID], [IncidentTimeSpent] FROM [tblIncident] WHERE ([IncidentID] = @IncidentID)">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="IncidentID" 
                    QueryStringField="SourceID" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <table cellpadding="2" cellspacing="0" width="100%">
            <tr>
                <td style="width:22px; background: url('Images/ticket_perf_l.gif') repeat-y;">
                    &nbsp;</td>
                <td style="padding: 10px 20px 10px 0px; background-color: #f8f8f8; ">
                <asp:FormView ID="FV1" runat="server" DataKeyNames="IncidentID" 
                    DataSourceID="SqlDataSource1" BackColor="#F8F8F8" style="padding:3px 10px; border-collapse:collapse" 
                        Width="100%" BorderStyle="None">
                    <EditItemTemplate>
                        <table cellpadding="2" cellspacing="0" class="style2">
                            <tr>
                                <td class="white text black_bg">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="big">
                                                Incident Update</td>
                                            <td align="right" style="padding-right: 10px">
                                                Incident ID:
                                                <asp:Label ID="IncidentIDLabel" runat="server" 
                                                    Text='<%# Eval("IncidentID") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="height:10px;">
                                <td>
                                    <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td style="width: 75px" class="small grey">
                                                            Incident</td>
                                                        <td colspan="2">
                                                            <asp:TextBox ID="IncidentTitleTextBox" runat="server" CssClass="text" 
                                                                TabIndex="1" Text='<%# Bind("IncidentTitle") %>' Width="95%" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                                ControlToValidate="IncidentTitleTextBox" ErrorMessage="*" 
                                                                ValidationGroup="EditValidation">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 75px" class="small grey">
                                                            User</td>
                                                        <td>
                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <td width="120" style="padding-right:5px;">
                                                                        <ajaxToolkit:ComboBox ID="CB1" runat="server" AppendDataBoundItems="True" 
                                                                            AutoCompleteMode="SuggestAppend" AutoPostBack="True" CssClass="small text" 
                                                                            DataTextField="Name" DataValueField="User" DropDownStyle="DropDownList" 
                                                                            TabIndex="2" RenderMode="Block">
                                                                            <asp:ListItem></asp:ListItem>
                                                                        </ajaxToolkit:ComboBox>
                                                                    </td>
                                                                    <td>
<%--                                                                        <asp:DropDownList ID="UserDropDownList" runat="server" 
                                                                            AppendDataBoundItems="True" AutoPostBack="True" CssClass="text" 
                                                                            DataTextField="Name" DataValueField="User" 
                                                                            onselectedindexchanged="UserDropDownList_SelectedIndexChanged" 
                                                                            Visible="False">
                                                                        </asp:DropDownList>
--%>                                                                        <asp:TextBox ID="UserTextBox" runat="server" Text='<%# Eval("IncidentUser") %>' 
                                                                            Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:DropDownList ID="OfficeDropDownList" runat="server" CssClass="small darkGrey text" 
                                                                            TabIndex="3">
                                                                        </asp:DropDownList>
                                                                        <ajaxToolkit:CascadingDropDown ID="CCD1" runat="server" Category="Office" 
                                                                            PromptText="Select an office" ServiceMethod="GetOffices" 
                                                                            ServicePath="WebService.asmx" TargetControlID="OfficeDropDownList">
                                                                        </ajaxToolkit:CascadingDropDown>
                                                                        <asp:DropDownList ID="LocationDropDownList" runat="server" 
                                                                            CssClass="small darkGrey text" TabIndex="4">
                                                                        </asp:DropDownList>
                                                                        <ajaxToolkit:CascadingDropDown ID="CCD2" runat="server" Category="Location" 
                                                                            ParentControlID="OfficeDropDownList" PromptText="Select a location" 
                                                                            ServiceMethod="GetLocations" ServicePath="WebService.asmx" 
                                                                            TargetControlID="LocationDropDownList">
                                                                        </ajaxToolkit:CascadingDropDown>
                                                                        <asp:TextBox ID="OfficeTextBox" runat="server" Text='<%# Bind("OfficeID") %>' 
                                                                            Visible="False" Width="10px"></asp:TextBox>
                                                                        <asp:TextBox ID="LocationTextBox" runat="server" 
                                                                            Text='<%# Bind("LocationID") %>' Visible="False" Width="10px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="small grey">
                                                            Category</td>
                                                        <td colspan="2">
                                                            <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="text" 
                                                                DataSourceID="CategorySqlDataSource" DataTextField="CategoryTitle" 
                                                                DataValueField="CategoryID" SelectedValue='<%# Bind("CategoryID") %>' 
                                                                TabIndex="5" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <% 
                                        '<tr style="height:10px;">
                                            '<td>
                                                'Known Error:</td>
                                            '<td colspan="2">
                                                '<asp:DropDownList ID="KnownIssueDropDownList" runat="server" 
                                                    'AppendDataBoundItems="True" CssClass="text" 
                                                    'DataSourceID="KnownIssueSqlDataSource" DataTextField="KnownIssueTitle" 
                                                    'DataValueField="KnownIssueID" SelectedValue='<%# Bind("KnownIssueID") %><%' 
                                                    'TabIndex="5">
                                                    '<asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                '</asp:DropDownList>
                                            '</td>
                                        '</tr>
                                        %>
                                                    <tr>
                                                        <td class="small">
                                                            &nbsp;</td>
                                                        <td colspan="2" align="right" style="padding-right:5px;">
                                                            <u><asp:Label ID="AddDetail" runat="server" CssClass="small" style="color:Navy;" onmouseover="this.style.cursor='pointer';" /></u>
                                                            <ajaxToolkit:CollapsiblePanelExtender ID="CPE1" runat="server" TargetControlID="DescriptionPanel" CollapseControlID="AddDetail" ExpandControlID="AddDetail" Collapsed="True" TextLabelID="AddDetail" ExpandedText="Remove Extra Detail" CollapsedText="Add Extra Detail" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="25%" style="padding-left:8px;">
                                                <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td class="smaller grey" style="width:70px;">
                                                            Assigned to</td>
                                                        <td>
                                                            <asp:DropDownList ID="IncidentAssignedToDropDownList" runat="server" 
                                                                CssClass="small text" SelectedValue='<%# Bind("IncidentAssignedTo") %>' 
                                                                TabIndex="8">
                                                                <asp:ListItem>daleb</asp:ListItem>
                                                                <asp:ListItem>gaynorh</asp:ListItem>
                                                                <asp:ListItem>jeffe</asp:ListItem>
                                                                <asp:ListItem>kerrymc</asp:ListItem>
                                                                <asp:ListItem>krisstopherb</asp:ListItem>
                                                                <asp:ListItem>neilf</asp:ListItem>
                                                                <asp:ListItem>rhysw</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smaller grey">
                                                            Priority</td>
                                                        <td>
                                                            <asp:DropDownList ID="PriorityDropDownList" runat="server" 
                                                                CssClass="small text" DataSourceID="PrioritySqlDataSource" 
                                                                DataTextField="PriorityTitle" DataValueField="PriorityID" 
                                                                SelectedValue='<%# Bind("PriorityID") %>' TabIndex="9">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smaller grey">
                                                            Time Spent</td>
                                                        <td class="small">
                                                            <asp:TextBox ID="IncidentTimeSpentTextBox" runat="server" CssClass="text" 
                                                                MaxLength="5" TabIndex="10" Text='<%# Bind("IncidentTimeSpent") %>' 
                                                                Width="30px" />
                                                            (minutes)<asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                                                                runat="server" ControlToValidate="IncidentTimeSpentTextBox" ErrorMessage="*" 
                                                                ValidationGroup="EditValidation">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="small grey">
                                                            Status</td>
                                                        <td>
                                                            <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="text" 
                                                                DataSourceID="StatusSqlDataSource" DataTextField="StatusTitle" 
                                                                DataValueField="StatusID" SelectedValue='<%# Bind("StatusID") %>' 
                                                                TabIndex="11">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="DescriptionPanel" runat="server" style="overflow:hidden; height:0px;">
                                        <table id="IncidentDescription" runat="server" class="style2" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td class="orange_bg">
                                                    <b>Additional Details</b></td>
                                                <td width="25%">
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="IncidentDescriptionTextBox" runat="server" CssClass="text" 
                                                        Height="50px" TabIndex="6" Text='<%# Bind("IncidentDescription") %>' 
                                                        TextMode="MultiLine" Width="99%" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td class="orange_bg">
                                                <b>Action / Resolution</b></td>
                                            <td width="25%" align="right" valign="bottom" rowspan="2">
                                                <table border="0" cellpadding="10" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="UpdateImage" runat="server" CommandName="Update" 
                                                                ImageUrl="~/Images/process_accept.png" ValidationGroup="EditValidation" />
                                                            <br />
                                                            <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" 
                                                                CommandName="Update" Text="Update" TabIndex="12" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="UpdateCancelImage" runat="server" CausesValidation="False" 
                                                                CommandName="Cancel" ImageUrl="~/Images/process_remove.png" OnClick="UpdateCancelImage_Click" />
                                                            <br />
                                                            <asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" 
                                                                CommandName="Cancel" Text="Cancel" TabIndex="13" OnClick="UpdateCancelButton_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="IncidentSolutionTextBox" runat="server" CssClass="text" 
                                                    Height="74px" TabIndex="7" Text='<%# Bind("IncidentSolution") %>' 
                                                    TextMode="MultiLine" Width="99%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("IncidentLastUpdatedBy") %>' />
                        <asp:TextBox ID="IncidentClosedTextBox" runat="server" 
                            Text='<%# Bind("IncidentClosed") %>' Visible="False"></asp:TextBox>
                        <asp:TextBox ID="IncidentClosedByTextBox" runat="server" 
                            Text='<%# Bind("IncidentClosedBy") %>' Visible="False"></asp:TextBox>
                        <asp:TextBox ID="StatusIDTextBox" runat="server" Text='<%# Eval("StatusID") %>' 
                            Visible="False"></asp:TextBox>
                        <asp:SqlDataSource ID="CategorySqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [CategoryID], [CategoryTitle] FROM [tblCategory] WHERE [CategoryParentID] IS NULL ORDER BY [CategoryTitle]">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="KnownIssueSqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [KnownIssueID], [KnownIssueTitle] FROM [tblKnownIssue] WHERE ([StatusID] = @StatusID) ORDER BY [KnownIssueTitle]">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="1" Name="StatusID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="PrioritySqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT * FROM [tblPriority]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="StatusSqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT * FROM [tblStatus]"></asp:SqlDataSource>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table cellpadding="2" cellspacing="0" class="style2">
                            <tr>
                                <td class="big white text black_bg">
                                    Log an Incident</td>
                            </tr>
                            <tr style="height:10px;">
                                <td>
                                    <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td>
                                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                    <tr>
                                                        <td class="small grey" style="width:75px;">
                                                            Incident</td>
                                                        <td>
                                                            <asp:TextBox ID="IncidentTitleTextBox" runat="server" CssClass="text" 
                                                                TabIndex="1" Text='<%# Bind("IncidentTitle") %>' Width="95%" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                                ControlToValidate="IncidentTitleTextBox" ErrorMessage="*" 
                                                                ValidationGroup="InsertValidation">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:UpdatePanel ID="UP3" runat="server">
                                                    <ContentTemplate>
                                                        <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td class="small grey" style="width: 75px;">
                                                                    User</td>
                                                                <td>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td width="120">
                                                                                <ajaxToolkit:ComboBox ID="CB1" runat="server" AppendDataBoundItems="True" 
                                                                                    AutoCompleteMode="SuggestAppend" AutoPostBack="True" CausesValidation="True" 
                                                                                    CssClass="small text" DataTextField="Name" DataValueField="User" 
                                                                                    DropDownStyle="DropDownList" RenderMode="Block" TabIndex="2">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </ajaxToolkit:ComboBox>
                                                                            </td>
                                                                            <td>
<%--                                                                                <asp:DropDownList ID="UserDropDownList" runat="server" 
                                                                                    AppendDataBoundItems="True" AutoPostBack="True" CssClass="text" 
                                                                                    DataTextField="Name" DataValueField="User" 
                                                                                    onselectedindexchanged="UserDropDownList_SelectedIndexChanged" 
                                                                                    Visible="False">
                                                                                    <asp:ListItem></asp:ListItem>
                                                                                </asp:DropDownList>
--%>                                                                                <asp:TextBox ID="UserTextBox" runat="server" Text='<%# Eval("IncidentUser") %>' 
                                                                                    Visible="False" Width="10px"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                                                    ControlToValidate="CB1" ErrorMessage="*" 
                                                                                    ValidationGroup="InsertValidation">*</asp:RequiredFieldValidator>
                                                                                <asp:DropDownList ID="OfficeDropDownList" runat="server" 
                                                                                    CssClass="small darkGrey text" TabIndex="3">
                                                                                </asp:DropDownList>
                                                                                <ajaxToolkit:CascadingDropDown ID="CCD1" runat="server" Category="Office" 
                                                                                    PromptText="Select an area" ServiceMethod="GetOffices" 
                                                                                    ServicePath="WebService.asmx" TargetControlID="OfficeDropDownList">
                                                                                </ajaxToolkit:CascadingDropDown>
                                                                                <asp:DropDownList ID="LocationDropDownList" runat="server" 
                                                                                    AppendDataBoundItems="True" CssClass="small darkGrey text" 
                                                                                    OnDataBound="LocationDropDownList_Databound" TabIndex="4">
                                                                                </asp:DropDownList>
                                                                                <ajaxToolkit:CascadingDropDown ID="CCD2" runat="server" Category="Location" 
                                                                                    ParentControlID="OfficeDropDownList" PromptText="Select a location" 
                                                                                    ServiceMethod="GetLocations" ServicePath="WebService.asmx" 
                                                                                    TargetControlID="LocationDropDownList">
                                                                                </ajaxToolkit:CascadingDropDown>
                                                                                <asp:TextBox ID="OfficeTextBox" runat="server" Text='<%# Bind("OfficeID") %>' 
                                                                                    Visible="False" Width="10px"></asp:TextBox>
                                                                                <asp:TextBox ID="LocationTextBox" runat="server" 
                                                                                    Text='<%# Bind("LocationID") %>' Visible="False" Width="10px"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <table border="0" cellpadding="2" cellspacing="1" width="100%">
                                                    <tr style="height:10px;">
                                                        <td class="small grey" style="width:75px;">
                                                            Category</td>
                                                        <td>
                                                            <asp:DropDownList ID="CategoryDropDownList" runat="server" CssClass="text" 
                                                                DataSourceID="CategorySqlDataSource" DataTextField="CategoryTitle" 
                                                                DataValueField="CategoryID" SelectedValue='<%# Bind("CategoryID") %>' 
                                                                TabIndex="5" Width="150px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <%
                                        '<tr style="height:10px;">
                                            '<td>
                                                'Known Error:</td>
                                            '<td colspan="2">
                                                '<asp:DropDownList ID="KnownIssueDropDownList" runat="server" 
                                                    'AppendDataBoundItems="True" CssClass="text" 
                                                    'DataSourceID="KnownIssueSqlDataSource" DataTextField="KnownIssueTitle" 
                                                    'DataValueField="KnownIssueID" SelectedValue='<%# Bind("KnownIssueID") %><%' 
                                                    'TabIndex="5">
                                                    '<asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                                '</asp:DropDownList>
                                            '</td>
                                        '</tr>
                                        %>
                                                    <tr style="height:10px;">
                                                        <td>
                                                            &nbsp;</td>
                                                        <td align="right" colspan="2">
                                                            <u>
                                                            <asp:Label ID="AddDetail" runat="server" CssClass="small" 
                                                                onmouseover="this.style.cursor='pointer';" style="color:Navy;" />
                                                            </u>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="25%" style="padding-left:8px;">
                                                <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                                    <tr>
                                                        <td class="smaller grey" width="75">
                                                            Assigned to</td>
                                                        <td>
                                                            <asp:DropDownList ID="IncidentAssignedToDropDownList" runat="server" 
                                                                CssClass="small text" SelectedValue='<%# Bind("IncidentAssignedTo") %>' 
                                                                TabIndex="8">
                                                                <asp:ListItem>daleb</asp:ListItem>
                                                                <asp:ListItem>gaynorh</asp:ListItem>
                                                                <asp:ListItem>jeffe</asp:ListItem>
                                                                <asp:ListItem>kerrymc</asp:ListItem>
                                                                <asp:ListItem>krisstopherb</asp:ListItem>
                                                                <asp:ListItem>neilf</asp:ListItem>
                                                                <asp:ListItem>rhysw</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smaller grey">
                                                            Priority</td>
                                                        <td>
                                                            <asp:DropDownList ID="PriorityDropDownList" runat="server" 
                                                                CssClass="small text" DataSourceID="PrioritySqlDataSource" 
                                                                DataTextField="PriorityTitle" DataValueField="PriorityID" 
                                                                SelectedValue='<%# Bind("PriorityID") %>' TabIndex="9">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="smaller grey">
                                                            Time Spent</td>
                                                        <td class="small">
                                                            <asp:TextBox ID="IncidentTimeSpentTextBox" runat="server" CssClass="text" 
                                                                MaxLength="5" TabIndex="10" Text='<%# Bind("IncidentTimeSpent") %>' 
                                                                Width="30px" />
                                                            &nbsp;(minutes)&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" 
                                                                runat="server" ControlToValidate="IncidentTimeSpentTextBox" ErrorMessage="*" 
                                                                ValidationGroup="InsertValidation">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="small grey">
                                                            Status</td>
                                                        <td>
                                                            <asp:DropDownList ID="StatusDropDownList" runat="server" CssClass="text" 
                                                                DataSourceID="StatusSqlDataSource" DataTextField="StatusTitle" 
                                                                DataValueField="StatusID" SelectedValue='<%# Bind("StatusID") %>' 
                                                                TabIndex="11">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="DescriptionPanel" runat="server" style="overflow:hidden; height:0px;">
                                        <table ID="IncidentDescription" runat="server" border="0" cellpadding="2" cellspacing="1" class="style2">
                                            <tr>
                                                <td class="orange_bg">
                                                    <b>Additional Details</b></td>
                                                <td width="25%" style="padding-left:8px;">
                                                    <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                                        <tr>
                                                            <td class="smaller grey" width="75">
                                                                Entered</td>
                                                            <td>
                                                            <asp:TextBox ID="IncidentEntered" runat="server" CssClass="small text" 
                                                                MaxLength="10" TabIndex="12" 
                                                                Width="70px" ToolTip="Date entered: Leave blank if recording current tickets or enter a date to record past tickets" />
                                                            <ajaxToolkit:MaskedEditExtender ID="EnteredDateMEE" runat="server" TargetControlID="IncidentEntered" UserDateFormat="DayMonthYear" AutoComplete="False" MaskType="Date" Mask="99/99/9999" />
                                                            <asp:TextBox ID="IncidentEntered2" runat="server" CssClass="small text" 
                                                                MaxLength="5" TabIndex="13" 
                                                                Width="40px" ToolTip="Time entered: Leave blank if recording current tickets or enter an approximate time when recording past tickets" />
                                                            <ajaxToolkit:MaskedEditExtender ID="EnteredTimeMEE" runat="server" TargetControlID="IncidentEntered2" UserTimeFormat="TwentyFourHour" AutoComplete="False" MaskType="Time" Mask="99:99" />
                                                            </td>
                                                        </tr>
                                                    </table>
   
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="IncidentDescriptionTextBox" runat="server" CssClass="text" 
                                                        Height="50px" TabIndex="6" Text='<%# Bind("IncidentDescription") %>' 
                                                        TextMode="MultiLine" Width="99%" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <ajaxToolkit:CollapsiblePanelExtender ID="CPE1" runat="server" 
                                        CollapseControlID="AddDetail" Collapsed="True" CollapsedText="Add Extra Detail" 
                                        ExpandControlID="AddDetail" ExpandedText="Remove Extra Detail" 
                                        TargetControlID="DescriptionPanel" TextLabelID="AddDetail" />
                                    <table class="style2" border="0" cellpadding="2" cellspacing="1">
                                        <tr>
                                            <td class="orange_bg">
                                                <b>Action / Resolution</b></td>
                                            <td align="right" rowspan="2" valign="bottom" width="25%">
                                                <table border="0" cellpadding="10" cellspacing="0">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:ImageButton ID="InsertImage" runat="server" CommandName="Insert" 
                                                                ImageUrl="~/Images/process_add.png" ValidationGroup="InsertValidation" /><br />
                                                                <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" 
                                                                CommandName="Insert" TabIndex="14" Text="Save" 
                                                                ValidationGroup="InsertValidation" />
                                                        </td>
                                                        <td align="center">
                                                            <asp:ImageButton ID="InsertCancelImage" runat="server" CausesValidation="False" 
                                                                CommandName="Cancel" ImageUrl="~/Images/remove.png" 
                                                                onclick="InsertCancelImage_Click" /><br />
                                                                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" 
                                                                CommandName="Cancel" onclick="InsertCancelButton_Click" TabIndex="15" 
                                                                Text="Cancel" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="IncidentSolutionTextBox" runat="server" CssClass="text" 
                                                    Height="74px" TabIndex="7" Text='<%# Bind("IncidentSolution") %>' 
                                                    TextMode="MultiLine" Width="99%" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="UserHiddenField" runat="server" Value='<%# Bind("IncidentEnteredBy") %>' />
                        <asp:SqlDataSource ID="CategorySqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [CategoryID], [CategoryTitle] FROM [tblCategory] WHERE [CategoryParentID] IS NULL ORDER BY [CategoryTitle]">
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="KnownIssueSqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT [KnownIssueID], [KnownIssueTitle] FROM [tblKnownIssue] WHERE ([StatusID] = @StatusID) ORDER BY [KnownIssueTitle]">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="1" Name="StatusID" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="PrioritySqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT * FROM [tblPriority]"></asp:SqlDataSource>
                        <asp:SqlDataSource ID="StatusSqlDataSource" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                            SelectCommand="SELECT * FROM [tblStatus]"></asp:SqlDataSource>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table cellpadding="2" cellspacing="1" border="0" class="style2">
                            <tr>
                                <td class="white text black_bg">
                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                        <tr>
                                            <td class="big">
                                                Incident Details</td>
                                            <td align="right" style="padding-right: 10px" class="small">
                                                Incident ID:
                                                <asp:Label ID="IncidentIDLabel" runat="server" CssClass="big heading" Text='<%# Eval("IncidentID") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="small">
                                                <asp:Label ID="PriorityLabel" runat="server" 
                                                    Text='<%# Eval("PriorityTitle") %>' />
                                                priority&nbsp;<asp:Label ID="CategoryLabel" runat="server" 
                                                    Text='<%# Eval("CategoryTitle") %>' />
                                                related incident assigned to
                                                <asp:Label ID="IncidentAssignedToLabel" runat="server" 
                                                    Text='<%# Eval("IncidentAssignedTo") %>' />
                                            </td>
                                            <td align="right" style="padding-right: 10px" class="small">
                                                Status:
                                                <asp:Label ID="Status" runat="server" CssClass="big heading" Text='<%# Eval("IStatus") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="2" cellspacing="1" border="0" class="style2">
                            <tr>
                                <td rowspan="2" class="formPaddingLeft" valign="top">
                                    <table cellpadding="2" cellspacing="1" border="0" class="style2">
                                        <tr>
                                            <td style="width: 60px;" class="small grey">
                                                Incident
                                            </td>
                                            <td class="text">
                                                <asp:Label ID="Title" runat="server" Text='<%# Eval("IncidentTitle") %>' CssClass="heading big" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="small grey">
                                                User
                                            </td>
                                            <td class="text">
                                                <asp:Label ID="User" runat="server" Text='<%# Eval("IncidentUser") %>' />
                                                <asp:Label ID="Location" runat="server" Text='<%# Eval("OfficeName", "({0} > ") & Eval("LocationName", "{0})") %>'
                                                    CssClass="small" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="small grey">
                                                Time Spent
                                            </td>
                                            <td class="text">
                                                <asp:Label ID="TimeSpent" runat="server" Text='<%# Eval("IncidentTimeSpent") %>' />
                                                minutes
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table id="IncidentDescription" runat="server" cellpadding="2" cellspacing="1" border="0" class="style2">
                                        <tr>
                                            <td colspan="2" class="orange_bg">
                                                <b>Additional Details</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" class="text" style="padding-left: 8px">
                                                <asp:Label ID="Description" runat="server" Text='<%# Eval("IncidentDescription") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="IncidentAction" runat="server" cellpadding="2" cellspacing="1" border="0" class="style2">
                                        <tr>
                                            <td class="orange_bg">
                                                <b>Action / Resolution</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="text" style="padding-left: 8px; padding-bottom: 10px;">
                                                <asp:Label ID="Solution" runat="server" Text='<%# Eval("IncidentSolution") %>' />
                                            </td>
                                        </tr>
                                     </table>
                                     <table id="AddUpdateTable" runat="server" cellpadding="0" cellspacing="0" border="0" class="style2">
                                        <tr class="small" style="height:2px;">
                                            <td align="right">
                                                <asp:Label ID="AddUpdate" runat="server" Style="color:navy; text-decoration: underline;"
                                                    onmouseover="this.style.cursor='pointer';" />
                                                <ajaxToolkit:CollapsiblePanelExtender ID="CPE1" runat="server" TargetControlID="Updates" Collapsed="True" TextLabelID="AddUpdate" CollapsedText="Add Comments" CollapseControlID="AddUpdate" ExpandControlID="AddUpdate" ExpandedText="Close Comments" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Updates" runat="server" Width="100%">
                                        <table cellpadding="2" cellspacing="1" border="0" class="style2">
                                            <tr>
                                                <td colspan="2" style="padding-left:8px;">
                                                    <asp:UpdatePanel ID="UP2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="GV1" runat="server" AutoGenerateColumns="False" 
                                                                DataKeyNames="NotesID" DataSourceID="IncidentUpdateSqlDataSource" 
                                                                EmptyDataText="Add" GridLines="None" onrowupdating="GV1_RowUpdating" 
                                                                ShowFooter="True" ShowHeader="False" Width="100%" CssClass="darkGrey" style="overflow:hidden; height:0px;">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="NotesDescription" SortExpression="NotesDescription">
                                                                        <EditItemTemplate>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="DescriptionTextBox"
                                                                                Display="Dynamic" ErrorMessage="Required Field!!">Required Field!!</asp:RequiredFieldValidator><asp:TextBox
                                                                                    ID="DescriptionTextBox" runat="server" CssClass="text" Text='<%# Bind("NotesDescription") %>'
                                                                                    TextMode="MultiLine" Width="99%" Height="35px"></asp:TextBox><asp:HiddenField ID="UserHiddenField0"
                                                                                        runat="server" Value='<%# Bind("NotesLastUpdatedBy") %>' />
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="DescriptionTextBox"
                                                                                Display="Dynamic" ErrorMessage="Required Field!!" ValidationGroup="Footer">Required Field!!</asp:RequiredFieldValidator>
                                                                            <asp:TextBox ID="DescriptionTextBox" runat="server" CssClass="text" 
                                                                                TextMode="MultiLine" Width="99%" Height="35px" ValidationGroup="Footer"></asp:TextBox>
                                                                            <ajaxToolkit:TextBoxWatermarkExtender
                                                                                ID="TBWE1" runat="server" TargetControlID="DescriptionTextBox" 
                                                                                WatermarkText="Add comments here" WatermarkCssClass="watermark small text">
                                                                            </ajaxToolkit:TextBoxWatermarkExtender>
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <div class="commentOuter">
                                                                                <span class="smaller lightGrey text">
                                                                                    Comment by <asp:Label ID="LastUpdatedByLabel" runat="server" Text='<%# Bind("NotesLastUpdatedBy") %>'></asp:Label>
                                                                                    on <asp:Label ID="LastUpdatedLabel" runat="server" Text='<%# Left(Eval("NotesLastUpdated"), 16) %>'></asp:Label>
                                                                                </span><br />
                                                                                <div class="commentInner">
                                                                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("NotesDescription") %>' CssClass="text"></asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="gridViewBorder" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ShowHeader="False">
                                                                        <EditItemTemplate>
                                                                            <asp:LinkButton ID="UpdateLinkButton" runat="server" CausesValidation="True" CommandName="Update"
                                                                                Text="Update"></asp:LinkButton><br />
                                                                            <asp:LinkButton ID="CancelLinkButton" runat="server" CausesValidation="False" CommandName="Cancel"
                                                                                Text="Cancel"></asp:LinkButton>
                                                                        </EditItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:ImageButton ID="FooterImageButton" runat="server" ImageAlign="Middle"
                                                                                ImageUrl="~/Images/accept.png" OnClick="FooterImageButton_Click" 
                                                                                ToolTip="Click here to add comment or update" ValidationGroup="Footer" />
                                                                        </FooterTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="EditLinkButton" runat="server" CausesValidation="False" CommandName="Edit"
                                                                                Text="Edit"></asp:LinkButton><br />
                                                                            <asp:LinkButton ID="DeleteLinkButton" runat="server" CausesValidation="False" CommandName="Delete"
                                                                                Text="Delete"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <ItemStyle CssClass="gridViewBorder smaller" Width="35px" HorizontalAlign="Right" VerticalAlign="Bottom" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <table cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="DescriptionTextBox"
                                                                                    Display="Dynamic" ErrorMessage="Required Field!!" ValidationGroup="UpdateValidation">Required 
                                                                                Field!!</asp:RequiredFieldValidator><asp:TextBox ID="DescriptionTextBox" runat="server"
                                                                                    CssClass="text" TextMode="MultiLine" ToolTip="Add updates or comments here and click the OK button to the right of this box. Your name and the date will be recorded."
                                                                                    ValidationGroup="UpdateValidation" Width="98%" Height="35px"></asp:TextBox>
                                                                                <ajaxToolkit:TextBoxWatermarkExtender
                                                                                    ID="TBWE1" runat="server" TargetControlID="DescriptionTextBox" 
                                                                                    WatermarkText="Add comments here" WatermarkCssClass="watermark small text">
                                                                                </ajaxToolkit:TextBoxWatermarkExtender>
                                                                            </td>
                                                                            <td style="width:35px;">
                                                                                <asp:ImageButton ID="EmptyDataImageButton" runat="server" ImageAlign="Middle"
                                                                                    ImageUrl="~/Images/accept.png" OnClick="EmptyDataImageButton_Click" ToolTip="Click here to add update"
                                                                                    ValidationGroup="UpdateValidation" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>
                                                            <asp:SqlDataSource ID="IncidentUpdateSqlDataSource" runat="server" 
                                                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                                                                DeleteCommand="DELETE FROM [tblNotes] WHERE [NotesID] = @NotesID" 
                                                                InsertCommand="INSERT INTO [tblNotes] ([NotesDescription], [NotesLastUpdated], [NotesLastUpdatedBy], [IncidentID]) VALUES (@NotesDescription, @NotesLastUpdated, @NotesLastUpdatedBy, @IncidentID)" 
                                                                SelectCommand="SELECT [NotesID], [NotesDescription], [NotesLastUpdated], [NotesLastUpdatedBy] FROM [tblNotes] WHERE ([IncidentID] = @IncidentID) ORDER BY [NotesID]" 
                                                                UpdateCommand="UPDATE [tblNotes] SET [NotesDescription] = @NotesDescription, [NotesLastUpdated] = @NotesLastUpdated, [NotesLastUpdatedBy] = @NotesLastUpdatedBy WHERE [NotesID] = @NotesID">
                                                                <SelectParameters>
                                                                    <asp:QueryStringParameter DefaultValue="1" Name="IncidentID" 
                                                                        QueryStringField="ID" Type="Int32" />
                                                                </SelectParameters>
                                                                <DeleteParameters>
                                                                    <asp:Parameter Name="NotesID" Type="Int32" />
                                                                </DeleteParameters>
                                                                <UpdateParameters>
                                                                    <asp:Parameter Name="NotesDescription" Type="String" />
                                                                    <asp:Parameter Name="NotesLastUpdated" Type="DateTime" />
                                                                    <asp:Parameter Name="NotesLastUpdatedBy" Type="String" />
                                                                    <asp:Parameter Name="NotesID" Type="Int32" />
                                                                </UpdateParameters>
                                                                <InsertParameters>
                                                                    <asp:QueryStringParameter DefaultValue="1" Name="IncidentID" 
                                                                        QueryStringField="ID" Type="Int32" />
                                                                    <asp:Parameter Name="NotesDescription" Type="String" />
                                                                    <asp:Parameter Name="NotesLastUpdated" Type="DateTime" />
                                                                    <asp:Parameter Name="NotesLastUpdatedBy" Type="String" />
                                                                </InsertParameters>
                                                            </asp:SqlDataSource>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            </table>
                                    </asp:Panel>
                                    <table border="0" cellpadding="2" cellspacing="0" class="style2 smaller">
                                        <tr>
                                            <td>
                                                <asp:Label ID="IncidentEnteredLabel" runat="server" Text='<%# Eval("IncidentEntered", "Incident raised on {0} by ") %>' />
                                                <asp:Label ID="IncidentEnteredByLabel" runat="server" Text='<%# Eval("IncidentEnteredBy") %>' />
                                                <asp:Label ID="IncidentClosedLabel" runat="server" Text='<%# Eval("IncidentClosed") %>' />
                                            </td>
                                        </tr>
                                        <asp:TableRow ID="LastUpdatedTableRow" runat="server">
                                            <asp:TableCell ID="LastUpdatedTableCell" CssClass="small"> Last modified
                    <asp:Label ID="IncidentLastUpdatedLabel" runat="server" Text='<%# Eval("IncidentLastUpdated") %>' />
                    by
                    <asp:Label ID="IncidentLastUpdatedByLabel" runat="server" Text='<%# Eval("IncidentLastUpdatedBy") %>' />
                    </asp:TableCell></asp:TableRow></table></td><td style="padding:10px;" width="20%" valign="top">
                                    <table align="right" border="0" cellpadding="2" cellspacing="1">
                                        <tr style="height:25px;">
                                            <td>
                                                <asp:ImageButton ID="NewImage" runat="server" CommandName="New" CausesValidation="False"
                                                    ImageUrl="~/Images/add_16.png" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                                                    Text="New" />
                                            </td>
                                        </tr>
                                        <tr style="height:25px;">
                                            <td>
                                                <asp:ImageButton ID="EditImage" runat="server" CommandName="Edit" CausesValidation="False"
                                                    ImageUrl="~/Images/edit_16.png" /> 
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                                    Text="Edit" />
                                            </td>
                                        </tr>
                                        <tr style="height:25px;">
                                            <td>
                                                <asp:HyperLink ID="CopyImage" runat="server" ImageUrl="Images/copy_16.png" NavigateUrl='<%# Eval("IncidentID", "Incident.aspx?Action=Copy&SourceID={0}") %>'></asp:HyperLink></td><td valign="top">
                                                <asp:HyperLink ID="CopyLink" runat="server" NavigateUrl='<%# Eval("IncidentID", "Incident.aspx?Action=Copy&SourceID={0}") %>'>Copy</asp:HyperLink></td></tr><tr style="height:25px;">
                                            <td>
                                                <asp:ImageButton ID="DeleteImage" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_16.png" />
                                            </td>
                                            <td valign="top">
                                                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="Delete" />
                                                <ajaxToolkit:ConfirmButtonExtender ID="DeleteImageConfirm" runat="server" TargetControlID="DeleteImage"
                                                    ConfirmText="Please confirm you wish to delete this QuickTicket?">
                                                </ajaxToolkit:ConfirmButtonExtender>
                                                <ajaxToolkit:ConfirmButtonExtender ID="DeleteButtonConfirm" runat="server" TargetControlID="DeleteButton"
                                                    ConfirmText="Please confirm you wish to delete this QuickTicket?">
                                                </ajaxToolkit:ConfirmButtonExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" align="right">
                                    <div id="openLabel" runat="server" class="text">
                                        <span style="color:#eee; font-size:24pt; z-index:3; position:relative; top:20px; right:25px">status:</span> <br /><span style="color:#ddd; font-size:36pt; z-index:2;">Open</span> </div></td></tr></table></ItemTemplate></asp:FormView></td><td style="width:15px; background: url('Images/ticket_perf_r.gif') repeat-y;">
                    &nbsp; </td></tr></table><asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        DeleteCommand="DELETE FROM [tblNotes] WHERE [IncidentID] = @original_IncidentID; DELETE FROM [tblIncident] WHERE [IncidentID] = @original_IncidentID" 
        InsertCommand="INSERT INTO [tblIncident] ([IncidentTitle], [IncidentUser], [OfficeID], [LocationID], [CategoryID], [KnownIssueID], [IncidentDescription], [IncidentSolution], [IncidentAssignedTo], [PriorityID], [IncidentTimeSpent], [StatusID], [IncidentEntered], [IncidentEnteredBy], [IncidentClosed], [IncidentClosedBy], [IncidentLastUpdated], [IncidentLastUpdatedBy]) 
                        VALUES (@IncidentTitle, @IncidentUser, @OfficeID, @LocationID, @CategoryID, @KnownIssueID, @IncidentDescription, @IncidentSolution, @IncidentAssignedTo, @PriorityID, @IncidentTimeSpent, @StatusID, @IncidentEntered, @IncidentEnteredBy, @IncidentClosed, @IncidentClosedBy, @IncidentLastUpdated, @IncidentEnteredBy); SELECT @IncidentID = SCOPE_IDENTITY()" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCommand="SELECT     tblIncident.IncidentID, tblIncident.IncidentTitle, tblIncident.IncidentUser, tblIncident.OfficeID, tblIncident.LocationID, tblIncident.CategoryID, 
                           tblIncident.KnownIssueID, tblIncident.IncidentDescription, tblIncident.IncidentSolution, tblIncident.IncidentAssignedTo, tblIncident.PriorityID, 
                           tblIncident.IncidentTimeSpent, tblIncident.StatusID, tblIncident.IncidentEntered, tblIncident.IncidentEnteredBy, tblIncident.IncidentClosed, 
                           tblIncident.IncidentClosedBy, tblIncident.IncidentLastUpdated, tblIncident.IncidentLastUpdatedBy, tblOffice.OfficeName, tblLocation.LocationName, 
                           tblCategory.CategoryTitle, tblCategory.CategoryDescription, tblPriority.PriorityTitle, tblIS.StatusTitle AS IStatus, tblKI.KnownIssueTitle, 
                           tblKI.KnownIssueNotes, tblKIS.StatusTitle AS KIStatus
                       FROM       tblIncident INNER JOIN
                           tblOffice ON tblIncident.OfficeID = tblOffice.OfficeID LEFT OUTER JOIN
                           tblLocation ON tblIncident.LocationID = tblLocation.LocationID INNER JOIN
                           tblCategory ON tblIncident.CategoryID = tblCategory.CategoryID INNER JOIN
                           tblPriority ON tblIncident.PriorityID = tblPriority.PriorityID INNER JOIN
                           tblStatus AS tblIS ON tblIncident.StatusID = tblIS.StatusID LEFT OUTER JOIN
                           tblKnownIssue AS tblKI ON tblKI.KnownIssueID = tblIncident.KnownIssueID LEFT OUTER JOIN
                           tblStatus AS tblKIS ON tblKIS.StatusID = tblKI.StatusID
                       WHERE      (tblIncident.IncidentID = @IncidentID)" 
        UpdateCommand="UPDATE [tblIncident] SET [IncidentTitle] = @IncidentTitle, [IncidentUser] = @IncidentUser, [OfficeID] = @OfficeID, [LocationID] = @LocationID, [CategoryID] = @CategoryID, [KnownIssueID] = @KnownIssueID, [IncidentDescription] = @IncidentDescription, [IncidentSolution] = @IncidentSolution, [IncidentAssignedTo] = @IncidentAssignedTo, [PriorityID] = @PriorityID, [IncidentTimeSpent] = @IncidentTimeSpent, [StatusID] = @StatusID, [IncidentClosed] = @IncidentClosed, [IncidentClosedBy] = @IncidentClosedBy, [IncidentLastUpdated] = @IncidentLastUpdated, [IncidentLastUpdatedBy] = @IncidentLastUpdatedBy 
                        WHERE [IncidentID] = @original_IncidentID">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="1" Name="IncidentID" 
                QueryStringField="ID" Type="Int32" />
        </SelectParameters>
        <DeleteParameters>
            <asp:Parameter Name="original_IncidentID" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="IncidentTitle" Type="String" />
            <asp:Parameter Name="IncidentUser" Type="String" />
            <asp:Parameter Name="OfficeID" Type="Int32" />
            <asp:Parameter Name="LocationID" Type="Int32" />
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="KnownIssueID" Type="Int32" />
            <asp:Parameter Name="IncidentDescription" Type="String" />
            <asp:Parameter Name="IncidentSolution" Type="String" />
            <asp:Parameter Name="IncidentAssignedTo" Type="String" />
            <asp:Parameter Name="PriorityID" Type="Int32" />
            <asp:Parameter Name="IncidentTimeSpent" Type="Int32" />
            <asp:Parameter Name="StatusID" Type="Int32" />
            <asp:Parameter Name="IncidentClosed" Type="DateTime" />
            <asp:Parameter Name="IncidentClosedBy" Type="String" />
            <asp:Parameter Name="IncidentLastUpdated" Type="DateTime" />
            <asp:Parameter Name="IncidentLastUpdatedBy" Type="String" />
            <asp:Parameter Name="original_IncidentID" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="IncidentTitle" Type="String" />
            <asp:Parameter Name="IncidentUser" Type="String" />
            <asp:Parameter Name="OfficeID" Type="Int32" />
            <asp:Parameter Name="LocationID" Type="Int32" />
            <asp:Parameter Name="CategoryID" Type="Int32" />
            <asp:Parameter Name="KnownIssueID" Type="Int32" />
            <asp:Parameter Name="IncidentDescription" Type="String" ConvertEmptyStringToNull="true" />
            <asp:Parameter Name="IncidentSolution" Type="String" ConvertEmptyStringToNull="true" />
            <asp:Parameter Name="IncidentAssignedTo" Type="String" />
            <asp:Parameter Name="PriorityID" Type="Int32" />
            <asp:Parameter Name="IncidentTimeSpent" Type="Int32" />
            <asp:Parameter Name="StatusID" Type="Int32" />
            <asp:Parameter Name="IncidentEntered" Type="DateTime" />
            <asp:Parameter Name="IncidentEnteredBy" Type="String" />
            <asp:Parameter Name="IncidentClosed" Type="DateTime" />
            <asp:Parameter Name="IncidentClosedBy" Type="String" />
            <asp:Parameter Name="IncidentLastUpdated" Type="DateTime" />
            <asp:Parameter Name="IncidentID" Type="Int32" Direction="Output" />
        </InsertParameters>
    </asp:SqlDataSource>
</div>
</asp:Content>

