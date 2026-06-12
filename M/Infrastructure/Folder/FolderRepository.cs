using M.Domain.Folder;

namespace M.Infrastructure.Folder;

public sealed class FolderRepository : IFolderRepository
{
    public bool Create(string path, string name)
    {
        if (!Directory.Exists(path))
            return false;

        var newPath = Path.Combine(path, name);
        if (Directory.Exists(newPath))
            return true;

        Directory.CreateDirectory(newPath);
        return true;
    }

    public bool Move(string origin, string destination)
    {
        if (!Directory.Exists(origin) || !Directory.Exists(destination))
            return false;

        var finalDestination = Path.Combine(destination, Path.GetFileName(origin));
        if (Directory.Exists(finalDestination))
            return false;

        Directory.Move(origin, finalDestination);
        return true;
    }

    public bool Delete(string path)
    {
        if (!Directory.Exists(path))
            return false;

        Directory.Delete(path, recursive: true);
        return true;
    }

    public bool DeleteSubFolders(string path)
    {
        foreach (var folder in Directory.GetDirectories(path))
            Directory.Delete(folder, recursive: true);

        return true;
    }

    public bool DeleteFiles(string path)
    {
        foreach (var file in Directory.GetFiles(path))
            System.IO.File.Delete(file);

        return true;
    }

    public bool CopyFiles(string origin, string destination, bool cut = false)
    {
        if (!Directory.Exists(origin) || !Directory.Exists(destination))
            return false;

        foreach (var file in Directory.GetFiles(origin))
        {
            var dest = Path.Combine(destination, Path.GetFileName(file));
            if (cut)
                System.IO.File.Move(file, dest);
            else
                System.IO.File.Copy(file, dest, overwrite: true);
        }

        return true;
    }
}
