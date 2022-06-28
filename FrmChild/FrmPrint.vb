
Imports System.Data.SqlClient
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data
Imports System.Windows.Forms
Public Class FrmPrint

    Structure Parameter
        Dim ParameterName As String
        Dim ParameterValue As String
    End Structure
    Structure ReportCondition
        Dim ConnectionString As String
        Dim ReportFile As String
        Dim Fomula As String
    End Structure
    Public ReportAttribute As ReportCondition
    Public ReportAttributeParameter() As Parameter
    Friend conStr As String
    Friend connection As SqlConnection
    Friend dataSt As DataSet
    Friend adapter As SqlDataAdapter

    Private Sub FrmPrint_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Call GenReport()

        Me.Close()

        'Call GENREPORTING()
    End Sub

    Public Sub GenReport()
        Call DataClass.ALTERVIEW_FMSPACKINGLIST()
        Dim strFormula As String = ""
        Dim clsRptViewer As New FrmPrint
        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        If dtConfigDB.Rows.Count = 0 Then
            'ReadConfig(dtConfigDB)
            dtConfigDB = READDB()
        Else
        End If

        'Dim strcon As String = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & "  ;Initial Catalog=" & dtConfigDB.Rows(0).Item("DBSource").ToString & " ;User ID=" & dtConfigDB.Rows(0).Item("UserName").ToString & " ;Password= " & dtConfigDB.Rows(0).Item("PassWord").ToString & ";Connect Timeout=0 "

        'conStr = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & ";Initial Catalog= " & dtConfigDB.Rows(0).Item("DatabaseName").ToString & ";User ID= " & dtConfigDB.Rows(0).Item("User").ToString & ";Password= " & dtConfigDB.Rows(0).Item("Pass").ToString & ";Connect Timeout=0"
        Dim strcon As String = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & "  ;Initial Catalog=" & dtConfigDB.Rows(0).Item("DBAPP").ToString & " ;User ID=" & dtConfigDB.Rows(0).Item("USER").ToString & " ;Password= " & dtConfigDB.Rows(0).Item("PASSWORD").ToString & ";Connect Timeout=0 "

        Dim connectionStringSource As String = "Data Source= " & dtConfigDB.Rows(0).Item("SERVER").ToString & ";Initial Catalog= " & dtConfigDB.Rows(0).Item("DBSource").ToString & ";User ID= " & dtConfigDB.Rows(0).Item("USER").ToString & ";Password= " & dtConfigDB.Rows(0).Item("PASSWORD").ToString & ";Connect Timeout=0"
        connection = New SqlConnection(connectionStringSource)

        Dim conStrREPORT As New SqlClient.SqlConnectionStringBuilder(connectionStringSource)
        rpt.Load(frmPrintPop.txtPrintBROWSEEX.Text)

        clsRptViewer.ReportAttribute.Fomula = strFormula

        ReDim clsRptViewer.ReportAttributeParameter(2)

        Dim SORTFROM As String = frmPrintPop.txtPrint_From.Text.TrimEnd
        Dim SORTTO As String = frmPrintPop.txtPrint_To.Text.TrimEnd

        If SORTTO.TrimEnd = "" Then
            SORTTO = "zzzzzzzz"
        End If

        clsRptViewer.ReportAttributeParameter(1).ParameterName = "SORTFROM"
        clsRptViewer.ReportAttributeParameter(1).ParameterValue = SORTFROM

        clsRptViewer.ReportAttributeParameter(2).ParameterName = "SORTTO"
        clsRptViewer.ReportAttributeParameter(2).ParameterValue = SORTTO

        Dim crTable As CrystalDecisions.CrystalReports.Engine.Table
        Dim crTableLogonInfo As CrystalDecisions.Shared.TableLogOnInfo
        Dim ConnInfo As New CrystalDecisions.Shared.ConnectionInfo

        ConnInfo.ServerName = conStrREPORT.DataSource
        ConnInfo.UserID = conStrREPORT.UserID
        ConnInfo.Password = conStrREPORT.Password
        ConnInfo.DatabaseName = conStrREPORT.InitialCatalog
        ConnInfo.IntegratedSecurity = False

        Try
            For Each crTable In rpt.Database.Tables
                crTableLogonInfo = crTable.LogOnInfo
                crTableLogonInfo.ConnectionInfo = ConnInfo
                crTable.ApplyLogOnInfo(crTableLogonInfo)
            Next
            If (clsRptViewer.ReportAttributeParameter IsNot Nothing) Then
                For Each obj As Parameter In clsRptViewer.ReportAttributeParameter
                    If (obj.ParameterValue <> String.Empty) OrElse (obj.ParameterName <> String.Empty) Then
                        rpt.SetParameterValue(obj.ParameterName, obj.ParameterValue)
                    End If
                Next
            End If

            ''rpt.RecordSelectionFormula = clsRptViewer.ReportAttribute.Fomula

            'Open Crystal report viewer' 
            Using objForm As New Windows.Forms.Form
                objForm.StartPosition = FormStartPosition.CenterScreen
                objForm.Text = "FMS Packing list " & frmPrintPop.txtPrintBROWSEEX.Text
                objForm.WindowState = FormWindowState.Maximized

                Using rptViewer As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                    rptViewer.DisplayGroupTree = True
                    rptViewer.ShowCloseButton = True
                    rptViewer.ShowGroupTreeButton = True
                    rptViewer.ShowTextSearchButton = True
                    rptViewer.ShowZoomButton = True

                    rptViewer.Dock = DockStyle.Fill

                    objForm.Controls.Add(rptViewer)

                    rptViewer.ReportSource = rpt

                    objForm.ShowDialog()

                End Using
            End Using

        Catch ex As Exception
            Call WriteLog("Error 252: " & ex.Message, "EXPORT")
        End Try

    End Sub

    Sub GENREPORTING()

        Dim rd As ReportDocument = New ReportDocument()
        rd.Load(frmPrintPop.txtPrintBROWSEEX.Text)

        Dim SORTFROM As String = frmPrintPop.txtPrint_From.Text.TrimEnd
        Dim SORTTO As String = frmPrintPop.txtPrint_To.Text.TrimEnd

        rd.SetParameterValue("SORTFROM", SORTFROM)
        rd.SetParameterValue("SORTTO", SORTTO)
        'CRV1.ReportSource = rd

        'Open Crystal report viewer' 
        Using objForm As New Windows.Forms.Form
            objForm.StartPosition = FormStartPosition.CenterScreen
            'objForm.Text = Utilities.Info.Title
            objForm.WindowState = FormWindowState.Maximized

            Using rptViewer As New CrystalDecisions.Windows.Forms.CrystalReportViewer

                'rptViewer.DisplayGroupTree = TRUE
                rptViewer.ShowCloseButton = True
                rptViewer.ShowGroupTreeButton = True
                rptViewer.ShowTextSearchButton = True
                rptViewer.ShowZoomButton = True

                rptViewer.Dock = DockStyle.Fill

                objForm.Controls.Add(rptViewer)

                rptViewer.ReportSource = rd

                objForm.ShowDialog()

            End Using
        End Using


    End Sub


End Class