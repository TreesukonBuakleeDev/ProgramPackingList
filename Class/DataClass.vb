Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms
Public Class DataClass

#Region "ITEM MASTER"

#Region "INSERT"
    Public Shared Sub INSETICITEMTEMP()
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                Dim Str As String
                'Str = "DELETE FROM FMSICITEM_TEMP" & Environment.NewLine
                'Str &= "  INSERT INTO FMSICITEM_TEMP  " & Environment.NewLine
                'Str &= "  SELECT  " & Environment.NewLine
                'Str &= " ICITEM.ITEMNO, " & Environment.NewLine
                'Str &= " [DESC] ," & Environment.NewLine
                'Str &= " ISNULL(ICITMC.CUSTNO,'') AS IDCUST," & Environment.NewLine
                'Str &= " STOCKUNIT," & Environment.NewLine
                'Str &= " 0 AS WIDTH, " & Environment.NewLine
                'Str &= " 0 AS [LENGTH], " & Environment.NewLine
                'Str &= " 0 AS [HEIGHT],  " & Environment.NewLine
                'Str &= " 0 AS QTYPER_BOX, " & Environment.NewLine
                'Str &= " 0 AS QTYPER_PALLET, " & Environment.NewLine
                'Str &= " 0 AS NUM_LAYER," & Environment.NewLine
                'Str &= " 0 AS HEIGHTPER_LEVEL, " & Environment.NewLine
                'Str &= " 0 AS PALLET_HEIGHT, " & Environment.NewLine
                'Str &= " 0 AS PALLET_WEIGHT, " & Environment.NewLine
                'Str &= " 0 AS QTYBOXPER_LEVEL, " & Environment.NewLine
                'Str &= " 0 AS NETWEIGHT, " & Environment.NewLine
                'Str &= " 0 AS GROSSWEIGHT, " & Environment.NewLine
                'Str &= " 0 AS BOXWEIGHT, " & Environment.NewLine
                'Str &= " '' AS STA_0, " & Environment.NewLine
                'Str &= " '' AS [USER]," & Environment.NewLine
                'Str &= " '' AS [TIMESTAMP] " & Environment.NewLine

                'Str &= " FROM " & SCHEMA & ".dbo.ICITEM " & Environment.NewLine
                'Str &= " LEFT OUTER JOIN " & SCHEMA & ".dbo.ICITMC ON ICITEM.ITEMNO = " & SCHEMA & ".dbo.ICITMC.ITEMNO  " & Environment.NewLine

                Str = "DELETE FROM FMSICITEM_TEMP " & Environment.NewLine
                Str &= "  INSERT INTO FMSICITEM_TEMP   " & Environment.NewLine
                Str &= "  SELECT  " & Environment.NewLine
                Str &= " ISNULL( FMSMASTERITEM.ITEMNO, ICITEM.ITEMNO),  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.[ITEMDESC], ICITEM.[DESC]) , " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.IDCUST , ICITMC.CUSTNO) AS IDCUST, " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.STOCKUNIT,ICITEM.STOCKUNIT) STOCKUNIT, " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.WIDTH ,0) AS WIDTH,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.[LENGTH] ,0) AS [LENGTH],  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.[HEIGHT] ,0) AS [HEIGHT],  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.QTYPER_BOX ,0) AS QTYPER_BOX,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.QTYPER_PALLET ,0) AS QTYPER_PALLET,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.NUM_LAYER ,0) AS NUM_LAYER, " & Environment.NewLine
                Str &= "ISNULL(FMSMASTERITEM.HEIGHTPER_LEVEL ,0) AS HEIGHTPER_LEVEL,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.PALLET_HEIGHT ,0) AS PALLET_HEIGHT,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.PALLET_WEIGHT ,0) AS PALLET_WEIGHT,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.QTYBOXPER_LEVEL ,0) AS QTYBOXPER_LEVEL,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.NETWEIGHT ,0) AS NETWEIGHT,  " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.GROSSWEIGHT ,0) AS GROSSWEIGHT, " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.BOXWEIGHT ,0) AS BOXWEIGHT, " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.STA_0 ,'') AS STA_0, " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.[USER] ,'') AS [USER], " & Environment.NewLine
                Str &= " ISNULL(FMSMASTERITEM.[TIMESTAMP] ,'') AS [TIMESTAMP]  " & Environment.NewLine

                Str &= " FROM " & SCHEMA & ".dbo.ICITEM " & Environment.NewLine
                Str &= " LEFT OUTER JOIN FMSMASTERITEM ON FMSMASTERITEM.ITEMNO = ICITEM.ITEMNO" & Environment.NewLine
                Str &= " LEFT OUTER JOIN " & SCHEMA & ".dbo.ICITMC On ICITEM.ITEMNO = " & SCHEMA & ".dbo.ICITMC.ITEMNO  " & Environment.NewLine
                Str &= " WHERE " & SCHEMA & ".dbo.ICITEM.STOCKITEM = 1 "

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()

            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 45 INSETICITEMTEMP():" & ex.Message)
        End Try
    End Sub

    Public Shared Sub INSERTFMSMASTERITEM(ByVal DTITEMSOURCE As DataTable, Optional ByVal BYIMPORT As String = "")
        Dim CountErr As Integer
        Dim DTITEM As DataTable = New DataTable
        Try
            If DTITEMSOURCE.Rows.Count > 0 Then

                DTITEM = DTITEMSOURCE.Copy
                DTITEM.Columns.Add("RESULT")
                Connection.Openconnect("DB", connect)
                If dtConfigDB.Rows.Count > 0 Then

                    For i = 0 To DTITEM.Rows.Count - 1

                        Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                        Dim Str As String = ""
                        If i = 0 Then

                            If BYIMPORT.TrimEnd = "IMPORT" Then
                                Str = "DELETE FROM FMSMASTERITEM WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                                Str &= "INSERT INTO [dbo].[FMSMASTERITEM] " & Environment.NewLine
                            Else
                                Str &= "INSERT INTO [dbo].[FMSMASTERITEM] " & Environment.NewLine
                            End If
                        Else
                            Str = "DELETE FROM FMSMASTERITEM WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                            Str &= "INSERT INTO [dbo].[FMSMASTERITEM] " & Environment.NewLine
                        End If

                        Str &= "          ([ITEMNO] " & Environment.NewLine
                        Str &= "          ,[ITEMDESC] " & Environment.NewLine
                        Str &= "          ,[IDCUST] " & Environment.NewLine
                        Str &= "          ,[STOCKUNIT] " & Environment.NewLine
                        Str &= "          ,[WIDTH] " & Environment.NewLine
                        Str &= "          ,[LENGTH] " & Environment.NewLine
                        Str &= "          ,[HEIGHT] " & Environment.NewLine
                        Str &= "          ,[QTYPER_BOX] " & Environment.NewLine
                        Str &= "          ,[QTYPER_PALLET] " & Environment.NewLine
                        Str &= "          ,[NUM_LAYER] " & Environment.NewLine
                        Str &= "          ,[HEIGHTPER_LEVEL] " & Environment.NewLine
                        Str &= "          ,[PALLET_HEIGHT] " & Environment.NewLine
                        Str &= "          ,[PALLET_WEIGHT] " & Environment.NewLine
                        Str &= "          ,[QTYBOXPER_LEVEL] " & Environment.NewLine
                        Str &= "           ,[NETWEIGHT] " & Environment.NewLine
                        Str &= "           ,[GROSSWEIGHT] " & Environment.NewLine
                        Str &= "          ,[BOXWEIGHT] " & Environment.NewLine
                        Str &= "          ,[STA_0] " & Environment.NewLine
                        Str &= "          ,[USER] " & Environment.NewLine
                        Str &= "          ,[TIMESTAMP]) " & Environment.NewLine
                        Str &= "    VALUES " & Environment.NewLine

                        Str &= "("
                        Str &= "           '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("ITEMDESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("STOCKUNIT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("WIDTH").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("LENGTH").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("HEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYPER_BOX").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYPER_PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("NUM_LAYER").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("HEIGHTPER_LEVEL").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("PALLET_HEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("PALLET_WEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYBOXPER_LEVEL").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("NETWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("GROSSWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("BOXWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine

                        Select Case DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd
                            Case "1"
                                Str &= "          ,'" & DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd & "' " & Environment.NewLine
                            Case "2"
                                Str &= "          ,'" & DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd & "' " & Environment.NewLine
                            Case Else
                                Str &= "          ,'1' " & Environment.NewLine
                        End Select

                        Str &= "          ,'" & DTITEM.Rows(i).Item("USER").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,GETDATE() " & Environment.NewLine
                        Str &= ")"

                        Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                        Try
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            CountErr = CountErr + 1
                            DTITEM.Rows(i).Item("RESULT") = "FAILED"
                            Continue For
                        End Try
                    Next
                Else

                End If
                connect.Close()
            Else

                WriteLog("Error 140 INSERTFMSMASTERITEM(): Error Empty Master data, The Application can not access source file. ")
                MessageBox.Show("Error 140 INSERTFMSMASTERITEM() : The Application can not access any data in source file.")
                Exit Sub
            End If
        Catch ex As Exception


            MessageBox.Show("INCOMPLETE")
            WriteLog("Error 150 INSERTFMSMASTERITEM():" & ex.Message)
            Exit Sub
        End Try
        If CountErr = 0 Then
            MessageBox.Show("Save Successfully" & DTITEM.Rows.Count & " / " & DTITEM.Rows.Count)
        Else
            'Gen Import result into Excel file 
            Call PROCESS.GENLISTIMPORT(DTITEM)

            MessageBox.Show("The application can import : " & DTITEM.Rows.Count - CountErr & " / " & DTITEM.Rows.Count)
        End If
    End Sub

    Public Shared Sub INSERTFMSICITEM_TEMP(ByVal DTITEMSOURCE As DataTable, Optional ByVal BYIMPORT As String = "")
        Dim CountErr As Integer
        Dim DTITEM As DataTable = New DataTable
        Try
            If DTITEMSOURCE.Rows.Count > 0 Then

                DTITEM = DTITEMSOURCE.Copy
                DTITEM.Columns.Add("RESULT")
                Connection.Openconnect("DB", connect)
                If dtConfigDB.Rows.Count > 0 Then

                    For i = 0 To DTITEM.Rows.Count - 1

                        Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                        Dim Str As String = ""
                        If i = 0 Then

                            Select Case BYIMPORT.TrimEnd
                                Case "IMPORT"
                                    Str = "DELETE FROM FMSICITEM_TEMP  " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSICITEM_TEMP] " & Environment.NewLine
                                Case "UPDATE"
                                    Str = "DELETE FROM FMSMASTERITEM WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' AND IDCUST = '" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd & "' " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSMASTERITEM] " & Environment.NewLine
                                Case Else
                                    Str = "DELETE FROM FMSICITEM_TEMP WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' AND IDCUST = '" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd & "' " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSICITEM_TEMP] " & Environment.NewLine
                            End Select

                        Else
                            Select Case BYIMPORT.TrimEnd
                                Case "IMPORT"
                                    Str = "DELETE FROM FMSICITEM_TEMP WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' AND IDCUST = '" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd & "' " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSICITEM_TEMP] " & Environment.NewLine
                                Case "UPDATE"
                                    Str = "DELETE FROM FMSMASTERITEM WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' AND IDCUST = '" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd & "' " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSMASTERITEM] " & Environment.NewLine
                                Case Else
                                    Str = "DELETE FROM FMSICITEM_TEMP WHERE ITEMNO = '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' AND IDCUST = '" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd & "' " & Environment.NewLine
                                    Str &= "INSERT INTO [dbo].[FMSICITEM_TEMP] " & Environment.NewLine
                            End Select

                        End If

                        Str &= "          ([ITEMNO] " & Environment.NewLine
                        Str &= "          ,[ITEMDESC] " & Environment.NewLine
                        Str &= "          ,[IDCUST] " & Environment.NewLine
                        Str &= "          ,[STOCKUNIT] " & Environment.NewLine
                        Str &= "          ,[WIDTH] " & Environment.NewLine
                        Str &= "          ,[LENGTH] " & Environment.NewLine
                        Str &= "          ,[HEIGHT] " & Environment.NewLine
                        Str &= "          ,[QTYPER_BOX] " & Environment.NewLine
                        Str &= "          ,[QTYPER_PALLET] " & Environment.NewLine
                        Str &= "          ,[NUM_LAYER] " & Environment.NewLine
                        Str &= "          ,[HEIGHTPER_LEVEL] " & Environment.NewLine
                        Str &= "          ,[PALLET_HEIGHT] " & Environment.NewLine
                        Str &= "          ,[PALLET_WEIGHT] " & Environment.NewLine
                        Str &= "          ,[QTYBOXPER_LEVEL] " & Environment.NewLine
                        Str &= "           ,[NETWEIGHT] " & Environment.NewLine
                        Str &= "           ,[GROSSWEIGHT] " & Environment.NewLine
                        Str &= "          ,[BOXWEIGHT] " & Environment.NewLine
                        Str &= "          ,[STA_0] " & Environment.NewLine
                        Str &= "          ,[USER] " & Environment.NewLine
                        Str &= "          ,[TIMESTAMP]) " & Environment.NewLine
                        Str &= "    VALUES " & Environment.NewLine

                        Str &= "("
                        Str &= "           '" & DTITEM.Rows(i).Item("ITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("ITEMDESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("IDCUST").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("STOCKUNIT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("WIDTH").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("LENGTH").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("HEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYPER_BOX").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYPER_PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("NUM_LAYER").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("HEIGHTPER_LEVEL").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("PALLET_HEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("PALLET_WEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("QTYBOXPER_LEVEL").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("NETWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("GROSSWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,'" & DTITEM.Rows(i).Item("BOXWEIGHT").ToString.TrimEnd & "' " & Environment.NewLine

                        Select Case DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd
                            Case "1"
                                Str &= "          ,'" & DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd & "' " & Environment.NewLine
                            Case "2"
                                Str &= "          ,'" & DTITEM.Rows(i).Item("STA_0").ToString.TrimEnd & "' " & Environment.NewLine
                            Case Else
                                Str &= "          ,'1' " & Environment.NewLine
                        End Select

                        Str &= "          ,'" & DTITEM.Rows(i).Item("USER").ToString.TrimEnd & "' " & Environment.NewLine
                        Str &= "          ,GETDATE() " & Environment.NewLine
                        Str &= ")"

                        Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                        Try
                            cmd.ExecuteNonQuery()
                        Catch ex As Exception
                            CountErr = CountErr + 1
                            DTITEM.Rows(i).Item("RESULT") = "FAILED"
                            Continue For
                        End Try
                    Next
                Else

                End If
                connect.Close()
            Else

                WriteLog("Error 268 INSERTFMSICITEM_TEMP(): Error Empty Master data, The Application can not access source file. ")
                MessageBox.Show(New Form With {.TopMost = True}, "Error 268 INSERTFMSICITEM_TEMP() : The Application can not access any data in source file.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As Exception

            MessageBox.Show(New Form With {.TopMost = True}, "INCOMPLETE", "Import", MessageBoxButtons.OK, MessageBoxIcon.Error)
            WriteLog("Error 273 INSERTFMSICITEM_TEMP():" & ex.Message)
            Exit Sub
        End Try
        If CountErr = 0 Then
            MessageBox.Show(New Form With {.TopMost = True}, "Save Successfully" & DTITEM.Rows.Count & " / " & DTITEM.Rows.Count, "Import", MessageBoxButtons.OK, MessageBoxIcon.Information)
            frmBrowse.Close()

        Else
            'Gen Import result into Excel file 
            Call PROCESS.GENLISTIMPORT(DTITEM)
            frmBrowse.Close()
            MessageBox.Show(New Form With {.TopMost = True}, "The application can import : " & DTITEM.Rows.Count - CountErr & " / " & DTITEM.Rows.Count, "Import", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

#End Region

#Region "MERGE"
    Public Shared Sub MERGEITEMMASTER()
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd

                Str = "MERGE FMSMASTERITEM AS A " & Environment.NewLine
                Str &= " USING FMSICITEM_TEMP AS B " & Environment.NewLine
                Str &= " ON (A.ITEMNO = B.ITEMNO) AND (A.IDCUST = B.IDCUST) " & Environment.NewLine
                Str &= "WHEN MATCHED  " & Environment.NewLine
                Str &= "THEN UPDATE  " & Environment.NewLine
                Str &= "SET			A.[ITEMNO]			= B.[ITEMNO]," & Environment.NewLine
                Str &= "			A.[ITEMDESC]		= B.[ITEMDESC]," & Environment.NewLine
                Str &= "			A.[IDCUST]			= B.[IDCUST]," & Environment.NewLine
                Str &= "			A.[STOCKUNIT]		= B.[STOCKUNIT]," & Environment.NewLine
                Str &= "			A.[WIDTH]			= B.[WIDTH]," & Environment.NewLine
                Str &= "			A.[LENGTH]			= B.[LENGTH], " & Environment.NewLine
                Str &= "			A.[HEIGHT]			= B.[HEIGHT], " & Environment.NewLine
                Str &= "			A.[QTYPER_BOX]		= B.[QTYPER_BOX], " & Environment.NewLine
                Str &= "			A.[QTYPER_PALLET]	= B.[QTYPER_PALLET], " & Environment.NewLine
                Str &= "			A.[NUM_LAYER]		= B.[NUM_LAYER], " & Environment.NewLine
                Str &= "			A.[HEIGHTPER_LEVEL] = B.[HEIGHTPER_LEVEL]," & Environment.NewLine
                Str &= "			A.[PALLET_HEIGHT]	= B.[PALLET_HEIGHT], " & Environment.NewLine
                Str &= "			A.[PALLET_WEIGHT]	= B.[PALLET_WEIGHT]," & Environment.NewLine
                Str &= "			A.[QTYBOXPER_LEVEL] = B.[QTYBOXPER_LEVEL]," & Environment.NewLine
                Str &= "			A.[NETWEIGHT]		= B.[NETWEIGHT]," & Environment.NewLine
                Str &= "			A.[GROSSWEIGHT]		= B.[GROSSWEIGHT]," & Environment.NewLine
                Str &= "			A.[BOXWEIGHT]		= B.[BOXWEIGHT]," & Environment.NewLine
                Str &= "			A.[STA_0]			= B.[STA_0]," & Environment.NewLine
                Str &= "			A.[USER]			= B.[USER]," & Environment.NewLine
                Str &= "			A.[TIMESTAMP]		= B.[TIMESTAMP]" & Environment.NewLine


                Str &= "When Not MATCHED BY TARGET " & Environment.NewLine
                Str &= "Then INSERT (	ITEMNO," & Environment.NewLine
                Str &= "				ITEMDESC," & Environment.NewLine
                Str &= "				IDCUST," & Environment.NewLine
                Str &= "				STOCKUNIT," & Environment.NewLine
                Str &= "				WIDTH," & Environment.NewLine
                Str &= "				[LENGTH]," & Environment.NewLine
                Str &= "				HEIGHT," & Environment.NewLine
                Str &= "				QTYPER_BOX," & Environment.NewLine
                Str &= "				QTYPER_PALLET," & Environment.NewLine
                Str &= "				NUM_LAYER," & Environment.NewLine
                Str &= "				HEIGHTPER_LEVEL," & Environment.NewLine
                Str &= "				PALLET_HEIGHT," & Environment.NewLine
                Str &= "				PALLET_WEIGHT," & Environment.NewLine
                Str &= "				QTYBOXPER_LEVEL," & Environment.NewLine
                Str &= "				NETWEIGHT," & Environment.NewLine
                Str &= "				GROSSWEIGHT," & Environment.NewLine
                Str &= "				BOXWEIGHT," & Environment.NewLine
                Str &= "				STA_0," & Environment.NewLine
                Str &= "				[USER]," & Environment.NewLine
                Str &= "				[TIMESTAMP]" & Environment.NewLine
                Str &= "			)" & Environment.NewLine
                Str &= "VALUES " & Environment.NewLine
                Str &= "            (" & Environment.NewLine
                Str &= "				B.ITEMNO," & Environment.NewLine
                Str &= "				B.ITEMDESC," & Environment.NewLine
                Str &= "				ISNULL(B.IDCUST,'')," & Environment.NewLine
                Str &= "				B.STOCKUNIT," & Environment.NewLine
                Str &= "				B.WIDTH," & Environment.NewLine
                Str &= "				B.[LENGTH]," & Environment.NewLine
                Str &= "				B.HEIGHT," & Environment.NewLine
                Str &= "				B.QTYPER_BOX," & Environment.NewLine
                Str &= "				B.QTYPER_PALLET," & Environment.NewLine
                Str &= "				B.NUM_LAYER," & Environment.NewLine
                Str &= "				B.HEIGHTPER_LEVEL," & Environment.NewLine
                Str &= "				B.PALLET_HEIGHT," & Environment.NewLine
                Str &= "				B.PALLET_WEIGHT," & Environment.NewLine
                Str &= "				B.QTYBOXPER_LEVEL," & Environment.NewLine
                Str &= "				B.NETWEIGHT," & Environment.NewLine
                Str &= "				B.GROSSWEIGHT," & Environment.NewLine
                Str &= "				B.BOXWEIGHT," & Environment.NewLine
                Str &= "				'1'," & Environment.NewLine
                Str &= "				B.[USER]," & Environment.NewLine
                Str &= "				GETDATE()" & Environment.NewLine
                Str &= "            );"

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()

            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 245 MERGEITEMMASTER():" & ex.Message & Str)
        End Try
    End Sub

#End Region

#Region "UPDATE"
    Public Shared Sub UPDATEFMSMASTERITEM(ByVal DTMASTERITEM_TEMP As DataTable)
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                For i = 0 To DTMASTERITEM_TEMP.Rows.Count - 1
                    If DTMASTERITEM_TEMP.Rows(i).Item("STA_0").ToString.TrimEnd = "2" Then
                        Dim ITEMNO As String = DTMASTERITEM_TEMP.Rows(i).Item("ITEMNO").ToString.TrimEnd
                        Dim IDCUST As String = DTMASTERITEM_TEMP.Rows(i).Item("IDCUST").ToString.TrimEnd
                        Dim Str As String
                        Str = "UPDATE FMSMASTERITEM SET STA_0 = 2 WHERE ITEMNO = '" & ITEMNO & "' AND IDCUST = '" & IDCUST & "' "

                        Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                        cmd.ExecuteNonQuery()
                    End If
                Next
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 187 UPDATEFMSMASTERITEM():" & ex.Message)
        End Try
    End Sub



#End Region

#End Region

#Region "MAIN"

#Region "INSERT"
    'Public Shared Sub INSERTFMSPACKING(ByVal dt As DataTable)
    '    Try
    '        'Clear Lasted records 

    '        Connection.Openconnect("DB", connect)
    '        If dtConfigDB.Rows.Count > 0 Then
    '            Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
    '            Dim Str As String = ""
    '            For i = 0 To dt.Rows.Count - 1
    '                If Str = "" Then
    '                    Str = "  INSERT INTO [dbo].[FMSPACKING]  " & Environment.NewLine
    '                Else
    '                    Str &= " INSERT INTO [dbo].[FMSPACKING]  " & Environment.NewLine
    '                End If
    '                Str &= "            ([ORDNUMBER] " & Environment.NewLine
    '                Str &= "            ,[ORDDATE] " & Environment.NewLine
    '                Str &= "            ,[CUSTOMER] " & Environment.NewLine
    '                Str &= "            ,[BILNAME] " & Environment.NewLine
    '                Str &= "            ,[DESC] " & Environment.NewLine
    '                Str &= "            ,[EXPDATE]" & Environment.NewLine
    '                Str &= "            ,[From] " & Environment.NewLine
    '                Str &= "            ,[To]" & Environment.NewLine
    '                Str &= "            ,[ETD]" & Environment.NewLine
    '                Str &= "            ,[ETA]" & Environment.NewLine
    '                Str &= "            ,[FREIGHT]" & Environment.NewLine
    '                Str &= "            ,[FLIGHTVESSEL]" & Environment.NewLine
    '                Str &= "            ,[BL]" & Environment.NewLine
    '                Str &= "            ,[PORTCHARGE]" & Environment.NewLine
    '                Str &= "            ,[FINALDEST]" & Environment.NewLine
    '                Str &= "            ,[ITEM]" & Environment.NewLine
    '                Str &= "            ,[CITEMNO]" & Environment.NewLine
    '                Str &= "            ,[PARTNAMEPO]" & Environment.NewLine
    '                Str &= "            ,[CITEMDESC]" & Environment.NewLine
    '                Str &= "            ,[QTY]" & Environment.NewLine
    '                Str &= "            ,[ORDUNIT]" & Environment.NewLine
    '                Str &= "            ,[PALLET]" & Environment.NewLine
    '                Str &= "            ,[NW]" & Environment.NewLine
    '                Str &= "            ,[GW]" & Environment.NewLine
    '                Str &= "            ,[M3]" & Environment.NewLine
    '                Str &= "            ,[DIMENSION]" & Environment.NewLine
    '                Str &= "            ,[ORDUNIQ]" & Environment.NewLine
    '                Str &= "            ,[QTYSHPTODT]" & Environment.NewLine
    '                Str &= "            ,[QTYPER_PALLET]" & Environment.NewLine
    '                Str &= "            ,[QTYBACKORD]" & Environment.NewLine
    '                Str &= "            ,[PONO]" & Environment.NewLine
    '                Str &= "            ,[TERM]" & Environment.NewLine
    '                Str &= "            ,[MARK]" & Environment.NewLine
    '                Str &= "            ,[LINENUM]" & Environment.NewLine
    '                Str &= "            ,[STA_0] " & Environment.NewLine
    '                Str &= "            ,[SEQ]" & Environment.NewLine
    '                Str &= "            )" & Environment.NewLine

    '                Str &= "      VALUES" & Environment.NewLine
    '                Str &= "            (" & Environment.NewLine

    '                Str &= "             '" & dt.Rows(i).Item("ORDNUMBER").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ORDDATE").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("CUSTOMER").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("BILNAME").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("DESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("EXPDATE").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("From").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("To").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ETD").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ETA").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("FREIGHT").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("FLIGHTVESSEL").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("BL").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("PORTCHARGE").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("FINALDEST").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ITEM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("CITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("PARTNAMEPO").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("CITEMDESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("QTY").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ORDUNIT").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("PALLET").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("NW").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("GW").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("M3").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("DIMENSION").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("ORDUNIQ").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("QTYSHPTODT").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("QTYPER_PALLET").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("QTYBACKORD").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("PONO").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("TERM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("MARK").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
    '                Str &= "            ,'" & dt.Rows(i).Item("LINENUM").ToString.TrimEnd & "' " & Environment.NewLine
    '                Str &= "            ,'1' --Insert  " & Environment.NewLine
    '                Str &= "            ," & i + 1 & "" & Environment.NewLine

    '                Str &= "            )" & Environment.NewLine


    '            Next
    '            Dim cmd As SqlCommand = New SqlCommand(Str, connect)
    '            cmd.ExecuteNonQuery()
    '        Else

    '        End If
    '        connect.Close()
    '    Catch ex As Exception
    '        WriteLog("Error 300 INSERTFMSPACKINGTEMP():" & ex.Message)
    '    End Try
    'End Sub
    Public Shared Function INSERTFMSPACKING(ByVal dt As DataTable) As Boolean
        Dim STA_0 As Boolean = False
        Try
            'Clear Lasted records 

            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                Dim Str As String = ""
                dt.DefaultView.Sort = "LINENUM"
                For i = 0 To dt.Rows.Count - 1
                    If Str = "" Then
                        Str = "  INSERT INTO [dbo].[FMSPACKING]  " & Environment.NewLine
                    Else
                        Str &= " INSERT INTO [dbo].[FMSPACKING]  " & Environment.NewLine
                    End If
                    Str &= "            ([ORDNUMBER] " & Environment.NewLine
                    Str &= "            ,[ORDDATE] " & Environment.NewLine
                    Str &= "            ,[CUSTOMER] " & Environment.NewLine
                    Str &= "            ,[BILNAME] " & Environment.NewLine
                    Str &= "            ,[DESC] " & Environment.NewLine
                    Str &= "            ,[EXPDATE]" & Environment.NewLine
                    Str &= "            ,[From] " & Environment.NewLine
                    Str &= "            ,[To]" & Environment.NewLine
                    Str &= "            ,[ETD]" & Environment.NewLine
                    Str &= "            ,[ETA]" & Environment.NewLine
                    Str &= "            ,[FREIGHT]" & Environment.NewLine
                    Str &= "            ,[FLIGHTVESSEL]" & Environment.NewLine
                    Str &= "            ,[BL]" & Environment.NewLine
                    Str &= "            ,[PORTCHARGE]" & Environment.NewLine
                    Str &= "            ,[FINALDEST]" & Environment.NewLine
                    Str &= "            ,[ITEM]" & Environment.NewLine
                    Str &= "            ,[CITEMNO]" & Environment.NewLine
                    Str &= "            ,[PARTNAMEPO]" & Environment.NewLine
                    Str &= "            ,[CITEMDESC]" & Environment.NewLine
                    Str &= "            ,[QTY]" & Environment.NewLine
                    Str &= "            ,[ORDUNIT]" & Environment.NewLine
                    Str &= "            ,[PALLET]" & Environment.NewLine
                    Str &= "            ,[NW]" & Environment.NewLine
                    Str &= "            ,[GW]" & Environment.NewLine
                    Str &= "            ,[M3]" & Environment.NewLine
                    Str &= "            ,[DIMENSION]" & Environment.NewLine
                    Str &= "            ,[ORDUNIQ]" & Environment.NewLine
                    Str &= "            ,[QTYSHPTODT]" & Environment.NewLine
                    Str &= "            ,[QTYPER_PALLET]" & Environment.NewLine
                    Str &= "            ,[QTYBACKORD]" & Environment.NewLine
                    Str &= "            ,[PONO]" & Environment.NewLine
                    Str &= "            ,[TERM]" & Environment.NewLine
                    Str &= "            ,[MARK]" & Environment.NewLine
                    Str &= "            ,[LINENUM]" & Environment.NewLine
                    Str &= "            ,[STA_0] " & Environment.NewLine
                    Str &= "            ,[SEQ]" & Environment.NewLine
                    Str &= "            )" & Environment.NewLine

                    Str &= "      VALUES" & Environment.NewLine
                    Str &= "            (" & Environment.NewLine

                    Str &= "             '" & dt.Rows(i).Item("ORDNUMBER").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDDATE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CUSTOMER").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("BILNAME").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("DESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("EXPDATE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("From").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("To").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ETD").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ETA").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FREIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FLIGHTVESSEL").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("BL").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PORTCHARGE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FINALDEST").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ITEM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PARTNAMEPO").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CITEMDESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTY").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDUNIT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("NW").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("GW").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("M3").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("DIMENSION").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDUNIQ").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYSHPTODT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYPER_PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYBACKORD").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PONO").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("TERM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("MARK").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("LINENUM").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'1' --Insert  " & Environment.NewLine
                    Str &= "            ," & i + 1 & "" & Environment.NewLine

                    Str &= "            )" & Environment.NewLine


                Next
                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
                STA_0 = True
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 300 INSERTFMSPACKINGTEMP():" & ex.Message)
            STA_0 = False
        End Try
        Return STA_0
    End Function
    Public Shared Function INSERTFMSPACKINGEDIT(ByVal dt As DataTable) As Boolean
        Dim STA_0 As Boolean = False
        Try
            'Clear Lasted records 

            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
                Dim Str As String = ""
                For i = 0 To dt.Rows.Count - 1
                    If Str = "" Then
                        Str = " DECLARE @idmax INT " & Environment.NewLine
                        Str &= "SELECT @idmax = ISNULL(MAX(TRANSID),0) + 1 FROM FMSPACKINGEDIT " & Environment.NewLine
                        Str &= "  INSERT INTO [dbo].[FMSPACKINGEDIT]  " & Environment.NewLine
                    Else
                        Str &= "  INSERT INTO [dbo].[FMSPACKINGEDIT]  " & Environment.NewLine
                    End If
                    Str &= "             ([ORDNUMBER] " & Environment.NewLine
                    Str &= "            ,[ORDDATE] " & Environment.NewLine
                    Str &= "            ,[CUSTOMER] " & Environment.NewLine
                    Str &= "            ,[BILNAME] " & Environment.NewLine
                    Str &= "            ,[DESC] " & Environment.NewLine
                    Str &= "            ,[EXPDATE]" & Environment.NewLine
                    Str &= "            ,[From] " & Environment.NewLine
                    Str &= "            ,[To]" & Environment.NewLine
                    Str &= "            ,[ETD]" & Environment.NewLine
                    Str &= "            ,[ETA]" & Environment.NewLine
                    Str &= "            ,[FREIGHT]" & Environment.NewLine
                    Str &= "            ,[FLIGHTVESSEL]" & Environment.NewLine
                    Str &= "            ,[BL]" & Environment.NewLine
                    Str &= "            ,[PORTCHARGE]" & Environment.NewLine
                    Str &= "            ,[FINALDEST]" & Environment.NewLine
                    Str &= "            ,[ITEM]" & Environment.NewLine
                    Str &= "            ,[CITEMNO]" & Environment.NewLine
                    Str &= "            ,[PARTNAMEPO]" & Environment.NewLine
                    Str &= "            ,[CITEMDESC]" & Environment.NewLine
                    Str &= "            ,[QTY]" & Environment.NewLine
                    Str &= "            ,[ORDUNIT]" & Environment.NewLine
                    Str &= "            ,[PALLET]" & Environment.NewLine
                    Str &= "            ,[NW]" & Environment.NewLine
                    Str &= "            ,[GW]" & Environment.NewLine
                    Str &= "            ,[M3]" & Environment.NewLine
                    Str &= "            ,[DIMENSION]" & Environment.NewLine
                    Str &= "            ,[ORDUNIQ]" & Environment.NewLine
                    Str &= "            ,[QTYSHPTODT]" & Environment.NewLine
                    Str &= "            ,[QTYPER_PALLET]" & Environment.NewLine
                    Str &= "            ,[QTYBACKORD]" & Environment.NewLine
                    Str &= "            ,[PONO]" & Environment.NewLine
                    Str &= "            ,[TERM]" & Environment.NewLine
                    Str &= "            ,[MARK]" & Environment.NewLine
                    Str &= "            ,[LINENUM]" & Environment.NewLine
                    Str &= "            ,[TIMESTAMP_0]" & Environment.NewLine
                    Str &= "            ,[STA_0]" & Environment.NewLine
                    Str &= "            ,[SEQ] " & Environment.NewLine
                    Str &= "            ,[TRANSID]" & Environment.NewLine
                    Str &= "            )" & Environment.NewLine
                    Str &= "      VALUES" & Environment.NewLine
                    Str &= "            (" & Environment.NewLine

                    Str &= "             '" & dt.Rows(i).Item("ORDNUMBER").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDDATE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CUSTOMER").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("BILNAME").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("DESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("EXPDATE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("From").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("To").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ETD").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ETA").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FREIGHT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FLIGHTVESSEL").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("BL").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PORTCHARGE").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("FINALDEST").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ITEM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CITEMNO").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PARTNAMEPO").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("CITEMDESC").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTY").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDUNIT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("NW").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("GW").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("M3").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("DIMENSION").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("ORDUNIQ").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYSHPTODT").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYPER_PALLET").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("QTYBACKORD").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("PONO").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("TERM").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("MARK").ToString.TrimEnd.Replace("'", "") & "' " & Environment.NewLine
                    Str &= "            ,'" & dt.Rows(i).Item("LINENUM").ToString.TrimEnd & "' " & Environment.NewLine
                    Str &= "            , GETDATE()" & Environment.NewLine
                    Str &= "            ,'2' --Edit" & Environment.NewLine
                    If MASTER.CHECKEXIST_FMSPACKING(dt.Rows(i).Item("ORDNUMBER").ToString.TrimEnd) = True Then
                        Str &= "        ,'" & dt.Rows(i).Item("SEQ").ToString.TrimEnd & "'" & Environment.NewLine
                    Else
                        Str &= "        ," & i + 1 & "" & Environment.NewLine
                    End If

                    Str &= "            ,@idmax " & Environment.NewLine
                    Str &= "            )" & Environment.NewLine

                Next
                Connection.Openconnect("DB", connect)
                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
                STA_0 = True
            Else
                STA_0 = False
            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 395 INSERTFMSPACKINGEDIT():" & ex.Message)
            STA_0 = False
        End Try
        Return STA_0
    End Function

#End Region

#Region "MERGE"

    Public Shared Sub MERGEFMSPACKING(ByVal ORDNO As String)
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBAPP").ToString.TrimEnd

                Str &= "MERGE FMSPACKING AS A  " & Environment.NewLine
                Str &= "USING  FMSPACKINGEDIT AS B ON (A.ORDNUMBER = B.ORDNUMBER  AND A.SEQ = B.SEQ AND B.TRANSID = (select MAX(TRANSID)  from FMSPACKINGEDIT) AND B.STA_0 <> 3) --3 - DELETE " & Environment.NewLine
                Str &= "WHEN MATCHED " & Environment.NewLine
                Str &= "THEN UPDATE " & Environment.NewLine
                Str &= "SET A.ORDNUMBER		= B.ORDNUMBER," & Environment.NewLine
                Str &= "	A.ORDDATE       = B.ORDDATE," & Environment.NewLine
                Str &= "	A.CUSTOMER      = B.CUSTOMER," & Environment.NewLine
                Str &= "	A.BILNAME       = B.BILNAME," & Environment.NewLine
                Str &= "	A.[DESC]        = B.[DESC]," & Environment.NewLine
                Str &= "	A.EXPDATE       = B.EXPDATE," & Environment.NewLine
                Str &= "	A.[From]        = B.[From]," & Environment.NewLine
                Str &= "	A.[To]          = B.[To]," & Environment.NewLine
                Str &= "	A.ETD           = B.ETD," & Environment.NewLine
                Str &= "	A.ETA           = B.ETA," & Environment.NewLine
                Str &= "	A.FREIGHT       = B.FREIGHT," & Environment.NewLine
                Str &= "	A.FLIGHTVESSEL  = B.FLIGHTVESSEL," & Environment.NewLine
                Str &= "	A.BL            = B.BL," & Environment.NewLine
                Str &= "	A.PORTCHARGE    = B.PORTCHARGE," & Environment.NewLine
                Str &= "	A.FINALDEST     = B.FINALDEST," & Environment.NewLine
                Str &= "	A.ITEM          = B.ITEM," & Environment.NewLine
                Str &= "	A.CITEMNO       = B.CITEMNO," & Environment.NewLine
                Str &= "	A.PARTNAMEPO    = B.PARTNAMEPO," & Environment.NewLine
                Str &= "	A.CITEMDESC     = B.CITEMDESC," & Environment.NewLine
                Str &= "	A.QTY           = B.QTY," & Environment.NewLine
                Str &= "	A.ORDUNIT       = B.ORDUNIT," & Environment.NewLine
                Str &= "	A.PALLET        = B.PALLET," & Environment.NewLine
                Str &= "	A.NW            = B.NW," & Environment.NewLine
                Str &= "	A.GW            = B.GW," & Environment.NewLine
                Str &= "	A.M3            = B.M3," & Environment.NewLine
                Str &= "	A.DIMENSION     = B.DIMENSION," & Environment.NewLine
                Str &= "	A.ORDUNIQ       = B.ORDUNIQ," & Environment.NewLine
                Str &= "	A.QTYSHPTODT    = B.QTYSHPTODT," & Environment.NewLine
                Str &= "	A.QTYPER_PALLET = B.QTYPER_PALLET," & Environment.NewLine
                Str &= "    A.QTYBACKORD    = B.QTYBACKORD," & Environment.NewLine
                Str &= "	A.PONO          = B.PONO," & Environment.NewLine
                Str &= "	A.TERM          = B.TERM," & Environment.NewLine
                Str &= "	A.MARK          = B.MARK," & Environment.NewLine
                Str &= "	A.LINENUM       = B.LINENUM," & Environment.NewLine
                Str &= "	A.STA_0         = B.STA_0" & Environment.NewLine
                Str &= "                When Not MATCHED BY TARGET And CAST(B.SEQ AS INT) > (SELECT MAX(SEQ) FROM FMSPACKING WHERE [ORDNUMBER] = B.ORDNUMBER )  THEN " & Environment.NewLine
                Str &= " INSERT" & Environment.NewLine
                Str &= "           ([ORDNUMBER]" & Environment.NewLine
                Str &= "           ,[ORDDATE]" & Environment.NewLine
                Str &= "           ,[CUSTOMER]" & Environment.NewLine
                Str &= "           ,[BILNAME]" & Environment.NewLine
                Str &= "           ,[DESC]" & Environment.NewLine
                Str &= "           ,[EXPDATE]" & Environment.NewLine
                Str &= "           ,[From]" & Environment.NewLine
                Str &= "           ,[To]" & Environment.NewLine
                Str &= "           ,[ETD]" & Environment.NewLine
                Str &= "           ,[ETA]" & Environment.NewLine
                Str &= "           ,[FREIGHT]" & Environment.NewLine
                Str &= "           ,[FLIGHTVESSEL]" & Environment.NewLine
                Str &= "           ,[BL]" & Environment.NewLine
                Str &= "          ,[PORTCHARGE]" & Environment.NewLine
                Str &= "           ,[FINALDEST]" & Environment.NewLine
                Str &= "           ,[ITEM]" & Environment.NewLine
                Str &= "           ,[CITEMNO]" & Environment.NewLine
                Str &= "           ,[PARTNAMEPO]" & Environment.NewLine
                Str &= "           ,[CITEMDESC]" & Environment.NewLine
                Str &= "           ,[QTY]" & Environment.NewLine
                Str &= "           ,[ORDUNIT]" & Environment.NewLine
                Str &= "           ,[PALLET]" & Environment.NewLine
                Str &= "           ,[NW]" & Environment.NewLine
                Str &= "           ,[GW]" & Environment.NewLine
                Str &= "           ,[M3]" & Environment.NewLine
                Str &= "           ,[DIMENSION]" & Environment.NewLine
                Str &= "           ,[ORDUNIQ]" & Environment.NewLine
                Str &= "           ,[QTYSHPTODT]" & Environment.NewLine
                Str &= "           ,[QTYPER_PALLET]" & Environment.NewLine
                Str &= "           ,[PONO]" & Environment.NewLine
                Str &= "           ,[TERM]" & Environment.NewLine
                Str &= "           ,[MARK]" & Environment.NewLine
                Str &= "           ,[LINENUM]" & Environment.NewLine
                Str &= "           ,[STA_0]" & Environment.NewLine
                Str &= "           ,[SEQ]" & Environment.NewLine
                Str &= "           ,[QTYBACKORD])" & Environment.NewLine
                Str &= "     VALUES" & Environment.NewLine
                Str &= "           (" & Environment.NewLine
                Str &= "            B.[ORDNUMBER]" & Environment.NewLine
                Str &= "           ,B.[ORDDATE]" & Environment.NewLine
                Str &= "           ,B.[CUSTOMER]" & Environment.NewLine
                Str &= "          ,B.[BILNAME]" & Environment.NewLine
                Str &= "           ,B.[DESC]" & Environment.NewLine
                Str &= "           ,B.[EXPDATE]" & Environment.NewLine
                Str &= "           ,B.[From]" & Environment.NewLine
                Str &= "           ,B.[To]" & Environment.NewLine
                Str &= "           ,B.[ETD]" & Environment.NewLine
                Str &= "           ,B.[ETA]" & Environment.NewLine
                Str &= "           ,B.[FREIGHT]" & Environment.NewLine
                Str &= "           ,B.[FLIGHTVESSEL]" & Environment.NewLine
                Str &= "           ,B.[BL]" & Environment.NewLine
                Str &= "           ,B.[PORTCHARGE]" & Environment.NewLine
                Str &= "           ,B.[FINALDEST]" & Environment.NewLine
                Str &= "           ,B.[ITEM]" & Environment.NewLine
                Str &= "           ,B.[CITEMNO]" & Environment.NewLine
                Str &= "           ,B.[PARTNAMEPO]" & Environment.NewLine
                Str &= "           ,B.[CITEMDESC]" & Environment.NewLine
                Str &= "           ,B.[QTY]" & Environment.NewLine
                Str &= "           ,B.[ORDUNIT]" & Environment.NewLine
                Str &= "           ,B.[PALLET]" & Environment.NewLine
                Str &= "           ,B.[NW]" & Environment.NewLine
                Str &= "           ,B.[GW]" & Environment.NewLine
                Str &= "           ,B.[M3]" & Environment.NewLine
                Str &= "           ,B.[DIMENSION]" & Environment.NewLine
                Str &= "           ,B.[ORDUNIQ]" & Environment.NewLine
                Str &= "           ,B.[QTYSHPTODT]" & Environment.NewLine
                Str &= "           ,B.[QTYPER_PALLET]" & Environment.NewLine
                Str &= "           ,B.[PONO]" & Environment.NewLine
                Str &= "           ,B.[TERM]" & Environment.NewLine
                Str &= "           ,B.[MARK]" & Environment.NewLine
                Str &= "           ,B.[LINENUM]" & Environment.NewLine
                Str &= "           ,B.[STA_0]" & Environment.NewLine
                Str &= "           ,B.[SEQ]" & Environment.NewLine
                Str &= "           ,B.[QTYBACKORD]" & Environment.NewLine
                Str &= "            );"

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()

            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 470 MERGEFMSPACKING():" & ex.Message & Str)
        End Try
    End Sub

#End Region

#Region "UPDATE"

    Public Shared Sub UPDATEFMSPACKING(ByVal ORDNUMBER As String)
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then

                Str = "DELETE FROM FMSPACKING WHERE ORDNUMBER = '" & ORDNUMBER & "'" & Environment.NewLine
                Str &= "UPDATE FMSPACKINGEDIT SET STA_0  = 3  WHERE ORDNUMBER = '" & ORDNUMBER & "' "

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 490 UPDATEFMSPACKING():" & ex.Message & Str)
        End Try
    End Sub

#End Region

#Region "DELETE"
    Public Shared Sub DELETELINEFMSPACKING(ByVal ORDNUMBER As String, ByVal SEQ As String, ByVal ITEM As String, ByVal CUSTOMER As String)
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then

                Str = "UPDATE FMSPACKING SET STA_0 = 3 WHERE ORDNUMBER = '" & ORDNUMBER & "' AND SEQ = '" & SEQ & "' AND ITEM  = '" & ITEM & "' AND CUSTOMER = '" & CUSTOMER & "'  " & Environment.NewLine
                'Str &= "UPDATE FMSPACKINGEDIT SET STA_0  = 3  WHERE ORDNUMBER = '" & ORDNUMBER & "' "

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 490 UPDATEFMSPACKING():" & ex.Message & Str)
        End Try
    End Sub
#End Region

#Region "ALTER VIEW"

    Public Shared Sub ALTERVIEW_FMSPACKINGLIST()
        Dim Str As String
        Try
            Connection.Openconnect("Source", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBAPP").ToString.TrimEnd

                Str = "ALTER VIEW [dbo].[FMSPACKINGLIST] AS " & Environment.NewLine
                Str &= "SELECT     * " & Environment.NewLine
                Str &= "FROM    " & SCHEMA & ".dbo.FMSPACKING "

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()

            Else


            End If

            connect.Close()
        Catch ex As Exception

            WriteLog("Error 635 ALTERVIEW_FMSPACKINGLIST():" & ex.Message)

        End Try

    End Sub

#End Region

#End Region


#Region "CREATE TABLE "
    Public Shared Sub CREATEFMSPACKINGEDIT()
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then

                Str = "CREATE TABLE [dbo].[FMSPACKINGEDIT](
	                    [ORDNUMBER] [nvarchar](100) NULL,
	                    [ORDDATE] [nvarchar](20) NULL,
	                    [CUSTOMER] [nvarchar](100) NULL,
	                    [BILNAME] [nvarchar](500) NULL,
	                    [DESC] [text] NULL,
	                    [EXPDATE] [nvarchar](20) NULL,
	                    [From] [nvarchar](100) NULL,
	                    [To] [nvarchar](100) NULL,
	                    [ETD] [nvarchar](100) NULL,
	                    [ETA] [nvarchar](100) NULL,
	                    [FREIGHT] [nvarchar](100) NULL,
	                    [FLIGHTVESSEL] [nvarchar](100) NULL,
	                    [BL] [nvarchar](100) NULL,
	                    [PORTCHARGE] [nvarchar](100) NULL,
	                    [FINALDEST] [nvarchar](100) NULL,
	                    [ITEM] [nvarchar](100) NULL,
	                    [CITEMNO] [nvarchar](100) NULL,
	                    [PARTNAMEPO] [nvarchar](100) NULL,
	                    [CITEMDESC] [text] NULL,
	                    [QTY] [nvarchar](100) NULL,
	                    [ORDUNIT] [nvarchar](100) NULL,
	                    [PALLET] [nvarchar](100) NULL,
	                    [NW] [nvarchar](100) NULL,
	                    [GW] [nvarchar](100) NULL,
	                    [M3] [nvarchar](100) NULL,
	                    [DIMENSION] [nvarchar](100) NULL,
	                    [ORDUNIQ] [nvarchar](100) NULL,
	                    [QTYSHPTODT] [nvarchar](100) NULL,
	                    [QTYPER_PALLET] [nvarchar](100) NULL,
	                    [PONO] [nvarchar](1000) NULL,
	                    [TERM] [nvarchar](100) NULL,
	                    [MARK] [text] NULL,
	                    [LINENUM] [numeric](18, 0) NULL,
	                    [TIMESTAMP_0] [datetime] NULL,
	                    [STA_0] [nvarchar](20) NULL,
	                    [SEQ] [numeric](18, 0) NULL,
	                    [QTYBACKORD] [nvarchar](100) NULL,
	                    [TRANSID] [int] NULL
                    )  "
                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 860 CREATEFMSPACKINGEDIT():" & ex.Message)
        End Try
    End Sub

    Public Shared Sub CREATEFMSPACKING()
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then

                Str = "CREATE TABLE [dbo].[FMSPACKING](
	                    [ORDNUMBER] [nvarchar](100) NULL,
	                    [ORDDATE] [nvarchar](20) NULL,
	                    [CUSTOMER] [nvarchar](100) NULL,
	                    [BILNAME] [nvarchar](500) NULL,
	                    [DESC] [text] NULL,
	                    [EXPDATE] [nvarchar](20) NULL,
	                    [From] [nvarchar](100) NULL,
	                    [To] [nvarchar](100) NULL,
	                    [ETD] [nvarchar](100) NULL,
	                    [ETA] [nvarchar](100) NULL,
	                    [FREIGHT] [nvarchar](100) NULL,
	                    [FLIGHTVESSEL] [nvarchar](100) NULL,
	                    [BL] [nvarchar](100) NULL,
	                    [PORTCHARGE] [nvarchar](100) NULL,
	                    [FINALDEST] [nvarchar](100) NULL,
	                    [ITEM] [nvarchar](100) NULL,
	                    [CITEMNO] [nvarchar](100) NULL,
	                    [PARTNAMEPO] [nvarchar](100) NULL,
	                    [CITEMDESC] [text] NULL,
	                    [QTY] [nvarchar](100) NULL,
	                    [ORDUNIT] [nvarchar](100) NULL,
	                    [PALLET] [nvarchar](100) NULL,
	                    [NW] [nvarchar](100) NULL,
	                    [GW] [nvarchar](100) NULL,
	                    [M3] [nvarchar](100) NULL,
	                    [DIMENSION] [nvarchar](100) NULL,
	                    [ORDUNIQ] [nvarchar](100) NULL,
	                    [QTYSHPTODT] [nvarchar](100) NULL,
	                    [QTYPER_PALLET] [nvarchar](100) NULL,
	                    [PONO] [nvarchar](1000) NULL,
	                    [TERM] [nvarchar](100) NULL,
	                    [MARK] [text] NULL,
	                    [LINENUM] [numeric](18, 0) NULL,
	                    [STA_0] [nvarchar](20) NULL,
	                    [SEQ] [numeric](18, 0) NULL,
	                    [QTYBACKORD] [nvarchar](100) NULL
                    ) "
                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 919 CREATEFMSPACKING():" & ex.Message)
        End Try
    End Sub

    Public Shared Sub CREATEFMSICITEM_TEMP()
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then
                Str = "CREATE TABLE [dbo].[FMSICITEM_TEMP](
	                    [ITEMNO] [nvarchar](30) NULL,
	                    [ITEMDESC] [text] NULL,
	                    [IDCUST] [nvarchar](30) NULL,
	                    [STOCKUNIT] [nvarchar](30) NULL,
	                    [WIDTH] [decimal](18, 2) NULL,
	                    [LENGTH] [decimal](18, 2) NULL,
	                    [HEIGHT] [decimal](18, 2) NULL,
	                    [QTYPER_BOX] [decimal](18, 2) NULL,
	                    [QTYPER_PALLET] [decimal](18, 2) NULL,
	                    [NUM_LAYER] [decimal](18, 2) NULL,
	                    [HEIGHTPER_LEVEL] [decimal](18, 2) NULL,
	                    [PALLET_HEIGHT] [decimal](18, 2) NULL,
	                    [PALLET_WEIGHT] [decimal](18, 2) NULL,
	                    [QTYBOXPER_LEVEL] [decimal](18, 2) NULL,
	                    [NETWEIGHT] [decimal](18, 5) NULL,
	                    [GROSSWEIGHT] [decimal](18, 5) NULL,
	                    [BOXWEIGHT] [decimal](18, 2) NULL,
	                    [STA_0] [nvarchar](2) NULL,
	                    [USER] [nvarchar](30) NULL,
	                    [TIMESTAMP] [datetime] NULL
                    ) "
                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 955 CREATEFMSICITEM_TEMP():" & ex.Message)
        End Try
    End Sub

    Public Shared Sub CREATEFMSMASTERITEM()
        Dim Str As String = ""
        Try
            Connection.Openconnect("DB", connect)
            If dtConfigDB.Rows.Count > 0 Then

                Str = "CREATE TABLE [dbo].[FMSMASTERITEM](
	                    [ITEMNO] [nvarchar](30) NOT NULL,
	                    [ITEMDESC] [text] NULL,
	                    [IDCUST] [nvarchar](30) NOT NULL,
	                    [STOCKUNIT] [nvarchar](30) NULL,
	                    [WIDTH] [decimal](18, 2) NULL,
	                    [LENGTH] [decimal](18, 2) NULL,
	                    [HEIGHT] [decimal](18, 2) NULL,
	                    [QTYPER_BOX] [decimal](18, 2) NULL,
	                    [QTYPER_PALLET] [decimal](18, 2) NULL,
	                    [NUM_LAYER] [decimal](18, 2) NULL,
	                    [HEIGHTPER_LEVEL] [decimal](18, 2) NULL,
	                    [PALLET_HEIGHT] [decimal](18, 2) NULL,
	                    [PALLET_WEIGHT] [decimal](18, 2) NULL,
	                    [QTYBOXPER_LEVEL] [decimal](18, 2) NULL,
	                    [NETWEIGHT] [decimal](18, 5) NULL,
	                    [GROSSWEIGHT] [decimal](18, 5) NULL,
	                    [BOXWEIGHT] [decimal](18, 5) NULL,
	                    [STA_0] [nvarchar](2) NULL,
	                    [USER] [nvarchar](30) NULL,
	                    [TIMESTAMP] [datetime] NULL,
                     CONSTRAINT [ITEMNO] PRIMARY KEY CLUSTERED 
                    (
	                    [ITEMNO] ASC,
	                    [IDCUST] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    )"

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 1000 CREATEFMSICITEM_TEMP():" & ex.Message)
        End Try
    End Sub

#End Region

#Region "CREATE VIEW"

    Public Shared Sub CREATEVIEWFMSPACKINGLIST()
        Dim Str As String = ""
        Try
            Connection.Openconnect("Source", connect)

            If dtConfigDB.Rows.Count > 0 Then
                Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBAPP").ToString.TrimEnd
                Str = "CREATE VIEW [dbo].[FMSPACKINGLIST]
                        AS
                        SELECT     *
                        FROM        " & SCHEMA & ".dbo.FMSPACKING "

                Dim cmd As SqlCommand = New SqlCommand(Str, connect)
                cmd.ExecuteNonQuery()
            Else

            End If
            connect.Close()
        Catch ex As Exception
            WriteLog("Error 1030 CREATEVIEWFMSPACKINGLIST():" & ex.Message & Str)
        End Try
    End Sub

#End Region

End Class
