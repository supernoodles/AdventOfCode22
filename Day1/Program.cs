// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var input = File.ReadAllText("input.txt");

using var reader = new StringReader(input);

var line = "";

int maxHealth = 0;

int currentHealth = 0;

while (line != null)
{
    line = reader.ReadLine();

    if(string.IsNullOrWhiteSpace(line))
    {
        if(currentHealth > maxHealth)
        {
            maxHealth = currentHealth;
        }

        currentHealth = 0;

        continue;
    }

    currentHealth += int.Parse(line);
}

if(currentHealth > maxHealth)
{
    maxHealth = currentHealth;
}

Console.WriteLine(maxHealth);