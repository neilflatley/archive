Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports AjaxControlToolkit
Imports System.Data
Imports System.Data.SqlClient
Imports System.Collections.Generic

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
<System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class WebService
    Inherits System.Web.Services.WebService
    '<WebMethod()> _
    'Public Function GetCompletionList(ByVal prefixText As String
    <WebMethod()> _
Public Function GetOffices(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
        Dim strOfficesQuery As String = "SELECT OfficeID, OfficeName FROM tblOffice ORDER BY OfficeName"
        Dim cmdFetchOffices As SqlCommand = New SqlCommand(strOfficesQuery, sqlConn)
        Dim dtrOffices As SqlDataReader
        Dim myOffices As New List(Of CascadingDropDownNameValue)
        sqlConn.Open()
        dtrOffices = cmdFetchOffices.ExecuteReader
        While dtrOffices.Read()
            Dim strOfficeName As String = dtrOffices("OfficeName").ToString
            Dim strOfficeID As String = dtrOffices("OfficeID").ToString
            myOffices.Add(New CascadingDropDownNameValue(strOfficeName, strOfficeID))
        End While
        Return myOffices.ToArray
    End Function
    <WebMethod()> _
    Public Function GetLocations(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
        Dim strLocationQuery As String = "SELECT LocationID, LocationName FROM tblLocation WHERE OfficeID = @OfficeID AND LocationIsCurrent = 1 ORDER BY LocationName"
        Dim cmdFetchLocation As SqlCommand = New SqlCommand(strLocationQuery, sqlConn)
        Dim dtrLocation As SqlDataReader
        Dim kvLocation As StringDictionary = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
        Dim intOfficeID As Integer
        If Not kvLocation.ContainsKey("Office") Or Not Int32.TryParse(kvLocation("Office"), intOfficeID) Then
            Return Nothing
        End If
        cmdFetchLocation.Parameters.AddWithValue("@OfficeID", intOfficeID)
        Dim myLocations As New List(Of CascadingDropDownNameValue)
        'myLocations.Add(New CascadingDropDownNameValue("Area Office", "0"))
        sqlConn.Open()
        dtrLocation = cmdFetchLocation.ExecuteReader
        Dim blnAO As Boolean = False
        While dtrLocation.Read()
            Dim strLocationName As String = dtrLocation("LocationName").ToString
            Dim strLocationID As String = dtrLocation("LocationID").ToString
            If strLocationName = "Area Office" Then
                blnAO = True
            Else
                blnAO = False
            End If
            myLocations.Add(New CascadingDropDownNameValue(strLocationName, strLocationID, blnAO))
        End While
        Return myLocations.ToArray
    End Function


    <WebMethod()> _
    Public Function GetCats(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
        Dim strParentQuery As String = "SELECT CategoryID, CategoryTitle FROM tblCategory WHERE CategoryParentID IS NULL ORDER BY CategoryTitle"
        Dim cmdFetchParents As SqlCommand = New SqlCommand(strParentQuery, sqlConn)
        Dim dtrParents As SqlDataReader
        Dim myParentCats As New List(Of CascadingDropDownNameValue)
        sqlConn.Open()
        dtrParents = cmdFetchParents.ExecuteReader
        While dtrParents.Read()
            Dim strCategoryTitle As String = dtrParents("CategoryTitle").ToString
            Dim strCategoryID As String = dtrParents("CategoryID").ToString
            myParentCats.Add(New CascadingDropDownNameValue(strCategoryTitle, strCategoryID))
        End While
        Return myParentCats.ToArray
    End Function
    <WebMethod()> _
    Public Function GetSubcats(ByVal knownCategoryValues As String, ByVal category As String) As CascadingDropDownNameValue()
        Dim strConnection As String = ConfigurationManager.ConnectionStrings("ConnectionString").ConnectionString
        Dim sqlConn As SqlConnection = New SqlConnection(strConnection)
        Dim strSubcatsQuery As String = "SELECT CategoryID, CategoryTitle FROM tblCategory WHERE CategoryParentID = @ParentID ORDER BY CategoryTitle"
        Dim cmdFetchSubcats As SqlCommand = New SqlCommand(strSubcatsQuery, sqlConn)
        Dim dtrSubcats As SqlDataReader
        Dim kvSubcats As StringDictionary = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)
        Dim intCategoryID As Integer
        If Not kvSubcats.ContainsKey("Category") Or Not Int32.TryParse(kvSubcats("Category"), intCategoryID) Then
            Return Nothing
        End If
        cmdFetchSubcats.Parameters.AddWithValue("@ParentID", intCategoryID)
        Dim myCategories As New List(Of CascadingDropDownNameValue)
        myCategories.Add(New CascadingDropDownNameValue("", ""))
        sqlConn.Open()
        dtrSubcats = cmdFetchSubcats.ExecuteReader
        While dtrSubcats.Read()
            Dim strLocationName As String = dtrSubcats("CategoryTitle").ToString
            Dim strLocationID As String = dtrSubcats("CategoryID").ToString
            myCategories.Add(New CascadingDropDownNameValue(strLocationName, strLocationID))
        End While
        Return myCategories.ToArray
    End Function



End Class
