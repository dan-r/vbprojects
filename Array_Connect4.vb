Module Module1
    Dim table(6, 7) As String
    Dim selrow(7) As String
    Dim key As ConsoleKeyInfo
    Dim selrowindex As Integer = 1
    Dim turns As Integer = 1
    Dim currplayer As String = "X"
    Sub Main()
        Do

            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("      1   2   3   4   5   6   7" & vbCrLf & vbCrLf & "    ---------------------------")

            For i = 1 To 6
                Console.ForegroundColor = ConsoleColor.Red
                Console.Write((" " & i & " ").PadRight(4, " "))
                For x = 1 To 7
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.Write("|")

                    If table(i, x) = "X" Then
                        Console.ForegroundColor = ConsoleColor.DarkYellow
                    Else
                        Console.ForegroundColor = ConsoleColor.Green
                    End If

                    Console.Write(" " & (table(i, x) & " ").PadRight(2, " "))
                    Console.ForegroundColor = ConsoleColor.Gray

                Next
                Console.WriteLine(vbCrLf & "    ---------------------------")

            Next
            selrow(selrowindex) = currplayer
            Console.WriteLine("Turns: " & turns)

            Do
                Console.SetCursorPosition(4, 1)


                For i = 1 To 7
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.Write("|")

                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.Write(" " & (selrow(i) & " ").PadRight(2, " "))

                    Console.ForegroundColor = ConsoleColor.Gray



                Next
                key = Console.ReadKey
                Dim validsel As Boolean = False
                Select Case key.Key
                    Case ConsoleKey.LeftArrow
                        Do

                            selrow(selrowindex) = ""
                            selrowindex -= 1
                            If selrowindex > 0 Then
                                validsel = True
                            Else
                                validsel = False
                                selrowindex += 1
                            End If

                            selrow(selrowindex) = currplayer
                            Exit Select
                        Loop Until validsel

                    Case ConsoleKey.RightArrow
                        Do
                            selrow(selrowindex) = ""
                            selrowindex += 1
                            If selrowindex < 8 Then
                                validsel = True
                            Else
                                validsel = False
                                selrowindex -= 1
                            End If
                            selrow(selrowindex) = currplayer
                        Loop Until validsel
                        Exit Select
                    Case ConsoleKey.DownArrow
                        turn(currplayer, selrowindex)
                        Exit Do

                End Select
            Loop

            Console.Clear()
            wincheck()

            For i = 1 To 7
                selrow(i) = ""
            Next
            If currplayer = "X" Then
                currplayer = "O"
            Else
                currplayer = "X"
            End If
            turns += 1
        Loop


        Console.ReadKey()


    End Sub
    Sub turn(player, col)
        For i = 6 To 1 Step -1
            If table(i, col) = "" Then
                table(i, col) = player
                Exit For
            End If
        Next

    End Sub
    Sub wincheck()
        Dim last As String = ""
        Dim streak As Integer
        Dim streakplayer As String

        REM Horizontal win checking
        For i = 1 To 6

            For x = 1 To 7
                If last = table(i, x) Then
                    If Not table(i, x) = "" Then
                        streak += 1
                        streakplayer = table(i, x)
                        If streak = 3 Then
                            win()
                        End If
                    End If

                Else
                    streak = 0
                End If
                last = table(i, x)
            Next
        Next

        REM Vertical win check
        For x = 1 To 7

            For i = 1 To 6
                If last = table(i, x) Then
                    If Not table(i, x) = "" Then
                        streak += 1
                        streakplayer = table(i, x)
                        If streak = 3 Then
                            win()
                        End If
                    End If

                Else
                    streak = 0
                End If
                last = table(i, x)
            Next
        Next

        Dim currcoords(2) As Integer
        REM Diagonal checking
        For i = 1 To 6

            For x = 1 To 7
                currcoords = {i, x}
                last = ""
                Do
                    If currcoords(0) > 6 Then
                        Exit Do
                    End If
                    If currcoords(1) > 7 Then
                        Exit Do
                    End If
                    If currcoords(0) < 1 Then
                        Exit Do
                    End If
                    If currcoords(1) < 1 Then
                        Exit Do
                    End If
                    'Console.WriteLine(currcoords(0))
                    'Console.WriteLine(currcoords(1))
                    If last = table(currcoords(0), currcoords(1)) Then
                        If Not table(currcoords(0), currcoords(1)) = "" Then
                            streak += 1
                            streakplayer = table(currcoords(0), currcoords(1))
                            If streak = 3 Then
                                win()
                            End If
                        End If

                    Else
                        streak = 0
                    End If


                    last = table(currcoords(0), currcoords(1))
                    currcoords = {currcoords(0) - 1, currcoords(1) + 1}
                Loop
                
            Next
        Next
        For i = 1 To 6

            For x = 1 To 7
                currcoords = {i, x}
                last = ""
                Do
                    If currcoords(0) > 6 Then
                        Exit Do
                    End If
                    If currcoords(1) > 7 Then
                        Exit Do
                    End If
                    If currcoords(0) < 1 Then
                        Exit Do
                    End If
                    If currcoords(1) < 1 Then
                        Exit Do
                    End If
                    
                    If last = table(currcoords(0), currcoords(1)) Then
                        If Not table(currcoords(0), currcoords(1)) = "" Then
                            streak += 1
                            streakplayer = table(currcoords(0), currcoords(1))
                            If streak = 3 Then
                                win()
                            End If
                        End If

                    Else
                        streak = 0
                    End If


                    last = table(currcoords(0), currcoords(1))
                    currcoords = {currcoords(0) - 1, currcoords(1) - 1}
                Loop

            Next
        Next
    End Sub
    Sub win()
        Console.WriteLine(currplayer & " won!")
        Console.ReadKey()
        End
    End Sub

End Module
