using M.Domain.File;
using SysFile = System.IO.File;

namespace M.Infrastructure.File;

public sealed class FileRepository : IFileRepository
{
    public bool Create(string directoryPath, string fileName)
    {
        if (!Directory.Exists(directoryPath))
            return false;

        var filePath = Path.Combine(directoryPath, fileName);
        if (SysFile.Exists(filePath))
            return true;

        using var _ = SysFile.Create(filePath);
        return true;
    }

    public bool Move(string filePath, string destinationDirectory)
    {
        if (!SysFile.Exists(filePath) || !Directory.Exists(destinationDirectory))
            return false;

        var destination = Path.Combine(destinationDirectory, Path.GetFileName(filePath));
        if (SysFile.Exists(destination))
            return false;

        SysFile.Move(filePath, destination);
        return true;
    }

    public bool Delete(string filePath)
    {
        if (!SysFile.Exists(filePath))
            return false;

        SysFile.Delete(filePath);
        return true;
    }

    public bool Copy(string filePath, string destinationDirectory, bool cut = false)
    {
        if (!SysFile.Exists(filePath) || !Directory.Exists(destinationDirectory))
            return false;

        var destination = Path.Combine(destinationDirectory, Path.GetFileName(filePath));

        if (cut)
            SysFile.Move(filePath, destination, overwrite: true);
        else
            SysFile.Copy(filePath, destination, overwrite: true);

        return true;
    }
}
