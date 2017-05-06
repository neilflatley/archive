<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="ReportPage.aspx.vb" Inherits="ReportPage" title="QuickTicket Reports" EnableEventValidation="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .style3 { width: 65px; }
    .style3a { width: 64px; }
    .style4 { width: 80px; }
    .leftColumn { padding-left:5px; }
</style>
<script type="text/javascript">
function TwoRowHighlight_On(obj)
{
    if(obj != null)
    {
        obj.originalClassName = obj.className;
        obj.className = obj.className + 'HL';
    }
}
function TwoRowHighlight_Off(obj)
{
    if(obj != null)
    {
        obj.className = obj.originalClassName;
    }
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" CssClass="noPrint">
        <asp:Image ID="ExpandImage" runat="server" 
            ImageUrl="~/Images/WebResourceMinus.gif" />
        &nbsp;<asp:Label ID="HeaderLabel" runat="server" CssClass="text"></asp:Label>
    </asp:Panel>
    <asp:Panel ID="SearchPanel" runat="server" Height="0" style="overflow:hidden;" CssClass="small darkGrey noPrint">
        <table cellpadding="3" cellspacing="0" style="width:750px;">
            <tr>
                <td class="style3">
                    Keywords:</td>
                <td colspan="3">
                    <asp:TextBox ID="KeywordsTextBox" runat="server" Width="340px"></asp:TextBox>
                </td>
                <td align="right" class="style3">Assigned:</td>
                <td style="width:80px;">
                    <asp:DropDownList ID="AssignedToDropDownList" runat="server" CssClass="text" 
                        EnableViewState="False">
                        <asp:ListItem Value="%">All</asp:ListItem>
                        <asp:ListItem Value="daleb">Dale</asp:ListItem>
                        <asp:ListItem Value="gaynorh">Gaynor</asp:ListItem>
                        <asp:ListItem Value="jeffe">Jeff</asp:ListItem>
                        <asp:ListItem Value="kerrymc">Kerry</asp:ListItem>
                        <asp:ListItem Value="krisstopherb">Krisstopher</asp:ListItem>
                        <asp:ListItem Value="neilf">Neil</asp:ListItem>
                        <asp:ListItem Value="rhysw">Rhys</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3" align="right">&nbsp;</td>
                <td>
                    <asp:Button ID="SubmitButton" runat="server" CssClass="text" Text="Search" />
                </td>
            </tr>
            <tr>
                <td class="style3">
                    Start Date:</td>
                <td class="style4">
                    <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="text" Width="80px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="StartDateCalendar" runat="server" 
                        TargetControlID="StartDateTextBox" PopupButtonID="Image1" 
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Image ID="Image1" runat="server" ImageUrl="Images/calendar.png" />
                </td>
                <td align="right" class="style3">
                    End Date:</td>
                <td>
                    <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="text" Width="80px"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="EndDateCalendar" runat="server" 
                        PopupButtonID="Image2" TargetControlID="EndDateTextBox" 
                        Format="dd/MM/yyyy">
                    </ajaxToolkit:CalendarExtender>
                    <asp:Image ID="Image2" runat="server" ImageUrl="Images/calendar.png" />
                </td>
                <td align="right">Status:</td>
                <td>
                    <asp:DropDownList ID="StatusDropDownList" runat="server" 
                        AppendDataBoundItems="True" CssClass="text" DataSourceID="StatusSqlDataSource" 
                        DataTextField="StatusTitle" DataValueField="StatusID">
                        <asp:ListItem Value="">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="style4">
                    <asp:Button ID="ClearButton" runat="server" CssClass="text" Text="Clear" />
                    <asp:CheckBox ID="KnownCheckBox" runat="server" CssClass="text" 
                        Visible="False" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="MorePanel" runat="server">
        <table cellpadding="3" cellspacing="0" style="width:750px;">
            <tr>
                <td class="style3a">
                    User:</td>
                <td class="style4">
                    <asp:DropDownList ID="UserDropDownList" runat="server" 
                        AppendDataBoundItems="True" CssClass="text" DataTextField="IncidentUser" 
                        DataValueField="IncidentUser" DataSourceID="UsersSqlDataSource" 
                        EnableViewState="False">
                        <asp:ListItem Value="%">All</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="UserTextBox" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
                <td class="style3" align="right">
                    Office:</td>
                <td class="style4">
                    <asp:DropDownList ID="OfficeDropDownList" runat="server" 
                        AppendDataBoundItems="True" CssClass="text" EnableViewState="False">
                        <asp:ListItem Value="">All</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="CCD1" runat="server" Category="Office" 
                        PromptText="All" ServiceMethod="GetOffices" ServicePath="WebService.asmx" 
                        TargetControlID="OfficeDropDownList">
                    </ajaxToolkit:CascadingDropDown>
                    <asp:TextBox ID="OfficeTextBox" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
                <td align="right" class="style3">
                    Location:</td>
                <td class="style4">
                    <asp:DropDownList ID="LocationDropDownList" runat="server" 
                        AppendDataBoundItems="True" CssClass="text" EnableViewState="False">
                        <asp:ListItem Value="">All</asp:ListItem>
                    </asp:DropDownList>
                    <ajaxToolkit:CascadingDropDown ID="CCD2" runat="server" Category="Location" 
                        ParentControlID="OfficeDropDownList" PromptText="All" 
                        ServiceMethod="GetLocations" ServicePath="WebService.asmx" 
                        TargetControlID="LocationDropDownList">
                    </ajaxToolkit:CascadingDropDown>
                    <asp:TextBox ID="LocationTextBox" runat="server" Visible="False" Width="10px"></asp:TextBox>
                </td>
                <td class="style3">
                    Category:</td>
                <td>
                    <asp:DropDownList ID="CategoryDropDownList" runat="server" 
                        AppendDataBoundItems="True" CssClass="text" 
                        DataSourceID="CategorySqlDataSource" DataTextField="CategoryTitle" 
                        DataValueField="CategoryID" EnableViewState="False">
                        <asp:ListItem Value="">All</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        </asp:Panel>
        <table cellpadding="3" cellspacing="0" style="width:750px;">
            <tr>
                <td valign="top">
                    <ajaxToolkit:CollapsiblePanelExtender ID="CPE2" runat="server" 
                        CollapseControlID="MoreLinkButton" Collapsed="True" CollapsedText="More..." 
                        ExpandControlID="MoreLinkButton" ExpandedText="Less..." SuppressPostBack="True" 
                        TargetControlID="MorePanel" TextLabelID="MoreLinkButton">
                    </ajaxToolkit:CollapsiblePanelExtender>
                    <asp:LinkButton ID="MoreLinkButton" runat="server" CssClass="small">More...</asp:LinkButton>
                    <asp:SqlDataSource ID="UsersSqlDataSource" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="SELECT DISTINCT { fn LCASE(IncidentUser) } AS IncidentUser FROM tblIncident ORDER BY IncidentUser">
                    </asp:SqlDataSource>
                </td>
                <td align="right">
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
    <ajaxToolkit:CollapsiblePanelExtender ID="CPE1" 
        runat="server" CollapseControlID="Panel1" 
        CollapsedText="Search results - click here to refine your search" 
        ExpandControlID="Panel1" ExpandedText="Enter search criteria below" 
        TargetControlID="SearchPanel" TextLabelID="HeaderLabel" 
        CollapsedImage="~/Images/WebResourcePlus.gif" 
        ExpandedImage="~/Images/WebResourceMinus.gif" ImageControlID="ExpandImage" 
        Collapsed="True">
    </ajaxToolkit:CollapsiblePanelExtender>
    <div style="width:100%">
        <asp:GridView ID="GV1" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BorderStyle="None" CellPadding="2" CssClass="small" 
                    DataKeyNames="IncidentID" 
                    GridLines="None" Width="100%" 
                     ShowFooter="True">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" SelectText="" Visible="False" />
                        <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="IncidentID">
                            <ItemTemplate>
                                <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("IncidentID") %>' CssClass="lightGrey"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="42px" />
                            <ControlStyle />
                            <ItemStyle CssClass="small text leftColumn" />
                        </asp:TemplateField>
<%--                        <asp:BoundField DataField="CategoryTitle" HeaderText="Category" SortExpression="CategoryTitle">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="darkGrey small text" />
                        </asp:BoundField>
--%>                    <asp:TemplateField HeaderText="Incident" SortExpression="IncidentTitle">
                            <ItemTemplate>
                                <span id="tag" runat="server" style="float:right;">
                                    <asp:Label ID="Priority" runat="server" Text='<%# Bind("PriorityTitle") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="Cat" runat="server" Text='<%# Bind("CategoryTitle") %>'></asp:Label> |
                                    <asp:Label ID="Assigned" runat="server" Text='<%# Bind("IncidentAssignedTo") %>' />
                                    <span id="updatesTag" runat="server"> |
                                        <asp:Label ID="Updates" runat="server" Text='<%# Bind("IncidentUpdates") %>' CssClass="red" />
                                    </span>
                                </span>
                                <asp:Label ID="TitleLabel" CssClass="lucida" runat="server" Text='<%# Bind("IncidentTitle") %>'></asp:Label>
                                <asp:HiddenField ID="DescriptionHF" runat="server" Value='<%# Eval("IncidentDescription") %>' Visible="false" />
                                <asp:HiddenField ID="SolutionHF" runat="server" Value='<%# Eval("IncidentSolution") %>' Visible="false" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="52%" />
                            <ControlStyle />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location" SortExpression="OfficeName,LocationName">
                            <HeaderTemplate>
                                <asp:LinkButton ID="UserSortButton" runat="server" 
                                    CommandArgument="IncidentUser" CommandName="Sort" CssClass="white">User</asp:LinkButton>
                                &nbsp;/
                                <asp:LinkButton ID="LocationSortButton" runat="server" 
                                    CommandArgument="OfficeName, LocationName" CommandName="Sort" CssClass="white">Location</asp:LinkButton>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="UserLabel" runat="server" Text='<%# Bind("IncidentUser") %>'></asp:Label>
                                <span class="smaller">
                                    [<asp:Label ID="OfficeLabel" runat="server" Text='<%# Bind("OfficeName") %>'></asp:Label>
                                    &gt;
                                    <asp:Label ID="LocationLabel" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>]
                                </span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="27%" />
                            <ControlStyle />
                            <ItemStyle CssClass="small text padDown" />
                        </asp:TemplateField>
<%--                        <asp:TemplateField HeaderText="Assigned" SortExpression="IncidentAssignedTo">
                            <ItemTemplate>
                                <asp:Label ID="AssignedToLabel" runat="server" Text='<%# Bind("IncidentAssignedTo") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="small text" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="PriorityTitle" HeaderText="Priority" 
                            SortExpression="PriorityTitle" >
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle CssClass="small text" />
                        </asp:BoundField>
--%>                        <asp:BoundField DataField="IncidentTimeSpent" HeaderText="Time" 
                            SortExpression="IncidentTimeSpent" >
                            <HeaderStyle HorizontalAlign="Left" Width="42px" />
                            <ControlStyle />
                            <ItemStyle CssClass="small text" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Entered" SortExpression="IncidentEntered">
                            <ItemTemplate>
                                <asp:Label ID="Entered" runat="server" Text='<%# Left(Eval("IncidentEntered"), 16) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="80px" />
                            <ControlStyle />
                            <ItemStyle CssClass="smaller text" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="StatusTitle">
                            <ItemTemplate>
                                <asp:Label ID="StatusLabel" runat="server" Text='<%# Bind("StatusTitle") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Width="60px" />
                            <ControlStyle />
                            <ItemStyle CssClass="small text" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="white small text black_bg plainHyperLink" ForeColor="White" />
                    <RowStyle CssClass="allowPgBrk" />
                </asp:GridView>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT tblIncident.IncidentID, tblIncident.IncidentTitle, tblIncident.IncidentUser, tblOffice.OfficeName, tblLocation.LocationName, tblCategory.CategoryTitle, tblIncident.IncidentDescription, tblIncident.IncidentSolution, tblIncident.IncidentAssignedTo, tblPriority.PriorityTitle, tblIncident.IncidentTimeSpent, tblStatus.StatusTitle, COUNT(tblNotes.NotesID) AS IncidentUpdates, tblIncident.IncidentEntered 
        FROM tblIncident INNER JOIN 
        tblOffice ON tblIncident.OfficeID = tblOffice.OfficeID LEFT OUTER JOIN 
        tblLocation ON tblIncident.LocationID = tblLocation.LocationID AND tblOffice.OfficeID = tblLocation.OfficeID INNER JOIN 
        tblCategory ON tblIncident.CategoryID = tblCategory.CategoryID INNER JOIN 
        tblPriority ON tblIncident.PriorityID = tblPriority.PriorityID INNER JOIN 
        tblStatus ON tblIncident.StatusID = tblStatus.StatusID LEFT OUTER JOIN
        tblNotes ON tblIncident.IncidentID = tblNotes.IncidentID
        WHERE (tblIncident.IncidentEntered BETWEEN @StartDate AND @EndDate) 
        AND ((tblIncident.IncidentTitle LIKE @Keywords) OR (tblIncident.IncidentDescription LIKE @Keywords) OR (tblIncident.IncidentSolution LIKE @Keywords)) 
        AND (tblIncident.IncidentAssignedTo LIKE @AssignedTo) 
        AND (@StatusID IS NULL OR (tblIncident.StatusID = @StatusID)) 
        AND (@CategoryID IS NULL OR (tblIncident.CategoryID = @CategoryID)) 
        AND (tblIncident.IncidentUser LIKE @User)
        AND (@OfficeID IS NULL OR (tblIncident.OfficeID = @OfficeID)) 
        AND (@LocationID IS NULL OR (tblIncident.LocationID = @LocationID))
        GROUP BY tblIncident.IncidentID, tblIncident.IncidentTitle, tblIncident.IncidentUser, tblOffice.OfficeName, tblLocation.LocationName, tblCategory.CategoryTitle, tblIncident.IncidentDescription, tblIncident.IncidentSolution, tblIncident.IncidentAssignedTo, tblPriority.PriorityTitle, tblIncident.IncidentTimeSpent, tblStatus.StatusTitle, tblIncident.IncidentEntered 
        ORDER BY tblIncident.IncidentID DESC" 
        CancelSelectOnNullParameter="False">
        <SelectParameters>
            <asp:ControlParameter ControlID="KeywordsTextBox" Name="Keywords" 
                PropertyName="Text" Type="String" DefaultValue="%" />
            <asp:ControlParameter ControlID="StartDateTextBox" Name="StartDate" 
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="EndDateTextBox" Name="EndDate" 
                PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter ControlID="AssignedToDropDownList" Name="AssignedTo" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="StatusDropDownList" Name="StatusID" 
                PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="CategoryDropDownList" Name="CategoryID" 
                PropertyName="SelectedValue" Type="Int16" />
            <asp:ControlParameter ControlID="UserTextBox" Name="User" 
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="OfficeTextBox" Name="OfficeID" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="LocationTextBox" Name="LocationID" 
                PropertyName="Text" />
         </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="StatusSqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [tblStatus]"></asp:SqlDataSource>
    <asp:SqlDataSource ID="CategorySqlDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT [CategoryID], [CategoryTitle] FROM [tblCategory] ORDER BY [CategoryTitle]">
    </asp:SqlDataSource>
</asp:Content>

