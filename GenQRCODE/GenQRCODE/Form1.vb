Imports System.Text
Imports System.IO
Imports ZXing
Imports System.Drawing.Imaging
Imports ZXing.QrCode
Public Class Form1
  
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 10000
        Timer1.Start()
        'Dim encoder As New ZXing.BarcodeWriter
        'encoder.Format = BarcodeFormat.QR_CODE
        'encoder.Options.Height = 50
        'encoder.Options.Width = 50
        'Dim BimMap = encoder.Write("123456789")
        'Dim g As Graphics = Graphics.FromImage(BimMap)
        'BimMap.Save("D:\111.png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim i As Integer = 0
        Dim stCode As String
        ' Dim directory = "D:\GenCode"
        Dim directory = "\\192.168.0.5\DataAX\QRCode"
        For Each filename As String In IO.Directory.GetFiles(directory, "*", IO.SearchOption.AllDirectories)
            Dim fname As String = IO.Path.GetExtension(filename)
            If fname = ".XML" Or fname = ".xml" Then
                Dim ds As DataSet
                ds = New DataSet
                ds.ReadXml(filename)
                ' MsgBox(ds.Tables(0).Rows(0)(0).ToString)
                Dim qrcode As New BarcodeFormat
                stCode = ds.Tables(0).Rows(0)(0).ToString
                Dim encoder As New ZXing.BarcodeWriter
                encoder.Format = BarcodeFormat.QR_CODE
                encoder.Options.Height = 100
                encoder.Options.Width = 100
                encoder.Options.Margin = 0
                Dim BimMap = encoder.Write(stCode)
                Dim g As Graphics = Graphics.FromImage(BimMap)
                BimMap.Save(directory & "\" & stCode & ".png")
                For Each ImageFile As String In IO.Directory.GetFiles(directory, "*.png", IO.SearchOption.AllDirectories)
                    Dim ImgName As String = IO.Path.GetFileName(ImageFile)
                    If ImgName = stCode & ".png" Then
                        System.IO.File.Delete(filename)
                    End If
                Next
            End If
        Next
    End Sub
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Application.DoEvents()
        System.Threading.Thread.Sleep(5000)
        Me.WindowState = FormWindowState.Minimized
        Me.Hide()
    End Sub
    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        Me.Show()
        Me.WindowState = FormWindowState.Normal
        Timer2.Interval = 10000
        Timer2.Enabled = True
        With NotifyIcon1
            .BalloonTipIcon = ToolTipIcon.Info
            .BalloonTipText = "Show Message"
            .BalloonTipTitle = "Program running"
            .ShowBalloonTip(100)
        End With
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Me.Hide()
        Me.WindowState = FormWindowState.Minimized
    End Sub
   
End Class
