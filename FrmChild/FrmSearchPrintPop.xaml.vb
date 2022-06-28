Imports System.Data
Public Class FrmSearchPrintPop
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Topmost = True
            txtPrintSearch_Condition.Text = "CONTAIN WITH"
            txtPrintSearch_by.Text = "Order No"
            BTN_PRINTCheckBox.IsChecked = True

        Catch ex As Exception
            WriteLog("Error 13 FrmSearchPrintPop.Window_Loaded() :" & ex.Message)
        End Try
    End Sub

    Private Sub txtPRINTSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtPRINTSearch_Text.TextChanged
        Try
            Dim frmMaster As New FrmMasterItem
            Dim dtORDERNO As DataTable = New DataTable

            dtORDERNO = MASTER.GETSEARCHPRINT()


            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            Select Case txtPrintSearch_by.Text
                Case "Order No"
                    Select Case txtPrintSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDNUMBER LIKE  '" & txtPRINTSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDNUMBER LIKE  '%" & txtPRINTSearch_Text.Text & "%'")
                    End Select
                Case "Order Date"
                    Select Case txtPrintSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDDATE LIKE  '" & txtPRINTSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDDATE LIKE  '%" & txtPRINTSearch_Text.Text & "%'")
                    End Select
                Case "Customer Code"
                    Select Case txtPrintSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("CUSTOMER LIKE  '" & txtPRINTSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("CUSTOMER LIKE  '%" & txtPRINTSearch_Text.Text & "%'")
                    End Select

            End Select


            dtFilter = dtORDERNO.Clone
            dtFilter.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtFilter.ImportRow(rowF)
                Next
            Else
                dtFilter = dtORDERNO.Copy
            End If

            'DGV_MASTERSEARCH.ItemsSource = Nothing

            DGV_PRINTSEARCH.ItemsSource = dtFilter.DefaultView

            With DGV_PRINTSEARCH

                .Columns(0).Header = "Order No."
                .Columns(1).Header = "Order date"
                .Columns(2).Header = "Customer Code"
                .Columns(3).Header = "Customer Name"

            End With



        Catch ex As Exception
            WriteLog("Error 77 txtPRINTSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_PRINTSEARCH_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles DGV_PRINTSEARCH.SelectedCellsChanged
        Try
            Dim cell As DataGridCellInfo = DGV_PRINTSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim OrdernoCurr As String = cell.Item(0).ToString

            Me.Close()
            If VFrom = True Then
                frmPrintPop.txtPrint_From.Text = OrdernoCurr
            Else
                frmPrintPop.txtPrint_To.Text = OrdernoCurr
            End If

        Catch ex As Exception
            WriteLog("Error 105 DGV_MAINSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_FILTERORDERNO_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERNO.Click
        txtPrintSearch_by.Text = "Order No"
    End Sub

    Private Sub BTN_FILTERORDERDATE_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERDATE.Click
        txtPrintSearch_by.Text = "Order Date"
    End Sub

    Private Sub BTN_FILTERIDCUST_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERIDCUST.Click
        txtPrintSearch_by.Text = "Customer Code"
    End Sub

    Private Sub CBXSEARCHPRINTPOP_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHPRINTPOP_CONTAINWITH.Click
        txtPrintSearch_Condition.Text = "CONTAIN WITH"
    End Sub

    Private Sub CBXSEARCHPRINTPOP_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHPRINTPOP_STARTWITH.Click
        txtPrintSearch_Condition.Text = "START WITH"
    End Sub
End Class
