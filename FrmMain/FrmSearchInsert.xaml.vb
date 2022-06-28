Imports System.Data
Public Class FrmSearchInsert
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Topmost = True
            txtMasterInsert_Condition.Text = "CONTAIN WITH"
            txtMasterInsert_by.Text = "Item No"
            BTN_MasterCheckBox.IsChecked = True
            Call txtMasterInsert_Text_TextChanged(Nothing, Nothing)
        Catch ex As Exception
            WriteLog("Error 11 FrmSearchMaster.Window_Loaded() :" & ex.Message)
        End Try
    End Sub

    Private Sub txtMasterInsert_Text_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMasterInsert_Text.TextChanged
        Try
            Dim frmMaster As New FrmMasterItem
            Dim dtITEM As DataTable = New DataTable

            dtITEM = MASTER.GETFMSMASTERITEM()
            Dim dtFilter As DataTable = New DataTable
            Dim ROWFilter As DataRow() = Nothing
            Select Case txtMasterInsert_by.Text
                Case "Item No"
                    Select Case txtMasterInsert_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '" & txtMasterInsert_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("ITEMNO LIKE  '%" & txtMasterInsert_Text.Text & "%'")
                    End Select
                Case "Item Description"
                    Select Case txtMasterInsert_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '" & txtMasterInsert_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("ITEMDESC LIKE  '%" & txtMasterInsert_Text.Text & "%'")
                    End Select
                Case "Customer Code"
                    Select Case txtMasterInsert_Condition.Text
                        Case "START WITH"
                            ROWFilter = dtITEM.[Select]("IDCUST LIKE  '" & txtMasterInsert_Text.Text & "%'")
                        Case "CONTAIN WITH"
                            ROWFilter = dtITEM.[Select]("IDCUST LIKE  '%" & txtMasterInsert_Text.Text & "%'")
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

            DGV_MASTERINSERT.ItemsSource = dtFilter.DefaultView

            With DGV_MASTERINSERT

                .Columns(0).Header = "Item No."
                .Columns(1).Header = "Item Description"
                .Columns(2).Header = "Customer Code"

            End With


            With DGV_MASTERINSERT

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
                '.Columns(19).Visibility = Visibility.Hidden
                '.Columns(20).Visibility = Visibility.Hidden

                '.Columns(21).Visibility = Visibility.Hidden
                '.Columns(22).Visibility = Visibility.Hidden
                '.Columns(23).Visibility = Visibility.Hidden
                '.Columns(24).Visibility = Visibility.Hidden
                '.Columns(25).Visibility = Visibility.Hidden
                '.Columns(26).Visibility = Visibility.Hidden

                '.Columns(27).Visibility = Visibility.Hidden
                '.Columns(28).Visibility = Visibility.Hidden
                '.Columns(29).Visibility = Visibility.Hidden

                '.Columns(30).Visibility = Visibility.Hidden
                '.Columns(31).Visibility = Visibility.Hidden
                '.Columns(32).Visibility = Visibility.Hidden
                '.Columns(33).Visibility = Visibility.Hidden
                '.Columns(34).Visibility = Visibility.Hidden

            End With

        Catch ex As Exception
            WriteLog("Error 110 txtMasterInsert_Text_TextChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_MASTERINSERT_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles DGV_MASTERINSERT.SelectionChanged
        Try
            Dim cell As DataGridCellInfo = DGV_MASTERINSERT.CurrentCell
            Dim columnindex As Integer = cell.Column.DisplayIndex
            Dim ITEMCurr As String = cell.Item(0).ToString
            Dim IDCUSTCurr As String
            If cell.Item(2).ToString = "" Then
                IDCUSTCurr = cell.Item(2).ToString
            Else
                IDCUSTCurr = frmMN.txtMain_IDCUST.Text.TrimEnd
            End If
            DGV_MASTERINSERT.Items.IndexOf(cell.Item)

            DGV_MASTERINSERT.Focus()

            Call txtMaster_Itemno_Refresh(ITEMCurr)

            Me.Close()

            Dim dtINSERT As DataTable = CType(frmMN.DGV_MAIN.ItemsSource, DataView).ToTable

            Dim dtITEM As DataTable = New DataTable

            dtITEM = MASTER.GETFMSMASTERITEM()


            Dim ROWFilter As DataRow() = Nothing


            ROWFilter = dtITEM.[Select]("ITEMNO = '" & ITEMCurr & "' AND IDCUST = '" & IDCUSTCurr & "' ")

            Dim dtITEMVALUE As DataTable = New DataTable

            dtITEMVALUE = dtITEM.Clone

            If ROWFilter Is Nothing = False Then
                For Each rowF As DataRow In ROWFilter
                    dtITEMVALUE.ImportRow(rowF)
                Next
            End If
            Dim ITEMDESC As String = ""
            Dim QTYPERPALLET As String = ""
            'Dim As String = ""
            Dim STOCKUNIT As String = ""
            Dim PONO As String = frmMN.txtMain_PONO.Text.TrimEnd
            Dim TERM As String = frmMN.txtMain_TERM.Text.TrimEnd
            Dim MARK As String = frmMN.txtMain_SHIPMARK.Text.TrimEnd
            'Dim LINENUM As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.TrimEnd, "LINENUM")
            'Dim SEQ As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.TrimEnd, "SEQ")
            Dim LINENUM As Decimal
            Dim SEQ As Decimal
            Dim ORDUNIQ As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.Trim, "ORDUNIQ")
            Dim NW As Decimal
            Dim GW As Decimal
            Dim M3 As Decimal
            Dim DIMENSION As String


            If dtITEMVALUE.Rows.Count <> 0 Then
                ITEMDESC = dtITEMVALUE.Rows(0).Item("ITEMDESC").ToString
                QTYPERPALLET = dtITEMVALUE.Rows(0).Item("QTYPER_PALLET").ToString
                '= dtITEMVALUE.Rows(0).Item("").ToString
                STOCKUNIT = dtITEMVALUE.Rows(0).Item("STOCKUNIT").ToString
                '= dtITEMVALUE.Rows(0).Item("").ToString

                'RE-CALCURATE 
                Dim rowValueNW As Decimal = CDec(dtITEMVALUE.Rows(0).Item("NETWEIGHT").ToString)
                Dim rowValueGW As Decimal = CDec(dtITEMVALUE.Rows(0).Item("GROSSWEIGHT").ToString)
                Dim rowValueHEIGHT As Decimal
                Dim rowValueBOXWEIGHT As Decimal = CDec(dtITEMVALUE.Rows(0).Item("BOXWEIGHT").ToString)
                Dim rowValueQTYPER_BOX As Decimal = CDec(dtITEMVALUE.Rows(0).Item("QTYPER_BOX").ToString)
                Dim rowValuePALLET_WEIGHT As Decimal = CDec(dtITEMVALUE.Rows(0).Item("PALLET_WEIGHT").ToString)
                Dim rowValuePALLET_HEIGHT As Decimal = CDec(dtITEMVALUE.Rows(0).Item("PALLET_HEIGHT").ToString)

                Dim rowValueQTYBOXPER_LEVEL As Decimal = CDec(dtITEMVALUE.Rows(0).Item("QTYBOXPER_LEVEL").ToString)
                Dim rowValueHEIGHTPER_LEVEL As Decimal = CDec(dtITEMVALUE.Rows(0).Item("HEIGHTPER_LEVEL").ToString)

                Dim rowValueWIDTH As Decimal = CDec(dtITEMVALUE.Rows(0).Item("WIDTH").ToString)
                Dim rowValueLENGTH As Decimal = CDec(dtITEMVALUE.Rows(0).Item("LENGTH").ToString)

                'NW

                NW = CDec(QTYPERPALLET) * CDec(rowValueNW)

                'GW 

                If rowValueQTYPER_BOX <> 0 Then
                    GW = (CDec(rowValueNW) * CDec(QTYPERPALLET))
                    GW = GW + (CDec(rowValueBOXWEIGHT) * (CDec(QTYPERPALLET) / CDec(rowValueQTYPER_BOX)))
                    GW = GW + (CDec(rowValuePALLET_WEIGHT) * 1)

                Else
                    GW = 0.0000
                End If

                ' M3 
                If rowValueQTYPER_BOX <> 0 And rowValueQTYBOXPER_LEVEL <> 0 Then

                    Dim A As Decimal = (QTYPERPALLET / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL)
                    A = Math.Ceiling(A)
                    rowValueHEIGHT = (A * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
                    rowValueHEIGHT = Math.Round(rowValueHEIGHT, 2)
                Else
                    rowValueHEIGHT = 0.00
                End If

                M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000

                'DIMENSION 

                DIMENSION = rowValueWIDTH & " x " & rowValueLENGTH & " x " & rowValueHEIGHT

            End If

            'If LINENUM.TrimEnd <> "" Then
            '    'LINENUM = LINENUM * 2

            'End If

            'If SEQ.TrimEnd <> "" Then
            '    'SEQ = SEQ + 1

            'End If

            SEQ = FrmMain.IDXITEM
            LINENUM = FrmMain.LINENUMITEM - 1
            're-running No. 
            For j = 0 To dtINSERT.Rows.Count - 1
                If CDec(dtINSERT.Rows(j).Item("SEQ")) >= SEQ Then
                    dtINSERT.Rows(j).Item("SEQ") = dtINSERT.Rows(j).Item("SEQ") + 1
                    dtINSERT.Rows(j).Item("NO") = dtINSERT.Rows(j).Item("NO") + 1
                End If

            Next

            dtINSERT.AcceptChanges()

            dtINSERT.Rows.Add(SEQ, dtINSERT.Rows(0).Item(1).ToString.TrimEnd, dtINSERT.Rows(0).Item(2).ToString.TrimEnd, dtINSERT.Rows(0).Item(3).ToString.TrimEnd, dtINSERT.Rows(0).Item(4).ToString.TrimEnd, dtINSERT.Rows(0).Item(5).ToString.TrimEnd, dtINSERT.Rows(0).Item(6).ToString.TrimEnd, dtINSERT.Rows(0).Item(7).ToString.TrimEnd, dtINSERT.Rows(0).Item(8).ToString.TrimEnd, dtINSERT.Rows(0).Item(9).ToString.TrimEnd, dtINSERT.Rows(0).Item(10).ToString.TrimEnd, dtINSERT.Rows(0).Item(11).ToString.TrimEnd, dtINSERT.Rows(0).Item(12).ToString.TrimEnd, dtINSERT.Rows(0).Item(13).ToString.TrimEnd, dtINSERT.Rows(0).Item(14).ToString.TrimEnd, dtINSERT.Rows(0).Item(15).ToString.TrimEnd, ITEMCurr, ITEMCurr, ITEMCurr, ITEMDESC, QTYPERPALLET, STOCKUNIT, "1", NW, GW, M3, DIMENSION, ORDUNIQ, QTYPERPALLET, QTYPERPALLET, PONO, TERM, MARK, LINENUM, "1", SEQ, QTYPERPALLET)

            'frmMN.DGV_MAIN.ItemsSource = dtINSERT.DefaultView
            dtINSERT.DefaultView.Sort = "LINENUM"
            Dim DTINS As DataTable = New DataTable()
            DTINS = dtINSERT.Clone
            DTINS = dtINSERT.Copy
            DTINS.DefaultView.Sort = "LINENUM"
            DTINS.AcceptChanges()

            DTINS.AcceptChanges()
            Call frmMN.DisplayDGVMAIN(DTINS)
            Call frmMN.DisplayHEADER(dtINSERT)

        Catch ex As Exception
            WriteLog("Error 240 DGV_MASTERINSERT_SelectionChanged() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_SEARCHTEXT_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_SEARCHTEXT.MouseDown
        txtMasterInsert_Text_TextChanged(Nothing, Nothing)
    End Sub

    Private Sub CBXSEARCHMASTER_STARTWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMASTER_STARTWITH.Click
        txtMasterInsert_Condition.Text = "START WITH"
    End Sub

    Private Sub CBXSEARCHMASTER_CONTAINWITH_Click(sender As Object, e As RoutedEventArgs) Handles CBXSEARCHMASTER_CONTAINWITH.Click
        txtMasterInsert_Condition.Text = "CONTAIN WITH"
    End Sub

    Private Sub BTN_FILTERITEMNO_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERITEMNO.Click
        txtMasterInsert_by.Text = "Item No"
    End Sub

    Private Sub BTN_FILTERDESC_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERDESC.Click
        txtMasterInsert_by.Text = "Item Description"
    End Sub

    Private Sub BTN_FILTERIDCUST_Click(sender As Object, e As RoutedEventArgs) Handles BTN_FILTERIDCUST.Click
        txtMasterInsert_by.Text = "Customer Code"
    End Sub
End Class
