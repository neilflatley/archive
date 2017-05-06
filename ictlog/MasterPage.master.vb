Imports System.IO
Imports System.Data.SqlClient

Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    'Private Shared ReadOnly REGEX_BETWEEN_TAGS As New Regex(">\s+<", RegexOptions.Compiled)
    'Private Shared ReadOnly REGEX_LINE_BREAKS As New Regex("\n\s+", RegexOptions.Compiled)
    ' ''' <summary> 
    ' ''' Initializes the <see cref="T:System.Web.UI.HtmlTextWriter"></see> object and calls on the child 
    ' ''' controls of the <see cref="T:System.Web.UI.Page"></see> to render. 
    ' ''' </summary> 
    ' ''' <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter"></see> that receives the page content.</param> 
    'Protected Overloads Overrides Sub Render(ByVal writer As HtmlTextWriter)
    '    Using htmlwriter As New HtmlTextWriter(New System.IO.StringWriter())
    '        MyBase.Render(htmlwriter)
    '        Dim html As String = htmlwriter.InnerWriter.ToString()

    '        html = REGEX_BETWEEN_TAGS.Replace(html, "> <")
    '        html = REGEX_LINE_BREAKS.Replace(html, String.Empty)

    '        writer.Write(html.Trim())
    '    End Using
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'If Not Page.IsPostBack Then
        '    'Programmatically pick a random image from the ~/Images directory
        '    HeaderImage.ImageUrl = PickImageFromDirectory("~/Images/rotator")
        'End If

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

            Today.Text = iToday & " today"
            Yesterday.Text = iYesterday & " yesterday"
            ThisWeek.Text = iThisWeek & " this week"
            LastWeek.Text = iLastWeek & " last week"
            ThisMonth.Text = iThisMonth & " this month"
            LastMonth.Text = iLastMonth & " last month"
            ThisYear.Text = iThisYear & " this year"
            LastYear.Text = iLastYear & " last year"


        End Using

    End Sub


    ''Returns the virtual path to a randomly-selected image in the specified directory
    'Private Function PickImageFromDirectory(ByVal directoryPath As String) As String
    '    Dim dirInfo As New DirectoryInfo(Server.MapPath(directoryPath))
    '    Dim fileList() As FileInfo = dirInfo.GetFiles()
    '    Dim numberOfFiles As Integer = fileList.Length

    '    'Pick a random image from the list
    '    Dim rnd As New Random
    '    Dim randomFileIndex As Integer = rnd.Next(numberOfFiles)

    '    Dim imageFileName As String = fileList(randomFileIndex).Name
    '    'If Thumbs.db chosen - pick another file
    '    Do Until imageFileName <> "Thumbs.db"
    '        randomFileIndex = rnd.Next(numberOfFiles)
    '        imageFileName = fileList(randomFileIndex).Name
    '    Loop
    '    Dim fullImageFileName As String = Path.Combine(directoryPath, imageFileName)

    '    Return fullImageFileName
    'End Function

    Protected Sub Go_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Go.Click
        If IsNumeric(Trim(JumpTo.Text)) Then
            Response.Redirect("Incident.aspx?ID=" & Trim(JumpTo.Text))
        Else
            Response.Redirect("Search.aspx?Keywords=" & Trim(JumpTo.Text))
        End If
    End Sub
End Class

