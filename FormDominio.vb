Imports System.Net
Imports System.Management
Imports System.Management.Instrumentation
Imports System.Data
Imports System.Globalization
Imports System.Threading
Imports System.Data.OleDb
Imports System.Configuration





Public Class FormDominio
    Dim p As New System.Globalization.CultureInfo("pt-BR")
    Public Property RunspaceFactory As Object

    Private Sub FormDominio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        TextBox3.PasswordChar = "*"
        TextBox3.MaxLength = 15
        Label6.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ListBox2.Items.Clear()


        Try

            If TextBox2.Text = "" Then
                Label6.Show()
            Else
                Label6.Hide()
            End If

            If My.Computer.Network.Ping("" + TextBox2.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox2.Text + "\c$") Then

                    Label6.Hide()

                    Try

                        Dim strHostName As String
                        strHostName = System.Net.Dns.GetHostEntry(TextBox2.Text).HostName

                        ' PARA FUNCIONAR E PRECISO IR EM APLICAÇOES DO PROJETO E TIRAR OPçÂO DE X32 BITS

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c %temp%\psexec \\" + TextBox2.Text + " netdom join " + strHostName + " /domain:" + TextBox4.Text + " /userd:" + TextBox4.Text + "\" + TextBox1.Text + " /passwordd:" + TextBox3.Text + ""
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()





                        ListBox2.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.None))



                        p.Close()


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

                    TextBox1.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()


                Else
                    Label6.Show()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""

                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()
                End If

            Else
                Label6.Show()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            End If
        Catch ex As Exception
            Label6.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ListBox2.Items.Clear()


        Try

            If TextBox2.Text = "" Then
                Label6.Show()
            Else
                Label6.Hide()
            End If

            If My.Computer.Network.Ping("" + TextBox2.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox2.Text + "\c$") Then

                    Label6.Hide()

                    Try
                        Dim strHostName As String
                        strHostName = System.Net.Dns.GetHostEntry(TextBox2.Text).HostName

                        ' PARA FUNCIONAR E PRECISO IR EM APLICAÇOES DO PROJETO E TIRAR OPçÂO DE X32 BITS

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c %temp%\psexec \\" + TextBox2.Text + " netdom remove " + strHostName + " /domain:" + TextBox4.Text + " /userd:" + TextBox4.Text + "\" + TextBox1.Text + " /passwordd:" + TextBox3.Text + ""
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()





                        ListBox2.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.None))



                        p.Close()



                        TextBox1.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()


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
                    Label6.Show()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""

                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()

                End If

            Else
                Label6.Show()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            End If
        Catch ex As Exception
            Label6.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        ListBox2.Items.Clear()

        Label6.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox2.Items.Clear()


        Try

            If TextBox2.Text = "" Then
                Label6.Show()
            Else
                Label6.Hide()
            End If

            If My.Computer.Network.Ping("" + TextBox2.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox2.Text + "\c$") Then

                    Label6.Hide()

                    Try

                        ' PARA FUNCIONAR E PRECISO IR EM APLICAÇOES DO PROJETO E TIRAR OPçÂO DE X32 BITS

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c %temp%\psexec \\" + TextBox2.Text + " shutdown -r -t 5 -f"
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()



                        Dim ps As String = "REINICIANDO . . ."

                        ListBox2.Items.Add(ps)



                        p.Close()



                        TextBox1.Clear()
                        TextBox3.Clear()
                        TextBox4.Clear()


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
                    Label6.Show()
                    TextBox1.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    TextBox4.Text = ""

                    TextBox1.Clear()
                    TextBox2.Clear()
                    TextBox3.Clear()
                    TextBox4.Clear()

                End If

            Else
                Label6.Show()
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""

                TextBox1.Clear()
                TextBox2.Clear()
                TextBox3.Clear()
                TextBox4.Clear()
            End If
        Catch ex As Exception
            Label6.Show()
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""

            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
        End Try
    End Sub
End Class

