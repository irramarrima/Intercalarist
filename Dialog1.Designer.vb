<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dialog1
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Dialog1))
        TableLayoutPanel1 = New TableLayoutPanel()
        OK_Button = New Button()
        LabelProductName = New Label()
        LabelVersion = New Label()
        LabelCopyright = New Label()
        LabelDeedLink = New LinkLabel()
        PictureBox2 = New PictureBox()
        TableLayoutPanel1.SuspendLayout()
        CType(PictureBox2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        TableLayoutPanel1.ColumnCount = 1
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Controls.Add(OK_Button, 0, 0)
        TableLayoutPanel1.Location = New Point(452, 287)
        TableLayoutPanel1.Margin = New Padding(4, 3, 4, 3)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel1.Size = New Size(90, 33)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' OK_Button
        ' 
        OK_Button.Anchor = AnchorStyles.None
        OK_Button.Location = New Point(6, 3)
        OK_Button.Margin = New Padding(4, 3, 4, 3)
        OK_Button.Name = "OK_Button"
        OK_Button.Size = New Size(77, 27)
        OK_Button.TabIndex = 0
        OK_Button.Text = "OK"
        ' 
        ' LabelProductName
        ' 
        LabelProductName.AutoSize = True
        LabelProductName.Font = New Font("Segoe UI", 13F, FontStyle.Bold)
        LabelProductName.Location = New Point(118, 24)
        LabelProductName.Name = "LabelProductName"
        LabelProductName.Size = New Size(67, 25)
        LabelProductName.TabIndex = 1
        LabelProductName.Text = "Label1"
        ' 
        ' LabelVersion
        ' 
        LabelVersion.AutoSize = True
        LabelVersion.Location = New Point(122, 63)
        LabelVersion.Name = "LabelVersion"
        LabelVersion.Size = New Size(41, 15)
        LabelVersion.TabIndex = 2
        LabelVersion.Text = "Label2"
        ' 
        ' LabelCopyright
        ' 
        LabelCopyright.Location = New Point(122, 95)
        LabelCopyright.Name = "LabelCopyright"
        LabelCopyright.Size = New Size(375, 163)
        LabelCopyright.TabIndex = 3
        LabelCopyright.Text = "Label3"
        ' 
        ' LabelDeedLink
        ' 
        LabelDeedLink.AutoSize = True
        LabelDeedLink.Location = New Point(122, 267)
        LabelDeedLink.Name = "LabelDeedLink"
        LabelDeedLink.Size = New Size(63, 15)
        LabelDeedLink.TabIndex = 5
        LabelDeedLink.TabStop = True
        LabelDeedLink.Text = "LinkLabel1"
        ' 
        ' PictureBox2
        ' 
        PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), Image)
        PictureBox2.Location = New Point(21, 24)
        PictureBox2.Name = "PictureBox2"
        PictureBox2.Size = New Size(80, 80)
        PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox2.TabIndex = 7
        PictureBox2.TabStop = False
        ' 
        ' Dialog1
        ' 
        AcceptButton = OK_Button
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(556, 334)
        Controls.Add(PictureBox2)
        Controls.Add(LabelDeedLink)
        Controls.Add(LabelCopyright)
        Controls.Add(LabelVersion)
        Controls.Add(LabelProductName)
        Controls.Add(TableLayoutPanel1)
        FormBorderStyle = FormBorderStyle.FixedDialog
        Margin = New Padding(4, 3, 4, 3)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Dialog1"
        ShowInTaskbar = False
        StartPosition = FormStartPosition.CenterParent
        Text = "Dialog1"
        TableLayoutPanel1.ResumeLayout(False)
        CType(PictureBox2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As Button
    Friend WithEvents LabelProductName As Label
    Friend WithEvents LabelVersion As Label
    Friend WithEvents LabelCopyright As Label
    Friend WithEvents LabelDeedLink As LinkLabel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents PictureBox2 As PictureBox

End Class
