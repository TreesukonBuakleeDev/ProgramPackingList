Imports System.Data
Imports System.Windows.Forms

Public Class FrmMain

    Public Shared dtORDERTEMP As DataTable = New DataTable()
    Public Shared SHOWMENUSTRIP As ContextMenuStrip = New ContextMenuStrip()
    Public Shared DTCHANGEEDIT As DataTable = New DataTable()
    Public Shared IDXITEM As Decimal
    Public Shared LINENUMITEM As Decimal

#Region "BUTTON"
    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)


    End Sub

    Private Sub txtMain_OrderNo_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtMain_OrderNo.TextChanged
        Try
            Call CLEARTXT()
            Call GETDATA()

        Catch ex As Exception
            WriteLog("Error 16 BTN_NEXT_MouseEnter() :" & ex.Message)
        End Try

    End Sub


    Private Sub BTN_NEXT_Click(sender As Object, e As RoutedEventArgs) Handles BTN_NEXT.Click
        Try
            Call CLEARTXT()
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            NEXTORDERNO = MASTER.GETORDNUMBER(">", txtMain_OrderNo.Text)
            If NEXTORDERNO.TrimEnd = "" Then
                NEXTORDERNO = MASTER.GETORDNUMBER("<", txtMain_OrderNo.Text)
            End If
            txtMain_OrderNo.Text = NEXTORDERNO

            '2. GETDATA()
            Call GETDATA()

        Catch ex As Exception
            WriteLog("Error 36 BTN_NEXT_MouseEnter() :" & ex.Message)
        End Try
    End Sub




    Private Sub BTN_NEXTEND_Click(sender As Object, e As RoutedEventArgs) Handles BTN_NEXTEND.Click
        Try
            Call CLEARTXT()
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            NEXTORDERNO = MASTER.GETORDNUMBER(">>", txtMain_OrderNo.Text)
            txtMain_OrderNo.Text = NEXTORDERNO

            '2. GETDATA()
            Call GETDATA()
        Catch ex As Exception
            WriteLog("Error 50 BTN_NEXTEND_MouseEnter() :" & ex.Message)
        End Try
    End Sub



    Private Sub BTN_BACK_Click(sender As Object, e As RoutedEventArgs) Handles BTN_BACK.Click
        Try
            Call CLEARTXT()
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            NEXTORDERNO = MASTER.GETORDNUMBER("<", txtMain_OrderNo.Text)
            If NEXTORDERNO.TrimEnd = "" Then
                NEXTORDERNO = MASTER.GETORDNUMBER(">", txtMain_OrderNo.Text)
            End If
            txtMain_OrderNo.Text = NEXTORDERNO

            '2. GETDATA()
            Call GETDATA()
        Catch ex As Exception
            WriteLog("Error 70 BTN_BACK_MouseDown() :" & ex.Message)
        End Try
    End Sub


    Private Sub BTN_BACKEND_Click(sender As Object, e As RoutedEventArgs) Handles BTN_BACKEND.Click
        Try
            Call CLEARTXT()
            'METHOD
            '1. NEXT ORDER NUMBER
            Dim NEXTORDERNO As String = ""
            NEXTORDERNO = MASTER.GETORDNUMBER("<<", txtMain_OrderNo.Text)
            txtMain_OrderNo.Text = NEXTORDERNO

            '2. GETDATA()
            Call GETDATA()
        Catch ex As Exception
            WriteLog("Error 85 BTN_BACKEND_MouseEnter() :" & ex.Message)
        End Try
    End Sub


    Private Sub BTNMASTER_SAVE_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_SAVE.Click
        Try

            'Dim ORDNO As String = txtMain_OrderNo.Text.TrimEnd
            Dim dt As DataTable = New DataTable

            dt = CType(DGV_MAIN.ItemsSource, DataView).ToTable

            If dt.Rows.Count <> 0 Then
                'METHOD 

                '1. Check Exist 
                Dim ORDNO As String = dt.Rows(0).Item("ORDNUMBER").ToString

                If MASTER.CHECKEXIST_FMSPACKING(ORDNO) = True Then

                    '2. FMSPACKING 

                    '2.1 True - UPDATE 

                    'Process dt 
                    For i = 0 To dt.Rows.Count - 1

                        dt.Rows(i).Item("ORDNUMBER") = txtMain_OrderNo.Text
                        dt.Rows(i).Item("ORDDATE") = CDate(txtMain_OrderDate.Text).ToString("yyyyMMdd")
                        dt.Rows(i).Item("CUSTOMER") = txtMain_IDCUST.Text
                        dt.Rows(i).Item("BILNAME") = txtMain_NAMECUST.Text
                        dt.Rows(i).Item("DESC") = txtMain_INVNO.Text
                        dt.Rows(i).Item("EXPDATE") = CDate(txtMain_INVDATE.Text).ToString("yyyyMMdd")

                        dt.Rows(i).Item("From") = txtMain_From.Text
                        dt.Rows(i).Item("To") = txtMain_To.Text
                        dt.Rows(i).Item("ETD") = txtMain_ETD.Text
                        dt.Rows(i).Item("ETA") = txtMain_ETA.Text
                        dt.Rows(i).Item("FREIGHT") = txtMain_Freight.Text
                        dt.Rows(i).Item("FLIGHTVESSEL") = txtMain_FreightVessel.Text
                        dt.Rows(i).Item("BL") = txtMain_BL.Text
                        dt.Rows(i).Item("PORTCHARGE") = txtMain_Discharge.Text
                        dt.Rows(i).Item("FINALDEST") = txtMain_FinalDest.Text

                        dt.Rows(i).Item("PONO") = txtMain_PONO.Text
                        dt.Rows(i).Item("TERM") = txtMain_TERM.Text



                        dt.Rows(i).Item("MARK") = txtMain_SHIPMARK.Text
                    Next

                    'INSERTFMSPACKINGEDIT 
                    'Call DataClass.INSERTFMSPACKINGEDIT(dt)

                    Dim STATUS = DataClass.INSERTFMSPACKINGEDIT(dt)
                    If STATUS = True Then

                        MessageBox.Show(New Form With {.TopMost = True}, "SAVE SUCCESFULLY", "SAVE SUCCESFULLY", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else
                        MessageBox.Show(New Form With {.TopMost = True}, "FAILED", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                    'MERGE INSERTFMSPACKINGEDIT <--> INSERTFMSPACKING
                    Call DataClass.MERGEFMSPACKING(ORDNO)



                Else

                    '2.2 False - INSERT
                    'Process dt 
                    For i = 0 To dt.Rows.Count - 1

                        dt.Rows(i).Item("ORDNUMBER") = txtMain_OrderNo.Text
                        dt.Rows(i).Item("ORDDATE") = CDate(txtMain_OrderDate.Text).ToString("yyyyMMdd")
                        dt.Rows(i).Item("CUSTOMER") = txtMain_IDCUST.Text
                        dt.Rows(i).Item("BILNAME") = txtMain_NAMECUST.Text
                        dt.Rows(i).Item("DESC") = txtMain_INVNO.Text
                        dt.Rows(i).Item("EXPDATE") = CDate(txtMain_INVDATE.Text).ToString("yyyyMMdd")

                        dt.Rows(i).Item("From") = txtMain_From.Text
                        dt.Rows(i).Item("To") = txtMain_To.Text
                        dt.Rows(i).Item("ETD") = txtMain_ETD.Text
                        dt.Rows(i).Item("ETA") = txtMain_ETA.Text
                        dt.Rows(i).Item("FREIGHT") = txtMain_Freight.Text
                        dt.Rows(i).Item("FLIGHTVESSEL") = txtMain_FreightVessel.Text
                        dt.Rows(i).Item("BL") = txtMain_BL.Text
                        dt.Rows(i).Item("PORTCHARGE") = txtMain_Discharge.Text
                        dt.Rows(i).Item("FINALDEST") = txtMain_FinalDest.Text

                        dt.Rows(i).Item("PONO") = txtMain_PONO.Text
                        dt.Rows(i).Item("TERM") = txtMain_TERM.Text
                        dt.Rows(i).Item("MARK") = txtMain_SHIPMARK.Text

                    Next

                    'Call DataClass.INSERTFMSPACKING(dt)

                    Dim STATUS = DataClass.INSERTFMSPACKING(dt)
                    If STATUS = True Then
                        MessageBox.Show(New Form With {.TopMost = True}, "SAVE SUCCESFULLY", "SAVE SUCCESFULLY", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        MessageBox.Show(New Form With {.TopMost = True}, "FAILED", "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If

                End If
                Call GETDATA()
            End If
        Catch ex As Exception
            WriteLog("Error 170 BTNMASTER_SAVE_Click() :" & ex.Message)
        End Try

    End Sub

    Private Sub BTNMAIN_DELETE_Click(sender As Object, e As RoutedEventArgs) Handles BTNMAIN_DELETE.Click
        Try
            Dim dialogOK As MessageBoxButton = MsgBox("Do you want to delete this packing list ?", MessageBoxButton.OKCancel)
            If dialogOK = 1 Then

                Call DataClass.UPDATEFMSPACKING(txtMain_OrderNo.Text.TrimEnd)
                Call GETDATA()
                MessageBox.Show("Delete Already")
            Else

            End If

        Catch ex As Exception
            WriteLog("Error 190 BTNMAIN_DELETE_Click() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTN_SEARCHORDER_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTN_SEARCHORDER.MouseDown
        Try
            Topmost = False
            txtMain_OrderNo.Text = ""


            Call DISPLAYORDERNO()

        Catch ex As Exception
            WriteLog("Error 111 BTNMASTER_DELETE_Click() : " & ex.Message)
        End Try
    End Sub

    Private Sub BTNMAIN_PRINT_Click(sender As Object, e As RoutedEventArgs) Handles BTNMAIN_PRINT.Click

        Try
            frmPrintPop = New FrmPrintPopup
            frmPrintPop.Show()

            frmPrintPop.txtPrint_From.Text = txtMain_OrderNo.Text
            frmPrintPop.txtPrint_To.Text = txtMain_OrderNo.Text
        Catch ex As Exception
            WriteLog("Error 217 BTNMAIN_PRINT_Click() :" & ex.Message)
        End Try
    End Sub


    Public Sub DISPLAYORDERNO()
        Try
            Dim frmSearch As New FrmSearchMain

            Call frmSearch.Show()

            Dim dtORDERNO As DataTable = New DataTable

            dtORDERNO = MASTER.GETSEARCHORDER()

            frmSearch.DGV_MAINSEARCH.ItemsSource = dtORDERNO.DefaultView

            dtORDERTEMP = dtORDERNO.Copy

            frmSearch.txtMainSearch_Condition.Text = "CONTAIN WITH"

            frmSearch.txtMainSearch_Text.Text = txtMain_OrderNo.Text

        Catch ex As Exception
            WriteLog("Error 225 DISPLAYORDERNO() :" & ex.Message)
        End Try

    End Sub


#End Region

#Region "EVENT"

    'BACKUP REVISE 1 BRFORE 24/02/2022 EDIT CAL GW, DIMENSION HEIGHT 
    'Sub GETDATA()
    '    Try
    '        Dim DTGETDATA As New DataTable()
    '        Dim chkFMSPACKING As Boolean
    '        Dim ORDNUMBER As String = txtMain_OrderNo.Text.TrimEnd
    '        'METHOD: 

    '        '1. GET DATA 
    '        Try

    '            DTGETDATA = MASTER.GETDATA(ORDNUMBER, chkFMSPACKING)

    '        Catch ex As Exception
    '            WriteLog("Error 187 GETDATA() :" & ex.Message)
    '        End Try


    '        '2. PROCESS DATA
    '        Try

    '            Dim PATTERN As String = ""
    '            If DTGETDATA.Rows.Count <> 0 Then
    '                PATTERN = MASTER.GETPATTERN(DTGETDATA.Rows(0).Item("CUSTOMER").ToString)

    '                Select Case PATTERN

    '                    Case "A"
    '                        '2.1 PATTERN A
    '                        Try
    '                            'CALCURATE 
    '                            ''CALCURATE QTY 
    '                            If chkFMSPACKING = False Then 'NO EXIST IN FMSPACKING
    '                                If DTGETDATA.Rows.Count > 0 Then
    '                                    Dim CNT As Decimal = DTGETDATA.Rows.Count - 1
    '                                    For i = 0 To CNT
    '                                        Dim j As Integer = 0
    '                                        Dim ITEMNO As String = DTGETDATA.Rows(i).Item("ITEM").ToString.TrimEnd
    '                                        Dim QTYSHPTODT As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYSHPTODT").ToString).ToString("F2")
    '                                        Dim QTYPER_PALLET As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYPER_PALLET").ToString).ToString("F2")
    '                                        If QTYSHPTODT > QTYPER_PALLET Then
    '                                            DTGETDATA.Rows(i).Item("QTY") = QTYPER_PALLET

    '                                            If QTYPER_PALLET = 0 Then
    '                                                MessageBox.Show("Please Checking item " & ITEMNO & " QTY per PALLET = 0  ")
    '                                            Else
    '                                                'Determine number of rows
    '                                                Dim NUMBERROWS As Decimal

    '                                                NUMBERROWS = QTYSHPTODT / QTYPER_PALLET

    '                                                NUMBERROWS = Math.Ceiling(NUMBERROWS)

    '                                                'For loop add as determine number of rows
    '                                                For j = 0 To NUMBERROWS - 2
    '                                                    Dim row As DataRow = DTGETDATA.Rows(i)

    '                                                    Select Case j
    '                                                        Case NUMBERROWS - 2
    '                                                            DTGETDATA.ImportRow(row)
    '                                                            DTGETDATA.AcceptChanges()
    '                                                            Dim rowCnt As Integer = DTGETDATA.Rows.Count - 1
    '                                                            DTGETDATA.Rows(rowCnt).Item("QTY") = QTYSHPTODT - ((NUMBERROWS - 1) * QTYPER_PALLET)
    '                                                            row = Nothing

    '                                                        Case Else
    '                                                            DTGETDATA.ImportRow(row)
    '                                                            row = Nothing

    '                                                    End Select
    '                                                Next

    '                                            End If
    '                                        Else
    '                                            'Case QTYSHPTODT < QTYPER_PALLET
    '                                            DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT
    '                                        End If
    '                                    Next

    '                                    DTGETDATA.DefaultView.Sort ="LINENUM"
    '                                Else
    '                                    Exit Try
    '                                End If

    '                                'CALCURATE N.W. , G.W. , M3 , Dimension 
    '                                Dim DTITEM As DataTable = New DataTable
    '                                DTITEM = MASTER.GETFMSMASTERITEM()

    '                                For k = 0 To DTGETDATA.Rows.Count - 1

    '                                    Dim ITEM As String = ""
    '                                    Dim QTY As String = ""
    '                                    Dim NW As String = ""
    '                                    Dim GW As String = ""
    '                                    Dim M3 As String = ""
    '                                    Dim DIMENSION As String = ""

    '                                    Dim ROWDTCAL() As DataRow
    '                                    Dim rowValueNW As Decimal
    '                                    Dim rowValueGW As Decimal
    '                                    Dim rowValueHEIGHT As Decimal
    '                                    Dim rowValueBOXWEIGHT As Decimal
    '                                    Dim rowValueQTYPER_BOX As Decimal
    '                                    Dim rowValuePALLET_WEIGHT As Decimal
    '                                    Dim rowValuePALLET_HEIGHT As Decimal

    '                                    Dim rowValueQTYBOXPER_LEVEL As Decimal
    '                                    Dim rowValueHEIGHTPER_LEVEL As Decimal

    '                                    Dim rowValueWIDTH As Decimal
    '                                    Dim rowValueLENGTH As Decimal

    '                                    If DTITEM.Rows.Count > 0 Then

    '                                        ITEM = DTGETDATA.Rows(k).Item("ITEM").ToString.TrimEnd
    '                                        QTY = DTGETDATA.Rows(k).Item("QTY").ToString.TrimEnd

    '                                        ' N.W.
    '                                        ROWDTCAL = DTITEM.[Select]("ITEMNO =  '" & ITEM & "'")

    '                                        If ROWDTCAL.Count > 0 Then
    '                                            rowValueNW = ROWDTCAL(0).Item("NETWEIGHT").ToString.TrimEnd
    '                                        End If

    '                                        NW = CDec(QTY) * CDec(rowValueNW)

    '                                        'G.W"
    '                                        rowValueGW = ROWDTCAL(0).Item("GROSSWEIGHT").ToString.TrimEnd
    '                                        rowValueBOXWEIGHT = ROWDTCAL(0).Item("BOXWEIGHT").ToString.TrimEnd
    '                                        rowValueQTYPER_BOX = ROWDTCAL(0).Item("QTYPER_BOX").ToString.TrimEnd
    '                                        rowValuePALLET_WEIGHT = ROWDTCAL(0).Item("PALLET_WEIGHT").ToString.TrimEnd
    '                                        rowValueQTYBOXPER_LEVEL = ROWDTCAL(0).Item("QTYBOXPER_LEVEL").ToString.TrimEnd
    '                                        rowValueHEIGHTPER_LEVEL = ROWDTCAL(0).Item("HEIGHTPER_LEVEL").ToString.TrimEnd
    '                                        rowValuePALLET_HEIGHT = ROWDTCAL(0).Item("PALLET_HEIGHT").ToString.TrimEnd
    '                                        rowValueWIDTH = ROWDTCAL(0).Item("WIDTH").ToString.TrimEnd
    '                                        rowValueLENGTH = ROWDTCAL(0).Item("LENGTH").ToString.TrimEnd

    '                                        If rowValueQTYPER_BOX <> 0 Then
    '                                            GW = (CDec(rowValueGW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
    '                                        Else
    '                                            GW = 0.0000
    '                                        End If
    '                                    End If
    '                                    'Update fields
    '                                    DTGETDATA.Rows(k).Item("NW") = NW
    '                                    DTGETDATA.Rows(k).Item("GW") = GW
    '                                    ' M3 ,
    '                                    If rowValueQTYPER_BOX <> 0 And rowValueQTYBOXPER_LEVEL <> 0 Then
    '                                        rowValueHEIGHT = ((QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL) * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
    '                                    Else
    '                                        rowValueHEIGHT = 0
    '                                    End If

    '                                    M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000

    '                                    'DIMENSION

    '                                    DIMENSION = rowValueWIDTH & " x " & rowValueLENGTH & " x " & rowValueHEIGHT

    '                                    'Update fields
    '                                    DTGETDATA.Rows(k).Item("M3") = M3
    '                                    DTGETDATA.Rows(k).Item("DIMENSION") = DIMENSION

    '                                Next

    '                            Else
    '                                'EXIST IN FMSPACKING

    '                            End If

    '                        Catch ex As Exception
    '                            WriteLog("Error 347 GETDATA() :" & ex.Message)
    '                        End Try

    '                    Case "B"
    '                        '2.2 PATTERN B
    '                        Try
    '                            'CALCURATE 
    '                            ''CALCURATE QTY 
    '                            If chkFMSPACKING = False Then 'NO EXIST IN FMSPACKING
    '                                If DTGETDATA.Rows.Count > 0 Then
    '                                    Dim CNT As Decimal = DTGETDATA.Rows.Count - 1
    '                                    For i = 0 To CNT
    '                                        Dim j As Integer = 0
    '                                        Dim ITEMNO As String = DTGETDATA.Rows(i).Item("ITEM").ToString.TrimEnd
    '                                        Dim QTYSHPTODT As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYSHPTODT").ToString).ToString("F2")
    '                                        Dim QTYPER_PALLET As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYPER_PALLET").ToString).ToString("F2")
    '                                        Dim QTYBACKORD As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYBACKORD").ToString).ToString("F2")
    '                                        'DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT + QTYBACKORD
    '                                        If QTYPER_PALLET = 0 Then
    '                                            DTGETDATA.Rows(i).Item("PALLET") = 0.00
    '                                        Else
    '                                            DTGETDATA.Rows(i).Item("PALLET") = Math.Ceiling((QTYSHPTODT + QTYBACKORD) / QTYPER_PALLET)
    '                                        End If
    '                                        QTYSHPTODT = QTYSHPTODT + QTYBACKORD
    '                                        If QTYSHPTODT > QTYPER_PALLET Then
    '                                            DTGETDATA.Rows(i).Item("QTY") = QTYPER_PALLET

    '                                            If QTYPER_PALLET = 0 Then
    '                                                MessageBox.Show("Please Checking item " & ITEMNO & " QTY per PALLET = 0  ")
    '                                            Else
    '                                                'Determine number of rows
    '                                                Dim NUMBERROWS As Decimal

    '                                                NUMBERROWS = QTYSHPTODT / QTYPER_PALLET

    '                                                NUMBERROWS = Math.Ceiling(NUMBERROWS)

    '                                                'For loop add as determine number of rows
    '                                                For j = 0 To NUMBERROWS - 2
    '                                                    Dim row As DataRow = DTGETDATA.Rows(i)

    '                                                    Select Case j
    '                                                        Case NUMBERROWS - 2
    '                                                            DTGETDATA.ImportRow(row)
    '                                                            DTGETDATA.AcceptChanges()
    '                                                            Dim rowCnt As Integer = DTGETDATA.Rows.Count - 1
    '                                                            DTGETDATA.Rows(rowCnt).Item("QTY") = QTYSHPTODT - ((NUMBERROWS - 1) * QTYPER_PALLET)
    '                                                            row = Nothing

    '                                                        Case Else
    '                                                            DTGETDATA.ImportRow(row)
    '                                                            row = Nothing

    '                                                    End Select
    '                                                Next

    '                                            End If
    '                                        Else
    '                                            'Case QTYSHPTODT < QTYPER_PALLET
    '                                            DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT
    '                                        End If
    '                                    Next

    '                                    DTGETDATA.DefaultView.Sort ="LINENUM"

    '                                Else
    '                                    Exit Try
    '                                End If

    '                                'CALCURATE N.W. , G.W. , M3 , Dimension 
    '                                Dim DTITEM As DataTable = New DataTable
    '                                DTITEM = MASTER.GETFMSMASTERITEM()

    '                                For k = 0 To DTGETDATA.Rows.Count - 1

    '                                    Dim ITEM As String = ""
    '                                    Dim QTY As String = ""
    '                                    Dim NW As String = ""
    '                                    Dim GW As String = ""
    '                                    Dim M3 As String = ""
    '                                    Dim DIMENSION As String = ""

    '                                    Dim ROWDTCAL() As DataRow
    '                                    Dim rowValueNW As Decimal
    '                                    Dim rowValueGW As Decimal
    '                                    Dim rowValueHEIGHT As Decimal
    '                                    Dim rowValueBOXWEIGHT As Decimal
    '                                    Dim rowValueQTYPER_BOX As Decimal
    '                                    Dim rowValuePALLET_WEIGHT As Decimal
    '                                    Dim rowValuePALLET_HEIGHT As Decimal

    '                                    Dim rowValueQTYBOXPER_LEVEL As Decimal
    '                                    Dim rowValueHEIGHTPER_LEVEL As Decimal

    '                                    Dim rowValueWIDTH As Decimal
    '                                    Dim rowValueLENGTH As Decimal

    '                                    If DTITEM.Rows.Count > 0 Then

    '                                        ITEM = DTGETDATA.Rows(k).Item("ITEM").ToString.TrimEnd
    '                                        QTY = DTGETDATA.Rows(k).Item("QTY").ToString.TrimEnd

    '                                        ' N.W.
    '                                        ROWDTCAL = DTITEM.[Select]("ITEMNO =  '" & ITEM & "'")

    '                                        If ROWDTCAL.Count > 0 Then
    '                                            rowValueNW = ROWDTCAL(0).Item("NETWEIGHT").ToString.TrimEnd
    '                                        End If

    '                                        NW = CDec(QTY) * CDec(rowValueNW)

    '                                        'G.W"
    '                                        rowValueGW = ROWDTCAL(0).Item("GROSSWEIGHT").ToString.TrimEnd
    '                                        rowValueBOXWEIGHT = ROWDTCAL(0).Item("BOXWEIGHT").ToString.TrimEnd
    '                                        rowValueQTYPER_BOX = ROWDTCAL(0).Item("QTYPER_BOX").ToString.TrimEnd
    '                                        rowValuePALLET_WEIGHT = ROWDTCAL(0).Item("PALLET_WEIGHT").ToString.TrimEnd
    '                                        rowValueQTYBOXPER_LEVEL = ROWDTCAL(0).Item("QTYBOXPER_LEVEL").ToString.TrimEnd
    '                                        rowValueHEIGHTPER_LEVEL = ROWDTCAL(0).Item("HEIGHTPER_LEVEL").ToString.TrimEnd
    '                                        rowValuePALLET_HEIGHT = ROWDTCAL(0).Item("PALLET_HEIGHT").ToString.TrimEnd
    '                                        rowValueWIDTH = ROWDTCAL(0).Item("WIDTH").ToString.TrimEnd
    '                                        rowValueLENGTH = ROWDTCAL(0).Item("LENGTH").ToString.TrimEnd

    '                                        If rowValueQTYPER_BOX <> 0 Then
    '                                            GW = (CDec(rowValueGW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
    '                                        Else
    '                                            GW = 0.0000
    '                                        End If
    '                                    End If
    '                                    'Update fields
    '                                    DTGETDATA.Rows(k).Item("NW") = NW
    '                                    DTGETDATA.Rows(k).Item("GW") = GW
    '                                    ' M3 ,
    '                                    If rowValueQTYPER_BOX <> 0 And rowValueQTYBOXPER_LEVEL <> 0 Then
    '                                        rowValueHEIGHT = ((QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL) * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
    '                                    Else
    '                                        rowValueHEIGHT = 0
    '                                    End If

    '                                    M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000

    '                                    'DIMENSION

    '                                    DIMENSION = rowValueWIDTH * 10 & " x " & rowValueLENGTH * 10 & " x " & rowValueHEIGHT * 10

    '                                    'Update fields
    '                                    DTGETDATA.Rows(k).Item("M3") = M3
    '                                    DTGETDATA.Rows(k).Item("DIMENSION") = DIMENSION

    '                                Next

    '                            Else
    '                                'EXIST IN FMSPACKING

    '                            End If

    '                        Catch ex As Exception
    '                            WriteLog("Error 460 GETDATA() :" & ex.Message)
    '                        End Try

    '                    Case Else
    '                        Call CLEARTXT()
    '                        MessageBox.Show("WARNNING : APPLICATION CANNOT CLASSIFY PATTERN A OR B. PLEASE CHECK A/R CUSTOMER OPTION FIELD 'PATTERN'.")
    '                        Exit Sub

    '                End Select

    '                For K = 0 To DTGETDATA.Rows.Count - 1
    '                    DTGETDATA.Rows(K).Item("NO") = K + 1
    '                    Dim vGETPONO As String = MASTER.GETPONO(ORDNUMBER)
    '                    If vGETPONO.TrimEnd = "" Then

    '                    Else
    '                        DTGETDATA.Rows(K).Item("PONO") = vGETPONO
    '                    End If

    '                    DTGETDATA.Rows(K).Item("MARK") = MASTER.GETMARK(ORDNUMBER)

    '                Next

    '            Else
    '                Exit Sub
    '            End If
    '        Catch ex As Exception
    '            WriteLog("Error 550 GETDATA() :" & ex.Message)
    '        End Try


    '        '3. DISPLAY DATA 
    '        Try
    '            DisplayDGVMAIN(DTGETDATA)
    '            DisplayHEADER(DTGETDATA)

    '        Catch ex As Exception
    '            WriteLog("Error 560 GETDATA() :" & ex.Message)
    '        End Try

    '    Catch ex As Exception
    '        WriteLog("Error 565 GETDATA() :" & ex.Message)
    '    End Try
    'End Sub

    Sub GETDATA()
        Try
            Dim DTGETDATA As New DataTable()
            Dim chkFMSPACKING As Boolean
            Dim ORDNUMBER As String = txtMain_OrderNo.Text.TrimEnd
            'METHOD: 

            '1. GET DATA 
            Try

                DTGETDATA = MASTER.GETDATA(ORDNUMBER, chkFMSPACKING)

            Catch ex As Exception
                WriteLog("Error 187 GETDATA() :" & ex.Message)
            End Try


            '2. PROCESS DATA
            Try

                Dim PATTERN As String = ""
                If DTGETDATA.Rows.Count <> 0 Then
                    PATTERN = MASTER.GETPATTERN(DTGETDATA.Rows(0).Item("CUSTOMER").ToString)

                    Select Case PATTERN

                        Case "A"
                            '2.1 PATTERN A
                            Try
                                'CALCURATE 
                                ''CALCURATE QTY 
                                If chkFMSPACKING = False Then 'NO EXIST IN FMSPACKING
                                    If DTGETDATA.Rows.Count > 0 Then
                                        Dim CNT As Decimal = DTGETDATA.Rows.Count - 1
                                        For i = 0 To CNT
                                            Dim j As Integer = 0
                                            Dim ITEMNO As String = DTGETDATA.Rows(i).Item("ITEM").ToString.TrimEnd
                                            Dim QTYSHPTODT As Decimal
                                            Dim VQTYSHPTODT As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYSHPTODT").ToString).ToString("F2")
                                            Dim QTYBACKORD As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYBACKORD").ToString).ToString("F2")
                                            Dim QTYPER_PALLET As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYPER_PALLET").ToString).ToString("F2")


                                            If VQTYSHPTODT = 0 Then
                                                QTYSHPTODT = QTYBACKORD
                                            Else
                                                QTYSHPTODT = VQTYSHPTODT
                                            End If

                                            If QTYSHPTODT > QTYPER_PALLET Then
                                                DTGETDATA.Rows(i).Item("QTY") = QTYPER_PALLET

                                                If QTYPER_PALLET = 0 Then
                                                    MessageBox.Show("Please Checking item " & ITEMNO & " QTY per PALLET = 0  ")
                                                Else
                                                    'Determine number of rows
                                                    Dim NUMBERROWS As Decimal

                                                    NUMBERROWS = QTYSHPTODT / QTYPER_PALLET

                                                    NUMBERROWS = Math.Ceiling(NUMBERROWS)

                                                    'For loop add as determine number of rows
                                                    For j = 0 To NUMBERROWS - 2
                                                        Dim row As DataRow = DTGETDATA.Rows(i)

                                                        Select Case j
                                                            Case NUMBERROWS - 2
                                                                DTGETDATA.ImportRow(row)
                                                                DTGETDATA.AcceptChanges()
                                                                Dim rowCnt As Integer = DTGETDATA.Rows.Count - 1
                                                                DTGETDATA.Rows(rowCnt).Item("QTY") = QTYSHPTODT - ((NUMBERROWS - 1) * QTYPER_PALLET)
                                                                row = Nothing

                                                            Case Else
                                                                DTGETDATA.ImportRow(row)
                                                                row = Nothing

                                                        End Select
                                                    Next

                                                End If
                                            Else
                                                'Case QTYSHPTODT < QTYPER_PALLET

                                                DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT


                                            End If
                                        Next

                                        DTGETDATA.DefaultView.Sort = "LINENUM"
                                    Else
                                        Exit Try
                                    End If

                                    'CALCURATE N.W. , G.W. , M3 , Dimension 
                                    Dim DTITEM As DataTable = New DataTable
                                    DTITEM = MASTER.GETFMSMASTERITEM()

                                    For k = 0 To DTGETDATA.Rows.Count - 1

                                        Dim ITEM As String = ""
                                        Dim QTY As String = ""
                                        Dim NW As String = ""
                                        Dim GW As String = ""
                                        Dim M3 As String = ""
                                        Dim DIMENSION As String = ""

                                        Dim ROWDTCAL() As DataRow
                                        Dim rowValueNW As Decimal
                                        Dim rowValueGW As Decimal
                                        Dim rowValueHEIGHT As Decimal
                                        Dim rowValueBOXWEIGHT As Decimal
                                        Dim rowValueQTYPER_BOX As Decimal
                                        Dim rowValuePALLET_WEIGHT As Decimal
                                        Dim rowValuePALLET_HEIGHT As Decimal

                                        Dim rowValueQTYBOXPER_LEVEL As Decimal
                                        Dim rowValueHEIGHTPER_LEVEL As Decimal

                                        Dim rowValueWIDTH As Decimal
                                        Dim rowValueLENGTH As Decimal

                                        If DTITEM.Rows.Count > 0 Then

                                            ITEM = DTGETDATA.Rows(k).Item("ITEM").ToString.TrimEnd
                                            QTY = DTGETDATA.Rows(k).Item("QTY").ToString.TrimEnd

                                            ' N.W.
                                            ROWDTCAL = DTITEM.[Select]("ITEMNO =  '" & ITEM & "' AND IDCUST = '" & DTGETDATA.Rows(k).Item("CUSTOMER").ToString.TrimEnd & "'  ")

                                            If ROWDTCAL.Count > 0 Then
                                                rowValueNW = ROWDTCAL(0).Item("NETWEIGHT").ToString.TrimEnd
                                            End If

                                            NW = CDec(QTY) * CDec(rowValueNW)

                                            'G.W"
                                            rowValueGW = ROWDTCAL(0).Item("GROSSWEIGHT").ToString.TrimEnd

                                            rowValueBOXWEIGHT = ROWDTCAL(0).Item("BOXWEIGHT").ToString.TrimEnd
                                            rowValueQTYPER_BOX = ROWDTCAL(0).Item("QTYPER_BOX").ToString.TrimEnd
                                            rowValuePALLET_WEIGHT = ROWDTCAL(0).Item("PALLET_WEIGHT").ToString.TrimEnd
                                            rowValueQTYBOXPER_LEVEL = ROWDTCAL(0).Item("QTYBOXPER_LEVEL").ToString.TrimEnd
                                            rowValueHEIGHTPER_LEVEL = ROWDTCAL(0).Item("HEIGHTPER_LEVEL").ToString.TrimEnd
                                            rowValuePALLET_HEIGHT = ROWDTCAL(0).Item("PALLET_HEIGHT").ToString.TrimEnd
                                            rowValueWIDTH = ROWDTCAL(0).Item("WIDTH").ToString.TrimEnd
                                            rowValueLENGTH = ROWDTCAL(0).Item("LENGTH").ToString.TrimEnd

                                            If rowValueQTYPER_BOX <> 0 Then
                                                'GW = (CDec(rowValueGW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
                                                '24/02/2022
                                                'GW = (CDec(NW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
                                                GW = (CDec(rowValueNW) * CDec(QTY))
                                                'GW = GW + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX)))
                                                GW = GW + (CDec(rowValueBOXWEIGHT) * Math.Ceiling((CDec(QTY) / CDec(rowValueQTYPER_BOX))))
                                                GW = GW + (CDec(rowValuePALLET_WEIGHT) * 1)
                                            Else
                                                GW = 0.0000
                                            End If
                                        End If
                                        'Update fields
                                        DTGETDATA.Rows(k).Item("NW") = Math.Round(CDec(NW), 5)
                                        DTGETDATA.Rows(k).Item("GW") = Math.Round(CDec(GW), 5)
                                        ' M3 ,
                                        If rowValueQTYPER_BOX <> 0 And rowValueQTYBOXPER_LEVEL <> 0 Then
                                            ' rowValueHEIGHT = ((QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL) * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
                                            '24/02/2022
                                            Dim A As Decimal = (QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL)
                                            A = Math.Ceiling(A)
                                            rowValueHEIGHT = (A * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
                                            rowValueHEIGHT = Math.Round(rowValueHEIGHT, 2)
                                        Else
                                            rowValueHEIGHT = 0.00
                                        End If

                                        M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000


                                        'DIMENSION

                                        DIMENSION = rowValueWIDTH & " x " & rowValueLENGTH & " x " & rowValueHEIGHT

                                        'Update fields
                                        DTGETDATA.Rows(k).Item("M3") = M3
                                        DTGETDATA.Rows(k).Item("DIMENSION") = DIMENSION

                                    Next

                                Else
                                    'EXIST IN FMSPACKING

                                End If

                            Catch ex As Exception
                                WriteLog("Error 347 GETDATA() :" & ex.Message)
                            End Try

                        Case "B"
                            '2.2 PATTERN B
                            Try
                                'CALCURATE 
                                ''CALCURATE QTY 
                                If chkFMSPACKING = False Then 'NO EXIST IN FMSPACKING
                                    If DTGETDATA.Rows.Count > 0 Then
                                        Dim CNT As Decimal = DTGETDATA.Rows.Count - 1
                                        For i = 0 To CNT
                                            Dim j As Integer = 0
                                            Dim ITEMNO As String = DTGETDATA.Rows(i).Item("ITEM").ToString.TrimEnd
                                            Dim QTYSHPTODT As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYSHPTODT").ToString).ToString("F2")
                                            Dim QTYPER_PALLET As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYPER_PALLET").ToString).ToString("F2")
                                            Dim QTYBACKORD As Decimal = CDec(DTGETDATA.Rows(i).Item("QTYBACKORD").ToString).ToString("F2")

                                            If QTYPER_PALLET = 0 Then
                                                DTGETDATA.Rows(i).Item("PALLET") = 0.00
                                            Else
                                                DTGETDATA.Rows(i).Item("PALLET") = Math.Ceiling((QTYSHPTODT + QTYBACKORD) / QTYPER_PALLET)
                                            End If
                                            QTYSHPTODT = QTYSHPTODT + QTYBACKORD

                                            'If QTYSHPTODT > QTYPER_PALLET Then
                                            '    DTGETDATA.Rows(i).Item("QTY") = QTYPER_PALLET

                                            '    If QTYPER_PALLET = 0 Then
                                            '        MessageBox.Show("Please Checking item " & ITEMNO & " QTY per PALLET = 0  ")
                                            '    Else
                                            '        'Determine number of rows
                                            '        Dim NUMBERROWS As Decimal

                                            '        NUMBERROWS = QTYSHPTODT / QTYPER_PALLET

                                            '        NUMBERROWS = Math.Ceiling(NUMBERROWS)

                                            '        ''For loop add as determine number of rows
                                            '        'For j = 0 To NUMBERROWS - 2
                                            '        '    Dim row As DataRow = DTGETDATA.Rows(i)

                                            '        '    Select Case j
                                            '        '        Case NUMBERROWS - 2
                                            '        '            DTGETDATA.ImportRow(row)
                                            '        '            DTGETDATA.AcceptChanges()
                                            '        '            Dim rowCnt As Integer = DTGETDATA.Rows.Count - 1
                                            '        '            DTGETDATA.Rows(rowCnt).Item("QTY") = QTYSHPTODT - ((NUMBERROWS - 1) * QTYPER_PALLET)
                                            '        '            row = Nothing

                                            '        '        Case Else
                                            '        '            DTGETDATA.ImportRow(row)
                                            '        '            row = Nothing

                                            '        '    End Select
                                            '        'Next

                                            '    End If
                                            'Else
                                            '    'Case QTYSHPTODT < QTYPER_PALLET
                                            '    DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT
                                            'End If
                                            DTGETDATA.Rows(i).Item("QTY") = QTYSHPTODT
                                        Next

                                        DTGETDATA.DefaultView.Sort = "LINENUM"

                                    Else
                                        Exit Try
                                    End If

                                    'CALCURATE N.W. , G.W. , M3 , Dimension 
                                    Dim DTITEM As DataTable = New DataTable
                                    DTITEM = MASTER.GETFMSMASTERITEM()

                                    For k = 0 To DTGETDATA.Rows.Count - 1

                                        Dim ITEM As String = ""
                                        Dim QTY As String = ""
                                        Dim NW As String = ""
                                        Dim GW As String = ""
                                        Dim M3 As String = ""
                                        Dim DIMENSION As String = ""

                                        Dim ROWDTCAL() As DataRow
                                        Dim rowValueNW As Decimal
                                        Dim rowValueGW As Decimal
                                        Dim rowValueHEIGHT As Decimal
                                        Dim rowValueBOXWEIGHT As Decimal
                                        Dim rowValueQTYPER_BOX As Decimal
                                        Dim rowValuePALLET_WEIGHT As Decimal
                                        Dim rowValuePALLET_HEIGHT As Decimal

                                        Dim rowValueQTYBOXPER_LEVEL As Decimal
                                        Dim rowValueHEIGHTPER_LEVEL As Decimal

                                        Dim rowValueWIDTH As Decimal
                                        Dim rowValueLENGTH As Decimal

                                        Dim HEIGHT As Decimal

                                        If DTITEM.Rows.Count > 0 Then

                                            ITEM = DTGETDATA.Rows(k).Item("ITEM").ToString.TrimEnd
                                            QTY = DTGETDATA.Rows(k).Item("QTY").ToString.TrimEnd
                                            Dim VPALLET As Decimal
                                            VPALLET = CDec(DTGETDATA.Rows(k).Item("PALLET").ToString.TrimEnd)
                                            ' N.W.
                                            'ROWDTCAL = DTITEM.[Select]("ITEMNO =  '" & ITEM & "'")
                                            ROWDTCAL = DTITEM.[Select]("ITEMNO =  '" & ITEM & "' AND IDCUST = '" & DTGETDATA.Rows(k).Item("CUSTOMER").ToString.TrimEnd & "'  ")

                                            If ROWDTCAL.Count > 0 Then
                                                rowValueNW = ROWDTCAL(0).Item("NETWEIGHT").ToString.TrimEnd
                                            End If

                                            NW = CDec(QTY) * CDec(rowValueNW)

                                            'G.W"
                                            rowValueGW = ROWDTCAL(0).Item("GROSSWEIGHT").ToString.TrimEnd
                                            rowValueBOXWEIGHT = ROWDTCAL(0).Item("BOXWEIGHT").ToString.TrimEnd
                                            rowValueQTYPER_BOX = ROWDTCAL(0).Item("QTYPER_BOX").ToString.TrimEnd
                                            rowValuePALLET_WEIGHT = ROWDTCAL(0).Item("PALLET_WEIGHT").ToString.TrimEnd
                                            rowValueQTYBOXPER_LEVEL = ROWDTCAL(0).Item("QTYBOXPER_LEVEL").ToString.TrimEnd
                                            rowValueHEIGHTPER_LEVEL = ROWDTCAL(0).Item("HEIGHTPER_LEVEL").ToString.TrimEnd
                                            rowValuePALLET_HEIGHT = ROWDTCAL(0).Item("PALLET_HEIGHT").ToString.TrimEnd
                                            rowValueWIDTH = ROWDTCAL(0).Item("WIDTH").ToString.TrimEnd
                                            rowValueLENGTH = ROWDTCAL(0).Item("LENGTH").ToString.TrimEnd
                                            HEIGHT = ROWDTCAL(0).Item("HEIGHT").ToString.TrimEnd


                                            If rowValueQTYPER_BOX <> 0 Then
                                                'GW = (CDec(rowValueGW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
                                                '24/02/2022
                                                'GW = (CDec(NW) * CDec(QTY)) + (CDec(rowValueBOXWEIGHT) * (CDec(QTY) / CDec(rowValueQTYPER_BOX))) + (CDec(rowValuePALLET_WEIGHT) * 1)
                                                GW = (CDec(rowValueNW) * CDec(QTY))
                                                'GW = GW + (CDec(rowValueBOXWEIGHT) * Math.Round((CDec(QTY) / CDec(rowValueQTYPER_BOX)), 2))
                                                '20/04/2022
                                                GW = GW + (CDec(rowValueBOXWEIGHT) * Math.Ceiling((CDec(QTY) / CDec(rowValueQTYPER_BOX))))

                                                WriteLog("1.CDec(QTY) " & CDec(QTY))
                                                WriteLog("2.CDec(rowValueQTYPER_BOX)" & CDec(rowValueQTYPER_BOX))
                                                WriteLog("3.(CDec(QTY) / CDec(rowValueQTYPER_BOX)" & (CDec(QTY) / CDec(rowValueQTYPER_BOX)))
                                                WriteLog("4.Math.Round((CDec(QTY) / CDec(rowValueQTYPER_BOX)), 2) = " & Math.Round((CDec(QTY) / CDec(rowValueQTYPER_BOX)), 2))
                                                WriteLog("5. Math.Ceiling((CDec(QTY) / CDec(rowValueQTYPER_BOX))) = " & Math.Ceiling((CDec(QTY) / CDec(rowValueQTYPER_BOX))))


                                                'GW = GW + (CDec(rowValuePALLET_WEIGHT) * 1)
                                                GW = GW + (CDec(rowValuePALLET_WEIGHT) * VPALLET)
                                            Else
                                                GW = 0.0000
                                            End If
                                        End If
                                        'Update fields
                                        DTGETDATA.Rows(k).Item("NW") = Math.Round(CDec(NW), 5)
                                        DTGETDATA.Rows(k).Item("GW") = Math.Round(CDec(GW), 5)
                                        ' M3 ,
                                        If rowValueQTYPER_BOX <> 0 And rowValueQTYBOXPER_LEVEL <> 0 Then
                                            ' rowValueHEIGHT = ((QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL) * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
                                            '24/02/2022
                                            Dim A As Decimal = (QTY / rowValueQTYPER_BOX / rowValueQTYBOXPER_LEVEL)
                                            A = Math.Ceiling(A)
                                            rowValueHEIGHT = (A * rowValueHEIGHTPER_LEVEL) + rowValuePALLET_HEIGHT
                                            rowValueHEIGHT = Math.Round(rowValueHEIGHT, 2)
                                        Else
                                            rowValueHEIGHT = 0.00
                                        End If

                                        'M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000
                                        '24/06/2022
                                        M3 = (rowValueWIDTH * rowValueLENGTH * rowValueHEIGHT) / 1000000000

                                        'DIMENSION

                                        ' DIMENSION = rowValueWIDTH * 10 & " x " & rowValueLENGTH * 10 & " x " & rowValueHEIGHT * 10

                                        'DIMENSION = rowValueWIDTH * 10 & " x " & rowValueLENGTH * 10 & " x " & HEIGHT * 10

                                        '24/06/2022
                                        DIMENSION = rowValueWIDTH & " x " & rowValueLENGTH & " x " & HEIGHT

                                        'Update fields
                                        DTGETDATA.Rows(k).Item("M3") = M3
                                        DTGETDATA.Rows(k).Item("DIMENSION") = DIMENSION

                                    Next

                                Else
                                    'EXIST IN FMSPACKING

                                End If

                            Catch ex As Exception
                                WriteLog("Error 460 GETDATA() :" & ex.Message)
                            End Try

                        Case Else
                            Call CLEARTXT()
                            MessageBox.Show("WARNNING : APPLICATION CANNOT CLASSIFY PATTERN A OR B. PLEASE CHECK A/R CUSTOMER OPTION FIELD 'PATTERN'.")
                            Exit Sub

                    End Select
                    DTGETDATA.DefaultView.Sort = "LINENUM"
                    DTGETDATA = DTGETDATA.DefaultView.ToTable
                    DTGETDATA.AcceptChanges()



                    For K = 0 To DTGETDATA.Rows.Count - 1

                        DTGETDATA.Rows(K).Item("NO") = K + 1

                        Dim vGETPONO As String = MASTER.GETPONO(ORDNUMBER)
                        If vGETPONO.TrimEnd = "" Then

                        Else
                            DTGETDATA.Rows(K).Item("PONO") = vGETPONO
                        End If

                        DTGETDATA.Rows(K).Item("MARK") = MASTER.GETMARK(ORDNUMBER)

                    Next

                Else
                    Exit Sub
                End If
            Catch ex As Exception
                WriteLog("Error 550 GETDATA() :" & ex.Message)
            End Try


            '3. DISPLAY DATA 
            Try

                DisplayDGVMAIN(DTGETDATA)
                DisplayHEADER(DTGETDATA)

            Catch ex As Exception
                WriteLog("Error 560 GETDATA() :" & ex.Message)
            End Try

        Catch ex As Exception
            WriteLog("Error 565 GETDATA() :" & ex.Message)
        End Try
    End Sub

    Public Sub DisplayDGVMAIN(ByVal DTGETDATA As DataTable)
        DGV_MAIN.ItemsSource = Nothing
        Try
            DTGETDATA.DefaultView.Sort = "LINENUM"
            Dim DT As DataTable = New DataTable()
            DT = DTGETDATA.Clone
            DT = DTGETDATA.Copy
            DT.DefaultView.Sort = "LINENUM"
            DGV_MAIN.ItemsSource = DT.DefaultView

            With DGV_MAIN

                .Columns(0).Header = "No."
                .Columns(16).Header = "Item No."
                .Columns(17).Header = "Part No."
                .Columns(18).Header = "Part Name"
                .Columns(19).Header = "Item Description"
                .Columns(20).Header = "QTY"
                .Columns(21).Header = "Unit"
                .Columns(22).Header = "Pallet"
                .Columns(23).Header = "N.W.(Kgs.)"
                .Columns(24).Header = "G.W.(Kgs.)"
                .Columns(25).Header = "M^3"
                .Columns(26).Header = "Dimension"

            End With

            With DGV_MAIN
                .Columns(1).Visibility = Visibility.Hidden
                .Columns(2).Visibility = Visibility.Hidden
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

                .Columns(27).Visibility = Visibility.Hidden
                .Columns(28).Visibility = Visibility.Hidden
                .Columns(29).Visibility = Visibility.Hidden

                .Columns(30).Visibility = Visibility.Hidden

                .Columns(31).Visibility = Visibility.Hidden
                .Columns(32).Visibility = Visibility.Hidden
                .Columns(33).Visibility = Visibility.Hidden
                .Columns(34).Visibility = Visibility.Hidden

                .Columns(35).Visibility = Visibility.Hidden
                .Columns(36).Visibility = Visibility.Hidden


            End With



        Catch ex As Exception
            WriteLog("Error 570 GETDATA() :" & ex.Message)
        End Try
    End Sub

    Sub DisplayHEADER(ByVal DTGETDATA As DataTable)
        Try
            If DTGETDATA.Rows.Count > 0 Then
                'txtMain_OrderDate.Text = (DTGETDATA.Rows(0).Item("ORDDATE").ToString.TrimEnd)
                txtMain_OrderDate.Text = DateTime.ParseExact(DTGETDATA.Rows(0).Item("ORDDATE").ToString, "yyyyMMdd", Nothing).ToString("dd\/MM\/yyyy")
                txtMain_IDCUST.Text = DTGETDATA.Rows(0).Item("CUSTOMER").ToString.TrimEnd
                'If DTGETDATA.Columns.Contains("BILNAME") = True Then
                '    txtMain_NAMECUST.Text = DTGETDATA.Rows(0).Item("BILNAME").ToString.TrimEnd
                'ElseIf DTGETDATA.Columns.Contains("BILNAME") = True Then
                '    txtMain_NAMECUST.Text = DTGETDATA.Rows(0).Item("BILNAME").ToString.TrimEnd
                'End If
                txtMain_NAMECUST.Text = DTGETDATA.Rows(0).Item("BILNAME").ToString.TrimEnd
                txtMain_INVNO.Text = DTGETDATA.Rows(0).Item("DESC").ToString.TrimEnd
                    txtMain_INVDATE.SelectedDate = DateTime.ParseExact(DTGETDATA.Rows(0).Item("EXPDATE").ToString, "yyyyMMdd", Nothing).ToString("dd\/MM\/yyyy")
                    txtMain_From.Text = DTGETDATA.Rows(0).Item("From").ToString.TrimEnd
                    txtMain_To.Text = DTGETDATA.Rows(0).Item("To").ToString.TrimEnd
                    txtMain_ETD.Text = DTGETDATA.Rows(0).Item("ETD").ToString.TrimEnd
                    txtMain_ETA.Text = DTGETDATA.Rows(0).Item("ETA").ToString.TrimEnd
                    txtMain_Freight.Text = DTGETDATA.Rows(0).Item("FREIGHT").ToString.TrimEnd
                    txtMain_FreightVessel.Text = DTGETDATA.Rows(0).Item("FLIGHTVESSEL").ToString.TrimEnd
                    txtMain_BL.Text = DTGETDATA.Rows(0).Item("BL").ToString.TrimEnd
                    txtMain_Discharge.Text = DTGETDATA.Rows(0).Item("PORTCHARGE").ToString.TrimEnd
                    txtMain_FinalDest.Text = DTGETDATA.Rows(0).Item("FINALDEST").ToString.TrimEnd
                    txtMain_PONO.Text = DTGETDATA.Rows(0).Item("PONO").ToString.TrimEnd
                    txtMain_TERM.Text = DTGETDATA.Rows(0).Item("TERM").ToString.TrimEnd
                    txtMain_SHIPMARK.Text = DTGETDATA.Rows(0).Item("MARK").ToString.TrimEnd

                End If

        Catch ex As Exception
            WriteLog("Error 590 DisplayHEADER() :" & ex.Message)
        End Try
    End Sub

    Sub CLEARTXT()
        Try
            txtMain_IDCUST.Text = ""
            txtMain_NAMECUST.Text = ""

            txtMain_INVNO.Text = ""
            txtMain_INVDATE.Text = ""
            txtMain_From.Text = ""
            txtMain_To.Text = ""
            txtMain_ETD.Text = ""
            txtMain_ETA.Text = ""
            txtMain_Freight.Text = ""
            txtMain_FreightVessel.Text = ""
            txtMain_BL.Text = ""
            txtMain_Discharge.Text = ""
            txtMain_FinalDest.Text = ""
            txtMain_PONO.Text = ""
            txtMain_TERM.Text = ""
            txtMain_SHIPMARK.Text = ""
            DGV_MAIN.ItemsSource = Nothing

        Catch ex As Exception
            WriteLog("Error 1218 CLEARTXT() :" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_MAIN_MouseRightButtonDown(sender As Object, e As MouseButtonEventArgs) Handles DGV_MAIN.MouseRightButtonDown
        SHOWMENUSTRIP = New ContextMenuStrip

        SHOWMENUSTRIP.Items.Add("Insert")
        SHOWMENUSTRIP.Items.Add("Re-Calcurate")
        SHOWMENUSTRIP.Items.Add("Delete")

        Dim boolEXIST As Boolean = MASTER.CHECKEXIST_FMSPACKING(txtMain_OrderNo.Text.TrimEnd)
        If boolEXIST = True Then
            SHOWMENUSTRIP.Items(0).Enabled = True
        Else
            SHOWMENUSTRIP.Items(0).Enabled = False
        End If


        AddHandler SHOWMENUSTRIP.ItemClicked, AddressOf SHOWMENUSTRIP_ItemClicked

        SHOWMENUSTRIP.Show(Control.MousePosition)

    End Sub

    Private Sub SHOWMENUSTRIP_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)

        Try

            Dim str As String = e.ClickedItem.Text

            Select Case str

                Case "Delete"
                    Dim dialogOK As MessageBoxResult = MsgBox("Do you want to delete this line ?", MessageBoxButton.YesNo)
                    If dialogOK = 6 Then
                        Dim rowDataRowView As DataRowView = DGV_MAIN.SelectedItem


                        Dim dtDEL As DataTable = CType(DGV_MAIN.ItemsSource, DataView).ToTable

                        For i = 0 To dtDEL.Rows.Count - 1
                            Dim ORDNUMBER As String = rowDataRowView.Row.ItemArray(1).ToString
                            Dim boolEXIST As Boolean = MASTER.CHECKEXIST_FMSPACKING(ORDNUMBER)
                            If boolEXIST = True Then
                                Dim indx As String = rowDataRowView.Row.ItemArray(35).ToString

                                Dim ITEM As String = rowDataRowView.Row.ItemArray(16).ToString
                                Dim CUSTOMER As String = rowDataRowView.Row.ItemArray(3).ToString
                                Dim SEQ As String = dtDEL.Rows(i).Item("SEQ").ToString

                                If dtDEL.Rows(i).Item(35).ToString = indx Then

                                    DataClass.DELETELINEFMSPACKING(ORDNUMBER, SEQ, ITEM, CUSTOMER)
                                    Call GETDATA()

                                    Exit For
                                End If

                            Else

                                dtDEL.Rows.RemoveAt(i)

                                dtDEL.AcceptChanges()

                                're-running No. 
                                For j = 0 To dtDEL.Rows.Count - 1
                                    dtDEL.Rows(j).Item(0) = j + 1
                                    dtDEL.AcceptChanges()
                                Next

                                DisplayDGVMAIN(dtDEL)
                                DisplayHEADER(dtDEL)
                                'DGV_MAIN.ItemsSource = dtDEL.DefaultView
                                Exit For

                            End If
                        Next
                    Else

                    End If

                Case "Insert"

                    'SELECT INDEX
                    Dim rowDataRowView As DataRowView = DGV_MAIN.SelectedItem
                    IDXITEM = rowDataRowView.Row.ItemArray(35).ToString
                    LINENUMITEM = rowDataRowView.Row.ItemArray(33).ToString
                    WriteLog("IDXITEM:" & IDXITEM)
                    WriteLog("LINENUMITEM:" & LINENUMITEM)

                    frmInsert = New FrmSearchInsert

                    frmInsert.Show()

                Case "Re-Calcurate"





            End Select
        Catch ex As Exception
            WriteLog("Error 1257 (SHOWMENUSTRIP_ItemClicked):" & ex.Message)
        End Try
    End Sub

    Private Sub DGV_MAIN_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles DGV_MAIN.CellEditEnding

        If e.Column.Header = "QTY" Then

            Dim QTY_EDIT As String = e.EditingElement.ToString

            QTY_EDIT = QTY_EDIT.Substring(33, QTY_EDIT.Length - 33)


            'Dim tb = TryCast(e.EditingElement, String)

            Dim ITEMNO As String = e.Row.Item(16).ToString
            Dim IDCUST As String = e.Row.Item(3).ToString

            Dim DTEDIT As DataTable = New DataTable

            DTEDIT = MASTER.GETFMSMASTERITEM("WHERE ITEMNO = '" & ITEMNO & "' AND  IDCUST = '" & IDCUST & "' ")

            'Dim As String = ""
            Dim STOCKUNIT As String = ""
            Dim PONO As String = frmMN.txtMain_PONO.Text.TrimEnd
            Dim TERM As String = frmMN.txtMain_TERM.Text.TrimEnd
            Dim MARK As String = frmMN.txtMain_SHIPMARK.Text.TrimEnd
            Dim LINENUM As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.TrimEnd, "LINENUM")
            Dim SEQ As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.TrimEnd, "SEQ")
            Dim ORDUNIQ As String = MASTER.GETMAXFMSPACKING(frmMN.txtMain_OrderNo.Text.Trim, "ORDUNIQ")
            Dim NW As Decimal
            Dim GW As Decimal
            Dim M3 As Decimal
            Dim DIMENSION As String


            If DTEDIT.Rows.Count <> 0 Then
                Dim QTYPERPALLET As String
                QTYPERPALLET = QTY_EDIT

                'CASE EDIT 

                '= dtITEMVALUE.Rows(0).Item("").ToString
                STOCKUNIT = DTEDIT.Rows(0).Item("STOCKUNIT").ToString
                '= dtITEMVALUE.Rows(0).Item("").ToString

                'RE-CALCURATE 
                Dim rowValueNW As Decimal = CDec(DTEDIT.Rows(0).Item("NETWEIGHT").ToString)
                Dim rowValueGW As Decimal = CDec(DTEDIT.Rows(0).Item("GROSSWEIGHT").ToString)
                Dim rowValueHEIGHT As Decimal
                Dim rowValueBOXWEIGHT As Decimal = CDec(DTEDIT.Rows(0).Item("BOXWEIGHT").ToString)
                Dim rowValueQTYPER_BOX As Decimal = CDec(DTEDIT.Rows(0).Item("QTYPER_BOX").ToString)
                Dim rowValuePALLET_WEIGHT As Decimal = CDec(DTEDIT.Rows(0).Item("PALLET_WEIGHT").ToString)
                Dim rowValuePALLET_HEIGHT As Decimal = CDec(DTEDIT.Rows(0).Item("PALLET_HEIGHT").ToString)

                Dim rowValueQTYBOXPER_LEVEL As Decimal = CDec(DTEDIT.Rows(0).Item("QTYBOXPER_LEVEL").ToString)
                Dim rowValueHEIGHTPER_LEVEL As Decimal = CDec(DTEDIT.Rows(0).Item("HEIGHTPER_LEVEL").ToString)

                Dim rowValueWIDTH As Decimal = CDec(DTEDIT.Rows(0).Item("WIDTH").ToString)
                Dim rowValueLENGTH As Decimal = CDec(DTEDIT.Rows(0).Item("LENGTH").ToString)

                'NW

                NW = CDec(QTYPERPALLET.ToString) * CDec(rowValueNW)

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

            'MessageBox.Show(NW & "-" & GW & "-" & M3 & "-" & DIMENSION)

            DTCHANGEEDIT = CType(DGV_MAIN.ItemsSource, DataView).ToTable

            For J = 0 To DTCHANGEEDIT.Rows.Count - 1
                If J = e.Row.GetIndex Then
                    DTCHANGEEDIT.Rows(J).Item("QTY") = QTY_EDIT
                    DTCHANGEEDIT.Rows(J).Item("NW") = NW
                    DTCHANGEEDIT.Rows(J).Item("GW") = GW
                    DTCHANGEEDIT.Rows(J).Item("M3") = M3
                    DTCHANGEEDIT.Rows(J).Item("DIMENSION") = DIMENSION

                End If

            Next

            DTCHANGEEDIT.AcceptChanges()

            'frmMN.DisplayDGVMAIN(DTCHANGEEDIT)
            'frmMN.DisplayHEADER(DTCHANGEEDIT)


        End If
    End Sub

    Private Sub DGV_MAIN_CurrentCellChanged(sender As Object, e As EventArgs) Handles DGV_MAIN.CurrentCellChanged
        Try


            If DTCHANGEEDIT.Rows.Count > 0 Then

                Dim DTCHANGE As DataTable = New DataTable
                DTCHANGE = DTCHANGEEDIT.Copy
                frmMN.DisplayDGVMAIN(DTCHANGE)
                frmMN.DisplayHEADER(DTCHANGE)
                DTCHANGEEDIT = Nothing
                DTCHANGEEDIT.Rows.Clear()
                DTCHANGEEDIT.Columns.Clear()
            End If



        Catch ex As Exception
            WriteLog("Error 1450 DGV_MAIN_CurrentCellChanged() :" & ex.Message)
        End Try
    End Sub









#End Region

End Class


