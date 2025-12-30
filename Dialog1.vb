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

Imports System.Security.Policy
Imports System.Windows.Forms

Public Class Dialog1

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As Object, ByVal e As EventArgs)
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub Dialog1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = TextsInLanguage(56, CurrentLanguage) & " " & MainTitle
        Me.LabelProductName.Text = MainTitle
        Me.LabelVersion.Text = TextsInLanguage(57, CurrentLanguage) & " © 2025 Irramárrima. "
        Me.LabelCopyright.Text = TextsInLanguage(59, CurrentLanguage)
        Me.LabelDeedLink.Text = TextsInLanguage(60, CurrentLanguage)
    End Sub

    Private Sub LabelDeedLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LabelDeedLink.LinkClicked
        Dim url As String = TextsInLanguage(60, CurrentLanguage)
        Process.Start(New ProcessStartInfo(url) With {
            .UseShellExecute = True
        })
    End Sub
End Class
