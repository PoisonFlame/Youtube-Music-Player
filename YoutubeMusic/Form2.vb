Imports System.Threading
Imports Google.Apis.Auth.OAuth2
Imports Google.Apis.Auth.OAuth2.Flows
Imports Google.Apis.Util.Store
Imports Google.Apis.YouTube.v3

Public Class Form2
    Dim res As Boolean
    Private Async Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        res = Await IsUserAuthenticated()

        If res = True Then
            Label1.Text = "Authenticated"
        Else
            Label1.Text = "Not Authenticated"
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form1.Show()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_TextChanged(sender As Object, e As EventArgs) Handles Label1.TextChanged
        If Label1.Text = "Authenticated" Then
            Form1.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()

    End Sub
End Class