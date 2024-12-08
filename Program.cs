﻿using FountainOfObjects;

int userChoice;

Console.WriteLine("Enter the maze!");
string? userInput;
do
{
    Console.WriteLine("Do you want to play in a small, medium or large maze? ");
    Console.WriteLine("1. Small");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Large");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out userChoice) || userChoice is < 1 or > 3);

var mazeSize = (userChoice + 1) * 2;
var maze = new Maze(mazeSize);

var playerRow = 0;
var playerColumn = 0;
while (!maze.IsFountainActivated || maze.Rooms[playerRow, playerColumn] != Content.Exit)
{
    maze.PrintLocationContent(playerRow, playerColumn);
    var userAction = GetUserAction();
    switch (userAction)
    {
        case "move east":
            playerColumn = Math.Clamp(++playerColumn, 0, mazeSize - 1);
            if (playerColumn == mazeSize - 1)
                Console.WriteLine("You've reached the eastern wall.");
            break;
        case "move north":
            playerRow = Math.Clamp(--playerRow, 0, mazeSize - 1);
            if (playerRow == 0)
                Console.WriteLine("You've reached the northern wall.");
            break;
        case "move south":
            playerRow = Math.Clamp(++playerRow, 0, mazeSize - 1);
            if (playerRow == mazeSize - 1)
                Console.WriteLine("You've reached the southern wall.");
            break;
        case "move west":
            playerColumn = Math.Clamp(--playerColumn, 0, mazeSize - 1);
            if (playerColumn == 0)
                Console.WriteLine("You've reached the southern wall.");
            break;
        case "enable fountain":
            if (maze.Rooms[playerRow, playerColumn] == Content.Fountain)
                maze.IsFountainActivated = true;
            else
                Console.WriteLine("The fountain is not in this room!");
            break;
        case "help":
            ShowHelpText();
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }

    if (maze.Rooms[playerRow, playerColumn] == Content.Maelstrom)
    {
        (int row, int column) maelstrom = (playerRow, playerColumn);
        Console.WriteLine("Oh no! you ran into a maelstrom!");
        playerRow = Math.Clamp(playerRow + 2, 0, mazeSize - 1);
        playerColumn = Math.Clamp(++playerColumn, 0, mazeSize - 1);

        maelstrom.row = Math.Clamp(maelstrom.row - 2, 0, mazeSize - 1);
        maelstrom.column = Math.Clamp(--maelstrom.column, 0, mazeSize - 1);
        Console.WriteLine($"The player got sneezed by a maelstrom to (Row={playerRow} Column={playerColumn})");
    }
}

Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
Console.WriteLine("You win!");
return;


string GetUserAction()
{
    string? userAction;
    
    do
    {
        Console.Write("What do you want to do? ");
        userAction = Console.ReadLine();
    } while (string.IsNullOrEmpty(userAction));

    return userAction;
}

void ShowHelpText()
{
    Console.WriteLine("List of valid actions:");
    Console.WriteLine("=======");
    Console.WriteLine("move (north|south|east|west)");
    Console.WriteLine("enable fountain");
    Console.WriteLine("=======");
}