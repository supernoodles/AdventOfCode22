using System.Text.RegularExpressions;

Console.WriteLine("Hello, AdventOfCode Day 5!");

Regex matcher = new Regex(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled);

var input = File.ReadAllText("input.txt");

var inputTest = """
    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2
""";

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

(List<Stack<string>> stacks, int currentLine) ReadMap(List<string> lines)
{
    var firstLine = lines[0];

    var stackCount = (int)Math.Ceiling((decimal)firstLine.Length / 4m);

    var lists = new List<List<string>>(stackCount);
    var stacks = new List<Stack<string>>(stackCount);

    for (var i = 0; i < stackCount; ++i)
    {
        lists.Add(new List<string>());
        stacks.Add(null);
    }

    var currentLine = 0;

    while (true)
    {
        var line = lines[currentLine++];

        if (line.StartsWith(" 1   2"))
        {
            break;
        }

        for (var index = 0; index < stackCount; ++index)
        {
            var slot = line.Substring(4 * index + 1, 1);

            if (slot == " ")
            {
                continue;
            }

            lists[index].Add(slot);
        }
    }

    for (var index = 0; index < stackCount; ++index)
    {
        lists[index].Reverse();
        stacks[index] = new Stack<string>(lists[index]);
    }

    return (stacks, currentLine);
}

(int number, int from, int to) ParseInstruction(string line)
{
    var match = matcher.Match(line);

    var number = int.Parse(match.Groups[1].Value);
    var from = int.Parse(match.Groups[2].Value) - 1;
    var to = int.Parse(match.Groups[3].Value) - 1;

    return (number, from, to);
}

string TopMostCrates(List<Stack<string>> stacks) => string.Concat(stacks.Select(_ => _.Peek()));

string Part1(string input)
{
    var lineList = InputToLineList(input);

    var (stacks, currentLine) = ReadMap(lineList);

    foreach (var line in lineList.Skip(currentLine))
    {
        var (number, from, to) = ParseInstruction(line);

        for (int step = 0; step < number; ++step)
        {
            stacks[to].Push(stacks[from].Pop());
        }
    }

    return TopMostCrates(stacks);
}

string Part2(string input)
{
    var lineList = InputToLineList(input);

    var (stacks, currentLine) = ReadMap(lineList);

    List<string> removedCrates = new();

    foreach (var line in lineList.Skip(currentLine))
    {
        var (number, from, to) = ParseInstruction(line);

        if(number == 1)
        {
            stacks[to].Push(stacks[from].Pop());
            continue;
        }

        removedCrates.Clear();

        do
        {
            removedCrates.Add(stacks[from].Pop());
            --number;
        } while (number > 0);

        removedCrates.Reverse();

        foreach (var crate in removedCrates)
        {
            stacks[to].Push(crate);
        }
    }

    return TopMostCrates(stacks);
}