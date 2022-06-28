Imports System.Data
Imports System.Windows.Forms
Imports System

Public Class FrmSearchMaster


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Topmost = True
            txtMasterSearch_Condition.Text = "CONTAIN WITH"
            txtMasterSearch_by.Text = "Item No"
            BTN_MasterCheckBox.IsChecked = True
        Catch ex As Exception
            WriteLog("Error 13 FrmSearchMaster.Window_Loaded() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_MasterCheckBox_Click(sender As Object, e As RoutedEventArgs) Handles BTN_MasterCheckBox.Click

    End Sub

    Private Sub txtMasterSearch_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMasterSearch_Text.TextChanged
        Try
            Dim frmMaster As New FrmMasterItem
            Dim dtITEM As DataTable = New DataTable

            dtITEM = FrmMasterItem.dtITEMTEMP.Copy
            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            Select Case txtMasterSearch_by.Text
                Case "Item No"
                    Select Case txtMasterSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '" & txtMasterSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '%" & txtMasterSearch_Text.Text & "%'")
                    End Select
                Case "Item Description"
                    Select Case txtMasterSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '" & txtMasterSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '%" & txtMasterSearch_Text.Text & "%'")
                    End Select
                Case "Customer Code"
                    Select Case txtMasterSearch_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("IDCUST LIKE  '" & txtMasterSearch_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("IDCUST LIKE  '%" & txtMasterSearch_Text.Text & "%'")
                    End Select

            End Select


            dtFilter = dtITEM.Clone
            dtFilter.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtFilter.ImportRow(rowF)
                Next
            End If

            'DGV_MASTERSEARCH.ItemsSource = Nothing

            DGV_MASTERSEARCH.ItemsSource = dtFilter.DefaultView

            With DGV_MASTERSEARCH

                .Columns(0).Header = "Item No."
                .Columns(1).Header = "Item Description"
                .Columns(2).Header = "Customer Code"

            End With


            With DGV_MASTERSEARCH

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
            WriteLog("Error 110 txtMasterSearch_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_MASTERSEARCH_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DGV_MASTERSEARCH.SelectionChanged
        Try
            Dim cell As DataGridCellInfo = DGV_MASTERSEARCH.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim ITEMCurr As String = cell.Item(0).ToString

            DGV_MASTERSEARCH.Items.IndexOf(cell.Item)

            DGV_MASTERSEARCH.Focus()

            Call txtMaster_Itemno_Refresh(ITEMCurr)

            Me.Close()

            Dim idx As Integer = GETROWINDEX(ITEMCurr)

            frmMT.DGV_MASTER.Items.Refresh()
            'frmMT.DGV_MASTER.ScrollIntoView(frmMT.DGV_MASTER.Items(frmMT.DGV_MASTER.Items.Count - 1))

            Dim item = frmMT.DGV_MASTER.Items.GetItemAt(idx)
            frmMT.DGV_MASTER.ScrollIntoView(item)
            frmMT.DGV_MASTER.SelectedItem = frmMT.DGV_MASTER.Items(idx)


        Catch ex As Exception
            WriteLog("Error 99 DGV_MASTERSEARCH_SelectionChanged() :" & ex.Message)
        End Try

    End Sub

    Private Sub BTN_SEARCHTEXT_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_SEARCHTEXT.MouseDown
        txtMasterSearch_Text_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub CBXSEARCHMASTER_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMASTER_STARTWITH.Click
        txtMasterSearch_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHMASTER_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMASTER_CONTAINWITH.Click
        txtMasterSearch_Condition.Text = "CONTAIN WITH"
    End Sub

    Private Sub BTN_FILTERITEMNO_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERITEMNO.Click
        txtMasterSearch_by.Text = "Item No"
    End Sub

    Private Sub BTN_FILTERDESC_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERDESC.Click
        txtMasterSearch_by.Text = "Item Description"
    End Sub

    Private Sub BTN_FILTERIDCUST_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERIDCUST.Click
        txtMasterSearch_by.Text = "Customer Code"
    End Sub
End Class
