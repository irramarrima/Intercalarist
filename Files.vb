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
Imports System.IO
Module Files

    'This file contains subs that handle file operations.

    Public loadedDefaultCalendar As String = ""
    Public Const savingDefaultCalendar As Integer = 0
    Public Const savingDefaultLanguage As Integer = 1
    Public Const savingCollapsedGroupboxes As Integer = 2
    Public Const savingRecentFiles As Integer = 3
    Public Const savingNothingInParticular As Integer = 4
    Public CurrentFile As String = ""
    Public ReadFromFile As String
    Public storedRecentFiles(10) As String
    Public Const MaxRecentFiles As Integer = 10
    Public recentSubmenuCount As Integer = 0

    Public Sub LoadInitCfg()

        Dim fileLines As String()
        Array.Clear(storedRecentFiles)

        Try
            fileLines = System.IO.File.ReadAllLines(ConfigFile)

            Dim iKeyword As String = ""
            Dim iDefaultCalendar As String = ""
            Dim iDefaultLanguage As String = ""
            Dim tempCollapsedGroupboxes(NumberOfCalendarCollapsibleGroupboxes) As Boolean
            For iLine As Integer = 0 To fileLines.Length - 1

                iKeyword = "DefaultCalendar "
                If fileLines(iLine).Length >= iKeyword.Length Then
                    If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                        iDefaultCalendar = fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length)
                    End If
                End If

                iKeyword = "DefaultLanguage "
                If fileLines(iLine).Length >= iKeyword.Length Then
                    If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                        iDefaultLanguage = fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length)
                    End If
                End If

                iKeyword = "Collapsed "
                If fileLines(iLine).Length >= iKeyword.Length Then
                    If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                        Dim tempCollapsedWhich As Integer
                        tempCollapsedWhich = Int(fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length))
                        tempCollapsedGroupboxes(tempCollapsedWhich) = True
                    End If
                End If

                iKeyword = "RecentFile "
                If fileLines(iLine).Length >= iKeyword.Length Then
                    If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                        Dim tempNameOfRecentfile As String = ""
                        tempNameOfRecentfile = fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length)
                        UpdateRecentFiles(tempNameOfRecentfile) ', True)
                    End If
                End If

            Next

            If String.IsNullOrWhiteSpace(iDefaultCalendar) = False Then
                Dim successfulLoad As Boolean = OpenCalendarFile(iDefaultCalendar)
                'successfulLoad is a global var that's set to false in opencalendarfile then updated there
                If successfulLoad Then
                    loadedDefaultCalendar = CurrentFile
                    UpdateRecentFiles(CurrentFile)
                Else
                    loadedDefaultCalendar = ""
                End If
            End If

            If String.IsNullOrWhiteSpace(iDefaultLanguage) = False And Int(iDefaultLanguage) >= 1 And Int(iDefaultLanguage) < 3 Then
                CurrentLanguage = Int(iDefaultLanguage)
                If CurrentLanguage = 0 Then
                    MainViewForm.EnglishToolStripMenuItem.Checked = True
                ElseIf CurrentLanguage = 1 Then
                    MainViewForm.EnglishToolStripMenuItem.Checked = False
                End If
            Else
                'English as default language
                CurrentLanguage = 0
                MainViewForm.EnglishToolStripMenuItem.Checked = True
            End If

            MainViewForm.SpanishToolStripMenuItem.Checked = Not MainViewForm.EnglishToolStripMenuItem.Checked
            RefreshStaticTexts()

            Dim tempDidCollapse As Boolean = False
            For i = 1 To NumberOfCalendarCollapsibleGroupboxes
                If tempCollapsedGroupboxes(i) Then
                    MakeGroupBoxCollapse(i)
                    UpdateEnvironmentBounds()
                End If
            Next

        Catch ex As Exception
            CurrentLanguage = 0 'English
            MainViewForm.EnglishToolStripMenuItem.Checked = True
            MainViewForm.SpanishToolStripMenuItem.Checked = Not MainViewForm.EnglishToolStripMenuItem.Checked
            RefreshStaticTexts()
            MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)  '" - Unnamed"
            MessageBox.Show(TextsInLanguage(64, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            SaveInitCfg(savingNothingInParticular)
        End Try
    End Sub

    Public Sub SaveInitCfg(ByVal TypeOfSave As Integer)

        'The config file will be overwritten

        'Check which groupboxes are collapsed to save it
        Dim tempGroupBoxCollapsed(NumberOfCalendarCollapsibleGroupboxes) As Boolean
        For i As Integer = 1 To NumberOfCalendarCollapsibleGroupboxes
            Dim foundControls() As Control = MainViewForm.PanelCalendar.Controls.Find("GroupBox" + i.ToString(), False)
            Dim foundControl As Control = foundControls(0)
            Dim measuringGroupBox As GroupBox = DirectCast(foundControl, GroupBox)
            If measuringGroupBox.Height < Val(measuringGroupBox.Tag) Then
                tempGroupBoxCollapsed(i) = True
            Else
                tempGroupBoxCollapsed(i) = False
            End If
        Next

        Using writer As New StreamWriter(ConfigFile)
            Try
                If TypeOfSave = savingDefaultCalendar Then
                    writer.WriteLine("DefaultCalendar " + CurrentFile)
                Else
                    writer.WriteLine("DefaultCalendar " + loadedDefaultCalendar)
                End If

                writer.WriteLine("DefaultLanguage " + CurrentLanguage.ToString)

                For j = 1 To NumberOfCalendarCollapsibleGroupboxes
                    If tempGroupBoxCollapsed(j) Then
                        writer.WriteLine("Collapsed " & j)
                    End If
                Next
                For j = MaxRecentFiles To 1 Step -1 'so they stay in order when loading cfg
                    If storedRecentFiles(j) <> "" Then
                        writer.WriteLine("RecentFile " & storedRecentFiles(j))
                    End If
                Next

                '"This calendar has been set as the default calendar."
                If TypeOfSave = savingDefaultCalendar And CurrentFile <> "" Then
                    MessageBox.Show(TextsInLanguage(1, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Information)
                    loadedDefaultCalendar = CurrentFile
                End If
            Catch ex As Exception
                MessageBox.Show(TextsInLanguage(25, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End Using

    End Sub

    Public Function OpenCalendarFile(ByVal whichFile As String) As Boolean

        'Suspend autoscroll and rendering
        'to prevent making a mess when the panel isn't visible.
        MainViewForm.MonthsPanel.AutoScroll = False
        MainViewForm.MonthsPanel.SuspendLayout()

        Dim successfulLoad As Boolean = False

        Dim fileLines As String()

        Try
            fileLines = System.IO.File.ReadAllLines(whichFile)

            Dim iConstructingString As String = ""
            Dim iReadStopMode As Boolean = False

            Dim iName As String = ""
            Dim iDays As Integer = 0
            Dim iLeap As Boolean = False
            Dim iKeyword As String = ""
            Dim tempI As Integer
            Dim tempWeekCommenting As Integer
            For iLine As Integer = 0 To fileLines.Length - 1

                If iReadStopMode = False Then

                    iKeyword = "Month "
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            iName = fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length)
                        End If
                    End If

                    iKeyword = "Days "
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            iDays = Int(fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length))
                        End If
                    End If

                    iKeyword = "HasIntercalary"
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            iLeap = True
                        End If
                    End If

                    If iDays > 0 And iDays < 366 Then
                        'Everything about a month must be between the Month and the Days statement. The Days statement triggers
                        'the creation of a month.
                        'So, IsLeap and the comments must be sandwiched between them, like so:
                        '       Month Whatever
                        '       IsLeap
                        '       Days 20
                        CreateMonth(iName, iDays, iLeap)
                        iDays = 0
                        iName = ""
                        iLeap = False
                    End If

                    iKeyword = "StartMonth "
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            tempI = Int(fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length))
                            If tempI > 0 And tempI <= MainViewForm.ComboCycleMonth.Items.Count Then
                                MainViewForm.ComboCycleMonth.SelectedIndex = tempI - 1
                            End If
                        End If
                    End If

                    iKeyword = "StartDay "
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            tempI = Int(fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length))
                            If tempI > 0 And tempI <= MainViewForm.ComboCycleDay.Items.Count Then
                                MainViewForm.ComboCycleDay.SelectedIndex = tempI - 1
                            End If
                        End If
                    End If

                End If

                If iReadStopMode Then
                    If fileLines(iLine) = "Stop" Then
                        CommentsWeek(tempWeekCommenting) = iConstructingString
                        iConstructingString = ""
                        iReadStopMode = False
                    Else
                        iConstructingString = fileLines(iLine).Replace("(!/n)", ControlChars.CrLf)
                        iConstructingString = iConstructingString.Replace("(!/t)", ControlChars.Tab)
                    End If
                Else
                    'Enter read/stop mode
                    iKeyword = "Week "
                    If fileLines(iLine).Length >= iKeyword.Length Then
                        If fileLines(iLine).Substring(0, iKeyword.Length) = iKeyword Then
                            tempWeekCommenting = Int(fileLines(iLine).Substring(iKeyword.Length, fileLines(iLine).Length - iKeyword.Length)) - 1
                            If tempWeekCommenting > -1 And tempWeekCommenting < 53 Then
                                iReadStopMode = True
                                iConstructingString = ""
                            End If
                        End If
                    End If
                End If
            Next

            'Don't place RefreshWeekComments() within this structure
            'because you'll get an empty comment week for week 1 even if it isn't
            If TotalMonths > 0 Then
                MainViewForm.Text = MainTitle + " - " + whichFile
                CurrentFile = whichFile
                RefreshLabelCalendarDate()
                successfulLoad = True
                UpdateRecentFiles(whichFile)
            Else
                'Unnamed calendar
                MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)
                CurrentFile = ""
                RefreshLabelCalendarDate()
            End If

        Catch ex As Exception
            'Just in case, scratch everything and start with an Unnamed calendar
            MessageBox.Show(whichFile + " " + TextsInLanguage(27, CurrentLanguage), MainTitle, MessageBoxButtons.OK, MessageBoxIcon.Error)
            QuickRemoveAll()
            MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)
        End Try

        RefreshLeapDay()
        RefreshDayBudget()
        RefreshTabIndeces()
        RefreshLabelCalendarDate()

        'Resume rendering and autoscroll
        MainViewForm.MonthsPanel.PerformLayout()
        MainViewForm.MonthsPanel.ResumeLayout()
        MainViewForm.MonthsPanel.Refresh()
        MainViewForm.MonthsPanel.AutoScroll = True
        showingFirstTime = True

        Return successfulLoad

    End Function

    Public Sub SaveCalendarFile(ByVal whichFile As String)

        ' Saves:
        '   StartMonth
        '   StartDay
        '   MonthName
        '   OptionLeapDay
        '   MonthDays
        '   Weekly notes

        'this will overwrite an existing file!
        Using writer As New StreamWriter(whichFile)

            'Open the file with general information

            'Inactive for now, but in case future releases change the format, even just a little.
            writer.WriteLine("Version " & AppCurrentVersion.ToString())

            'These indeces are saved with an extra 1 that's removed by the opener to make the file itself
            'easier to read without this app, for quick savefile debugging purposes
            writer.WriteLine("StartMonth " & (Int(MainViewForm.ComboCycleMonth.SelectedIndex) + 1).ToString())
            writer.WriteLine("StartDay " & (Int(MainViewForm.ComboCycleDay.SelectedIndex) + 1).ToString())

            'Move on to each specific month
            For i = 1 To TotalMonths

                'Month is always at the beginning of each
                writer.WriteLine("Month " & storedMonths(i, storedName))

                If storedMonths(i, storedLeap) = True Then
                    writer.WriteLine("HasIntercalary")
                End If

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Days is always at the end of each
                writer.WriteLine("Days " + storedMonths(i, storedDay).ToString())
            Next

            ' Save the weekly comments.
            For j = 0 To 52
                If String.IsNullOrWhiteSpace(CommentsWeek(j)) = False Then
                    writer.WriteLine("Week " & (j + 1).ToString())
                    Dim iComments As String = CommentsWeek(j).Replace(ControlChars.CrLf, "(!/n)")
                    iComments = iComments.Replace(ControlChars.Tab, "(!/t)")
                    writer.WriteLine(iComments)
                    writer.WriteLine("Stop")
                End If
            Next

            MainViewForm.Text = MainTitle + " - " + CurrentFile

        End Using

    End Sub

End Module
