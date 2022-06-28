Imports System.Data
Imports System.IO

Public Class FrmLOGIN
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try

            txtLOGIN_SESSION.Text = Now.ToShortDateString

            Dim dtCompany As DataTable = Connection.READDB()
            For i = 0 To dtCompany.Rows.Count - 1
                Dim comp As String = ""
                'comp = dtCompany.Rows(i).Item("DBSource").ToString.TrimEnd
                comp = dtCompany.Rows(i).Item("COMPNAME").ToString.TrimEnd
                txtLOGIN_Company.Items.Add(comp)

            Next


        Catch ex As Exception
            WriteLog("Error 14 (FrmLOGIN.Window_Loaded):" & ex.Message)
        End Try
    End Sub

    Private Sub BTNLOGIN_OK_Click(sender As Object, e As RoutedEventArgs) Handles BTNLOGIN_OK.Click
        Try
            Dim dtUSER As DataTable = Connection.READAUTHOR()
            If dtUSER.Rows.Count > 0 Then
                frmMainWin = New MainWindow
                For j = 0 To dtUSER.Rows.Count - 1
                    If txtLOGIN_USERID.Text.TrimEnd = dtUSER.Rows(j).Item("USER").ToString.TrimEnd Then
                        If txtLOGIN_PASSWORD.Password = dtUSER.Rows(j).Item("PASSWORD").ToString.TrimEnd Then
                            Comp = txtLOGIN_Company.Text

                            Me.Close()
                            frmMainWin.Show()
                            If dtUSER.Rows(j).Item("AUTHOR").ToString.TrimEnd = "ADMIN" Then
                            Else
                                frmMainWin.Author.IsEnabled = False
                                frmMainWin.DBsetup.IsEnabled = False
                            End If

                            Exit For
                        Else
                            MessageBox.Show("Log in failed. Mismatch User Name or Password.")
                        End If
                    End If
                Next
            Else
                MessageBox.Show("Log in failed. Please check authorization setup.")

            End If
        Catch ex As Exception
            WriteLog("Error 55 (FrmLOGIN.BTNLOGIN_OK_Click):" & ex.Message)

        End Try
    End Sub

    Private Sub txtLOGIN_Company_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles txtLOGIN_Company.MouseDown
        Dim dtCompany As DataTable = Connection.READDB()
        For i = 0 To dtCompany.Rows.Count - 1
            Dim comp As String = ""
            ' comp = dtCompany.Rows(i).Item("DBSource").ToString.TrimEnd
            comp = dtCompany.Rows(i).Item("COMPNAME").ToString.TrimEnd
            txtLOGIN_Company.Items.Add(comp)

        Next
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        Me.Close()

    End Sub

    Private Sub BTNLOGIN_CANCEL_Click(sender As Object, e As RoutedEventArgs) Handles BTNLOGIN_CANCEL.Click
        Me.Close()
    End Sub
End Class
