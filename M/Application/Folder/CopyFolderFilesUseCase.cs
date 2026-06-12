using M.Domain.Folder;

namespace M.Application.Folder;

public sealed class CopyFolderFilesUseCase(IFolderRepository repository)
{
    public bool Execute(string origin, string destination, bool cut = false)
    {
        if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(destination))
            return false;

        return repository.CopyFiles(origin, destination, cut);
    }
}
