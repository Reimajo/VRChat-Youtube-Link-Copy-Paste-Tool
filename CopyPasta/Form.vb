Imports System.Runtime.InteropServices
''' <summary>
''' Applikation made by KeksTheFurry to copy youtube links into VRChat.
''' 
''' Update: Has full support for all applications like browsers and vrchat now. 
''' Also supports fpsVR hotkeys, so you never need to leave VR anymore. 
''' Simply have a browser with youtube open and this application running and you can press the fpsVR Utilities 
''' button that says "Ctrl+Shift+3" on the bottem left. Your browser with youtube will appear on the desktop screen. 
''' You then have 5 seconds to select the URL which will then get postet into the textbox that you've selected in VRChat before. 
''' Turn off Auto-Post to select the input box afterwards. Use Ctrl+Shift+1 to just open the GET-Application (e.g. Youtube) 
''' and Ctrl+Shift+2 to show the program.
''' 
''' Sorry for the mixed german and english source-code, I started to translate some of it but eventually stopped doing so due to the lack
''' of third party programmers so far.
''' </summary>
Public Class Form
    ''' <summary>
    ''' Wird beim Programmstart aufgerufen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBoxAnzeigeDelay.Text = CStr(CDbl(delay) / 1000)
        SetTextToAllPOSTTextboxes("VRChat")
        SetTextToAllGETTextboxes("Youtube")
        Clipboard.Clear()
        Call RegisterHotkey()
        Dim r As Rectangle
        If Parent IsNot Nothing Then
            r = Parent.RectangleToScreen(Parent.ClientRectangle)
        Else
            r = Screen.FromPoint(Me.Location).WorkingArea
        End If
        Dim x = r.Left + (r.Width - Me.Width) \ 2
        Dim y = r.Top + (r.Height - Me.Height) \ 2
        Me.Location = New Point(x, y)
    End Sub
    ''' <summary>
    ''' Repräsentiert den aktuellen Fortschritt
    ''' </summary>
    Dim aktuellerFortschritt As Integer
    ''' <summary>
    ''' Zeigt den aktuellen Fortschritt in der ProgressBar an
    ''' </summary>
    Private Sub ZeigeProzess()
        ProgressBar.Value = aktuellerFortschritt
        ProgressBar.Update()
    End Sub
    '-------------------------------------- Interaktionselemente --------------------------------------------------
    'Zeile 1
    Private Sub ButtonGET1_Click(sender As Object, e As EventArgs) Handles ButtonGET1.Click
        TextBoxAnzeige1.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET1.Text, windowNamePOST:=TextBoxTaskPOST1.Text, buttonObj:=ButtonGET1)
    End Sub
    Private Sub ButtonPOST1_Click(sender As Object, e As EventArgs) Handles ButtonPOST1.Click
        Call SendeAnApplikation(nachricht:=TextBoxAnzeige1.Text, windowNamePOST:=TextBoxTaskPOST1.Text, buttonObj:=ButtonPOST1, withDelay:=True)
    End Sub
    'Zeile 2
    Private Sub ButtonGET2_Click(sender As Object, e As EventArgs) Handles ButtonGET2.Click
        TextBoxAnzeige2.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET2.Text, windowNamePOST:=TextBoxTaskPOST2.Text, buttonObj:=ButtonGET2)
    End Sub
    Private Sub ButtonPOST2_Click(sender As Object, e As EventArgs) Handles ButtonPOST2.Click
        Call SendeAnApplikation(nachricht:=TextBoxAnzeige2.Text, windowNamePOST:=TextBoxTaskPOST2.Text, buttonObj:=ButtonPOST2, withDelay:=True)
    End Sub
    'Zeile 3
    Private Sub ButtonGET3_Click(sender As Object, e As EventArgs) Handles ButtonGET3.Click
        TextBoxAnzeige3.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET3.Text, windowNamePOST:=TextBoxTaskPOST3.Text, buttonObj:=ButtonGET3)
    End Sub
    Private Sub ButtonPOST3_Click(sender As Object, e As EventArgs) Handles ButtonPOST3.Click
        Call SendeAnApplikation(nachricht:=TextBoxAnzeige3.Text, windowNamePOST:=TextBoxTaskPOST3.Text, buttonObj:=ButtonPOST3, withDelay:=True)
    End Sub
    'Zeile 4
    Private Sub ButtonGET4_Click(sender As Object, e As EventArgs) Handles ButtonGET4.Click
        TextBoxAnzeige4.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET4.Text, windowNamePOST:=TextBoxTaskPOST4.Text, buttonObj:=ButtonGET4)
    End Sub
    Private Sub ButtonPOST4_Click(sender As Object, e As EventArgs) Handles ButtonPOST4.Click
        Call SendeAnApplikation(nachricht:=TextBoxAnzeige4.Text, windowNamePOST:=TextBoxTaskPOST4.Text, buttonObj:=ButtonPOST4, withDelay:=True)
    End Sub
    'Zeile 5
    Private Sub ButtonGET5_Click(sender As Object, e As EventArgs) Handles ButtonGET5.Click
        TextBoxAnzeige5.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET5.Text, windowNamePOST:=TextBoxTaskPOST5.Text, buttonObj:=ButtonGET5)
    End Sub
    Private Sub ButtonPOST5_Click(sender As Object, e As EventArgs) Handles ButtonPOST5.Click
        Call SendeAnApplikation(nachricht:=TextBoxAnzeige5.Text, windowNamePOST:=TextBoxTaskPOST5.Text, buttonObj:=ButtonPOST5, withDelay:=True)
    End Sub
    '-----------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Holt den markierten Text von einer Applikation
    ''' </summary>
    ''' <param name="nachricht"></param>
    ''' <param name="windowNamePOST"></param>
    ''' <param name="buttonObj"></param>
    Private Sub SendeAnApplikation(nachricht As String, windowNamePOST As String, buttonObj As Button, withDelay As Boolean)
        If Not IsNothing(nachricht) AndAlso nachricht <> "" Then
            buttonObj.Enabled = False
            buttonObj.BackColor = Color.Green
            buttonObj.Update()
            SetFocusToWindow(windowNamePOST)
            If withDelay Then
                aktuellerFortschritt = 100
                While (aktuellerFortschritt > 0)
                    Threading.Thread.Sleep(CInt(delay / 10))
                    aktuellerFortschritt -= 10
                    ZeigeProzess()
                End While
            End If
            Clipboard.SetText(nachricht)
            Threading.Thread.Sleep(100)
            SendKeys.SendWait("^v")
            Threading.Thread.Sleep(700)
            SendKeys.SendWait("{Enter}")
            buttonObj.Enabled = True
            buttonObj.BackColor = Color.Gray
        Else
            Clipboard.Clear()
        End If
    End Sub
    ''' <summary>
    ''' Sendet den Text der TextBox an eine Applikation
    ''' </summary>
    ''' <param name="windowNameGET"></param>
    ''' <param name="buttonObj"></param>
    ''' <returns></returns>
    Private Function HoleVonApplikation(windowNameGET As String, windowNamePOST As String, buttonObj As Button) As String
        buttonObj.Enabled = False
        buttonObj.BackColor = Color.Green
        buttonObj.Update()
        SetFocusToWindow(windowNameGET)
        aktuellerFortschritt = 100
        While (aktuellerFortschritt > 0)
            Threading.Thread.Sleep(CInt(delay / 10))
            aktuellerFortschritt -= 10
            ZeigeProzess()
        End While
        SendKeys.SendWait("^c")
        Threading.Thread.Sleep(300)
        Try
            HoleVonApplikation = Clipboard.GetText.ToString()
        Catch
            HoleVonApplikation = ""
        End Try
        buttonObj.Enabled = True
        buttonObj.BackColor = Color.Gray
        If ButtonAutoPost.Text = "Auto-POST: ON" Then
            SendeAnApplikation(nachricht:=HoleVonApplikation, windowNamePOST:=windowNamePOST, buttonObj:=buttonObj, withDelay:=False)
        Else
            Call ActivateMyself()
        End If
    End Function
    ''' <summary>
    ''' Setzt den Fokus auf ein aktives Fenster welches den namen windowNamePOST enthält
    ''' </summary>
    ''' <param name="windowName">Name des aktiven Fensters einer laufenden Applikation</param>
    Private Function SetFocusToWindow(windowName As String) As Boolean
        SetFocusToWindow = False
        Dim searchName As String = Trim(Replace(windowName.ToUpper, " ", ""))
        If searchName <> "" Then
            For Each OneProcess As Process In Process.GetProcesses
                Dim windowTitle As String = Replace(OneProcess.MainWindowTitle.ToUpper, " ", "")
                If Not IsNothing(windowTitle) AndAlso windowTitle <> "" Then
                    Debug.Print(windowTitle)
                    If windowTitle.Contains(searchName) Then
                        Try
                            AppActivateMaximized(OneProcess.MainWindowTitle)
                            SetFocusToWindow = True
                            Exit Function
                        Catch
                        End Try
                    End If
                    Try
                        AppActivateMaximized(windowName)
                        SetFocusToWindow = True
                        Exit Function
                    Catch
                        SetFocusToWindow = False
                    End Try
                End If
            Next
            If windowName.ToLower = "youtube" Then
                Dim webAddress As String = "https://www.youtube.com/"
                Process.Start(webAddress)
            End If
        End If
    End Function
    ''' <summary>
    ''' Aktiviert die eigene Applikation
    ''' </summary>
    Private Sub ActivateMyself()
        Try
            AppActivateMaximized("CopyPasta by KeksTheFurry")
        Catch
        End Try
    End Sub

    Private Sub AppActivateMaximized(windowName As String)
        AppActivate(windowName)
        Dim hwnd As Integer = API.GetFirstWindowHandle(windowName)
        Try 'Ich vertraue keinen third-party Klassen
            If API.IsMinimized(hwnd) Then
                API.ShowWindow(hwnd)
            End If
        Catch
        End Try
    End Sub
    '-------------------------- Funktionen zum Bewegen des Forms -----------------------------------
    ''' <summary>
    ''' Beim MouseDown Event wird mouseDownBool auf true gesetzt und die aktuelle Position als lastLocation gespeichert
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PanelForMoving_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PanelForMoving.MouseDown
        mouseDownBool = True
        lastLocation = e.Location
    End Sub
    Dim mouseDownBool As Boolean = False
    Dim lastLocation As Point
    ''' <summary>
    ''' Beim MouseMove Events wird - sofern mouseDownBool true ist - das Form um die bisher zurückgelegte Differenz zu lastLocation bewegt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PanelForMoving_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PanelForMoving.MouseMove
        If (mouseDownBool) Then
            Me.Location = New Point(
                (Me.Location.X - lastLocation.X) + e.X,
                (Me.Location.Y - lastLocation.Y) + e.Y)
            Me.Update()
        End If
    End Sub
    ''' <summary>
    ''' Beim MouseUp Event wird mouseDownBool auf false gesetzt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PanelForMoving_MouseUp(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PanelForMoving.MouseUp
        mouseDownBool = False
    End Sub
    '-----------------------------------------------------------------------------------------------
    ''' <summary>
    ''' Beendet die Applikation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonApplikationBeenden_Click(sender As Object, e As EventArgs) Handles ButtonApplikationBeenden.Click
        Me.Close()
    End Sub
    '----------------------------Funktionen zum Steuern des Delays--------------------------------------------------
    ''' <summary>
    ''' Das eingestellte Delay in Millisekunden
    ''' </summary>
    Dim delay As Integer = 5000
    ''' <summary>
    ''' Vergrößert das Delay um 100 Millisekunden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonDelayPlus_Click(sender As Object, e As EventArgs) Handles ButtonDelayPlus.Click
        Dim delayNeu As Integer = CInt(CDbl(TextBoxAnzeigeDelay.Text) * 1000)
        delayNeu += 100
        delay = delayNeu
        TextBoxAnzeigeDelay.Text = CStr(CDbl(delay) / 1000)
    End Sub
    ''' <summary>
    ''' Verkleinert das Delay um 100 Millisekunden
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonDelayMinus_Click(sender As Object, e As EventArgs) Handles ButtonDelayMinus.Click
        Dim delayNeu As Integer = CInt(CDbl(TextBoxAnzeigeDelay.Text) * 1000)
        delayNeu -= 100
        If delayNeu >= 100 Then
            delay = delayNeu
            TextBoxAnzeigeDelay.Text = CStr(CDbl(delay) / 1000)
        End If
    End Sub
    ''' <summary>
    ''' Setzt die Eingabe der obersten linken TextBox in alle TextBoxen darunter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCopyGetDown_Click(sender As Object, e As EventArgs) Handles ButtonCopyGetDown.Click
        TextBoxTaskGET1.Text = Trim(TextBoxTaskGET1.Text)
        Dim inhalt As String = TextBoxTaskGET1.Text
        SetTextToAllGETTextboxes(inhalt)
    End Sub
    Private Sub SetTextToAllGETTextboxes(inhalt As String)
        TextBoxTaskGET1.Text = inhalt
        TextBoxTaskGET2.Text = inhalt
        TextBoxTaskGET3.Text = inhalt
        TextBoxTaskGET4.Text = inhalt
        TextBoxTaskGET5.Text = inhalt
    End Sub
    ''' <summary>
    ''' Setzt die Eingabe der obersten rechten TextBox in alle TextBoxen darunter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ButtonCopyPostDown_Click(sender As Object, e As EventArgs) Handles ButtonCopyPostDown.Click
        TextBoxTaskPOST1.Text = Trim(TextBoxTaskPOST1.Text)
        Dim inhalt As String = TextBoxTaskPOST1.Text
        SetTextToAllPOSTTextboxes(inhalt)
    End Sub
    Private Sub SetTextToAllPOSTTextboxes(inhalt As String)
        TextBoxTaskPOST1.Text = inhalt
        TextBoxTaskPOST2.Text = inhalt
        TextBoxTaskPOST3.Text = inhalt
        TextBoxTaskPOST4.Text = inhalt
        TextBoxTaskPOST5.Text = inhalt
    End Sub

    Private Sub ButtonAutoPost_Click(sender As Object, e As EventArgs) Handles ButtonAutoPost.Click
        If ButtonAutoPost.Text = "Auto-POST: ON" Then
            ButtonAutoPost.Text = "Auto-POST: OFF"
        Else
            ButtonAutoPost.Text = "Auto-POST: ON"
        End If
    End Sub
    '---------------------------- Hotkey support für fpsVR --------------------------
    Public Const MOD_CONTROL As Integer = &H2
    Public Const MOD_SHIFT As Integer = &H4
    Public Const WM_HOTKEY As Integer = &H312
    ''' <summary>
    ''' Registriert einen neuen Hotkey
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <param name="id"></param>
    ''' <param name="fsModifiers"></param>
    ''' <param name="vk"></param>
    ''' <returns></returns>
    <DllImport("User32.dll")>
    Public Shared Function RegisterHotKey(ByVal hwnd As IntPtr,
                                      ByVal id As Integer, ByVal fsModifiers As Integer,
                                      ByVal vk As Integer) As Integer
    End Function
    ''' <summary>
    ''' Entfernt einen registrierten Hotkey
    ''' </summary>
    ''' <param name="hwnd"></param>
    ''' <param name="id"></param>
    ''' <returns></returns>
    <DllImport("User32.dll")>
    Public Shared Function UnregisterHotKey(ByVal hwnd As IntPtr,
                ByVal id As Integer) As Integer
    End Function
    ''' <summary>
    ''' Registriert alle Hotkeys
    ''' </summary>
    Private Sub RegisterHotkey()
        RegisterHotKey(Me.Handle, 100, MOD_CONTROL Or MOD_SHIFT, Keys.D3)
        RegisterHotKey(Me.Handle, 200, MOD_CONTROL Or MOD_SHIFT, Keys.D2)
        RegisterHotKey(Me.Handle, 300, MOD_CONTROL Or MOD_SHIFT, Keys.D1)
    End Sub
    ''' <summary>
    ''' Wenn die Tastenkombination gedrückt wird
    ''' </summary>
    ''' <param name="m"></param>
    Protected Overrides Sub DefWndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.DefWndProc(m)
        If m.Msg = WM_HOTKEY Then
            Select Case CType(m.WParam, Integer)
                Case 100
                    TextBoxAnzeige1.Text = HoleVonApplikation(windowNameGET:=TextBoxTaskGET1.Text, windowNamePOST:=TextBoxTaskPOST1.Text, buttonObj:=ButtonGET1)
                Case 200
                    ActivateMyself()
                Case 300
                    SetFocusToWindow(TextBoxTaskGET1.Text)
            End Select
        End If
    End Sub
    ''' <summary>
    ''' Beim Schließen wird der Hotkey entfernt
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Form_FormClosing(ByVal sender As System.Object,
                        ByVal e As System.Windows.Forms.FormClosingEventArgs) _
                        Handles MyBase.FormClosing
        UnregisterHotKey(Me.Handle, 100)
        UnregisterHotKey(Me.Handle, 200)
        UnregisterHotKey(Me.Handle, 300)
    End Sub
    ''' <summary>
    ''' Öffnet YouTube
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim webAddress As String = "https://www.youtube.com/"
        Process.Start(webAddress)
    End Sub
End Class









''' <summary>
''' Class from Bill ( https://stackoverflow.com/a/2171744 ) to maximize minimized applications
''' </summary>
Public Class API

    Private Declare Function apiGetTopWindow Lib "user32" Alias "GetTopWindow" (ByVal hwnd As Integer) As Integer
    Private Declare Function apiGetDesktopWindow Lib "user32" Alias "GetDesktopWindow" () As Integer
    Private Declare Function apiGetWindow Lib "user32" Alias "GetWindow" (ByVal hwnd As Integer, ByVal wCmd As Integer) As Integer
    Private Declare Function apiGetWindowTextLength Lib "user32" Alias "GetWindowTextLengthA" (ByVal hwnd As Integer) As Integer
    Private Declare Function apiGetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hwnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer
    Private Declare Function apiShowWindow Lib "user32" Alias "ShowWindow" (ByVal hwnd As IntPtr, ByVal nCmdShow As Integer) As Integer
    Private Declare Function apiIsIconic Lib "user32" Alias "IsIconic" (ByVal hwnd As IntPtr) As Boolean

    Private Const GW_HWNDNEXT As Integer = 2
    Private Const SW_NORMAL As Integer = 1

    Public Shared Function GetFirstWindowHandle(ByVal sStartingWith As String) As Integer

        Dim hwnd As Integer
        Dim sWindowName As String

        Dim iHandle As Integer = 0

        hwnd = apiGetTopWindow(apiGetDesktopWindow)

        Do While hwnd <> 0
            sWindowName = zGetWindowName(hwnd)
            If sWindowName.StartsWith(sStartingWith) Then
                iHandle = hwnd
                Exit Do
            End If
            hwnd = apiGetWindow(hwnd, GW_HWNDNEXT)
        Loop

        Return iHandle

    End Function

    Public Shared Function IsMinimized(ByVal hwnd As Integer) As Boolean

        Dim ip As New IntPtr(hwnd)

        Return apiIsIconic(ip)

    End Function

    Public Shared Sub ShowWindow(ByVal hwnd As Integer)

        Dim ip As New IntPtr(hwnd)

        apiShowWindow(ip, SW_NORMAL)

    End Sub

    Private Shared Function zGetWindowName(ByVal hWnd As Integer) As String

        Dim nBufferLength As Integer
        Dim nTextLength As Integer
        Dim sName As String

        nBufferLength = apiGetWindowTextLength(hWnd) + 4
        sName = New String(" "c, nBufferLength)

        nTextLength = apiGetWindowText(hWnd, sName, nBufferLength)
        sName = sName.Substring(0, nTextLength)

        Return sName

    End Function

End Class