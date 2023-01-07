Console.WriteLine("Hello, AdventOfCode Day 8!");

var input = System.IO.File.ReadAllText("input.txt");

var inputTest = """
30373
25512
65332
33549
35390
""";

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(inputTest)}");

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

int Part1(string input)
{
    var lines = InputToLineList(input);

    var width = lines[0].Length;
    var height = lines.Count;

    int[] treeHeights = new int[width*height];

    Console.WriteLine($"Width={width} Height={height}");

    var row = 0;
    foreach (var line in lines)
    {
        var column = 0;
        foreach(var treeHeight in line)
        {
            treeHeights[row*width + column] = int.Parse(treeHeight.ToString());
            ++column;
        }

        ++row;
    }

    var visiblePerimeter = 2 * width + 2 * height - 4;

    var visibleInternal = 0;

    for(row = 1; row < height - 1; ++row)
    {
        var allRow = treeHeights.Skip(row * width).Take(width).ToArray();

        for(var col = 1; col < width - 1; ++col)
        {
            var allCol = GetColumn(col, treeHeights, width, height);

            var currentTreeHeight = treeHeights[row * width + col];

            //Left
            var leftVisible = !allRow.Take(col).Any(_ => _ >= currentTreeHeight);

            //Right
            var rightVisible = !allRow.Skip(col + 1).Any(_ => _ >= currentTreeHeight);

            //Top
            var topVisible = !allCol.Take(row).Any(_ => _ >= currentTreeHeight);

            //Bottom
            var bottomVisible = !allCol.Skip(row + 1).Any(_ => _ >= currentTreeHeight);

            if(leftVisible || rightVisible || topVisible || bottomVisible)
            {
                ++visibleInternal;
            }
        }
    }

    return visibleInternal + visiblePerimeter;
}

int Part2(string input)
{
    return 0;
}

int[] GetColumn(int col, int[] array, int width, int height) =>
    Enumerable.Range(0, height)
        .Select(_ => array[width * _ + col])
        .ToArray();
