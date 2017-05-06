Imports System.DirectoryServices
Imports System.Data
Imports System.Data.SqlClient
Imports AjaxControlToolkit

Partial Class Incident
    Inherits System.Web.UI.Page
    Private strOfficeID As String
    Dim intNewIncidentID As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("Action") = "Insert" Then
            FV1.ChangeMode(FormViewMode.Insert)
        ElseIf Request("Action") = "Edit" Then
            FV1.ChangeMode(FormViewMode.Edit)
        ElseIf Request("Action") = "Copy" And Not IsPostBack Then
            FV1.ChangeMode(FormViewMode.Insert)
            Dim TitleTB As TextBox = CType(FV1.Row.FindControl("IncidentTitleTextBox"), TextBox)
            Dim CategoryDDL As DropDownList = CType(FV1.Row.FindControl("CategoryDropDownList"), DropDownList)
            Dim DescriptionTB As TextBox = CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox)
            Dim SolutionTB As TextBox = CType(FV1.Row.FindControl("IncidentSolutionTextBox"), TextBox)
            Dim TimeSpentTB As TextBox = CType(FV1.Row.FindControl("IncidentTimeSpentTextBox"), TextBox)
            Dim PriorityDDL As DropDownList = CType(FV1.Row.FindControl("PriorityDropDownList"), DropDownList)

            'Copy facility
            Dim intSourceID As Integer
            If Request("SourceID") = Nothing Then 'set default value
                intSourceID = 1
            Else
                intSourceID = Request("SourceID")
            End If
            'Manual db query
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
            Dim strQuery As String = "SELECT IncidentTitle, CategoryID, IncidentDescription, IncidentSolution, PriorityID, IncidentTimeSpent FROM tblIncident WHERE (IncidentID = " & intSourceID & ")"
            Dim cmdFetch As SqlCommand = New SqlCommand(strQuery, sqlConn)
            Dim dtrReader As SqlDataReader

            sqlConn.Open()
            dtrReader = cmdFetch.ExecuteReader()
            While dtrReader.Read()
                TitleTB.Text = dtrReader("IncidentTitle").ToString
                CategoryDDL.SelectedValue = dtrReader("CategoryID").ToString
                DescriptionTB.Text = dtrReader("IncidentDescription").ToString
                SolutionTB.Text = dtrReader("IncidentSolution").ToString
                TimeSpentTB.Text = dtrReader("IncidentTimeSpent").ToString
                PriorityDDL.SelectedValue = dtrReader("PriorityID").ToString
            End While

            Dim UserCB As ComboBox = CType(FV1.Row.FindControl("CB1"), ComboBox)
            If UserCB.SelectedValue = "Select User" Then
                UserCB.ForeColor = Drawing.Color.Red
            End If
        End If
        'If no results or FormView in Insert mode - stops error if record doesn't exist
        If (Not FV1.DataItemCount < 1 And FV1.CurrentMode = FormViewMode.Edit) Or FV1.CurrentMode = FormViewMode.Insert Then
            If Not Page.IsPostBack Then
                Dim TitleTB As TextBox = CType(FV1.Row.FindControl("IncidentTitleTextBox"), TextBox)
                Page.SetFocus(TitleTB)

            End If
        End If
    End Sub

    Protected Sub SqlDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSource1.Inserting

        'Remove any additional description added then removed
        Dim CPE1 As CollapsiblePanelExtender = CType(FV1.Row.FindControl("CPE1"), CollapsiblePanelExtender)
        If Boolean.Parse(CPE1.ClientState) Then 'if value true then panel collapsed, if false panel expanded
            e.Command.Parameters("@IncidentDescription").Value = Nothing
        Else
            If CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox).Text = "" Then
                e.Command.Parameters("@IncidentDescription").Value = Nothing
            Else
                e.Command.Parameters("@IncidentDescription").Value = Trim(CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox).Text)
            End If
        End If

    End Sub

    Protected Sub SqlDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles SqlDataSource1.Updating

        'Remove any additional description added then removed
        Dim CPE1 As CollapsiblePanelExtender = CType(FV1.Row.FindControl("CPE1"), CollapsiblePanelExtender)
        If Boolean.Parse(CPE1.ClientState) Then 'if value true then panel collapsed, if false panel expanded
            e.Command.Parameters("@IncidentDescription").Value = Nothing
        Else
            If CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox).Text = "" Then
                e.Command.Parameters("@IncidentDescription").Value = Nothing
            Else
                e.Command.Parameters("@IncidentDescription").Value = Trim(CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox).Text)
            End If
        End If

    End Sub

    Protected Sub SqlDataSource1_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Selected
    End Sub
    Protected Sub FV1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles FV1.DataBinding
    End Sub

    Protected Sub FV1_DataBound(ByVal sender As Object, ByVal e As System.EventArgs) Handles FV1.DataBound
        'If no results or FormView in Insert mode - stops error if record doesn't exist
        If Not FV1.DataItemCount < 1 Or FV1.CurrentMode = FormViewMode.Insert Then

            If FV1.CurrentMode = FormViewMode.Insert Or FV1.CurrentMode = FormViewMode.Edit Then
                'Populate hidden field with current user
                Dim UserHF As HiddenField = CType(FV1.Row.FindControl("UserHiddenField"), HiddenField)
                UserHF.Value = Session("strUser")

                'Bind the AD search DataTable to the DropDownList
                Dim UserCB As ComboBox = CType(FV1.Row.FindControl("CB1"), ComboBox)
                UserCB.DataSource = MasterClass.ActiveDirectorySearch("displayName", "Ascending")
                UserCB.DataBind()

                'Dim UserDDL As DropDownList = CType(FV1.Row.FindControl("UserDropDownList"), DropDownList)
                'UserDDL.DataSource = MasterClass.ActiveDirectorySearch("displayName", "Ascending")
                'UserDDL.DataBind()

                If FV1.CurrentMode = FormViewMode.Edit Then
                    Dim UserTB As TextBox = CType(FV1.Row.FindControl("UserTextBox"), TextBox)
                    UserCB.SelectedValue = UserTB.Text
                    'If user not found in current staff list create
                    'dummy list item to preserve user id
                    If UserCB.SelectedValue = "" Then
                        Dim cbItem As New ListItem
                        cbItem.Value = UserTB.Text
                        cbItem.Text = UserTB.Text
                        UserCB.Items.Add(cbItem)
                        UserCB.SelectedValue = UserTB.Text
                    End If

                End If

            End If

            If FV1.CurrentMode = FormViewMode.Insert Then

                Page.Title = Page.Title & ": Log an Incident"

                Dim TimeSpentTB As TextBox = CType(FV1.Row.FindControl("IncidentTimeSpentTextBox"), TextBox)
                Dim AssignedToDDL As DropDownList = CType(FV1.Row.FindControl("IncidentAssignedToDropDownList"), DropDownList)
                Dim PriorityDDL As DropDownList = CType(FV1.Row.FindControl("PriorityDropDownList"), DropDownList)
                Dim StatusDDL As DropDownList = CType(FV1.Row.FindControl("StatusDropDownList"), DropDownList)

                'Complete default values
                TimeSpentTB.Text = "2"
                AssignedToDDL.SelectedValue = Session("strUser")
                PriorityDDL.SelectedValue = 1
                StatusDDL.SelectedValue = 2


            ElseIf FV1.CurrentMode = FormViewMode.Edit Then

                Page.Title = Page.Title & ": Incident Update"

                'Load values into cascaded drop downs
                Dim OfficeTB As TextBox = CType(FV1.Row.FindControl("OfficeTextBox"), TextBox)
                Dim OfficeCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.Row.FindControl("CCD1"), AjaxControlToolkit.CascadingDropDown)
                Dim LocationTB As TextBox = CType(FV1.FindControl("LocationTextBox"), TextBox)
                Dim LocationCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.FindControl("CCD2"), AjaxControlToolkit.CascadingDropDown)

                OfficeCCD.SelectedValue = OfficeTB.Text
                LocationCCD.SelectedValue = LocationTB.Text

                'Hide Additional Details box if no value entered
                Dim CPE1 As CollapsiblePanelExtender = CType(FV1.Row.FindControl("CPE1"), CollapsiblePanelExtender)
                Dim IncidentDescriptionTB As TextBox = CType(FV1.Row.FindControl("IncidentDescriptionTextBox"), TextBox)
                If IncidentDescriptionTB.Text = "" Then
                    CPE1.Collapsed = True
                Else
                    CPE1.Collapsed = False
                End If


            ElseIf FV1.CurrentMode = FormViewMode.ReadOnly Then

                Page.Title = Page.Title & ": Incident Details"

                'Add line breaks to incident description and solution labels
                Dim lblDescription As Label = CType(FV1.Row.FindControl("Description"), Label)
                Dim lblSolution As Label = CType(FV1.Row.FindControl("Solution"), Label)
                lblDescription.Text = Replace(lblDescription.Text, vbCrLf, "<br />")
                lblSolution.Text = Replace(lblSolution.Text, vbCrLf, "<br />")

                'Hide Additional Details table if no value entered
                Dim IncidentDescription As HtmlTable = CType(FV1.Row.FindControl("IncidentDescription"), HtmlTable)
                If lblDescription.Text = "" Then
                    IncidentDescription.Visible = False
                End If

                'Hide Action/Resolution table if no value entered
                Dim IncidentAction As HtmlTable = CType(FV1.Row.FindControl("IncidentAction"), HtmlTable)
                If lblSolution.Text = "" Then
                    IncidentAction.Visible = False
                End If

                ''Hide Known Issue table row if not related
                'Dim trKnownIssue As TableRow = CType(FV1.Row.FindControl("KnownIssueTableRow"), TableRow)
                'Dim lblKnownIssueTitle As Label = CType(FV1.Row.FindControl("KnownIssueTitleLabel"), Label)
                'If lblKnownIssueTitle.Text = "" Then
                '    trKnownIssue.Visible = False
                'End If

                Dim dv As DataView
                dv = SqlDataSource1.Select(DataSourceSelectArguments.Empty)

                Dim lblAssignedTo As Label = CType(FV1.Row.FindControl("IncidentAssignedToLabel"), Label)
                Dim lblUser As Label = CType(FV1.Row.FindControl("User"), Label)
                Dim lblEnteredBy As Label = CType(FV1.Row.FindControl("IncidentEnteredByLabel"), Label)
                Dim lblClosedBy As Label = CType(FV1.Row.FindControl("IncidentClosedLabel"), Label)
                Dim lblLastUpdatedBy As Label = CType(FV1.Row.FindControl("IncidentLastUpdatedByLabel"), Label)

                'Replace username label values with AD DisplayName
                lblAssignedTo.Text = MasterClass.ADNameSearch(lblAssignedTo.Text, "givenName")
                lblUser.Text = MasterClass.ADNameSearch(lblUser.Text, "displayName")
                lblEnteredBy.Text = MasterClass.ADNameSearch(lblEnteredBy.Text, "displayName")
                lblLastUpdatedBy.Text = MasterClass.ADNameSearch(lblLastUpdatedBy.Text, "displayName")
                If Not lblClosedBy.Text = "" Then 'only if incident is closed
                    'If incident closed at time of call
                    If dv.Table.Rows(0).Item("IncidentClosed") = dv.Table.Rows(0).Item("IncidentEntered") Then
                        lblClosedBy.Text = "and was closed at time of call"
                    Else
                        'Hides ClosedBy if same as person who entered incident
                        If dv.Table.Rows(0).Item("IncidentClosedBy") = dv.Table.Rows(0).Item("IncidentEnteredBy") Then
                            lblClosedBy.Text = "and was closed on " & lblClosedBy.Text
                        Else
                            lblClosedBy.Text = "and was closed on " & lblClosedBy.Text & " by " & MasterClass.ADNameSearch(dv.Table.Rows(0).Item("IncidentClosedBy"), "displayName")
                        End If
                    End If
                End If

                'Hide Last Updated table row if incident last modified at time of call
                Dim tcLastUpdated As TableCell = CType(FV1.Row.FindControl("LastUpdatedTableCell"), TableCell)
                Dim strLastUpdated As String = dv.Table.Rows(0).Item("IncidentLastUpdated").ToString
                Dim strClosed As String = dv.Table.Rows(0).Item("IncidentClosed").ToString
                If dv.Table.Rows(0).Item("IncidentLastUpdated") = dv.Table.Rows(0).Item("IncidentEntered") Then
                    tcLastUpdated.Text = ""
                ElseIf Not IsDBNull(dv.Table.Rows(0).Item("IncidentClosed")) Then
                    If DateTime.Compare(strLastUpdated, strClosed) = 0 Then
                        tcLastUpdated.Text = ""
                    End If
                End If

                Dim lblStatus As Label = CType(FV1.Row.FindControl("Status"), Label)
                Dim openLabel As HtmlGenericControl = CType(FV1.Row.FindControl("openLabel"), HtmlGenericControl)
                If lblStatus.Text = "Open" Then
                    openLabel.Visible = True
                Else
                    openLabel.Visible = False
                End If
            End If

        End If

    End Sub

    Protected Sub FV1_ItemInserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertEventArgs) Handles FV1.ItemInserting
        Dim tb As TextBox
        Dim ddl As DropDownList
        Dim UserCB As ComboBox = CType(FV1.Row.FindControl("CB1"), ComboBox)

        tb = CType(FV1.Row.FindControl("OfficeTextBox"), TextBox)
        ddl = CType(FV1.Row.FindControl("OfficeDropDownList"), DropDownList)
        tb.Text = ddl.SelectedValue
        e.Values("OfficeID") = tb.Text

        tb = CType(FV1.Row.FindControl("LocationTextBox"), TextBox)
        ddl = CType(FV1.Row.FindControl("LocationDropDownList"), DropDownList)
        tb.Text = ddl.SelectedValue
        e.Values("LocationID") = tb.Text

        tb = CType(FV1.Row.FindControl("UserTextBox"), TextBox)
        tb.Text = UserCB.SelectedValue
        e.Values("IncidentUser") = tb.Text

        Dim ddlStatus As DropDownList = CType(FV1.Row.FindControl("StatusDropDownList"), DropDownList)
        If ddlStatus.SelectedValue = 2 Then
            SqlDataSource1.InsertParameters("IncidentClosed").DefaultValue = DateTime.Now
            SqlDataSource1.InsertParameters("IncidentClosedBy").DefaultValue = Session("strUser")
        End If


        Dim IncidentEntered As TextBox = CType(FV1.Row.FindControl("IncidentEntered"), TextBox) ' date
        Dim IncidentEntered2 As TextBox = CType(FV1.Row.FindControl("IncidentEntered2"), TextBox) ' time

        If IncidentEntered.Text = "" Then
            SqlDataSource1.InsertParameters("IncidentEntered").DefaultValue = DateTime.Now
        Else
            If IncidentEntered2.Text = "" Then
                SqlDataSource1.InsertParameters("IncidentEntered").DefaultValue = CDate(IncidentEntered.Text) & " 00:00:00"
            Else
                SqlDataSource1.InsertParameters("IncidentEntered").DefaultValue = CDate(IncidentEntered.Text) & " " & IncidentEntered2.Text
            End If
        End If

        SqlDataSource1.InsertParameters("IncidentLastUpdated").DefaultValue = DateTime.Now


    End Sub

    Protected Sub FV1_ItemInserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewInsertedEventArgs) Handles FV1.ItemInserted
        If e.Exception Is Nothing Then
            Response.Redirect("Incident.aspx?ID=" & intNewIncidentID)
        End If
    End Sub

    Protected Sub SqlDataSource1_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource1.Inserted
        'Retrieve just inserted incident
        If e.Exception Is Nothing Then
            intNewIncidentID = e.Command.Parameters("@IncidentID").Value
        End If
    End Sub

    Protected Sub FV1_ItemUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdateEventArgs) Handles FV1.ItemUpdating
        Dim tb As TextBox
        Dim ddl As DropDownList
        Dim UserCB As ComboBox = CType(FV1.Row.FindControl("CB1"), ComboBox)

        tb = CType(FV1.Row.FindControl("OfficeTextBox"), TextBox)
        ddl = CType(FV1.Row.FindControl("OfficeDropDownList"), DropDownList)
        tb.Text = ddl.SelectedValue
        e.NewValues("OfficeID") = tb.Text

        tb = CType(FV1.Row.FindControl("LocationTextBox"), TextBox)
        ddl = CType(FV1.Row.FindControl("LocationDropDownList"), DropDownList)
        tb.Text = ddl.SelectedValue
        e.NewValues("LocationID") = tb.Text

        tb = CType(FV1.Row.FindControl("UserTextBox"), TextBox)
        tb.Text = UserCB.SelectedValue
        e.NewValues("IncidentUser") = tb.Text

        Dim ddlStatus As DropDownList = CType(FV1.Row.FindControl("StatusDropDownList"), DropDownList)
        Dim tbIncidentClosed As TextBox = CType(FV1.Row.FindControl("IncidentClosedTextBox"), TextBox)
        Dim tbIncidentClosedBy As TextBox = CType(FV1.Row.FindControl("IncidentClosedByTextBox"), TextBox)
        Dim tbOriginalStatusID As TextBox = CType(FV1.Row.FindControl("StatusIDTextBox"), TextBox)
        If (ddlStatus.SelectedValue = 2 And tbOriginalStatusID.Text = "1") Then
            e.NewValues("IncidentClosed") = DateTime.Now
            e.NewValues("IncidentClosedBy") = Session("strUser")
        End If
        SqlDataSource1.UpdateParameters("IncidentLastUpdated").DefaultValue = DateTime.Now

    End Sub

    Protected Sub FV1_ItemDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewDeletedEventArgs) Handles FV1.ItemDeleted
        If e.Exception Is Nothing Then
            Response.Redirect("Search.aspx")
        End If
    End Sub

    Protected Sub UserDropDownList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim UserDDL As DropDownList = CType(FV1.Row.FindControl("UserDropDownList"), DropDownList)

        Dim Entry As New DirectoryEntry("LDAP://DC=cwmgp,DC=co,DC=uk")
        Dim Searcher As New DirectorySearcher(Entry)
        Dim AdObj As SearchResult

        Searcher.PropertiesToLoad.Add("physicalDeliveryOfficeName")
        Searcher.Filter = "(&(sAMAccountName=" & UserDDL.SelectedValue & ")(ObjectCategory=Person)(ObjectClass=User)(sn=*)(!description=hide))"
        AdObj = Searcher.FindOne()

        If Not AdObj.Properties("physicalDeliveryOfficeName").Count = 0 Then
            Dim strSelectedUserOffice As String
            strSelectedUserOffice = AdObj.Properties("physicalDeliveryOfficeName").Item(0)
            If Not InStr(strSelectedUserOffice, " ") = 0 Then
                strSelectedUserOffice = Trim(Left(strSelectedUserOffice, InStr(strSelectedUserOffice, " ")))
            End If

            'Find office ID from office name
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
            Dim strOfficesQuery As String = "SELECT OfficeID FROM tblOffice WHERE OfficeName='" & strSelectedUserOffice & "'"
            Dim cmdFetchOffices As SqlCommand = New SqlCommand(strOfficesQuery, sqlConn)
            Dim dtrOffices As SqlDataReader

            sqlConn.Open()
            dtrOffices = cmdFetchOffices.ExecuteReader()
            While dtrOffices.Read()
                strOfficeID = dtrOffices("OfficeID").ToString

            End While

            Dim OfficeCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.Row.FindControl("CCD1"), AjaxControlToolkit.CascadingDropDown)
            Dim LocationCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.Row.FindControl("CCD2"), AjaxControlToolkit.CascadingDropDown)
            If Not strOfficeID = Nothing Then
                OfficeCCD.SelectedValue = strOfficeID
                Select Case strOfficeID
                    Case "1"
                        LocationCCD.SelectedValue = "102"
                    Case "2"
                        LocationCCD.SelectedValue = "103"
                    Case "3"
                        LocationCCD.SelectedValue = "104"
                    Case "4"
                        LocationCCD.SelectedValue = "105"
                    Case "5"
                        LocationCCD.SelectedValue = "106"
                    Case "6"
                        LocationCCD.SelectedValue = "107"
                    Case "7"
                        LocationCCD.SelectedValue = "108"
                    Case "8"
                        LocationCCD.SelectedValue = "109"
                    Case "9"
                        LocationCCD.SelectedValue = "110"
                    Case "10"
                        LocationCCD.SelectedValue = "111"
                    Case "11"
                        LocationCCD.SelectedValue = "112"
                End Select
            End If

            UserDDL.ForeColor = Drawing.Color.Black 'for copy function ddl is initially red

            'Remove "Select User" option when an AD option has been picked to prevent crash
            UserDDL.Items.RemoveAt(0)
        End If

    End Sub

    Protected Sub UpdateCancelImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        CancelActions()
    End Sub

    Protected Sub UpdateCancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CancelActions()
    End Sub

    Protected Sub InsertCancelButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        CancelActions()
    End Sub

    Protected Sub InsertCancelImage_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        CancelActions()
    End Sub

    Protected Sub CancelActions()
        If Request("Action") = "Copy" Or Request("ID") = Nothing Then
            Response.Redirect("Search.aspx")
        End If

        If Request("Action") = "Edit" And Not Request("ID") = Nothing Then
            Response.Redirect("Incident.aspx?ID=" & Request("ID"))
        End If
    End Sub

    Protected Sub GV1_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
        Dim sqlIncidentUpdateDS As SqlDataSource = CType(FV1.Row.FindControl("IncidentUpdateSqlDataSource"), SqlDataSource)

        sqlIncidentUpdateDS.UpdateParameters("NotesLastUpdated").DefaultValue = DateTime.Now

    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim sqlIncidentUpdateDS As SqlDataSource = CType(FV1.Row.FindControl("IncidentUpdateSqlDataSource"), SqlDataSource)

        Dim GV1 As GridView = CType(FV1.Row.FindControl("GV1"), GridView)
        Dim strDescription As String = CType(GV1.FooterRow.FindControl("DescriptionTextBox"), TextBox).Text

        sqlIncidentUpdateDS.InsertParameters("NotesDescription").DefaultValue = strDescription
        sqlIncidentUpdateDS.InsertParameters("NotesLastUpdated").DefaultValue = DateTime.Now
        sqlIncidentUpdateDS.InsertParameters("NotesLastUpdatedBy").DefaultValue = Session("strUser")

        sqlIncidentUpdateDS.Insert()

    End Sub
    Protected Sub IncidentUpdateInsert(ByVal strDescription As String)
        Dim sqlIncidentUpdateDS As SqlDataSource = CType(FV1.Row.FindControl("IncidentUpdateSqlDataSource"), SqlDataSource)
        sqlIncidentUpdateDS.InsertParameters("NotesDescription").DefaultValue = strDescription
        sqlIncidentUpdateDS.InsertParameters("NotesLastUpdated").DefaultValue = DateTime.Now
        sqlIncidentUpdateDS.InsertParameters("NotesLastUpdatedBy").DefaultValue = Session("strUser")
        sqlIncidentUpdateDS.Insert()

    End Sub

    Protected Sub EmptyDataImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim GV1 As GridView = CType(FV1.Row.FindControl("GV1"), GridView)
        Dim txtDescription As TextBox = CType(sender, ImageButton).NamingContainer.FindControl("DescriptionTextBox")

        IncidentUpdateInsert(txtDescription.Text)
    End Sub

    Protected Sub FooterImageButton_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim GV1 As GridView = CType(FV1.Row.FindControl("GV1"), GridView)
        Dim strDescription As String = CType(GV1.FooterRow.FindControl("DescriptionTextBox"), TextBox).Text

        IncidentUpdateInsert(strDescription)
    End Sub

    Protected Sub GV1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate Then
                Dim LastUpdatedByLabel As Label = CType(e.Row.FindControl("LastUpdatedByLabel"), Label)
                LastUpdatedByLabel.Text = MasterClass.ADNameSearch(LastUpdatedByLabel.Text, "displayName")
            End If
            Dim AddUpdateTable As HtmlTable = CType(FV1.Row.FindControl("AddUpdateTable"), HtmlTable)
            AddUpdateTable.Visible = False
        ElseIf e.Row.RowType = DataControlRowType.EmptyDataRow Then
        End If

    End Sub

    Protected Sub LocationDropDownList_DataBound(ByVal sender As Object, ByVal e As System.EventArgs)
        LocationDropDownList.Focus()

    End Sub

    Protected Sub CB1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CB1.SelectedIndexChanged
        Dim UserCB As ComboBox = CType(FV1.Row.FindControl("CB1"), ComboBox)

        Dim Entry As New DirectoryEntry("LDAP://DC=cwmgp,DC=co,DC=uk")
        Dim Searcher As New DirectorySearcher(Entry)
        Dim AdObj As SearchResult

        Searcher.PropertiesToLoad.Add("physicalDeliveryOfficeName")
        Searcher.Filter = "(&(sAMAccountName=" & UserCB.SelectedValue & ")(ObjectCategory=Person)(ObjectClass=User)(sn=*)(!description=hide))"
        AdObj = Searcher.FindOne()

        If Not AdObj.Properties("physicalDeliveryOfficeName").Count = 0 Then
            Dim strSelectedUserOffice As String
            strSelectedUserOffice = AdObj.Properties("physicalDeliveryOfficeName").Item(0)
            If Not InStr(strSelectedUserOffice, " ") = 0 Then
                strSelectedUserOffice = Trim(Left(strSelectedUserOffice, InStr(strSelectedUserOffice, " ")))
            End If

            'Find office ID from office name
            Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
            Dim strOfficesQuery As String = "SELECT OfficeID FROM tblOffice WHERE OfficeName='" & strSelectedUserOffice & "'"
            Dim cmdFetchOffices As SqlCommand = New SqlCommand(strOfficesQuery, sqlConn)
            Dim dtrOffices As SqlDataReader

            sqlConn.Open()
            dtrOffices = cmdFetchOffices.ExecuteReader()
            While dtrOffices.Read()
                strOfficeID = dtrOffices("OfficeID").ToString

            End While

            Dim OfficeCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.Row.FindControl("CCD1"), AjaxControlToolkit.CascadingDropDown)
            Dim LocationCCD As AjaxControlToolkit.CascadingDropDown = CType(FV1.Row.FindControl("CCD2"), AjaxControlToolkit.CascadingDropDown)
            If Not strOfficeID = Nothing Then
                OfficeCCD.SelectedValue = strOfficeID
                Select Case strOfficeID
                    Case "1"
                        LocationCCD.SelectedValue = "102"
                    Case "2"
                        LocationCCD.SelectedValue = "103"
                    Case "3"
                        LocationCCD.SelectedValue = "104"
                    Case "4"
                        LocationCCD.SelectedValue = "105"
                    Case "5"
                        LocationCCD.SelectedValue = "106"
                    Case "6"
                        LocationCCD.SelectedValue = "107"
                    Case "7"
                        LocationCCD.SelectedValue = "108"
                    Case "8"
                        LocationCCD.SelectedValue = "109"
                    Case "9"
                        LocationCCD.SelectedValue = "110"
                    Case "10"
                        LocationCCD.SelectedValue = "111"
                    Case "11"
                        LocationCCD.SelectedValue = "112"
                End Select
            End If

            UserCB.ForeColor = Drawing.Color.Black 'for copy function ddl is initially red

            'Remove "Select User" option when an AD option has been picked to prevent crash
            UserCB.Items.RemoveAt(0)
        End If

    End Sub

    Protected Sub FV1_ItemUpdated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.FormViewUpdatedEventArgs) Handles FV1.ItemUpdated

        If Request("Action") = "Edit" And Not Request("ID") = Nothing Then
            Response.Redirect("Incident.aspx?ID=" & Request("ID"))
        End If

    End Sub

End Class
