Console.WriteLine("Hello, AdventOfCode Day 3!");

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

    var totalPriority = 0;

    foreach (var rucksack in rucksacks)
    {
        var pocketSize = rucksack.Length / 2;

        var firstCompartment = rucksack.Take(pocketSize);
        var secondCompartment = rucksack.Skip(pocketSize);

        var common = firstCompartment.Intersect(secondCompartment).FirstOrDefault();

        totalPriority += char.IsUpper(common)
            ? common - 'A' + 27
            : common - 'a' + 1;
    }

    return totalPriority;
}

int Priority(char letter) => 
    char.IsUpper(letter)
        ? letter - 'A' + 27
        : letter - 'a' + 1;

int Part2(string input)
{
    var rucksacks = input
     .Split('\n')
     .Where(_ => !string.IsNullOrWhiteSpace(_))
     .ToList();

    var groups = rucksacks.Count() / 3;

    var totalPriority =
        Enumerable.Range(0, groups)
            .Select(index => {
                var group = rucksacks.Skip(index * 3).Take(3);
                
                var common = group.First()
                    .Intersect(group.Skip(1).First())
                    .Intersect(group.Skip(2).First())
                    .FirstOrDefault();
                
                return Priority(common);
            })
            .Sum();

    return totalPriority;
}
