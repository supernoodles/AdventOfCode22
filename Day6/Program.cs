Console.WriteLine("Hello, AdventOfCode Day 6!");

var input = File.ReadAllText("input.txt");

var inputTest = """
mjqjpqmgbljsphdztnvjfqwrcgsmlb
""";

Console.WriteLine($"Part1 = {Partn(input, 4)}");

Console.WriteLine($"Part2 = {Partn(input, 14)}");

int Partn(string input, int markerLength)
{
    for (var index = 0; index < input.Length; ++index)
    {
        var markerCandidate = input.Skip(index).Take(markerLength);

        if(!markerCandidate.GroupBy(_ => _).Any(_ => _.Count() > 1))
        {
            return index + markerLength;
        }
    }
    
    return 0;
}
