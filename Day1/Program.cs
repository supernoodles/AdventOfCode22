// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, AdventOfCode Day1!");

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    using var reader = new StringReader(input);

    var line = "";

    int maxCalories = 0;

    int currentElfCalories = 0;

    while (line != null)
    {
        line = reader.ReadLine();

        if (string.IsNullOrWhiteSpace(line))
        {
            if (currentElfCalories > maxCalories)
            {
                maxCalories = currentElfCalories;
            }

            currentElfCalories = 0;

            continue;
        }

        currentElfCalories += int.Parse(line);
    }

    if (currentElfCalories > maxCalories)
    {
        maxCalories = currentElfCalories;
    }

    return maxCalories;
}

int Part2(string input)
{
    using var reader = new StringReader(input);

    var line = "";

    List<int> elfCalories = new ();

    int currentElfCalories = 0;

    while (line != null)
    {
        line = reader.ReadLine();

        if (string.IsNullOrWhiteSpace(line))
        {
            elfCalories.Add(currentElfCalories);

            currentElfCalories = 0;

            continue;
        }

        currentElfCalories += int.Parse(line);
    }

    elfCalories.Add(currentElfCalories);

    return
        elfCalories.OrderByDescending(health => health)
        .Take(3)
        .Sum();
}