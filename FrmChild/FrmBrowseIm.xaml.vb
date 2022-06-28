Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class FrmBrowseIm

#Region "BUTTON"
    Private Sub BTNMASTER_SEARCHIM_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNMASTER_SEARCHIM.MouseDown
        Try
            Dim dialog = New Microsoft.Win32.OpenFileDialog()

            dialog.DefaultExt = ".xls"
            dialog.Filter = "Excel File (.xls)|*.xls"
            Dim result As Boolean? = dialog.ShowDialog()

            If result = True Then
                Dim filename As String = dialog.FileName
                txtMasterBROWSEIM.Text = filename
            End If

        Catch ex As Exception
            WriteLog("Error 17 BTNMASTER_SEARCHIM_MouseDown() :" & ex.Message)
        End Try
    End Sub


    Private Sub BTNMASTER_IM_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_IM.Click
        Try
            Try

                Dim dt As DataTable = New DataTable()
                Dim pathImport As String = txtMasterBROWSEIM.Text
                IMPath = Path.GetDirectoryName(txtMasterBROWSEIM.Text)

                Call READEXCEL(dt, pathImport)
                Call DataClass.INSERTFMSICITEM_TEMP(dt, "IMPORT")



                '2.MERGE FMSMASTERITEM <--> FMSICITEM_TEMP 
                Call DataClass.MERGEITEMMASTER()


                '3.GET DATA FMSMASTERITEM
                Dim DTGETITEM As DataTable = MASTER.GETFMSMASTERITEM()

                '4.DISPLAY DATA 
                frmMT.DGV_MASTER.ItemsSource = DTGETITEM.DefaultView

                frmMT.DGV_MASTER.CanUserAddRows = False


                frmMT.DGV_MASTER.Items.Refresh()
                frmMT.Topmost = False
                DisplayFrmMaster()


                'Call PROCESS.ShowFrmMaster("REFRESH")

            Catch ex As Exception
                WriteLog("Error 100 BTNMASTER_IM_MouseDoubleClick() :" & ex.Message)
                MessageBox.Show("Error 40 : Import Failed. " & ex.Message)
            End Try

        Catch ex As Exception
            WriteLog("Error 17 BTNMASTER_IM_MouseDoubleClick() :" & ex.Message)
        End Try
    End Sub


#End Region

#Region "EVENT"
    Sub READEXCEL(ByRef dtRead As DataTable, ByVal path As String)
        Dim conn As OleDbConnection
        Dim dta As OleDbDataAdapter = New OleDbDataAdapter
        Dim dtar As OleDbDataAdapter = New OleDbDataAdapter

        dtRead.Rows.Clear()
        dtRead.Columns.Clear()
        Call FrmMasterItem.KillAllExcelProcess()


        Try
            If path <> "" Then
                conn = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";Extended Properties=Excel 8.0;")
                dtar = New OleDbDataAdapter("Select * From [MASTER$]", conn)
            End If

            dtRead = New DataTable
            dtar.Fill(dtRead)



        Catch ex As Exception
            MessageBox.Show("Error 270 : Cannot read data from source file" & ex.Message)
        End Try

    End Sub




#End Region

End Class
