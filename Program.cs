string? userInput;
var availableMazeSizes = new Dictionary<int, int>
{
    { 1, 4 },
    { 2, 6 },
    { 3, 8 }
};
int userChoice;
do
{
    Console.WriteLine("Do you want to play in a small, medium or large game? ");
    Console.WriteLine("1. Small");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Large");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out userChoice) || userChoice is < 1 or > 3);

// used for maelstrom sneezes (all maelstroms sneeze due to allergies!)
var random = new Random();
var sizeOfMaze = availableMazeSizes[userChoice];

var fountainLocation = (0, 2);

var maelstromCount = sizeOfMaze == 8 ? 2 : 1;
var maelstroms = new (int y, int x)[maelstromCount];
(int y, int x) playerPos = (0, 0);

for (var i = 0; i < maelstromCount; i++)
{
    maelstroms[i] = CreateMaelstrom();
}

var isFountainActivated = false;
var playerIsAtExit = playerPos is { y: 0, x: 0 };
while (!isFountainActivated || !playerIsAtExit)
{
    DisplayLocationMessage(playerPos);
    var userAction = GetUserAction();
    switch (userAction)
    {
        case "move east":
            if (playerPos.x == sizeOfMaze - 1)
                Console.WriteLine("You've hit a wall. Cannot move east.");
            else
                playerPos.x += 1;
            break;
        case "move north":
            if (playerPos.y == 0)
                Console.WriteLine("You've hit a wall. Cannot move north.");
            else
                playerPos.y -= 1;
            break;
        case "move south":
            if (playerPos.y == sizeOfMaze - 1)
                Console.WriteLine("You've hit a wall. Cannot move south.");
            else
                playerPos.y += 1;
            break;
        case "move west":
            if (playerPos.x == 0)
                Console.WriteLine("You've hit a wall. Cannot move west.");
            else
                playerPos.x -= 1;
            break;
        case "enable fountain":
            if (playerPos == fountainLocation)
                if (isFountainActivated)
                    Console.WriteLine("You've already activated the fountain");
                else
                    isFountainActivated = true;
            break;
        case "help":
            DisplayCommandText();
            break;
        default:
            Console.WriteLine("Invalid command");
            break;
    }

    playerIsAtExit = playerPos is { y: 0, x: 0 };
    if (maelstroms.Any(m => m == playerPos))
    {
        Console.WriteLine("Oh no! you ran into a maelstrom!");
        var previousMaelstromLocation = playerPos;
        var nextMaelstromLocation = playerPos;
        playerPos.y = Math.Clamp(playerPos.y + 2, 0, sizeOfMaze);
        playerPos.x = Math.Clamp(playerPos.x + 1, 0, sizeOfMaze);
        
        nextMaelstromLocation.y = Math.Clamp(nextMaelstromLocation.y - 2, 0, sizeOfMaze);
        nextMaelstromLocation.x = Math.Clamp(nextMaelstromLocation.x - 1, 0, sizeOfMaze);

        if (maelstroms[0] == previousMaelstromLocation)
            maelstroms[0] = nextMaelstromLocation;
        else
            maelstroms[1] = nextMaelstromLocation;
        
        Console.WriteLine($"The player got sneezed by a maelstrom to location {playerPos}");
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

void DisplayCommandText()
{
    Console.WriteLine("List of valid actions:");
    Console.WriteLine("=======");
    Console.WriteLine("move (north|south|east|west)");
    Console.WriteLine("enable fountain");
    Console.WriteLine("=======");
}

void DisplayLocationMessage((int y, int x) playerLocation)
{
    Console.WriteLine($"You are in the room at (Row={playerLocation.y}, Column={playerLocation.x}).");
    if (playerLocation is { y: 0, x: 0 })
        Console.WriteLine("You see light coming from the cavern entrance.");
    else if (playerLocation is { y: 0, x: 2 })
        Console.WriteLine(isFountainActivated
            ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated!"
            : "You hear water dripping in this room. The Fountain of Objects is here!");
}

(int, int) CreateMaelstrom()
{
    (int y, int x) newMaelstrom = (0, 0);
    do
    {
        newMaelstrom.y = random.Next(sizeOfMaze);
        newMaelstrom.x = random.Next(sizeOfMaze);
    } while (newMaelstrom is { x: 0, y: 0 } && newMaelstrom != fountainLocation);

    return newMaelstrom;
}
