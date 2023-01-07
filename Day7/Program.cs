Console.WriteLine("Hello, AdventOfCode Day 7!");

var input = System.IO.File.ReadAllText("input.txt");

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

var root = ParseSession(InputToLineList(input)); 

Console.WriteLine($"Part1 = {Part1(root)}");

Console.WriteLine($"Part2 = {Part2(root)}");

List<string> InputToLineList(string input) => 
    input
        .Split('\n')
        .Where(_ => !string.IsNullOrWhiteSpace(_))
        .Select(_ => _.Replace("\r", ""))
        .ToList();

int Part1(Folder root) =>
    FindTotalWhereFolderLt100000(root);

int Part2(Folder root)
{
    var required = 30000000 - (70000000 - root.TotalSize);

    Console.WriteLine($"Space required = {required}");

    List<Folder> candidates = FindByMinSize(required, root, new List<Folder>());

    return candidates.MinBy(_ => _.TotalSize).TotalSize; 
}

Folder ParseSession(IList<string> lines)
{
    Folder root = new ("/", null);

    var currentFolder = root;

    foreach (var line in lines)
    {
        var lineParts = line.Split(" ");

        if (line == "$ cd /" || line == "$ ls")
        {
            continue;
        }

        if (line.StartsWith("dir "))
        {
            var folder = lineParts[1];
            currentFolder.SubFolders.Add(new Folder(folder, currentFolder));
            continue;
        }

        if (line.StartsWith("$ cd "))
        {
            var folder = lineParts[2];

            currentFolder = folder == ".."
                ? currentFolder.Parent
                : currentFolder = currentFolder.SubFolders.Where(f => f.Name == folder).First();
            
            continue;
        }

        var file = new File(lineParts[1], int.Parse(lineParts[0]));
        currentFolder.Files.Add(file);
    }

    root.CalcSize();

    return root;
}

int FindTotalWhereFolderLt100000(Folder folder)
{
    var subs = folder.SubFolders.Sum(_ => FindTotalWhereFolderLt100000(_));

    return folder.TotalSize < 100000
        ? folder.TotalSize + subs
        : subs;
}

List<Folder> FindByMinSize(int minSize, Folder folder, List<Folder> candidates)
{
    foreach (var sub in folder.SubFolders)
    {
        FindByMinSize(minSize, sub, candidates);
    }

    if (folder.TotalSize >= minSize)
    {
        candidates.Add(folder);
    }

    return candidates;
}

class Folder
{
    public string Name { get; set; }

    public Folder? Parent { get; set; }

    public List<Folder> SubFolders { get; set; }

    public List<File> Files {get; set;}

    public int TotalSize { get; set; }

    public Folder(string name, Folder? parent)
    {
        Name = name;
        Parent = parent;
        SubFolders = new ();
        Files = new ();
    }

    public int CalcSize()
    {
        TotalSize = Files.Sum(_ => _.Size) + SubFolders.Sum(_ => _.CalcSize());
        return TotalSize;
    }
}

record File (string Name, int Size);
