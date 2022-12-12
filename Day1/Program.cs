// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, AdventOfCode Day1!");

var input = File.ReadAllText("input.txt");

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

int Part1(string input)
{
    using var reader = new StringReader(input);

    var line = "";

    int maxHealth = 0;

    int currentHealth = 0;

    while (line != null)
    {
        line = reader.ReadLine();

        if (string.IsNullOrWhiteSpace(line))
        {
            if (currentHealth > maxHealth)
            {
                maxHealth = currentHealth;
            }

            currentHealth = 0;

            continue;
        }

        currentHealth += int.Parse(line);
    }

    if (currentHealth > maxHealth)
    {
        maxHealth = currentHealth;
    }

    return maxHealth;
}

int Part2(string input)
{
    using var reader = new StringReader(input);

    var line = "";

    List<int> elfHealth = new ();

    int currentHealth = 0;

    while (line != null)
    {
        line = reader.ReadLine();

        if (string.IsNullOrWhiteSpace(line))
        {
            elfHealth.Add(currentHealth);

            currentHealth = 0;

            continue;
        }

        currentHealth += int.Parse(line);
    }

    elfHealth.Add(currentHealth);

    return
        elfHealth.OrderByDescending(health => health)
        .Take(3)
        .Sum();
}