Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Public Class WebForm10
    Inherits System.Web.UI.Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ''call function Searchfile for loading data gridview
        If Not Me.IsPostBack Then
            Me.Searchfile()
            '' Me.Searchfile1()
        End If

    End Sub
    Private Sub Searchfile1()
        'function that loads all the file information in data gridview

        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        query = ("SELECT tblapprovers.approverName, tblapprovers.emailAddress,  tblapprovers.approverStatus, tblapprovers.approvDate,tblapprovers.remarks FROM tblmasterlist INNER JOIN tblapprovers ON tblmasterlist.formControlnum = tblapprovers.formControlnum")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Using sda As New MySqlDataAdapter(command)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView3.DataSource = dt
            GridView3.DataBind()
        End Using

        connection.Close()

    End Sub

    Private Sub Searchfile()
        'function that loads all the file information in data gridview

        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.formControlnum = tblapprovalrequest.formControlnum")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Using sda As New MySqlDataAdapter(command)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using

        connection.Close()

    End Sub
    Protected Sub OnPaging(sender As Object, e As GridViewPageEventArgs)
        '' displaying the other data in next page

        GridView1.PageIndex = e.NewPageIndex
        Me.Searchfile()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        ' search the file and display in datagridview based on the applicable specification 
        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.applicableSpecs = tblapprovalrequest.applicableSpecs WHERE tblmasterlist.applicableSpecs LIKE '" & TextBox1.Text & "%'")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Using sda As New MySqlDataAdapter(command)
            Dim dt As New DataTable()
            sda.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using

        connection.Close()

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GridView1.SelectedIndexChanged

        ''getting the value of seleted index in grid view
        Dim formtitle As String = GridView1.SelectedRow.Cells(0).Text
        '' Dim applicablespecs As String = GridView1.SelectedRow.Cells(1).Text '' sila na mag iinput ng applicable specs kaya d na ma uulit or same for the meantime sa filename muna tayo mag base for viewing lang naman

        Dim applicablespecs As String = GridView1.SelectedRow.Cells(3).Text
        Dim formctrlnum As String = GridView1.SelectedRow.Cells(1).Text '' affected lahat ng data gridview kasi ito yung nakalagay sa where clause sa mysql database

        Dim filename As String

        ''create a connection to database
        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        ''MySql query that select the file based on the formtitle And applicable specifications
        Dim query As String
        ''query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.applicableSpecs, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus , tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.applicableSpecs = tblapprovalrequest.applicableSpecs WHERE tblmasterlist.applicableSpecs = '" & applicablespecs & "'")
        query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus , tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.formControlnum = tblapprovalrequest.formControlnum WHERE tblapprovalrequest.formControlnum = '" & formctrlnum & "'")
        command = New MySqlCommand(query, connection)

        Dim reader As MySqlDataReader
        connection.Open()
        reader = command.ExecuteReader()
        reader.Read()

        'Label4.Text = reader(0)
        Dim kuha As String
        kuha = GridView1.PageIndex.ToString

        '' Label1.Text = formtitle + " " + applicablespecs
        '' Label2.Text = kuha
        '' GridView1.PageIndex = pageList.SelectedIndex

        filename = reader(3) 'getting the filename of the form

        ''Label1.Text = String.Format(ResolveUrl("~/pdf_files/" + filename + ""))

        Dim directory As String

        directory = Server.MapPath("~/for_approval/" + filename)

        ''  Label2.Text = directory

        'open pdf in same page ------------------------------------------------------------------------------------------------------------

        'Dim embed As String
        'embed = " "
        'embed = "<object data=""{0}"" type=""application/pdf"" width=""600px"" height=""600px"" > </object>"
        'HyperLink1.Text = String.Format(embed, ResolveUrl("~/for_approval/" + filename))

        'Label3.Text = String.Format(ResolveUrl("~/pdf_files/" + filename))

        ''open din pdf kaso napapatungan si system-----------------------------------------------------------------------------------------

        'Dim path As String = Server.MapPath("~/pdf_files/" + filename)
        'Dim client As New WebClient()
        'Dim buffer As [Byte]() = client.DownloadData(path)

        'If buffer IsNot Nothing Then
        '    Response.ContentType = "application/pdf"
        '    Response.AddHeader("content-length", buffer.Length.ToString())
        '    Response.BinaryWrite(buffer)
        '    Response.End()

        'End If

        '' open pdf kay foxiy application -------------------------------------------------------------------------------------------------

        Process.Start("C:\Users\romer.legaspi\source\repos\connection\connection\for_approval\" + filename)

        ''---------------------------------------------------------------------------------------------------------------------------------

        reader.Close()
        connection.Close()

        Me.Searchfile1()

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim approverStatus As String
        approverStatus = "Approved"

        For Each gvr As GridViewRow In GridView3.Rows

            Dim status As DropDownList = DirectCast(gvr.FindControl("ddlApproverstatus"), DropDownList)
            Dim appstatus As String = status.Text

            Dim tb1 As Label = DirectCast(gvr.FindControl("Label1"), Label)
            Dim txt1 As String = tb1.Text

            If approverStatus = appstatus Then

                MsgBox(appstatus + " by " + txt1)

            End If

        Next

    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

    End Sub
End Class