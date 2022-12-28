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

// X Lose 0     Rock        1
// Y Draw 3     Paper       2
// Z Win  6     Scissors    3

//              Win         Draw        Lose
// A Rock       Paper       Rock        Scissors
// B Paper      Scissors    Paper       Rock
// C Scissors   Rock        Scissors    Paper

int Part2(string input)
{
    Dictionary<string, int> Map = new() {
        {"A X", 3},
        {"B X", 1},
        {"C X", 2},
        {"A Y", 1 + 3},
        {"B Y", 2 + 3},
        {"C Y", 3 + 3},
        {"A Z", 2 + 6},
        {"B Z", 3 + 6},
        {"C Z", 1 + 6}
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

        totalScore += Map[line];
    }

    return totalScore;
}