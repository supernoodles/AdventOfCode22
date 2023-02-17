Console.WriteLine("Hello, AdventOfCode Day 9!");

var input = System.IO.File.ReadAllLines("input.txt");

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

HashSet<Position> Visited = new();

Console.WriteLine($"Part1 = {Part1(input, Visited)}");

Console.WriteLine($"Part2 = {Part2(input)}");

Position FindNextKnotPosition(Position current, Position next)
{
    var deltaX = current.X - next.X;
    var deltaY = current.Y - next.Y;

    if (Math.Abs(deltaY) < 2 && Math.Abs(deltaX) < 2)
    {
        return next;
    }

    next = next with
    {
        X = next.X + Math.Sign(deltaX),
        Y = next.Y + Math.Sign(deltaY)
    };

    return next;
}

int Part1(string[] inputLines, HashSet<Position> visited)
{
    Dictionary<char, Func<Position, Position>> movements = new Dictionary<char, Func<Position, Position>>()
    {
        {'U', _ => _ with { Y = _.Y + 1 }},
        {'D', _ => _ with { Y = _.Y - 1 }},
        {'L', _ => _ with { X = _.X + 1 }},
        {'R', _ => _ with { X = _.X - 1 }}
    };

    Position head = new(0, 0);
    Position tail = new(0, 0);

    inputLines
        .Select(line =>
            new { Direction = line[0], Number = int.Parse(line[2..]) })
        .SelectMany(instruction =>
            Enumerable.Range(0, instruction.Number)
                .Select(_ =>
                {
                    head = movements[instruction.Direction](head);

                    tail = FindNextKnotPosition(head, tail);

                    return tail;
                })
        )
        .Aggregate(visited, (set, position) =>
            {
                set.Add(position);
                return set;
            });

    return visited.Count();
}

string Part2(string[] inputLines)
{
    return "";
}

record Position(int X, int Y);