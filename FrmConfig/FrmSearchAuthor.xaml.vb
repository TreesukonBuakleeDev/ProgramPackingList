Imports System.Data
Public Class FrmSearchAuthor
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        txtMasterSearch_by.Text = "ID"
        BTN_AUTHCheckBox.IsChecked = True

    End Sub

    Private Sub txtAUTHSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtAUTHSearch_Text.TextChanged
        Try


            Dim dtAUTHOR As DataTable = New DataTable

            dtAUTHOR = Connection.READAUTHOR()

            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            If txtAUTHSearch_Text.Text.TrimEnd = "" Then
                DGV_AUTHSEARCH.ItemsSource = dtAUTHOR.DefaultView
            Else
                Select Case txtMasterSearch_by.Text
                Case "ID"

                    ROWFilter = dtAUTHOR.[Select]("ID =  '" & txtAUTHSearch_Text.Text & "' ")

                Case "NAME"

                    Select Case txtAUTHSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtAUTHOR.[Select]("USER LIKE  '" & txtAUTHSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtAUTHOR.[Select]("USER LIKE  '%" & txtAUTHSearch_Text.Text & "%'")

                    End Select

            End Select


            dtFilter = dtAUTHOR.Clone
            dtFilter.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtFilter.ImportRow(rowF)
                Next
            End If


                DGV_AUTHSEARCH.ItemsSource = dtFilter.DefaultView

            End If


            With DGV_AUTHSEARCH

                .Columns(0).Header = "ID"
                .Columns(1).Header = "USER"

            End With

            With DGV_AUTHSEARCH
                .Columns(0).Width = 200
                .Columns(1).Width = 300
            End With

        Catch ex As Exception
            WriteLog("Error 110 txtAUTHSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_AUTHSEARCH_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DGV_AUTHSEARCH.SelectionChanged
        Try
            Dim cell As DataGridCellInfo = DGV_AUTHSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim ITEMCurr As String = cell.Item(0).ToString

            DGV_AUTHSEARCH.Items.IndexOf(cell.Item)

            DGV_AUTHSEARCH.Focus()

            Me.Close()

            frmAUTH.txtAuthorUserID.Text = ITEMCurr.TrimEnd
            frmAUTH.txtAuthorUser.Text = cell.Item(1).ToString.TrimEnd
            frmAUTH.txtAuthorPassword.Password = cell.Item(2).ToString.TrimEnd
            frmAUTH.txtAuthorized.Text = cell.Item(3).ToString

        Catch ex As Exception
            WriteLog("Error 99 DGV_AUTHSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_FILTERID_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERID.Click
        txtMasterSearch_by.Text = "ID"
    End Sub

    Private Sub BTN_FILTERNAME_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERNAME.Click
        txtMasterSearch_by.Text = "NAME"
    End Sub

    Private Sub CBXSEARCHAUTHOR_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHAUTHOR_STARTWITH.Click
        txtAUTHSearch_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHAUTHOR_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHAUTHOR_CONTAINWITH.Click
        txtAUTHSearch_Condition.Text = "CONTAIN WITH"
    End Sub
End Class
