<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.GlControl = New OpenTK.GLControl()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.Timer = New System.Windows.Forms.Timer(Me.components)
        Me.StartUp = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'GlControl
        '
        Me.GlControl.BackColor = System.Drawing.Color.Black
        Me.GlControl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GlControl.Location = New System.Drawing.Point(0, 0)
        Me.GlControl.Name = "GlControl"
        Me.GlControl.Size = New System.Drawing.Size(984, 586)
        Me.GlControl.TabIndex = 6
        Me.GlControl.VSync = False
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Filter = "Level files (*.lvl)|*.lvl"
        Me.OpenFileDialog.Title = "Load level"
        '
        'Timer
        '
        Me.Timer.Interval = 8
        '
        'StartUp
        '
        Me.StartUp.Enabled = True
        Me.StartUp.Interval = 8
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(984, 586)
        Me.Controls.Add(Me.GlControl)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Breaker ball"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GlControl As OpenTK.GLControl
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents Timer As Timer
    Friend WithEvents StartUp As Timer
End Class
