

Public Class FrmPrintPopup

#Region "BUTTON"

    Private Sub BTNMAIN_PRINT_Click(sender As Object, e As RoutedEventArgs) Handles BTNMAIN_PRINT.Click

        Dim frmPrinting As New FrmPrint
        frmPrinting.Show()

    End Sub

    Private Sub BTNMASTER_SEARCHEX_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNMASTER_SEARCHEX.MouseDown
        Try
            Dim dialog = New Microsoft.Win32.OpenFileDialog()

            dialog.DefaultExt = ".rpt"
            dialog.Filter = "Crystal Report (.rpt)|*.rpt"
            Dim result As Boolean? = dialog.ShowDialog()

            If result = True Then
                Dim filename As String = dialog.FileName
                txtPrintBROWSEEX.Text = filename
            End If

        Catch ex As Exception
            WriteLog("Error 27 BTNMASTER_SEARCHEX_MouseDown() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNPRINT_SEARCHFROM_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNPRINT_SEARCHFROM.MouseDown
        Try
            VFrom = True
            VTo = False
            Call SearchPrintORDNUM()
        Catch ex As Exception
            WriteLog("Error 39 BTNPRINT_SEARCHFROM_MouseDown() :" & ex.Message)
        End Try
    End Sub

    Private Sub BTNPRINT_SEARCHTO_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNPRINT_SEARCHTO.MouseDown
        Try
            VFrom = False
            VTo = True
            Call SearchPrintORDNUM()
        Catch ex As Exception
            WriteLog("Error 49 BTNPRINT_SEARCHTO_MouseDown() :" & ex.Message)
        End Try
    End Sub

    Sub SearchPrintORDNUM()
        Dim frmSearchPrint As New FrmSearchPrintPop
        frmSearchPrint.Show()

    End Sub



#End Region
End Class
