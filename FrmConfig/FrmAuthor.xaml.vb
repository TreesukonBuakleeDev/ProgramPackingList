Imports System.Data
Public Class FrmAuthor

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        txtConfirmPass.Visibility = Visibility.Hidden
        lbcONFIRM.Visibility = Visibility.Hidden

    End Sub
    Private Sub BTN_BACKEND_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_BACKEND.MouseDown
        Try
            Dim DTAPP As DataTable = Connection.READAUTHOR
            DTAPP.DefaultView.Sort = "ID ASC"
            DTAPP = DTAPP.DefaultView.ToTable
            txtAuthorUserID.Text = DTAPP.Rows(0).Item("ID").ToString
            txtAuthorUser.Text = DTAPP.Rows(0).Item("USER").ToString
            txtAuthorPassword.Password = DTAPP.Rows(0).Item("PASSWORD").ToString
            txtAuthorized.Text = DTAPP.Rows(0).Item("AUTHOR").ToString
        Catch ex As Exception
            WriteLog("Error 17 (BTN_BACKEND_MouseDown) :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_BACK_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_BACK.MouseDown
        Try
            Dim DTAPP As DataTable = Connection.READAUTHOR
            DTAPP.DefaultView.Sort = "ID ASC"
            DTAPP = DTAPP.DefaultView.ToTable
            If DTAPP.Rows.Count > 0 Then
                For i = 0 To DTAPP.Rows.Count - 1
                    If txtAuthorUserID.Text = "" Then
                        txtAuthorUserID.Text = DTAPP.Rows(0).Item("ID").ToString.TrimEnd
                        txtAuthorUser.Text = DTAPP.Rows(0).Item("USER").ToString.TrimEnd
                        txtAuthorPassword.Password = DTAPP.Rows(0).Item("PASSWORD").ToString.TrimEnd
                        txtAuthorized.Text = DTAPP.Rows(0).Item("AUTHOR").ToString.TrimEnd
                    Else
                        'Dim index As Integer
                        Dim rowIndex = DTAPP.AsEnumerable().[Select](Function(r) r.Field(Of Integer)("ID")).ToList().FindIndex(Function(col) col = CInt(txtAuthorUserID.Text))
                        'MessageBox.Show(rowIndex)

                        Select Case rowIndex - 1
                            Case Is < DTAPP.Rows.Count - 1
                                txtAuthorUserID.Text = DTAPP.Rows(rowIndex - 1).Item("ID").ToString.TrimEnd
                                txtAuthorUser.Text = DTAPP.Rows(rowIndex - 1).Item("USER").ToString.TrimEnd
                                txtAuthorPassword.Password = DTAPP.Rows(rowIndex - 1).Item("PASSWORD").ToString.TrimEnd
                                txtAuthorized.Text = DTAPP.Rows(rowIndex - 1).Item("AUTHOR").ToString.TrimEnd
                            Case Is = DTAPP.Rows.Count - 1
                                txtAuthorUserID.Text = DTAPP.Rows(rowIndex + 1).Item("ID").ToString.TrimEnd
                                txtAuthorUser.Text = DTAPP.Rows(rowIndex + 1).Item("USER").ToString.TrimEnd
                                txtAuthorPassword.Password = DTAPP.Rows(rowIndex + 1).Item("PASSWORD").ToString.TrimEnd
                                txtAuthorized.Text = DTAPP.Rows(rowIndex + 1).Item("AUTHOR").ToString.TrimEnd

                            Case Else
                                txtAuthorUserID.Text = DTAPP.Rows(0).Item("ID").ToString.TrimEnd
                                txtAuthorUser.Text = DTAPP.Rows(0).Item("USER").ToString.TrimEnd
                                txtAuthorPassword.Password = DTAPP.Rows(0).Item("PASSWORD").ToString.TrimEnd
                                txtAuthorized.Text = DTAPP.Rows(0).Item("AUTHOR").ToString.TrimEnd

                        End Select
                    End If
                    Exit For
                Next

            Else
                MessageBox.Show("Records Not found")

            End If
        Catch ex As Exception
            WriteLog("Error 57 (BTN_AuthorNext_Click) :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_NEXT_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_NEXT.MouseDown

        Try
            Dim DTAPP As DataTable = Connection.READAUTHOR
            DTAPP.DefaultView.Sort = "ID ASC"
            DTAPP = DTAPP.DefaultView.ToTable
            If DTAPP.Rows.Count > 0 Then
                For i = 0 To DTAPP.Rows.Count - 1
                    If txtAuthorUserID.Text = "" Then
                        txtAuthorUserID.Text = DTAPP.Rows(0).Item("ID").ToString
                        txtAuthorUser.Text = DTAPP.Rows(0).Item("USER").ToString
                        txtAuthorPassword.Password = DTAPP.Rows(0).Item("PASSWORD").ToString
                        txtAuthorized.Text = DTAPP.Rows(0).Item("AUTHOR").ToString
                    Else
                        'Dim index As Integer
                        Dim rowIndex = DTAPP.AsEnumerable().[Select](Function(r) r.Field(Of Integer)("ID")).ToList().FindIndex(Function(col) col = CInt(txtAuthorUserID.Text))
                        'MessageBox.Show(rowIndex)

                        Select Case rowIndex + 1
                            Case Is < DTAPP.Rows.Count - 1
                                txtAuthorUserID.Text = DTAPP.Rows(rowIndex + 1).Item("ID").ToString
                                txtAuthorUser.Text = DTAPP.Rows(rowIndex + 1).Item("USER").ToString
                                txtAuthorPassword.Password = DTAPP.Rows(rowIndex + 1).Item("PASSWORD").ToString
                                txtAuthorized.Text = DTAPP.Rows(rowIndex + 1).Item("AUTHOR").ToString
                            Case Is = DTAPP.Rows.Count - 1
                                txtAuthorUserID.Text = DTAPP.Rows(rowIndex + 1).Item("ID").ToString
                                txtAuthorUser.Text = DTAPP.Rows(rowIndex + 1).Item("USER").ToString
                                txtAuthorPassword.Password = DTAPP.Rows(rowIndex + 1).Item("PASSWORD").ToString
                                txtAuthorized.Text = DTAPP.Rows(rowIndex + 1).Item("AUTHOR").ToString

                            Case Else
                                txtAuthorUserID.Text = DTAPP.Rows(0).Item("ID").ToString
                                txtAuthorUser.Text = DTAPP.Rows(0).Item("USER").ToString
                                txtAuthorPassword.Password = DTAPP.Rows(0).Item("PASSWORD").ToString
                                txtAuthorized.Text = DTAPP.Rows(0).Item("AUTHOR").ToString

                        End Select
                    End If
                    Exit For
                Next

            Else
                MessageBox.Show("Records Not found")

            End If
        Catch ex As Exception
            WriteLog("Error 57 (BTN_AuthorNext_Click) :" & ex.Message)
        End Try

    End Sub

    Private Sub BTN_NEXTEND_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_NEXTEND.MouseDown
        Try
            Dim DTAPP As DataTable = Connection.READAUTHOR
            DTAPP.DefaultView.Sort = "ID ASC"
            DTAPP = DTAPP.DefaultView.ToTable
            txtAuthorUserID.Text = DTAPP.Rows(DTAPP.Rows.Count - 1).Item("ID").ToString
            txtAuthorUser.Text = DTAPP.Rows(DTAPP.Rows.Count - 1).Item("USER").ToString
            txtAuthorPassword.Password = DTAPP.Rows(DTAPP.Rows.Count - 1).Item("PASSWORD").ToString
            txtAuthorized.Text = DTAPP.Rows(DTAPP.Rows.Count - 1).Item("AUTHOR").ToString
        Catch ex As Exception
            WriteLog("Error 127 (BTN_NEXTEND_MouseDown) :" & ex.Message)
        End Try
    End Sub
    Private Sub BTNAUTH_SAVE_Click(sender As Object, e As RoutedEventArgs) Handles BTNAUTH_SAVE.Click
        Try
            Dim DTAPP As DataTable = Connection.READAUTHOR
            DTAPP.DefaultView.Sort = "ID ASC"
            'DTAPP = DTAPP.DefaultView.ToTable
            Dim USERID_NEW As String = ""
            Call Connection.SAVEAUTHOR(DTAPP, txtAuthorUserID.Text, txtAuthorUser.Text, txtAuthorPassword.Password, txtConfirmPass.Password, txtAuthorized.Text, USERID_NEW)
            txtAuthorUserID.Text = USERID_NEW
        Catch ex As Exception
            WriteLog("Error 137 (BTNAUTH_SAVE_MouseDown) :" & ex.Message)
        End Try
    End Sub
    Private Sub BTNAUTH_DELETE_Click(sender As Object, e As RoutedEventArgs) Handles BTNAUTH_DELETE.Click
        Try
            Dim dialogOK As MessageBoxButton = MsgBox("Do you want to delete this user ?", MessageBoxButton.YesNo)
            If dialogOK = 6 Then
                Dim DTAPP As DataTable = Connection.READAUTHOR
                For i = 0 To DTAPP.Rows.Count - 1
                    If DTAPP.Rows(i).Item("ID").ToString.TrimEnd = txtAuthorUserID.Text.TrimEnd Then
                        DTAPP.Rows(i).Delete()
                    End If
                Next
                Dim USERID_NEW As String = ""
                Call Connection.SAVEAUTHOR(DTAPP, txtAuthorUserID.Text, "", "", "", "", USERID_NEW)
                ClearTEXT()

            End If
        Catch ex As Exception
            WriteLog("Error 167 (BTNAUTH_DELETE_Click) :" & ex.Message)
        End Try
    End Sub

    Sub ClearTEXT()
        txtAuthorUserID.Text = ""
        txtAuthorUser.Text = ""
        txtAuthorPassword.Password = ""
        txtAuthorized.Text = ""
    End Sub

    Private Sub BTNAUTH_NEW_Click(sender As Object, e As RoutedEventArgs) Handles BTNAUTH_NEW.Click
        ClearTEXT()
        txtAuthorUserID.Text = "***NEW***"
        txtConfirmPass.Visibility = Visibility.Visible
        lbcONFIRM.Visibility = Visibility.Visible
    End Sub

    Private Sub txtAuthorUserID_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtAuthorUserID.TextChanged
        If txtAuthorUserID.Text.TrimEnd = "***NEW***" Then
            txtConfirmPass.Visibility = True
            txtAuthorPassword.Password = ""
        Else
            txtConfirmPass.Visibility = False
            txtAuthorPassword.Password = ""
        End If
    End Sub

    Private Sub BTN_SEARCHAUTH_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_SEARCHAUTH.MouseDown
        Call DISPLAYAUTHOR()
    End Sub

    Public Sub DISPLAYAUTHOR()
        Try
            Dim frmSearchAUTH As New FrmSearchAuthor

            Call frmSearchAUTH.Show()

            Dim dtAUTHOR As DataTable = New DataTable

            dtAUTHOR = Connection.READAUTHOR()

            frmSearchAUTH.DGV_AUTHSEARCH.ItemsSource = dtAUTHOR.DefaultView

            With frmSearchAUTH.DGV_AUTHSEARCH

                .Columns(2).Visibility = Visibility.Hidden
                .Columns(3).Visibility = Visibility.Hidden

            End With


            'dtORDERTEMP = dtORDERNO.Copy

            frmSearchAUTH.txtAUTHSearch_Condition.Text = "START WITH"

            frmSearchAUTH.txtAUTHSearch_Text.Text = txtAuthorUserID.Text

        Catch ex As Exception
            WriteLog("Error 205 DISPLAYAUTHOR() :" & ex.Message)
        End Try

    End Sub
End Class
