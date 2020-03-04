Imports System.Security.Principal

Public Class Form1
    Public Property ListBox1 As Object

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Label1.Enabled = False
        Label1.ForeColor = Color.LightGray

        ' Não deixa redimencionar a tela
        FormBorderStyle = FormBorderStyle.FixedDialog
        ' para ativar opção de form dentro de form
        IsMdiContainer = True

        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False
        Button6.Enabled = False
        Label2.Hide()

        PictureBox1.Enabled = False

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        formAcesso.MdiParent = Me
        formAcesso.Show()
        formAcesso.Top = 2
        formAcesso.Left = 180

        FormCopia.Close()
        FormDerrubar.Close()
        FormDesligar.Close()
        FormDominio.Close()





    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' abrir form no form
        FormCopia.MdiParent = Me
        FormCopia.Show()
        FormCopia.Top = 2
        FormCopia.Left = 180


        FormDerrubar.Close()
        formAcesso.Close()
        FormDesligar.Close()
        FormDominio.Close()


    End Sub

    Private Sub Formlogin_load(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

        CheckForIllegalCrossThreadCalls = False

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ' autentica no active direct windows

        If TextBox1.Text = "" Then

            TextBox1.BackColor = Color.LightSteelBlue

        Else

            TextBox1.BackColor = DefaultBackColor

            If TextBox2.Text = "" Then

                TextBox2.BackColor = Color.LightSteelBlue

            Else

                TextBox2.BackColor = DefaultBackColor

                'AUTENTICA PELO AD 

                ' Try 

                ' Dim directoryEntry = New DirectoryEntry("LDAP://DC=unisoadm,DC=uniso,DC=br", TextBox1.Text, TextBox2.Text)
                ' Dim directorySearcher = New DirectorySearcher(directoryEntry)
                ' directorySearcher.Filter = "(&(objectClass=user)(sAMAccountName=" + TextBox1.Text + "))"
                ' Dim SearchResult = directorySearcher.FindAll()

                ' If Err.Number = 0 And SearchResult.Count <> 0 Then

                ' MsgBox("ok")

                ' Else

                'Label2.Show()
                '   TextBox2.Text = ""
                'End If


                ' Catch ex As Exception

                ' Label2.Show()
                'TextBox2.Text = ""

                'End Try


                If TextBox1.Text = "admin" And TextBox2.Text = "labti_uniso" Then

                    'COPIA PSEXEC PARA %TEMP%
                    Dim pse = My.Resources.psexec
                    System.IO.File.WriteAllBytes(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "psexec.exe"), My.Resources.psexec)

                    Dim netd = My.Resources.psexec
                    System.IO.File.WriteAllBytes(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "netdom.exe"), My.Resources.psexec)


                    Dim p = New Process
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.Arguments = "/c copy %temp%\netdom.exe C:\Windows\System32"
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.Start()


                    TextBox1.Hide()
                    TextBox2.Hide()
                    Button5.Hide()

                    Label2.Hide()
                    Label1.Enabled = True
                    Label1.ForeColor = Color.White

                    Button1.Enabled = True
                    Button2.Enabled = True
                    Button3.Enabled = True
                    Button4.Enabled = True
                    Button6.Enabled = True


                Else
                    Label2.Show()
                    TextBox2.Text = ""
                End If




            End If
        End If
    End Sub

    Private Function objetoAD() As Object
        Throw New NotImplementedException()
    End Function

    Private Function null() As DirectorySearcher
        Throw New NotImplementedException()
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Form1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles MyBase.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Button5.PerformClick()
        End If
    End Sub

    Private Sub Label1_MouseClick(sender As Object, e As MouseEventArgs) Handles Label1.MouseClick
        FormCopia.Close()
        formAcesso.Close()
        FormDerrubar.Close()
        FormDominio.Close()

        Button1.Enabled = False
        Button2.Enabled = False
        Button3.Enabled = False
        Button4.Enabled = False


        TextBox1.Show()
        TextBox2.Show()
        Button5.Show()

        TextBox2.Text = ""

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        FormDerrubar.Show()
        FormDerrubar.MdiParent = Me
        FormDerrubar.Show()
        FormDerrubar.Top = 2
        FormDerrubar.Left = 180

        FormCopia.Close()
        FormDesligar.Close()
        formAcesso.Close()
        FormDominio.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        FormDesligar.Show()
        FormDesligar.MdiParent = Me
        FormDesligar.Show()
        FormDesligar.Top = 2
        FormDesligar.Left = 180

        FormCopia.Close()
        formAcesso.Close()
        FormDerrubar.Close()
        FormDominio.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        FormDominio.Show()
        FormDominio.MdiParent = Me
        FormDominio.Show()
        FormDominio.Top = 2
        FormDominio.Left = 180

        FormCopia.Close()
        formAcesso.Close()
        FormDerrubar.Close()
        FormDesligar.Close()
    End Sub
End Class
