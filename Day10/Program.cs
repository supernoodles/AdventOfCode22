Console.WriteLine("Hello, AdventOfCode Day 10!");

var input = System.IO.File.ReadAllLines("input.txt");

var inputTest = """
addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop
""";

//var input = inputTest.Split("\r\n");

Console.WriteLine($"Part1 = {Solve(input)}");

Console.WriteLine($"Part2 = {Solve(input)}");

int Solve(string[] inputLines)
{
    var X = 1;

    var runtimeResults =
        inputLines
            .Select(_ => new { Instruction = _[0..4], Arg = _.Count() > 4 ? int.Parse(_[5..]) : 0 })
            .SelectMany(operation =>
            {
                if (operation.Instruction == "noop")
                {
                    return new[] { X };
                }

                var oldX = X;
                X += operation.Arg;

                return new[] { oldX, oldX };
            })
            .ToList();

    return Enumerable.Range(0, 6)
        .Select(_ => 20 + _ * 40)
        .Select(_ => runtimeResults[_ - 1] * _)
        .Sum();
}