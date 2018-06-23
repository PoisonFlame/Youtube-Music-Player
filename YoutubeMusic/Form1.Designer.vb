<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.pnlBrowser = New System.Windows.Forms.Panel()
        Me.pnlNavigation = New System.Windows.Forms.Panel()
        Me.lblSettings = New System.Windows.Forms.Label()
        Me.pnlTopBar = New System.Windows.Forms.Panel()
        Me.pnlBottomBar = New System.Windows.Forms.Panel()
        Me.pnlVolumeControl = New System.Windows.Forms.Panel()
        Me.trkVolume = New System.Windows.Forms.TrackBar()
        Me.pnlVideoInfo = New System.Windows.Forms.Panel()
        Me.lblVideoTitle = New System.Windows.Forms.Label()
        Me.pnlMediaControls = New System.Windows.Forms.Panel()
        Me.lblDuration = New System.Windows.Forms.Label()
        Me.lblCurrentTime = New System.Windows.Forms.Label()
        Me.trkDuration = New System.Windows.Forms.TrackBar()
        Me.pnlOverlay = New System.Windows.Forms.Panel()
        Me.tmrSettings = New System.Windows.Forms.Timer(Me.components)
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.picVolume = New System.Windows.Forms.PictureBox()
        Me.picThumbnail = New System.Windows.Forms.PictureBox()
        Me.picRepeat = New System.Windows.Forms.PictureBox()
        Me.picNext = New System.Windows.Forms.PictureBox()
        Me.picPausePlay = New System.Windows.Forms.PictureBox()
        Me.picPrev = New System.Windows.Forms.PictureBox()
        Me.picShuffle = New System.Windows.Forms.PictureBox()
        Me.pnlNavigation.SuspendLayout()
        Me.pnlBottomBar.SuspendLayout()
        Me.pnlVolumeControl.SuspendLayout()
        CType(Me.trkVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlVideoInfo.SuspendLayout()
        Me.pnlMediaControls.SuspendLayout()
        CType(Me.trkDuration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlOverlay.SuspendLayout()
        CType(Me.picVolume, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picThumbnail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picRepeat, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picNext, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPausePlay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPrev, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picShuffle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlBrowser
        '
        Me.pnlBrowser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlBrowser.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.pnlBrowser.Location = New System.Drawing.Point(169, 40)
        Me.pnlBrowser.Name = "pnlBrowser"
        Me.pnlBrowser.Size = New System.Drawing.Size(631, 306)
        Me.pnlBrowser.TabIndex = 0
        '
        'pnlNavigation
        '
        Me.pnlNavigation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlNavigation.BackColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer), CType(CType(45, Byte), Integer))
        Me.pnlNavigation.Controls.Add(Me.lblSettings)
        Me.pnlNavigation.Location = New System.Drawing.Point(0, 40)
        Me.pnlNavigation.Name = "pnlNavigation"
        Me.pnlNavigation.Size = New System.Drawing.Size(169, 306)
        Me.pnlNavigation.TabIndex = 1
        '
        'lblSettings
        '
        Me.lblSettings.AutoSize = True
        Me.lblSettings.Font = New System.Drawing.Font("Microsoft Sans Serif", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSettings.ForeColor = System.Drawing.Color.White
        Me.lblSettings.Location = New System.Drawing.Point(12, 9)
        Me.lblSettings.Name = "lblSettings"
        Me.lblSettings.Size = New System.Drawing.Size(128, 33)
        Me.lblSettings.TabIndex = 0
        Me.lblSettings.Text = "Settings"
        '
        'pnlTopBar
        '
        Me.pnlTopBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTopBar.Location = New System.Drawing.Point(0, 0)
        Me.pnlTopBar.Name = "pnlTopBar"
        Me.pnlTopBar.Size = New System.Drawing.Size(800, 40)
        Me.pnlTopBar.TabIndex = 0
        '
        'pnlBottomBar
        '
        Me.pnlBottomBar.BackColor = System.Drawing.Color.FromArgb(CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer), CType(CType(36, Byte), Integer))
        Me.pnlBottomBar.Controls.Add(Me.pnlVolumeControl)
        Me.pnlBottomBar.Controls.Add(Me.pnlVideoInfo)
        Me.pnlBottomBar.Controls.Add(Me.pnlMediaControls)
        Me.pnlBottomBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlBottomBar.Location = New System.Drawing.Point(0, 344)
        Me.pnlBottomBar.Name = "pnlBottomBar"
        Me.pnlBottomBar.Size = New System.Drawing.Size(800, 106)
        Me.pnlBottomBar.TabIndex = 1
        '
        'pnlVolumeControl
        '
        Me.pnlVolumeControl.Controls.Add(Me.trkVolume)
        Me.pnlVolumeControl.Controls.Add(Me.picVolume)
        Me.pnlVolumeControl.Dock = System.Windows.Forms.DockStyle.Right
        Me.pnlVolumeControl.Location = New System.Drawing.Point(600, 0)
        Me.pnlVolumeControl.Name = "pnlVolumeControl"
        Me.pnlVolumeControl.Size = New System.Drawing.Size(200, 106)
        Me.pnlVolumeControl.TabIndex = 5
        '
        'trkVolume
        '
        Me.trkVolume.AutoSize = False
        Me.trkVolume.LargeChange = 20
        Me.trkVolume.Location = New System.Drawing.Point(52, 45)
        Me.trkVolume.Maximum = 100
        Me.trkVolume.Name = "trkVolume"
        Me.trkVolume.Size = New System.Drawing.Size(104, 30)
        Me.trkVolume.SmallChange = 10
        Me.trkVolume.TabIndex = 1
        Me.trkVolume.TickStyle = System.Windows.Forms.TickStyle.None
        Me.trkVolume.Value = 50
        '
        'pnlVideoInfo
        '
        Me.pnlVideoInfo.Controls.Add(Me.picThumbnail)
        Me.pnlVideoInfo.Controls.Add(Me.lblVideoTitle)
        Me.pnlVideoInfo.Dock = System.Windows.Forms.DockStyle.Left
        Me.pnlVideoInfo.Location = New System.Drawing.Point(0, 0)
        Me.pnlVideoInfo.Name = "pnlVideoInfo"
        Me.pnlVideoInfo.Size = New System.Drawing.Size(255, 106)
        Me.pnlVideoInfo.TabIndex = 3
        '
        'lblVideoTitle
        '
        Me.lblVideoTitle.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVideoTitle.ForeColor = System.Drawing.Color.White
        Me.lblVideoTitle.Location = New System.Drawing.Point(107, 11)
        Me.lblVideoTitle.Name = "lblVideoTitle"
        Me.lblVideoTitle.Size = New System.Drawing.Size(141, 86)
        Me.lblVideoTitle.TabIndex = 1
        Me.lblVideoTitle.Text = "No Info Available"
        Me.lblVideoTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlMediaControls
        '
        Me.pnlMediaControls.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlMediaControls.Controls.Add(Me.lblDuration)
        Me.pnlMediaControls.Controls.Add(Me.lblCurrentTime)
        Me.pnlMediaControls.Controls.Add(Me.trkDuration)
        Me.pnlMediaControls.Controls.Add(Me.picRepeat)
        Me.pnlMediaControls.Controls.Add(Me.picNext)
        Me.pnlMediaControls.Controls.Add(Me.picPausePlay)
        Me.pnlMediaControls.Controls.Add(Me.picPrev)
        Me.pnlMediaControls.Controls.Add(Me.picShuffle)
        Me.pnlMediaControls.Location = New System.Drawing.Point(254, 3)
        Me.pnlMediaControls.Name = "pnlMediaControls"
        Me.pnlMediaControls.Size = New System.Drawing.Size(347, 103)
        Me.pnlMediaControls.TabIndex = 2
        '
        'lblDuration
        '
        Me.lblDuration.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.lblDuration.AutoSize = True
        Me.lblDuration.ForeColor = System.Drawing.Color.White
        Me.lblDuration.Location = New System.Drawing.Point(281, 73)
        Me.lblDuration.Name = "lblDuration"
        Me.lblDuration.Size = New System.Drawing.Size(28, 13)
        Me.lblDuration.TabIndex = 7
        Me.lblDuration.Text = "0:00"
        '
        'lblCurrentTime
        '
        Me.lblCurrentTime.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCurrentTime.AutoSize = True
        Me.lblCurrentTime.ForeColor = System.Drawing.Color.White
        Me.lblCurrentTime.Location = New System.Drawing.Point(57, 73)
        Me.lblCurrentTime.Name = "lblCurrentTime"
        Me.lblCurrentTime.Size = New System.Drawing.Size(28, 13)
        Me.lblCurrentTime.TabIndex = 6
        Me.lblCurrentTime.Text = "0:00"
        '
        'trkDuration
        '
        Me.trkDuration.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trkDuration.AutoSize = False
        Me.trkDuration.Location = New System.Drawing.Point(91, 73)
        Me.trkDuration.Name = "trkDuration"
        Me.trkDuration.Size = New System.Drawing.Size(184, 15)
        Me.trkDuration.TabIndex = 5
        Me.trkDuration.TickStyle = System.Windows.Forms.TickStyle.None
        '
        'pnlOverlay
        '
        Me.pnlOverlay.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlOverlay.Controls.Add(Me.lblLoading)
        Me.pnlOverlay.Location = New System.Drawing.Point(169, 40)
        Me.pnlOverlay.Name = "pnlOverlay"
        Me.pnlOverlay.Size = New System.Drawing.Size(631, 306)
        Me.pnlOverlay.TabIndex = 2
        '
        'tmrSettings
        '
        Me.tmrSettings.Enabled = True
        '
        'lblLoading
        '
        Me.lblLoading.BackColor = System.Drawing.Color.Transparent
        Me.lblLoading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoading.ForeColor = System.Drawing.Color.White
        Me.lblLoading.Image = Global.YoutubeMusic.My.Resources.Resources.loading2
        Me.lblLoading.Location = New System.Drawing.Point(0, 0)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(631, 306)
        Me.lblLoading.TabIndex = 1
        Me.lblLoading.Text = "Loading. Please Wait."
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        '
        'picVolume
        '
        Me.picVolume.Image = CType(resources.GetObject("picVolume.Image"), System.Drawing.Image)
        Me.picVolume.Location = New System.Drawing.Point(16, 40)
        Me.picVolume.Name = "picVolume"
        Me.picVolume.Size = New System.Drawing.Size(30, 30)
        Me.picVolume.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picVolume.TabIndex = 0
        Me.picVolume.TabStop = False
        '
        'picThumbnail
        '
        Me.picThumbnail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.picThumbnail.ErrorImage = Global.YoutubeMusic.My.Resources.Resources.No_Image
        Me.picThumbnail.Image = Global.YoutubeMusic.My.Resources.Resources.No_Image
        Me.picThumbnail.Location = New System.Drawing.Point(15, 11)
        Me.picThumbnail.Name = "picThumbnail"
        Me.picThumbnail.Size = New System.Drawing.Size(86, 86)
        Me.picThumbnail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picThumbnail.TabIndex = 0
        Me.picThumbnail.TabStop = False
        '
        'picRepeat
        '
        Me.picRepeat.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picRepeat.Image = Global.YoutubeMusic.My.Resources.Resources.repeat
        Me.picRepeat.Location = New System.Drawing.Point(235, 27)
        Me.picRepeat.Name = "picRepeat"
        Me.picRepeat.Size = New System.Drawing.Size(40, 40)
        Me.picRepeat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picRepeat.TabIndex = 4
        Me.picRepeat.TabStop = False
        '
        'picNext
        '
        Me.picNext.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picNext.Image = Global.YoutubeMusic.My.Resources.Resources.forward
        Me.picNext.Location = New System.Drawing.Point(199, 27)
        Me.picNext.Name = "picNext"
        Me.picNext.Size = New System.Drawing.Size(40, 40)
        Me.picNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picNext.TabIndex = 3
        Me.picNext.TabStop = False
        '
        'picPausePlay
        '
        Me.picPausePlay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picPausePlay.Image = Global.YoutubeMusic.My.Resources.Resources.play
        Me.picPausePlay.Location = New System.Drawing.Point(163, 27)
        Me.picPausePlay.Name = "picPausePlay"
        Me.picPausePlay.Size = New System.Drawing.Size(40, 40)
        Me.picPausePlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPausePlay.TabIndex = 2
        Me.picPausePlay.TabStop = False
        '
        'picPrev
        '
        Me.picPrev.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picPrev.Image = Global.YoutubeMusic.My.Resources.Resources.back
        Me.picPrev.Location = New System.Drawing.Point(127, 27)
        Me.picPrev.Name = "picPrev"
        Me.picPrev.Size = New System.Drawing.Size(40, 40)
        Me.picPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPrev.TabIndex = 1
        Me.picPrev.TabStop = False
        '
        'picShuffle
        '
        Me.picShuffle.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.picShuffle.Image = Global.YoutubeMusic.My.Resources.Resources.shuffle
        Me.picShuffle.Location = New System.Drawing.Point(91, 27)
        Me.picShuffle.Name = "picShuffle"
        Me.picShuffle.Size = New System.Drawing.Size(40, 40)
        Me.picShuffle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picShuffle.TabIndex = 0
        Me.picShuffle.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.pnlOverlay)
        Me.Controls.Add(Me.pnlNavigation)
        Me.Controls.Add(Me.pnlBottomBar)
        Me.Controls.Add(Me.pnlTopBar)
        Me.Controls.Add(Me.pnlBrowser)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Youtube Music Player"
        Me.pnlNavigation.ResumeLayout(False)
        Me.pnlNavigation.PerformLayout()
        Me.pnlBottomBar.ResumeLayout(False)
        Me.pnlVolumeControl.ResumeLayout(False)
        CType(Me.trkVolume, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlVideoInfo.ResumeLayout(False)
        Me.pnlMediaControls.ResumeLayout(False)
        Me.pnlMediaControls.PerformLayout()
        CType(Me.trkDuration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlOverlay.ResumeLayout(False)
        CType(Me.picVolume, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picThumbnail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picRepeat, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picNext, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPausePlay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPrev, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picShuffle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlBrowser As Panel
    Friend WithEvents pnlNavigation As Panel
    Friend WithEvents lblSettings As Label
    Friend WithEvents pnlTopBar As Panel
    Friend WithEvents pnlBottomBar As Panel
    Friend WithEvents pnlOverlay As Panel
    Friend WithEvents tmrSettings As Timer
    Friend WithEvents picThumbnail As PictureBox
    Friend WithEvents lblVideoTitle As Label
    Friend WithEvents pnlMediaControls As Panel
    Friend WithEvents picRepeat As PictureBox
    Friend WithEvents picNext As PictureBox
    Friend WithEvents picPausePlay As PictureBox
    Friend WithEvents picPrev As PictureBox
    Friend WithEvents picShuffle As PictureBox
    Friend WithEvents pnlVolumeControl As Panel
    Friend WithEvents pnlVideoInfo As Panel
    Friend WithEvents trkVolume As TrackBar
    Friend WithEvents picVolume As PictureBox
    Friend WithEvents lblDuration As Label
    Friend WithEvents lblCurrentTime As Label
    Friend WithEvents trkDuration As TrackBar
    Friend WithEvents lblLoading As Label
End Class
