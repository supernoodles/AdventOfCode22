Console.WriteLine("Hello, AdventOfCode Day 9!");

var input = System.IO.File.ReadAllText("input.txt");

var inputTest = """
R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2
""";

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

var lineList = InputToLineList(input);//Test);

HashSet<Position> Visited = new ();

Console.WriteLine($"Part1 = {Part1(lineList, Visited)}");

Console.WriteLine($"Part2 = {Part2(lineList)}");

Position FindTail(Position head, Position tail)
{
    if (head.Y == tail.Y)
    {
        if(Math.Abs(head.X - tail.X) == 2)
        {
            tail = tail with { X = head.X < tail.X ? tail.X - 1 : tail.X + 1 };
        }

        return tail;
    }

    if (head.X == tail.X)
    {
        if(Math.Abs(head.Y - tail.Y) == 2)
        {
            tail = tail with { Y = head.Y < tail.Y ? tail.Y - 1 : tail.Y + 1 };
        }

        return tail;
    }

    if (Math.Abs(head.Y - tail.Y) == 2 || Math.Abs(head.X - tail.X) == 2)
    {
        tail = tail with { 
            X = head.X < tail.X ? tail.X - 1 : tail.X + 1, 
            Y = head.Y < tail.Y ? tail.Y - 1 : tail.Y + 1 
        };

        return tail;
    }

    return tail;
}

int Part1(List<string> inputLines, HashSet<Position> visited)
{
    Position head = new (0, 0);
    Position tail = new (0, 0);

    foreach (var line in inputLines)
    {
        var direction = line[0];
        var number = int.Parse(line[2..]);

        for (int repeat = 0; repeat < number; ++repeat)
        {
            switch(direction)
            {
                case 'U':
                    head = head with { Y = head.Y + 1};
                    break;
            
                case 'D':
                    head = head with { Y = head.Y - 1};
                    break;

                case 'L':
                    head = head with { X = head.X - 1};
                    break;

                case 'R':
                    head = head with { X = head.X + 1};
                    break;
            }

            tail = FindTail(head, tail);

            visited.Add(tail);
        }
    }
 
    Console.WriteLine(head);

    return visited.Count();
}

string Part2(List<string> inputLines)
{
    return "";
}

record Position(int X, int Y);