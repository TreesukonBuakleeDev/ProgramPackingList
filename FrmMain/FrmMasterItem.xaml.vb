Imports System.Data
Imports Excel = Microsoft.Office.Interop.Excel

Public Class FrmMasterItem
    Public Shared dtITEMTEMP As DataTable = New DataTable()
    Public Shared dtMASTEREX As DataTable = New DataTable()

#Region "Parameter"


#End Region

#Region "BUTTON"
    Private Sub BTNMASTER_SAVE_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_SAVE.Click
        Try
            frmMT.Topmost = False
            Dim DTITEM As DataTable = New DataTable

            DTITEM = CType(DGV_MASTER.ItemsSource, DataView).ToTable

            '>>Case txtMaster_Itemno 
            If txtMaster_Itemno.Text.TrimEnd <> "" Then
                'Update
                Dim dtMASTER As DataTable = New DataTable()
                Dim ROWFilter As DataRow() = Nothing

                ROWFilter = DTITEM.[Select]("ITEMNO =  '" & frmMT.txtMaster_Itemno.Text.TrimEnd & "'  AND IDCUST = '" & frmMT.txtIDCUSTNUM.Text.TrimEnd & "'  ")

                dtMASTER = DTITEM.Clone
                dtMASTER.NewRow()

                If ROWFilter Is Nothing = False Then
                    For Each rowF As DataRow In ROWFilter
                        dtMASTER.ImportRow(rowF)
                    Next
                End If
                'frmMT.WindowState = WindowState.Minimized
                Call DataClass.INSERTFMSICITEM_TEMP(dtMASTER, "UPDATE")
            Else
                'INSERT
                Call DataClass.INSERTFMSMASTERITEM(DTITEM)

            End If
        Catch ex As Exception
            WriteLog("Error 20 CLEARTXT() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_NEW_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_NEW.Click
        Try
            DGV_MASTER.CanUserAddRows = True

        Catch ex As Exception
            WriteLog("Error 30 BTNMASTER_NEW_Click() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_DELETE_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_DELETE.Click
        Try
            Dim dialogOK As MessageBoxResult = MsgBox("Do you want to delete this item ?", MessageBoxButton.YesNo)
            If dialogOK = 6 Then
                Dim dtMT As DataTable = New DataTable

                dtMT = CType(DGV_MASTER.ItemsSource, DataView).ToTable

                If dtMT.Rows.Count <> 0 Then
                    dtMT.Rows(txtCurrentRow.Text).Item("STA_0") = "2"
                End If
                Call DataClass.UPDATEFMSMASTERITEM(dtMT)

                DGV_MASTER.ItemsSource = dtMT.DefaultView

            Else

            End If
        Catch ex As Exception
            WriteLog("Error 55 BTNMASTER_DELETE_Click() :" & ex.Message)
        End Try
    End Sub


    Private Sub BTNMASTER_IMPORTITEM_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_IMPORTITEM.Click
        Try

            dtMASTEREX = CType(DGV_MASTER.ItemsSource, DataView).ToTable

            frmBrowse = New FrmBrowseIm

            Call frmBrowse.Show()

        Catch ex As Exception
            WriteLog("Error 70 BTNMASTER_IMPORTITEM_Click() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_EXPORTITEM_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_EXPORTITEM.Click
        Try

            dtMASTEREX = CType(DGV_MASTER.ItemsSource, DataView).ToTable

            frmEXPORT = New FrmBrowseEx

            Call frmEXPORT.Show()

        Catch ex As Exception
            WriteLog("Error 85 BTNMASTER_EXPORTITEM_Click() :" & ex.Message)
        End Try
    End Sub


#End Region

#Region "EVENT"

    Private Sub DGV_MASTER_SelectedCellsChanged(sender As Object, e As SelectedCellsChangedEventArgs) Handles DGV_MASTER.SelectedCellsChanged
        Try
            Dim cell As DataGridCellInfo = DGV_MASTER.CurrentCell

            Dim rowIndex As Integer = DGV_MASTER.Items.IndexOf(cell.Item)
            txtCurrentRow.Text = rowIndex
            txtCurrentRow.Visibility = False
        Catch ex As Exception
            WriteLog("Error 101 DGV_MASTER_SelectedCellsChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_SEARCH_MouseEnter(sender As Object, e As MouseEventArgs) Handles BTNMASTER_SEARCH.MouseDown
        Try
            Dim dtMT As DataTable = New DataTable

            dtMT = CType(DGV_MASTER.ItemsSource, DataView).ToTable

            Call DISPLAYITEMNO(dtMT)

        Catch ex As Exception
            WriteLog("Error 111 BTNMASTER_DELETE_Click() :" & ex.Message)
        End Try
    End Sub

    Public Sub DISPLAYITEMNO(ByVal DTMT As DataTable)
        Try
            Dim frmSearch As New FrmSearchMaster

            Call frmSearch.Show()
            frmSearch.DGV_MASTERSEARCH.ItemsSource = DTMT.DefaultView

            With frmSearch.DGV_MASTERSEARCH
                .Columns(0).Header = "Item No."
                .Columns(1).Header = "Item Description"
                .Columns(2).Header = "Customer Code"
                .Columns(3).Header = "Unit of Measure"
                .Columns(4).Header = "Width"

                .Columns(5).Header = "Length"
                .Columns(6).Header = "Height"
                .Columns(7).Header = "Qty. per Box "
                .Columns(8).Header = "Qty. per Pallet"
                .Columns(9).Header = "No. of layers in pallet"

                .Columns(10).Header = "Height per level"
                .Columns(11).Header = "Pallet Height"
                .Columns(12).Header = "Pallet Weight"
                .Columns(13).Header = "Qty. Box per Level"
                .Columns(14).Header = "Net Weight (Pcs)"

                .Columns(15).Header = "Gross Weight (Pcs)"
                .Columns(16).Header = "Box Weight"
            End With

            With frmSearch.DGV_MASTERSEARCH
                .Columns(17).Visibility = False
                .Columns(18).Visibility = False
                .Columns(19).Visibility = False
            End With

            dtITEMTEMP = DTMT.Copy
            frmSearch.txtMasterSearch_Condition.Text = "START WITH"
            frmSearch.txtMasterSearch_Text.Text = txtMaster_Itemno.Text.TrimEnd



        Catch ex As Exception
            WriteLog("Error 125 DISPLAYITEMNO() :" & ex.Message)
        End Try

    End Sub

    Public Function GENMASTER_EXPORT() As Boolean
        Dim STA_EXPORT As Boolean = False
        Try
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim status As Boolean = False
            Dim pathExport As String = ""



            If EXPath.EndsWith("\") = True Then
                pathExport = EXPath & "FileExport" & Date.Now.ToString("Hmmss").TrimEnd & ".xls"
            Else
                pathExport = EXPath & "\FileExport" & Date.Now.ToString("Hmmss").TrimEnd & ".xls"
            End If


            If pathExport.TrimEnd = "" Then
                MessageBox.Show("Please assign export path")
                Return False
                Exit Function
            End If

            Dim IDCUST As String = ""


            'Copy Template ,>> Check Exist file
            System.IO.File.Copy(System.AppDomain.CurrentDomain.BaseDirectory & "\Template\TEMPLATE_EXPORT.xls", pathExport, True)

            xlApp = New Excel.Application
            xlApp.Visible = True
            xlApp.WindowState = Excel.XlWindowState.xlMinimized
            xlWorkBook = xlApp.Workbooks.Open(pathExport)

            'Copy DGV --> datatable 
            Dim dtMASTER As DataTable = New DataTable()


            'dtMASTER = dtMASTEREX.Copy

            Dim ROWFilter As DataRow() = Nothing

            Dim ITEMMASTER_To As String

            If frmEXPORT.txtMASTER_To.Text.TrimEnd = "" Then
                ITEMMASTER_To = "zzzzzz"
            Else
                ITEMMASTER_To = frmEXPORT.txtMASTER_To.Text.TrimEnd
            End If

            ROWFilter = dtMASTEREX.[Select]("ITEMNO >=  '" & frmEXPORT.txtMASTER_From.Text.TrimEnd & "' AND ITEMNO <= '" & ITEMMASTER_To & "' ")



            dtMASTER = dtMASTEREX.Clone
            dtMASTER.NewRow()

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtMASTER.ImportRow(rowF)
                Next
            End If

            For Each xlWorkSheet In xlWorkBook.Worksheets
                Select Case xlWorkSheet.Name
                    Case "MASTER"

                        GENEXPORTEXCEL(xlWorkSheet, dtMASTER)

                End Select
            Next

            'Case Generate Error

            xlWorkBook.Save()
            xlWorkBook.Close()
            xlApp.Quit()
            STA_EXPORT = True
        Catch ex As Exception
            STA_EXPORT = False
            Call WriteLog("Error 290: " & ex.Message)
        End Try
        Return STA_EXPORT
    End Function


    Sub GENEXPORTEXCEL(ByRef xlWorkSheet As Excel.Worksheet, ByVal dtMASTER As DataTable)

        Try
            If dtMASTER.Rows.Count > 0 Then
                For i = 0 To dtMASTER.Rows.Count - 1

                    'Write Excel
                    xlWorkSheet.Cells(2 + i, 1) = dtMASTER.Rows(i).Item(0).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 2) = dtMASTER.Rows(i).Item(1).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 3) = dtMASTER.Rows(i).Item(2).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 4) = dtMASTER.Rows(i).Item(3).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 5) = dtMASTER.Rows(i).Item(4).ToString.TrimEnd

                    xlWorkSheet.Cells(2 + i, 6) = dtMASTER.Rows(i).Item(5).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 7) = dtMASTER.Rows(i).Item(6).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 8) = dtMASTER.Rows(i).Item(7).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 9) = dtMASTER.Rows(i).Item(8).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 10) = dtMASTER.Rows(i).Item(9).ToString.TrimEnd

                    xlWorkSheet.Cells(2 + i, 11) = dtMASTER.Rows(i).Item(10).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 12) = dtMASTER.Rows(i).Item(11).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 13) = dtMASTER.Rows(i).Item(12).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 14) = dtMASTER.Rows(i).Item(13).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 15) = dtMASTER.Rows(i).Item(14).ToString.TrimEnd

                    xlWorkSheet.Cells(2 + i, 16) = dtMASTER.Rows(i).Item(15).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 17) = dtMASTER.Rows(i).Item(16).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 18) = dtMASTER.Rows(i).Item(17).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 19) = dtMASTER.Rows(i).Item(18).ToString.TrimEnd
                    xlWorkSheet.Cells(2 + i, 20) = dtMASTER.Rows(i).Item(19).ToString.TrimEnd

                    Dim strStatus As String = i & "/" & dtMASTER.Rows.Count

                Next
            Else

            End If

        Catch ex As Exception
            Call WriteLog("Error 186: " & ex.Message)
        End Try

    End Sub


    Public Shared Sub KillAllExcelProcess()
        Try
            For Each proc As System.Diagnostics.Process In System.Diagnostics.Process.GetProcessesByName("EXCEL")
                proc.Kill()
            Next
        Catch ex As Exception
            Call WriteLog("Error 194: " & ex.Message)
        End Try

    End Sub

    Private Sub DGV_MASTER_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles DGV_MASTER.CellEditEnding
        Dim cell As DataGridCellInfo = DGV_MASTER.CurrentCell
        Dim columnindex As Integer = cell.Column.DisplayIndex
        txtMaster_Itemno.Text = cell.Item(0).ToString
        txtIDCUSTNUM.Text = cell.Item(2).ToString
    End Sub





#End Region

End Class
