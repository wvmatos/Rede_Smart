Imports System.Net
Imports System.Management
Imports System.Management.Instrumentation
Imports System.Data
Imports System.Text
Public Class formAcesso
    Private Sub ACESSO_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Label3.Hide()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Label3.Hide()
        BackgroundWorker1.RunWorkerAsync()
        TextBox1.Text = TextBox1.Text
        'Process.Start("Cmd.exe", "/c echo off %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 0 /f >nul 2>&1 ")
        'Process.Start("Cmd.exe", "/c echo off %temp%\psexec.exe \\" + TextBox1.Text + "  %systemroot%\system32\NetSh Advfirewall set  allprofiles state off   >nul 2>&1 ")

    End Sub
    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = TextBox1.Text
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        ListBox1.Items.Clear()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        Label3.Hide()

        Button5.Enabled = True
        Button6.Enabled = True
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork

        Try

            ' PEGA O ARQUIVO QUE ESTA NO RESOURCE E COPIA PARA TEMP 
            Dim pse = My.Resources.psexec
            System.IO.File.WriteAllBytes(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "psexec.exe"), My.Resources.psexec)


            ListBox1.Items.Clear()


            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then


                    ' RETORNA MAC-ADRESS NO LISTBOX
                    Dim theManagementScope As New ManagementScope("\\" & TextBox1.Text & "\root\cimv2")
                    Dim theQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"

                    Dim theObjectQuery As New ObjectQuery(theQueryString)

                    Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
                    Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

                    ' RETORNA NOME , IP 

                    Dim strHostName As String
                    strHostName = System.Net.Dns.GetHostEntry(TextBox1.Text).HostName

                    Dim strIPAddress As String
                    strIPAddress = System.Net.Dns.GetHostEntry(TextBox1.Text).AddressList(0).ToString()

                    For Each currentResult As ManagementObject In theResultsCollection
                        ListBox1.Items.Add(currentResult("MacAddress").ToString())
                        ListBox1.Items.Add(strHostName)
                        ListBox1.Items.Add(strIPAddress)

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c  WMIC /NODE:" + TextBox1.Text + " COMPUTERSYSTEM GET USERNAME"
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()



                        ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))

                        ListBox1.Items.RemoveAt(3)


                    Next


                    If CheckBox2.CheckState = 0 And CheckBox1.CheckState = 0 Then
                        TextBox1.Text = TextBox1.Text
                        Dim ac = New Process
                        ac.StartInfo.UseShellExecute = True
                        ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        ac.StartInfo.FileName = "cmd.exe"
                        ac.StartInfo.Arguments = "/c msra /offerRA " + TextBox1.Text + ""
                        ac.StartInfo.CreateNoWindow = True
                        ac.Start()

                    End If


                    If CheckBox2.CheckState = 1 Then
                        'BAIXA NIVEL PARA ACESSO REMOTO
                        TextBox1.Text = TextBox1.Text




                        Dim p = New Process
                        p.StartInfo.UseShellExecute = True
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 0 /f"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\NetSh Advfirewall set  allprofiles state disabled"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\netsh advfirewall set currentprofile state off"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server"" /v fDenyTSConnections /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp"" /v UserAuthentication /t REG_DWORD /F /D 1 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorAdmin /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorUser /t REG_DWORD /F /D 1 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v PromptOnSecureDesktop /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableInstallerDetection /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableVirtualization /t REG_DWORD /F /D 1 "
                        p.Start()


                        TextBox1.Text = TextBox1.Text

                        Dim ac = New Process
                        ac.StartInfo.UseShellExecute = True
                        ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        ac.StartInfo.FileName = "cmd.exe"
                        ac.StartInfo.Arguments = "/c msra /offerRA " + TextBox1.Text + ""
                        ac.StartInfo.CreateNoWindow = True
                        ac.Start()



                    Else

                        If CheckBox1.CheckState = 1 Then

                            TextBox1.Text = TextBox1.Text

                            Dim p = New Process
                            p.StartInfo.UseShellExecute = True
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            p.StartInfo.FileName = "cmd.exe"
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 3 /f"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\NetSh Advfirewall set  allprofiles state enabled"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\netsh advfirewall set currentprofile state on"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server"" /v fDenyTSConnections /t REG_DWORD /F /D 0 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp"" /v UserAuthentication /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorAdmin /t REG_DWORD /F /D 2 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorUser /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v PromptOnSecureDesktop /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableInstallerDetection /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableVirtualization /t REG_DWORD /F /D 1 "
                            p.Start()


                            Dim ac = New Process
                            ac.StartInfo.UseShellExecute = True
                            ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            ac.StartInfo.FileName = "cmd.exe"
                            ac.StartInfo.Arguments = "/c msra /offerRA " + TextBox1.Text + ""
                            ac.StartInfo.CreateNoWindow = True
                            ac.Start()

                        End If
                    End If
                Else
                    TextBox1.Text = ""
                    ListBox1.Items.Clear()
                    Label3.Show()

                End If
            Else
                TextBox1.Text = ""
                ListBox1.Items.Clear()
                Label3.Show()
            End If


        Catch ex As Exception
            TextBox1.Text = ""
            ListBox1.Items.Clear()
            Label3.Show()
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        If TextBox1.Text = "" Then
            Label3.Show()
        Else
            Label3.Hide()
            Process.Start("Explorer", "\\" + TextBox1.Text + "\c$")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label3.Hide()
        BackgroundWorker2.RunWorkerAsync()
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork
        Try

            ' PEGA O ARQUIVO QUE ESTA NO RESOURCE E COPIA PARA TEMP 
            Dim pse = My.Resources.psexec
            System.IO.File.WriteAllBytes(IO.Path.Combine(My.Computer.FileSystem.SpecialDirectories.Temp, "psexec.exe"), My.Resources.psexec)

            TextBox1.Text = TextBox1.Text

            ListBox1.Items.Clear()

            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then
                    TextBox1.Text = TextBox1.Text

                    ' RETORNA MAC-ADRESS NO LISTBOX
                    Dim theManagementScope As New ManagementScope("\\" & TextBox1.Text & "\root\cimv2")
                    Dim theQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"

                    Dim theObjectQuery As New ObjectQuery(theQueryString)

                    Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
                    Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

                    ' RETORNA NOME , IP 

                    Dim strHostName As String
                    strHostName = System.Net.Dns.GetHostEntry(TextBox1.Text).HostName

                    Dim strIPAddress As String
                    strIPAddress = System.Net.Dns.GetHostEntry(TextBox1.Text).AddressList(0).ToString()

                    For Each currentResult As ManagementObject In theResultsCollection
                        ListBox1.Items.Add(currentResult("MacAddress").ToString())
                        ListBox1.Items.Add(strHostName)
                        ListBox1.Items.Add(strIPAddress)

                        Dim p = New Process
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c  WMIC /NODE:" + TextBox1.Text + " COMPUTERSYSTEM GET USERNAME"
                        p.StartInfo.CreateNoWindow = True
                        p.StartInfo.RedirectStandardOutput = True
                        p.Start()



                        ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))

                        ListBox1.Items.RemoveAt(3)


                    Next


                    If CheckBox2.CheckState = 0 And CheckBox1.CheckState = 0 Then
                        TextBox1.Text = TextBox1.Text
                        Dim ac = New Process
                        ac.StartInfo.UseShellExecute = True
                        ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        ac.StartInfo.FileName = "cmd.exe"
                        ac.StartInfo.Arguments = "/c mstsc /admin /v:" + TextBox1.Text + ""
                        ac.StartInfo.CreateNoWindow = True
                        ac.Start()

                    End If


                    If CheckBox2.CheckState = 1 Then
                        'BAIXA NIVEL PARA ACESSO REMOTO
                        TextBox1.Text = TextBox1.Text




                        Dim p = New Process
                        p.StartInfo.UseShellExecute = True
                        p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        p.StartInfo.FileName = "cmd.exe"
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 0 /f"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\NetSh Advfirewall set  allprofiles state disabled"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\netsh advfirewall set currentprofile state off"
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server"" /v fDenyTSConnections /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp"" /v UserAuthentication /t REG_DWORD /F /D 1 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorAdmin /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorUser /t REG_DWORD /F /D 1 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v PromptOnSecureDesktop /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableInstallerDetection /t REG_DWORD /F /D 0 "
                        p.Start()
                        p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableVirtualization /t REG_DWORD /F /D 1 "
                        p.Start()



                        TextBox1.Text = TextBox1.Text
                        Dim ac = New Process
                        ac.StartInfo.UseShellExecute = True
                        ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                        ac.StartInfo.FileName = "cmd.exe"
                        ac.StartInfo.Arguments = "/c mstsc /admin /v:" + TextBox1.Text + ""
                        ac.StartInfo.CreateNoWindow = True
                        ac.Start()



                    Else

                        If CheckBox1.CheckState = 1 Then

                            TextBox1.Text = TextBox1.Text

                            Dim p = New Process
                            p.StartInfo.UseShellExecute = True
                            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            p.StartInfo.FileName = "cmd.exe"
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableLUA /t REG_DWORD /d 3 /f"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\NetSh Advfirewall set  allprofiles state enabled"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " %systemroot%\system32\netsh advfirewall set currentprofile state on"
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server"" /v fDenyTSConnections /t REG_DWORD /F /D 0 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\System\CurrentControlSet\Control\Terminal Server\WinStations\RDP-Tcp"" /v UserAuthentication /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorAdmin /t REG_DWORD /F /D 2 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v ConsentPromptBehaviorUser /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v PromptOnSecureDesktop /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableInstallerDetection /t REG_DWORD /F /D 1 "
                            p.Start()
                            p.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " REG ADD ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System"" /v EnableVirtualization /t REG_DWORD /F /D 1 "
                            p.Start()


                            Dim ac = New Process
                            ac.StartInfo.UseShellExecute = True
                            ac.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                            ac.StartInfo.FileName = "cmd.exe"
                            ac.StartInfo.Arguments = "/c mstsc /admin /v:" + TextBox1.Text + ""
                            ac.StartInfo.CreateNoWindow = True
                            ac.Start()

                        End If
                    End If
                Else
                    TextBox1.Text = ""
                    ListBox1.Items.Clear()
                    Label3.Show()

                End If
            Else
                TextBox1.Text = ""
                ListBox1.Items.Clear()
                Label3.Show()
            End If


        Catch ex As Exception
            TextBox1.Text = ""
            ListBox1.Items.Clear()
            Label3.Show()
        End Try
    End Sub

    Private Sub Button3_Click_1(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Label3.Hide()
            ListBox1.Items.Clear()
            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then


                    ' RETORNA MAC-ADRESS NO LISTBOX
                    Dim theManagementScope As New ManagementScope("\\" & TextBox1.Text & "\root\cimv2")
                    Dim theQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"

                    Dim theObjectQuery As New ObjectQuery(theQueryString)

                    Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
                    Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

                    ' RETORNA NOME , IP 

                    Dim strHostName As String
                    strHostName = System.Net.Dns.GetHostEntry(TextBox1.Text).HostName


                    Dim strIPAddress As String
                    strIPAddress = System.Net.Dns.GetHostEntry(TextBox1.Text).AddressList(0).ToString()



                    For Each currentResult As ManagementObject In theResultsCollection
                        ListBox1.Items.Add(currentResult("MacAddress").ToString())
                        ListBox1.Items.Add(strHostName)
                        ListBox1.Items.Add(strIPAddress)
                    Next

                    Dim p = New Process
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.Arguments = "/c  WMIC /NODE:" + TextBox1.Text + " COMPUTERSYSTEM GET USERNAME"
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.Start()



                    ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))

                    ListBox1.Items.RemoveAt(3)

                End If




            Else
                TextBox1.Text = ""
                ListBox1.Items.Clear()
                Label3.Show()
            End If



        Catch ex As Exception
            TextBox1.Text = ""
            ListBox1.Items.Clear()
            Label3.Show()
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Button5.Enabled = False
        Button6.Enabled = True

        Try
            ListBox1.Items.Clear()
            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then



                    ' RETORNA MAC-ADRESS NO LISTBOX
                    Dim theManagementScope As New ManagementScope("\\" & TextBox1.Text & "\root\cimv2")
                    Dim theQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"

                    Dim theObjectQuery As New ObjectQuery(theQueryString)

                    Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
                    Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

                    ' RETORNA NOME , IP 

                    Dim strHostName As String
                    strHostName = System.Net.Dns.GetHostEntry(TextBox1.Text).HostName


                    Dim strIPAddress As String
                    strIPAddress = System.Net.Dns.GetHostEntry(TextBox1.Text).AddressList(0).ToString()



                    For Each currentResult As ManagementObject In theResultsCollection
                        ListBox1.Items.Add(currentResult("MacAddress").ToString())
                        ListBox1.Items.Add(strHostName)
                        ListBox1.Items.Add(strIPAddress)
                    Next


                    'HABILITA USER COMO ADM DA MAQUINA

                    Dim p = New Process
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.Arguments = "/c  WMIC /NODE:" + TextBox1.Text + " COMPUTERSYSTEM GET USERNAME"
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.Start()


                    ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))


                    Dim ps As String = ListBox1.Items(4)


                    Dim p1 = New Process
                    p1.StartInfo.UseShellExecute = True
                    p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p1.StartInfo.FileName = "cmd.exe"
                    p1.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " net localgroup Administradores " + ps + " /add "
                    p1.StartInfo.CreateNoWindow = True
                    p1.Start()

                    ListBox1.Items.RemoveAt(3)


                End If

                Dim adm As String = "USUÁRIO COMO ADMIN HABILITADO"
                ListBox1.Items.Add(adm)


            Else
                TextBox1.Text = ""
                ListBox1.Items.Clear()
                Label3.Show()
            End If



        Catch ex As Exception
            TextBox1.Text = ""
            ListBox1.Items.Clear()
            Label3.Show()
        End Try



    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Button5.Enabled = True
        Button6.Enabled = False

        Try
            ListBox1.Items.Clear()
            If My.Computer.Network.Ping("" + TextBox1.Text + "") Then

                If IO.Directory.Exists("\\" + TextBox1.Text + "\c$") Then



                    ' RETORNA MAC-ADRESS NO LISTBOX
                    Dim theManagementScope As New ManagementScope("\\" & TextBox1.Text & "\root\cimv2")
                    Dim theQueryString = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1"

                    Dim theObjectQuery As New ObjectQuery(theQueryString)

                    Dim theSearcher As New ManagementObjectSearcher(theManagementScope, theObjectQuery)
                    Dim theResultsCollection As ManagementObjectCollection = theSearcher.Get()

                    ' RETORNA NOME , IP 

                    Dim strHostName As String
                    strHostName = System.Net.Dns.GetHostEntry(TextBox1.Text).HostName


                    Dim strIPAddress As String
                    strIPAddress = System.Net.Dns.GetHostEntry(TextBox1.Text).AddressList(0).ToString()



                    For Each currentResult As ManagementObject In theResultsCollection
                        ListBox1.Items.Add(currentResult("MacAddress").ToString())
                        ListBox1.Items.Add(strHostName)
                        ListBox1.Items.Add(strIPAddress)
                    Next


                    'DESABILITA USER COMO ADM DA MAQUINA

                    Dim p = New Process
                    p.StartInfo.UseShellExecute = False
                    p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p.StartInfo.FileName = "cmd.exe"
                    p.StartInfo.Arguments = "/c  WMIC /NODE:" + TextBox1.Text + " COMPUTERSYSTEM GET USERNAME"
                    p.StartInfo.CreateNoWindow = True
                    p.StartInfo.RedirectStandardOutput = True
                    p.Start()

                    ListBox1.Items.AddRange(p.StandardOutput.ReadToEnd.Split(New String() {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries))


                    Dim ps As String = ListBox1.Items(4)


                    Dim p1 = New Process
                    p1.StartInfo.UseShellExecute = True
                    p1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
                    p1.StartInfo.FileName = "cmd.exe"
                    p1.StartInfo.Arguments = "/c  %temp%\psexec.exe \\" + TextBox1.Text + " net localgroup Administradores " + ps + " /del "
                    p1.Start()

                    ListBox1.Items.RemoveAt(3)



                End If

                Dim adm As String = "USUÁRIO COMO ADMIN DESABILITADO"
                ListBox1.Items.Add(adm)



            Else
                TextBox1.Text = ""
                ListBox1.Items.Clear()
                Label3.Show()
            End If



        Catch ex As Exception
            TextBox1.Text = ""
            ListBox1.Items.Clear()
            Label3.Show()
        End Try

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        Clipboard.SetText(String.Join(Environment.NewLine, ListBox1.SelectedItems.Cast(Of String).ToArray))
    End Sub


End Class