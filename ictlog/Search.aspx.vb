Imports AjaxControlToolkit

Partial Class Search
    Inherits System.Web.UI.Page

    Dim intRowCount As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Set values
            StartDateTextBox.Text = Date.Now.Date.AddDays(-6)
            EndDateTextBox.Text = Date.Now.Date
            OpenTicketsCPE.ClientState = "false"

            'Add any keywords in querystring to session variable
            If Not Request("Keywords") Is Nothing Then
                Session("Keywords") = Server.UrlDecode(Request("Keywords"))
                Session("SearchCriteriaPanelCollapse") = False
                StartDateTextBox.Text = String.Empty
            End If

            'Retrieve session variables into controls
            If Not Session("Keywords") Is Nothing Then
                KeywordsTextBox.Text = Session("Keywords")
            End If
            If Not Session("AssignedTo") Is Nothing Then
                AssignedToDropDownList.SelectedValue = Session("AssignedTo")
            End If
            If Not Session("Known") Is Nothing Then
                KnownCheckBox.Checked = Session("Known")
            End If
            If Not Session("StartDate") Is Nothing Then
                StartDateTextBox.Text = Session("StartDate")
            End If
            If Not Session("Keywords") Is Nothing Then
                EndDateTextBox.Text = Session("EndDate")
            End If
            If Not Session("Status") Is Nothing Then
                StatusDropDownList.SelectedValue = Session("Status")
            End If
            If Not Session("User") Is Nothing Then
                UserTextBox.Text = Session("User")
            End If
            If Not Session("Office") Is Nothing Then
                CCD1.SelectedValue = Session("Office")
            End If
            If Not Session("Location") Is Nothing Then
                CCD2.SelectedValue = Session("Location")
            End If
            If Not Session("Category") Is Nothing Then
                CategoryDropDownList.SelectedValue = Session("Category")
            End If
            If Not Session("SearchCriteriaPanelCollapse") Is Nothing Then
                CPE1.Collapsed = Session("SearchCriteriaPanelCollapse")
            End If
            If Not Session("OpenTicketsPanelCollapse") Is Nothing Then
                OpenTicketsCPE.Collapsed = Session("OpenTicketsPanelCollapse")
            End If
            If Not Session("GVPageIndex") Is Nothing Then
                GV1.PageIndex = Session("GVPageIndex")
            End If
            If Not Session("OpenGVPageIndex") Is Nothing Then
                OtherGV.PageIndex = Session("OpenGVPageIndex")
            End If
            If Not Session("OtherGVPageIndex") Is Nothing Then
                OtherGV.PageIndex = Session("OtherGVPageIndex")
            End If
        End If
        OpenGV.DataBind()
        OtherGV.DataBind()
        GV1.DataBind()


    End Sub

    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource1.Selecting
        'Default to very beginning if no value entered
        If StartDateTextBox.Text = "" Then
            e.Command.Parameters("@StartDate").Value = "10/03/2008 00:00:00"
        End If

        'To include EndDate in searching datetime field add 23:59:59.997 to the end
        e.Command.Parameters("@EndDate").Value = e.Command.Parameters("@EndDate").Value & " 23:59:59.997"

        'Keyword search - include %percent% before and after each word
        e.Command.Parameters("@Keywords").Value = "%" & Replace(Trim(e.Command.Parameters("@Keywords").Value), " ", "% %") & "%"

        If KnownCheckBox.Checked Then
            e.Command.Parameters("@KnownIssueID").Value = "[^0]"
        End If

        UserTextBox.Text = UserDropDownList.SelectedValue
        If Not CCD1.SelectedValue = "" Then
            OfficeTextBox.Text = Mid(CCD1.SelectedValue, 1, InStr(CCD1.SelectedValue, ":::") - 1)
        End If
        If Not CCD2.SelectedValue = "" Then
            LocationTextBox.Text = Mid(CCD2.SelectedValue, 1, InStr(CCD2.SelectedValue, ":::") - 1)
        End If
        e.Command.Parameters("@User").Value = UserTextBox.Text
        If Not OfficeTextBox.Text = "" Then
            e.Command.Parameters("@OfficeID").Value = CInt(OfficeTextBox.Text)
        End If
        If Not LocationTextBox.Text = "" Then
            e.Command.Parameters("@LocationID").Value = CInt(LocationTextBox.Text)
        End If
        If Page.IsPostBack Then
            'MsgBox("Clicked")
            Session("Keywords") = KeywordsTextBox.Text
            Session("AssignedTo") = AssignedToDropDownList.SelectedValue
            Session("Known") = KnownCheckBox.Checked
            Session("StartDate") = StartDateTextBox.Text
            Session("EndDate") = EndDateTextBox.Text
            Session("Status") = StatusDropDownList.SelectedValue
            Session("User") = UserTextBox.Text
            Session("Office") = CCD1.SelectedValue
            Session("Location") = CCD2.SelectedValue
            Session("Category") = CategoryDropDownList.SelectedValue

            Session("SearchCriteriaPanelCollapse") = CPE1.Collapsed
            Session("OpenTicketsPanelCollapse") = OpenTicketsCPE.Collapsed
            'MsgBox(Session("User"))
            'MsgBox(UserTextBox.Text)
        End If

    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected
        intRowCount = e.AffectedRows
    End Sub

    Protected Sub GV1_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV1.PageIndexChanged
        ' Store the new page index in the ViewState so we keep track of where the user
        ' is at between page refreshes
        ViewState("GVPageIndex") = DirectCast(sender, GridView).PageIndex
        Session("GVPageIndex") = DirectCast(sender, GridView).PageIndex

    End Sub

    Protected Sub GV1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV1.DataBinding
        ' See if we have a previous PageIndex for the GridView that needs to be set
        If ViewState("GVPageIndex") IsNot Nothing Then
            Me.GV1.PageIndex = CInt(ViewState("GVPageIndex"))
        End If

    End Sub

    Protected Sub GV1_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV1.RowCreated
        'Dynamically puts footer message into bottom right column
        If e.Row.RowType = DataControlRowType.Footer Then
            'Merge cells
            'e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells(0).ColumnSpan = 8
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Text = intRowCount & " results"
        End If
    End Sub

    Protected Sub GV1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';TwoRowHighlight_On(this);")
            e.Row.Attributes.Add("onmouseout", "TwoRowHighlight_Off(this);")

            Dim popExtender As PopupControlExtender = CType(e.Row.FindControl("PopEx"), PopupControlExtender)
            Dim lbClosePopup As LinkButton = CType(e.Row.FindControl("ClosePopupButton"), LinkButton)

            popExtender.BehaviorID = "popup" & e.Row.RowIndex
            lbClosePopup.Attributes.Add("onclick", "$find('popup" & e.Row.RowIndex & "').hidePopup(); return false;")

            Dim lbDescription As Label = CType(e.Row.FindControl("Description"), Label)
            Dim lbAction As Label = CType(e.Row.FindControl("Action"), Label)
            If lbDescription.Text = "" And lbAction.Text = "" Then
                Dim DetailImage As Image = CType(e.Row.FindControl("DetailImage"), Image)
                DetailImage.Visible = False
            End If


            Dim lblPriority As Label = CType(e.Row.FindControl("Priority"), Label)
            Dim tagLine As HtmlGenericControl = CType(e.Row.FindControl("tag"), HtmlGenericControl)
            Select Case lblPriority.Text
                Case "Low"
                    e.Row.Attributes.Add("style", "color:#666666; font-size:8pt;")
                    tagLine.Attributes.Add("class", "darkGrey smaller text padDown")
                Case "Medium"
                    e.Row.Attributes.Add("style", "font-size:small;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
                Case "High"
                    e.Row.Attributes.Add("style", "font-size:small; font-weight:bold;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
            End Select

            Dim lblUser As Label = CType(e.Row.FindControl("UserLabel"), Label)
            Dim lblAssignedTo As Label = CType(e.Row.FindControl("Assigned"), Label)
            lblUser.Text = MasterClass.ADNameSearch(lblUser.Text, "displayName")
            lblAssignedTo.Text = MasterClass.ADNameSearch(lblAssignedTo.Text, "givenName")

            Dim lblStatus As Label = CType(e.Row.FindControl("StatusLabel"), Label)
            If lblStatus.Text = "Open" Then
                e.Row.Attributes("class") = "open_bg"
            Else
                e.Row.Attributes("class") = "closed_bg"
            End If

            Dim lblUpdates As Label = CType(e.Row.FindControl("Updates"), Label)
            Dim updatesTag As HtmlGenericControl = CType(e.Row.FindControl("updatesTag"), HtmlGenericControl)
            If lblUpdates.Text = "0" Then
                updatesTag.Visible = False
            ElseIf lblUpdates.Text = "1" Then
                lblUpdates.Text = lblUpdates.Text & " comment"
            Else
                lblUpdates.Text = lblUpdates.Text & " comments"
            End If

            Dim lblEntered As Label = CType(e.Row.FindControl("Entered"), Label)
            If CDate(lblEntered.Text).Date = DateTime.Now.Date Then
                lblEntered.Text = "<span class=""red"">Today</span> " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            ElseIf DateDiff(DateInterval.Day, CDate(lblEntered.Text).Date, DateTime.Now.Date) < 7 Then
                lblEntered.Text = ShortDayOfWeek(CDate(lblEntered.Text)) & " " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            Else
                lblEntered.Text = Left(CDate(lblEntered.Text).ToString, 16)
            End If

        End If
    End Sub

    Protected Sub GV1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GV1.SelectedIndexChanged
        Response.Redirect("Incident.aspx?ID=" & GV1.SelectedDataKey.Value)
    End Sub

    Protected Overrides Sub Render(ByVal writer As System.Web.UI.HtmlTextWriter)
        If GV1.Rows.Count > 0 Then
            For Each row As GridViewRow In GV1.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim intCount As Integer = 0
                    Do Until intCount = 6 '8
                        row.Cells(intCount).Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(GV1, "Select$" & row.RowIndex, True))
                        intCount += 1
                    Loop
                    intCount = 0
                    Do Until intCount = 8
                        row.Cells(intCount).Attributes.Add("style", "border-bottom: 1px solid #b7d3ef;")
                        intCount += 1
                    Loop
                    row.Cells(0).Attributes.Remove("onclick")
                End If
            Next
        End If

        If OpenGV.Rows.Count > 0 Then
            For Each row As GridViewRow In OpenGV.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim intCount As Integer = 0
                    Do Until intCount = 6 '8
                        row.Cells(intCount).Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(OpenGV, "Select$" & row.RowIndex, True))
                        intCount += 1
                    Loop
                    intCount = 0
                    Do Until intCount = 6
                        row.Cells(intCount).Attributes.Add("style", "border-bottom: 1px solid #b7d3ef;")
                        intCount += 1
                    Loop
                    row.Cells(0).Attributes.Remove("onclick")
                End If
            Next
        End If

        If OtherGV.Rows.Count > 0 Then
            For Each row As GridViewRow In OtherGV.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    Dim intCount As Integer = 0
                    Do Until intCount = 6 '8
                        row.Cells(intCount).Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(OtherGV, "Select$" & row.RowIndex, True))
                        intCount += 1
                    Loop
                    intCount = 0
                    Do Until intCount = 6
                        row.Cells(intCount).Attributes.Add("style", "border-bottom: 1px solid #b7d3ef;")
                        intCount += 1
                    Loop
                    row.Cells(0).Attributes.Remove("onclick")
                End If
            Next
        End If

        MyBase.Render(writer)
    End Sub

    Protected Sub SubmitButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SubmitButton.Click
        Session("OpenTicketsPanelCollapse") = True
        'OpenGV.DataBind()
        'OtherGV.DataBind()
        GV1.DataBind()
    End Sub

    Protected Sub GV1_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GV1.Sorting
        If e.SortExpression = "OfficeName, LocationName" And e.SortDirection = 1 Then
            e.SortExpression = "OfficeName DESC, LocationName"
        End If
    End Sub

    Private Function ShortDayOfWeek(ByVal d As String) As String
        'Work out day of week
        Dim dt As DateTime = CDate(d)
        Try
            Return Left(dt.DayOfWeek.ToString, 3)
        Catch ex As Exception
            Return "Error: " & ex.InnerException.ToString
        End Try
    End Function

    Protected Sub ClearButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        'Clear session variables and forward
        Session("Keywords") = Nothing
        Session("AssignedTo") = Nothing
        Session("Known") = Nothing
        Session("StartDate") = Nothing
        Session("EndDate") = Nothing
        Session("Status") = Nothing
        Session("User") = Nothing
        Session("Office") = Nothing
        Session("Location") = Nothing
        Session("Category") = Nothing
        Session("SearchCriteriaPanelCollapse") = Nothing
        Session("OpenTicketsPanelCollapse") = Nothing
        Session("GVPageIndex") = Nothing
        Session("OpenGVPageIndex") = Nothing
        Session("OtherGVPageIndex") = Nothing

        Response.Redirect("Search.aspx")

    End Sub

    Protected Sub OpenSDS_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles OpenSDS.Selected
        intRowCount = e.AffectedRows
    End Sub

    Protected Sub OpenSDS_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles OpenSDS.Selecting
        'Default to very beginning if no value entered
        If StartDateTextBox.Text = "" Then
            e.Command.Parameters("@StartDate").Value = "10/03/2008 00:00:00"
        End If

        'To include EndDate in searching datetime field add 23:59:59.997 to the end
        e.Command.Parameters("@EndDate").Value = e.Command.Parameters("@EndDate").Value & " 23:59:59.997"

    End Sub

    Protected Sub OpenGV_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenGV.DataBinding
        ' See if we have a previous PageIndex for the GridView that needs to be set
        If ViewState("OpenGVPageIndex") IsNot Nothing Then
            Me.OpenGV.PageIndex = CInt(ViewState("OpenGVPageIndex"))
        End If

    End Sub

    Protected Sub OpenGV_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenGV.PageIndexChanged
        ' Store the new page index in the ViewState so we keep track of where the user
        ' is at between page refreshes
        ViewState("OpenGVPageIndex") = DirectCast(sender, GridView).PageIndex
        Session("OpenGVPageIndex") = DirectCast(sender, GridView).PageIndex
    End Sub

    Protected Sub OpenGV_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles OpenGV.RowCreated
        'Dynamically puts footer message into bottom right column
        If e.Row.RowType = DataControlRowType.Footer Then
            'Merge cells
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells(0).ColumnSpan = 5
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Text = intRowCount & " results"
        End If

    End Sub

    Protected Sub OpenGV_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles OpenGV.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';TwoRowHighlight_On(this);")
            e.Row.Attributes.Add("onmouseout", "TwoRowHighlight_Off(this);")

            Dim popExtender As PopupControlExtender = CType(e.Row.FindControl("PopEx2"), PopupControlExtender)
            Dim lbClosePopup As LinkButton = CType(e.Row.FindControl("ClosePopupButton"), LinkButton)

            popExtender.BehaviorID = "Opopup" & e.Row.RowIndex
            lbClosePopup.Attributes.Add("onclick", "$find('Opopup" & e.Row.RowIndex & "').hidePopup(); return false;")

            Dim lbDescription As Label = CType(e.Row.FindControl("Description"), Label)
            Dim lbAction As Label = CType(e.Row.FindControl("Action"), Label)
            If lbDescription.Text = "" And lbAction.Text = "" Then
                Dim DetailImage As Image = CType(e.Row.FindControl("DetailImage"), Image)
                DetailImage.Visible = False
            End If


            Dim lblPriority As Label = CType(e.Row.FindControl("Priority"), Label)
            Dim tagLine As HtmlGenericControl = CType(e.Row.FindControl("tag"), HtmlGenericControl)
            Select Case lblPriority.Text
                Case "Low"
                    e.Row.Attributes.Add("style", "color:#666666; font-size:8pt;")
                    tagLine.Attributes.Add("class", "darkGrey smaller text padDown")
                Case "Medium"
                    e.Row.Attributes.Add("style", "font-size:small;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
                Case "High"
                    e.Row.Attributes.Add("style", "font-size:small; font-weight:bold;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
            End Select

            Dim lblUser As Label = CType(e.Row.FindControl("UserLabel"), Label)
            Dim lblAssignedTo As Label = CType(e.Row.FindControl("Assigned"), Label)
            lblUser.Text = MasterClass.ADNameSearch(lblUser.Text, "displayName")
            lblAssignedTo.Text = MasterClass.ADNameSearch(lblAssignedTo.Text, "givenName")

            Dim lblStatus As Label = CType(e.Row.FindControl("StatusLabel"), Label)
            If lblStatus.Text = "Open" Then
                e.Row.Attributes("class") = "open_bg"
            Else
                e.Row.Attributes("class") = "closed_bg"
            End If

            Dim lblUpdates As Label = CType(e.Row.FindControl("Updates"), Label)
            Dim updatesTag As HtmlGenericControl = CType(e.Row.FindControl("updatesTag"), HtmlGenericControl)
            If lblUpdates.Text = "0" Then
                updatesTag.Visible = False
            ElseIf lblUpdates.Text = "1" Then
                lblUpdates.Text = lblUpdates.Text & " comment"
            Else
                lblUpdates.Text = lblUpdates.Text & " comments"
            End If

            Dim lblEntered As Label = CType(e.Row.FindControl("Entered"), Label)
            If CDate(lblEntered.Text).Date = DateTime.Now.Date Then
                lblEntered.Text = "<span class=""red"">Today</span> " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            ElseIf DateDiff(DateInterval.Day, CDate(lblEntered.Text).Date, DateTime.Now.Date) < 7 Then
                lblEntered.Text = ShortDayOfWeek(CDate(lblEntered.Text)) & " " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            Else
                lblEntered.Text = Left(CDate(lblEntered.Text).ToString, 16)
            End If

        End If

    End Sub

    Protected Sub OpenGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OpenGV.SelectedIndexChanged
        Response.Redirect("Incident.aspx?ID=" & OpenGV.SelectedDataKey.Value)

    End Sub

    Protected Sub OpenGV_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles OpenGV.Sorting
        If e.SortExpression = "OfficeName, LocationName" And e.SortDirection = 1 Then
            e.SortExpression = "OfficeName DESC, LocationName"
        End If

    End Sub

    Protected Sub OtherSDS_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles OtherSDS.Selected
        intRowCount = e.AffectedRows
    End Sub

    Protected Sub OtherSDS_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles OtherSDS.Selecting
        'Default to very beginning if no value entered
        If StartDateTextBox.Text = "" Then
            e.Command.Parameters("@StartDate").Value = "10/03/2008 00:00:00"
        End If

        'To include EndDate in searching datetime field add 23:59:59.997 to the end
        e.Command.Parameters("@EndDate").Value = e.Command.Parameters("@EndDate").Value & " 23:59:59.997"

    End Sub

    Protected Sub OtherGV_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles OtherGV.DataBinding
        ' See if we have a previous PageIndex for the GridView that needs to be set
        If ViewState("OtherGVPageIndex") IsNot Nothing Then
            Me.OtherGV.PageIndex = CInt(ViewState("OtherGVPageIndex"))
        End If

    End Sub

    Protected Sub OtherGV_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OtherGV.PageIndexChanged
        ' Store the new page index in the ViewState so we keep track of where the user
        ' is at between page refreshes
        ViewState("OtherGVPageIndex") = DirectCast(sender, GridView).PageIndex
        Session("OtherGVPageIndex") = DirectCast(sender, GridView).PageIndex
    End Sub

    Protected Sub OtherGV_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles OtherGV.RowCreated
        'Dynamically puts footer message into bottom right column
        If e.Row.RowType = DataControlRowType.Footer Then
            'Merge cells
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells.RemoveAt(0)
            e.Row.Cells(0).ColumnSpan = 5
            e.Row.Cells(0).HorizontalAlign = HorizontalAlign.Right
            e.Row.Cells(0).Text = intRowCount & " results"
        End If

    End Sub

    Protected Sub OtherGV_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles OtherGV.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';TwoRowHighlight_On(this);")
            e.Row.Attributes.Add("onmouseout", "TwoRowHighlight_Off(this);")

            Dim popExtender As PopupControlExtender = CType(e.Row.FindControl("PopEx3"), PopupControlExtender)
            Dim lbClosePopup As LinkButton = CType(e.Row.FindControl("ClosePopupButton"), LinkButton)

            popExtender.BehaviorID = "OOpopup" & e.Row.RowIndex
            lbClosePopup.Attributes.Add("onclick", "$find('OOpopup" & e.Row.RowIndex & "').hidePopup(); return false;")

            Dim lbDescription As Label = CType(e.Row.FindControl("Description"), Label)
            Dim lbAction As Label = CType(e.Row.FindControl("Action"), Label)
            If lbDescription.Text = "" And lbAction.Text = "" Then
                Dim DetailImage As Image = CType(e.Row.FindControl("DetailImage"), Image)
                DetailImage.Visible = False
            End If


            Dim lblPriority As Label = CType(e.Row.FindControl("Priority"), Label)
            Dim tagLine As HtmlGenericControl = CType(e.Row.FindControl("tag"), HtmlGenericControl)
            Select Case lblPriority.Text
                Case "Low"
                    e.Row.Attributes.Add("style", "color:#666666; font-size:8pt;")
                    tagLine.Attributes.Add("class", "darkGrey smaller text padDown")
                Case "Medium"
                    e.Row.Attributes.Add("style", "font-size:small;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
                Case "High"
                    e.Row.Attributes.Add("style", "font-size:small; font-weight:bold;")
                    tagLine.Attributes.Add("class", "darkGrey small text padDown")
            End Select

            Dim lblUser As Label = CType(e.Row.FindControl("UserLabel"), Label)
            Dim lblAssignedTo As Label = CType(e.Row.FindControl("Assigned"), Label)
            lblUser.Text = MasterClass.ADNameSearch(lblUser.Text, "displayName")
            lblAssignedTo.Text = MasterClass.ADNameSearch(lblAssignedTo.Text, "givenName")

            Dim lblStatus As Label = CType(e.Row.FindControl("StatusLabel"), Label)
            If lblStatus.Text = "Open" Then
                e.Row.Attributes("class") = "open_bg"
            Else
                e.Row.Attributes("class") = "closed_bg"
            End If

            Dim lblUpdates As Label = CType(e.Row.FindControl("Updates"), Label)
            Dim updatesTag As HtmlGenericControl = CType(e.Row.FindControl("updatesTag"), HtmlGenericControl)
            If lblUpdates.Text = "0" Then
                updatesTag.Visible = False
            ElseIf lblUpdates.Text = "1" Then
                lblUpdates.Text = lblUpdates.Text & " comment"
            Else
                lblUpdates.Text = lblUpdates.Text & " comments"
            End If

            Dim lblEntered As Label = CType(e.Row.FindControl("Entered"), Label)
            If CDate(lblEntered.Text).Date = DateTime.Now.Date Then
                lblEntered.Text = "<span class=""red"">Today</span> " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            ElseIf DateDiff(DateInterval.Day, CDate(lblEntered.Text).Date, DateTime.Now.Date) < 7 Then
                lblEntered.Text = ShortDayOfWeek(CDate(lblEntered.Text)) & " " & Left(CDate(lblEntered.Text).TimeOfDay.ToString, 5)
            Else
                lblEntered.Text = Left(CDate(lblEntered.Text).ToString, 16)
            End If

        End If

    End Sub

    Protected Sub OtherGV_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OtherGV.SelectedIndexChanged
        Response.Redirect("Incident.aspx?ID=" & OtherGV.SelectedDataKey.Value)

    End Sub

    Protected Sub OtherGV_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles OtherGV.Sorting
        If e.SortExpression = "OfficeName, LocationName" And e.SortDirection = 1 Then
            e.SortExpression = "OfficeName DESC, LocationName"
        End If

    End Sub



End Class
