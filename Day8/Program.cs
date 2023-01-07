Console.WriteLine("Hello, AdventOfCode Day 8!");

var input = System.IO.File.ReadAllText("input.txt");

var inputTest = """
30373
25512
65332
33549
35390
""";

Dictionary<int,int[]> ColumnCache = new ();

var (treeMap, width, height) = ParseTreeMap(input);

Console.WriteLine($"Part1 = {Part1(treeMap, width, height)}");

Console.WriteLine($"Part2 = {Part2(treeMap, width, height)}");

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

(int[] map, int width, int height) ParseTreeMap(string input)
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

    return (treeHeights, width, height);
}

int Part1(int[] treeHeights, int width, int height)
{
    var visiblePerimeter = 2 * width + 2 * height - 4;

    var visibleInternal = 0;

    for(var row = 1; row < height - 1; ++row)
    {
        var currentRow = treeHeights.Skip(row * width).Take(width).ToArray();

        for(var col = 1; col < width - 1; ++col)
        {
            var currentCol = GetColumn(col, treeHeights, width, height);

            var currentTreeHeight = treeHeights[row * width + col];

            //Left
            var leftVisible = !currentRow.Take(col).Any(_ => _ >= currentTreeHeight);

            //Right
            var rightVisible = !currentRow.Skip(col + 1).Any(_ => _ >= currentTreeHeight);

            //Top
            var topVisible = !currentCol.Take(row).Any(_ => _ >= currentTreeHeight);

            //Bottom
            var bottomVisible = !currentCol.Skip(row + 1).Any(_ => _ >= currentTreeHeight);

            if(leftVisible || rightVisible || topVisible || bottomVisible)
            {
                ++visibleInternal;
            }
        }
    }

    return visibleInternal + visiblePerimeter;
}

int TreesSeen(IEnumerable<int> range, Func<int,int> height, int currentTreeHeight)
{
    var count = 0;
    foreach (var index in range)
    {
        ++count;

        if (height(index) >= currentTreeHeight)
        {
            break;
        }
    }

    return count;
}

int Part2(int[] treeHeights, int width, int height)
{
    List<int> scores = new ();

    for(var row = 1; row < height - 1; ++row)
    {
        var currentRow = treeHeights.Skip(row * width).Take(width).ToArray();

        for(var col = 1; col < width - 1; ++col)
        {
            var currentCol = GetColumn(col, treeHeights, width, height);

            var currentTreeHeight = treeHeights[row * width + col];

            var left = TreesSeen(Enumerable.Range(0, col).Reverse(), _ => currentRow[_], currentTreeHeight);
            var right = TreesSeen(Enumerable.Range(col + 1, width - col - 1), _ => currentRow[_], currentTreeHeight);
            var top = TreesSeen(Enumerable.Range(0, row).Reverse(), _ => currentCol[_], currentTreeHeight);
            var bottom = TreesSeen(Enumerable.Range(row + 1, height - row - 1), _ => currentCol[_], currentTreeHeight);

            scores.Add(left * right * top * bottom);
        }
    }

    return scores.Max();
}

int[] GetColumn(int col, int[] array, int width, int height)
{
    if(ColumnCache.TryGetValue(col, out var column))
    {
        return column;
    }

    ColumnCache[col] = Enumerable.Range(0, height)
        .Select(_ => array[width * _ + col])
        .ToArray();

    return ColumnCache[col];
}