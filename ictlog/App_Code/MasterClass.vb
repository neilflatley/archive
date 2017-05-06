Imports Microsoft.VisualBasic
Imports System.DirectoryServices
Imports System.Data

Public Class MasterClass
    Public Shared Function ADNameSearch(ByVal strUser As String, ByVal strADField As String) As String
        'Feed a username and output parameter, will return from AD
        Dim Entry As New DirectoryEntry("LDAP://DC=cwmgp,DC=co,DC=uk")
        Dim Searcher As New DirectorySearcher(Entry)
        Dim AdObj As SearchResult

        Searcher.SearchScope = SearchScope.Subtree
        Searcher.PropertiesToLoad.Add(strADField)
        Searcher.Filter = "(&(ObjectCategory=Person)(ObjectClass=User)(sAMAccountName=" & strUser & "))"

        AdObj = Searcher.FindOne()
        'If user does not exist in AD any more just display strUser
        If AdObj Is Nothing Then
            ADNameSearch = strUser
        Else
            ADNameSearch = AdObj.Properties(strADField).Item(0)
        End If
    End Function

    Public Shared Function ActiveDirectorySearch(ByVal strSortParameter As String, ByVal strSortDirection As String) As DataTable

        Dim Entry As New DirectoryEntry("LDAP://DC=cwmgp,DC=co,DC=uk")
        Dim Searcher As New DirectorySearcher(Entry)
        Dim AdObj As SearchResult

        Searcher.SearchScope = SearchScope.Subtree
        Searcher.PropertiesToLoad.Add("sAMAccountName")
        Searcher.PropertiesToLoad.Add("displayName")
        Searcher.Filter = "(&(ObjectCategory=Person)(ObjectClass=User)(sn=*)(physicalDeliveryOfficeName=*)(!description=hide))"
        Searcher.Sort.PropertyName = strSortParameter
        If strSortDirection = "Descending" Then
            Searcher.Sort.Direction = SortDirection.Descending
        End If

        'Create the DataTable
        Dim dt As New DataTable

        'Create the columns
        Dim dcUser As New DataColumn("User")
        Dim dcName As New DataColumn("Name")

        'Add the columns to the DataTable's Columns collection
        dt.Columns.Add(dcUser)
        dt.Columns.Add(dcName)

        'Populate rows
        For Each AdObj In Searcher.FindAll
            Dim dr As DataRow
            dr = dt.NewRow
            dr("User") = AdObj.Properties("sAMAccountName").Item(0)
            dr("Name") = AdObj.Properties("displayName").Item(0)
            dt.Rows.Add(dr)
        Next

        ActiveDirectorySearch = dt

    End Function

    Public Shared Function getThisMonday(ByVal aDate As DateTime) As DateTime
        If aDate.DayOfWeek = DayOfWeek.Monday Then
            Return aDate
        ElseIf aDate.DayOfWeek = DayOfWeek.Sunday Then
            Return aDate.AddDays(-6)
        Else
            Return aDate.AddDays(-CDbl(aDate.DayOfWeek) + 1)
        End If
    End Function

    Public Shared Function getLastMonday(ByVal aDate As DateTime) As DateTime
        Return getThisMonday(aDate).AddDays(-7)
    End Function

End Class
