Imports System.Data
Public Class FrmSearchDB
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        txtDBSearch_by.Text = "ID"
        BTN_DBCheckBox.IsChecked = True
    End Sub

    Private Sub txtDBSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtDBSearch_Text.TextChanged
        Try


            Dim dtDB As DataTable = New DataTable

            dtDB = Connection.READDB()

            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            If txtDBSearch_Text.Text.TrimEnd = "" Then
                DGV_DBSEARCH.ItemsSource = dtDB.DefaultView
            Else
                Select Case txtDBSearch_by.Text
                    Case "ID"

                        ROWFilter = dtDB.[Select]("ID =  '" & txtDBSearch_Text.Text & "' ")

                    Case "NAME"

                        Select Case txtDBSearch_Condition.Text
                            Case "START WITH"
                                ROWFilter = dtDB.[Select]("COMPNAME LIKE  '" & txtDBSearch_Text.Text & "%'")
                            Case "CONTAIN WITH"
                                ROWFilter = dtDB.[Select]("COMPNAME LIKE  '%" & txtDBSearch_Text.Text & "%'")

                        End Select

                End Select


                dtFilter = dtDB.Clone
                dtFilter.NewRow()

                If ROWFilter Is Nothing = False Then
                    For Each rowF As DataRow In ROWFilter
                        dtFilter.ImportRow(rowF)
                    Next
                End If


                DGV_DBSEARCH.ItemsSource = dtFilter.DefaultView

            End If


            With DGV_DBSEARCH

                .Columns(0).Header = "ID"

                .Columns(7).Header = "USER"

            End With

            With DGV_DBSEARCH

                .Columns(1).Visibility = Visibility.Hidden
                .Columns(2).Visibility = Visibility.Hidden
                .Columns(3).Visibility = Visibility.Hidden
                .Columns(4).Visibility = Visibility.Hidden
                .Columns(5).Visibility = Visibility.Hidden
                .Columns(6).Visibility = Visibility.Hidden
            End With

            With DGV_DBSEARCH
                .Columns(0).Width = 200
                .Columns(7).Width = 300
            End With

        Catch ex As Exception
            WriteLog("Error 110 txtDBSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_DBSEARCH_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DGV_DBSEARCH.SelectionChanged
        Try
            Dim cell As DataGridCellInfo = DGV_DBSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim ITEMCurr As String = cell.Item(0).ToString

            DGV_DBSEARCH.Items.IndexOf(cell.Item)

            DGV_DBSEARCH.Focus()

            Me.Close()

            frmDb.txtDBID.Text = cell.Item(0).ToString.TrimEnd
            frmDb.Acc_Company.Text = cell.Item(1).ToString.TrimEnd
            frmDb.Acc_UserID.Text = cell.Item(2).ToString.TrimEnd
            frmDb.Acc_Password.Password = cell.Item(3).ToString.TrimEnd
            frmDb.Acc_version.Text = cell.Item(4).ToString.TrimEnd

            frmDb.txtServer.Text = cell.Item(1).ToString.TrimEnd
            frmDb.txtUser.Text = cell.Item(2).ToString.TrimEnd
            frmDb.txtPassword.Password = cell.Item(3).ToString.TrimEnd
            frmDb.txtDB.Text = cell.Item(5).ToString.TrimEnd


            If cell.Item(6).ToString.TrimEnd = 1 Then
                BTN_DBCheckBox.IsChecked = True
            Else
                BTN_DBCheckBox.IsChecked = False
            End If

            frmDb.Acc_CompNAME.Text = cell.Item(7).ToString.TrimEnd

        Catch ex As Exception
            WriteLog("Error 99 DGV_DBSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNDB_FILTERID_Click(sender As Object, e As RoutedEventArgs) Handles BTNDB_FILTERID.Click
        txtDBSearch_by.Text = "ID"
    End Sub

    Private Sub BTNDB_FILTERNAME_Click(sender As Object, e As RoutedEventArgs) Handles BTNDB_FILTERNAME.Click
        txtDBSearch_by.Text = "NAME"
    End Sub

    Private Sub CBXSEARCHDB_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHDB_STARTWITH.Click
        txtDBSearch_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHDB_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHDB_CONTAINWITH.Click
        txtDBSearch_Condition.Text = "CONTAIN WITH"
    End Sub
End Class
