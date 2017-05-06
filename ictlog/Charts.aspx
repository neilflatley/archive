<%@ Page Title="Quickticket Charts" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Charts.aspx.vb" Inherits="Charts" EnableViewState="True" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="C" Runat="Server">

    <table width="100%" cellpadding="5" cellspacing="0">
    <col width="33%" />
    <col width="33%" />
    <col width="33%" />
    <tr>
        <td>
            <asp:UpdatePanel ID="upCat" runat="server">
                <ContentTemplate>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Category Summary
                </div>
                <div class="chartButtons text small">
                    <span style="text-align:center; width: 100%; padding-bottom: 5px;">
                        <asp:LinkButton ID="catToday" runat="server">Today</asp:LinkButton> |
                        <asp:LinkButton ID="cat7day" runat="server">7 day</asp:LinkButton> |
                        <asp:LinkButton ID="cat30day" runat="server">30 day</asp:LinkButton> |
                        <asp:LinkButton ID="cat90day" runat="server">90 day</asp:LinkButton> |
                        <asp:LinkButton ID="catTotal" runat="server">Total</asp:LinkButton>
                    </span>
                    <br />
                    <br style="line-height:5px;" />
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:28px">
                                    From 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="catStart" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="catStartCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calCatStart" runat="server" TargetControlID="catStart" FirstDayOfWeek="Monday" PopupButtonID="catStartCal" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:15px">
                                    To 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="catEnd" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="catEndCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calCatEnd" runat="server" TargetControlID="catEnd" FirstDayOfWeek="Monday" PopupButtonID="catEndCal" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="catGo" runat="server" CssClass="smaller" Text="go" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            <asp:Chart ID="CategoryChart" runat="server" DataSourceID="CategorySDS" 
                    Width="375px">
                <series>
                    <asp:Series Name="Closed" ChartType="StackedBar" XValueMember="CategoryTitle" 
                        YValueMembers="Closed" 
                        CustomProperties="DrawingStyle=Emboss, PointWidth=1" 
                        Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" 
                        Legend="Legend1">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="StackedBar" 
                        CustomProperties="DrawingStyle=Emboss, PointWidth=1" Name="Open" 
                        XValueMember="CategoryTitle" YValueMembers="Open" Legend="Legend1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                        <AxisX IsReversed="True">
                        </AxisX>
                        <AxisX2 IsReversed="True">
                        </AxisX2>
                    </asp:ChartArea>
                </chartareas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1" TableStyle="Wide">
                    </asp:Legend>
                </Legends>
                <BorderSkin BorderColor="Silver" />
            </asp:Chart>
            </div>
            <asp:SqlDataSource ID="CategorySDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                SelectCommand="SELECT     CategoryTitle,
                          (SELECT     COUNT(IncidentID) AS Expr1
                            FROM          tblIncident
                            WHERE      (StatusID = 1) AND (CategoryID = tblCategory.CategoryID) AND (DATEDIFF(dd, @startDate, IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, 
                                                   @startDate, IncidentEntered) &gt;= 0)) AS 'Open',
                          (SELECT     COUNT(IncidentID) AS Expr1
                            FROM          tblIncident AS tblIncident_1
                            WHERE      (StatusID = 2) AND (CategoryID = tblCategory.CategoryID) AND (DATEDIFF(dd, @startDate, IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, 
                                                   @startDate, IncidentEntered) &gt;= 0)) AS 'Closed'
FROM         tblCategory">
                <SelectParameters>
                    <asp:Parameter Name="startDate" />
                    <asp:Parameter Name="DateIntervalValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            <asp:UpdatePanel ID="upPriority" runat="server">
                <ContentTemplate>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Priority Summary
                </div>
                <div class="chartButtons text small">
                    <span style="text-align:center; width: 100%; padding-bottom: 5px;">
                        <asp:LinkButton ID="priorToday" runat="server">Today</asp:LinkButton> |
                        <asp:LinkButton ID="prior7day" runat="server">7 day</asp:LinkButton> |
                        <asp:LinkButton ID="prior30day" runat="server">30 day</asp:LinkButton> |
                        <asp:LinkButton ID="prior90day" runat="server">90 day</asp:LinkButton> |
                        <asp:LinkButton ID="priorTotal" runat="server">Total</asp:LinkButton>
                    </span>
                    <br />
                    <br style="line-height:5px;" />
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:28px">
                                    From 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="priorStart" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="priorStartCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calPriorStart" runat="server" TargetControlID="priorStart" FirstDayOfWeek="Monday" PopupButtonID="priorStartCal" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:15px">
                                    To 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="priorEnd" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="priorEndCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calPriorEnd" runat="server" TargetControlID="priorEnd" FirstDayOfWeek="Monday" PopupButtonID="priorEndCal" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="priorGo" runat="server" CssClass="smaller" Text="go" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            <asp:Chart ID="PriorityChart" runat="server" DataSourceID="PrioritySDS" 
                    Width="375px">
                <series>
                    <asp:Series Name="Series1" ChartType="Pie" XValueMember="PriorityTitle" 
                        YValueMembers="Count" 
                        CustomProperties="PieDrawingStyle=Concave, PieLabelStyle=Outside" 
                        Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="False" Legend="Legend1">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1" Area3DStyle-Enable3D="True">
                        <AxisY>
                            <LabelStyle Enabled="False" />
                        </AxisY>
                        <AxisX>
                            <LabelStyle Enabled="False" />
                        </AxisX>
                        <AxisX2>
                            <LabelStyle Enabled="False" />
                        </AxisX2>
                        <AxisY2>
                            <LabelStyle Enabled="False" />
                        </AxisY2>
                    </asp:ChartArea>
                </chartareas>
                <Legends>
                    <asp:Legend Name="Legend1" TitleFont="Microsoft Sans Serif, 6pt, style=Bold" Docking="Bottom">
                    </asp:Legend>
                </Legends>
                <BorderSkin BorderColor="Silver" />
            </asp:Chart>
            </div>
            <asp:SqlDataSource ID="PrioritySDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT     PriorityTitle,
                          (SELECT     COUNT(IncidentID) AS Expr1
                            FROM          tblIncident
                            WHERE      (PriorityID = tblPriority.PriorityID) AND (DATEDIFF(dd, @startDate, IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, @startDate, 
                                                   IncidentEntered) &gt;= 0)) AS Count
FROM         tblPriority">
                <SelectParameters>
                    <asp:Parameter Name="startDate" />
                    <asp:Parameter Name="DateIntervalValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            <asp:UpdatePanel ID="upAssigned" runat="server">
                <ContentTemplate>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Tickets Assigned
                </div>
                <div class="chartButtons text small">
                    <span style="text-align:center; width: 100%; padding-bottom: 5px;">
                        <asp:LinkButton ID="assToday" runat="server">Today</asp:LinkButton> |
                        <asp:LinkButton ID="ass7day" runat="server">7 day</asp:LinkButton> |
                        <asp:LinkButton ID="ass30day" runat="server">30 day</asp:LinkButton> |
                        <asp:LinkButton ID="ass90day" runat="server">90 day</asp:LinkButton> |
                        <asp:LinkButton ID="assTotal" runat="server">Total</asp:LinkButton>
                    </span>
                    <br />
                    <br style="line-height:5px;" />
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:28px">
                                    From 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="assStart" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="assStartCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calAssStart" runat="server" TargetControlID="assStart" FirstDayOfWeek="Monday" PopupButtonID="assStartCal" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:15px">
                                    To 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="assEnd" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="assEndCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calAssEnd" runat="server" TargetControlID="assEnd" FirstDayOfWeek="Monday" PopupButtonID="assEndCal" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="assGo" runat="server" CssClass="smaller" Text="go" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            <asp:Chart ID="AssignedChart" runat="server" DataSourceID="AssignedSDS" 
                    Width="375px" Palette="Pastel">
                <series>
                    <asp:Series Name="Tickets Raised" ChartType="Bar" XValueMember="IncidentAssignedTo" 
                        YValueMembers="RaisedCount" 
                        CustomProperties="DrawingStyle=Emboss, PointWidth=1" 
                        Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True">
                    </asp:Series>
                    <asp:Series ChartArea="ChartArea1" ChartType="Bar" 
                        CustomProperties="DrawingStyle=Emboss, PointWidth=1" 
                        Font="Microsoft Sans Serif, 7pt" IsValueShownAsLabel="True" Name="Tickets Closed" 
                        XValueMember="IncidentAssignedTo" YValueMembers="ClosedCount">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
                <Legends>
                    <asp:Legend Docking="Bottom" Name="Legend1" TableStyle="Wide">
                    </asp:Legend>
                </Legends>
                <BorderSkin BorderColor="Silver" />
            </asp:Chart>
            </div>
            <asp:SqlDataSource ID="AssignedSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DISTINCT IncidentAssignedTo,
                          (SELECT     COUNT(IncidentID) AS Expr1
                            FROM          tblIncident AS tblIncident_1
                            WHERE      (IncidentAssignedTo = tblIncident.IncidentAssignedTo) AND (DATEDIFF(dd, @startDate, IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, 
                                                   @startDate, IncidentEntered) &gt;= 0)) AS RaisedCount,
                          (SELECT     COUNT(IncidentID) AS Expr1
                            FROM          tblIncident AS tblIncident_2
                            WHERE      (IncidentAssignedTo = tblIncident.IncidentAssignedTo) AND (DATEDIFF(dd, @startDate, IncidentClosed) &lt; @DateIntervalValue) AND (DATEDIFF(dd, 
                                                   @startDate, IncidentClosed) &gt;= 0)) AS ClosedCount
FROM         tblIncident
ORDER BY IncidentAssignedTo DESC">
                <SelectParameters>
                    <asp:Parameter Name="startDate" />
                    <asp:Parameter Name="DateIntervalValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Statistics</div>
here&nbsp;
            </div>

        </td>
        <td>
            <asp:UpdatePanel ID="upUsers" runat="server">
                <ContentTemplate>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Top Users</div>
                <div class="chartButtons text small">
                    <table cellpadding="0" cellspacing="2" style="width:100%; display:inline-table;">
                        <tr>
                            <td valign="top">
                                <span style="text-align:center; width: 100%; padding-bottom: 5px;">
                                    <asp:LinkButton ID="usersToday" runat="server">Today</asp:LinkButton> |
                                    <asp:LinkButton ID="users7day" runat="server">7 day</asp:LinkButton> |
                                    <asp:LinkButton ID="users30day" runat="server">30 day</asp:LinkButton> |
                                    <asp:LinkButton ID="users90day" runat="server">90 day</asp:LinkButton> |
                                    <asp:LinkButton ID="usersTotal" runat="server">Total</asp:LinkButton>
                                </span>                            
                            </td>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="2" style="float:right; display:inline-table;">
                                    <tr>
                                        <td>Top</td>
                                        <td>
                                            <asp:DropDownList ID="NumberDDL" runat="server" AutoPostBack="True" 
                                                CssClass="small">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="20" Value="20" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="40" Value="40" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="75" Value="75" />
                                                <asp:ListItem Text="100" Value="100" />
                                                <asp:ListItem Text="200" Value="200" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:28px">
                                    From 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="usersStart" runat="server" 
                                        CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="usersStartCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calUsersStart" runat="server" TargetControlID="usersStart" FirstDayOfWeek="Monday" PopupButtonID="usersStartCal" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:15px">
                                    To 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="usersEnd" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="usersEndCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calUsersEnd" runat="server" TargetControlID="usersEnd" FirstDayOfWeek="Monday" PopupButtonID="usersEndCal" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="usersGo" runat="server" CssClass="smaller" Text="go" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            <asp:Repeater ID="UsersRpt" runat="server" DataSourceID="UsersSDS">
                <HeaderTemplate><div class="scrollingBox text"><ol class="usersList"></HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:Label ID="Count" runat="server" Text='<%# Eval("Count") %>' style="float:right; padding-right:10px;" />
                        <asp:Label ID="User" runat="server" Text='<%# MasterClass.ADNameSearch(Eval("User"), "displayName") %>' />
                    </li>
                </ItemTemplate>
                <FooterTemplate></ol></div></FooterTemplate>
            </asp:Repeater>
            </div>
            <asp:SqlDataSource ID="UsersSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT     TOP (@Number) IncidentUser AS 'User', COUNT(IncidentID) AS Count
FROM         tblIncident
WHERE     (DATEDIFF(dd, @startDate, IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, @startDate, IncidentEntered) &gt;= 0)
GROUP BY IncidentUser
ORDER BY Count DESC">
                <SelectParameters>
                    <asp:Parameter Name="Number" DbType="Int16" />
                    <asp:Parameter Name="startDate" />
                    <asp:Parameter Name="DateIntervalValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td>
            <asp:UpdatePanel ID="upLocations" runat="server">
                <ContentTemplate>
            <div class="chartContainer">
                <div class="chartHeader text">
                    Top Locations</div>
                <div class="chartButtons text small">
                    <table cellpadding="0" cellspacing="2" style="width:100%; display:inline-table;">
                        <tr>
                            <td valign="top">
                                <span style="text-align:center; width: 100%; padding-bottom: 5px;">
                                    <asp:LinkButton ID="locToday" runat="server">Today</asp:LinkButton> |
                                    <asp:LinkButton ID="loc7day" runat="server">7 day</asp:LinkButton> |
                                    <asp:LinkButton ID="loc30day" runat="server">30 day</asp:LinkButton> |
                                    <asp:LinkButton ID="loc90day" runat="server">90 day</asp:LinkButton> |
                                    <asp:LinkButton ID="locTotal" runat="server">Total</asp:LinkButton>
                                </span>                            
                            </td>
                            <td valign="top">
                                <table cellpadding="0" cellspacing="2" style="float:right; display:inline-table;">
                                    <tr>
                                        <td>Top</td>
                                        <td>
                                            <asp:DropDownList ID="LocationsDDL" runat="server" AutoPostBack="True" 
                                                CssClass="small">
                                                <asp:ListItem Text="10" Value="10" />
                                                <asp:ListItem Text="20" Value="20" />
                                                <asp:ListItem Text="30" Value="30" />
                                                <asp:ListItem Text="40" Value="40" />
                                                <asp:ListItem Text="50" Value="50" />
                                                <asp:ListItem Text="75" Value="75" />
                                                <asp:ListItem Text="100" Value="100" />
                                                <asp:ListItem Text="200" Value="200" />
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:28px">
                                    From 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="locStart" runat="server" 
                                        CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="locStartCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calLocStart" runat="server" TargetControlID="locStart" FirstDayOfWeek="Monday" PopupButtonID="locStartCal" Format="dd/MM/yyyy" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="chartTextBox">
                        <table cellpadding="0" cellspacing="2" style="display: inline">
                            <tr>
                                <td style="width:15px">
                                    To 
                                </td>
                                <td class="chartDateBox">
                                    <asp:TextBox ID="locEnd" runat="server" CssClass="chartDateBox text small" />
                                </td>
                                <td class="chartCalBox">
                                    <asp:Image ID="locEndCal" runat="server" ImageUrl="Images/calendar.png" />
                                    <asp:CalendarExtender ID="calLocEnd" runat="server" TargetControlID="locEnd" FirstDayOfWeek="Monday" PopupButtonID="locEndCal" Format="dd/MM/yyyy" />
                                </td>
                                <td>
                                    <asp:Button ID="locGo" runat="server" CssClass="smaller" Text="go" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            <asp:Repeater ID="LocationsRpt" runat="server" DataSourceID="LocationsSDS">
                <HeaderTemplate><div class="scrollingBox text"><ol class="usersList"></HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:Label ID="Count" runat="server" Text='<%# Eval("Count") %>' style="float:right; padding-right:10px;" />
                        <asp:Label ID="Office" runat="server" Text='<%# Eval("Office") %>' />
                        <asp:Label ID="Location" runat="server" Text='<%# Eval("Location") %>' />
                    </li>
                </ItemTemplate>
                <FooterTemplate></ol></div></FooterTemplate>
            </asp:Repeater>
            </div>
            <asp:SqlDataSource ID="LocationsSDS" runat="server" 
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT     TOP (@Number) tblOffice.OfficeName AS Office, tblLocation.LocationName AS Location, COUNT(tblIncident.IncidentID) AS Count
FROM         tblIncident INNER JOIN
                      tblLocation ON tblIncident.LocationID = tblLocation.LocationID INNER JOIN
                      tblOffice ON tblIncident.OfficeID = tblOffice.OfficeID AND tblLocation.OfficeID = tblOffice.OfficeID
WHERE     (DATEDIFF(dd, @startDate, tblIncident.IncidentEntered) &lt; @DateIntervalValue) AND (DATEDIFF(dd, @startDate, tblIncident.IncidentEntered) &gt;= 0)
GROUP BY tblLocation.LocationName, tblOffice.OfficeName
ORDER BY Count DESC">
                <SelectParameters>
                    <asp:Parameter Name="Number" DbType="Int16" />
                    <asp:Parameter Name="startDate" />
                    <asp:Parameter Name="DateIntervalValue" />
                </SelectParameters>
            </asp:SqlDataSource>
                </ContentTemplate>
            </asp:UpdatePanel>
        
        </td>
    </tr>
</table>
</asp:Content>

