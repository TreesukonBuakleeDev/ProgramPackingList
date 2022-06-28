Imports System.Data
Public Class FrmSearchBrowseEX
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Topmost = True
            txtEXSearch_Condition.Text = "CONTAIN WITH"
            txtEXSearch_by.Text = "Item No"
            BTN_EXPORTCheckBox.IsChecked = True

            Dim dtITEM As DataTable = New DataTable
            dtITEM = MASTER.GETFMSMASTERITEM()

            DGV_EXPORTSEARCH.ItemsSource = dtITEM.DefaultView

            Call DISPLAY_DGVEXPORTSEARCH()

        Catch ex As Exception
            WriteLog("Error 18 FrmSearchBrowseEX.Window_Loaded() :" & ex.Message)
        End Try
    End Sub

    Private Sub txtEXPORTSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtEXPORTSearch_Text.TextChanged
        Try
            Dim frmMaster As New FrmMasterItem
            Dim dtITEM As DataTable = New DataTable

            dtITEM = MASTER.GETFMSMASTERITEM()
            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing


            If txtEXPORTSearch_Text.Text.TrimEnd = "" Then
            Else

                Select Case txtEXSearch_by.Text
                    Case "Item No"
                        Select Case txtEXSearch_Condition.Text
                            Case "START WITH"
                                ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '" & txtEXPORTSearch_Text.Text & "%'")
                            Case "CONTAIN WITH"
                                ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '%" & txtEXPORTSearch_Text.Text & "%'")
                        End Select
                    Case "Item Description"
                        Select Case txtEXSearch_Condition.Text
                            Case "START WITH"
                                ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '" & txtEXPORTSearch_Text.Text & "%'")
                            Case "CONTAIN WITH"
                                ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '%" & txtEXPORTSearch_Text.Text & "%'")
                        End Select
                    Case "Customer Code"
                        Select Case txtEXSearch_Condition.Text
                            Case "START WITH"
                                ROWFilter = dtITEM.[Select]("IDCUST LIKE  '" & txtEXPORTSearch_Text.Text & "%'")
                            Case "CONTAIN WITH"
                                ROWFilter = dtITEM.[Select]("IDCUST LIKE  '%" & txtEXPORTSearch_Text.Text & "%'")
                        End Select

                End Select
            End If

            dtFilter = dtITEM.Clone
            dtFilter.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtFilter.ImportRow(rowF)
                Next
            Else
                dtFilter = dtITEM.Copy
            End If

            'DGV_MASTERSEARCH.ItemsSource = Nothing

            DGV_EXPORTSEARCH.ItemsSource = dtFilter.DefaultView

            Call DISPLAY_DGVEXPORTSEARCH()

        Catch ex As Exception
            WriteLog("Error 110 txtMasterSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Sub DISPLAY_DGVEXPORTSEARCH()
        Try
            With DGV_EXPORTSEARCH

                .Columns(0).Header = "Item No."
                .Columns(1).Header = "Item Description"
                .Columns(2).Header = "Customer Code"

            End With


            With DGV_EXPORTSEARCH

                .Columns(3).Visibility = Visibility.Hidden
                .Columns(4).Visibility = Visibility.Hidden
                .Columns(5).Visibility = Visibility.Hidden
                .Columns(6).Visibility = Visibility.Hidden
                .Columns(7).Visibility = Visibility.Hidden
                .Columns(8).Visibility = Visibility.Hidden
                .Columns(9).Visibility = Visibility.Hidden
                .Columns(10).Visibility = Visibility.Hidden
                .Columns(11).Visibility = Visibility.Hidden
                .Columns(12).Visibility = Visibility.Hidden
                .Columns(13).Visibility = Visibility.Hidden
                .Columns(14).Visibility = Visibility.Hidden
                .Columns(15).Visibility = Visibility.Hidden
                .Columns(16).Visibility = Visibility.Hidden
                .Columns(17).Visibility = Visibility.Hidden
                .Columns(18).Visibility = Visibility.Hidden
                .Columns(19).Visibility = Visibility.Hidden
                .Columns(20).Visibility = Visibility.Hidden

                .Columns(21).Visibility = Visibility.Hidden
                .Columns(22).Visibility = Visibility.Hidden
                .Columns(23).Visibility = Visibility.Hidden
                .Columns(24).Visibility = Visibility.Hidden
                .Columns(25).Visibility = Visibility.Hidden
                .Columns(26).Visibility = Visibility.Hidden

                .Columns(27).Visibility = Visibility.Hidden
                .Columns(28).Visibility = Visibility.Hidden
                .Columns(29).Visibility = Visibility.Hidden

                .Columns(30).Visibility = Visibility.Hidden
                .Columns(31).Visibility = Visibility.Hidden
                .Columns(32).Visibility = Visibility.Hidden
                .Columns(33).Visibility = Visibility.Hidden
                .Columns(34).Visibility = Visibility.Hidden

            End With
        Catch ex As Exception
            WriteLog("Error 135 DGV_MAINSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_EXPORTSEARCH_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles DGV_EXPORTSEARCH.SelectedCellsChanged
        Try
            Dim cell As DataGridCellInfo = DGV_EXPORTSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim OrdernoCurr As String = cell.Item(0).ToString

            Me.Close()
            If VFrom = True Then
                frmEXPORT.txtMASTER_From.Text = OrdernoCurr
            Else
                frmEXPORT.txtMASTER_To.Text = OrdernoCurr
            End If

        Catch ex As Exception
            WriteLog("Error 152 DGV_MAINSEARCH_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_FILTERORDERNO_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERNO.Click
        txtEXSearch_by.Text = "Item No"
    End Sub

    Private Sub BTN_FILTERORDERDATE_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERORDERDATE.Click
        txtEXSearch_by.Text = "Item Description"
    End Sub

    Private Sub BTN_FILTERIDCUST_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERIDCUST.Click
        txtEXSearch_by.Text = "Customer Code"
    End Sub

    Private Sub CBXSEARCHEX_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHEX_STARTWITH.Click
        txtEXSearch_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHEX_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHEX_CONTAINWITH.Click
        txtEXSearch_Condition.Text = "CONTAIN WITH"
    End Sub
End Class
