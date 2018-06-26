Public Class MyComboBox
    Inherits ComboBox

    Private WithEvents cms As ContextMenuStrip = Nothing
    Private Const WM_LBUTTONDOWN As Integer = &H201
    Private Const WM_LBUTTONDBLCLK As Integer = &H203
    Private formWidth As Integer
    Public Sub New(width As Integer)
        Me.formWidth = width
    End Sub

    Public Sub updateWidth(width As Integer)
        Me.formWidth = width
    End Sub
    Private Sub cms_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)

    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = WM_LBUTTONDBLCLK Then Return
        If m.Msg = WM_LBUTTONDOWN Then
            If cms Is Nothing Then
                cms = New ContextMenuStrip
                cms.ShowImageMargin = False
                'cms.ShowCheckMargin = False
                cms.Font = Me.Font
                cms.ForeColor = Me.ForeColor
                cms.BackColor = Me.BackColor
                cms.Height = Me.DropDownHeight
                'cms.Width = Me.DropDownWidth
                cms.Renderer = New MyRenderer
                cms.AutoSize = False
                cms.Width = 125
                cms.Height = 42
                AddHandler cms.MouseMove, AddressOf cms_MouseMove
                For Each itm As String In Me.Items
                    cms.Items.Add(itm)
                Next
                Dim pts As Point = Me.PointToScreen(Me.Location)
                Dim scrn As Rectangle = Screen.FromControl(Me).WorkingArea
                Dim loc As Point = New Point(0, Me.Height)
                If (pts.Y + Me.Height + cms.Height) > scrn.Bottom Then
                    loc.Y = -1 - cms.Height - Me.Height
                End If
                If (pts.X + cms.Width) > scrn.Right Then
                    loc.X = 0 - (cms.Width - Me.Width) - 18
                End If

                'loc.X = 0
                'loc.X = formWidth - (cms.Width - Me.Width) - 18
                Console.WriteLine(Me.DropDownWidth)
                loc.X = -(Me.DropDownWidth) + 36 + 8
                cms.Show(Me, loc.X, loc.Y)
                Return
            Else
                cms.Dispose()
                cms = Nothing
                Return
            End If
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub cms_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripDropDownClosingEventArgs) Handles cms.Closing
        Dim btnrect As New Rectangle(Me.Bounds.Right - 18, Me.Bounds.Top, 18, Me.Bounds.Bottom)
        If Not btnrect.Contains(Me.Parent.PointToClient(MousePosition)) Then
            cms = Nothing
        End If
    End Sub

    Private Sub cms_ItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles cms.ItemClicked
        cms.Dispose()
        cms = Nothing
        Me.SelectedItem = e.ClickedItem.Text
    End Sub

    Private Class MyRenderer
        Inherits ToolStripProfessionalRenderer
        Protected Overrides Sub OnRenderMenuItemBackground(e As ToolStripItemRenderEventArgs)
            If Not e.Item.Selected Then
                MyBase.OnRenderMenuItemBackground(e)
            Else
                Dim menuRectangle As Rectangle = New Rectangle(Point.Empty, e.Item.Size)
                Dim br As Brush = New Drawing.SolidBrush(Color.FromArgb(28, 28, 28))
                'Fill Color
                e.Graphics.FillRectangle(br, menuRectangle)
                'Border Color
                Dim pn As Pen = New Drawing.Pen(br)
                e.Graphics.DrawRectangle(pn, 1, 0, menuRectangle.Width - 2, menuRectangle.Height - 1)
            End If

        End Sub
    End Class

End Class

