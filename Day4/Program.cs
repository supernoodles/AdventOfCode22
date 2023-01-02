using System.Text.RegularExpressions;

Console.WriteLine("Hello, AdventOfCode Day 4!");

var input = File.ReadAllText("input.txt");

var inputTest = """
2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8
""";

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    Regex Input = new Regex(@"(\d*)-(\d*),(\d*)-(\d*)", RegexOptions.Compiled);
    
    var pairs = input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .ToList();

    var count = 0;

    foreach (var pair in pairs)
    {
        var match = Input.Match(pair);

        var elf1Min = int.Parse(match.Groups[1].Value);
        var elf1Max = int.Parse(match.Groups[2].Value);
        var elf2Min = int.Parse(match.Groups[3].Value);
        var elf2Max = int.Parse(match.Groups[4].Value);

        if (elf1Min >= elf2Min && elf1Max <= elf2Max
            || elf2Min >= elf1Min && elf2Max <= elf1Max)
        {
            ++count;
        }
    }

    return count;
}

int Part2(string input)
{
    Regex Input = new Regex(@"(\d*)-(\d*),(\d*)-(\d*)", RegexOptions.Compiled);
    
    var pairs = input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .ToList();

    var count = 0;

    foreach (var pair in pairs)
    {
        var match = Input.Match(pair);

        var elf1Min = int.Parse(match.Groups[1].Value);
        var elf1Max = int.Parse(match.Groups[2].Value);
        var elf2Min = int.Parse(match.Groups[3].Value);
        var elf2Max = int.Parse(match.Groups[4].Value);

        if ((elf1Min <= elf2Min && elf1Max >= elf2Min) 
            || elf2Min <= elf1Min && elf2Max >= elf1Min)
        {
            ++count;
        }        
    }

    return count;
}
