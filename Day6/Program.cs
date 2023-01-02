Console.WriteLine("Hello, AdventOfCode Day 6!");

var input = File.ReadAllText("input.txt");

var inputTest = """
mjqjpqmgbljsphdztnvjfqwrcgsmlb
""";

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    for (var index = 0; index < input.Length; ++index)
    {
        var markerCandidate = input.Skip(index).Take(4);

        if(!markerCandidate.GroupBy(_ => _).Any(_ => _.Count() > 1))
        {
            return index + 4;
        }
    }
    
    return 0;
}

int Part2(string input)
{
    return 0;
}