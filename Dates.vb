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
Module Dates

    'This module contains subs and functions related to calculations for dates.

    'Used for operations,
    'can be changed in runtime from the date listing groupbox
    Public MainDateYear As Integer = Date.Now.Year
    Public MainDateMonth As Integer = Date.Now.Month
    Public MainDateDay As Integer = Date.Now.Day
    Public MainDateDayOfYear As Integer = Date.Now.DayOfYear

    'Month info is kept here
    Public storedMonths(100, 2) As Object
    Public Const storedName As Integer = 0
    Public Const storedDay As Integer = 1
    Public Const storedLeap As Integer = 2
    Public Const storedMaxIndex = 2

    'To request return values more clearly when calling calendar functions.
    Public Const returnDate As Integer = 0
    Public Const returnWeek As Integer = 1
    Public Const returnDay As Integer = 2

    Public Sub RemoveStoredMonth(ByVal whichIndex As Integer)

        'This sub just moves all storedmonths one index up

        If whichIndex < TotalMonths Then
            'if it's the last index don't even bother, it won't be accessible.
            'And if the user adds another month, all its contents will be reset at creation.
            'Totalmonths is decreased in the calling sub.
            For j = whichIndex To TotalMonths - 1
                For i = 0 To storedMaxIndex
                    storedMonths(j, i) = storedMonths(j + 1, i)
                Next
            Next
        End If
    End Sub

    Public Sub SwapStoredMonthsPlaces(ByVal firstIndex As Integer, ByVal secondIndex As Integer)
        Dim tempArray(storedMaxIndex) As Object
        For i = 0 To storedMaxIndex
            tempArray(i) = storedMonths(firstIndex, i)
            storedMonths(firstIndex, i) = storedMonths(secondIndex, i)
        Next
        For j = 0 To storedMaxIndex
            storedMonths(secondIndex, j) = tempArray(j)
        Next
    End Sub

    Public Function giveFormattedDate(ByVal dayNumber As Integer) As String

        Dim targetYear As Integer = YearOfReference
        Dim dateFormat As String = "MMMM dd"
        Dim cultureEnglish As New CultureInfo("en-US")
        Dim cultureSpanish As New CultureInfo("es-ES")

        Dim firstDayOfYear As New DateTime(targetYear, 1, 1)
        Dim resultDate As DateTime = firstDayOfYear.AddDays(dayNumber - 1)

        Dim formattedDate As String = ""
        If CurrentLanguage = lgEnglish Then
            formattedDate = resultDate.ToString(dateFormat, cultureEnglish)
        ElseIf CurrentLanguage = lgSpanish Then
            formattedDate = resultDate.ToString(dateFormat, cultureSpanish)
        End If

        Return formattedDate

    End Function

    Public Function GiveCalendarDate(ByVal tempToday As Integer, ByVal tempYear As Integer, ByVal TypeOfReturn As Integer) As String

        'tempToday is the Gregorian day that's fed to get a calendar date
        'tempYear is the current year for calculation

        'TypeOfReturn is set by global variables returnDate, returnWeek, returnDay
        'and determines what the function actually returns.

        'THIS ITERATION OF THE FUNCTION MAKES SURE THAT CYCLES ***ALWAYS*** START ON THE ASSIGNED GREGORIAN DATE
        'AND THE REST WILL CHANGE DEPENDING ON WHEN THERE IS A LEAP DAY IN THE GREGORIAN

        '1st of May = 60 (No leap) = 61 (Leap)

        'Find which month has the leap day,
        'store it as tempJ
        Dim tempJ As Integer = 1
        For i = 1 To TotalMonths
            If storedMonths(i, storedLeap) = True Then
                tempJ = i
            End If
        Next

        Dim ShouldAddDay As Boolean = False
        Dim targetCycleStart As Integer = CycleStartDay
        Dim overflowAfter As Integer = DaysPerYear
        Dim CycleSum As Integer = 0
        Dim mustOverflow As Boolean = False
        If DateTime.IsLeapYear(tempYear) Then
            If CycleStartDay >= 60 Then
                targetCycleStart += 1
                If tempToday >= targetCycleStart Then
                    CycleSum = targetCycleStart
                    overflowAfter = DaysPerYear + 1 'but shouldn't
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'MUST
                    mustOverflow = True
                    ShouldAddDay = True
                End If
            Else
                If tempToday >= targetCycleStart Then
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear + 1 'but shouldn't
                    ShouldAddDay = True
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'MUST
                    mustOverflow = True
                End If
            End If
        Else
            If tempToday >= CycleStartDay Then
                If DateTime.IsLeapYear(tempYear + 1) Then
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'can't
                    If CycleStartDay >= 60 Then
                        'only add a leap if the cycle
                        'bleeds into the next year,
                        'which is leap
                        'and the cycle begins on or after
                        'March 1
                        ShouldAddDay = True
                    End If
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'can't
                End If
            Else
                If DateTime.IsLeapYear(tempYear - 1) Then
                    If CycleStartDay >= 60 Then
                        CycleSum = CycleStartDay + 1
                        overflowAfter = DaysPerYear + 1 'must
                        mustOverflow = True
                    Else
                        CycleSum = CycleStartDay
                        overflowAfter = DaysPerYear + 1
                        ShouldAddDay = True
                        If CycleStartDay > 1 Then
                            'only then it will bleed
                            mustOverflow = True
                        End If
                    End If
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'must if bleeds
                    If CycleStartDay > 1 Then
                        mustOverflow = True
                    End If
                End If
            End If
        End If

        'Now, start adding day by day
        Dim tempL As Integer = 1    'the ordinal day is stored here
        Dim tempI As Integer        'the month is stored here
        Dim tempK As Integer = 0    'the day of the month is stored here
        Dim hasOverflown As Boolean = False
        Dim iLeapDay As Integer
        Dim iFoundTheDate As Boolean = False
        For tempI = 1 To TotalMonths

            'Add a leap day to the month being summed
            If tempI = tempJ And ShouldAddDay Then
                iLeapDay = 1
            Else
                iLeapDay = 0
            End If

            For iMonthCounting = 1 To storedMonths(tempI, storedDay) + iLeapDay

                If CycleSum = tempToday Then
                    If (mustOverflow And hasOverflown) Or (mustOverflow = False) Then
                        tempK = iMonthCounting
                        iFoundTheDate = True
                        Exit For
                    End If
                End If

                tempL += 1
                CycleSum += 1
                If CycleSum > overflowAfter Then
                    CycleSum = 1
                    hasOverflown = True
                End If

            Next

            If iFoundTheDate Then
                Exit For
            End If

        Next

        Dim tempMonthName As String = storedMonths(tempI, storedName)
        If String.IsNullOrWhiteSpace(tempMonthName) Then
            tempMonthName = TextsInLanguage(36, CurrentLanguage) + " " + tempI.ToString()
        End If

        Dim iReturn As String = ""
        Select Case TypeOfReturn
            Case returnDate
                iReturn = tempMonthName + ", " + tempK.ToString()
            Case returnWeek
                iReturn = (Math.Ceiling(tempL / 7)).ToString()
            Case returnDay
                iReturn = tempL.ToString()
        End Select

        Return iReturn

    End Function

    Public Function CalculateCalendarDate(ByVal passedDay As Integer)

        'This function returns the calculated date for the day argument
        'of its cycle (not the Gregorian ordinal).
        'It is a modified version of GiveCalendarDate.

        'This function is only used for the week comments, hence...
        If passedDay > 364 Then
            passedDay = 365
        End If

        'Find which month has the leap day,
        'store it as tempJ
        Dim tempJ As Integer = 1
        For i = 1 To TotalMonths
            If storedMonths(i, storedLeap) = True Then
                tempJ = i
            End If
        Next

        Dim tempyear = MainDateYear

        Dim ShouldAddDay As Boolean = False
        Dim targetCycleStart As Integer = CycleStartDay
        Dim overflowAfter As Integer = DaysPerYear
        Dim CycleSum As Integer = 0
        Dim mustOverflow As Boolean = False
        If DateTime.IsLeapYear(tempyear) Then
            If CycleStartDay >= 60 Then
                targetCycleStart += 1
                If passedDay >= targetCycleStart Then
                    CycleSum = targetCycleStart
                    overflowAfter = DaysPerYear + 1 'but shouldn't
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'MUST
                    mustOverflow = True
                    ShouldAddDay = True
                End If
            Else
                If passedDay >= targetCycleStart Then
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear + 1 'but shouldn't
                    ShouldAddDay = True
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'MUST
                    mustOverflow = True
                End If
            End If
        Else
            If passedDay >= CycleStartDay Then
                If DateTime.IsLeapYear(tempyear + 1) Then
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'can't
                    If CycleStartDay >= 60 Then
                        'only add a leap if the cycle
                        'bleeds into the next year,
                        'which is leap
                        'and the cycle begins on or after
                        'March 1
                        ShouldAddDay = True
                    End If
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'can't
                End If
            Else
                If DateTime.IsLeapYear(tempyear - 1) Then
                    If CycleStartDay >= 60 Then
                        CycleSum = CycleStartDay + 1
                        overflowAfter = DaysPerYear + 1 'must
                        mustOverflow = True
                    Else
                        CycleSum = CycleStartDay
                        overflowAfter = DaysPerYear + 1
                        ShouldAddDay = True
                        If CycleStartDay > 1 Then
                            'only then it will bleed
                            mustOverflow = True
                        End If
                    End If
                Else
                    CycleSum = CycleStartDay
                    overflowAfter = DaysPerYear 'must if bleeds
                    If CycleStartDay > 1 Then
                        mustOverflow = True
                    End If
                End If
            End If
        End If

        Dim iFoundIt As Boolean = False

        Dim tempSum As Integer = 0  'this has to add up to tempToday = passedDay
        Dim tempI As Integer        'calculating month
        Dim tempK As Integer = 0    'calculated day
        Dim iLeapDay As Integer

        For tempI = 1 To TotalMonths

            'Add a leap day to the month being summed
            If tempI = tempJ And ShouldAddDay Then
                iLeapDay = 1
            Else
                iLeapDay = 0
            End If

            For iSum = 1 To storedMonths(tempI, storedDay) + iLeapDay
                tempSum += 1
                If tempSum = passedDay Then
                    tempK = iSum
                    'tempK is the day, tempI is the month
                    iFoundIt = True
                End If
            Next

            If iFoundIt Then
                Exit For
            End If

        Next

        Dim tempMonthName As String = storedMonths(tempI, storedName)

        If String.IsNullOrWhiteSpace(tempMonthName) Then
            tempMonthName = TextsInLanguage(36, CurrentLanguage) + " " + tempI.ToString()
        End If

        Return tempMonthName + ", " + tempK.ToString()

    End Function

End Module
