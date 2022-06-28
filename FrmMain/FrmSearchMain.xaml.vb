Imports System.Data
Public Class FrmSearchMain

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Topmost = True
            txtMainSearch_Condition.Text = "CONTAIN WITH"
            txtMainSearch_by.Text = "Order No"
            BTN_MainCheckBox.IsChecked = True

            Dim dtORDERNO As DataTable = New DataTable

            dtORDERNO = MASTER.GETSEARCHORDER()
            DGV_MAINSEARCH.ItemsSource = dtORDERNO.DefaultView


        Catch ex As Exception
            WriteLog("Error 13 FrmSearchMain.Window_Loaded() :" & ex.Message)
        End Try
    End Sub

    Private Sub txtMainSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMainSearch_Text.TextChanged
        Try
            Dim frmMaster As New FrmMasterItem
            Dim dtORDERNO As DataTable = New DataTable

            dtORDERNO = MASTER.GETSEARCHORDER()


            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            Select Case txtMainSearch_by.Text
                Case "Order No"
                    Select Case txtMainSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDNUMBER LIKE  '" & txtMainSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDNUMBER LIKE  '%" & txtMainSearch_Text.Text & "%'")
                    End Select
                Case "Order Date"
                    Select Case txtMainSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDDATE LIKE  '" & txtMainSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("ORDDATE LIKE  '%" & txtMainSearch_Text.Text & "%'")
                    End Select
                Case "Customer Code"
                    Select Case txtMainSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtORDERNO.[Select]("CUSTOMER LIKE  '" & txtMainSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtORDERNO.[Select]("CUSTOMER LIKE  '%" & txtMainSearch_Text.Text & "%'")
                    End Select

            End Select


            dtFilter = dtORDERNO.Clone
            dtFilter.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtFilter.ImportRow(rowF)
                Next
            End If

            'DGV_MASTERSEARCH.ItemsSource = Nothing

            DGV_MAINSEARCH.ItemsSource = dtFilter.DefaultView

            With DGV_MAINSEARCH

                .Columns(0).Header = "Order No."
                .Columns(1).Header = "Order date"
                .Columns(2).Header = "Customer Code"
                .Columns(3).Header = "Customer Name"

            End With



        Catch ex As Exception
            WriteLog("Error 77 txtMainSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_MAINSEARCH_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles DGV_MAINSEARCH.SelectedCellsChanged
        Try
            Dim cell As DataGridCellInfo = DGV_MAINSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim OrdernoCurr As String = cell.Item(0).ToString

            Me.Close()

            frmMN.txtMain_OrderNo.Text = OrdernoCurr

        Catch ex As Exception
            WriteLog("Error 105 DGV_MAINSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_FILTERORDERNO_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERNO.Click
        txtMainSearch_by.Text = "Order No"
    End Sub

    Private Sub BTN_FILTERORDERDATE_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERDATE.Click
        txtMainSearch_by.Text = "Order Date"
    End Sub

    Private Sub BTN_FILTERIDCUST_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERIDCUST.Click
        txtMainSearch_by.Text = "Customer Code"
    End Sub

    Private Sub CBXSEARCHMAIN_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMAIN_STARTWITH.Click
        txtMainSearch_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHMAIN_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMAIN_CONTAINWITH.Click
        txtMainSearch_Condition.Text = "CONTAIN WITH"
    End Sub

End Class
