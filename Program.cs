(int x, int y) player = (0, 0);
Console.WriteLine($"Player is at {player}");

var isFountainActivated = false;
var playerIsAtExit = player is { x: 0, y: 0 };
while (!isFountainActivated || !playerIsAtExit)
{
    Console.WriteLine($"You are in the room at (Row={player.x}, Column={player.y}).");
    if (player is { x: 0, y: 0 })
        Console.WriteLine("You see light coming from the cavern entrance.");
    else if (player is { x: 0, y: 2 })
        Console.WriteLine(isFountainActivated
            ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated!"
            : "You hear water dripping in this room. The Fountain of Objects is here!");
    var userAction = GetUserAction();
    switch (userAction)
    {
        case "move east":
            if (player.y == 3)
                Console.WriteLine("You've hit a wall. Cannot move east.");
            else
                player.y += 1;
            break;
        case "move north":
            if (player.x == 0)
                Console.WriteLine("You've hit a wall. Cannot move north.");
            else
                player.x -= 1;
            break;
        case "move south":
            if (player.x == 3)
                Console.WriteLine("You've hit a wall. Cannot move south.");
            else
                player.x += 1;
            break;
        case "move west":
            if (player.y == 0)
                Console.WriteLine("You've hit a wall. Cannot move west.");
            else
                player.y -= 1;
            break;
        case "enable fountain":
            if (player is { x: 0, y: 2 })
            {
                if (isFountainActivated)
                    Console.WriteLine("You've already activated the fountain");
                else
                    isFountainActivated = true;
            }

            break;
        default:
            Console.WriteLine("Unknown command.");

            Console.WriteLine("List of valid actions:");
            Console.WriteLine("=======");
            Console.WriteLine("move north/south/east/west");
            Console.WriteLine("enable fountain");
            Console.WriteLine("=======");
            break;
    }

    playerIsAtExit = player is { x: 0, y: 0 };
}

Console.WriteLine("The Fountain of Objects has been reactivated, and you have escaped with your life!");
Console.WriteLine("You win!");



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