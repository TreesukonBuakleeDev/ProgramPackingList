Imports System.Windows.Forms
Public Class FrmBrowseEx


    Private Sub BTNMASTER_SEARCHEX_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNMASTER_SEARCHEX.MouseDown
        Try
            'Dim dialog =
            Dim dialog = New System.Windows.Forms.FolderBrowserDialog()
            Dim result As System.Windows.Forms.DialogResult = dialog.ShowDialog()


            txtMasterBROWSEEX.Text = dialog.SelectedPath


        Catch ex As Exception
            WriteLog("Error 17 BTNMASTER_SEARCHEX_MouseDown() : " & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_EX_Click(sender As Object, e As RoutedEventArgs) Handles BTNMASTER_EX.Click
        Try
            EXPath = txtMasterBROWSEEX.Text

            Dim frmMASTER_EX As New FrmMasterItem

            Dim STA_0 As Boolean = frmMASTER_EX.GENMASTER_EXPORT()


            If STA_0 = True Then

                MessageBox.Show(New Form With {.TopMost = True}, "EXPORT COMPLETE", "EXPORT COMPLETE", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else

                MessageBox.Show(New Form With {.TopMost = True}, "EXPORT FAILED", "EXPORT FAILED", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            WriteLog("Error 29 BTNMASTER_EX_Click() : " & ex.Message)
        End Try

    End Sub

    Private Sub BTNMASTER_SEARCHEX_From_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNMASTER_SEARCHEX_From.MouseDown
        Try
            VFrom = True
            VTo = False
            Call SearchEXPORTORDNUM()
        Catch ex As Exception
            WriteLog("Error 37 BTNMASTER_SEARCHEX_From_MouseDown() : " & ex.Message)
        End Try
    End Sub

    Private Sub BTNMASTER_SEARCHEX_To_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles BTNMASTER_SEARCHEX_To.MouseDown
        Try
            VFrom = False
            VTo = True
            Call SearchEXPORTORDNUM()
        Catch ex As Exception
            WriteLog("Error 47 BTNMASTER_SEARCHEX_To_MouseDown() : " & ex.Message)
        End Try
    End Sub

    Sub SearchEXPORTORDNUM()
        Try
            Dim frmSearchPrint As New FrmSearchBrowseEX
            frmSearchPrint.Show()
        Catch ex As Exception
            WriteLog("Error 57 SearchEXPORTORDNUM() : " & ex.Message)
        End Try
    End Sub
End Class
