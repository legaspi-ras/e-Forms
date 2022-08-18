

Public Class WebForm9
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim P As New ProcessStartInfo
        'P.FileName = "C:\Program Files (x86)\Foxit Software\Foxit PDF Reader\FoxitPDFReader.exe"

        'P.UseShellExecute = True
        'P.WindowStyle = ProcessWindowStyle.Normal
        'P.Verb = "runas"

        'Dim pro As Process = Process.Start(P)
        Label1.Text = "resume.pdf" ''pinaltan ko lang file name ng ecn 

        Process.Start("C:\Users\romer.legaspi\source\repos\connection\connection\pdf_files\" + Label1.Text)



    End Sub
End Class