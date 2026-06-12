namespace M.Domain.Folder;

public interface IFolderRepository
{
    bool Create(string path, string name);
    bool Move(string origin, string destination);
    bool Delete(string path);
    bool DeleteSubFolders(string path);
    bool DeleteFiles(string path);
    bool CopyFiles(string origin, string destination, bool cut = false);
}
