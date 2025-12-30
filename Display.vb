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
Imports System.Windows.Forms
Module Display

    'This module contains all subs in charge of arranging and modifying controls;
    'and displaying information.

    'For the today checkbox
    Public Const MaxDisplayYears As Integer = 100
    Public MainOriginalYear As Integer = MainDateYear
    Public MainOriginalMonth As Integer = MainDateMonth
    Public MainOriginalDay As Integer = MainDateDay
    Public MainOriginalDayOfYear As Integer = MainDateDayOfYear
    Public MainOriginalDate As New DateTime(MainOriginalYear, MainOriginalMonth, MainOriginalDay)
    Public NumberOfCalendarCollapsibleGroupboxes As Integer = 4

    Public Const listItemsToShow As Integer = 15

    Public TotalMonths As Integer = 0
    Public CycleStartDay As Integer = 1
    Public Const DaysPerYear As Integer = 365
    Public DayBudget As Integer = DaysPerYear

    'This NON-LEAP year of reference is used for some specific calculations.
    'If another is picked, make sure neither the previous nor following year are leap years either.
    Public Const YearOfReference As Integer = 2010
    Public combostartSuspendChangeEvent As Boolean = False
    Public combostartObligatoryDayValue As Integer
    Public combostartMonthSuspendChangeEvent As Boolean = False
    Public combostartObligatoryMonthValue As Integer

    Public thereNoNotes As Boolean = False
    Public ReadingEditorWeek As Integer = 0
    Public showingFirstTime As Boolean = True
    Public activeWeekComments As Integer = 0

    'the year has 52.14... weeks = 53
    Public CommentsWeek(52) As String
    Public weekSuspendChangeEvent As Boolean = False

    Public Const ButtonAddSpace As Integer = 30

    Public Sub RefreshTabIndeces()
        Dim containerControl As Panel = MainViewForm.MonthsPanel
        Dim tempI As Integer = 1
        For Each ctrl As Control In containerControl.Controls
            If TypeOf ctrl Is TextBox Then
                ctrl.TabIndex = tempI
                tempI += 1
            End If
        Next

        For Each ctrlB As Control In containerControl.Controls
            If TypeOf ctrlB Is NumericUpDown Then
                ctrlB.TabIndex = tempI
                tempI += 1
            End If
        Next

        For Each ctrlC As Control In containerControl.Controls
            If TypeOf ctrlC Is Button Then
                ctrlC.TabIndex = tempI
                tempI += 1
            End If
        Next

    End Sub

    Public Sub RefreshLeapDay()

        If TotalMonths > 0 Then
            Dim thereIsMarked As Boolean = False
            Dim whichToCheck As Integer = 0
            For i = 1 To TotalMonths
                If storedMonths(i, storedLeap) = True Then
                    thereIsMarked = True
                    whichToCheck = i
                    Exit For
                End If
            Next

            If thereIsMarked = False Then
                whichToCheck = 1
                storedMonths(whichToCheck, storedLeap) = True
            End If

            Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find("OptionLeapDay" + whichToCheck.ToString(), False)
            Dim foundControl As Control = foundControls(0)
            Dim tempIsLeap As RadioButton = DirectCast(foundControl, RadioButton)
            tempIsLeap.Checked = storedMonths(whichToCheck, storedLeap)
        End If
    End Sub

    Public Sub UpdateRecentFiles(Optional ByVal filenameToInclude As String = "")

        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        'This sub is called when a file is opened successfully.
        'The submenus are handled by RECENTFILECLICKED

        'You have to call saveinitcfg manually after calling this sub!

        'if no argument is passed, the sub updates things viz. the menus

        If filenameToInclude <> "" Then
            'Move all the files down, and add this one at the top
            For i = MaxRecentFiles To 2 Step -1
                storedRecentFiles(i) = storedRecentFiles(i - 1)
            Next
            storedRecentFiles(1) = filenameToInclude
        End If

        'Make sure there are no repeated entries
        'Clean
        For i = 1 To MaxRecentFiles - 1
            For j = i + 1 To MaxRecentFiles
                If storedRecentFiles(j) = storedRecentFiles(i) Then
                    storedRecentFiles(j) = ""
                End If
            Next
        Next
        'Reorder
        For i = 1 To MaxRecentFiles - 1
            If storedRecentFiles(i) = "" Then
                For j = i + 1 To MaxRecentFiles
                    If storedRecentFiles(j) <> "" Then
                        storedRecentFiles(i) = storedRecentFiles(j)
                        storedRecentFiles(j) = ""
                        Exit For
                    End If
                Next
            End If
        Next

        'Count the items
        Dim totalItems As Integer = 0
        For i = 1 To MaxRecentFiles
            If storedRecentFiles(i) <> "" Then
                totalItems += 1
            Else
                ' Should be ordered by now!
                Exit For
            End If
        Next

        'Work on the submenus for recent
        'Find out how many menus there are and add or remove as necessary
        Dim totalSubmenus As Integer = 0
        For i = 1 To MaxRecentFiles
            If MainViewForm.RecentToolStripMenuItem.DropDownItems.ContainsKey("SubmenuRecent" & i.ToString()) Then
                totalSubmenus += 1
            End If
        Next

        If totalItems > totalSubmenus Then
            'not enough submenus
            For i = totalSubmenus + 1 To totalItems
                Dim newSubmenu As New ToolStripMenuItem("")
                newSubmenu.Name = "SubmenuRecent" & i.ToString()
                MainViewForm.RecentToolStripMenuItem.DropDownItems.Add(newSubmenu)
                AddHandler newSubmenu.Click, AddressOf RecentFileClicked
            Next
        ElseIf totalItems < totalSubmenus Then
            'too many submenus, remove
            For i = totalSubmenus To totalItems + 1 Step -1
                Dim removingSubmenu = MainViewForm.RecentToolStripMenuItem.DropDownItems(i - 1)
                RemoveHandler removingSubmenu.Click, AddressOf RecentFileClicked
                MainViewForm.RecentToolStripMenuItem.DropDownItems.Remove(removingSubmenu)
                removingSubmenu.Dispose()
            Next
        End If

        'Rename the submenus based on storedrecent
        If totalItems > 0 Then
            For i = 1 To totalItems
                Dim renamingSubmenu = DirectCast(MainViewForm.RecentToolStripMenuItem.DropDownItems("SubmenuRecent" & i.ToString), ToolStripMenuItem)
                Dim iAdd As String = ""
                If storedRecentFiles(i) = loadedDefaultCalendar Then
                    iAdd = TextsInLanguage(66, CurrentLanguage)
                End If
                renamingSubmenu.Text = i.ToString & ". " & storedRecentFiles(i) & iAdd
            Next
            MainViewForm.RecentToolStripMenuItem.Enabled = True
        Else
            MainViewForm.RecentToolStripMenuItem.Enabled = False
        End If

    End Sub

    Public Sub RecentFileClicked(sender As Object, e As EventArgs)
        Dim clickedSubmenu = DirectCast(sender, ToolStripMenuItem)
        Dim iWhich As Integer = Int(clickedSubmenu.Name.Substring("SubmenuRecent".Length, clickedSubmenu.Name.Length - "SubmenuRecent".Length))

        'Dim iChanged As Boolean = False
        QuickRemoveAll()
        If OpenCalendarFile(storedRecentFiles(iWhich)) = False Then
            storedRecentFiles(iWhich) = ""
            'iChanged = True
        End If

        RefreshWeekComments()
        RefreshLabelCalendarDate()
        UpdateRecentFiles()
        'If iWhich > 1 Or iChanged Then
        'UpdateRecentFiles()
        SaveInitCfg(savingRecentFiles)
        'End If
    End Sub

    Public Sub MakeGroupBoxCollapse(ByVal iWhich As Integer)
        Dim foundControls() As Control = MainViewForm.PanelCalendar.Controls.Find("GroupBox" + iWhich.ToString(), False)
        Dim foundControl As Control = foundControls(0)
        Dim tempCollapsing As GroupBox = DirectCast(foundControl, GroupBox)
        tempCollapsing.Height = 24
        tempCollapsing.Text = TextsInLanguage(18 + iWhich, CurrentLanguage) & TextsInLanguage(58, CurrentLanguage)

        For Each ctrl As Control In tempCollapsing.Controls
            ctrl.Visible = False
        Next

        For j As Integer = 1 To 4
            If j > iWhich Then
                Dim foundControlsB() As Control = MainViewForm.PanelCalendar.Controls.Find("GroupBox" + j.ToString(), False)
                Dim foundControlB As Control = foundControlsB(0)
                Dim movingGroupBox As GroupBox = DirectCast(foundControlB, GroupBox)
                movingGroupBox.Top -= Val(tempCollapsing.Tag) - tempCollapsing.Height
            End If
        Next
    End Sub

    Public Sub RefreshGroupBoxCollapse(sender As Object, e As EventArgs)

        Dim tempControl As GroupBox = DirectCast(sender, GroupBox)
        Dim whichClicked As Integer = Int(tempControl.Name.Substring("GroupBox".Length, tempControl.Name.Length - "GroupBox".Length))

        If tempControl.Height = Val(tempControl.Tag) Then
            tempControl.Height = 24
            tempControl.Text += TextsInLanguage(58, CurrentLanguage)
            For i As Integer = 1 To 4
                If i > whichClicked Then
                    Dim foundControls() As Control = MainViewForm.PanelCalendar.Controls.Find("GroupBox" + i.ToString(), False)
                    Dim foundControl As Control = foundControls(0)
                    Dim movingGroupBox As GroupBox = DirectCast(foundControl, GroupBox)
                    movingGroupBox.Top -= Val(tempControl.Tag) - tempControl.Height
                End If
            Next
        Else
            tempControl.Height = Val(tempControl.Tag)
            tempControl.Text = TextsInLanguage(19 + whichClicked - 1, CurrentLanguage)
            For i As Integer = 1 To 4
                If i > whichClicked Then
                    Dim foundControls() As Control = MainViewForm.PanelCalendar.Controls.Find("GroupBox" + i.ToString(), False)
                    Dim foundControl As Control = foundControls(0)
                    Dim movingGroupBox As GroupBox = DirectCast(foundControl, GroupBox)
                    movingGroupBox.Top += tempControl.Height - 24
                End If
            Next
        End If

        For Each ctrl As Control In tempControl.Controls
            If tempControl.Height < Val(tempControl.Tag) Then
                ctrl.Visible = False
            Else
                ctrl.Visible = True
            End If
        Next

        UpdateEnvironmentBounds()
        SaveInitCfg(savingCollapsedGroupboxes)

    End Sub

    Public Sub SetupEnvironment()

        MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)

        For i = 1 To 53
            MainViewForm.ComboWeekSelect.Items.Add(i.ToString())
        Next
        MainViewForm.ComboWeekSelect.SelectedIndex = 0

        MainViewForm.GroupBox1.Tag = MainViewForm.GroupBox1.Height
        MainViewForm.GroupBox2.Tag = MainViewForm.GroupBox2.Height
        MainViewForm.GroupBox3.Tag = MainViewForm.GroupBox3.Height
        MainViewForm.GroupBox6.Tag = MainViewForm.GroupBox6.Height
        MainViewForm.GroupBox4.Tag = MainViewForm.GroupBox4.Height
        MainViewForm.GroupBox5.Tag = MainViewForm.GroupBox5.Height

        MainViewForm.MenuViewEditor.Checked = False
        MainViewForm.MenuViewCalendar.Checked = Not MainViewForm.MenuViewEditor.Checked
        UpdateEnvironmentBounds()

    End Sub

    Public Sub UpdateEnvironmentBounds()

        MainViewForm.PanelEditor.Left = 0
        MainViewForm.PanelEditor.Top = MainViewForm.MenuStrip1.Height

        MainViewForm.PanelCalendar.Left = 0
        MainViewForm.PanelCalendar.Top = MainViewForm.PanelEditor.Top
        MainViewForm.PanelCalendar.Height = MainViewForm.GroupBox4.Top + MainViewForm.GroupBox4.Height + 15

        MainViewForm.PanelCalendar.Visible = MainViewForm.MenuViewCalendar.Checked
        MainViewForm.PanelEditor.Visible = MainViewForm.MenuViewEditor.Checked

        If MainViewForm.MenuViewEditor.Checked = True Then
            MainViewForm.Width = MainViewForm.PanelEditor.Width + 16
            MainViewForm.Height = MainViewForm.PanelEditor.Height + MainViewForm.MenuStrip1.Height + 40
        Else
            MainViewForm.Width = MainViewForm.PanelCalendar.Width + 16
            MainViewForm.Height = MainViewForm.PanelCalendar.Height + MainViewForm.MenuStrip1.Height + 40
        End If

    End Sub

    Public Sub RefreshDayBudget()

        Dim totalSum As Integer = 0

        For i = 1 To TotalMonths
            totalSum += storedMonths(i, storedDay)
        Next

        DayBudget = DaysPerYear - totalSum

        '"Days to distribute: "
        MainViewForm.LabelDayBudget.Text = TextsInLanguage(2, CurrentLanguage) + DayBudget.ToString() & "/" & DaysPerYear

        If DayBudget > 0 And DayBudget < DaysPerYear Then
            MainViewForm.LabelDayBudget.ForeColor = System.Drawing.SystemColors.ControlText
            MainViewForm.LabelDayBudget.BackColor = System.Drawing.SystemColors.Control
        ElseIf DayBudget = 0 Then
            MainViewForm.LabelDayBudget.BackColor = Color.DarkGreen
            MainViewForm.LabelDayBudget.ForeColor = Color.White
        Else
            MainViewForm.LabelDayBudget.BackColor = Color.DarkRed
            MainViewForm.LabelDayBudget.ForeColor = Color.White
        End If

    End Sub

    Public Sub RefreshGregorianDays()
        Dim containerControl As Panel = MainViewForm.MonthsPanel
        Dim totalSum As Integer = CycleStartDay
        Dim monthCount = 0
        For Each ctrl As Control In containerControl.Controls
            If TypeOf ctrl Is NumericUpDown Then

                monthCount += 1

                For Each ctrlB As Control In containerControl.Controls
                    If TypeOf ctrlB Is Label Then
                        Dim tempLabel As Label = CType(ctrlB, Label)
                        If tempLabel.Name = "LabelGregorianDay" + monthCount.ToString() Then
                            tempLabel.Text = giveFormattedDate(totalSum)
                        End If
                    End If
                Next

                Dim numUpDown As NumericUpDown = CType(ctrl, NumericUpDown)
                totalSum += numUpDown.Value

            End If
        Next
    End Sub

    Public Sub Load_Months()

        For i = 1 To MaxDisplayYears
            MainViewForm.ComboStartYear.Items.Add(MainOriginalYear - (MaxDisplayYears / 2) + i)
        Next

        For i = 1 To 12
            MainViewForm.ComboCycleMonth.Items.Add(TextsInLanguage(2 + i, CurrentLanguage))
            MainViewForm.ComboStartMonth.Items.Add(TextsInLanguage(2 + i, CurrentLanguage))
        Next

        'These last statements each trigger an event that sets up the days in the other corresponding combobox
        MainViewForm.ComboCycleMonth.SelectedIndex = MainDateMonth - 1
        MainViewForm.ComboStartMonth.SelectedIndex = MainDateMonth - 1

    End Sub

    Public Sub Editor_Refresh_MonthDays()
        Dim OldSelectedIndex As Integer = Val(MainViewForm.ComboCycleDay.Text) - 1
        Dim MaxDays As Integer = 30

        Select Case MainViewForm.ComboCycleMonth.SelectedIndex
            Case 0, 2, 4, 6, 7, 9, 11
                MaxDays = 31
            Case 1
                MaxDays = 28
        End Select

        MainViewForm.ComboCycleDay.Items.Clear()
        For i = 1 To MaxDays
            MainViewForm.ComboCycleDay.Items.Add(i)
        Next

        If MainViewForm.ComboCycleDay.Items.Count - 1 < OldSelectedIndex Then
            MainViewForm.ComboCycleDay.SelectedIndex = MainViewForm.ComboCycleDay.Items.Count - 1
            If MainViewForm.ComboCycleDay.SelectedIndex < 0 Then
                MainViewForm.ComboCycleDay.SelectedIndex = 0
            End If
        Else
            MainViewForm.ComboCycleDay.SelectedIndex = OldSelectedIndex
        End If
    End Sub

    Public Sub DynamicRemoveMonth_Click(sender As Object, e As EventArgs)

        Dim clickedButton As Button = DirectCast(sender, Button)
        Dim RemovedMonth = clickedButton.Name.Substring("MonthRemove".Length, clickedButton.Name.Length - "MonthRemove".Length)

        'Remove the X button itself and its handler
        Dim targetControlB As Control = Nothing
        For Each ctrlB As Control In MainViewForm.MonthsPanel.Controls
            If ctrlB.Name = "MonthRemove" + TotalMonths.ToString() Then
                targetControlB = ctrlB
                Exit For
            End If
        Next
        RemoveHandler targetControlB.Click, AddressOf DynamicRemoveMonth_Click
        MainViewForm.MonthsPanel.Controls.Remove(targetControlB)
        targetControlB.Dispose()

        'Remove the down button and its handler
        Dim targetControlE As Control = Nothing
        For Each ctrlE As Control In MainViewForm.MonthsPanel.Controls
            If ctrlE.Name = "MonthMoveDown" + TotalMonths.ToString() Then
                targetControlE = ctrlE
                Exit For
            End If
        Next
        RemoveHandler targetControlE.Click, AddressOf DynamicMoveMonthDown
        MainViewForm.MonthsPanel.Controls.Remove(targetControlE)
        targetControlE.Dispose()

        'Remove the numUpDown and its handlers
        Dim targetControl As Control = Nothing
        For Each ctrl As Control In MainViewForm.MonthsPanel.Controls
            If ctrl.Name = "MonthDays" + TotalMonths.ToString() Then
                targetControl = ctrl
                Exit For
            End If
        Next
        Dim tempUpDown As NumericUpDown = CType(targetControl, NumericUpDown)
        RemoveHandler tempUpDown.ValueChanged, AddressOf DynamicDayValue_Changed
        RemoveHandler tempUpDown.GotFocus, AddressOf DynamicUpDown_GotFocus
        RemoveHandler tempUpDown.Click, AddressOf DynamicUpDown_GotFocus
        MainViewForm.MonthsPanel.Controls.Remove(tempUpDown)
        tempUpDown.Dispose()

        'Remove the MonthName and its handlers
        Dim targetControlC As Control = Nothing
        For Each ctrlC As Control In MainViewForm.MonthsPanel.Controls
            If ctrlC.Name = "MonthName" + TotalMonths.ToString() Then
                targetControlC = ctrlC
                Exit For
            End If
        Next
        Dim tempMonthName As TextBox = CType(targetControlC, TextBox)
        RemoveHandler tempMonthName.TextChanged, AddressOf DynamicMonthName_Changed
        RemoveHandler tempMonthName.GotFocus, AddressOf DynamicTextbox_GotFocus
        RemoveHandler tempMonthName.Click, AddressOf DynamicTextbox_GotFocus
        MainViewForm.MonthsPanel.Controls.Remove(tempMonthName)
        tempMonthName.Dispose()

        'Remove the MonthName and its handlers
        Dim targetControlD As Control = Nothing
        For Each ctrlD As Control In MainViewForm.MonthsPanel.Controls
            If ctrlD.Name = "OptionLeapDay" + TotalMonths.ToString() Then
                targetControlD = ctrlD
                Exit For
            End If
        Next
        Dim tempOptionLeap As RadioButton = CType(targetControlD, RadioButton)
        RemoveHandler tempOptionLeap.CheckedChanged, AddressOf DynamicLeapCheckedChanged
        MainViewForm.MonthsPanel.Controls.Remove(tempOptionLeap)
        tempOptionLeap.Dispose()

        'Remove the rest, without handlers
        PanelRemoveObject("LabelMonthNumber" + TotalMonths.ToString())
        PanelRemoveObject("LabelGregorianDay" + TotalMonths.ToString())

        RemoveStoredMonth(RemovedMonth)
        TotalMonths -= 1
        UpdateDynamicObjectsFromStoredMonths()

        RefreshLeapDay()
        RefreshDayBudget()
        RefreshGregorianDays()
        RefreshTabIndeces()
        RefreshLabelCalendarDate()

        MainViewForm.ButtonAddMonth.Top -= ButtonAddSpace

    End Sub

    Public Sub UpdateDynamicObjectsFromStoredMonths()
        For i = 1 To TotalMonths
            Dim foundControlsA() As Control = MainViewForm.MonthsPanel.Controls.Find("MonthName" + i.ToString(), False)
            Dim foundControlA As Control = foundControlsA(0)
            Dim tempMonthName As TextBox = DirectCast(foundControlA, TextBox)
            If tempMonthName.Text.Equals(storedMonths(i, storedName)) = False Then
                tempMonthName.Text = storedMonths(i, storedName)
            End If

            Dim foundControlsB() As Control = MainViewForm.MonthsPanel.Controls.Find("MonthDays" + i.ToString(), False)
            Dim foundControlB As Control = foundControlsB(0)
            Dim tempMonthDays As NumericUpDown = DirectCast(foundControlB, NumericUpDown)
            If tempMonthDays.Value.ToString().Equals(storedMonths(i, 1).ToString()) = False Then
                tempMonthDays.Value = storedMonths(i, 1)
            End If

            Dim foundControlsC() As Control = MainViewForm.MonthsPanel.Controls.Find("OptionLeapDay" + i.ToString(), False)
            Dim foundControlC As Control = foundControlsC(0)
            Dim tempIsLeap As RadioButton = DirectCast(foundControlC, RadioButton)
            If tempIsLeap.Checked <> storedMonths(i, storedLeap) Then
                tempIsLeap.Checked = storedMonths(i, storedLeap)
            End If
        Next
    End Sub

    Public Sub DynamicMonthName_Changed(sender As Object, e As EventArgs)

        Dim clickedDay As TextBox = DirectCast(sender, TextBox)
        storedMonths(clickedDay.Name.Substring("MonthName".Length, clickedDay.Name.Length - "MonthName".Length), storedName) = clickedDay.Text

        RefreshLabelCalendarDate()

    End Sub
    Public Sub PanelRemoveObject(ByVal controlName As String)
        'ONLY for controls WITHOUT handlers
        Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(controlName, False)
        MainViewForm.MonthsPanel.Controls.Remove(foundControls(0))
        foundControls(0).Dispose()
    End Sub

    'Public Sub ChangeControlName(ByVal oldName As String, ByVal newName As String)
    'This sub is ONLY called by controls WITHOUT handlers!
    'Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(oldName, False)
    'Dim targetControl As Control = foundControls(0)

    '   targetControl.Name = newName
    '    targetControl.Tag = Val(targetControl.Tag) - 1
    'End Sub

    'Public Sub ChangeRemoveName(ByVal oldName As String, ByVal newName As String)
    'This is because it's a button and the handler has to be removed and remade

    'Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(oldName, False)
    'Dim targetControl As Button = foundControls(0)

    'RemoveHandler() targetControl.Click, AddressOf DynamicRemoveMonth_Click
    '    targetControl.Name = newName
    '    targetControl.Tag = Val(targetControl.Tag) - 1
    '
    '   AddHandler() targetControl.Click, AddressOf DynamicRemoveMonth_Click
    '  End Sub

    'Public Sub ChangeUpDownName(ByVal oldName As String, ByVal newName As String)
    'This is because it's a numericupdown and the handlers have to be removed and remade

    'Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(oldName, False)
    'Dim targetControl As NumericUpDown = foundControls(0)

    'RemoveHandler() targetControl.ValueChanged, AddressOf DynamicDayValue_Changed
    'RemoveHandler() targetControl.GotFocus, AddressOf DynamicUpDown_GotFocus
    'RemoveHandler() targetControl.Click, AddressOf DynamicUpDown_GotFocus
    '   targetControl.Name = newName
    '  targetControl.Tag = Val(targetControl.Tag) - 1

    'AddHandler() targetControl.ValueChanged, AddressOf DynamicDayValue_Changed
    'AddHandler() targetControl.GotFocus, AddressOf DynamicUpDown_GotFocus
    'AddHandler() targetControl.Click, AddressOf DynamicUpDown_GotFocus
    'End Sub

    'Public Sub UpdateLabelNumber(ByVal labelName As String)
    'Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(labelName, False)
    'Dim targetControl As Control = foundControls(0)
    '   targetControl.Text = targetControl.Tag.ToString()
    'End Sub

    'Public Sub UpdateControlProperties(ByVal labelName As String)
    'Dim foundControls() As Control = MainViewForm.MonthsPanel.Controls.Find(labelName, False)
    'Dim targetControl As Control = foundControls(0)
    '   targetControl.Top -= ButtonAddSpace
    'End Sub

    Public Sub DynamicMoveMonthDown(sender As Object, e As EventArgs)
        Dim clickedDay As Button = DirectCast(sender, Button)
        Dim whichMoveDown As Integer = clickedDay.Name.Substring("MonthMoveDown".Length, clickedDay.Name.Length - "MonthMoveDown".Length)

        If whichMoveDown < TotalMonths Then
            SwapStoredMonthsPlaces(whichMoveDown, whichMoveDown + 1)
            UpdateDynamicObjectsFromStoredMonths()
            RefreshLeapDay()
            RefreshGregorianDays()
            RefreshTabIndeces()
            RefreshLabelCalendarDate()
        End If
    End Sub

    Public Sub CreateMonth(Optional ByVal passedName As String = "", Optional ByVal passedDays As Integer = 1, Optional ByVal passedLeap As Boolean = False)

        If TotalMonths < 99 Then
            TotalMonths += 1

            Dim newMonth As New TextBox With {
            .Name = "MonthName" + TotalMonths.ToString(),
            .Text = passedName,
            .PlaceholderText = TextsInLanguage(15, CurrentLanguage), ' "Add a name"
            .Tag = TotalMonths,
            .Top = MainViewForm.ButtonAddMonth.Top,
            .Left = MainViewForm.LabelMonthName.Left,
            .Width = MainViewForm.ButtonAddMonth.Width,
            .Height = MainViewForm.ButtonAddMonth.Height
            }
            MainViewForm.MonthsPanel.Controls.Add(newMonth)
            AddHandler newMonth.TextChanged, AddressOf DynamicMonthName_Changed
            AddHandler newMonth.GotFocus, AddressOf DynamicTextbox_GotFocus
            AddHandler newMonth.Click, AddressOf DynamicTextbox_GotFocus
            storedMonths(TotalMonths, storedName) = passedName

            Dim newDays As New NumericUpDown With {
            .Name = "MonthDays" + TotalMonths.ToString(),
            .Minimum = 1,
            .Value = passedDays,
            .Maximum = 365,
            .Tag = TotalMonths,
            .Top = MainViewForm.ButtonAddMonth.Top,
            .Left = MainViewForm.LabelDays.Left,
            .Width = 59,
            .Height = MainViewForm.ButtonAddMonth.Height
            }
            MainViewForm.MonthsPanel.Controls.Add(newDays)
            AddHandler newDays.ValueChanged, AddressOf DynamicDayValue_Changed
            AddHandler newDays.GotFocus, AddressOf DynamicUpDown_GotFocus
            AddHandler newDays.Click, AddressOf DynamicUpDown_GotFocus
            storedMonths(TotalMonths, storedDay) = passedDays

            Dim newMonthLabel As New Label With {
            .Name = "LabelMonthNumber" + TotalMonths.ToString(),
            .Text = TotalMonths.ToString(),
            .Tag = TotalMonths,
            .AutoSize = False,
            .TextAlign = System.Drawing.ContentAlignment.MiddleRight,
            .Top = MainViewForm.ButtonAddMonth.Top + 2,
            .Left = MainViewForm.ButtonAddMonth.Left - 32,
            .Width = 26,
            .Height = 18
            }
            MainViewForm.MonthsPanel.Controls.Add(newMonthLabel)

            Dim newGregorianDay As New Label With {
            .Name = "LabelGregorianDay" + TotalMonths.ToString(),
            .Text = TotalMonths.ToString(),
            .Tag = TotalMonths,
            .AutoSize = True,
            .TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
            .Top = MainViewForm.ButtonAddMonth.Top + 2,
            .Left = MainViewForm.LabelTitleGregorian.Left
            }
            MainViewForm.MonthsPanel.Controls.Add(newGregorianDay)

            Dim newMonthRemove As New Button With {
            .Name = "MonthRemove" + TotalMonths.ToString(),
            .Text = "X",
            .Tag = TotalMonths,
            .Top = MainViewForm.ButtonAddMonth.Top,
            .Left = MainViewForm.ButtonAddMonth.Left - 94,
            .Width = 28,
            .Height = 23
            }
            MainViewForm.MonthsPanel.Controls.Add(newMonthRemove)
            AddHandler newMonthRemove.Click, AddressOf DynamicRemoveMonth_Click

            Dim newMonthMoveDown As New Button With {
            .Name = "MonthMoveDown" + TotalMonths.ToString(),
            .Text = "▼",
            .Tag = TotalMonths,
            .Top = MainViewForm.ButtonAddMonth.Top,
            .Left = MainViewForm.ButtonAddMonth.Left - 60,
            .Width = 28,
            .Height = 23
            }
            MainViewForm.MonthsPanel.Controls.Add(newMonthMoveDown)
            AddHandler newMonthMoveDown.Click, AddressOf DynamicMoveMonthDown

            Dim newOptionLeapDay As New RadioButton With {
            .Name = "OptionLeapDay" + TotalMonths.ToString(),
            .Text = "",
            .Checked = passedLeap,
            .Tag = TotalMonths,
            .Top = MainViewForm.ButtonAddMonth.Top + 5,
            .Left = MainViewForm.LabelLeap.Left + 38,
            .Width = 14,
            .Height = 13
            }
            MainViewForm.MonthsPanel.Controls.Add(newOptionLeapDay)
            AddHandler newOptionLeapDay.CheckedChanged, AddressOf DynamicLeapCheckedChanged
            storedMonths(TotalMonths, storedLeap) = passedLeap

            MainViewForm.ButtonAddMonth.Top += ButtonAddSpace

            MainViewForm.MonthsPanel.VerticalScroll.Value = MainViewForm.MonthsPanel.VerticalScroll.Maximum
        End If

        RefreshGregorianDays()

    End Sub

    Public Sub DynamicLeapCheckedChanged(sender As Object, e As EventArgs)
        Dim clickedLeap As RadioButton = DirectCast(sender, RadioButton)
        Dim whichclicked = CInt(clickedLeap.Name.Substring("OptionLeapDay".Length, clickedLeap.Name.Length - "OptionLeapDay".Length))
        For i = 1 To TotalMonths
            If i = whichclicked Then
                storedMonths(i, storedLeap) = True
            Else
                storedMonths(i, storedLeap) = False
            End If
        Next
    End Sub

    Public Sub DynamicDayValue_Changed(sender As Object, e As EventArgs)
        Dim clickedDay As NumericUpDown = DirectCast(sender, NumericUpDown)
        storedMonths(clickedDay.Name.Substring("MonthDays".Length, clickedDay.Name.Length - "MonthDays".Length), storedDay) = clickedDay.Value

        RefreshDayBudget()
        RefreshGregorianDays()
        RefreshLabelCalendarDate()

    End Sub

    Public Sub SetCurrentNotes()
        Dim iWeek As String = GiveCalendarDate(MainDateDayOfYear, MainDateYear, returnWeek)
        Dim iDay As String = GiveCalendarDate(MainDateDayOfYear, MainDateYear, returnDay)
        MainViewForm.LabelYearDetails.Text = TextsInLanguage(17, CurrentLanguage) + " " + iWeek + " | " + TextsInLanguage(18, CurrentLanguage) + " " + iDay

        'Find and show previous/standing week comments
        ' CurrentEditorWeek is offset -1 (goes from 1 to 53) because of indeces (0 to 52)
        Dim tempI As Integer = Int(iWeek) - 1
        Dim tempWrapAround As Boolean = False
        thereNoNotes = False

        'This could be stopped with Exit Do, instead of having a tempGo,
        'but I don't like "perpetual" Do Loop While(True)
        Dim tempGo As Boolean = True
        Do
            If CommentsWeek(tempI) <> "" Then
                activeWeekComments = tempI
                If showingFirstTime = True Then
                    MainViewForm.ComboWeekSelect.SelectedIndex = tempI
                    showingFirstTime = False
                End If
                tempGo = False
            Else
                tempI -= 1
                'Wrap around because no prior comments were found yet
                If tempI < 0 Then
                    If tempWrapAround = False Then
                        tempI = 52
                        tempWrapAround = True
                    Else
                        tempGo = False
                    End If
                ElseIf tempI = Int(iWeek) And tempWrapAround Then
                    'Stop if the wraparound is completed, there are no comments
                    tempGo = False
                    thereNoNotes = True
                End If
            End If
        Loop While (tempGo)
    End Sub

    Public Sub RefreshDatesAndNotes()

        Dim tempDateToday = MainDateDayOfYear
        Dim tempMaxDays = DaysPerYear
        Dim tempYear As Integer = MainDateYear

        MainViewForm.ProgressBarYear.Value = GiveCalendarDate(tempDateToday, tempYear, returnDay)

        SetCurrentNotes()

        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ''' Build the string to show the days of the coming 2 weeks

        If DateTime.IsLeapYear(MainDateYear) Then
            tempMaxDays += 1
        End If
        Dim tempBuildingLabelText As String = ""
        For tempCountingDates = 0 To listItemsToShow - 1
            Dim iHeader As String = ""
            Dim iFooter As String = ""
            Dim targetDate As DateTime = New DateTime(tempYear, 1, 1).AddDays(tempDateToday - 1)
            Select Case CInt(targetDate.DayOfWeek)
                Case 0, 6
                    iHeader = "\f0\fs21\cf1 "
                    iFooter = ""
                Case Else
                    iHeader = "\f0\fs21\cf2 "
            End Select

            Dim addToday As String = ""
            Dim addCorrespondingNotes As String = ""
            If tempCountingDates = 0 Then
                addToday = "\b "
                addToday += TextsInLanguage(55, CurrentLanguage) & " \tab "
                If thereNoNotes = False Then
                    addCorrespondingNotes = " " & TextsInLanguage(63, CurrentLanguage) & (activeWeekComments + 1).ToString() & ")"
                End If
            Else
                addToday = TextsInLanguage(62, CurrentLanguage) & GiveCalendarDate(tempDateToday, tempYear, returnWeek) & ")\tab "
            End If

            Dim addStar As String = ""
            If String.IsNullOrWhiteSpace(addCorrespondingNotes) And String.IsNullOrWhiteSpace(CommentsWeek(Int(GiveCalendarDate(tempDateToday, tempYear, returnWeek)) - 1)) = False And (Int(GiveCalendarDate(tempDateToday, tempYear, returnDay)) + 6) Mod 7 = 0 Then
                addStar = " " & TextsInLanguage(61, CurrentLanguage)
            End If

            If tempCountingDates = 0 Then
                iFooter += "\b0"
            End If

            ' For debugging! Leave it disabled in release.
            'addToday += " " & tempDateToday & " "

            tempBuildingLabelText += iHeader & addToday & GiveCalendarDate(tempDateToday, tempYear, returnDate) & addStar & addCorrespondingNotes & iFooter & " \par "

            tempDateToday += 1
            If tempDateToday > tempMaxDays Then
                tempDateToday = 1
                tempYear += 1
            End If
        Next

        tempBuildingLabelText = "{\rtf1\ansi\deff0{\fonttbl{\f0 Segoe UI;}}{\colortbl;\red100\green80\blue250;\red20\green20\blue20;}" & tempBuildingLabelText & "}"

        Dim tempTxt As String = ""
        Using tempRtf As New RichTextBox()
            tempRtf.Rtf = tempBuildingLabelText
            tempTxt = tempRtf.Text

            If MainViewForm.RichCurrentDates.Rtf <> tempRtf.Rtf Then
                MainViewForm.RichCurrentDates.Rtf = tempBuildingLabelText
            End If
        End Using
    End Sub

    Public Sub ClearProgressBar()
        MainViewForm.ProgressBarYear.Value = 0
    End Sub

    Public Sub DynamicUpDown_GotFocus(sender As Object, e As EventArgs)
        sender.Select(0, sender.Value.ToString.Length)
    End Sub

    Public Sub DynamicTextbox_GotFocus(sender As Object, e As EventArgs)
        If sender.Text IsNot "" Then
            sender.Select(0, sender.Text.ToString.Length)
        End If
    End Sub

    Public Sub RefreshLabelCalendarDate()
        If DayBudget = 0 Then
            MainViewForm.LabelCalendarDate.Text = CalculateCalendarDate(((ReadingEditorWeek + 1) * 7) - 6)
        Else
            MainViewForm.LabelCalendarDate.Text = TextsInLanguage(28, CurrentLanguage)
        End If
    End Sub

    Public Sub CreateNewCalendar()
        QuickRemoveAll()
        MainViewForm.ComboCycleMonth.SelectedIndex = 0
        MainViewForm.ComboCycleDay.SelectedIndex = 0
        CurrentFile = ""
        MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)
    End Sub

    Public Sub QuickRemoveAll()

        'Suspend panel rendering to prevent messing things up
        MainViewForm.MonthsPanel.SuspendLayout()

        ' Quickly remove all:
        '   RemoveMonth + handler
        '   Move down button + handler
        '   LabelMonthNumber
        '   MonthName + handlers
        '   MonthDays + handlers
        '   IsLeap + handler
        '   LabelGregorian
        '   
        '   Week comments from array and textbox

        If TotalMonths > 0 Then
            For tempI = 1 To TotalMonths
                PanelRemoveObject("LabelMonthNumber" + tempI.ToString())
                PanelRemoveObject("LabelGregorianDay" + tempI.ToString())
            Next

            Dim totalControls As Integer = MainViewForm.MonthsPanel.Controls.Count
            For i = totalControls - 1 To 0 Step -1
                Dim ctrlB As Control = MainViewForm.MonthsPanel.Controls(i)
                If ctrlB.Name.Contains("MonthDays") Then
                    Dim targetControl As Control
                    targetControl = ctrlB
                    Dim tempUpDown As NumericUpDown = CType(targetControl, NumericUpDown)
                    RemoveHandler tempUpDown.ValueChanged, AddressOf DynamicDayValue_Changed
                    RemoveHandler tempUpDown.GotFocus, AddressOf DynamicUpDown_GotFocus
                    RemoveHandler tempUpDown.Click, AddressOf DynamicUpDown_GotFocus
                    MainViewForm.MonthsPanel.Controls.Remove(tempUpDown)
                    tempUpDown.Dispose()
                ElseIf ctrlB.Name.Contains("MonthRemove") Then
                    Dim targetControl As Control
                    targetControl = ctrlB
                    Dim tempButton As Button = CType(targetControl, Button)
                    RemoveHandler tempButton.Click, AddressOf DynamicRemoveMonth_Click
                    MainViewForm.MonthsPanel.Controls.Remove(tempButton)
                    tempButton.Dispose()
                ElseIf ctrlB.Name.Contains("OptionLeapDay") Then
                    Dim targetControl As Control
                    targetControl = ctrlB
                    Dim tempRadio As RadioButton = CType(targetControl, RadioButton)
                    RemoveHandler tempRadio.CheckedChanged, AddressOf DynamicLeapCheckedChanged
                    MainViewForm.MonthsPanel.Controls.Remove(tempRadio)
                    tempRadio.Dispose()
                ElseIf ctrlB.Name.Contains("MonthMoveDown") Then
                    Dim targetControl As Control
                    targetControl = ctrlB
                    Dim tempMoveDown As Button = CType(targetControl, Button)
                    RemoveHandler tempMoveDown.Click, AddressOf DynamicMoveMonthDown
                    MainViewForm.MonthsPanel.Controls.Remove(tempMoveDown)
                    tempMoveDown.Dispose()
                ElseIf ctrlB.Name.Contains("MonthName") And ctrlB.Name.Contains("LabelMonthName") = False Then
                    Dim targetControl As Control
                    targetControl = ctrlB
                    Dim tempText As TextBox = CType(targetControl, TextBox)
                    RemoveHandler tempText.TextChanged, AddressOf DynamicMonthName_Changed
                    RemoveHandler tempText.GotFocus, AddressOf DynamicTextbox_GotFocus
                    RemoveHandler tempText.Click, AddressOf DynamicTextbox_GotFocus
                    MainViewForm.MonthsPanel.Controls.Remove(tempText)
                    tempText.Dispose()
                End If
            Next

            MainViewForm.ButtonAddMonth.Top -= ButtonAddSpace * TotalMonths
            TotalMonths = 0
            RefreshDayBudget()
        End If

        Array.Clear(storedMonths, 0, storedMonths.Length)

        ' Remove comments from array and editor
        weekSuspendChangeEvent = True
        Array.Clear(CommentsWeek, 0, CommentsWeek.Length)
        MainViewForm.RichWeekNotes.Clear()
        MainViewForm.ComboWeekSelect.SelectedIndex = 0
        ReadingEditorWeek = 0
        RefreshWeekComments()
        weekSuspendChangeEvent = False

        ''
        'Resume panel rendering
        MainViewForm.MonthsPanel.PerformLayout()
        MainViewForm.MonthsPanel.ResumeLayout()
        MainViewForm.MonthsPanel.Refresh()

    End Sub

    Public Sub RefreshWeekComments()
        MainViewForm.RichWeekNotes.Clear()
        MainViewForm.RichWeekNotes.Rtf = CommentsWeek(ReadingEditorWeek)
    End Sub

    Public Sub Calendar_Refresh_MonthDays()

        Dim OldSelectedIndex As Integer = Val(MainViewForm.ComboStartDay.Text) - 1
        Dim MaxDays As Integer = 30

        Select Case MainViewForm.ComboStartMonth.SelectedIndex
            Case 0, 2, 4, 6, 7, 9, 11
                MaxDays = 31
            Case 1
                MaxDays = 28
                If DateTime.IsLeapYear(MainDateYear) Then
                    MaxDays += 1
                End If
        End Select

        MainViewForm.ComboStartDay.Items.Clear()
        For i = 1 To MaxDays
            MainViewForm.ComboStartDay.Items.Add(i)
        Next

        If MainViewForm.ComboStartDay.Items.Count - 1 < OldSelectedIndex Then
            MainViewForm.ComboStartDay.SelectedIndex = MainViewForm.ComboStartDay.Items.Count - 1
            If MainViewForm.ComboStartDay.SelectedIndex < 0 Then
                MainViewForm.ComboStartDay.SelectedIndex = 0
            End If
        Else
            MainViewForm.ComboStartDay.SelectedIndex = OldSelectedIndex
        End If
    End Sub

    Public Sub ProcessComboStartDayLimits(ByVal whichDirection As Integer)
        If whichDirection > 0 Then 'wheel up
            'This event runs BEFORE _selectedindexchanged            
            If MainViewForm.ComboStartDay.SelectedIndex = 0 Then
                'First change the month, so the max item in this combobox is adapted to the right month.
                'If it's January, go back a year (if possible)
                If MainViewForm.ComboStartMonth.SelectedIndex = 0 Then
                    If MainViewForm.ComboStartYear.SelectedIndex > 0 Then
                        MainViewForm.ComboStartYear.SelectedIndex -= 1
                        MainViewForm.ComboStartMonth.SelectedIndex = MainViewForm.ComboStartMonth.Items.Count - 1
                    Else
                        'Can't go back because it's the earliest possible selectable date
                        Exit Sub
                    End If
                Else
                    MainViewForm.ComboStartMonth.SelectedIndex -= 1
                End If
                combostartSuspendChangeEvent = True
                combostartObligatoryDayValue = MainViewForm.ComboStartDay.Items.Count - 1
                MainViewForm.ComboStartDay.SelectedIndex = MainViewForm.ComboStartDay.Items.Count - 1
            End If
        Else 'wheel down
            If MainViewForm.ComboStartDay.SelectedIndex = MainViewForm.ComboStartDay.Items.Count - 1 Then
                If MainViewForm.ComboStartMonth.SelectedIndex = MainViewForm.ComboStartMonth.Items.Count - 1 Then
                    If MainViewForm.ComboStartYear.SelectedIndex < MainViewForm.ComboStartYear.Items.Count - 1 Then
                        MainViewForm.ComboStartYear.SelectedIndex += 1
                        MainViewForm.ComboStartMonth.SelectedIndex = 0
                    Else
                        Exit Sub
                    End If
                Else
                    MainViewForm.ComboStartMonth.SelectedIndex += 1
                End If
                combostartSuspendChangeEvent = True
                combostartObligatoryDayValue = 0
                MainViewForm.ComboStartDay.SelectedIndex = 0
            End If
        End If
    End Sub

    Public Sub ProcessComboStartMonthLimits(ByVal whichDirection As Integer)
        If whichDirection > 0 Then 'up         

            If MainViewForm.ComboStartMonth.SelectedIndex = 0 Then
                If MainViewForm.ComboStartYear.SelectedIndex > 0 Then
                    MainViewForm.ComboStartYear.SelectedIndex -= 1
                    combostartMonthSuspendChangeEvent = True
                    combostartObligatoryMonthValue = MainViewForm.ComboStartMonth.Items.Count - 1
                    MainViewForm.ComboStartMonth.SelectedIndex = MainViewForm.ComboStartMonth.Items.Count - 1
                Else
                    Exit Sub
                End If
            End If
        Else 'down
            If MainViewForm.ComboStartMonth.SelectedIndex = MainViewForm.ComboStartMonth.Items.Count - 1 Then
                If MainViewForm.ComboStartYear.SelectedIndex < MainViewForm.ComboStartYear.Items.Count - 1 Then
                    MainViewForm.ComboStartYear.SelectedIndex += 1
                    combostartMonthSuspendChangeEvent = True
                    combostartObligatoryMonthValue = 0
                    MainViewForm.ComboStartMonth.SelectedIndex = 0
                Else
                    Exit Sub
                End If
            End If

        End If
    End Sub

End Module

