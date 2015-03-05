Public Sub GimVicUploader()
Set wkb = ActiveSheet
Dim fileName As String
Dim MaxCols As Integer
fileName = "c:\Jedilnik\jedilnik.csv"
'Application.GetSaveAsFilename("", "CSV File (*.csv), *.csv")

If fileName = "False" Then
End
End If

On Error GoTo eh
Const adTypeText = 2
Const adSaveCreateOverWrite = 2

Dim BinaryStream
Set BinaryStream = CreateObject("ADODB.Stream")
BinaryStream.Charset = "UTF-8"
BinaryStream.Type = adTypeText
BinaryStream.Open

For r = 1 To 40
s = ""
c = 1
While c < 7
s = s & wkb.Cells(r, c).Value & ";"
c = c + 1
Wend
BinaryStream.WriteText s, 1
Next r

BinaryStream.SaveToFile fileName, adSaveCreateOverWrite
BinaryStream.Close

Shell "C:\Jedilnik\JedilnikUploader.exe", vbMaximizedFocus

eh:

End Sub

