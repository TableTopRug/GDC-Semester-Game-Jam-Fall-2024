using System.Collections.Generic;
using Godot;


public static class FileSystemUtils
{
    public static string[] ScanDirForFileType(string directory, string extension) 
    {
        using var dir = DirAccess.Open(directory);
        dir.ListDirBegin();

        string fileName = dir.GetNext();
        List<string> names = new List<string>();

        while (fileName != "")
        {
            if (!dir.CurrentIsDir())
            {
                if (fileName.Substring(fileName.Length - extension.Length).Contains(extension)) 
                {
                    names.Add(fileName);
                }
            }

            fileName = dir.GetNext();
        }

        return names.ToArray();
    }
}