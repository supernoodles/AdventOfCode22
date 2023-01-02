using System.Text.RegularExpressions;

Console.WriteLine("Hello, AdventOfCode Day 5!");

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

string Part1(string input)
{
    var lines = 
        input.Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r",""))
        .ToList();

    var firstLine = lines[0];

    var stackCount = (int)Math.Ceiling((decimal)firstLine.Length / 4m);

    var lists = new List<List<string>>(stackCount);

    var stacks = new List<Stack<string>>(stackCount);

    for (var i = 0; i < stackCount; ++i)
    {
        lists.Add(new List<string>());
        stacks.Add(new Stack<string>());
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

            if(slot == " ")
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

    Regex matcher = new Regex(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled);

    foreach(var line in lines.Skip(currentLine))
    {
        var match = matcher.Match(line);

        if(!match.Success)
        {
            continue;
        }

        var number = int.Parse(match.Groups[1].Value);
        var from = int.Parse(match.Groups[2].Value) - 1;
        var to = int.Parse(match.Groups[3].Value) - 1;

        for(int step = 0; step < number; ++step)
        {
            stacks[to].Push(stacks[from].Pop());
        }
    } 

    return string.Concat(stacks.Select(_ => _.Peek()));
}

string Part2(string input)
{
    var lines = 
        input.Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r",""))
        .ToList();

    var firstLine = lines[0];

    var stackCount = (int)Math.Ceiling((decimal)firstLine.Length / 4m);

    var lists = new List<List<string>>(stackCount);

    var stacks = new List<Stack<string>>(stackCount);

    for (var i = 0; i < stackCount; ++i)
    {
        lists.Add(new List<string>());
        stacks.Add(new Stack<string>());
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

            if(slot == " ")
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

    Regex matcher = new Regex(@"move (\d*) from (\d*) to (\d*)", RegexOptions.Compiled);

    foreach(var line in lines.Skip(currentLine))
    {
        var match = matcher.Match(line);

        if(!match.Success)
        {
            continue;
        }

        var number = int.Parse(match.Groups[1].Value);
        var from = int.Parse(match.Groups[2].Value) - 1;
        var to = int.Parse(match.Groups[3].Value) - 1;

        List<string> removed = new ();

        for(int step = 0; step < number; ++step)
        {
            removed.Add(stacks[from].Pop());
        }

        removed.Reverse();

        foreach (var crate in removed)
        {
            stacks[to].Push(crate);
        }

    } 

    return string.Concat(stacks.Select(_ => _.Peek()));
}