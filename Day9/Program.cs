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

Console.WriteLine($"Part1 = {Solve(input, 2)}");

Console.WriteLine($"Part2 = {Solve(input, 10)}");

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

int Solve(string[] inputLines, int ropeLength)
{
    HashSet<Position> visited = new ();

    Dictionary<char, Func<Position, Position>> movements = new ()
    {
        {'U', _ => _ with { Y = _.Y + 1 }},
        {'D', _ => _ with { Y = _.Y - 1 }},
        {'L', _ => _ with { X = _.X + 1 }},
        {'R', _ => _ with { X = _.X - 1 }}
    };

    var rope = 
        Enumerable
            .Range(0, ropeLength)
            .Select(_ => new Position(0, 0))
            .ToArray();

    inputLines
        .Select(line =>
            new { Direction = line[0], Number = int.Parse(line[2..]) })
        .SelectMany(instruction =>
            Enumerable
                .Range(0, instruction.Number)
                .Select(_ =>
                {
                    rope[0] = movements[instruction.Direction](rope[0]);

                    Enumerable
                        .Range(1, ropeLength - 1)
                        .ToList()
                        .ForEach(next => 
                            rope[next] = FindNextKnotPosition(rope[next - 1], rope[next]));

                    return rope[ropeLength - 1];
                })
        )
        .Aggregate(visited, 
            (set, position) =>
            {
                set.Add(position);
                return set;
            });

    return visited.Count();
}

record Position(int X, int Y);