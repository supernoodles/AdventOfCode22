Console.WriteLine("Hello, AdventOfCode Day 2!");

var input = File.ReadAllText("input.txt");

// var input = """
// vJrwpWtwJgWrhcsFMMfFFhFp
// jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
// PmmdzqPrVvPwwTWBwg
// wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
// ttgJtRGJQctTZtZT
// CrZsJsPPZsGzwwsLwLmpwMDw
// """;

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    var rucksacks = input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .ToList();

    var score = 0;

    foreach (var rucksack in rucksacks)
    {
        var pocketSize = rucksack.Length / 2;

        var firstCompartment = rucksack.Take(pocketSize);
        var secondCompartment = rucksack.Skip(pocketSize);

        var common = firstCompartment.Intersect(secondCompartment).FirstOrDefault();

        score += char.IsUpper(common)
            ? common - 'A' + 27
            : common - 'a' + 1;
    }

    return score;
}

int Part2(string input)
{
    return 0;
}
