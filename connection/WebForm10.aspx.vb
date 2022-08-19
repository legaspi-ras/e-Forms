﻿Imports MySql.Data.MySqlClient
Imports System.Web

Public Class WebForm10
    Inherits System.Web.UI.Page

    Dim connection As MySqlConnection
    Dim command As MySqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblEmpname.Text = HttpContext.Current.Session(“empname”)

        ''call function Searchfile for loading data gridview
        If Not Me.IsPostBack Then
            Me.Searchfile()
        End If

    End Sub

    Private Sub Searchfile()
        'function that loads all the file information in data gridview

        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        query = ("SELECT tblform_masterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.filename, tblapprovalrequest.applicableSpecs ,tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment,  tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblform_masterlist INNER JOIN tblapprovalrequest ON tblform_masterlist.formControlnum = tblapprovalrequest.formControlnum;")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Try
            Using sda As New MySqlDataAdapter(command)
                Dim dt As New DataTable()
                sda.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
            End Using
        Catch ex As Exception
            MsgBox("for update")
        End Try


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
        Dim search As String

        search = txtSearch.Value

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        query = ("SELECT tblform_masterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.filename, tblapprovalrequest.applicableSpecs ,tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment,  tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblform_masterlist INNER JOIN tblapprovalrequest ON tblform_masterlist.formControlnum = tblapprovalrequest.formControlnum WHERE tblapprovalrequest.formControlnum = '" & search & "' OR tblapprovalrequest.applicableSpecs = '" & search & "'")

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

        Dim applicablespecs As String = GridView1.SelectedRow.Cells(2).Text
        Dim formctrlnum As String = GridView1.SelectedRow.Cells(1).Text '' affected lahat ng data gridview kasi ito yung nakalagay sa where clause sa mysql database FOR TESTING LANG TO 

        Dim filename As String

        ''create a connection to database
        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        ''MySql query that select the file based on the formtitle And applicable specifications
        Dim query As String
        ''query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.applicableSpecs, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus , tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.applicableSpecs = tblapprovalrequest.applicableSpecs WHERE tblmasterlist.applicableSpecs = '" & applicablespecs & "'")
        '' query = ("SELECT tblmasterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.modifdate, tblapprovalrequest.filename, tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment, tblapprovalrequest.requestStatus , tblapprovalrequest.requestDate FROM tblmasterlist INNER JOIN tblapprovalrequest ON tblmasterlist.formControlnum = tblapprovalrequest.formControlnum WHERE tblapprovalrequest.formControlnum = '" & formctrlnum & "'")
        query = ("SELECT tblform_masterlist.formTitle, tblapprovalrequest.formControlnum, tblapprovalrequest.filename, tblapprovalrequest.applicableSpecs ,tblapprovalrequest.requestorName, tblapprovalrequest.requestorDepartment,  tblapprovalrequest.requestStatus, tblapprovalrequest.requestDate FROM tblform_masterlist INNER JOIN tblapprovalrequest ON tblform_masterlist.formControlnum = tblapprovalrequest.formControlnum WHERE tblapprovalrequest.formControlnum = '" & formctrlnum & "'")
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

        filename = reader(2) 'getting the filename of the form

        HttpContext.Current.Session(“formdepartment”) = GridView1.SelectedRow.Cells(4).Text
        HttpContext.Current.Session(“formctrlnum”) = GridView1.SelectedRow.Cells(1).Text
        HttpContext.Current.Session(“formtitle”) = GridView1.SelectedRow.Cells(0).Text
        HttpContext.Current.Session(“appspecs”) = GridView1.SelectedRow.Cells(2).Text
        HttpContext.Current.Session(“filename”) = filename

        ''Label1.Text = String.Format(ResolveUrl("~/pdf_files/" + filename + ""))

        Dim directory As String

        directory = Server.MapPath("~/for_approval/" + filename)

        ''  Label2.Text = directory

        'open pdf in same page ------------------------------------------------------------------------------------------------------------

        'Dim embed As String
        'embed = " "
        'embed = "<object data=""{0}"" type=""application/pdf"" width=""400x"" height=""400px"" > </object>"
        'HyperLink1.Text = String.Format(embed, ResolveUrl("~/for_approval/" + filename))

        ''Label3.Text = String.Format(ResolveUrl("~/pdf_files/" + filename))

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

        ''Process.Start("C:\Users\romer.legaspi\source\repos\e_forms\connection\for_approval\" + filename)

        ''---------------------------------------------------------------------------------------------------------------------------------

        reader.Close()
        connection.Close()

        Response.Redirect("WebForm11.aspx")

        'connection = New MySqlConnection
        'connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        'query = ("SELECT tblapprovers.approverName, tblapprovers.emailAddress,  tblapprovers.approverStatus, tblapprovers.approvDate,tblapprovers.remarks FROM tblmasterlist INNER JOIN tblapprovers ON tblmasterlist.formControlnum = tblapprovers.formControlnum  WHERE tblmasterlist.formControlnum  =  '" & formctrlnum & "'")

        'command = New MySqlCommand(query, connection)
        'connection.Open()

        'Using sda As New MySqlDataAdapter(command)
        '    Dim dt As New DataTable()
        '    sda.Fill(dt)
        '    GridView2.DataSource = dt
        '    GridView2.DataBind()
        'End Using

        'connection.Close()

    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click


        empuser.Text = HttpContext.Current.Session(“empId”)

        Dim query As String
        Dim logstatus As String
        Dim usernow As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        query = ("SELECT * FROM tblloginhistory ORDER BY id DESC LIMIT 1")

        command = New MySqlCommand(query, connection)
        connection.Open()
        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        logstatus = reader(2)
        usernow = reader(1)

        reader.Close()
        connection.Close()


        Dim logoutdatentime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")


        query = ("UPDATE tblloginhistory SET logstatus = 'Logout', applicableSpecs = '" & HttpContext.Current.Session(“appspecs”) & "', formControlnum = '" & HttpContext.Current.Session(“formctrlnum”) & "' , logoutDatentime = '" & logoutdatentime & "' WHERE empId = '" & empuser.Text & "' AND logStatus = 'Login'")


        command = New MySqlCommand(query, connection)
            connection.Open()

            reader = command.ExecuteReader()
            reader.Read()

            reader.Close()
            connection.Close()


            '' call niya na dito yung next window tab na for viewing for approval
            Response.Redirect("Login.aspx")


    End Sub
End Class