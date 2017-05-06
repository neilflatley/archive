Imports System.Data.SqlClient

Partial Class CategoryAdmin
    Inherits System.Web.UI.Page

    Protected Sub GV1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And (e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate)) Then
            If e.Row.DataItemIndex = 0 Then
                e.Row.Visible = False
            End If

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim RemovedLabel As Label = CType(e.Row.FindControl("RemovedLabel"), Label)
            Dim RemovedByLabel As Label = CType(e.Row.FindControl("RemovedByLabel"), Label)
            Dim RemovedHeader As Label = CType(e.Row.FindControl("RemovedHeader"), Label)
            Dim UpdateHeader As Label = CType(e.Row.FindControl("UpdateHeader"), Label)
            Dim UpdateLabel As Label = CType(e.Row.FindControl("LastUpdateLabel"), Label)
            Dim UpdateByLabel As Label = CType(e.Row.FindControl("LastUpdateByLabel"), Label)
            If RemovedLabel.Text <> "" And RemovedByLabel.Text <> "" Then
                e.Row.Style.Add("background", "#eee")
                RemovedLabel.Text = Left(RemovedLabel.Text, 16)
                RemovedByLabel.Text = "<br />" & MasterClass.ADNameSearch(RemovedByLabel.Text, "displayName")
                UpdateHeader.Visible = False
                UpdateLabel.Visible = False
                UpdateByLabel.Visible = False
                If (e.Row.RowState And DataControlRowState.Edit) > 0 Then
                    Dim RemoveButton As LinkButton = CType(e.Row.FindControl("RemoveButton"), LinkButton)
                    RemoveButton.CommandName = "Restore"
                    RemoveButton.Text = "Restore"

                End If
            ElseIf UpdateLabel.Text <> "" And UpdateByLabel.Text <> "" Then
                RemovedHeader.Visible = False
                UpdateLabel.Text = Left(UpdateLabel.Text, 16)
                UpdateByLabel.Text = "<br />" & MasterClass.ADNameSearch(UpdateByLabel.Text, "displayName")
            End If

        End If


    End Sub

    Protected Sub CategorySDS_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles CategorySDS.Inserting
        e.Command.Parameters("@CategoryLastUpdate").Value = DateTime.Now
        e.Command.Parameters("@CategoryLastUpdateBy").Value = Session("strUser")
    End Sub

    Protected Sub CategorySDS_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles CategorySDS.Updating
        e.Command.Parameters("@CategoryLastUpdate").Value = DateTime.Now
        e.Command.Parameters("@CategoryLastUpdateBy").Value = Session("strUser")

    End Sub

    Protected Sub GV1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV1.RowCommand
        If e.CommandName = "Insert" Then
            Dim NameTB As TextBox = CType(GV1.FooterRow.FindControl("NameTB"), TextBox)
            CategorySDS.InsertParameters("CategoryTitle").DefaultValue = NameTB.Text
            CategorySDS.InsertParameters("CategoryParentID").DefaultValue = ParentDDL.SelectedValue


            CategorySDS.Insert()

        ElseIf e.CommandName = "Restore" Then
            Dim gvRow As GridViewRow = CType(e.CommandSource, LinkButton).NamingContainer
            Dim intCategoryID As Integer = CInt(GV1.DataKeys(gvRow.RowIndex).Value())

            Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Using sqlConn As New SqlConnection(strConnection)

                Dim strQuery As String = "UPDATE tblCategory SET CategoryDeleted = NULL, CategoryDeletedBy = NULL, CategoryLastUpdate = '" & DateTime.Now & "', CategoryLastUpdateBy = '" & Session("strUser") & "' WHERE CategoryID = " & intCategoryID
                Dim cmdFetch As SqlCommand = New SqlCommand(strQuery, sqlConn)
                sqlConn.Open()
                cmdFetch.ExecuteNonQuery()
                sqlConn.Close()

            End Using

            GV1.DataBind()
            GV1.EditIndex = -1
        ElseIf e.CommandName = "Remove" Then
            Dim gvRow As GridViewRow = CType(e.CommandSource, LinkButton).NamingContainer
            Dim intCategoryID As Integer = CInt(GV1.DataKeys(gvRow.RowIndex).Value())

            Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
            Using sqlConn As New SqlConnection(strConnection)

                Dim strQuery As String = "UPDATE tblCategory SET CategoryDeleted = '" & DateTime.Now & "', CategoryDeletedBy = '" & Session("strUser") & "' WHERE CategoryID = " & intCategoryID
                Dim cmdFetch As SqlCommand = New SqlCommand(strQuery, sqlConn)
                sqlConn.Open()
                cmdFetch.ExecuteNonQuery()
                sqlConn.Close()

            End Using

            GV1.DataBind()
            GV1.EditIndex = -1
        End If
    End Sub

    Protected Sub GV1_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GV1.RowDeleting

    End Sub
End Class
