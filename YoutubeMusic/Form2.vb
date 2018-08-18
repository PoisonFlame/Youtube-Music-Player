Imports System.Threading
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Auth.OAuth2.Flows
Imports Google.Apis.Services
Imports Google.Apis.Util.Store
Imports Google.Apis.YouTube.v3

Public Class Form2
    Dim res As Boolean
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GetAuth()
    End Sub
    Public Async Sub GetAuth()
        res = Await IsUserAuthenticated()

        If res = True Then
            lblAuthNotice.Text = "Authenticated"
        Else
            lblAuthNotice.Text = "Not Authenticated"
        End If
    End Sub
    Public Async Function IsUserAuthenticated() As Task(Of Boolean)
        Dim initializer = New GoogleAuthorizationCodeFlow.Initializer With {
        .ClientSecrets = New ClientSecrets With {
            .ClientId = Util.getClientID,
            .ClientSecret = Util.getClientSecret
        },
        .Scopes = {YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeReadonly},
        .DataStore = Util.getDataStore
        }
        Dim flow = New AuthorizationCodeFlow(initializer)
        Dim codeReceiver = New GoogleWebAuthorizationBroker()
        Dim token = Await flow.LoadTokenAsync("user", CancellationToken.None).ConfigureAwait(False)
        ' MsgBox(token)

        If token Is Nothing OrElse (token.RefreshToken Is Nothing AndAlso token.IsExpired(flow.Clock)) Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Async Sub btnSignIn_Click(sender As Object, e As EventArgs) Handles btnSignIn.Click

        Dim cltSecrets As New ClientSecrets
        cltSecrets.ClientId = Util.getClientID
        cltSecrets.ClientSecret = Util.getClientSecret

        Dim credentials = Await GoogleWebAuthorizationBroker.AuthorizeAsync(cltSecrets, {YouTubeService.Scope.Youtube, YouTubeService.Scope.YoutubeReadonly}, "user", CancellationToken.None, CType(Util.getDataStore, IDataStore))

        Dim bcs = New BaseClientService.Initializer()
        bcs.HttpClientInitializer = credentials
        bcs.ApplicationName = "SpoTube"

        GetAuth()

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles lblAuthNotice.Click

    End Sub

    Private Sub Label1_TextChanged(sender As Object, e As EventArgs) Handles lblAuthNotice.TextChanged
        If lblAuthNotice.Text = "Authenticated" Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        Me.Hide()

    End Sub
End Class