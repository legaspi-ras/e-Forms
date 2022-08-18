Imports System.IO
Imports System.Net
Imports MySql.Data.MySqlClient

Public Class _Default
    Inherits Page
    Dim connection As MySqlConnection
    Dim command As MySqlCommand

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        ''call function Searchfile for loading data gridview

        If Not Me.IsPostBack Then
            Me.Searchfile()
        End If

    End Sub

    Private Sub Searchfile()
        ''function that loads all the file information in data gridview

        Dim query As String

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        query = ("SELECT formControlnum, formTitle FROM tblform_masterlist")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()


        If reader.HasRows Then

            reader.Close()

            Using sda As New MySqlDataAdapter(command)
                Dim dt As New DataTable()
                sda.Fill(dt)
                gvfiles.DataSource = dt
                gvfiles.DataBind()
            End Using

        Else

            MsgBox("Sorry, I cant load the data because your database table named masterlist is empty.")

        End If

        connection.Close()

    End Sub

    Protected Sub OnPaging(sender As Object, e As GridViewPageEventArgs)
        '' displaying the other data in next page

        gvfiles.PageIndex = e.NewPageIndex
        Me.Searchfile()

    End Sub

    Protected Sub btnsearch_Click(sender As Object, e As EventArgs) Handles btnsearch.Click

        '' search the file and display in datagridview based on the applicable specification 
        Dim query As String
        Dim frmsearch As String

        frmsearch = txtsearch.Value

        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")


        query = ("SELECT * FROM tblform_masterlist WHERE formControlnum LIKE '" & frmsearch & "%' OR formTitle LIKE '" & frmsearch & "%' ")

        command = New MySqlCommand(query, connection)
        connection.Open()

        Dim reader As MySqlDataReader
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then
            reader.Close()

            Using sda As New MySqlDataAdapter(command)
                Dim dt As New DataTable()
                sda.Fill(dt)
                gvfiles.DataSource = dt
                gvfiles.DataBind()
            End Using

        Else

            MsgBox("Sorry, the e-form that your looking for is not available.")

        End If

        connection.Close()

    End Sub

    Protected Sub gvfiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles gvfiles.SelectedIndexChanged

        ''getting the value of seleted index in grid view
        Dim formtitle As String = gvfiles.SelectedRow.Cells(1).Text
        Dim formcntrolnum As String = gvfiles.SelectedRow.Cells(0).Text

        Dim filename As String

        ''create a connection to database
        connection = New MySqlConnection
        connection.ConnectionString = ("server='localhost'; port='3306'; username='root'; password='powerhouse'; database='eforms'")

        'mysql query that select the file based on the formtitle and formctrlnum specifications
        Dim query As String
        query = ("select * from tblform_masterlist where  formTitle = '" & formtitle & "' and formControlnum = '" & formcntrolnum & "'")
        command = New MySqlCommand(query, connection)

        Dim reader As MySqlDataReader
        connection.Open()
        reader = command.ExecuteReader()
        reader.Read()

        If reader.HasRows Then

            filename = reader(5) 'getting the filename of the form from the database

            Dim directory As String
            directory = Server.MapPath("~/pdf_files/" + filename)

            'open pdf in same page ------------------------------------------------------------------------------------------------------------

            Dim embed As String
            embed = " "
            embed = "<object data=""{0}"" type=""application/pdf"" width=""500px"" height=""500px"" > </object>"
            HyperLink1.Text = String.Format(embed, ResolveUrl("~/pdf_files/" + filename))

        Else

            MsgBox("Uh-oh, you got a missing pdf file.")

        End If

        reader.Close()
        connection.Close()

    End Sub

End Class