Console.WriteLine("Hello, AdventOfCode Day 5!");

var input = File.ReadAllText("input.txt");

var inputTest = """
$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k
""";

Console.WriteLine($"Part1 = {Part1(input)}");

Console.WriteLine($"Part2 = {Part2(input)}");

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

int Part1(string input)
{
    var lines = InputToLineList(input);

    return 0;
}

int Part2(string input)
{
    return 0;
}

class Folder
{
    public string Name { get; set; }

    public Folder Parent { get; set; }

    public List<Folder> SubFolders { get; set; }

    public List<int> FileSizes {get; set;}

    public Folder(string name, Folder parent)
    {
        Name = name;
        Parent = parent;
        SubFolders = new ();
        FileSizes = new ();
    }
}