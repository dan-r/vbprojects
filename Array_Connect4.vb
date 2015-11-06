Module Module1
    Dim table(6, 7) As String ' Stores the game grid
    Dim selrow(7) As String ' Floating X/O row storage
    Dim key As ConsoleKeyInfo ' Key pressed (L/R/D)
    Dim selrowindex As Integer = 1 ' Floating X/O position
    Dim turns As Integer = 1 ' Current turn
    Dim currplayer As String = "X" ' Starting player
    Sub Main()
        Do
            ' Column number labels
            Console.ForegroundColor = ConsoleColor.Red
            ' Leaves a line empty for floating X/O later
            Console.WriteLine("      1   2   3   4   5   6   7" & vbCrLf & vbCrLf & "    ---------------------------")

            ' Loops for each row
            For i = 1 To 6
                Console.ForegroundColor = ConsoleColor.Red
                ' Prints row labels
                Console.Write((" " & i & " ").PadRight(4, " "))
                ' Loops for each column
                For x = 1 To 7
                    Console.ForegroundColor = ConsoleColor.Gray
                    ' Vertical seperators
                    Console.Write("|")

                    ' Colour text based on side
                    If table(i, x) = "X" Then
                        Console.ForegroundColor = ConsoleColor.DarkYellow
                    Else
                        Console.ForegroundColor = ConsoleColor.Green
                    End If

                    Console.Write(" " & (table(i, x) & " ").PadRight(2, " "))
                    Console.ForegroundColor = ConsoleColor.Gray

                Next
                ' Horizontal seperators
                Console.WriteLine(vbCrLf & "    ---------------------------")

            Next
            selrow(selrowindex) = currplayer

            ' Prints current turn
            Console.WriteLine("Turn " & turns)

            Do
                ' Moves back up to empty line/start of line
                Console.SetCursorPosition(4, 1)


                For i = 1 To 7
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.Write("|")

                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.Write(" " & (selrow(i) & " ").PadRight(2, " "))

                    Console.ForegroundColor = ConsoleColor.Gray



                Next

                ' Reacts to left/right/down arrows
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
                                selrowindex -= 2
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
        ' Player turn - keeps trying up column until empty slot
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

        ' Horizontal win checking
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

        ' Vertical win check
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
        ' Diagonal checking
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
        Console.WriteLine("Turns taken: " & (turns + 1))
        Console.ReadKey()
        End
    End Sub

End Module
