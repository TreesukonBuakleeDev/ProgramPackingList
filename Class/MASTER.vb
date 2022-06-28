Imports System.Data
Imports System.Data.SqlClient
Public Class MASTER

#Region "ItemMaster"

    Public Shared Function GETFMSMASTERITEM(Optional ByVal FILTERSTRING As String = "") As DataTable
        Connection.Openconnect("DB", connect)
        Dim sql1 As String
        sql1 = "SELECT * " & Environment.NewLine
        sql1 &= "FROM FMSMASTERITEM" & Environment.NewLine
        sql1 &= FILTERSTRING


        'sql1 &= "WHERE STA_0 <> 2" & Environment.NewLine 'NOT SHOW INACTIVE

        Command = New SqlCommand(sql1, connect)
        adapter = New SqlDataAdapter(Command)
        Dim dataSt = New DataSet()
        adapter.Fill(dataSt, "Data")
        connect.Close()
        Return dataSt.Tables("Data")
    End Function

#End Region

#Region "Main"
    Public Shared Function GETDATA(ByVal ORDNUMBER As String, ByRef boolEXIST As Boolean) As DataTable
        Connection.Openconnect("DB", connect)
        boolEXIST = False
        If dtConfigDB.Rows.Count > 0 Then
            Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
            Dim sql1 As String = ""

            'Check EXIST IN FMSPACKING 

            boolEXIST = CHECKEXIST_FMSPACKING(ORDNUMBER)

            If boolEXIST = True Then
                sql1 = sql1CaseEXIST(SCHEMA, ORDNUMBER)
            Else
                sql1 = sql1CaseNOEXIST(SCHEMA, ORDNUMBER)
            End If

            Command = New SqlCommand(sql1, connect)
            adapter = New SqlDataAdapter(Command)
            Dim dataSt = New DataSet()
            adapter.Fill(dataSt, "Data")
            connect.Close()
            Return dataSt.Tables("Data")
        Else
            Return Nothing
        End If


    End Function

    Public Shared Function sql1CaseNOEXIST(ByVal SCHEMA As String, ByVal ORDNUMBER As String) As String
        Dim sql1 As String = ""
        Try
            sql1 = "SELECT " & Environment.NewLine
            sql1 &= " '' AS [NO], " & Environment.NewLine
            sql1 &= "OEORDH.ORDNUMBER ,  " & Environment.NewLine
            sql1 &= "OEORDH.ORDDATE, " & Environment.NewLine
            sql1 &= "LTRIM(OEORDH.CUSTOMER) AS CUSTOMER, " & Environment.NewLine
            sql1 &= "LTRIM(OEORDH.BILNAME) AS BILNAME, " & Environment.NewLine
            sql1 &= "OEORDH.[DESC], " & Environment.NewLine
            sql1 &= "OEORDH.EXPDATE, " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'FROM'),'') AS [From], " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'TO'),'') AS [To], " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'ETD'),'') AS [ETD], " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'ETA'),'') AS [ETA], " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'FREIGHT'),'') AS FREIGHT, " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'FLIGHTVESSEL'),'') AS FLIGHTVESSEL, " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'BL'),'') AS BL, " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'PORTCHARGE'),'') AS PORTCHARGE, " & Environment.NewLine
            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.OEORDHO OEORDHO WHERE OEORDHO.ORDUNIQ = OEORDH.ORDUNIQ AND OEORDHO.OPTFIELD = 'FINALDEST'),'') AS FINALDEST, " & Environment.NewLine

            sql1 &= "LTRIM(OEORDD.ITEM) AS ITEM, " & Environment.NewLine
            sql1 &= "ICITMC.CITEMNO, " & Environment.NewLine

            sql1 &= "ISNULL((SELECT [VALUE] AS VALUE FROM " & SCHEMA & ".dbo.ICITEMO ICITEMO WHERE ICITEMO.ITEMNO = OEORDD.ITEM AND ICITEMO.OPTFIELD = 'PARTNAMEPO'),'')  AS PARTNAMEPO, " & Environment.NewLine
            sql1 &= "LTRIM(ICITMC.CITEMDESC) AS CITEMDESC , " & Environment.NewLine
            sql1 &= "'' AS QTY, " & Environment.NewLine
            sql1 &= "OEORDD.ORDUNIT, " & Environment.NewLine
            sql1 &= "'1' AS PALLET, " & Environment.NewLine
            sql1 &= "''  AS NW, " & Environment.NewLine
            sql1 &= "''  AS GW, " & Environment.NewLine
            sql1 &= " '' AS M3,  " & Environment.NewLine
            sql1 &= "''  AS DIMENSION, " & Environment.NewLine
            sql1 &= "OEORDD.ORDUNIQ, " & Environment.NewLine
            'sql1 &= "CASE WHEN OEORDD.QTYSHPTODT = 0  THEN OEORDD.QTYORDERED ELSE OEORDD.QTYSHPTODT END AS QTYSHPTODT," & Environment.NewLine
            sql1 &= "CASE WHEN OEORDD.QTYSHPTODT = 0  THEN OEORDD.QTYSHPTODT ELSE OEORDD.QTYSHPTODT END AS QTYSHPTODT," & Environment.NewLine
            sql1 &= "FMSMASTER.QTYPER_PALLET , " & Environment.NewLine
            sql1 &= "OEORDD.QTYBACKORD," & Environment.NewLine
            'sql1 &= "ISNULL(LTRIM(OEORDH.PONUMBER),LTRIM(OEORDH.COMMENT) ) As PONO,  " & Environment.NewLine
            sql1 &= "CASE WHEN LTRIM(OEORDH.PONUMBER) = '' THEN LTRIM(OEORDH.COMMENT) ELSE LTRIM(OEORDH.PONUMBER)  END AS PONO ," & Environment.NewLine
            sql1 &= "ISNULL((SELECT TEXTDESC FROM " & SCHEMA & ".dbo.ARRTA WHERE ARRTA.CODETERM = OEORDH.TERMS ),'') AS TERM," & Environment.NewLine
            sql1 &= " '' AS MARK, " & Environment.NewLine
            sql1 &= "OEORDD.LINENUM  " & Environment.NewLine

            sql1 &= "FROM " & SCHEMA & ".dbo.OEORDH OEORDH " & Environment.NewLine
            sql1 &= "INNER JOIN " & SCHEMA & ".dbo.OEORDD OEORDD ON OEORDH.ORDUNIQ = OEORDD.ORDUNIQ  " & Environment.NewLine
            sql1 &= "LEFT OUTER JOIN " & SCHEMA & ".dbo.ICITMC ON ICITMC.ITEMNO = OEORDD.ITEM AND ICITMC.CUSTNO = OEORDH.CUSTOMER " & Environment.NewLine
            sql1 &= "LEFT OUTER JOIN FMSMASTERITEM FMSMASTER ON FMSMASTER.ITEMNO = OEORDD.ITEM AND FMSMASTER.IDCUST = OEORDH.CUSTOMER " & Environment.NewLine

            sql1 &= "WHERE OEORDH.ORDNUMBER = '" & ORDNUMBER.TrimEnd & "'  " & Environment.NewLine
            sql1 &= "AND OEORDD.LINETYPE = 1  " & Environment.NewLine

        Catch ex As Exception
            WriteLog("Error 95 (sql1Condition):" & ORDNUMBER & ex.Message & sql1)
        End Try

        Return sql1
    End Function

    Public Shared Function sql1CaseEXIST(ByVal SCHEMA As String, ByVal ORDNUMBER As String) As String
        Dim sql1 As String = ""
        Try
            sql1 = "SELECT '' AS [NO], * " & Environment.NewLine
            sql1 &= "FROM FMSPACKING  " & Environment.NewLine
            sql1 &= "WHERE ORDNUMBER = '" & ORDNUMBER & "' " & Environment.NewLine
            sql1 &= "AND STA_0 <> 3"
        Catch ex As Exception
            WriteLog("Error 125 (sql1CaseEXIST):" & ORDNUMBER & ex.Message)
        End Try
        Return sql1
    End Function

    Public Shared Function CHECKEXIST_FMSPACKING(ByVal TXTORDNUMBER As String) As Boolean
        Dim STATUS As Boolean
        Connection.Openconnect("DB", connect)

        Dim ORDNUMBER As String = ""
        Dim str As String = "SELECT * FROM FMSPACKING WHERE ORDNUMBER = '" & TXTORDNUMBER & "' "

        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()

            If result("ORDNUMBER").ToString.TrimEnd <> "" Then
                STATUS = True
            Else
                STATUS = False
            End If
        End While
        connect.Close()
        Return STATUS

    End Function

    Public Shared Function GETORDNUMBER(ByVal CONDITION As String, ByVal TXTORDNUMBER As String) As String
        Connection.Openconnect("Source", connect)
        Dim str As String
        Dim ORDNUMBER As String = ""
        Select Case CONDITION
            Case ">"
                If TXTORDNUMBER.TrimEnd <> "" Then
                    str = "SELECT TOP 1 * FROM OEORDH WHERE ORDNUMBER > '" & TXTORDNUMBER & "'   ORDER BY ORDNUMBER ASC"
                Else
                    str = "SELECT TOP 1 * FROM OEORDH  ORDER BY ORDNUMBER DESC"
                End If

            Case "<"
                If TXTORDNUMBER.TrimEnd <> "" Then
                    str = "SELECT TOP 1 * FROM OEORDH WHERE ORDNUMBER < '" & TXTORDNUMBER & "'   ORDER BY ORDNUMBER DESC"
                Else
                    str = "SELECT TOP 1 * FROM OEORDH  ORDER BY ORDNUMBER ASC"
                End If

            Case ">>"
                str = "SELECT TOP 1 * FROM OEORDH  ORDER BY ORDNUMBER DESC"

            Case "<<"
                str = "SELECT TOP 1 * FROM OEORDH  ORDER BY ORDNUMBER ASC"

            Case Else
                str = "SELECT TOP 1 * FROM OEORDH ORDER BY ORDNUMBER DESC"

        End Select


        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()
            ORDNUMBER = result("ORDNUMBER").ToString.TrimEnd
        End While
        connect.Close()
        Return ORDNUMBER

    End Function

    Public Shared Function GETPATTERN(ByVal TXTIDCUST As String) As String
        Connection.Openconnect("Source", connect)
        Dim str As String
        Dim TEMP As String = ""

        str = "SELECT VALUE AS TEMP FROM ARCUSO  " & Environment.NewLine
        str &= "WHERE OPTFIELD = 'PATTERN' AND IDCUST = '" & TXTIDCUST.TrimEnd & "' "


        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()
            TEMP = result("TEMP").ToString.TrimEnd
        End While
        connect.Close()
        Return TEMP

    End Function

    Public Shared Function GETSEARCHORDER(Optional ByVal TXTFILTER As String = "") As DataTable
        Connection.Openconnect("DB", connect)

        If dtConfigDB.Rows.Count > 0 Then
            Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
            Dim sql1 As String = ""

            'Check EXIST IN FMSPACKING 

            sql1 = "SELECT DISTINCT" & Environment.NewLine

            sql1 &= "OEORDH.ORDNUMBER ,  " & Environment.NewLine
            sql1 &= "OEORDH.ORDDATE, " & Environment.NewLine
            sql1 &= "OEORDH.CUSTOMER, " & Environment.NewLine
            sql1 &= "OEORDH.BILNAME " & Environment.NewLine

            sql1 &= "FROM " & SCHEMA & ".dbo.OEORDH OEORDH " & Environment.NewLine
            sql1 &= "INNER JOIN " & SCHEMA & ".dbo.OEORDD OEORDD ON OEORDH.ORDUNIQ = OEORDD.ORDUNIQ  " & Environment.NewLine
            sql1 &= "WHERE OEORDD.LINETYPE = 1 " & Environment.NewLine


            Command = New SqlCommand(sql1, connect)
            adapter = New SqlDataAdapter(Command)
            Dim dataSt = New DataSet()
            adapter.Fill(dataSt, "Data")
            connect.Close()
            Return dataSt.Tables("Data")
        Else
            Return Nothing
        End If


    End Function

    Public Shared Function GETSEARCHPRINT(Optional ByVal TXTFILTER As String = "") As DataTable
        Connection.Openconnect("DB", connect)

        If dtConfigDB.Rows.Count > 0 Then
            Dim SCHEMA As String = dtConfigDB.Rows(0).Item("DBSource").ToString.TrimEnd
            Dim sql1 As String = ""

            sql1 = "SELECT DISTINCT ORDNUMBER,ORDDATE,CUSTOMER,BILNAME  FROM FMSPACKING"

            Command = New SqlCommand(sql1, connect)
            adapter = New SqlDataAdapter(Command)
            Dim dataSt = New DataSet()
            adapter.Fill(dataSt, "Data")
            connect.Close()
            Return dataSt.Tables("Data")
        Else
            Return Nothing
        End If


    End Function

    Public Shared Function GETMARK(ByVal ORDNUMBER As String) As String
        Connection.Openconnect("DB", connect)
        Dim str As String
        Dim TEMP As String = ""

        str = "SELECT TOP 1 CASE WHEN CONVERT(VARCHAR,FMSPACKINGEDIT.MARK) = '' THEN FMSPACKING.MARK ELSE FMSPACKINGEDIT.MARK END  AS TEMP  " & Environment.NewLine
        str &= "FROM FMSPACKING  " & Environment.NewLine
        str &= "LEFT OUTER JOIN FMSPACKINGEDIT ON FMSPACKING.ORDNUMBER = FMSPACKINGEDIT.ORDNUMBER " & Environment.NewLine
        str &= "AND FMSPACKINGEDIT.TRANSID = (SELECT MAX(TRANSID) FROM FMSPACKINGEDIT WHERE FMSPACKINGEDIT.ORDNUMBER = '" & ORDNUMBER & "') " & Environment.NewLine
        str &= "WHERE FMSPACKING.ORDNUMBER = '" & ORDNUMBER & "'" & Environment.NewLine
        str &= "AND FMSPACKING.STA_0 <> 3 "


        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()
            TEMP = result("TEMP").ToString.TrimEnd
        End While
        connect.Close()
        Return TEMP

    End Function

    Public Shared Function GETPONO(ByVal ORDNUMBER As String) As String
        Connection.Openconnect("DB", connect)
        Dim str As String
        Dim TEMP As String = ""

        str = "SELECT TOP 1 CASE WHEN CONVERT(VARCHAR,FMSPACKINGEDIT.PONO) = '' THEN FMSPACKING.PONO ELSE FMSPACKINGEDIT.PONO END  AS TEMP " & Environment.NewLine
        str &= "FROM FMSPACKING  " & Environment.NewLine
        str &= "LEFT OUTER JOIN FMSPACKINGEDIT ON FMSPACKING.ORDNUMBER = FMSPACKINGEDIT.ORDNUMBER " & Environment.NewLine
        str &= "AND FMSPACKINGEDIT.TRANSID = (SELECT MAX(TRANSID) FROM FMSPACKINGEDIT WHERE FMSPACKINGEDIT.ORDNUMBER = '" & ORDNUMBER & "') " & Environment.NewLine
        str &= "WHERE FMSPACKING.ORDNUMBER = '" & ORDNUMBER & "'" & Environment.NewLine
        str &= "AND FMSPACKING.STA_0 <> 3 "


        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()
            TEMP = result("TEMP").ToString.TrimEnd
        End While
        connect.Close()
        Return TEMP

    End Function

    Public Shared Function GETMAXFMSPACKING(ByVal ORDNUMBER As String, ByVal FIELD As String) As String
        Connection.Openconnect("DB", connect)
        Dim str As String
        Dim TEMP As String = ""

        str = "SELECT MAX(CAST(" & FIELD & " AS NUMERIC)) AS TEMP FROM FMSPACKING WHERE ORDNUMBER = '" & ORDNUMBER & "' "


        Dim cmd As SqlCommand = New SqlCommand(str, connect)
        Dim result As SqlDataReader = cmd.ExecuteReader()

        While result.Read()
            TEMP = result("TEMP").ToString.TrimEnd
        End While
        connect.Close()
        Return TEMP

    End Function



#End Region

End Class
