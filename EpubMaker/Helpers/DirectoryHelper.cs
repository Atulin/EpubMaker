namespace EpubMaker.Helpers;

public static class DirectoryHelper
{
    public static void EnsureCreated(string directory)
    {
        if (Directory.Exists(directory)) return;
        Directory.CreateDirectory(directory);
    }

    public static void DeleteAll(string directory)
    {
        var di = new DirectoryInfo(directory);
        foreach (var file in di.EnumerateFiles())
        {
            file.Delete();
        }

        foreach (var dir in di.EnumerateDirectories())
        {
            dir.Delete(true);
        }
    }
}