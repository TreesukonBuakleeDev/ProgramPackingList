Imports System.Data
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.IO

Module PROCESS
    Public frmMainWin As MainWindow
    Public frmMT As New FrmMasterItem
    Public frmMN As New FrmMain
    Public frmPrint As New FrmPrint
    Public frmPrintPop As New FrmPrintPopup
    Public frmEXPORT As New FrmBrowseEx
    Public frmBrowse As New FrmBrowseIm
    Public frmAUTH As New FrmAuthor
    Public frmDb As New FrmDbSetup
    Public frmInsert As New FrmSearchInsert
    Public Comp As String




#Region "MASTER"

#Region "SEARCH ITEM"

    Sub txtMaster_Itemno_Refresh(ByVal SELECTITEMNO As String)

        frmMT.txtMaster_Itemno.Text = SELECTITEMNO.TrimEnd

        frmMT.DGV_MASTER.EnableRowVirtualization = True


    End Sub

    Public Function GETROWINDEX(ByVal VALUE As String) As Integer

        Dim idx As Integer

        Dim dtMT As DataTable = New DataTable

        dtMT = CType(frmMT.DGV_MASTER.ItemsSource, DataView).ToTable

        For i = 0 To dtMT.Rows.Count - 1

            If dtMT.Rows(i).Item("ITEMNO").ToString.TrimEnd = VALUE.TrimEnd Then

                idx = i

                Exit For

            End If

        Next

        Return idx


    End Function


#End Region

#Region "IMPORT"
    Public Sub GENLISTIMPORT(ByVal DTITEM As DataTable)
        Try
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim status As Boolean = False
            Dim pathExport As String = "" '= System.AppDomain.CurrentDomain.BaseDirectory & "FileExport\FileExport" & Date.Now.ToString("Hmmss").TrimEnd & ".xls"

            If Not Directory.Exists(IMPath & "\RESULT") Then
                Directory.CreateDirectory(IMPath & "\RESULT")
            End If

            If IMPath.EndsWith("\") = True Then
                If Not Directory.Exists(IMPath & "RESULT") Then
                    Directory.CreateDirectory(IMPath & "RESULT")
                End If
                pathExport = IMPath & "RESULT\ImportList" & Date.Now.ToString("Hmmss").TrimEnd & ".xls"
            Else
                If Not Directory.Exists(IMPath & "\RESULT") Then
                    Directory.CreateDirectory(IMPath & "\RESULT")
                End If
                pathExport = IMPath & "\RESULT\ImportList" & Date.Now.ToString("Hmmss").TrimEnd & ".xls"
            End If


            If pathExport.TrimEnd = "" Then
                MessageBox.Show("Please assign export path")
                Exit Sub
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


            dtMASTER = DTITEM.Copy

            For Each xlWorkSheet In xlWorkBook.Worksheets
                Select Case xlWorkSheet.Name
                    Case "MASTER"

                        GENLISTIMPORTEXCEL(xlWorkSheet, dtMASTER)

                End Select
            Next

            'Case Generate Error

            xlWorkBook.Save()
            xlWorkBook.Close()
            xlApp.Quit()
        Catch ex As Exception
            Call WriteLog("Error 72: " & ex.Message)
        End Try

    End Sub

    Sub GENLISTIMPORTEXCEL(ByRef xlWorkSheet As Excel.Worksheet, ByVal dtMASTER As DataTable)

        Try
            If dtMASTER.Rows.Count > 0 Then
                For i = 0 To dtMASTER.Rows.Count - 1
                    If i = 0 Then
                        xlWorkSheet.Cells(1, 21) = "RESULT"
                    End If
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

                    If dtMASTER.Rows(i).Item(20).ToString.TrimEnd = "" Then

                        xlWorkSheet.Cells(2 + i, 21) = "IMPORTED"
                    Else
                        xlWorkSheet.Cells(2 + i, 21) = dtMASTER.Rows(i).Item(20).ToString.TrimEnd
                    End If


                    Dim strStatus As String = i & "/" & dtMASTER.Rows.Count

                Next
            Else

            End If

        Catch ex As Exception
            Call WriteLog("Error 126: " & ex.Message)
        End Try

    End Sub

#End Region


#End Region

#Region "MAIN"
    'BK 23/02/2022
    'Sub ShowFrmMaster(Optional ByVal REFRESH As String = "")
    '    Try

    '        frmMT = New FrmMasterItem
    '        'PanelMainControl.Children.Add(frmMT)

    '        'METHOD:

    '        Select Case REFRESH
    '            Case "REFRESH"

    '            Case Else
    '                '1.INSERT INTO FMSICITEM_TEMP 
    '                Call DataClass.INSETICITEMTEMP()

    '        End Select

    '        '2.MERGE FMSMASTERITEM <--> FMSICITEM_TEMP 
    '        Call DataClass.MERGEITEMMASTER()


    '        '3.GET DATA FMSMASTERITEM
    '        Dim DTGETITEM As DataTable = MASTER.GETFMSMASTERITEM()

    '        '4.DISPLAY DATA 
    '        frmMT.DGV_MASTER.ItemsSource = DTGETITEM.DefaultView

    '        frmMT.DGV_MASTER.CanUserAddRows = False

    '        frmMT.Show()
    '        frmMT.Topmost = True
    '        DisplayFrmMaster()


    '    Catch ex As Exception
    '        WriteLog("Error 166 (ShowFrmMaster):" & ex.Message)
    '    End Try
    'End Sub

    Public Sub ShowFrmMaster(Optional ByVal REFRESH As String = "")
        Try
            frmMT = New FrmMasterItem

            'PanelMainControl.Children.Add(frmMT)

            'METHOD:

            Select Case REFRESH
                Case "REFRESH"


                Case Else

                    '1.INSERT INTO FMSICITEM_TEMP 
                    Call DataClass.INSETICITEMTEMP()

            End Select

            '2.MERGE FMSMASTERITEM <--> FMSICITEM_TEMP 
            Call DataClass.MERGEITEMMASTER()


            '3.GET DATA FMSMASTERITEM
            Dim DTGETITEM As DataTable = MASTER.GETFMSMASTERITEM()

            '4.DISPLAY DATA 
            frmMT.DGV_MASTER.ItemsSource = DTGETITEM.DefaultView

            frmMT.DGV_MASTER.CanUserAddRows = False


            frmMT.Show()
            frmMT.Topmost = True
            DisplayFrmMaster()


        Catch ex As Exception
            WriteLog("Error 166 (ShowFrmMaster):" & ex.Message)
        End Try
    End Sub

    Sub DisplayFrmMaster()

        With frmMT.DGV_MASTER

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

        With frmMT.DGV_MASTER

            .Columns(0).IsReadOnly = True
            .Columns(1).IsReadOnly = True
            .Columns(2).IsReadOnly = True

        End With



    End Sub

    Sub ShowFrmDbSetup()
        Try
            frmDb = New FrmDbSetup
            frmDb.Show()
            'Call Connection.ReadConfig(dtConfigDB)
            dtConfigDB = READDB()

            If dtConfigDB.Rows.Count > 0 Then

                frmDb.txtDBID.Text = dtConfigDB.Rows(0).Item("ID").ToString.TrimEnd
                frmDb.Acc_Company.Text = dtConfigDB.Rows(0).Item("SERVER").ToString.TrimEnd
                frmDb.Acc_version.Text = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                frmDb.Acc_UserID.Text = dtConfigDB.Rows(0).Item("USER").ToString.TrimEnd
                frmDb.Acc_Password.Password = dtConfigDB.Rows(0).Item("PASSWORD").ToString.TrimEnd
                frmDb.txtServer.Text = dtConfigDB.Rows(0).Item("SERVER").ToString.TrimEnd
                frmDb.txtDB.Text = dtConfigDB.Rows(0).Item("DBAPP").ToString.TrimEnd
                frmDb.txtUser.Text = dtConfigDB.Rows(0).Item("USER").ToString.TrimEnd
                frmDb.txtPassword.Password = dtConfigDB.Rows(0).Item("PASSWORD").ToString.TrimEnd
                If dtConfigDB.Rows(0).Item("AUTHOR").ToString.TrimEnd = 0 Then
                    frmDb.BTNAUTHEN_YES.IsChecked = False
                Else
                    frmDb.BTNAUTHEN_YES.IsChecked = True
                End If
                frmDb.Acc_CompNAME.Text = dtConfigDB.Rows(0).Item("CompNAME").ToString.TrimEnd

            End If
        Catch ex As Exception
            WriteLog("Error 290 (ShowFrmDbSetup):" & ex.Message)
        End Try
    End Sub

    Sub ShowFrmAuthor()
        Try

            frmAUTH.Show()

            'Call Connection.ReadConfig(dtConfigDB)
            dtConfigDB = READDB()

        Catch ex As Exception
            WriteLog("Error 290 (ShowFrmDbSetup):" & ex.Message)
        End Try
    End Sub

#End Region






End Module
