Public Class FormDesligar
    Private Sub FormDesligar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Label4.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ProgressBar1.Value = 0
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()


        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            Label4.Show()


        Else




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

                        Button2.Enabled = False



                        Dim ip As String
                        ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""



                        If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "", 1000) Then
                            'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA

                            Dim resultado
                            resultado = MsgBox("DESEJA PROSSEGUIR ?", vbYesNo, "SE CONTINUAR, VAI DESLIGAR A MAQUINA !")

                            If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + "\c$") And resultado = vbYes Then



                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""

                                Shell("shutdown -s -f -m \\" + ip + " -c ""SEU PC SERA DESLIGADO EM 30 SEGUNDOS !"" ")

                                ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + TextBox4.Text + ""

                                Timer1.Enabled = True
                                ProgressBar1.Increment(100)

                                ListBox1.Items.Add("PC DESLIGADO - IP: " + ip)



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

                        Dim resultado
                        resultado = MsgBox("DESEJA PROSSEGUIR ?", vbYesNo, "SE CONTINUAR, VAI DESLIGAR AS MAQUINAS !")

                        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" And resultado = vbYes Then
                            Label4.Show()



                        Else

                            BackgroundWorker1.RunWorkerAsync()
                        End If

                    Catch ex As Exception

                    End Try

                End If

            End If

        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        ProgressBar1.Value = 0

        Button2.Enabled = False



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



            If My.Computer.Network.Ping("" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "", 1000) Then
                'MsgBox("pingando", vbExclamation, "AVISO") AVISO NA TELA



                If IO.Directory.Exists("\\" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + "\c$") Then




                    ip = "" + TextBox1.Text + "." + TextBox2.Text + "." + TextBox3.Text + "." + i + ""

                    Shell("shutdown -s -f -m \\" + ip + " -c ""SEU PC SERA DESLIGADO EM 30 SEGUNDOS !"" ")

                    ListBox1.Items.Add("PC DESLIGADO - IP: " + ip)




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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Button2.Enabled = True
        Button2.Text = "DESLIGAR PC"
        ProgressBar1.Value = 0

    End Sub
End Class