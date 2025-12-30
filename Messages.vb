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
Module Messages

    'This module contains all hardcoded messages and texts in English and Spanish

    Public Const lgEnglish As Integer = 0
    Public Const lgSpanish As Integer = 1
    Public CurrentLanguage As Integer = lgEnglish

    '(Message/Text number, Language)
    Public TextsInLanguage(100, 1) As String

    Public Sub Load_LanguageMessages()

        TextsInLanguage(0, 0) = "Unnamed"
        TextsInLanguage(0, 1) = "Sin nombre"

        TextsInLanguage(1, 0) = "This calendar has been set as the default calendar."
        TextsInLanguage(1, 1) = "Este calendario ha sido establecido como el calendario por defecto."

        TextsInLanguage(2, 0) = "Days to distribute: "
        TextsInLanguage(2, 1) = "Días a distribuir: "

        TextsInLanguage(3, 0) = "January"
        TextsInLanguage(3, 1) = "Enero"
        TextsInLanguage(4, 0) = "February"
        TextsInLanguage(4, 1) = "Febrero"
        TextsInLanguage(5, 0) = "March"
        TextsInLanguage(5, 1) = "Marzo"
        TextsInLanguage(6, 0) = "April"
        TextsInLanguage(6, 1) = "Abril"
        TextsInLanguage(7, 0) = "May"
        TextsInLanguage(7, 1) = "Mayo"
        TextsInLanguage(8, 0) = "June"
        TextsInLanguage(8, 1) = "Junio"
        TextsInLanguage(9, 0) = "July"
        TextsInLanguage(9, 1) = "Julio"
        TextsInLanguage(10, 0) = "August"
        TextsInLanguage(10, 1) = "Agosto"
        TextsInLanguage(11, 0) = "September"
        TextsInLanguage(11, 1) = "Septiembre"
        TextsInLanguage(12, 0) = "October"
        TextsInLanguage(12, 1) = "Octubre"
        TextsInLanguage(13, 0) = "November"
        TextsInLanguage(13, 1) = "Noviembre"
        TextsInLanguage(14, 0) = "December"
        TextsInLanguage(14, 1) = "Diciembre"

        TextsInLanguage(15, 0) = "Add a name"
        TextsInLanguage(15, 1) = "Agregue un nombre"
        TextsInLanguage(16, 0) = "Gregorian ordinal"
        TextsInLanguage(16, 1) = "Ordinal gregoriano"

        TextsInLanguage(17, 0) = "Week"
        TextsInLanguage(17, 1) = "Semana"
        TextsInLanguage(18, 0) = "Ordinal day"
        TextsInLanguage(18, 1) = "Día ordinal"

        'Reassigned

        TextsInLanguage(19, 0) = "List start"
        TextsInLanguage(19, 1) = "Comienzo del listado"

        TextsInLanguage(20, 0) = "Coming dates"
        TextsInLanguage(20, 1) = "Fechas siguientes"
        TextsInLanguage(21, 0) = "Weekly notes"
        TextsInLanguage(21, 1) = "Notas semanales"
        'Reassigned
        TextsInLanguage(22, 0) = "Cycle status"
        TextsInLanguage(22, 1) = "Estado del ciclo"

        TextsInLanguage(23, 0) = "Year design"
        TextsInLanguage(23, 1) = "Diseño anual"

        'reassigned

        TextsInLanguage(24, 0) = "Today"
        TextsInLanguage(24, 1) = "Hoy"

        TextsInLanguage(25, 0) = "There was a problem saving the configuration file."
        TextsInLanguage(25, 1) = "Hubo un problema al guardar el archivo de configuración."
        TextsInLanguage(26, 0) = "This calendar is not properly set. Please open another or use the editor."
        TextsInLanguage(26, 1) = "Este calendario no está configurado correctamente. Por favor abra otro o use el editor."
        TextsInLanguage(27, 0) = "couldn't be opened."
        TextsInLanguage(27, 1) = "no se pudo abrir."
        TextsInLanguage(28, 0) = "Cannot calculate dates unless the months add up to 365 days."
        TextsInLanguage(28, 1) = "No se pueden calcular fechas si los meses no suman 365 días."

        TextsInLanguage(29, 0) = "Open calendar"
        TextsInLanguage(29, 1) = "Abrir calendario"
        TextsInLanguage(30, 0) = MainTitle + " files (*." + AppFilesExtension + ")|*." + AppFilesExtension + "|All files (*.*)|*.*"
        TextsInLanguage(30, 1) = "Archivos de " + MainTitle + " (*." + AppFilesExtension + ")|*." + AppFilesExtension + "|Todos los archivos (*.*)|*.*"

        TextsInLanguage(31, 0) = "Please add at least one month to this calendar before saving."
        TextsInLanguage(31, 1) = "Por favor agregue al menos un mes a este calendario antes de guardar."
        TextsInLanguage(32, 0) = "Save calendar"
        TextsInLanguage(32, 1) = "Guardar calendario"
        TextsInLanguage(33, 0) = MainTitle + " files (*." + AppFilesExtension + ")|*." + AppFilesExtension
        TextsInLanguage(33, 1) = "Archivos de " + MainTitle + " (*." + AppFilesExtension + ")|*." + AppFilesExtension

        'Redundant index 34 reused below

        TextsInLanguage(35, 0) = "Save this calendar before setting it as default."
        TextsInLanguage(35, 1) = "Guarde este calendario antes de establecerlo como calendario por defecto."
        TextsInLanguage(36, 0) = "Month"
        TextsInLanguage(36, 1) = "Mes"

        ' Editor static labels and buttons
        TextsInLanguage(37, 0) = "First day of cycle"
        TextsInLanguage(37, 1) = "Primer día del ciclo"
        TextsInLanguage(38, 0) = "Month name"
        TextsInLanguage(38, 1) = "Nombre del mes"
        TextsInLanguage(39, 0) = "Days"
        TextsInLanguage(39, 1) = "Días"
        TextsInLanguage(40, 0) = "Leap day"
        TextsInLanguage(40, 1) = "Día bisiesto"
        TextsInLanguage(41, 0) = "Begins on"
        TextsInLanguage(41, 1) = "Empieza en"
        TextsInLanguage(42, 0) = "&Add a month"
        TextsInLanguage(42, 1) = "&Agregar un mes"

        'Reassigned
        TextsInLanguage(43, 0) = "Hold CTRL while scrolling to skip to the next notes."
        TextsInLanguage(43, 1) = "Presione CTRL mientras se desplaza para saltar a las siguientes notas."

        'Menus
        TextsInLanguage(44, 0) = "&View"
        TextsInLanguage(44, 1) = "&Ver"
        TextsInLanguage(45, 0) = "&Calendar"
        TextsInLanguage(45, 1) = "&Calendario"
        TextsInLanguage(46, 0) = "&Editor"
        TextsInLanguage(46, 1) = "&Editor"
        TextsInLanguage(47, 0) = "&Language"
        TextsInLanguage(47, 1) = "&Idioma"

        'Reassigned from above
        TextsInLanguage(34, 0) = "A&bout..."
        TextsInLanguage(34, 1) = "Acerca &de..."

        TextsInLanguage(48, 0) = "&File"
        TextsInLanguage(48, 1) = "A&rchivo"
        TextsInLanguage(49, 0) = "&New calendar"
        TextsInLanguage(49, 1) = "&Nuevo calendario"
        TextsInLanguage(50, 0) = "&Open calendar..."
        TextsInLanguage(50, 1) = "Abrir calendari&o..."
        TextsInLanguage(51, 0) = "Set as &default"
        TextsInLanguage(51, 1) = "Establecer como calendario por &defecto"
        TextsInLanguage(52, 0) = "&Save"
        TextsInLanguage(52, 1) = "&Guardar"
        TextsInLanguage(53, 0) = "Save &as..."
        TextsInLanguage(53, 1) = "Guardar &como..."
        TextsInLanguage(54, 0) = "&Exit"
        TextsInLanguage(54, 1) = "&Salir"

        'Reassigned
        TextsInLanguage(55, 0) = "Today is"
        TextsInLanguage(55, 1) = "Hoy es  "

        TextsInLanguage(56, 0) = "About"
        TextsInLanguage(56, 1) = "Acerca de"
        TextsInLanguage(57, 0) = "Version " & AppCurrentVersion
        TextsInLanguage(57, 1) = "Versión " & AppCurrentVersion

        'Reassigned
        TextsInLanguage(58, 0) = " (expand)"
        TextsInLanguage(58, 1) = " (expandir)"

        TextsInLanguage(59, 0) = "This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version." & ControlChars.CrLf & ControlChars.CrLf & "This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details."
        TextsInLanguage(59, 1) = TextsInLanguage(59, 0)

        TextsInLanguage(60, 0) = "https://www.gnu.org/licenses/gpl-3.0.html"
        TextsInLanguage(60, 1) = TextsInLanguage(60, 0)

        TextsInLanguage(61, 0) = "(next notes)"
        TextsInLanguage(61, 1) = "(notas siguientes)"

        TextsInLanguage(62, 0) = "(week "
        TextsInLanguage(62, 1) = "(sem. "

        TextsInLanguage(63, 0) = "(notes from week "
        TextsInLanguage(63, 1) = "(notas de la semana "

        TextsInLanguage(64, 0) = "There was a problem opening the configuration file."
        TextsInLanguage(64, 1) = "Se encontró un problema al abrir el archivo de configuración."

        TextsInLanguage(65, 0) = "&Recent"
        TextsInLanguage(65, 1) = "&Recientes"

        TextsInLanguage(66, 0) = " (d)"
        TextsInLanguage(66, 1) = TextsInLanguage(66, 0)

    End Sub

    Public Sub RefreshStaticTexts()

        'This sub refreshes all static labels, buttons, comboboxes, the title, and menu labels + shortcut keys
        'Also refreshes the "add a name" labels on month comboboxes

        MainViewForm.LabelMonthName.Text = TextsInLanguage(38, CurrentLanguage)
        MainViewForm.LabelDays.Text = TextsInLanguage(39, CurrentLanguage)
        MainViewForm.LabelLeap.Text = TextsInLanguage(40, CurrentLanguage)
        MainViewForm.LabelTitleGregorian.Text = TextsInLanguage(41, CurrentLanguage)
        MainViewForm.ButtonAddMonth.Text = TextsInLanguage(42, CurrentLanguage)

        MainViewForm.ViewToolStripMenuItem.Text = TextsInLanguage(44, CurrentLanguage)
        MainViewForm.MenuViewCalendar.Text = TextsInLanguage(45, CurrentLanguage)
        MainViewForm.MenuViewEditor.Text = TextsInLanguage(46, CurrentLanguage)

        MainViewForm.LanguageToolStripMenuItem.Text = TextsInLanguage(47, CurrentLanguage)
        MainViewForm.AboutToolStripMenuItem.Text = TextsInLanguage(34, CurrentLanguage)

        MainViewForm.FileToolStripMenuItem.Text = TextsInLanguage(48, CurrentLanguage)
        MainViewForm.NewToolStripMenuItem.Text = TextsInLanguage(49, CurrentLanguage)

        MainViewForm.OpenToolStripMenuItem.Text = TextsInLanguage(50, CurrentLanguage)
        If CurrentLanguage = 0 Then
            MainViewForm.OpenToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.O
        Else
            MainViewForm.OpenToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.A
        End If
        MainViewForm.OpenToolStripMenuItem.ShowShortcutKeys = True

        MainViewForm.RecentToolStripMenuItem.Text = TextsInLanguage(65, CurrentLanguage)

        MainViewForm.SetAsDefaultToolStripMenuItem.Text = TextsInLanguage(51, CurrentLanguage)
        MainViewForm.SaveToolStripMenuItem.Text = TextsInLanguage(52, CurrentLanguage)
        If CurrentLanguage = 0 Then
            MainViewForm.SaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.S
        Else
            MainViewForm.SaveToolStripMenuItem.ShortcutKeys = Keys.Control Or Keys.G
        End If
        MainViewForm.SaveToolStripMenuItem.ShowShortcutKeys = True
        MainViewForm.SaveAsToolStripMenuItem.Text = TextsInLanguage(53, CurrentLanguage)
        MainViewForm.ExitToolStripMenuItem.Text = TextsInLanguage(54, CurrentLanguage)

        If MainViewForm.GroupBox1.Height = Val(MainViewForm.GroupBox1.Tag) Then
            MainViewForm.GroupBox1.Text = TextsInLanguage(19, CurrentLanguage)
        Else
            MainViewForm.GroupBox1.Text = TextsInLanguage(19, CurrentLanguage) + TextsInLanguage(58, CurrentLanguage)
        End If
        If MainViewForm.GroupBox2.Height = Val(MainViewForm.GroupBox2.Tag) Then
            MainViewForm.GroupBox2.Text = TextsInLanguage(20, CurrentLanguage)
        Else
            MainViewForm.GroupBox2.Text = TextsInLanguage(20, CurrentLanguage) + TextsInLanguage(58, CurrentLanguage)
        End If
        If MainViewForm.GroupBox3.Height = Val(MainViewForm.GroupBox3.Tag) Then
            MainViewForm.GroupBox3.Text = TextsInLanguage(21, CurrentLanguage)
        Else
            MainViewForm.GroupBox3.Text = TextsInLanguage(21, CurrentLanguage) + TextsInLanguage(58, CurrentLanguage)
        End If
        If MainViewForm.GroupBox4.Height = Val(MainViewForm.GroupBox4.Tag) Then
            MainViewForm.GroupBox4.Text = TextsInLanguage(22, CurrentLanguage)
        Else
            MainViewForm.GroupBox4.Text = TextsInLanguage(22, CurrentLanguage) + TextsInLanguage(58, CurrentLanguage)
        End If
        MainViewForm.GroupBox5.Text = TextsInLanguage(23, CurrentLanguage)
        MainViewForm.GroupBox6.Text = TextsInLanguage(37, CurrentLanguage)

        MainViewForm.CheckStartToday.Text = TextsInLanguage(24, CurrentLanguage)

        MainViewForm.ToolTip1.SetToolTip(MainViewForm.ComboWeekSelect, TextsInLanguage(43, CurrentLanguage))

        'Rename month names in the cycle beginning section
        For i = 0 To 11
            MainViewForm.ComboCycleMonth.Items(i) = TextsInLanguage(3 + i, CurrentLanguage)
            MainViewForm.ComboStartMonth.Items(i) = TextsInLanguage(3 + i, CurrentLanguage)
        Next

        If CurrentFile = "" Then
            MainViewForm.Text = MainTitle + " - " + TextsInLanguage(0, CurrentLanguage)
        Else
            MainViewForm.Text = MainTitle + " - " + CurrentFile
        End If

        If TotalMonths > 0 Then
            For i = 1 To TotalMonths
                Dim foundControlsA() As Control = MainViewForm.MonthsPanel.Controls.Find("MonthName" + i.ToString(), False)
                Dim foundControlA As Control = foundControlsA(0)
                Dim tempMonthName As TextBox = DirectCast(foundControlA, TextBox)
                tempMonthName.PlaceholderText = TextsInLanguage(15, CurrentLanguage)
            Next
        End If
    End Sub

End Module
