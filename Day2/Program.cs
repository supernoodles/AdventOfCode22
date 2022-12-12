// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, AdventOfCode Day 2!");

var input = File.ReadAllText("input.txt");

// var input = """
//             A Y
//             B X
//             C Z
//             """;

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    Dictionary<char, char> Map = new() {
        {'A', 'X'},
        {'B', 'Y'},
        {'C', 'Z'}
    };

    Dictionary<char, int> Score = new() {
        {'X', 1},
        {'Y', 2},
        {'Z', 3}
    };

    Dictionary<char, char> Guide = new() {
        {'A', 'Y'},
        {'B', 'Z'},
        {'C', 'X'}
    };

    using var reader = new StringReader(input);

    var totalScore = 0;

    while (true)
    {
        var line = reader.ReadLine();

        if (line == null)
        {
            break;
        }

        var opponent = line[0];
        var ours = line[2];

        Console.WriteLine($"{opponent}-{ours}");

        totalScore += Score[ours];

        if (Map[opponent] == ours)
        {
            Console.WriteLine("Draw");
            totalScore += 3;
            continue;
        }

        if (Guide[opponent] == ours)
        {
            Console.WriteLine("Win");
            totalScore += 6;
        }

        Console.WriteLine(totalScore);
    }

    return totalScore;
}

int Part2(string input)
{
    return 0;
}