Imports System.IO
Imports System.Net
Imports CefSharp
Imports Google
Imports Google.Apis.Discovery
Imports Google.Apis.Services
Imports Google.Apis.YouTube.v3
Imports Newtonsoft.Json.Linq
Imports System
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports Windows.Media
Imports Windows.Media.Core
Imports Windows.Media.Playback
Imports Windows.Storage
Imports Windows.Storage.Streams
Imports System.Runtime.InteropServices.WindowsRuntime
Imports System.IO.WindowsRuntimeStreamExtensions

Public Class Form1
    Private WithEvents browser As WinForms.ChromiumWebBrowser
    Dim isPlayerReady As Boolean = False
    Dim songChanged As Boolean = True
    Dim videoID As String = "null"
    Dim currentTime, duration, title, state As String
    Dim volume As Integer
    Dim prevVolume As Integer
    Dim hotkey As New HotKeyRegistryClass(Me.Handle)
    Dim mPlayer As New MediaPlayer
    Dim mControls As SystemMediaTransportControls = mPlayer.SystemMediaTransportControls
    Dim dispUpdater As SystemMediaTransportControlsDisplayUpdater = mControls.DisplayUpdater
    Dim timeProperties As SystemMediaTransportControlsTimelineProperties = New SystemMediaTransportControlsTimelineProperties()

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

        mPlayer.CommandManager.IsEnabled = False
        mControls.IsChannelDownEnabled = False
        mControls.IsChannelUpEnabled = False
        mControls.IsFastForwardEnabled = False
        mControls.IsNextEnabled = True
        mControls.IsPauseEnabled = True
        mControls.IsPlayEnabled = True
        mControls.IsPreviousEnabled = True
        mControls.IsRecordEnabled = False
        mControls.IsRewindEnabled = False
        mControls.IsStopEnabled = False

        AddHandler mControls.ButtonPressed, AddressOf ControlsPressed

        DefaultCard()

    End Sub
    Private Sub DefaultCard()
        dispUpdater.Type = MediaPlaybackType.Music
        dispUpdater.MusicProperties.Title = "Youtube Music Player"
        dispUpdater.MusicProperties.AlbumArtist = "Rudra Sharma"
        dispUpdater.Thumbnail = RandomAccessStreamReference.CreateFromUri(New Uri("https://rudrasharma.net/extraStuff/No_Image.png"))
        mControls.PlaybackStatus = MediaPlaybackStatus.Stopped
        dispUpdater.Update()
        mControls.IsEnabled = True
    End Sub
    Private Sub ControlsPressed(sender As SystemMediaTransportControls, args As SystemMediaTransportControlsButtonPressedEventArgs)
        Select Case args.Button
            Case args.Button.Play
                If Not state = "Playing" Then
                    PausePlay()
                End If
            Case args.Button.Pause
                If Not state = "Paused" Then
                    PausePlay()
                End If
            Case args.Button.Next
                NextTrack()
            Case args.Button.Previous
                PrevTrack()
        End Select
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
            DefaultCard()
            mControls.PlaybackStatus = MediaPlaybackStatus.Stopped
            Return "Unstarted"
        ElseIf st = 0 Then
            mControls.PlaybackStatus = MediaPlaybackStatus.Closed
            DefaultCard()
            Return "Ended"
        ElseIf st = 1 Then
            mControls.IsEnabled = True
            mControls.PlaybackStatus = MediaPlaybackStatus.Playing
            Return "Playing"
        ElseIf st = 2 Then
            mControls.IsEnabled = True
            mControls.PlaybackStatus = MediaPlaybackStatus.Paused
            Return "Paused"
        ElseIf st = 3 Then
            mControls.PlaybackStatus = MediaPlaybackStatus.Changing
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

    Private Sub tmrUpdate_Tick(sender As Object, e As EventArgs) Handles tmrUpdateDisp.Tick
        If isPlayerReady And songChanged Then
            dispUpdater.Type = MediaPlaybackType.Music
            dispUpdater.MusicProperties.Title = title
            dispUpdater.MusicProperties.AlbumArtist = "Youtube"
            dispUpdater.Thumbnail = RandomAccessStreamReference.CreateFromUri(New Uri("https://img.youtube.com/vi/" + videoID + "/hqdefault.jpg"))
            dispUpdater.Update()
        End If
    End Sub

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
        hotkey.Unregister(1)
        hotkey.Unregister(2)
    End Sub
End Class
