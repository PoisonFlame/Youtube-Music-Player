Imports System.IO
Imports System.Net
Imports CefSharp
Imports Google
Imports Google.Apis.Discovery
Imports Google.Apis.Services
Imports Google.Apis.YouTube.v3
Imports Newtonsoft.Json.Linq
Public Class Form1
    Private WithEvents browser As WinForms.ChromiumWebBrowser
    Dim isPlayerReady As Boolean = False
    Dim songChanged As Boolean = True
    Dim videoID As String = "null"
    Dim currentTime, duration, title, state As String
    Dim volume As Integer
    Dim prevVolume As Integer
    Dim hotkey As New HotKeyRegistryClass(Me.Handle)
    Public Sub New()
        InitializeComponent()

        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)

        browser = New WinForms.ChromiumWebBrowser("https://rudrasharma.net/extraStuff/yt.php")
        'browser = New WinForms.ChromiumWebBrowser("https://rudrasharma.net/extraStuff/welcome.php")
        pnlBrowser.Controls.Add(browser)
        browser.Enabled = False
        AddHandler browser.LoadingStateChanged, AddressOf OnLoadingStateChanged
        hotkey.Register(HotKeyRegistryClass.Modifiers.MOD_NONE, Keys.MediaPlayPause)
        hotkey.Register(HotKeyRegistryClass.Modifiers.MOD_NONE, Keys.MediaNextTrack)
        hotkey.Register(HotKeyRegistryClass.Modifiers.MOD_NONE, Keys.MediaPreviousTrack)
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = HotKeyRegistryClass.Messages.WM_HOTKEY Then
            Dim ID As String = m.WParam.ToString()
            Select Case ID
                Case 0
                    PausePlay()
                Case 1
                    NextTrack()
                Case 2
                    PrevTrack()
                Case Else

            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Function removeOverlay() As Boolean
        pnlOverlay.Visible = False
        Return True
    End Function

    Private Sub OnLoadingStateChanged(sender As Object, args As LoadingStateChangedEventArgs)
        If args.IsLoading = False Then
            browser.EvaluateScriptAsync("isPlayerReady();").ContinueWith(Function(x)
                                                                             Dim response = x.Result

                                                                             If response.Success AndAlso response.Result IsNot Nothing Then
                                                                                 If response.Result = "true" Then
                                                                                     isPlayerReady = True
                                                                                     browser.ExecuteScriptAsync("changeDimensions(" + pnlBrowser.Width.ToString + "," + pnlBrowser.Height.ToString + ")")
                                                                                 End If
                                                                             End If
                                                                         End Function)

        End If
    End Sub
    Sub PausePlay()
        If state = "Playing" Then
            browser.ExecuteScriptAsync("pauseVideo();")
        ElseIf state = "Paused" Then
            browser.ExecuteScriptAsync("playVideo();")
        End If
    End Sub
    Sub NextTrack()
        browser.ExecuteScriptAsync("nextVideo();")
    End Sub
    Sub PrevTrack()
        browser.ExecuteScriptAsync("prevVideo();")
    End Sub
    Private Sub picPausePlay_Click(sender As Object, e As EventArgs) Handles picPausePlay.Click
        PausePlay()
    End Sub

    Private Sub picNext_Click(sender As Object, e As EventArgs) Handles picNext.Click
        NextTrack()
    End Sub

    Private Sub picPrev_Click(sender As Object, e As EventArgs) Handles picPrev.Click
        PrevTrack()
    End Sub
    Private Sub picVolume_Click(sender As Object, e As EventArgs) Handles picVolume.Click
        If volume = 0 Then
            trkVolume.Value = prevVolume
        Else
            prevVolume = trkVolume.Value
            trkVolume.Value = 0
        End If
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        'browser.ExecuteScriptAsync("loadPlaylistID('" + TextBox1.Text + "')")
    End Sub

    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        If isPlayerReady = True Then
            browser.ExecuteScriptAsync("changeDimensions(" + pnlBrowser.Width.ToString + "," + pnlBrowser.Height.ToString + ")")
        End If
    End Sub
    Function getState(state As String) As String
        Dim st As Integer = CInt(state)
        If st = -1 Then
            Return "Unstarted"
        ElseIf st = 0 Then
            Return "Ended"
        ElseIf st = 1 Then
            Return "Playing"
        ElseIf st = 2 Then
            Return "Paused"
        ElseIf st = 3 Then
            Return "Buffering"
        ElseIf st = 5 Then
            Return "Video Cued"
        End If
    End Function
    Private Sub tmrSettings_Tick(sender As Object, e As EventArgs) Handles tmrSettings.Tick
        If isPlayerReady = True Then
            removeOverlay()

            If songChanged Then
                browser.EvaluateScriptAsync("getVideoData();").ContinueWith(Function(x)
                                                                                Dim response = x.Result

                                                                                If response.Success AndAlso response.Result IsNot Nothing Then
                                                                                    videoID = response.Result(0)
                                                                                    currentTime = response.Result(1)
                                                                                    duration = response.Result(2)
                                                                                    volume = response.Result(3)
                                                                                    title = response.Result(4)
                                                                                    state = getState(response.Result(5))
                                                                                End If
                                                                            End Function)
            End If

        End If
        If Not videoID = "null" And songChanged Then
            Dim clt As WebClient = New WebClient
            Dim img As Bitmap = Bitmap.FromStream(New MemoryStream(clt.DownloadData("https://img.youtube.com/vi/" + videoID + "/hqdefault.jpg")))
            picThumbnail.Image = img
            lblVideoTitle.Text = title
            lblDuration.Text = convertSecondsToTime(duration)
            trkDuration.Maximum = CInt(duration)
            lblCurrentTime.Text = convertSecondsToTime(currentTime)
            trkDuration.Value = Double.Parse(currentTime)
        End If

        If state = "Playing" Then
            picPausePlay.Image = My.Resources.pause
            Me.Text = title
        Else
            picPausePlay.Image = My.Resources.play
            Me.Text = "Youtube Music Player"
        End If

        If volume = 0 Then
            picVolume.Image = My.Resources.mute
        Else
            picVolume.Image = My.Resources.volume
        End If


        trkVolume.Value = CInt(volume)

    End Sub

    Private Sub pnlBrowser_Click(sender As Object, e As EventArgs) Handles pnlBrowser.Click
        If state = "Playing" Then
            browser.ExecuteScriptAsync("pauseVideo();")
        ElseIf state = "Paused" Then
            browser.ExecuteScriptAsync("playVideo();")
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Function convertSecondsToTime(seconds As String) As String
        Dim sec As Double = Double.Parse(seconds)
        sec = Math.Round(sec)
        Dim min As Double = Math.Floor(sec / 60)
        sec = sec - min * 60
        If sec < 10 Then

            Return CStr(min) + ":" + "0" + CStr(sec)
        Else
            Return CStr(min) + ":" + CStr(sec)
        End If
    End Function

    Private Sub trkVolume_ValueChanged(sender As Object, e As EventArgs) Handles trkVolume.ValueChanged
        If isPlayerReady = True Then
            browser.ExecuteScriptAsync("setVolume(" + trkVolume.Value.ToString + ");")
        End If
    End Sub
    Private Sub trkDuration_Scroll(sender As Object, e As EventArgs) Handles trkDuration.Scroll
        If isPlayerReady = True Then
            browser.ExecuteScriptAsync("seekTo(" + trkDuration.Value.ToString + ")")
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        'RemoveGlobalHotkeySupport()
        hotkey.Unregister(0)
    End Sub
End Class
