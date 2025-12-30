<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MainViewForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainViewForm))
        MenuStrip1 = New MenuStrip()
        ViewToolStripMenuItem = New ToolStripMenuItem()
        MenuViewCalendar = New ToolStripMenuItem()
        MenuViewEditor = New ToolStripMenuItem()
        ToolStripMenuItem4 = New ToolStripSeparator()
        LanguageToolStripMenuItem = New ToolStripMenuItem()
        EnglishToolStripMenuItem = New ToolStripMenuItem()
        SpanishToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem5 = New ToolStripSeparator()
        AboutToolStripMenuItem = New ToolStripMenuItem()
        FileToolStripMenuItem = New ToolStripMenuItem()
        NewToolStripMenuItem = New ToolStripMenuItem()
        OpenToolStripMenuItem = New ToolStripMenuItem()
        RecentToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem3 = New ToolStripSeparator()
        SetAsDefaultToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem2 = New ToolStripSeparator()
        SaveToolStripMenuItem = New ToolStripMenuItem()
        SaveAsToolStripMenuItem = New ToolStripMenuItem()
        ToolStripMenuItem1 = New ToolStripSeparator()
        ExitToolStripMenuItem = New ToolStripMenuItem()
        PanelEditor = New Panel()
        GroupBox5 = New GroupBox()
        MonthsPanel = New Panel()
        LabelTitleGregorian = New Label()
        LabelLeap = New Label()
        LabelDays = New Label()
        LabelMonthName = New Label()
        ButtonAddMonth = New Button()
        GroupBox6 = New GroupBox()
        LabelDayNumber = New Label()
        ComboCycleDay = New ComboBox()
        ComboCycleMonth = New ComboBox()
        LabelDayBudget = New Label()
        PanelCalendar = New Panel()
        GroupBox1 = New GroupBox()
        CheckStartToday = New CheckBox()
        ComboStartDay = New ComboBox()
        ComboStartMonth = New ComboBox()
        ComboStartYear = New ComboBox()
        GroupBox4 = New GroupBox()
        LabelYearDetails = New Label()
        ProgressBarYear = New ProgressBar()
        GroupBox3 = New GroupBox()
        RichWeekNotes = New RichTextBox()
        ComboWeekSelect = New ComboBox()
        LabelCalendarDate = New Label()
        GroupBox2 = New GroupBox()
        RichCurrentDates = New RichTextBox()
        MainTimer = New Timer(components)
        ToolTip1 = New ToolTip(components)
        MenuStrip1.SuspendLayout()
        PanelEditor.SuspendLayout()
        GroupBox5.SuspendLayout()
        MonthsPanel.SuspendLayout()
        GroupBox6.SuspendLayout()
        PanelCalendar.SuspendLayout()
        GroupBox1.SuspendLayout()
        GroupBox4.SuspendLayout()
        GroupBox3.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {ViewToolStripMenuItem, FileToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(1562, 24)
        MenuStrip1.TabIndex = 19
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' ViewToolStripMenuItem
        ' 
        ViewToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {MenuViewCalendar, MenuViewEditor, ToolStripMenuItem4, LanguageToolStripMenuItem, ToolStripMenuItem5, AboutToolStripMenuItem})
        ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        ViewToolStripMenuItem.Size = New Size(44, 20)
        ViewToolStripMenuItem.Text = "&View"
        ' 
        ' MenuViewCalendar
        ' 
        MenuViewCalendar.Name = "MenuViewCalendar"
        MenuViewCalendar.ShortcutKeyDisplayString = ""
        MenuViewCalendar.ShortcutKeys = Keys.F1
        MenuViewCalendar.Size = New Size(180, 22)
        MenuViewCalendar.Text = "&Calendar"
        ' 
        ' MenuViewEditor
        ' 
        MenuViewEditor.Name = "MenuViewEditor"
        MenuViewEditor.ShortcutKeys = Keys.F2
        MenuViewEditor.Size = New Size(180, 22)
        MenuViewEditor.Text = "&Editor"
        ' 
        ' ToolStripMenuItem4
        ' 
        ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        ToolStripMenuItem4.Size = New Size(177, 6)
        ' 
        ' LanguageToolStripMenuItem
        ' 
        LanguageToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {EnglishToolStripMenuItem, SpanishToolStripMenuItem})
        LanguageToolStripMenuItem.Name = "LanguageToolStripMenuItem"
        LanguageToolStripMenuItem.Size = New Size(180, 22)
        LanguageToolStripMenuItem.Text = "&Language"
        ' 
        ' EnglishToolStripMenuItem
        ' 
        EnglishToolStripMenuItem.Name = "EnglishToolStripMenuItem"
        EnglishToolStripMenuItem.Size = New Size(115, 22)
        EnglishToolStripMenuItem.Text = "English"
        ' 
        ' SpanishToolStripMenuItem
        ' 
        SpanishToolStripMenuItem.Name = "SpanishToolStripMenuItem"
        SpanishToolStripMenuItem.Size = New Size(115, 22)
        SpanishToolStripMenuItem.Text = "Español"
        ' 
        ' ToolStripMenuItem5
        ' 
        ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        ToolStripMenuItem5.Size = New Size(177, 6)
        ' 
        ' AboutToolStripMenuItem
        ' 
        AboutToolStripMenuItem.Image = CType(resources.GetObject("AboutToolStripMenuItem.Image"), Image)
        AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        AboutToolStripMenuItem.Size = New Size(180, 22)
        AboutToolStripMenuItem.Text = "A&bout..."
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {NewToolStripMenuItem, OpenToolStripMenuItem, RecentToolStripMenuItem, ToolStripMenuItem3, SetAsDefaultToolStripMenuItem, ToolStripMenuItem2, SaveToolStripMenuItem, SaveAsToolStripMenuItem, ToolStripMenuItem1, ExitToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(37, 20)
        FileToolStripMenuItem.Text = "&File"
        ' 
        ' NewToolStripMenuItem
        ' 
        NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        NewToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.N
        NewToolStripMenuItem.Size = New Size(203, 22)
        NewToolStripMenuItem.Text = "&New calendar"
        ' 
        ' OpenToolStripMenuItem
        ' 
        OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        OpenToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.O
        OpenToolStripMenuItem.Size = New Size(203, 22)
        OpenToolStripMenuItem.Text = "&Open calendar..."
        ' 
        ' RecentToolStripMenuItem
        ' 
        RecentToolStripMenuItem.Name = "RecentToolStripMenuItem"
        RecentToolStripMenuItem.Size = New Size(203, 22)
        RecentToolStripMenuItem.Text = "Recent"
        ' 
        ' ToolStripMenuItem3
        ' 
        ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        ToolStripMenuItem3.Size = New Size(200, 6)
        ' 
        ' SetAsDefaultToolStripMenuItem
        ' 
        SetAsDefaultToolStripMenuItem.Name = "SetAsDefaultToolStripMenuItem"
        SetAsDefaultToolStripMenuItem.Size = New Size(203, 22)
        SetAsDefaultToolStripMenuItem.Text = "Set as &default"
        ' 
        ' ToolStripMenuItem2
        ' 
        ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        ToolStripMenuItem2.Size = New Size(200, 6)
        ' 
        ' SaveToolStripMenuItem
        ' 
        SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        SaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        SaveToolStripMenuItem.Size = New Size(203, 22)
        SaveToolStripMenuItem.Text = "&Save"
        ' 
        ' SaveAsToolStripMenuItem
        ' 
        SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        SaveAsToolStripMenuItem.Size = New Size(203, 22)
        SaveAsToolStripMenuItem.Text = "Save &as..."
        ' 
        ' ToolStripMenuItem1
        ' 
        ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        ToolStripMenuItem1.Size = New Size(200, 6)
        ' 
        ' ExitToolStripMenuItem
        ' 
        ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        ExitToolStripMenuItem.ShortcutKeyDisplayString = ""
        ExitToolStripMenuItem.ShortcutKeys = Keys.Alt Or Keys.F4
        ExitToolStripMenuItem.Size = New Size(203, 22)
        ExitToolStripMenuItem.Text = "&Exit"
        ' 
        ' PanelEditor
        ' 
        PanelEditor.Controls.Add(GroupBox5)
        PanelEditor.Controls.Add(GroupBox6)
        PanelEditor.Controls.Add(LabelDayBudget)
        PanelEditor.Location = New Point(648, 27)
        PanelEditor.Name = "PanelEditor"
        PanelEditor.Size = New Size(608, 639)
        PanelEditor.TabIndex = 20
        PanelEditor.Tag = "590, 840"
        ' 
        ' GroupBox5
        ' 
        GroupBox5.Controls.Add(MonthsPanel)
        GroupBox5.Location = New Point(12, 67)
        GroupBox5.Name = "GroupBox5"
        GroupBox5.Size = New Size(584, 533)
        GroupBox5.TabIndex = 38
        GroupBox5.TabStop = False
        ' 
        ' MonthsPanel
        ' 
        MonthsPanel.AutoScroll = True
        MonthsPanel.BackColor = SystemColors.Control
        MonthsPanel.Controls.Add(LabelTitleGregorian)
        MonthsPanel.Controls.Add(LabelLeap)
        MonthsPanel.Controls.Add(LabelDays)
        MonthsPanel.Controls.Add(LabelMonthName)
        MonthsPanel.Controls.Add(ButtonAddMonth)
        MonthsPanel.Location = New Point(9, 16)
        MonthsPanel.Name = "MonthsPanel"
        MonthsPanel.Size = New Size(565, 511)
        MonthsPanel.TabIndex = 35
        ' 
        ' LabelTitleGregorian
        ' 
        LabelTitleGregorian.AutoSize = True
        LabelTitleGregorian.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelTitleGregorian.Location = New Point(439, 8)
        LabelTitleGregorian.Name = "LabelTitleGregorian"
        LabelTitleGregorian.Size = New Size(61, 15)
        LabelTitleGregorian.TabIndex = 25
        LabelTitleGregorian.Text = "Begins on"
        LabelTitleGregorian.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabelLeap
        ' 
        LabelLeap.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelLeap.Location = New Point(336, 8)
        LabelLeap.Name = "LabelLeap"
        LabelLeap.Size = New Size(86, 15)
        LabelLeap.TabIndex = 24
        LabelLeap.Text = "Leap Day"
        LabelLeap.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabelDays
        ' 
        LabelDays.AutoSize = True
        LabelDays.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelDays.Location = New Point(258, 8)
        LabelDays.Name = "LabelDays"
        LabelDays.Size = New Size(33, 15)
        LabelDays.TabIndex = 21
        LabelDays.Text = "Days"
        LabelDays.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' LabelMonthName
        ' 
        LabelMonthName.AutoSize = True
        LabelMonthName.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LabelMonthName.Location = New Point(95, 8)
        LabelMonthName.Name = "LabelMonthName"
        LabelMonthName.Size = New Size(78, 15)
        LabelMonthName.TabIndex = 18
        LabelMonthName.Text = "Month name"
        LabelMonthName.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ButtonAddMonth
        ' 
        ButtonAddMonth.Location = New Point(95, 33)
        ButtonAddMonth.Name = "ButtonAddMonth"
        ButtonAddMonth.Size = New Size(147, 23)
        ButtonAddMonth.TabIndex = 16
        ButtonAddMonth.Text = "&Add a month"
        ButtonAddMonth.UseVisualStyleBackColor = True
        ' 
        ' GroupBox6
        ' 
        GroupBox6.Controls.Add(LabelDayNumber)
        GroupBox6.Controls.Add(ComboCycleDay)
        GroupBox6.Controls.Add(ComboCycleMonth)
        GroupBox6.Location = New Point(12, 10)
        GroupBox6.Name = "GroupBox6"
        GroupBox6.Size = New Size(584, 51)
        GroupBox6.TabIndex = 37
        GroupBox6.TabStop = False
        ' 
        ' LabelDayNumber
        ' 
        LabelDayNumber.AutoSize = True
        LabelDayNumber.Location = New Point(197, 21)
        LabelDayNumber.Name = "LabelDayNumber"
        LabelDayNumber.Size = New Size(41, 15)
        LabelDayNumber.TabIndex = 2
        LabelDayNumber.Text = "Label1"
        ' 
        ' ComboCycleDay
        ' 
        ComboCycleDay.FormattingEnabled = True
        ComboCycleDay.Location = New Point(135, 18)
        ComboCycleDay.Name = "ComboCycleDay"
        ComboCycleDay.Size = New Size(56, 23)
        ComboCycleDay.TabIndex = 1
        ' 
        ' ComboCycleMonth
        ' 
        ComboCycleMonth.FormattingEnabled = True
        ComboCycleMonth.Location = New Point(8, 18)
        ComboCycleMonth.Name = "ComboCycleMonth"
        ComboCycleMonth.Size = New Size(121, 23)
        ComboCycleMonth.TabIndex = 0
        ' 
        ' LabelDayBudget
        ' 
        LabelDayBudget.BorderStyle = BorderStyle.Fixed3D
        LabelDayBudget.FlatStyle = FlatStyle.Popup
        LabelDayBudget.Location = New Point(12, 606)
        LabelDayBudget.Name = "LabelDayBudget"
        LabelDayBudget.Size = New Size(584, 23)
        LabelDayBudget.TabIndex = 36
        LabelDayBudget.Text = "Days to distribute:"
        LabelDayBudget.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' PanelCalendar
        ' 
        PanelCalendar.Controls.Add(GroupBox1)
        PanelCalendar.Controls.Add(GroupBox4)
        PanelCalendar.Controls.Add(GroupBox3)
        PanelCalendar.Controls.Add(GroupBox2)
        PanelCalendar.Location = New Point(0, 27)
        PanelCalendar.Name = "PanelCalendar"
        PanelCalendar.Size = New Size(608, 757)
        PanelCalendar.TabIndex = 21
        PanelCalendar.Tag = "size 590. 330"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckStartToday)
        GroupBox1.Controls.Add(ComboStartDay)
        GroupBox1.Controls.Add(ComboStartMonth)
        GroupBox1.Controls.Add(ComboStartYear)
        GroupBox1.Location = New Point(12, 6)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(584, 51)
        GroupBox1.TabIndex = 14
        GroupBox1.TabStop = False
        ' 
        ' CheckStartToday
        ' 
        CheckStartToday.AutoSize = True
        CheckStartToday.Location = New Point(275, 19)
        CheckStartToday.Name = "CheckStartToday"
        CheckStartToday.Size = New Size(15, 14)
        CheckStartToday.TabIndex = 3
        CheckStartToday.UseVisualStyleBackColor = True
        ' 
        ' ComboStartDay
        ' 
        ComboStartDay.FormattingEnabled = True
        ComboStartDay.Location = New Point(213, 17)
        ComboStartDay.Name = "ComboStartDay"
        ComboStartDay.Size = New Size(56, 23)
        ComboStartDay.TabIndex = 2
        ' 
        ' ComboStartMonth
        ' 
        ComboStartMonth.FormattingEnabled = True
        ComboStartMonth.Location = New Point(86, 17)
        ComboStartMonth.Name = "ComboStartMonth"
        ComboStartMonth.Size = New Size(121, 23)
        ComboStartMonth.TabIndex = 1
        ' 
        ' ComboStartYear
        ' 
        ComboStartYear.FormattingEnabled = True
        ComboStartYear.Location = New Point(9, 17)
        ComboStartYear.Name = "ComboStartYear"
        ComboStartYear.Size = New Size(71, 23)
        ComboStartYear.TabIndex = 0
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(LabelYearDetails)
        GroupBox4.Controls.Add(ProgressBarYear)
        GroupBox4.Location = New Point(12, 569)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(584, 63)
        GroupBox4.TabIndex = 13
        GroupBox4.TabStop = False
        ' 
        ' LabelYearDetails
        ' 
        LabelYearDetails.Location = New Point(6, 19)
        LabelYearDetails.Name = "LabelYearDetails"
        LabelYearDetails.Size = New Size(572, 19)
        LabelYearDetails.TabIndex = 1
        LabelYearDetails.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' ProgressBarYear
        ' 
        ProgressBarYear.Location = New Point(6, 41)
        ProgressBarYear.Maximum = 366
        ProgressBarYear.Minimum = 1
        ProgressBarYear.Name = "ProgressBarYear"
        ProgressBarYear.Size = New Size(572, 16)
        ProgressBarYear.Style = ProgressBarStyle.Continuous
        ProgressBarYear.TabIndex = 0
        ProgressBarYear.Value = 1
        ProgressBarYear.Visible = False
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(RichWeekNotes)
        GroupBox3.Controls.Add(ComboWeekSelect)
        GroupBox3.Controls.Add(LabelCalendarDate)
        GroupBox3.Location = New Point(12, 342)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(584, 221)
        GroupBox3.TabIndex = 12
        GroupBox3.TabStop = False
        ' 
        ' RichWeekNotes
        ' 
        RichWeekNotes.AcceptsTab = True
        RichWeekNotes.Location = New Point(6, 49)
        RichWeekNotes.Name = "RichWeekNotes"
        RichWeekNotes.Size = New Size(572, 165)
        RichWeekNotes.TabIndex = 3
        RichWeekNotes.TabStop = False
        RichWeekNotes.Text = ""
        ' 
        ' ComboWeekSelect
        ' 
        ComboWeekSelect.FormattingEnabled = True
        ComboWeekSelect.Location = New Point(6, 20)
        ComboWeekSelect.Name = "ComboWeekSelect"
        ComboWeekSelect.Size = New Size(74, 23)
        ComboWeekSelect.TabIndex = 2
        ' 
        ' LabelCalendarDate
        ' 
        LabelCalendarDate.AutoSize = True
        LabelCalendarDate.Location = New Point(86, 23)
        LabelCalendarDate.Name = "LabelCalendarDate"
        LabelCalendarDate.Size = New Size(41, 15)
        LabelCalendarDate.TabIndex = 1
        LabelCalendarDate.Text = "Label1"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(RichCurrentDates)
        GroupBox2.Location = New Point(12, 63)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(584, 273)
        GroupBox2.TabIndex = 11
        GroupBox2.TabStop = False
        ' 
        ' RichCurrentDates
        ' 
        RichCurrentDates.BorderStyle = BorderStyle.None
        RichCurrentDates.Location = New Point(13, 22)
        RichCurrentDates.Name = "RichCurrentDates"
        RichCurrentDates.ReadOnly = True
        RichCurrentDates.Size = New Size(565, 245)
        RichCurrentDates.TabIndex = 0
        RichCurrentDates.TabStop = False
        RichCurrentDates.Text = ""
        ' 
        ' MainTimer
        ' 
        MainTimer.Interval = 10
        ' 
        ' MainViewForm
        ' 
        AllowDrop = True
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1562, 879)
        Controls.Add(PanelCalendar)
        Controls.Add(PanelEditor)
        Controls.Add(MenuStrip1)
        FormBorderStyle = FormBorderStyle.Fixed3D
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        MaximizeBox = False
        Name = "MainViewForm"
        ShowIcon = False
        StartPosition = FormStartPosition.CenterScreen
        Text = "Calendar Designer"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        PanelEditor.ResumeLayout(False)
        GroupBox5.ResumeLayout(False)
        MonthsPanel.ResumeLayout(False)
        MonthsPanel.PerformLayout()
        GroupBox6.ResumeLayout(False)
        GroupBox6.PerformLayout()
        PanelCalendar.ResumeLayout(False)
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox2.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MenuViewCalendar As ToolStripMenuItem
    Friend WithEvents MenuViewEditor As ToolStripMenuItem
    Friend WithEvents PanelEditor As Panel
    Friend WithEvents LabelDayBudget As Label
    Friend WithEvents MonthsPanel As Panel
    Friend WithEvents LabelTitleGregorian As Label
    Friend WithEvents LabelLeap As Label
    Friend WithEvents LabelDays As Label
    Friend WithEvents LabelMonthName As Label
    Friend WithEvents ButtonAddMonth As Button
    Friend WithEvents PanelCalendar As Panel
    Friend WithEvents MainTimer As Timer
    Friend WithEvents SetAsDefaultToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents LanguageToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents EnglishToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SpanishToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripSeparator
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem

    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RichCurrentDates As RichTextBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents LabelCalendarDate As Label
    Friend WithEvents ComboWeekSelect As ComboBox
    Friend WithEvents RichWeekNotes As RichTextBox
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents LabelDayNumber As Label
    Friend WithEvents ComboCycleDay As ComboBox
    Friend WithEvents ComboCycleMonth As ComboBox
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents LabelYearDetails As Label
    Friend WithEvents ProgressBarYear As ProgressBar
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ComboStartDay As ComboBox
    Friend WithEvents ComboStartMonth As ComboBox
    Friend WithEvents ComboStartYear As ComboBox
    Friend WithEvents CheckStartToday As CheckBox
    Friend WithEvents RecentToolStripMenuItem As ToolStripMenuItem


End Class
