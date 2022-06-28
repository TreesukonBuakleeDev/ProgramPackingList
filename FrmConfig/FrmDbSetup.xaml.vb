Imports System.Data
Public Class FrmDbSetup


#Region "EVENT"
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try

            'Call Connection.ReadConfig(dtConfigDB)
            dtConfigDB = Connection.READDB()

        Catch ex As Exception
            WriteLog("Error 11 FrmDbSetup.LOAD :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_SAVE_Click(sender As Object, e As RoutedEventArgs) Handles BTNDB_SAVE.Click
        'RetrivedTextbox()
        'Call Connection.SaveConfigDB()

        'READ DB
        Dim dtConfigDB As DataTable = Connection.READDB()
        'SAVE CONFIG
        Call SAVEDB(dtConfigDB)


    End Sub

    Sub RetrivedTextbox()
        CompanyName = Acc_Company.Text
        SageVersion = Acc_version.Text
        UserName = Acc_UserID.Text
        PassWord = Acc_Password.Password
        ServerName = txtServer.Text
        DB = txtDB.Text
        User = txtUser.Text
        Pass = txtPassword.Password
        ImportPath = txtImportPath.Text
        ExportPath = txtExportPath.Text

    End Sub

    Private Sub BTNDB_BACKEND_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNDB_BACKEND.MouseDown
        Try
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            Dim dtConfigDB As DataTable = Connection.READDB()

            If txtDBID.Text.TrimEnd = "" Then
            Else

            End If


            For i = 0 To dtConfigDB.Rows.Count - 1
                txtDBID.Text = dtConfigDB.Rows(0).Item("ID").ToString.TrimEnd
                Acc_Company.Text = dtConfigDB.Rows(0).Item("SERVER").ToString.TrimEnd
                Acc_version.Text = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                Acc_UserID.Text = dtConfigDB.Rows(0).Item("USER").ToString.TrimEnd
                Acc_Password.Password = dtConfigDB.Rows(0).Item("PASSWORD").ToString.TrimEnd

                txtServer.Text = dtConfigDB.Rows(0).Item("SERVER").ToString.TrimEnd
                txtDB.Text = dtConfigDB.Rows(0).Item("DBAPP").ToString.TrimEnd
                txtUser.Text = dtConfigDB.Rows(0).Item("USER").ToString.TrimEnd
                txtPassword.Password = dtConfigDB.Rows(0).Item("PASSWORD").ToString.TrimEnd
                If dtConfigDB.Rows(0).Item("AUTHOR").ToString.TrimEnd = 1 Then
                    BTNAUTHEN_YES.IsChecked = True
                Else
                    BTNAUTHEN_YES.IsChecked = False
                End If

                Acc_CompNAME.Text = dtConfigDB.Rows(0).Item("CompNAME").ToString.TrimEnd

                Exit Sub
            Next

        Catch ex As Exception
            WriteLog("Error 85 BTNDB_BACKEND_MouseEnter() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNdb_BACK_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNdb_BACK.MouseDown
        Try
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTDBID As String = ""
            Dim dtConfigDB As DataTable = Connection.READDB()

            NEXTDBID = GETNEXTDBID(dtConfigDB, "<")

            For i = 0 To dtConfigDB.Rows.Count - 1
                If NEXTDBID.TrimEnd = dtConfigDB.Rows(i).Item("ID").ToString.TrimEnd Then
                    txtDBID.Text = dtConfigDB.Rows(i).Item("ID").ToString.TrimEnd
                    Acc_Company.Text = dtConfigDB.Rows(i).Item("SERVER").ToString.TrimEnd
                    Acc_version.Text = dtConfigDB.Rows(i).Item("DBSource").ToString.TrimEnd
                    Acc_UserID.Text = dtConfigDB.Rows(i).Item("USER").ToString.TrimEnd
                    Acc_Password.Password = dtConfigDB.Rows(i).Item("PASSWORD").ToString.TrimEnd

                    txtServer.Text = dtConfigDB.Rows(i).Item("SERVER").ToString.TrimEnd
                    txtDB.Text = dtConfigDB.Rows(i).Item("DBAPP").ToString.TrimEnd
                    txtUser.Text = dtConfigDB.Rows(i).Item("USER").ToString.TrimEnd
                    txtPassword.Password = dtConfigDB.Rows(i).Item("PASSWORD").ToString.TrimEnd

                    If dtConfigDB.Rows(i).Item("AUTHOR").ToString.TrimEnd = 1 Then
                        BTNAUTHEN_YES.IsChecked = True
                    Else
                        BTNAUTHEN_YES.IsChecked = False
                    End If
                    Acc_CompNAME.Text = dtConfigDB.Rows(i).Item("CompNAME").ToString.TrimEnd
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            WriteLog("Error 100 BTNdb_BACK_MouseEnter() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_NEXT_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNDB_NEXT.MouseDown
        Try
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTDBID As String = ""
            Dim dtConfigDB As DataTable = Connection.READDB()

            NEXTDBID = GETNEXTDBID(dtConfigDB, ">")

            For i = 0 To dtConfigDB.Rows.Count - 1
                If NEXTDBID.TrimEnd = dtConfigDB.Rows(i).Item("ID").ToString.TrimEnd Then
                    txtDBID.Text = dtConfigDB.Rows(i).Item("ID").ToString.TrimEnd
                    Acc_Company.Text = dtConfigDB.Rows(i).Item("SERVER").ToString.TrimEnd
                    Acc_version.Text = dtConfigDB.Rows(i).Item("DBSource").ToString.TrimEnd
                    Acc_UserID.Text = dtConfigDB.Rows(i).Item("USER").ToString.TrimEnd
                    Acc_Password.Password = dtConfigDB.Rows(i).Item("PASSWORD").ToString.TrimEnd

                    txtServer.Text = dtConfigDB.Rows(i).Item("SERVER").ToString.TrimEnd
                    txtDB.Text = dtConfigDB.Rows(i).Item("DBAPP").ToString.TrimEnd
                    txtUser.Text = dtConfigDB.Rows(i).Item("USER").ToString.TrimEnd
                    txtPassword.Password = dtConfigDB.Rows(i).Item("PASSWORD").ToString.TrimEnd
                    If dtConfigDB.Rows(i).Item("AUTHOR").ToString.TrimEnd = 1 Then
                        BTNAUTHEN_YES.IsChecked = True
                    Else
                        BTNAUTHEN_YES.IsChecked = False
                    End If
                    Acc_CompNAME.Text = dtConfigDB.Rows(i).Item("CompNAME").ToString.TrimEnd
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            WriteLog("Error 100 BTNdb_BACK_MouseEnter() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_NEXTEND_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNDB_NEXTEND.MouseDown
        Try
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            Dim dtConfigDB As DataTable = Connection.READDB()



            For i = 0 To dtConfigDB.Rows.Count - 1

                txtDBID.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("ID").ToString.TrimEnd
                Acc_Company.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("SERVER").ToString.TrimEnd
                Acc_version.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("DBSource").ToString.TrimEnd
                Acc_UserID.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("USER").ToString.TrimEnd
                Acc_Password.Password = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("PASSWORD").ToString.TrimEnd

                txtServer.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("SERVER").ToString.TrimEnd
                txtDB.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("DBAPP").ToString.TrimEnd
                txtUser.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("USER").ToString.TrimEnd
                txtPassword.Password = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("PASSWORD").ToString.TrimEnd
                If dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("AUTHOR").ToString.TrimEnd = 1 Then
                    BTNAUTHEN_YES.IsChecked = True
                Else
                    BTNAUTHEN_YES.IsChecked = False
                End If
                Acc_CompNAME.Text = dtConfigDB.Rows(dtConfigDB.Rows.Count - 1).Item("CompNAME").ToString.TrimEnd

                Exit For

            Next

        Catch ex As Exception
            WriteLog("Error 183 BTNDB_NEXTEND_MouseEnter() :" & ex.Message)
        End Try
    End Sub


    Public Function GETNEXTDBID(ByVal DT As DataTable, ByVal CONDITION As String) As String
        Dim NEXTID As String = ""

        For I = 0 To DT.Rows.Count - 1
            If DT.Rows.Count > 1 Then
                Select Case CONDITION
                    Case "<"
                        If txtDBID.Text.TrimEnd = DT.Rows(I).Item("ID").ToString.TrimEnd Then
                            NEXTID = DT.Rows(I - 1).Item("ID").ToString.TrimEnd
                            Exit For
                        End If
                    Case ">"

                        If txtDBID.Text.TrimEnd = DT.Rows(I).Item("ID").ToString.TrimEnd Then

                            NEXTID = DT.Rows(I + 1).Item("ID").ToString.TrimEnd
                            Exit For
                        End If


                End Select
            Else
                NEXTID = DT.Rows(I).Item("ID").ToString.TrimEnd
                Exit For
            End If
        Next

        Return NEXTID
    End Function

    Public Sub BTNDB_NEW_Click(sender As Object, e As RoutedEventArgs) Handles BTNDB_NEW.Click
        Try
            ClearTEXTDB()
            txtDBID.Text = "***NEW***"

        Catch ex As Exception
            WriteLog("Error 225 BTNDB_NEW_Click() :" & ex.Message)
        End Try
    End Sub

    Sub ClearTEXTDB()
        Try
            txtDBID.Text = ""
            Acc_Company.Text = ""
            Acc_CompNAME.Text = ""
            Acc_Password.Password = ""
            Acc_UserID.Text = ""
            Acc_version.Text = ""

            txtServer.Text = ""
            txtDB.Text = ""
            txtUser.Text = ""
            txtPassword.Password = ""

            BTNAUTHEN_YES.IsChecked = False


        Catch ex As Exception
            WriteLog("Error 240 ClearTEXT() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_DELETE_Click(sender As Object, e As RoutedEventArgs) Handles BTNDB_DELETE.Click
        Try
            Dim dialogOK As MessageBoxButton = MsgBox("Do you want to delete this Company ?", MessageBoxButton.YesNo)
            If dialogOK = 6 Then
                Dim DTDEL As DataTable = New DataTable
                DTDEL = dtConfigDB.Clone
                DTDEL = dtConfigDB.Copy

                For I = 0 To DTDEL.Rows.Count - 1
                    If txtDBID.Text.TrimEnd = DTDEL.Rows(I).Item("ID").ToString.TrimEnd Then
                        DTDEL.Rows(I).Delete()
                    End If
                Next

                Call Connection.SAVEDB(DTDEL)
                ClearTEXTDB()
                dtConfigDB = Nothing
            End If
        Catch ex As Exception
            WriteLog("Error 270 BTNDB_DELETE_Click() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_SEARCHID_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNDB_SEARCHID.MouseDown
        Call DISPLAYDB()
    End Sub
    Public Sub DISPLAYDB()
        Try
            Dim frmSearchDB As New FrmSearchDB

            Call frmSearchDB.Show()

            Dim dtDB As DataTable = New DataTable

            dtDB = Connection.READDB()

            frmSearchDB.DGV_DBSEARCH.ItemsSource = dtDB.DefaultView

            frmSearchDB.txtDBSearch_Condition.Text = "START WITH"

            frmSearchDB.txtDBSearch_Text.Text = txtDBID.Text

        Catch ex As Exception
            WriteLog("Error 305 DISPLAYDB() :" & ex.Message)
        End Try

    End Sub



#End Region

End Class
