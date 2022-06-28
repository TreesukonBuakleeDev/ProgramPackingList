Imports System.Data
Imports System.IO


Class MainWindow



    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        Try
            Call CreateDirectory()
            Call DataClass.CREATEFMSPACKING()
            Call DataClass.CREATEFMSPACKINGEDIT()
            Call DataClass.CREATEFMSMASTERITEM()
            Call DataClass.CREATEFMSICITEM_TEMP()
            Call DataClass.CREATEVIEWFMSPACKINGLIST()
        Catch ex As Exception
            WriteLog("Error 15 (MainWindow.Window_Loaded):" & ex.Message)
        End Try
    End Sub

#Region "BUTTON"

    Private Sub TreeViewItem_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        Try

            Call ShowFrmMaster()

        Catch ex As Exception
            WriteLog("Error 25 (ShowFrmBillingBatch):" & ex.Message)
        End Try
    End Sub


    Private Sub TreeViewItem_MouseDoubleClick_1(sender As Object, e As MouseButtonEventArgs)
        Try
            Call ShowFrmDbSetup()
        Catch ex As Exception
            WriteLog("Error 34 (ShowFrmBillingBatch):" & ex.Message)
        End Try
    End Sub

    Private Sub TreeViewItem_MouseDoubleClick_2(sender As Object, e As MouseButtonEventArgs)
        Try
            'PanelMainControl.Children.Clear()
            Call ShowFrmMain()
        Catch ex As Exception
            WriteLog("Error 42 (ShowFrmBillingBatch):" & ex.Message)
        End Try
    End Sub

    Private Sub Author_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs) Handles Author.MouseDoubleClick
        Try
            Call ShowFrmAuthor()
        Catch ex As Exception
            WriteLog("Error 55 (ShowFrmBillingBatch):" & ex.Message)
        End Try
    End Sub


#End Region

#Region "EVENT"


    Sub ShowFrmMain()
        Try
            frmMN = New FrmMain
            frmMN.Show()
            'PanelMainControl.Children.Add(frmMN)
            frmMN.Topmost = True

        Catch ex As Exception
            WriteLog("Error 60 (ShowFrmMain):" & ex.Message)
        End Try
    End Sub

    Sub showFrmLogIn()
        Try
            Dim frmSignIn As New FrmLOGIN
            frmSignIn.Show()
        Catch ex As Exception
            WriteLog("Error 80 (showFrmLogIn):" & ex.Message)
        End Try
    End Sub



    Sub CreateDirectory()
        Try
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Template") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\Template")
            End If
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel")
            End If
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel\ErrorFileExportExcel") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel\ErrorFileExportExcel")
            End If
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel\BackupFileExportExcel") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportExcel\BackupFileExportExcel")
            End If
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\Configure") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\Configure")
            End If
            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportXML") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportXML")
            End If

            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportXML\BACKUPXML") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileExportXML\BACKUPXML")
            End If

            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileImport") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileImport")
            End If

            If Not Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory & "\FileImport\RESULT") Then
                Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory & "\FileImport\RESULT")
            End If
        Catch ex As Exception
            Call WriteLog("Error 130: " & ex.Message)
        End Try
    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        Me.Close()
        Environment.Exit(0)
    End Sub






#End Region

End Class
