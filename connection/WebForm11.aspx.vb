Imports MySql.Data.MySqlClient
Imports System.Web
Public Class WebForm11
    Inherits System.Web.UI.Page

    Dim connection As MySqlConnection
    Dim command As MySqlCommand


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim query As String
        Dim search As String

        search = lblRequestor.Text

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        query = ("SELECT EMP_NAME, DEPARTMENT FROM emp_masterlist WHERE EMP_NO = '" & HttpContext.Current.Session(“empId”) & "'") '' approver side na tayo kaya yung yung empId ay kay approver

        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        Dim empdept As String

        empdept = reader(1)

        reader.Close()
        connection.Close()

        lblRequestor.Text = HttpContext.Current.Session(“empname”)
        lblRequestordept.Text = empdept
        lblFormctrlnum.Text = HttpContext.Current.Session(“formctrlnum”)
        lblFormtitle.Text = HttpContext.Current.Session(“formtitle”)
        lblAppsspecs.Text = HttpContext.Current.Session(“appspecs”)
        lblFormdept.Text = HttpContext.Current.Session(“formdepartment”)


    End Sub

    Protected Sub btnview_Click(sender As Object, e As EventArgs) Handles btnview.Click

        btnupdate.Enabled = True

        Dim filename As String

        filename = HttpContext.Current.Session(“filename”)

        Dim directory As String

        directory = Server.MapPath("~/for_approval/" + filename)

        Process.Start("C:\Users\romer.legaspi\source\repos\e_forms\connection\for_approval\" + filename)

    End Sub

    Protected Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click

        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        query = ("UPDATE tblapprovalrequest SET requestStatus = '" & DropDownList1.SelectedItem.Value & "' WHERE applicableSpecs = '" & lblAppsspecs.Text & "'")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        reader.Close()
        connection.Close()

    End Sub
End Class