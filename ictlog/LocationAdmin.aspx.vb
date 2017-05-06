
Partial Class LocationAdmin
    Inherits System.Web.UI.Page

    Protected Sub GV1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GV1.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow And (e.Row.RowState = DataControlRowState.Normal Or e.Row.RowState = DataControlRowState.Alternate)) Then
            Dim CurrentLabel As Label = CType(e.Row.FindControl("CurrentLabel"), Label)

            If CurrentLabel.Text = "1" Then
                CurrentLabel.Text = "Yes"
            Else
                CurrentLabel.Text = "No"
            End If

        End If
    End Sub

    Protected Sub InsertButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub LocationSDS_Inserted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles LocationSDS.Inserted

    End Sub

    Protected Sub LocationSDS_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles LocationSDS.Inserting

    End Sub

    Protected Sub LocationSDS_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceCommandEventArgs) Handles LocationSDS.Updating

    End Sub

    Protected Sub GV1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GV1.RowCommand
        If e.CommandName = "Insert" Then
            Dim NameTB As TextBox = CType(GV1.FooterRow.FindControl("NameTB"), TextBox)
            LocationSDS.InsertParameters("LocationName").DefaultValue = NameTB.Text
            LocationSDS.InsertParameters("OfficeID").DefaultValue = OfficeDDL.SelectedValue


            LocationSDS.Insert()

        End If
    End Sub
End Class
