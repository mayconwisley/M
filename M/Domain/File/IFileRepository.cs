namespace M.Domain.File;

public interface IFileRepository
{
    bool Create(string directoryPath, string fileName);
    bool Move(string filePath, string destinationDirectory);
    bool Delete(string filePath);
    bool Copy(string filePath, string destinationDirectory, bool cut = false);
}
