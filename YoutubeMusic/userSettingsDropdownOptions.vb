Imports System.Threading
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Services
Imports Google.Apis.YouTube.v3

Public Module userSettingsDropdownOptions
    Public Sub Load(opt As String)
        If opt = "Login" Then
            Login()
        ElseIf opt = "Logout" Then
            Logout()
        ElseIf opt = "Show Video" Then
            ShowVideo()
        ElseIf opt = "Hide Video" Then
            HideVideo()
        ElseIf opt = "[E]Show Video" Or opt = "[E]Hide Video" Then
            'Do Nothing
        Else
            MsgBox("Unknown Error Occured. Please try again")
        End If
    End Sub

    Private Async Sub Login()
        Dim cltSecrets As New ClientSecrets
        cltSecrets.ClientId = Util.getClientID
        cltSecrets.ClientSecret = Util.getClientSecret
        Form1.credentials = Await GoogleWebAuthorizationBroker.AuthorizeAsync(cltSecrets, {YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeReadonly}, "user", CancellationToken.None)
        My.Settings.accessToken = Form1.credentials.Token.AccessToken.ToString
        My.Settings.Save()


        Dim bcs = New BaseClientService.Initializer()
        bcs.HttpClientInitializer = Form1.credentials
        bcs.ApplicationName = "Youtube Music Player"
        Form1.youtubeService = New YouTubeService(bcs)
        Dim channelInfoService = Form1.youtubeService.Channels.List("snippet")
        channelInfoService.Mine = True
        Dim channelListResponse = Await channelInfoService.ExecuteAsync()
        Form1.lblGoogleLogin.Text = "Welcome, " + channelListResponse.Items(0).Snippet.Title

        Dim playlistInfoService = Form1.youtubeService.Playlists.List("id,snippet")
        playlistInfoService.Mine = True
        playlistInfoService.MaxResults = 50
        Dim playlistResponse = Await playlistInfoService.ExecuteAsync()
        Dim strd As String = ""
        Form1.playListIDs.Clear()
        Form1.lstPlaylists.Items.Clear()
        Form1.lstPlaylists.Items.Add("Home")
        Form1.lstPlaylists.Items.Add("Browse")
        Form1.lstPlaylists.Items.Add("YOUR LIBRARY")

        For i As Integer = 0 To playlistResponse.Items.Count - 1
            Form1.lstPlaylists.Items.Add(playlistResponse.Items(i).Snippet.Title)
            Form1.playListIDs.Add(playlistResponse.Items(i).Id)
        Next
        Form1.userSettingDropDown.Items.Clear()
        Form1.userSettingDropDown.Items.Add("Logout")
    End Sub
    Private Async Sub Logout()
        'Logout Crap
        Await Form1.credentials.RevokeTokenAsync(CancellationToken.None)
        My.Settings.accessToken = "null"
        My.Settings.Save()
        Form1.lblGoogleLogin.Text = "Not Logged In"
        Form1.userSettingDropDown.Items.Clear()
        Form1.userSettingDropDown.Items.Add("Login")
        Form1.playListIDs.Clear()
        Form1.lstPlaylists.Items.Clear()
        Form1.lstPlaylists.Items.Add("No Info Available")
        Form1.lstPlaylists.Items.Add("Login to See Playlists")
    End Sub
    Private Sub ShowVideo()
        Form1.userSettingDropDown.Items.Item(Form1.userSettingDropDown.Items.IndexOf("Show Video")) = "[E]Show Video"
        Form1.userSettingDropDown.Items.Item(Form1.userSettingDropDown.Items.IndexOf("[E]Hide Video")) = "Hide Video"
        My.Settings.showVideo = True
        My.Settings.Save()
        Form1.pnlBrowser.Visible = True
    End Sub
    Private Sub HideVideo()
        Form1.userSettingDropDown.Items.Item(Form1.userSettingDropDown.Items.IndexOf("Hide Video")) = "[E]Hide Video"
        Form1.userSettingDropDown.Items.Item(Form1.userSettingDropDown.Items.IndexOf("[E]Show Video")) = "Show Video"
        My.Settings.showVideo = False
        My.Settings.Save()
        Form1.pnlBrowser.Visible = False
    End Sub
End Module
