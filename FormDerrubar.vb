Imports System.Net
Imports System.Management
Imports System.Management.Instrumentation
Imports System.Data



Public Class FormDerrubar
    Public Property RunspaceFactory As Object

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label6.Hide()

        ListBox1.Items.Clear()
        Label4.Hide()

        Try

            If TextBox1.Text = "" Then
                Label3.Show()
            Else
                Label3.Hide()
            End If


            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then

                    Label3.Hide()

                    Try

                        ' PARA FUNCIONAR E PRECISO IR EM APLICAÇOES DO PROJETO E TIRAR OPçÂO DE X32 BITS

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "powershell"
                        p.StartInfo.Arguments = "-windowstyle hidden query session /server:" + TextBox1.Text + ""
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()

                        ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))




                        ' OU USE ESTE ABAIXO PARA DERRUBAR USUSARIOS

                        ' Dim psi As New ProcessStartInfo With {
                        '.FileName = "cmd.exe",
                        '.Arguments = "/c query session /server:seu servidor aqui",
                        '.UseShellExecute = False,
                        '.WindowStyle = ProcessWindowStyle.Hidden,
                        '.RedirectStandardOutput = True}
                        'Dim p As New Process With {
                        '.StartInfo = psi}

                        'p.Start()

                        'ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.None))
                    Catch ex As Exception

                    End Try


                Else
                    Label3.Show()
                    TextBox1.Text = ""
                End If

            Else
                Label3.Show()
                TextBox1.Text = ""
            End If
        Catch ex As Exception
            Label3.Show()
            TextBox1.Text = ""
        End Try
    End Sub

    Private Sub FormDerrubar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Label3.Hide()
        Label4.Hide()
        Label6.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Label6.Hide()

        Try


            If TextBox1.Text = "" Then
                Label3.Show()

            Else

                If TextBox2.Text = "" Then
                    Label6.Show()

                Else



                    Dim resultado
                    resultado = MsgBox("DESEJA PROSSEGUIR ?", vbYesNo, "VERIFIQUE SE O ID ESTÁ CORRETO !")

                    If resultado = vbYes Then
                        Dim logoff = New Process
                        logoff.StartInfo.UseShellExecute = False
                        logoff.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        logoff.StartInfo.FileName = "powershell"
                        logoff.StartInfo.Arguments = "-windowstyle hidden rwinsta " + TextBox2.Text + " /server:" + TextBox1.Text + ""
                        logoff.Start()

                        If Err.Number = 0 Then
                            Label4.Show()
                            ListBox1.Items.Clear()
                            TextBox1.Text = ""
                            TextBox2.Text = ""
                        Else

                        End If

                    Else
                        TextBox1.Text = ""
                        TextBox2.Text = ""
                        ListBox1.Items.Clear()

                    End If

                End If

            End If


        Catch ex As Exception
            Label3.Show()
        End Try





    End Sub

End Class