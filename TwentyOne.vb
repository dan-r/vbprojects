Module Module1
    REM I am side 0, computer is side 1

    REM Array of posessed cards
    Dim cardarray(1, 13, 1) As String
    REM For random functions. Future: Maybe try and add abit more entropy?
    Dim random As New Random
    REM Current index of cardarray that each player is on
    Dim index() As Integer = {0, 0}
    Dim possiblecards(12, 1) As String
    REM How many rounds to play
    Dim rounds As Integer

    Sub Main()
        REM Declare all the cards that we can possibly be dealt
        possiblecards = {{"One", 1}, {"Two", 2}, {"Three", 3}, {"Four", 4}, {"Five", 5}, {"Six", 6}, {"Seven", 7}, {"Eight", 8}, {"Nine", 9}, {"Ten", 10}, {"Jack", 10}, {"Queen", 10}, {"King", 10}, {"Ace", 1}}

        Console.Write("How many rounds would you like to play? ")
        rounds = Console.ReadLine()
        Console.Clear()

        REM Lets go
        play()

    End Sub
    Sub play()
        REM Deal my cards
        deal(2, 0, False)

        REM Deal the computers cards
        deal(2, 1, False)

        REM List out initial cards
        listcards(0, False)
        listcards(1, False)

        For i = 1 To rounds
            REM Clear for prettiness
            Console.Clear()

            REM List all cards
            listcards(0, False)
            listcards(1, False)

            REM Take user input
            action()
            aiturn()
            REM Wait for input before clearing
            Console.ReadKey()
        Next

        REM Forces game over
        check(True)

    End Sub
    Sub aiturn()
        Dim total As Integer = 0
        Dim over As Integer
        Dim possibility As Single

        REM Count out the computers cards
        For i = 0 To index(1) - 1
            total += cardarray(1, i, 1)
        Next

        REM Counts how many cards are over 21-total
        For i = 0 To 13
            If possiblecards(i, 1) > 21 - total Then
                over += 1
            End If
        Next

        possibility = (over / 13) * 100

        REM If the chance of going over 21 is less than 50, computer takes a card
        If possibility <= 50 Then
            deal(1, 1, True)
        Else
            'Console.WriteLine("The computer chose not to deal a card.")
        End If
        check()
    End Sub
    Sub action()
        Dim choice As Char
        Console.Write("(S)tick or (T)wist? ")

        REM Take user input and convert it to a char
        choice = Console.ReadKey().KeyChar
        Console.WriteLine(vbCrLf)

        If choice = "t" Then
            REM Deal the player one card
            deal(1, 0, True)
        Else
            Console.WriteLine("You have not taken a card")

        End If
        check()
    End Sub
    Sub check(Optional force = False)
        Dim both(1) As Integer

        REM Loop for each side
        For i = 0 To 1
            Dim total As Integer = 0
            REM Counts up the cards
            For x = 0 To index(i) - 1
                total += cardarray(i, x, 1)
            Next

            REM Condition 1: Over 21
            If total > 21 Then
                If i = 0 Then
                    listcards(1, True)
                Else
                    listcards(0, True)
                End If
            End If

            REM Condition 2: Equal 21
            If total = 21 Then
                If i = 0 Then
                    listcards(0, True)
                Else
                    listcards(1, True)
                End If
            End If
            both(i) = total
        Next

        REM Condition 3: Out of rounds
        If force = True Then
            If both(0) > both(1) Then
                listcards(0, True)
            Else
                listcards(1, True)
            End If
        End If

    End Sub

    Sub deal(num, side, verbose)
        REM Deals however many cards are required
        For i = 1 To num
            REM Picks a random number 0-13
            Dim chosen = random.Next(0, 14)

            REM Sets card name
            cardarray(side, index(side), 0) = possiblecards(chosen, 0)
            REM Sets card value
            cardarray(side, index(side), 1) = possiblecards(chosen, 1)

            REM Whether or not to announce the card that was picked
            If verbose And side = 0 Then
                Console.WriteLine("You were dealt a " & possiblecards(chosen, 0) & " (Value: " & possiblecards(chosen, 1) & ")")
            ElseIf verbose And side = 1 Then
                'Console.WriteLine("The computer chose to take a card and was dealt a " & possiblecards(chosen, 0) & " (Value: " & possiblecards(chosen, 1) & ")")
            End If

            index(side) += 1

        Next
    End Sub
    Sub listcards(side, endgame)
        REM Declare and init local total variable
        Dim total As Integer = 0

        If side = 0 And endgame = False Then
            REM List players cards
            Console.WriteLine("Your cards:" & vbCrLf)

            REM Loop through cards
            REM For each not used due to the bad array implementation in VB
            For i = 0 To index(side) - 1
                Console.WriteLine("Card: " & cardarray(side, i, 0) & vbCrLf & "Value: " & cardarray(side, i, 1) & vbCrLf)
                total += cardarray(side, i, 1)
            Next

            Console.WriteLine("You are currently on: " & total)
        ElseIf side = 1 And endgame = False Then
            REM Only give a total for the computer
            For i = 0 To index(side) - 1
                total += cardarray(side, i, 1)
            Next
            'Console.WriteLine("The computer is currently on: " & total & vbCrLf)
            Console.WriteLine("One card the computer has is a " & cardarray(1, 0, 0) & " (Value: " & cardarray(1, 0, 1) & ")" & vbCrLf)
        End If

        If endgame Then
            REM After user input, clear display 
            Console.ReadKey()
            Console.Clear()

            REM Show who wins
            If side = 0 Then
                Console.WriteLine("You won!")
            ElseIf side = 1 Then
                Console.WriteLine("You lost!")
            End If

            Console.Write(vbCrLf & "You finished on: ")

            REM Counts player score
            total = 0
            For i = 0 To index(0) - 1
                total += cardarray(0, i, 1)
            Next
            Console.Write(total)
            Console.Write(vbCrLf & "The computer finished on: ")

            REM Counts computer score
            total = 0
            For i = 0 To index(1) - 1
                total += cardarray(1, i, 1)
            Next
            Console.Write(total)

            REM Ends program after user input
            Console.ReadKey()
            End
        End If
    End Sub
End Module

