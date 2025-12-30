'Copyright (C) 2025 Irramárrima
'
'This file is part of Intercalarist.
'
'Intercalarist is free software: you can redistribute it and/or modify
'it under the terms of the GNU General Public License as published by
'the Free Software Foundation, either version 3 of the License, or
'(at your option) any later version.
'
'Intercalarist is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU General Public License for more details.
'
'You should have received a copy of the GNU General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.
'
Imports System.Globalization
Imports System.IO
Imports System.Net.Quic

Public Class MainViewForm

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load messages in the available languages
        Load_LanguageMessages()

        'Position and visibility of panels, menu stuff
        SetupEnvironment()

        'Cycle start list of months, both editor and calendar
        'and also add years (20 years from now to 20 years in the future)
        Load_Months()
        'Set up the start date stuff; month was updated in the previous sub
        ComboCycleDay.SelectedIndex = 0
        ComboStartYear.SelectedIndex = (MaxDisplayYears / 2) - 1
        ComboStartDay.SelectedIndex = MainOriginalDay - 1
        CheckStartToday.Checked = True

        'Check the default calendar in the file, if there is one
        LoadInitCfg()

        'Just in case nothing was opened
        UpdateRecentFiles()

        RefreshLeapDay()
        RefreshDayBudget()
        RefreshTabIndeces()
        RefreshWeekComments()

        'Timer in charge of updating the calendar.
        MainTimer.Enabled = True

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        End
    End Sub

    Private Sub ButtonAddMonth_Click(sender As Object, e As EventArgs) Handles ButtonAddMonth.Click
        CreateMonth()
        RefreshLeapDay()
        RefreshDayBudget()
        RefreshTabIndeces()
        RefreshLabelCalendarDate()
    End Sub

    Private Sub EditorToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuViewEditor.Click
        MenuViewEditor.Checked = True
        MenuViewCalendar.Checked = False
        UpdateEnvironmentBounds()
    End Sub

    Private Sub CalendarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MenuViewCalendar.Click
        MenuViewCalendar.Checked = True
        MenuViewEditor.Checked = False
        UpdateEnvironmentBounds()
    End Sub

    Private Sub MainTimer_Tick(sender As Object, e As EventArgs) Handles MainTimer.Tick

        If DayBudget = 0 Then
            RefreshDatesAndNotes()
            If ProgressBarYear.Visible = False Then
                ProgressBarYear.Visible = True
            End If
        Else    ' the calendar is broken and must be edited
            LabelYearDetails.Text = ""
            ProgressBarYear.Value = ProgressBarYear.Minimum
            ProgressBarYear.Visible = False
            RichCurrentDates.Rtf = ""
            RichCurrentDates.Text = TextsInLanguage(26, CurrentLanguage)
        End If

    End Sub

    Private Sub NewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewToolStripMenuItem.Click

        MonthsPanel.AutoScroll = False
        MonthsPanel.SuspendLayout()

        CreateNewCalendar()
        RefreshLabelCalendarDate()

        MonthsPanel.PerformLayout()
        MonthsPanel.ResumeLayout()
        MonthsPanel.Refresh()
        MonthsPanel.AutoScroll = True

    End Sub

    Private Sub OpenToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OpenToolStripMenuItem.Click
        Dim openDialog As New OpenFileDialog()
        openDialog.Title = TextsInLanguage(29, CurrentLanguage)
        openDialog.Filter = TextsInLanguage(30, CurrentLanguage)
        openDialog.FilterIndex = 1

        If openDialog.ShowDialog() = DialogResult.OK Then
            QuickRemoveAll()
            If OpenCalendarFile(openDialog.FileName) Then
                SaveInitCfg(savingRecentFiles)
            End If
            RefreshWeekComments()
            RefreshLabelCalendarDate()
        End If
    End Sub

    Private Sub SaveToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        If TotalMonths > 0 And CurrentFile <> "" Then
            SaveCalendarFile(CurrentFile)
        ElseIf TotalMonths > 0 And CurrentFile = "" Then
            SaveAsToolStripMenuItem_Click(sender, e)
        Else
            MessageBox.Show(TextsInLanguage(31, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If TotalMonths > 0 Then
            Dim saveDialog As New SaveFileDialog()
            saveDialog.Title = TextsInLanguage(32, CurrentLanguage)
            saveDialog.Filter = TextsInLanguage(33, CurrentLanguage)
            saveDialog.FilterIndex = 1

            If saveDialog.ShowDialog() = DialogResult.OK Then
                CurrentFile = saveDialog.FileName
                SaveCalendarFile(CurrentFile)
            End If
        Else
            MessageBox.Show(TextsInLanguage(31, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub SetAsDefaultToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetAsDefaultToolStripMenuItem.Click
        If DayBudget = DaysPerYear Then
            MessageBox.Show(TextsInLanguage(31, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Else
            If String.IsNullOrWhiteSpace(CurrentFile) Then
                MessageBox.Show(TextsInLanguage(35, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Else
                SaveInitCfg(savingDefaultCalendar)
                UpdateRecentFiles()
            End If
        End If
    End Sub

    Private Sub EnglishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EnglishToolStripMenuItem.Click
        EnglishToolStripMenuItem.Checked = True
        SpanishToolStripMenuItem.Checked = False
        CurrentLanguage = lgEnglish
        RefreshStaticTexts()
        RefreshDayBudget()
        SaveInitCfg(savingDefaultLanguage)

    End Sub

    Private Sub SpanishToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpanishToolStripMenuItem.Click
        SpanishToolStripMenuItem.Checked = True
        EnglishToolStripMenuItem.Checked = False
        CurrentLanguage = lgSpanish
        RefreshStaticTexts()
        RefreshDayBudget()
        SaveInitCfg(savingDefaultLanguage)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dialog1.ShowDialog()
    End Sub

    Private Sub ComboWeekSelect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboWeekSelect.SelectedIndexChanged
        weekSuspendChangeEvent = True 'This makes sure the "textchanged" doesn't change the stored comments
        ReadingEditorWeek = ComboWeekSelect.SelectedIndex
        RefreshWeekComments()
        RefreshLabelCalendarDate()
        weekSuspendChangeEvent = False
    End Sub

    Private Sub RichWeekNotes_TextChanged(sender As Object, e As EventArgs) Handles RichWeekNotes.TextChanged
        If weekSuspendChangeEvent = False Then
            If String.IsNullOrWhiteSpace(RichWeekNotes.Text) = False Then
                CommentsWeek(ComboWeekSelect.SelectedIndex) = RichWeekNotes.Rtf
            Else
                CommentsWeek(ComboWeekSelect.SelectedIndex) = ""
            End If
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboCycleMonth.SelectedIndexChanged
        Editor_Refresh_MonthDays()
        RefreshGregorianDays()
        RefreshLabelCalendarDate()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboCycleDay.SelectedIndexChanged

        Dim SelectedDate = New DateTime(YearOfReference, Val(ComboCycleMonth.SelectedIndex) + 1, Val(ComboCycleDay.SelectedIndex) + 1)

        CycleStartDay = SelectedDate.DayOfYear
        '"Gregorian ordinal"
        LabelDayNumber.Text = TextsInLanguage(16, CurrentLanguage) + ": " + CycleStartDay.ToString()

        RefreshGregorianDays()
        RefreshLabelCalendarDate()

    End Sub

    Private Sub ComboWeekSelect_MouseWheel(sender As Object, e As MouseEventArgs) Handles ComboWeekSelect.MouseWheel

        If (Control.ModifierKeys And Keys.Control) = Keys.Control Then

            If e.Delta > 0 Then 'wheel up
                Dim startingIndex As Integer = ComboWeekSelect.SelectedIndex
                Dim iFound As Boolean = False
                For i = ComboWeekSelect.SelectedIndex - 1 To 0 Step -1
                    If String.IsNullOrWhiteSpace(CommentsWeek(i)) = False Then
                        ComboWeekSelect.SelectedIndex = i
                        iFound = True
                        Exit For
                    End If
                Next
                ' Wrap around
                If iFound = False Then
                    For i = 52 To startingIndex + 1 Step -1
                        If String.IsNullOrWhiteSpace(CommentsWeek(i)) = False Then
                            ComboWeekSelect.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
            Else 'wheel down
                Dim startingIndex As Integer = ComboWeekSelect.SelectedIndex
                Dim iFound As Boolean = False
                For i = ComboWeekSelect.SelectedIndex + 1 To 52
                    If String.IsNullOrWhiteSpace(CommentsWeek(i)) = False Then
                        ComboWeekSelect.SelectedIndex = i
                        iFound = True
                        Exit For
                    End If
                Next
                ' Wrap around
                If iFound = False Then
                    For i = 0 To startingIndex - 1
                        If String.IsNullOrWhiteSpace(CommentsWeek(i)) = False Then
                            ComboWeekSelect.SelectedIndex = i
                            Exit For
                        End If
                    Next
                End If
            End If
        End If

    End Sub

    Private Sub ComboStartYear_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboStartYear.SelectedIndexChanged
        MainDateYear = Val(ComboStartYear.Text)
        Calendar_Refresh_MonthDays() 'Do not postpone this or it'll crash!
        Dim tempDate As New DateTime(MainDateYear, MainDateMonth, MainDateDay)
        MainDateDayOfYear = tempDate.DayOfYear
        CheckStartToday.Checked = (MainOriginalDate = tempDate)
    End Sub

    Private Sub ComboStartMonth_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboStartMonth.SelectedIndexChanged

        If combostartMonthSuspendChangeEvent And ComboStartMonth.SelectedIndex <> combostartObligatoryMonthValue Then
            ComboStartMonth.SelectedIndex = combostartObligatoryMonthValue
            combostartMonthSuspendChangeEvent = False
        End If

        MainDateMonth = ComboStartMonth.SelectedIndex + 1
        Calendar_Refresh_MonthDays()
    End Sub

    Private Sub ComboStartDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboStartDay.SelectedIndexChanged

        If combostartSuspendChangeEvent And ComboStartDay.SelectedIndex <> combostartObligatoryDayValue Then
            ComboStartDay.SelectedIndex = combostartObligatoryDayValue
            combostartSuspendChangeEvent = False
        End If


        MainDateDay = ComboStartDay.SelectedIndex + 1
        Dim tempDate As New DateTime(MainDateYear, MainDateMonth, MainDateDay)
        MainDateDayOfYear = tempDate.DayOfYear

        CheckStartToday.Checked = (MainOriginalDate = tempDate)

    End Sub

    Private Sub CheckStartToday_Click(sender As Object, e As EventArgs) Handles CheckStartToday.Click
        If MainDateYear <> MainOriginalYear Or MainDateMonth <> MainOriginalMonth Or MainDateDayOfYear <> MainOriginalDayOfYear Then
            ComboStartYear.SelectedIndex = ComboStartYear.SelectedIndex + MainOriginalYear - MainDateYear
            ComboStartMonth.SelectedIndex = MainOriginalMonth - 1
            ComboStartDay.SelectedIndex = MainOriginalDay - 1
        End If
        CheckStartToday.Checked = True
    End Sub

    Private Sub GroupBox1_Click(sender As Object, e As EventArgs) Handles GroupBox1.Click
        RefreshGroupBoxCollapse(sender, e)
    End Sub

    Private Sub GroupBox2_Click(sender As Object, e As EventArgs) Handles GroupBox2.Click
        RefreshGroupBoxCollapse(sender, e)
    End Sub

    Private Sub GroupBox3_Click(sender As Object, e As EventArgs) Handles GroupBox3.Click
        RefreshGroupBoxCollapse(sender, e)
    End Sub

    Private Sub GroupBox4_Click(sender As Object, e As EventArgs) Handles GroupBox4.Click
        RefreshGroupBoxCollapse(sender, e)
    End Sub

    Private Sub ComboStartDay_MouseWheel(sender As Object, e As MouseEventArgs) Handles ComboStartDay.MouseWheel

        If e.Delta > 0 Then 'wheel up
            ProcessComboStartDayLimits(e.Delta)
        Else 'wheel down
            ProcessComboStartDayLimits(e.Delta)
        End If

    End Sub

    Private Sub ComboStartDay_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboStartDay.KeyDown
        If e.KeyCode = Keys.Up Then
            ProcessComboStartDayLimits(1)
        ElseIf e.KeyCode = Keys.Down Then
            ProcessComboStartDayLimits(-1)
        End If
    End Sub

    Private Sub ComboStartMonth_MouseWheel(sender As Object, e As MouseEventArgs) Handles ComboStartMonth.MouseWheel
        ProcessComboStartMonthLimits(e.Delta)
    End Sub

    Private Sub ComboStartMonth_KeyDown(sender As Object, e As KeyEventArgs) Handles ComboStartMonth.KeyDown
        If e.KeyCode = Keys.Up Then
            ProcessComboStartMonthLimits(1)
        ElseIf e.KeyCode = Keys.Down Then
            ProcessComboStartMonthLimits(-1)
        End If
    End Sub
End Class