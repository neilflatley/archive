Imports System.Data.SqlClient

Partial Class Charts
    Inherits System.Web.UI.Page
    Dim startDateToday As Date = CStr(Date.Now.Date & " 00:00:00")
    Dim dateIntervalToday As Integer = 1
    Dim startDate7day As Date = CStr(Date.Now.Date.AddDays(-7) & " 00:00:00")
    Dim dateInterval7day As Integer = 7
    Dim startDate30day As Date = CStr(Date.Now.Date.AddDays(-30) & " 00:00:00")
    Dim dateInterval30day As Integer = 30
    Dim startDate90day As Date = CStr(Date.Now.Date.AddDays(-90) & " 00:00:00")
    Dim dateInterval90day As Integer = 90
    Dim startDateTotal As Date = CDate("01/01/2008 00:00:00")
    Dim dateIntervalTotal As Integer = 3650

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Bind the statistics box
            BindStatistics()

            'Feed params for Category chart
            CategorySDS.SelectParameters("startDate").DefaultValue = startDateToday
            CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
            catToday.Font.Bold = True
            CategoryChart.DataBind()

            'Feed params for Priority chart
            PrioritySDS.SelectParameters("startDate").DefaultValue = startDateToday
            PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
            priorToday.Font.Bold = True
            PriorityChart.DataBind()

            'Feed params for Assigned chart
            AssignedSDS.SelectParameters("startDate").DefaultValue = startDateToday
            AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
            assToday.Font.Bold = True
            AssignedChart.DataBind()



            'Feed params for Users scoreboard
            UsersSDS.SelectParameters("startDate").DefaultValue = startDateToday
            UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
            UsersSDS.SelectParameters("Number").DefaultValue = 10
            usersToday.Font.Bold = True
            UsersRpt.DataBind()

            'Feed params for Locations scoreboard
            LocationsSDS.SelectParameters("startDate").DefaultValue = startDateToday
            LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
            LocationsSDS.SelectParameters("Number").DefaultValue = 10
            locToday.Font.Bold = True
            LocationsRpt.DataBind()

        End If

    End Sub

    Protected Sub catGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles catGo.Click
        'Unbold upper buttons
        catToday.Font.Bold = False
        cat7day.Font.Bold = False
        cat30day.Font.Bold = False
        cat90day.Font.Bold = False
        catTotal.Font.Bold = False

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = catStart.Text
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = DateDiff(DateInterval.Day, CDate(catStart.Text), CDate(catEnd.Text)) + 1
        CategoryChart.DataBind()

    End Sub

    Protected Sub catToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles catToday.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        cat7day.Font.Bold = False
        cat30day.Font.Bold = False
        cat90day.Font.Bold = False
        catTotal.Font.Bold = False
        catStart.Text = ""
        catEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = startDateToday
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
        CategoryChart.DataBind()
    End Sub

    Protected Sub cat7day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cat7day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        catToday.Font.Bold = False
        cat30day.Font.Bold = False
        cat90day.Font.Bold = False
        catTotal.Font.Bold = False
        catStart.Text = ""
        catEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = startDate7day
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval7day
        CategoryChart.DataBind()

    End Sub

    Protected Sub cat30day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cat30day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        catToday.Font.Bold = False
        cat7day.Font.Bold = False
        cat90day.Font.Bold = False
        catTotal.Font.Bold = False
        catStart.Text = ""
        catEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = startDate30day
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval30day
        CategoryChart.DataBind()
    End Sub

    Protected Sub cat90day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cat90day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        catToday.Font.Bold = False
        cat7day.Font.Bold = False
        cat30day.Font.Bold = False
        catTotal.Font.Bold = False
        catStart.Text = ""
        catEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = startDate90day
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval90day
        CategoryChart.DataBind()

    End Sub

    Protected Sub catTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles catTotal.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        catToday.Font.Bold = False
        cat7day.Font.Bold = False
        cat30day.Font.Bold = False
        cat90day.Font.Bold = False
        catStart.Text = ""
        catEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        CategorySDS.SelectParameters("startDate").DefaultValue = startDateTotal
        CategorySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalTotal
        CategoryChart.DataBind()

    End Sub

    Protected Sub priorToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles priorToday.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        prior7day.Font.Bold = False
        prior30day.Font.Bold = False
        prior90day.Font.Bold = False
        priorTotal.Font.Bold = False
        priorStart.Text = ""
        priorEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = startDateToday
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
        PriorityChart.DataBind()

    End Sub

    Protected Sub prior7day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles prior7day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        priorToday.Font.Bold = False
        prior30day.Font.Bold = False
        prior90day.Font.Bold = False
        priorTotal.Font.Bold = False
        priorStart.Text = ""
        priorEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = startDate7day
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval7day
        PriorityChart.DataBind()

    End Sub

    Protected Sub prior30day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles prior30day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        priorToday.Font.Bold = False
        prior7day.Font.Bold = False
        prior90day.Font.Bold = False
        priorTotal.Font.Bold = False
        priorStart.Text = ""
        priorEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = startDate30day
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval30day
        PriorityChart.DataBind()
    End Sub

    Protected Sub prior90day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles prior90day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        priorToday.Font.Bold = False
        prior7day.Font.Bold = False
        prior30day.Font.Bold = False
        priorTotal.Font.Bold = False
        priorStart.Text = ""
        priorEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = startDate90day
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval90day
        PriorityChart.DataBind()
    End Sub

    Protected Sub priorTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles priorTotal.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        priorToday.Font.Bold = False
        prior7day.Font.Bold = False
        prior30day.Font.Bold = False
        prior90day.Font.Bold = False
        priorStart.Text = ""
        priorEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = startDateTotal
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalTotal
        PriorityChart.DataBind()
    End Sub

    Protected Sub priorGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles priorGo.Click
        'Unbold upper buttons
        priorToday.Font.Bold = False
        prior7day.Font.Bold = False
        prior30day.Font.Bold = False
        prior90day.Font.Bold = False
        priorTotal.Font.Bold = False

        'Feed params to SqlDataSource & bind chart
        PrioritySDS.SelectParameters("startDate").DefaultValue = priorStart.Text
        PrioritySDS.SelectParameters("DateIntervalValue").DefaultValue = DateDiff(DateInterval.Day, CDate(priorStart.Text), CDate(priorEnd.Text)) + 1
        PriorityChart.DataBind()


    End Sub

    Protected Sub assToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles assToday.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        ass7day.Font.Bold = False
        ass30day.Font.Bold = False
        ass90day.Font.Bold = False
        assTotal.Font.Bold = False
        assStart.Text = ""
        assEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = startDateToday
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
        AssignedChart.DataBind()
    End Sub

    Protected Sub ass7day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ass7day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        assToday.Font.Bold = False
        ass30day.Font.Bold = False
        ass90day.Font.Bold = False
        assTotal.Font.Bold = False
        assStart.Text = ""
        assEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = startDate7day
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval7day
        AssignedChart.DataBind()
    End Sub

    Protected Sub ass30day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ass30day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        assToday.Font.Bold = False
        ass7day.Font.Bold = False
        ass90day.Font.Bold = False
        assTotal.Font.Bold = False
        assStart.Text = ""
        assEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = startDate30day
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval30day
        AssignedChart.DataBind()
    End Sub

    Protected Sub ass90day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ass90day.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        assToday.Font.Bold = False
        ass7day.Font.Bold = False
        ass30day.Font.Bold = False
        assTotal.Font.Bold = False
        assStart.Text = ""
        assEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = startDate90day
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval90day
        AssignedChart.DataBind()
    End Sub

    Protected Sub assTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles assTotal.Click
        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Unbold other buttons & clear text boxes
        assToday.Font.Bold = False
        ass7day.Font.Bold = False
        ass30day.Font.Bold = False
        ass90day.Font.Bold = False
        assStart.Text = ""
        assEnd.Text = ""

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = startDateTotal
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalTotal
        AssignedChart.DataBind()
    End Sub

    Protected Sub assGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles assGo.Click
        'Unbold upper buttons
        assToday.Font.Bold = False
        ass7day.Font.Bold = False
        ass30day.Font.Bold = False
        ass90day.Font.Bold = False
        assTotal.Font.Bold = False

        'Feed params to SqlDataSource & bind chart
        AssignedSDS.SelectParameters("startDate").DefaultValue = assStart.Text
        AssignedSDS.SelectParameters("DateIntervalValue").DefaultValue = DateDiff(DateInterval.Day, CDate(assStart.Text), CDate(assEnd.Text)) + 1
        AssignedChart.DataBind()

    End Sub

    Protected Sub usersToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles usersToday.Click
        'Unbold other buttons & clear text boxes
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False
        usersStart.Text = ""
        usersEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = startDateToday
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
        UsersRpt.DataBind()
    End Sub

    Protected Sub users7day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles users7day.Click
        'Unbold other buttons & clear text boxes
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False
        usersStart.Text = ""
        usersEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = startDate7day
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval7day
        UsersRpt.DataBind()
    End Sub

    Protected Sub users30day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles users30day.Click
        'Unbold other buttons & clear text boxes
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False
        usersStart.Text = ""
        usersEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = startDate30day
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval30day
        UsersRpt.DataBind()
    End Sub

    Protected Sub users90day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles users90day.Click
        'Unbold other buttons & clear text boxes
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False
        usersStart.Text = ""
        usersEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = startDate90day
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval90day
        UsersRpt.DataBind()
    End Sub

    Protected Sub usersTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles usersTotal.Click
        'Unbold other buttons & clear text boxes
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False
        usersStart.Text = ""
        usersEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = startDateTotal
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalTotal
        UsersRpt.DataBind()
    End Sub

    Protected Sub usersGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles usersGo.Click
        'Unbold upper buttons
        usersToday.Font.Bold = False
        users7day.Font.Bold = False
        users30day.Font.Bold = False
        users90day.Font.Bold = False
        usersTotal.Font.Bold = False

        'Feed params to SqlDataSource & bind chart
        UsersSDS.SelectParameters("startDate").DefaultValue = usersStart.Text
        UsersSDS.SelectParameters("DateIntervalValue").DefaultValue = DateDiff(DateInterval.Day, CDate(usersStart.Text), CDate(usersEnd.Text)) + 1
        UsersRpt.DataBind()

    End Sub

    Protected Sub NumberDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumberDDL.SelectedIndexChanged
        UsersSDS.SelectParameters("Number").DefaultValue = NumberDDL.SelectedValue
        UsersRpt.DataBind()
    End Sub

    Protected Sub locToday_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles locToday.Click
        'Unbold other buttons & clear text boxes
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False
        locStart.Text = ""
        locEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = startDateToday
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalToday
        LocationsRpt.DataBind()
    End Sub

    Protected Sub loc7day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loc7day.Click
        'Unbold other buttons & clear text boxes
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False
        locStart.Text = ""
        locEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = startDate7day
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval7day
        LocationsRpt.DataBind()

    End Sub

    Protected Sub loc30day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loc30day.Click
        'Unbold other buttons & clear text boxes
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False
        locStart.Text = ""
        locEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = startDate30day
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval30day
        LocationsRpt.DataBind()
    End Sub

    Protected Sub loc90day_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles loc90day.Click
        'Unbold other buttons & clear text boxes
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False
        locStart.Text = ""
        locEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = startDate90day
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateInterval90day
        LocationsRpt.DataBind()
    End Sub

    Protected Sub locTotal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles locTotal.Click
        'Unbold other buttons & clear text boxes
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False
        locStart.Text = ""
        locEnd.Text = ""

        'Bold current button
        CType(sender, LinkButton).Font.Bold = True

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = startDateTotal
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = dateIntervalTotal
        LocationsRpt.DataBind()
    End Sub

    Protected Sub locGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles locGo.Click
        'Unbold upper buttons
        locToday.Font.Bold = False
        loc7day.Font.Bold = False
        loc30day.Font.Bold = False
        loc90day.Font.Bold = False
        locTotal.Font.Bold = False

        'Feed params to SqlDataSource & bind chart
        LocationsSDS.SelectParameters("startDate").DefaultValue = locStart.Text
        LocationsSDS.SelectParameters("DateIntervalValue").DefaultValue = DateDiff(DateInterval.Day, CDate(locStart.Text), CDate(locEnd.Text)) + 1
        LocationsRpt.DataBind()
    End Sub

    Protected Sub LocationsDDL_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LocationsDDL.SelectedIndexChanged
        LocationsSDS.SelectParameters("Number").DefaultValue = LocationsDDL.SelectedValue
        LocationsRpt.DataBind()
    End Sub

    Protected Sub BindStatistics()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Using sqlConn As New SqlConnection(strConnection)

            'Find user's area id from UserArea table
            Dim strQuery As String = "SELECT " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_4 " & _
                            "WHERE      (IncidentEntered BETWEEN @YesterdayStart AND @YesterdayEnd)) AS Yesterday, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident " & _
                            "WHERE      (IncidentEntered BETWEEN @TodayStart AND @EndDate)) AS Today, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_1 " & _
                            "WHERE      (IncidentEntered BETWEEN @LastWeekStart AND @LastWeekEnd)) AS LastWeek, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_1 " & _
                            "WHERE      (IncidentEntered BETWEEN @WeekStart AND @EndDate)) AS ThisWeek, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_3 " & _
                            "WHERE      (IncidentEntered BETWEEN @LastMonthStart AND @LastMonthEnd)) AS LastMonth, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_3 " & _
                            "WHERE      (IncidentEntered BETWEEN @MonthStart AND @EndDate)) AS ThisMonth, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_2 " & _
                            "WHERE      (IncidentEntered BETWEEN @LastYearStart AND @LastYearEnd)) AS LastYear, " & _
                          "(SELECT     COUNT(IncidentID) AS Expr1 " & _
                            "FROM          tblIncident AS tblIncident_2 " & _
                            "WHERE      (IncidentEntered BETWEEN @YearStart AND @EndDate)) AS ThisYear"
            Dim dtrReader As SqlDataReader
            Dim iYesterday, iToday, iLastWeek, iThisWeek, iLastMonth, iThisMonth, iLastYear, iThisYear As Integer
            Dim YesterdayStart, YesterdayEnd, LastWeekStart, LastWeekEnd, LastMonthStart, LastMonthEnd, LastYearStart, LastYearEnd As DateTime
            Dim TodayStart, ThisWeekStart, ThisMonthStart, ThisYearStart, EndDate As DateTime

            TodayStart = Date.Now.Date
            ThisWeekStart = MasterClass.getThisMonday(Date.Now.Date)
            ThisMonthStart = "01/" & Date.Now.Month.ToString & "/" & Date.Now.Year.ToString
            ThisYearStart = "01/01/" & Date.Now.Year.ToString
            EndDate = Date.Now.Date.AddDays(1)

            YesterdayStart = Date.Now.Date.AddDays(-1)
            YesterdayEnd = Date.Now.Date
            LastWeekStart = MasterClass.getLastMonday(Date.Now.Date)
            LastWeekEnd = MasterClass.getThisMonday(Date.Now.Date)
            LastMonthStart = ThisMonthStart.AddMonths(-1)
            LastMonthEnd = ThisMonthStart
            LastYearStart = "01/01/" & Date.Now.AddYears(-1).Year.ToString
            LastYearEnd = "01/01/" & Date.Now.Year.ToString


            Dim cmdFetch As SqlCommand = New SqlCommand(strQuery, sqlConn)
            cmdFetch.Parameters.Add("@YesterdayStart", Data.SqlDbType.DateTime).Value = YesterdayStart
            cmdFetch.Parameters.Add("@YesterdayEnd", Data.SqlDbType.DateTime).Value = YesterdayEnd
            cmdFetch.Parameters.Add("@LastWeekStart", Data.SqlDbType.DateTime).Value = LastWeekStart
            cmdFetch.Parameters.Add("@LastWeekEnd", Data.SqlDbType.DateTime).Value = LastWeekEnd
            cmdFetch.Parameters.Add("@LastMonthStart", Data.SqlDbType.DateTime).Value = LastMonthStart
            cmdFetch.Parameters.Add("@LastMonthEnd", Data.SqlDbType.DateTime).Value = LastMonthEnd
            cmdFetch.Parameters.Add("@LastYearStart", Data.SqlDbType.DateTime).Value = LastYearStart
            cmdFetch.Parameters.Add("@LastYearEnd", Data.SqlDbType.DateTime).Value = LastYearEnd
            cmdFetch.Parameters.Add("@TodayStart", Data.SqlDbType.DateTime).Value = TodayStart
            cmdFetch.Parameters.Add("@WeekStart", Data.SqlDbType.DateTime).Value = ThisWeekStart
            cmdFetch.Parameters.Add("@MonthStart", Data.SqlDbType.DateTime).Value = ThisMonthStart
            cmdFetch.Parameters.Add("@YearStart", Data.SqlDbType.DateTime).Value = ThisYearStart
            cmdFetch.Parameters.Add("@EndDate", Data.SqlDbType.DateTime).Value = EndDate

            sqlConn.Open()
            dtrReader = cmdFetch.ExecuteReader
            While dtrReader.Read()
                iYesterday = CInt(dtrReader("Yesterday").ToString)
                iToday = CInt(dtrReader("Today").ToString)
                iLastWeek = CInt(dtrReader("LastWeek").ToString)
                iThisWeek = CInt(dtrReader("ThisWeek").ToString)
                iLastMonth = CInt(dtrReader("LastMonth").ToString)
                iThisMonth = CInt(dtrReader("ThisMonth").ToString)
                iLastYear = CInt(dtrReader("LastYear").ToString)
                iThisYear = CInt(dtrReader("ThisYear").ToString)
            End While

            sqlConn.Close()

            'Today.Text = iToday & " today"
            'Yesterday.Text = iYesterday & " yesterday"
            'ThisWeek.Text = iThisWeek & " this week"
            'LastWeek.Text = iLastWeek & " last week"
            'ThisMonth.Text = iThisMonth & " this month"
            'LastMonth.Text = iLastMonth & " last month"
            'ThisYear.Text = iThisYear & " this year"
            'LastYear.Text = iLastYear & " last year"


        End Using


    End Sub
End Class
