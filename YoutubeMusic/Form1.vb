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
Imports Google.Apis.Auth.OAuth2
Imports System.Threading
Imports Google.Apis.YouTube

Public Class Form1
    Private WithEvents browser As WinForms.ChromiumWebBrowser
    Dim isPlayerReady As Boolean = False
    Dim songPlayed As Boolean = False
    Dim videoID As String = "null"
    Dim currentTime, duration, title, state As String
    Dim volume As Integer
    Dim prevVolume As Integer
    Dim hotkey As New HotKeyRegistryClass(Me.Handle)
    Dim mPlayer As New MediaPlayer
    Dim mControls As SystemMediaTransportControls = mPlayer.SystemMediaTransportControls
    Dim dispUpdater As SystemMediaTransportControlsDisplayUpdater = mControls.DisplayUpdater
    Dim timeProperties As SystemMediaTransportControlsTimelineProperties = New SystemMediaTransportControlsTimelineProperties()
    Dim hiddenPath As String = Path.GetFullPath(Path.Combine(My.Computer.FileSystem.CurrentDirectory.ToString, "..\..\..\")) + "HiddenContent\"
    Dim apiKey As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "key_api.txt")
    Dim oauthClientID As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "oauth_client.txt")
    Dim clientSecret As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "client_secret.txt")
    Dim credentials As UserCredential
    Dim youtubeService As YouTubeService
    Dim playListIDs As ArrayList = New ArrayList
    Dim currentURL As String
    Dim currentlySelectedPlaylist As String = "Youtube"
    Dim userSettingDropDown As MyComboBox

    Public Sub New()
        InitializeComponent()
        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)

        browser = New WinForms.ChromiumWebBrowser("https://rudrasharma.net/extraStuff/welcome.php")
        pnlBrowser.Controls.Add(browser)
        browser.Enabled = False
        AddHandler browser.LoadingStateChanged, AddressOf OnLoadingStateChanged
        AddHandler browser.AddressChanged, AddressOf SiteURLChanged
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

        userSettingDropDown = New MyComboBox(Me.Location.X + Me.Width)
        userSettingDropDown.Bounds = New Rectangle(10, 10, 17, 10)
        userSettingDropDown.DropDownWidth = 150
        userSettingDropDown.Height = 39
        userSettingDropDown.Width = 18
        userSettingDropDown.Dock = DockStyle.Right
        userSettingDropDown.Font = New Drawing.Font("Calibri", 20, FontStyle.Italic)
        userSettingDropDown.BackColor = Color.FromArgb(36, 36, 36)
        userSettingDropDown.ForeColor = Color.White
        pnlUserSettings.Controls.Add(userSettingDropDown)
        AddHandler userSettingDropDown.SelectedIndexChanged, AddressOf userSettingDropDown_SelectedIndexChanged

        DefaultCard()
    End Sub
    Private Async Sub userSettingDropDown_SelectedIndexChanged()
        Dim ind As Integer = userSettingDropDown.SelectedIndex
        Dim item As String = userSettingDropDown.Items.Item(ind).ToString

        If item = "Login" Then
            ' Login Crap
            Dim cltSecrets As New ClientSecrets
            cltSecrets.ClientId = oauthClientID
            cltSecrets.ClientSecret = clientSecret
            credentials = Await GoogleWebAuthorizationBroker.AuthorizeAsync(cltSecrets, {YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeReadonly}, "user", CancellationToken.None)
            My.Settings.accessToken = credentials.Token.AccessToken.ToString
            My.Settings.Save()


            Dim bcs = New BaseClientService.Initializer()
            bcs.HttpClientInitializer = credentials
            bcs.ApplicationName = "Youtube Music Player"
            youtubeService = New YouTubeService(bcs)
            Dim channelInfoService = youtubeService.Channels.List("snippet")
            channelInfoService.Mine = True
            Dim channelListResponse = Await channelInfoService.ExecuteAsync()
            lblGoogleLogin.Text = "Welcome, " + channelListResponse.Items(0).Snippet.Title

            Dim playlistInfoService = youtubeService.Playlists.List("id,snippet")
            playlistInfoService.Mine = True
            playlistInfoService.MaxResults = 50
            Dim playlistResponse = Await playlistInfoService.ExecuteAsync()
            Dim strd As String = ""
            playListIDs.Clear()
            lstPlaylists.Items.Clear()
            lstPlaylists.Items.Add("Home")
            lstPlaylists.Items.Add("Browse")
            lstPlaylists.Items.Add("YOUR LIBRARY")

            For i As Integer = 0 To playlistResponse.Items.Count - 1
                lstPlaylists.Items.Add(playlistResponse.Items(i).Snippet.Title)
                playListIDs.Add(playlistResponse.Items(i).Id)
            Next
            userSettingDropDown.Items.Clear()
            userSettingDropDown.Items.Add("Logout")
        ElseIf item = "Logout" Then
            'Logout Crap
            Await credentials.RevokeTokenAsync(CancellationToken.None)
            My.Settings.accessToken = "null"
            My.Settings.Save()
            lblGoogleLogin.Text = "Not Logged In"
            userSettingDropDown.Items.Clear()
            userSettingDropDown.Items.Add("Login")
            playListIDs.Clear()
            lstPlaylists.Items.Clear()
            lstPlaylists.Items.Add("No Info Available")
            lstPlaylists.Items.Add("Login to See Playlists")
        Else

        End If
    End Sub
    Private Sub AddUserSettings()
        If lblGoogleLogin.Text = "Not Logged In" Then
            userSettingDropDown.Items.Add("Login")
            lstPlaylists.Items.Add("No Info Available")
            lstPlaylists.Items.Add("Login to See Playlists")
        Else
            userSettingDropDown.Items.Add("Logout")
        End If
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
    Private Sub SiteURLChanged(sender As Object, args As AddressChangedEventArgs)
        currentURL = args.Address
    End Sub
    Private Sub ControlsPressed(sender As SystemMediaTransportControls, args As SystemMediaTransportControlsButtonPressedEventArgs)
        Select Case args.Button
            Case 0 'args.Button.Play
                If Not state = "Playing" Then
                    PausePlay()
                End If
            Case 1 'args.Button.Pause
                If Not state = "Paused" Then
                    PausePlay()
                End If
            Case 6 'args.Button.Next
                NextTrack()
            Case 7 'args.Button.Previous
                PrevTrack()
        End Select
    End Sub
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = HotKeyRegistryClass.Messages.WM_HOTKEY Then
            Dim ID As String = m.WParam.ToString()
            Select Case ID
                Case "0"
                    PausePlay()
                Case "1"
                    NextTrack()

                Case "2"
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

            If songPlayed Then
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
        If Not videoID = "null" And songPlayed Then
            Dim clt As WebClient = New WebClient
            Dim img As Bitmap = CType(Bitmap.FromStream(New MemoryStream(clt.DownloadData("https://img.youtube.com/vi/" + videoID + "/hqdefault.jpg")), Bitmap)
            picThumbnail.Image = img
            lblVideoTitle.Text = title
            lblDuration.Text = convertSecondsToTime(duration)
            trkDuration.Maximum = CInt(duration)
            lblCurrentTime.Text = convertSecondsToTime(currentTime)
            trkDuration.Value = CInt(Double.Parse(currentTime))
            tmrUpdateDisp.Start()

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
        userSettingDropDown.UpdateWidth(Me.Location.X + Me.Width)
    End Sub

    Private Sub pnlBrowser_Click(sender As Object, e As EventArgs) Handles pnlBrowser.Click
        If state = "Playing" Then
            browser.ExecuteScriptAsync("pauseVideo();")
        ElseIf state = "Paused" Then
            browser.ExecuteScriptAsync("playVideo();")
        End If
    End Sub

    Private Async Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not My.Settings.accessToken = "null" Then
            Dim cltSecrets As New ClientSecrets
            cltSecrets.ClientId = oauthClientID
            cltSecrets.ClientSecret = clientSecret
            cmbUserSettings.Items.Add("Logout")
            credentials = Await GoogleWebAuthorizationBroker.AuthorizeAsync(cltSecrets, {YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeReadonly}, "user", CancellationToken.None)



            Dim bcs = New BaseClientService.Initializer()
            bcs.HttpClientInitializer = credentials
            bcs.ApplicationName = "Youtube Music Player"
            youtubeService = New YouTubeService(bcs)
            Dim channelInfoService = youtubeService.Channels.List("snippet")
            channelInfoService.Mine = True
            Dim channelListResponse = Await channelInfoService.ExecuteAsync()

            lblGoogleLogin.Text = "Welcome, " + channelListResponse.Items(0).Snippet.Title

            Dim playlistInfoService = youtubeService.Playlists.List("id,snippet")
            playlistInfoService.Mine = True
            playlistInfoService.MaxResults = 50
            Dim playlistResponse = Await playlistInfoService.ExecuteAsync()
            Dim strd As String = ""
            For i As Integer = 0 To playlistResponse.Items.Count - 1
                lstPlaylists.Items.Add(playlistResponse.Items(i).Snippet.Title)
                playListIDs.Add(playlistResponse.Items(i).Id)
            Next



        Else
        End If

        AddUserSettings()

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
        If isPlayerReady And songPlayed Then
            dispUpdater.Type = MediaPlaybackType.Music
            dispUpdater.MusicProperties.Title = title
            dispUpdater.MusicProperties.AlbumArtist = currentlySelectedPlaylist
            dispUpdater.Thumbnail = RandomAccessStreamReference.CreateFromUri(New Uri("https://img.youtube.com/vi/" + videoID + "/hqdefault.jpg"))
            dispUpdater.Update()
        End If
    End Sub

    Private Sub lblGoogleLogin_MouseHover(sender As Object, e As EventArgs) Handles lblGoogleLogin.MouseHover
        lblGoogleLogin.ForeColor = Color.Silver
    End Sub

    Private Sub trkVolume_ValueChanged(sender As Object, e As EventArgs) Handles trkVolume.ValueChanged
        If isPlayerReady = True Then
            browser.ExecuteScriptAsync("setVolume(" + trkVolume.Value.ToString + ");")
        End If
    End Sub

    Private Sub pnlTopBar_Paint(sender As Object, e As PaintEventArgs) Handles pnlTopBar.Paint

    End Sub

    Private Sub lstPlaylists_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstPlaylists.SelectedIndexChanged


        If lstPlaylists.SelectedIndex > 2 Then
            songPlayed = True
            If Not currentURL = "https://rudrasharma.net/extraStuff/yt.php" Then
                browser.Load("https://rudrasharma.net/extraStuff/yt.php?v=" + playListIDs(lstPlaylists.SelectedIndex - 3).ToString)

            Else
                browser.ExecuteScriptAsync("loadPlaylistID('" + playListIDs(lstPlaylists.SelectedIndex - 3).ToString + "')")

            End If

            currentlySelectedPlaylist = lstPlaylists.SelectedItem.ToString
        End If

    End Sub

    Private Sub lblSettings_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub lstPlaylists_MeasureItem(sender As Object, e As MeasureItemEventArgs) Handles lstPlaylists.MeasureItem
        If e.Index = 1 Then
            e.ItemHeight = 53
        ElseIf e.Index = 2 Then
            e.ItemHeight = 24
        Else
            e.ItemHeight = 30
        End If

    End Sub

    Private Sub trkDuration_Scroll(sender As Object, e As EventArgs) Handles trkDuration.Scroll
        If isPlayerReady = True Then
            browser.ExecuteScriptAsync("seekTo(" + trkDuration.Value.ToString + ")")
        End If
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        hotkey.Unregister(0)
        hotkey.Unregister(1)
        hotkey.Unregister(2)
    End Sub

    Private Sub lblGoogleLogin_MouseLeave(sender As Object, e As EventArgs) Handles lblGoogleLogin.MouseLeave
        lblGoogleLogin.ForeColor = Color.White
    End Sub

    Private Sub lstPlaylists_DrawItem(sender As Object, e As DrawItemEventArgs) Handles lstPlaylists.DrawItem
        Dim brush As Brush
        brush = New Drawing.SolidBrush(Color.FromArgb(12, 12, 12))
        If (e.State And DrawItemState.Selected) = DrawItemState.Selected Then
            e.Graphics.FillRectangle(brush, e.Bounds)
        Else
            e.Graphics.FillRectangle(brush, e.Bounds)
        End If


        Dim secondBrush As Brush = New Drawing.SolidBrush(Color.FromArgb(127, 127, 127))
        Dim f123 As Font = New Drawing.Font("Times New Roman", 9, FontStyle.Bold)
        If e.Index = 2 Then
            Try
                e.Graphics.DrawString(lstPlaylists.Items(e.Index), f123,
            secondBrush, e.Bounds.X, e.Bounds.Y)
            Catch ex As Exception

            End Try

        Else
            Try
                e.Graphics.DrawString(lstPlaylists.Items(e.Index), lstPlaylists.Font,
            secondBrush, e.Bounds.X, e.Bounds.Y)
            Catch ex As Exception

            End Try
        End If

        e.DrawFocusRectangle()
    End Sub

    Private Sub lstPlaylists_MouseMove(sender As Object, e As MouseEventArgs) Handles lstPlaylists.MouseMove
        Dim ind As Integer = lstPlaylists.IndexFromPoint(e.X, e.Y)
        If Not ind = 0 And Not ind = 1 And Not ind = 2 Then
            lblPlaylistClear.Text = lstPlaylists.Items.Item(ind).ToString
        Else
            lblPlaylistClear.Text = ""
        End If

    End Sub

    Private Sub lstPlaylists_MouseLeave(sender As Object, e As EventArgs) Handles lstPlaylists.MouseLeave
        lblPlaylistClear.Text = ""
    End Sub
End Class
