string? userInput;
// Declared for readability
var fountainLocation = (0, 2);
var mazeEntrancePos = (0, 0);
var availableMazeSizes = new Dictionary<int, int>
{
    { 1, 4 },
    { 2, 6 },
    { 3, 8 }
};
int userChoice;
var random = new Random();

Console.WriteLine("Enter the maze!");
do
{
    Console.WriteLine("Do you want to play in a small, medium or large maze? ");
    Console.WriteLine("1. Small");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Large");
    userInput = Console.ReadLine();
} while (!int.TryParse(userInput, out userChoice) || userChoice is < 1 or > 3);

var sizeOfMaze = availableMazeSizes[userChoice];

// For large mazes we need 2 maelstroms
var maelstromCount = sizeOfMaze == 8 ? 2 : 1;
var maelstroms = new (int row, int column)[maelstromCount];

for (var i = 0; i < maelstromCount; i++)
{
    maelstroms[i] = CreateMaelstrom();
}

var isFountainActivated = false;
(int row, int column) currentPlayerPos = (0, 0);
var playerIsAtExit = currentPlayerPos == mazeEntrancePos;
while (!isFountainActivated || !playerIsAtExit)
{
    DisplayLocationMessage(currentPlayerPos);
    var userAction = GetUserAction();
    switch (userAction)
    {
        case "move east":
            if (currentPlayerPos.column == sizeOfMaze - 1)
                Console.WriteLine("You've hit a wall. Cannot move east.");
            else
                currentPlayerPos.column += 1;
            break;
        case "move north":
            if (currentPlayerPos.row == 0)
                Console.WriteLine("You've hit a wall. Cannot move north.");
            else
                currentPlayerPos.row -= 1;
            break;
        case "move south":
            if (currentPlayerPos.row == sizeOfMaze - 1)
                Console.WriteLine("You've hit a wall. Cannot move south.");
            else
                currentPlayerPos.row += 1;
            break;
        case "move west":
            if (currentPlayerPos.column == 0)
                Console.WriteLine("You've hit a wall. Cannot move west.");
            else
                currentPlayerPos.column -= 1;
            break;
        case "enable fountain":
            if (currentPlayerPos == fountainLocation)
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

    playerIsAtExit = currentPlayerPos == mazeEntrancePos;
    if (maelstroms.Any(m => m == currentPlayerPos))
    {
        Console.WriteLine("Oh no! you ran into a maelstrom!");
        var previousMaelstromLocation = currentPlayerPos;
        var nextMaelstromLocation = currentPlayerPos;
        currentPlayerPos.row = Math.Clamp(currentPlayerPos.row + 2, 0, sizeOfMaze);
        currentPlayerPos.column = Math.Clamp(currentPlayerPos.column + 1, 0, sizeOfMaze);
        
        nextMaelstromLocation.row = Math.Clamp(nextMaelstromLocation.row - 2, 0, sizeOfMaze);
        nextMaelstromLocation.column = Math.Clamp(nextMaelstromLocation.column - 1, 0, sizeOfMaze);

        if (maelstroms[0] == previousMaelstromLocation)
            maelstroms[0] = nextMaelstromLocation;
        else
            maelstroms[1] = nextMaelstromLocation;
        
        Console.WriteLine($"The player got sneezed by a maelstrom to location {currentPlayerPos}");
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

void DisplayLocationMessage((int row, int column) playerLocation)
{
    Console.WriteLine($"You are in the room at (Row={playerLocation.row}, Column={playerLocation.column}).");
    if (playerLocation == mazeEntrancePos)
        Console.WriteLine("You see light coming from the cavern entrance.");
    else if (playerLocation == fountainLocation)
        Console.WriteLine(isFountainActivated
            ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated!"
            : "You hear water dripping in this room. The Fountain of Objects is here!");
}

(int, int) CreateMaelstrom()
{
    (int row, int column) newMaelstrom = (0, 0);
    do
    {
        newMaelstrom.row = random.Next(sizeOfMaze);
        newMaelstrom.column = random.Next(sizeOfMaze);
    } while (newMaelstrom == mazeEntrancePos || newMaelstrom == fountainLocation);

    return newMaelstrom;
}
