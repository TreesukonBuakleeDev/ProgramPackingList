Imports System.Data
Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO


Module Connection
    Public connect As SqlConnection
    Public dtConfigDB As DataTable = New DataTable()
    Public dtMapAcct As DataTable = New DataTable()
    Friend Command As SqlCommand
    Friend adapter As SqlDataAdapter


#Region "Parameter"
    Public CompanyName As String
    Public SageVersion As String
    Public DBSource As String
    Public UserName As String
    Public PassWord As String
    Public ServerName As String
    Public DB As String
    Public User As String
    Public Pass As String
    Public ImportPath As String
    Public ExportPath As String
    Public TimeSpan As String
    Public TaskActive As String
    Public EXPath As String
    Public IMPath As String

    Public VFrom As Boolean
    Public VTo As Boolean

#End Region
    'Public frmDB As New FrmDbSetup
#Region "Connection"
    Sub SaveConfigDB()
        Try
            Dim frmDB As New FrmDbSetup
            Dim FILE_text1 As String = My.Application.Info.DirectoryPath & "\Configure\Config.ini"

            Dim aryText(11) As String
            Dim i As Integer

            'aryText(0) = "CompanyName:" + EncryptDecrypt_Class.Encrypt(frmDB.Acc_Company.Text, "FMS1")
            'aryText(1) = "SageVersion:" + EncryptDecrypt_Class.Encrypt(frmDB.Acc_version.Text, "FMS1")
            'aryText(2) = "UserName:" + EncryptDecrypt_Class.Encrypt(frmDB.Acc_UserID.Text, "FMS1")
            'aryText(3) = "PassWord:" + EncryptDecrypt_Class.Encrypt(frmDB.Acc_Password.Text, "FMS1")

            'aryText(4) = "ServerName:" + EncryptDecrypt_Class.Encrypt(frmDB.txtServer.Text, "FMS1")
            'aryText(5) = "DB:" + EncryptDecrypt_Class.Encrypt(frmDB.txtDB.Text, "FMS1")
            'aryText(6) = "User:" + EncryptDecrypt_Class.Encrypt(frmDB.txtUser.Text, "FMS1")
            'aryText(7) = "Pass:" + EncryptDecrypt_Class.Encrypt(frmDB.txtPassword.Text, "FMS1")
            'aryText(8) = "ImportPath=" + frmDB.txtImportPath.Text
            'aryText(9) = "ExportPath=" + frmDB.txtExportPath.Text
            aryText(0) = "CompanyName:" + EncryptDecrypt_Class.Encrypt(CompanyName, "FMS1")
            aryText(1) = "SageVersion:" + EncryptDecrypt_Class.Encrypt(SageVersion, "FMS1")
            aryText(2) = "UserName:" + EncryptDecrypt_Class.Encrypt(UserName, "FMS1")
            aryText(3) = "PassWord:" + EncryptDecrypt_Class.Encrypt(PassWord, "FMS1")

            aryText(4) = "ServerName:" + EncryptDecrypt_Class.Encrypt(ServerName, "FMS1")
            aryText(5) = "DB:" + EncryptDecrypt_Class.Encrypt(DB, "FMS1")
            aryText(6) = "User:" + EncryptDecrypt_Class.Encrypt(User, "FMS1")
            aryText(7) = "Pass:" + EncryptDecrypt_Class.Encrypt(Pass, "FMS1")
            aryText(8) = "ImportPath=" + ImportPath
            aryText(9) = "ExportPath=" + ExportPath
            aryText(10) = "TimeSpan=5" '+ "frmDB.txtTimeSpan.Text"
            aryText(11) = "TaskActive=1" '+ IIf(frmDB.BTN_ActiveTask.Checked = True, "1", "0")

            Dim objWriter As New System.IO.StreamWriter(FILE_text1)
            For i = 0 To 11
                objWriter.WriteLine(aryText(i))
            Next
            objWriter.Close()
            Console.Read()
        Catch ex As Exception
            WriteLog("Error 78  SaveConfigDB() :" & ex.Message)
        End Try
    End Sub
    Sub ReadConfig(ByRef dtConfigDB As DataTable)
        Dim frmDB As New FrmDbSetup
        Try
            Dim filename As String = My.Application.Info.DirectoryPath & "\Configure\Config.ini"
            Dim fileReader As System.IO.StreamReader
            fileReader = My.Computer.FileSystem.OpenTextFileReader(filename)

            '>> Read text push to datatable

            Dim stringReader1 As String
            stringReader1 = fileReader.ReadLine()
            If stringReader1.Contains("CompanyName") = False Then
                Exit Sub
            End If
            Dim Sp1 As String()
            Sp1 = stringReader1.Split(":")
            Dim CompanyName As String = ""
            For i = 1 To Sp1.Length - 1
                CompanyName = CompanyName + Sp1(i).ToString()
            Next
            CompanyName = EncryptDecrypt_Class.Decrypt(CompanyName, "FMS1")

            Dim stringReader2 As String
            stringReader2 = fileReader.ReadLine()
            Dim Sp2 As String()
            Sp2 = stringReader2.Split(":")
            Dim SageVersion As String = ""
            For i = 1 To Sp2.Length - 1
                SageVersion = SageVersion + Sp2(i).ToString()
            Next
            SageVersion = EncryptDecrypt_Class.Decrypt(SageVersion, "FMS1")

            Dim stringReader3 As String
            stringReader3 = fileReader.ReadLine()
            Dim Sp3 As String()
            Sp3 = stringReader3.Split(":")
            Dim UserName As String = ""
            For i = 1 To Sp3.Length - 1
                UserName = UserName + Sp3(i).ToString()
            Next
            UserName = EncryptDecrypt_Class.Decrypt(UserName, "FMS1")

            Dim stringReader4 As String
            stringReader4 = fileReader.ReadLine()
            Dim Sp4 As String()
            Sp4 = stringReader4.Split(":")
            Dim PassWord As String = ""
            For i = 1 To Sp4.Length - 1
                PassWord = PassWord + Sp4(i).ToString()
            Next
            PassWord = EncryptDecrypt_Class.Decrypt(PassWord, "FMS1")

            Dim stringReaderVat1 As String
            stringReaderVat1 = fileReader.ReadLine()

            Dim SpVat1 As String()
            SpVat1 = stringReaderVat1.Split(":")
            Dim ServerName As String = ""
            For i = 1 To SpVat1.Length - 1
                ServerName = ServerName + SpVat1(i).ToString()
            Next
            ServerName = EncryptDecrypt_Class.Decrypt(ServerName, "FMS1")

            Dim stringReaderVat2 As String
            stringReaderVat2 = fileReader.ReadLine()
            Dim SpVat2 As String()
            SpVat2 = stringReaderVat2.Split(":")
            Dim DB As String = ""
            For i = 1 To SpVat2.Length - 1
                DB = DB + SpVat2(i).ToString()
            Next
            DB = EncryptDecrypt_Class.Decrypt(DB, "FMS1")

            Dim stringReaderVat3 As String
            stringReaderVat3 = fileReader.ReadLine()
            Dim SpVat3 As String()
            SpVat3 = stringReaderVat3.Split(":")
            Dim User As String = ""
            For i = 1 To SpVat3.Length - 1
                User = User + SpVat3(i).ToString()
            Next
            User = EncryptDecrypt_Class.Decrypt(User, "FMS1")

            Dim stringReaderVat4 As String
            stringReaderVat4 = fileReader.ReadLine()
            Dim SpVat4 As String()
            SpVat4 = stringReaderVat4.Split(":")
            Dim Pass As String = ""
            For i = 1 To SpVat4.Length - 1
                Pass = Pass + SpVat4(i).ToString()
            Next
            Pass = EncryptDecrypt_Class.Decrypt(Pass, "FMS1")

            Dim stringReaderpath1 As String
            stringReaderpath1 = fileReader.ReadLine()
            Dim SpPath1 As String()
            SpPath1 = stringReaderpath1.Split("=")
            Dim ImportPath As String = ""
            For i = 1 To SpPath1.Length - 1
                ImportPath = ImportPath + SpPath1(i).ToString()
            Next
            ImportPath = ImportPath

            Dim stringReaderpath2 As String
            stringReaderpath2 = fileReader.ReadLine()
            Dim SpPath2 As String()
            SpPath2 = stringReaderpath2.Split("=")
            Dim ExportPath As String = ""
            For i = 1 To SpPath2.Length - 1
                ExportPath = ExportPath + SpPath2(i).ToString()
            Next
            ExportPath = ExportPath

            Dim stringReaderTimeSpan As String
            stringReaderTimeSpan = fileReader.ReadLine()
            Dim SpTimeSpan As String()
            SpTimeSpan = stringReaderTimeSpan.Split("=")
            Dim TimeSpan As String = ""
            For i = 1 To SpTimeSpan.Length - 1
                TimeSpan = TimeSpan + SpTimeSpan(i).ToString()
            Next
            TimeSpan = TimeSpan

            Dim stringReaderActive As String
            stringReaderActive = fileReader.ReadLine()
            Dim SpActive As String()
            SpActive = stringReaderActive.Split("=")
            Dim Active As String = ""
            For i = 1 To SpActive.Length - 1
                Active = Active + SpActive(i).ToString()
            Next
            Active = Active

            frmDB.Acc_Company.Text = CompanyName
            frmDB.Acc_version.Text = SageVersion
            frmDB.Acc_UserID.Text = UserName
            'frmDB.Acc_Password.Text = PassWord
            frmDB.txtServer.Text = ServerName
            frmDB.txtDB.Text = DB
            frmDB.txtUser.Text = User
            'frmDB.txtPassword.Text = Pass
            frmDB.txtImportPath.Text = ImportPath
            frmDB.txtExportPath.Text = ExportPath
            'frmDB.txtTimeSpan.Text = TimeSpan

            'If Active = "1" Then
            '    frmDB.BTN_ActiveTask.Checked = True
            'Else
            '    frmDB.BTN_InactiveTask.Checked = True
            'End If

            dtConfigDB.Rows.Clear()
            dtConfigDB.Columns.Clear()

            dtConfigDB.Columns.Add("CompanyName")
            dtConfigDB.Columns.Add("SageVersion")
            dtConfigDB.Columns.Add("UserName")
            dtConfigDB.Columns.Add("PassWord")

            dtConfigDB.Columns.Add("ServerName")
            dtConfigDB.Columns.Add("DatabaseName")
            dtConfigDB.Columns.Add("User")
            dtConfigDB.Columns.Add("Pass")

            dtConfigDB.Columns.Add("ImportPath")
            dtConfigDB.Columns.Add("ExportPath")
            dtConfigDB.Columns.Add("TimeSpan")
            dtConfigDB.Columns.Add("TaskActive")

            Dim row As String() = New String() {CompanyName, SageVersion, UserName, PassWord, ServerName, DB, User, Pass, ImportPath, ExportPath, TimeSpan, Active}
            dtConfigDB.Rows.Add(row)

            fileReader.Close()
            fileReader.Dispose()
        Catch ex As Exception
            WriteLog("Error 250 (ReadConfig):" & ex.Message)
        End Try
    End Sub
    Sub Openconnect(ByVal DB As String, ByRef connection As SqlConnection)
        Try
            If dtConfigDB.Rows.Count = 0 Then
                'ReadConfig(dtConfigDB)
                dtConfigDB = READDB()
            Else
            End If
            If dtConfigDB.Rows.Count <> 0 Then
                Dim strcon As String = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & "  ;Initial Catalog=" & dtConfigDB.Rows(0).Item("DBAPP").ToString & " ;User ID=" & dtConfigDB.Rows(0).Item("USER").ToString & " ;Password= " & dtConfigDB.Rows(0).Item("PASSWORD").ToString & ";Connect Timeout=0 "

                Dim connectionStringSource As String = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & ";Initial Catalog= " & dtConfigDB.Rows(0).Item("DBSource").ToString & ";User ID= " & dtConfigDB.Rows(0).Item("USER").ToString & ";Password= " & dtConfigDB.Rows(0).Item("PASSWORD").ToString & ";Connect Timeout=0"
                If DB = "Source" Then
                    connection = New SqlConnection(connectionStringSource)
                Else
                    connection = New SqlConnection(strcon)
                End If
                If connection.State = ConnectionState.Closed Then

                    connection.Open()
                Else
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            WriteLog("Error 285 (Openconnect):" & ex.Message)
        End Try
    End Sub

    Sub SAVEDB(ByVal dtConfigDB As DataTable)
        Try

            Dim ID As Integer
            Dim SERVER As String = frmDb.Acc_Company.Text.TrimEnd
            Dim USER As String = frmDb.Acc_UserID.Text.TrimEnd
            Dim PASSWORD As String = frmDb.Acc_Password.Password.TrimEnd
            Dim DBSource As String = frmDb.Acc_version.Text.TrimEnd
            Dim DBBILL As String = frmDb.txtDB.Text.TrimEnd
            Dim ACTIVEAUTHEN As Integer
            If frmDb.BTNAUTHEN_YES.IsChecked = True Then
                ACTIVEAUTHEN = 1
            Else
                ACTIVEAUTHEN = 0
            End If

            Dim COMPNAME As String = frmDb.Acc_CompNAME.Text.TrimEnd

            If dtConfigDB.Columns.Count = 0 Or dtConfigDB Is Nothing = True Then
                dtConfigDB.Columns.Add("ID", GetType(Integer))
                dtConfigDB.Columns.Add("SERVER")
                dtConfigDB.Columns.Add("USER")
                dtConfigDB.Columns.Add("PASSWORD")
                dtConfigDB.Columns.Add("DBSource")
                dtConfigDB.Columns.Add("DBAPP")
                dtConfigDB.Columns.Add("AUTHOR")
                dtConfigDB.Columns.Add("COMPNAME")
            End If

            If frmDb.txtDBID.Text = "***NEW***" Then

                Dim ROWID As DataRow()
                    If dtConfigDB.Rows.Count = 0 Then
                        ID = "001"
                    Else
                        ROWID = dtConfigDB.[Select]("ID = MAX(ID)")
                        If ROWID.Count > 0 Then
                            ID = CInt((ROWID(0).Item("ID").ToString)) + 1
                        End If
                    End If
                Dim row As String() = New String() {ID, SERVER, USER, PASSWORD, DBSource, DBBILL, ACTIVEAUTHEN, COMPNAME}
                dtConfigDB.Rows.Add(row)
                    frmDb.txtDBID.Text = ID


            Else
                'CASE EDIT 
                For i = 0 To dtConfigDB.Rows.Count - 1
                    If dtConfigDB.Rows(i).Item("ID").ToString.TrimEnd = frmDb.txtDBID.Text.TrimEnd Then
                        dtConfigDB.Rows(i).Delete()
                        dtConfigDB.AcceptChanges()
                        ID = frmDb.txtDBID.Text
                        Dim row As String() = New String() {ID, SERVER, USER, PASSWORD, DBSource, DBBILL, ACTIVEAUTHEN, COMPNAME}
                        dtConfigDB.Rows.Add(row)
                        Exit For
                    End If
                Next

            End If

            dtConfigDB.DefaultView.Sort = "ID ASC"
            dtConfigDB = dtConfigDB.DefaultView.ToTable
            Dim BOM As XElement = New XElement("BOM")
            Dim BO As XElement = New XElement("BO")

            If dtConfigDB.Rows.Count <> 0 Then

                Dim DocumentsLine As XElement = New XElement("Document_Lines")
                For j = 0 To dtConfigDB.Rows.Count - 1
                    Dim Lrow As XElement = New XElement("row")

                    Dim vID As String = dtConfigDB.Rows(j).Item("ID").ToString.TrimEnd
                    Dim vServer As String = dtConfigDB.Rows(j).Item("SERVER").ToString.TrimEnd
                    Dim vUSER As String = dtConfigDB.Rows(j).Item("USER").ToString.TrimEnd
                    Dim vPASSWORD As String = dtConfigDB.Rows(j).Item("PASSWORD").ToString.TrimEnd
                    Dim vDBSource As String = dtConfigDB.Rows(j).Item("DBSource").ToString.TrimEnd
                    Dim vDBBILL As String = dtConfigDB.Rows(j).Item("DBAPP").ToString.TrimEnd
                    Dim vAuthor As String = dtConfigDB.Rows(j).Item("AUTHOR").ToString.TrimEnd
                    Dim vCOMPNAME As String = dtConfigDB.Rows(j).Item("COMPNAME").ToString.TrimEnd

                    Dim xmlID As XElement = New XElement("ID", vID)
                    Dim xmlSERVER As XElement = New XElement("SERVER", vServer)
                    Dim xmlUSER As XElement = New XElement("USER", vUSER)
                    Dim xmlPASSWORD As XElement = New XElement("PASSWORD", vPASSWORD)
                    Dim xmlDBSource As XElement = New XElement("DBSource", vDBSource)
                    Dim xmlDBBILL As XElement = New XElement("DBAPP", vDBBILL)
                    Dim xmlAuthor As XElement = New XElement("AUTHOR", vAuthor)
                    Dim xmlCOMPNAME As XElement = New XElement("COMPNAME", vCOMPNAME)

                    Lrow.Add(xmlID)
                    Lrow.Add(xmlSERVER)
                    Lrow.Add(xmlUSER)
                    Lrow.Add(xmlPASSWORD)
                    Lrow.Add(xmlDBSource)
                    Lrow.Add(xmlDBBILL)
                    Lrow.Add(xmlAuthor)
                    Lrow.Add(xmlCOMPNAME)

                    DocumentsLine.Add(Lrow)
                Next
                BO.Add(DocumentsLine)

                BOM.Add(BO)
                'Generate xml file
                Dim reader = BOM.CreateReader()
                reader.ReadInnerXml()
                reader.MoveToContent()

                Dim settingPath As XmlWriterSettings = New XmlWriterSettings()
                settingPath.Indent = True

                Dim pathaddr As String

                pathaddr = My.Application.Info.DirectoryPath & "\Configure\DBCONFIG.xml"
                Dim path As System.IO.StreamWriter = New StreamWriter(pathaddr)

                Using writer As New System.Xml.XmlTextWriter(path)
                    writer.WriteStartDocument()
                    writer.WriteRaw(reader.ReadOuterXml)
                End Using
            Else

            End If
            'MessageBox.Show("Save Complete")
        Catch ex As Exception
            Call WriteLog("ERROR 410 (SAVEDB) : " & ex.Message, "EXPORT")
        End Try
    End Sub

    Public Function READDB(Optional ByVal Comp As String = Nothing) As DataTable

        Dim DT As DataTable = New DataTable
        Dim DTT As DataTable = New DataTable
        If DT.Columns.Count = 0 Or DT Is Nothing = True Then
            DT.Columns.Add("ID", GetType(Integer))
            DT.Columns.Add("SERVER")
            DT.Columns.Add("USER")
            DT.Columns.Add("PASSWORD")
            DT.Columns.Add("DBSource")
            DT.Columns.Add("DBAPP")
            DT.Columns.Add("AUTHOR")
            DT.Columns.Add("COMPNAME")
        End If
        Try


            Dim xmlDoc As New XmlDocument 'For loading xml file to read

            Dim ImportFilename As String = My.Application.Info.DirectoryPath & "\Configure\DBCONFIG.xml"
            xmlDoc.Load(ImportFilename) 'loading the xml file, insert your file here
            Dim RND As Integer = xmlDoc.Schemas.Count


            'COUNT  

            Dim ArticleNodeList As XmlNodeList 'For getting the list of main/parent nodes
            ArticleNodeList = xmlDoc.GetElementsByTagName("row") 'Setting all <People> node to list
            For Each articlenode As XmlNode In ArticleNodeList 'Looping through <People> node           
                DT.Rows.Add()
            Next
            DT.Rows.Add()

            For J = 0 To DT.Rows.Count - 1
                ArticleNodeList = xmlDoc.GetElementsByTagName("row") 'Setting all <People> node to list
                RND = 0
                For Each articlenode As XmlNode In ArticleNodeList 'Looping through <People> node
                    RND = RND + 1
                    For Each basenode As XmlNode In articlenode 'Looping all <People> childnodes

                        Dim result As String = ""
                        result = basenode.Name 'use 
                        Select Case result
                            Case "ID"
                                If J = RND Then
                                    DT.Rows(J).Item("ID") = basenode.InnerText
                                End If
                            Case "SERVER"
                                If J = RND Then
                                    DT.Rows(J).Item("SERVER") = basenode.InnerText
                                End If
                            Case "USER"
                                If J = RND Then
                                    DT.Rows(J).Item("USER") = basenode.InnerText
                                End If
                            Case "PASSWORD"
                                If J = RND Then
                                    DT.Rows(J).Item("PASSWORD") = basenode.InnerText
                                End If

                            Case "DBSource"
                                If J = RND Then
                                    DT.Rows(J).Item("DBSource") = basenode.InnerText
                                End If
                            Case "DBAPP"
                                If J = RND Then
                                    DT.Rows(J).Item("DBAPP") = basenode.InnerText
                                End If

                            Case "AUTHOR"
                                If J = RND Then
                                    DT.Rows(J).Item("AUTHOR") = basenode.InnerText
                                End If

                            Case "COMPNAME"
                                If J = RND Then
                                    DT.Rows(J).Item("COMPNAME") = basenode.InnerText
                                End If
                        End Select
                    Next
                Next
            Next

            DT.Rows(0).Delete()

            If DT.Rows.Count <> 0 Then
                If Comp <> Nothing Then
                    For k = 0 To DT.Rows.Count - 1
                        Dim DBSource As String = DT.Rows(k).Item("DBSource").ToString.TrimEnd
                        If DBSource <> Comp Then
                        Else
                            DTT = DT.Clone
                            Dim row As String() = New String() {DT.Rows(k).Item(0).ToString.TrimEnd, DT.Rows(k).Item(1).ToString.TrimEnd, DT.Rows(k).Item(2).ToString.TrimEnd, DT.Rows(k).Item(3).ToString.TrimEnd, DT.Rows(k).Item(4).ToString.TrimEnd, DT.Rows(k).Item(5).ToString.TrimEnd, DT.Rows(k).Item(6).ToString.TrimEnd, DT.Rows(k).Item(7).ToString.TrimEnd}
                            DTT.Rows.Add(row)
                        End If
                    Next
                End If

            End If
        Catch ex As Exception
            WriteLog("Error 515 (READDB) : " & ex.Message)
            frmDb.Show()
            frmDb.txtDBID.Text = "***NEW***"
            'frmDb.BTNDB_NEW_Click(Nothing, Nothing)
        End Try
        If DTT.Rows.Count = 0 Then
            Return DT
        Else
            Return DTT
        End If

    End Function

#End Region

#Region "AUTHOR"
    Public Function READAUTHOR() As DataTable

        Dim DT As DataTable = New DataTable

        'If DT.Columns.Count = 0 Or DT Is Nothing = True Then
        DT.Columns.Add("ID", GetType(Integer))
        DT.Columns.Add("USER")
        DT.Columns.Add("PASSWORD")
        DT.Columns.Add("AUTHOR")

        'End If

        Try

            Dim xmlDoc As New XmlDocument 'For loading xml file to read

            Dim ImportFilename As String = My.Application.Info.DirectoryPath & "\Configure\APPAUTHORIZED.xml"
            xmlDoc.Load(ImportFilename) 'loading the xml file, insert your file here
            Dim RND As Integer = xmlDoc.Schemas.Count


            'COUNT  

            Dim ArticleNodeList As XmlNodeList 'For getting the list of FrmDb/parent nodes
            ArticleNodeList = xmlDoc.GetElementsByTagName("row") 'Setting all <People> node to list
            For Each articlenode As XmlNode In ArticleNodeList 'Looping through <People> node           
                DT.Rows.Add()
            Next
            DT.Rows.Add()

            For J = 0 To DT.Rows.Count - 1
                ArticleNodeList = xmlDoc.GetElementsByTagName("row") 'Setting all <People> node to list
                RND = 0
                For Each articlenode As XmlNode In ArticleNodeList 'Looping through <People> node
                    RND = RND + 1
                    For Each basenode As XmlNode In articlenode 'Looping all <People> childnodes

                        Dim result As String = ""
                        result = basenode.Name 'use 
                        Select Case result
                            Case "ID"
                                If J = RND Then
                                    DT.Rows(J).Item("ID") = basenode.InnerText
                                End If
                            Case "USER"
                                If J = RND Then
                                    DT.Rows(J).Item("USER") = basenode.InnerText
                                End If
                            Case "PASSWORD"
                                If J = RND Then
                                    DT.Rows(J).Item("PASSWORD") = basenode.InnerText
                                End If
                            Case Else
                                If J = RND Then
                                    DT.Rows(J).Item("AUTHOR") = basenode.InnerText
                                End If
                        End Select
                    Next
                Next
            Next

            DT.Rows(0).Delete()

        Catch ex As Exception
            WriteLog("Error 595 (READAUTHOR) : " & ex.Message)
            frmAUTH.Show()
            frmAUTH.txtAuthorUserID.Text = "***NEW***"
            frmAUTH.txtConfirmPass.Visibility = Visibility.Visible
            'frmDb.BTNDB_NEW_Click(Nothing, Nothing)
        End Try

        Return DT
    End Function

    Sub SAVEAUTHOR(ByVal DTAPP As DataTable, ByVal txtAuthorUserID As String, ByVal txtAuthorUser As String, ByVal txtAuthorPassword As String, ByVal txtConfirmPass As String, ByVal txtAuthorized As String, ByRef USERID_NEW As String)
        Try
            Dim ID As Integer
            Dim USER As String = txtAuthorUser
            Dim PASSWORD As String = txtAuthorPassword
            Dim AUTHOR As String = txtAuthorized

            If txtAuthorUserID = "***NEW***" Then
                If txtAuthorPassword.TrimEnd = txtConfirmPass.TrimEnd Then
                    Dim ROWID As DataRow()
                    If DTAPP.Rows.Count = 0 Then
                        ID = "001"
                    Else
                        ROWID = DTAPP.[Select]("ID = MAX(ID)")
                        If ROWID.Count > 0 Then
                            ID = CInt((ROWID(0).Item("ID").ToString)) + 1
                        End If
                    End If
                    Dim row As String() = New String() {ID, USER, PASSWORD, AUTHOR}
                    DTAPP.Rows.Add(row)
                    USERID_NEW = ID
                Else
                    MessageBox.Show("Warning! Please check and try again. Mismatch Password and Confirm Password. ")
                    Exit Try
                End If

            Else
                'CASE EDIT 
                For i = 0 To DTAPP.Rows.Count - 1
                    If DTAPP.Rows(i).Item("ID").ToString.TrimEnd = txtAuthorUserID.TrimEnd Then
                        DTAPP.Rows(i).Delete()
                        DTAPP.AcceptChanges()
                        ID = txtAuthorUserID
                        Dim row As String() = New String() {ID, USER, PASSWORD, AUTHOR}
                        DTAPP.Rows.Add(row)
                        Exit For
                    End If
                Next

            End If

            DTAPP.DefaultView.Sort = "ID ASC"
            DTAPP = DTAPP.DefaultView.ToTable
            Dim BOM As XElement = New XElement("BOM")
            Dim BO As XElement = New XElement("BO")

            If DTAPP.Rows.Count <> 0 Then

                Dim DocumentsLine As XElement = New XElement("Document_Lines")
                For j = 0 To DTAPP.Rows.Count - 1
                    Dim Lrow As XElement = New XElement("row")
                    Dim vID As String = DTAPP.Rows(j).Item("ID").ToString
                    Dim vUSER As String = DTAPP.Rows(j).Item("USER").ToString
                    Dim vPASSWORD As String = DTAPP.Rows(j).Item("PASSWORD").ToString
                    Dim vAuthor As String = DTAPP.Rows(j).Item("AUTHOR").ToString

                    Dim xmlID As XElement = New XElement("ID", vID)
                    Dim xmlUSER As XElement = New XElement("USER", vUSER)
                    Dim xmlPASSWORD As XElement = New XElement("PASSWORD", vPASSWORD)
                    Dim xmlAuthor As XElement = New XElement("AUTHOR", vAuthor)

                    Lrow.Add(xmlID)
                    Lrow.Add(xmlUSER)
                    Lrow.Add(xmlPASSWORD)
                    Lrow.Add(xmlAuthor)

                    DocumentsLine.Add(Lrow)
                Next
                BO.Add(DocumentsLine)

                BOM.Add(BO)
                'Generate xml file
                Dim reader = BOM.CreateReader()
                reader.ReadInnerXml()
                reader.MoveToContent()

                Dim settingPath As XmlWriterSettings = New XmlWriterSettings()
                settingPath.Indent = True

                Dim pathaddr As String

                pathaddr = My.Application.Info.DirectoryPath & "\Configure\APPAUTHORIZED.xml"
                Dim path As System.IO.StreamWriter = New StreamWriter(pathaddr)

                Using writer As New System.Xml.XmlTextWriter(path)
                    writer.WriteStartDocument()
                    writer.WriteRaw(reader.ReadOuterXml)
                End Using
            Else

            End If

        Catch ex As Exception

            Call WriteLog("ERROR 700 (SAVEAUTHOR) : " & ex.Message)
            MessageBox.Show("Error (700) SAVE AUTHORIZATION ERROR")
        End Try
    End Sub

#End Region

#Region "Setup"
    Public Sub WriteLog(ByVal strlog As String, Optional ByVal EXPORT As String = "")
        Try
            'Generate log file
            Dim FILE_LOG As String = ""
            Select Case EXPORT
                Case ""
                    FILE_LOG = My.Application.Info.DirectoryPath & "\LOG.ini"

            End Select

            Dim lineCount = File.ReadAllLines(FILE_LOG).Length
            If lineCount > 5000 Then ' stored log 10000 lines
                lineCount = 1
                System.IO.File.WriteAllText(FILE_LOG, "Log")
            End If

            'Read old log 
            Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(FILE_LOG)
            Dim oldLog As String = ""

            For cntLine = 0 To lineCount - 1
                oldLog &= reader.ReadLine & vbCrLf
            Next
            reader.Close()

            'Write log
            Dim objWriter As New System.IO.StreamWriter(FILE_LOG)
            objWriter.WriteLine(Now & " " & strlog)
            objWriter.WriteLine(oldLog)

            objWriter.Close()
            'MAIN.txtLogImport.Text = Now & " " & MAIN.txtLogImport.Text & vbCrLf & strlog

        Catch ex As Exception

        End Try
    End Sub

#End Region



End Module
