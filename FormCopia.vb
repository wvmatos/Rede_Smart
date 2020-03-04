Public Class FormCopia

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        ProgressBar1.Value = 0

        ListBox1.Items.Clear()
        ListBox2.Items.Clear()



        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then

            TextBox1.BackColor = Color.LightGray
            TextBox2.BackColor = Color.LightGray
            TextBox3.BackColor = Color.LightGray
            TextBox4.BackColor = Color.LightGray
            Label1.ForeColor = Color.DarkRed

        Else
            Label1.ForeColor = Color.DimGray
            TextBox1.BackColor = Color.White
            TextBox2.BackColor = Color.White
            TextBox3.BackColor = Color.White
            TextBox4.BackColor = Color.White

            If TextBox7.Text = "" Then

                fbd1.Description = "Selecione uma pasta para realizar a Copia"
                fbd1.RootFolder = Environment.SpecialFolder.MyComputer
                fbd1.ShowNewFolderButton = True
                fbd1.SelectedPath = True

                'Exibe a caixa de diálogo
                'Dim fbd1 As New OpenFileDialog   --USAR ESTE RECURSO PARA ABRIR ARQUIVOS FILES--

                If fbd1.ShowDialog = Windows.Forms.DialogResult.OK Then

                    'Exibe a pasta selecionada
                    'txtPastas.Text = fbd1.FileName --USAR ESTE RECURSO PARA ABRIR ARQUIVOS FILES--

                    TextBox7.Text = fbd1.SelectedPath


                End If

            Else

                If TextBox5.Text = "" Then
                    ProgressBar1.Value = 0
                    Try

                        Button2.Enabled = False
                        Button4.Enabled = True
                        Button4.Text = "DELETAR"


                        Dim ip As String
                        ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""

                        Dim destino As String

                        If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "", 1000) Then
                            'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA



                            If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "\c$") Then




                                Dim copia As String
                                copia = TextBox7.Text






                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""

                                'agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora
                                destino = "\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "\c$\INSTALL-[" + ip + "]"

                                'Dim hostname As String
                                'hostname = System.Net.Dns.GetHostEntry(ip).HostName
                                My.Computer.FileSystem.CopyDirectory(copia, destino, True)
                                ' My.Computer.FileSystem.CopyFile(copia, destino, True)   --PARA COPIAR O ARQUIVO FILE --

                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""

                                Timer1.Enabled = True
                                ProgressBar1.Increment(100)

                                ListBox1.Items.Add("COPIADO - IP: " + ip)



                                Button2.Text = "FIM"

                            Else

                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                                ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                            End If




                        Else

                            ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                            ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                        End If


                    Catch ex As Exception

                    End Try


                Else



                    Try

                        BackgroundWorker1.RunWorkerAsync()

                    Catch ex As Exception

                    End Try

                End If
            End If
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Define as propriedades do controle FolderBrowserDialog


        fbd1.Description = "Selecione uma pasta para realizar a Copia"
        fbd1.RootFolder = Environment.SpecialFolder.MyComputer
        fbd1.ShowNewFolderButton = True
        fbd1.SelectedPath = True

        'Exibe a caixa de diálogo
        'Dim fbd1 As New OpenFileDialog   --USAR ESTE RECURSO PARA ABRIR ARQUIVOS FILES--

        If fbd1.ShowDialog = Windows.Forms.DialogResult.OK Then

            'Exibe a pasta selecionada
            'txtPastas.Text = fbd1.FileName --USAR ESTE RECURSO PARA ABRIR ARQUIVOS FILES--

            TextBox7.Text = fbd1.SelectedPath


        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        fbd1.SelectedPath += TextBox7.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Button2.Enabled = True
        Button2.Text = "COPIAR"
        Button4.Enabled = True
        Button4.Text = "DELETAR"


        ProgressBar1.Value = 0
        Timer1.Stop()

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        ProgressBar1.Value = 0


        Button2.Enabled = True
        Button2.Text = "COPIAR"

        TextBox7.Text = ""
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()


        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then

            TextBox1.BackColor = Color.LightGray
            TextBox2.BackColor = Color.LightGray
            TextBox3.BackColor = Color.LightGray
            TextBox4.BackColor = Color.LightGray
            Label1.ForeColor = Color.DarkRed

        Else
            Label1.ForeColor = Color.DimGray
            TextBox1.BackColor = Color.White
            TextBox2.BackColor = Color.White
            TextBox3.BackColor = Color.White
            TextBox4.BackColor = Color.White



            If TextBox5.Text = "" Then
                ProgressBar1.Value = 0

                Try

                    Button4.Enabled = False
                    Button2.Enabled = True
                    Button2.Text = "COPIAR"

                    If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "", 1000) Then
                        'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA

                        If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "\c$") Then



                            Dim destino As String


                            'Dim hostname As String


                            'Dim agora As String
                            ' agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora

                            'hostname = System.Net.Dns.GetHostEntry(ip).HostName

                            Dim ip As String

                            ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                            destino = "\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "\c$\INSTALL-[" + ip + "]"

                            If IO.Directory.Exists(destino) Then

                                My.Computer.FileSystem.DeleteDirectory(destino, FileIO.DeleteDirectoryOption.DeleteAllContents)
                                ' My.Computer.FileSystem.CopyFile(copia, destino, True)   --PARA COPIAR O ARQUIVO FILE --

                                ListBox1.Items.Add("DELETADO - IP: " + ip)

                                Timer1.Enabled = True
                                ProgressBar1.Increment(100)

                                Button4.Text = "FIM"

                            Else

                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                                ListBox2.Items.Add("NÃO EXISTE A PASTA - IP: " + ip)

                                Timer1.Enabled = True
                                ProgressBar1.Increment(100)

                            End If

                        Else

                            Dim ip As String
                            ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                            ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                            Timer1.Enabled = True
                            ProgressBar1.Increment(100)


                        End If

                    Else

                        Dim ip As String
                        ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""
                        ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                        Timer1.Enabled = True
                        ProgressBar1.Increment(100)

                    End If

                Catch ex As Exception

                End Try


            Else

                ProgressBar1.Value = 0


                Try

                    BackgroundWorker2.RunWorkerAsync()

                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        'ProgressBar1.Increment(10)
        'If ProgressBar1.Value = 100 Then
        'Timer1.Stop()
        ' End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ProgressBar1.Value = 0

        Button2.Enabled = False
        Button4.Enabled = True
        Button4.Text = "DELETAR"


        Dim i As String = TextBox4.Text
        Dim num As Integer
        i = TextBox4.Text
        num = CInt(i)

        'Converte em inteiro
        Dim text2a As String = TextBox5.Text
        Dim text2 As Integer
        text2a = TextBox5.Text
        text2 = CInt(text2a)

        Dim subr = text2 - num
        Dim total = (text2 - num) / subr

        ' ProgressBar1.Increment(soma)

        Do While i <= text2

            Timer1.Enabled = True
            ProgressBar1.Increment(total)
            ProgressBar1.Maximum = subr


            'Dim agora As String
            'agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora
            Dim ip As String
            ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""

            Dim destino As String

            If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "", 1000) Then
                'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA



                If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "\c$") Then




                    Dim copia As String
                    copia = TextBox7.Text






                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""

                    'agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora
                    destino = "\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "\c$\INSTALL-[" + ip + "]"

                    'Dim hostname As String
                    'hostname = System.Net.Dns.GetHostEntry(ip).HostName
                    My.Computer.FileSystem.CopyDirectory(copia, destino, True)
                    ' My.Computer.FileSystem.CopyFile(copia, destino, True)   --PARA COPIAR O ARQUIVO FILE --

                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""
                    ListBox1.Items.Add("COPIADO - IP: " + ip)




                Else

                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""
                    ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                End If




            Else

                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""
                ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

            End If



            i += 1

        Loop

        Button2.Text = "FIM"
    End Sub

    Private Sub FormCopia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub BackgroundWorker2_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker2.DoWork

        Button4.Enabled = False
        Button2.Enabled = True
        Button2.Text = "COPIAR"


        Dim i As String = TextBox4.Text
        Dim num As Integer
        i = TextBox4.Text
        num = CInt(i)

        'Converte em inteiro
        Dim text2a As String = TextBox5.Text
        Dim text2 As Integer
        text2a = TextBox5.Text
        text2 = CInt(text2a)


        Dim subr = text2 - num
        Dim total = (text2 - num) / subr


        Do While i <= text2


            Timer1.Enabled = True
            ProgressBar1.Increment(total)
            ProgressBar1.Maximum = subr




            'Dim agora As String
            'agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora
            Dim ip As String
            ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""

            Dim destino As String

            If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "", 1000) Then
                'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA



                If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "\c$") Then



                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""

                    'agora = (DateTime.Now.ToString("dd-MM-yyyy")) para data pegue esta variavel agora
                    destino = "\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "\c$\INSTALL-[" + ip + "]"

                    'Dim hostname As String
                    'hostname = System.Net.Dns.GetHostEntry(ip).HostName
                    If IO.Directory.Exists(destino) Then

                        My.Computer.FileSystem.DeleteDirectory(destino, FileIO.DeleteDirectoryOption.DeleteAllContents)
                        ' My.Computer.FileSystem.CopyFile(copia, destino, True)   --PARA COPIAR O ARQUIVO FILE --

                        ListBox1.Items.Add("DELETADO - IP: " + ip)

                    End If




                Else

                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""
                    ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

                End If




            Else

                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""
                ListBox2.Items.Add("FORA DA REDE - IP: " + ip)

            End If



            i += 1

        Loop


        Button4.Text = "FIM"


    End Sub


End Class