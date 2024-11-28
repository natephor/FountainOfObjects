namespace FountainOfObjects;

public class Maze
{
    public bool IsFountainActivated { get; set; }
    public Content[,] Rooms { get; }

    public Maze(int size)
    {
        Rooms = new Content[size, size];
        for (var i = 0; i < size; i++)
        for (var j = 0; j < size; j++)
            Rooms[i, j] = Content.Empty;

        IsFountainActivated = false;
        Rooms[0, 0] = Content.Exit;
        GenerateMaelstrom();
        if (Rooms.GetLength(0) == 8) // Size is large
            GenerateMaelstrom();
        Rooms[0, 2] = Content.Fountain;
    }

    public void GenerateMaelstrom()
    {
        var random = new Random();
        (int row, int column) maelstrom = (0, 0);
        do
        {
            maelstrom.row = random.Next(Rooms.GetLength(0));
            maelstrom.column = random.Next(Rooms.GetLength(0));
        } while (Rooms[maelstrom.row, maelstrom.column] != Content.Empty);

        Rooms[maelstrom.column, maelstrom.row] = Content.Maelstrom;
    }

    public void PrintLocationContent(int row, int column)
    {
        Console.WriteLine($"You are in the room at (Row={row}, Column={column}).");
        if (Rooms[row, column] == Content.Exit)
            Console.WriteLine("You see light coming from the cavern entrance.");
        else if (Rooms[row, column] == Content.Fountain)
            Console.WriteLine(IsFountainActivated
                ? "You hear the rushing waters from the Fountain of Objects. It has been reactivated!"
                : "You hear water dripping in this room. The Fountain of Objects is here!");
    }
}