Imports System.IO
Imports Google.Apis.Util.Store

Module Util
    Dim hiddenPath As String = Path.GetFullPath(Path.Combine(My.Computer.FileSystem.CurrentDirectory.ToString, "..\..\..\")) + "HiddenContent\"
    Public Function getApiKey() As String
        Dim apiKey As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "key_api.txt")
        Return apiKey
    End Function
    Public Function getClientID() As String
        Dim oauthClientID As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "oauth_client.txt")
        Return oauthClientID
    End Function
    Public Function getClientSecret() As String
        Dim clientSecret As String = My.Computer.FileSystem.ReadAllText(hiddenPath + "client_secret.txt")
        Return clientSecret
    End Function
    Public Function getDataStore()
        Return New FileDataStore("Spotube")
    End Function
End Module
